# H??ng d?n s? d?ng AIBA API v?i Swagger

## 1. Kh?i ??ng ?ng d?ng

Sau khi ch?y l?nh `dotnet run --project AIBA`, trình duy?t s? t? ??ng m? Swagger UI.

N?u không t? ??ng m?, truy c?p:
- HTTPS: https://localhost:7016/swagger
- HTTP: http://localhost:5296/swagger

## 2. API Endpoints

### 2.1. POST /api/Projects/analyze
**Ch?c n?ng:** Phân tích yêu c?u nghi?p v? b?ng AI và t?o project m?i

**Cách s? d?ng trong Swagger:**
1. Click vào endpoint `POST /api/Projects/analyze`
2. Click nút "Try it out"
3. Nh?p d? li?u m?u:

```json
{
  "projectName": "Library Management System",
  "idea": "Tôi mu?n xây d?ng m?t h? th?ng qu?n lý th? vi?n cho tr??ng ??i h?c. H? th?ng c?n qu?n lý sách, sinh viên m??n sách, tr? sách, và theo dõi sách quá h?n."
}
```

4. Click nút "Execute"
5. Xem k?t qu? tr? v? trong ph?n "Response body"

**Response m?u:**
```json
{
  "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "message": "Analysis completed successfully"
}
```

**L?u ý:**
- `projectName`: Tên d? án (b?t bu?c, 1-200 ký t?)
- `idea`: Mô t? ý t??ng (b?t bu?c, 10-5000 ký t?)
- **QUAN TR?NG**: Ph?i c?u hình Gemini API Key h?p l? trong `appsettings.json`

### 2.2. GET /api/Projects/{id}
**Ch?c n?ng:** L?y thông tin chi ti?t project và k?t qu? phân tích

**Cách s? d?ng trong Swagger:**
1. Click vào endpoint `GET /api/Projects/{id}`
2. Click nút "Try it out"
3. Nh?p `projectId` nh?n ???c t? b??c tr??c vào tr??ng `id`
4. Click nút "Execute"

**Response m?u:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Library Management System",
  "createdAt": "2024-01-20T10:30:00Z",
  "analyses": [
    {
      "id": "4fb85f64-5717-4562-b3fc-2c963f66afa7",
      "idea": "Tôi mu?n xây d?ng m?t h? th?ng qu?n lý th? vi?n...",
      "userStories": "1. Là m?t sinh viên, tôi mu?n tìm ki?m sách...\n2. Là th? th?, tôi mu?n...",
      "useCases": "UC1: Tìm ki?m sách\nUC2: M??n sách\n...",
      "functionalRequirements": "FR1: H? th?ng ph?i cho phép...\nFR2: H? th?ng ph?i...",
      "databaseSchema": "Table: Books\n- BookId (PK)\n- Title\n...",
      "apiSuggestions": "POST /api/books\nGET /api/books/{id}\n...",
      "createdAt": "2024-01-20T10:30:00Z"
    }
  ]
}
```

## 3. Test các tr??ng h?p l?i

### 3.1. Thi?u thông tin b?t bu?c
Request:
```json
{
  "projectName": "",
  "idea": ""
}
```

Response:
```json
{
  "errors": {
    "ProjectName": ["Project name is required"],
    "Idea": ["Idea is required"]
  }
}
```

### 3.2. Project không t?n t?i
GET /api/Projects/00000000-0000-0000-0000-000000000000

Response:
```json
{
  "error": "Project not found"
}
```

### 3.3. API Key không h?p l?
N?u Gemini API Key không ?úng ho?c ch?a c?u hình:

Response:
```json
{
  "error": "API request failed: Unauthorized"
}
```

## 4. Tips

1. **L?u ProjectId**: Sau khi t?o project, hãy copy `projectId` ?? l?y thông tin chi ti?t sau
2. **Gemini API Key**: L?y mi?n phí t?i https://makersuite.google.com/app/apikey
3. **Ý t??ng chi ti?t h?n = K?t qu? t?t h?n**: Mô t? chi ti?t ?? AI phân tích t?t h?n
4. **Th?i gian ph?n h?i**: Endpoint `/analyze` có th? m?t 5-15 giây do ph?i g?i Gemini API

## 5. X? lý l?i th??ng g?p

| L?i | Nguyên nhân | Gi?i pháp |
|------|-------------|-----------|
| 400 Bad Request | Validation th?t b?i | Ki?m tra d? li?u ??u vào |
| 404 Not Found | Project không t?n t?i | Ki?m tra l?i ProjectId |
| 500 Internal Server Error | Gemini API l?i ho?c DB l?i | Ki?m tra API Key và connection string |

## 6. Database

Xem d? li?u trong SQL Server:
```sql
USE AIBA;
SELECT * FROM Projects;
SELECT * FROM Analyses;
```

## 7. CORS

API ?ã ???c c?u hình CORS ?? cho phép g?i t? m?i origin. Có th? tích h?p v?i frontend:

```javascript
// Ví d? JavaScript
fetch('https://localhost:7016/api/Projects/analyze', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    projectName: 'My Project',
    idea: 'My idea description...'
  })
})
.then(response => response.json())
.then(data => console.log(data));
```
