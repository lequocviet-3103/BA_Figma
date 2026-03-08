using AIBA.Application.DTO;
using AIBA.Application.IService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIBA.Infrastructure.Service
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public GeminiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<BAResultDto> GenerateAnalysisAsync(string idea)
        {
            var apiKey = _config["Gemini:ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Gemini API Key is not configured. Please add 'Gemini:ApiKey' to appsettings.json");
            }

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            var prompt = $@"Analyze the following business idea and generate a comprehensive business analysis. 
Return ONLY a valid JSON object (no markdown, no code blocks) with these EXACT fields as strings (not arrays):

{{
  ""UserStories"": ""string with newline-separated user stories"",
  ""UseCases"": ""string with newline-separated use cases"",
  ""FunctionalRequirements"": ""string with newline-separated requirements"",
  ""DatabaseSchema"": ""string describing database schema with tables and relationships"",
  ""ApiSuggestions"": ""string with newline-separated API endpoint suggestions""
}}

IMPORTANT: All field values MUST be strings, not arrays. Use \n for line breaks within strings.

Business Idea: {idea}

Generate detailed and professional analysis.";

            var body = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync(url, body);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Gemini API failed: {response.StatusCode}. Response: {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);

            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            // Remove markdown code blocks if present
            var cleanedText = text?.Trim();
            if (string.IsNullOrEmpty(cleanedText))
            {
                throw new InvalidOperationException("Gemini returned empty response");
            }

            // Remove ```json and ``` markers
            if (cleanedText.StartsWith("```json"))
            {
                cleanedText = cleanedText.Substring(7);
            }
            else if (cleanedText.StartsWith("```"))
            {
                cleanedText = cleanedText.Substring(3);
            }

            if (cleanedText.EndsWith("```"))
            {
                cleanedText = cleanedText.Substring(0, cleanedText.Length - 3);
            }

            cleanedText = cleanedText.Trim();

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<BAResultDto>(cleanedText, options)
                    ?? throw new InvalidOperationException("Failed to deserialize response");
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Invalid JSON from Gemini. Raw text: {cleanedText}", ex);
            }
        }
    }
}