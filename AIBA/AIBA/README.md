# AIBA - AI Business Analysis Project

## Gi?i thi?u
AIBA là m?t ?ng d?ng API s? d?ng AI (Google Gemini) ?? phân tích yêu c?u nghi?p v? và t? ??ng t?o ra:
- User Stories
- Use Cases
- Functional Requirements
- Database Schema
- API Suggestions

## Yêu c?u h? th?ng
- .NET 9 SDK
- SQL Server (LocalDB ho?c SQL Server Express)
- Visual Studio 2022 ho?c VS Code
- Google Gemini API Key (mi?n phí t?i https://makersuite.google.com/app/apikey)

## Cài ??t và ch?y project

### Cách 1: S? d?ng PowerShell Script (Khuy?n ngh?)
```powershell
.\setup-and-run.ps1
```

### Cách 2: Th? công

#### 1. C?u hình Database
M? file `appsettings.json` và c?p nh?t connection string n?u c?n:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AIBA;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

#### 2. C?u hình Gemini API Key
C?p nh?t API key c?a Gemini trong file `appsettings.json`:
```json
"Gemini": {
  "ApiKey": "your-actual-gemini-api-key-here"
}
```

**Quan tr?ng:** Ph?i có API key h?p l? ?? s? d?ng endpoint `/api/Projects/analyze`

#### 3. Cài ??t EF Core Tools (n?u ch?a có)
```bash
dotnet tool install --global dotnet-ef
```

#### 4. T?o Database
```bash
dotnet restore
dotnet ef migrations add InitialCreate --project AIBA.Infrastructure --startup-project AIBA
dotnet ef database update --project AIBA.Infrastructure --startup-project AIBA
```

#### 5. Ch?y ?ng d?ng
```bash
dotnet run --project AIBA
```

#### 6. Truy c?p Swagger UI
Trình duy?t s? t? ??ng m? Swagger UI t?i:
```
https://localhost:7016/swagger
```
ho?c
```
http://localhost:5296/swagger
```

## API Endpoints

### POST /api/Projects/analyze
Phân tích yêu c?u và t?o d? án m?i

**Request Body:**
```json
{
  "projectName": "My Project",
  "idea": "Tôi mu?n xây d?ng m?t ?ng d?ng qu?n lý th? vi?n"
}
```

**Response:**
```json
{
  "projectId": "guid-here"
}
```

### GET /api/Projects/{id}
L?y thông tin d? án theo ID

**Response:**
```json
{
  "id": "guid",
  "name": "My Project",
  "createdAt": "2024-01-01T00:00:00Z",
  "analyses": [
    {
      "id": "guid",
      "idea": "Tôi mu?n xây d?ng m?t ?ng d?ng qu?n lý th? vi?n",
      "userStories": "...",
      "useCases": "...",
      "functionalRequirements": "...",
      "databaseSchema": "...",
      "apiSuggestions": "...",
      "createdAt": "2024-01-01T00:00:00Z"
    }
  ]
}
```

## C?u trúc Project

- **AIBA** - Web API project (ASP.NET Core)
- **AIBA.Domain** - Domain entities và interfaces
- **AIBA.Application** - Use cases, DTOs và services interfaces
- **AIBA.Infrastructure** - Implementation c?a repositories và external services

## Công ngh? s? d?ng

- .NET 9
- Entity Framework Core 9.0
- SQL Server
- Swagger/OpenAPI
- Google Gemini AI
