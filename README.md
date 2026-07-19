# Conference Booking API

REST API для керування бронюванням конференц-залів, розроблене на **ASP.NET Core 9**. Проєкт дозволяє керувати конференц-залами, додатковими послугами, бронюваннями та формувати аналітичні звіти на основі даних, що зберігаються в **ClickHouse**.

## Основні можливості

* Керування конференц-залами (CRUD)
* Керування додатковими послугами (CRUD)
* Створення бронювань
* Перевірка конфліктів бронювання за часом
* Розрахунок вартості бронювання з урахуванням тарифів та додаткових послуг
* Формування аналітичних звітів
* Документування API за допомогою Swagger

## Використані технології

* ASP.NET Core 9 Web API
* Entity Framework Core
* PostgreSQL
* ClickHouse
* Mapster
* FluentValidation
* xUnit
* Moq
* Serilog
* Docker
* Swagger (OpenAPI)

## Архітектура

Проєкт побудований із використанням багатошарової архітектури.

```text
Controllers
    ↓
Services
    ↓
Repositories
    ↓
PostgreSQL / ClickHouse
```

### Основні модулі

* **ConferenceHall** — керування конференц-залами.
* **AdditionalService** — керування додатковими послугами.
* **Booking** — створення бронювань, перевірка доступності залів та розрахунок вартості.
* **Analytics** — побудова аналітичних звітів із використанням ClickHouse.

## Аналітичні звіти

Реалізовано такі звіти:

* Загальна виручка.
* Найпопулярніші конференц-зали.
* Виручка за днями.
* Завантаженість конференц-залів.
* Загальна інформація для інформаційної панелі (Dashboard).

## Структура проєкту

```text
ConferenceBooking.Api
│
├── Controllers
├── Data
├── DTOs
├── Mapping
├── Models
├── Repository
├── Services
├── Validators
└── Tests
```

## Запуск проєкту

### 1. Клонувати репозиторій

```bash
git clone <repository-url>
```

### 2. Налаштувати рядки підключення

У файлі `appsettings.json` необхідно вказати:

* PostgreSQL
* ClickHouse

### 3. Створити базу даних PostgreSQL

Виконати міграції:

```bash
dotnet ef database update
```

### 4. Запустити ClickHouse

Приклад запуску через Docker:

```bash
docker run -d ^
--name clickhouse-server ^
-p 8123:8123 ^
-p 9000:9000 ^
clickhouse/clickhouse-server
```

Після запуску створити таблицю `booking_analytics`.

### 5. Запустити застосунок

```bash
dotnet run
```

Swagger буде доступний за адресою:

```text
https://localhost:<port>/swagger
```

## Тестування

Для запуску тестів:

```bash
dotnet test
```

## Автор

**Maksim Chystikov**

Software Engineer (.NET)

GitHub: https://github.com/maksimka1267
