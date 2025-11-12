# 🧩 ToDo App

Aplikacja w technologii **.NET 8 + Angular 17 + PostgreSQL**.  
Pokazuje integrację backendu z frontendem przy użyciu Entity Framework Core i HttpClient.

---

## 🧱 WYMAGANIA

| Narzędzie      | Wersja minimalna | Komenda sprawdzająca |
|----------------|------------------|------------------------|
| .NET SDK       | 8.0              | `dotnet --version`     |
| Node.js        | 18+              | `node -v`              |
| Angular CLI    | 17+              | `ng version`           |
| PostgreSQL     | 14+              | `psql --version`       |

---

## ⚙️ KONFIGURACJA BAZY DANYCH
```markdown
**1️⃣ Utwórz bazę danych w PostgreSQL:**
CREATE DATABASE mettec_db;

2️⃣ Skonfiguruj połączenie w pliku appsettings.Development.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=mettec_db;Username=postgres;Password=hasło_do_bazy_danych"
}

3️⃣ Utwórz i zaktualizuj migracje:
cd TodoApi/MettecApi
dotnet ef migrations add Init
dotnet ef database update

4️⃣ Dodaj przykładowe dane testowe:
INSERT INTO "MettecItems" ("Title", "Description", "IsDone")
VALUES ('Pierwsze zadanie', 'Testowy opis zadania', false);

---

## 🚀 BACKEND (.NET API)
cd TodoApi/MettecApi
dotnet restore
dotnet run --launch-profile "https"

Swagger → http://localhost:5001/swagger/index.html
API → http://localhost:5001/api/mettec

---

## 🌐 FRONTEND (Angular)
cd TodoFront
npm install
ng serve

---

## 🧪 TESTY (xUnit)
cd TodoApi/MettecApi
dotnet test

Testy sprawdzają:
pobieranie zadań (GET /api/mettec)
dodawanie (POST)
aktualizację statusu (PUT)

---

📦 Struktura projektu:
ToDo-App/
├─ TodoApi/      # Backend (.NET 8)
└─ TodoFront/    # Frontend (Angular 17)

👨 Autor:
Piotr Markiewicz – Fullstack Developer (.NET + Angular)

🛈 Uwaga dotycząca nazw w projekcie:
W projekcie pojawiają się przykładowe nazwy użyte wyłącznie jako identyfikatory techniczne w celach demonstracyjnych.
Nie mają one związku z żadnym rzeczywistym podmiotem ani produktem.
