
FlightStatusApi - это API, предоставляющее информацию о статусе рейсов.

Шаг 1: Клонирование репозитория
git clone https://github.com/FarukhAvamov/flight-status-api.git

Шаг 2: Настройка подключения к базе данных
Перейдите в директорию проекта:
cd FlightStatusApi
Откройте файл appsettings.json, который находится в папке проекта FlightStatusApi.
Замените значение ConnectionString на вашу фактическую строку подключения к базе данных:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Ваш_Строка_Подключения_Здесь"
  }
}
```

Шаг 3: Обновление базы данных
Перейти в слой Infrastructure
cd Infrastructure
В терминале выполнить команду
```
dotnet ef database update --startup-project ../FlightStatusApi
```

Для получение доступа к эндпоинтам необходимо пройти регистрацию.
Эндпоинты регистрации / логина присутствуют в swagger






