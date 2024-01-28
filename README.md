﻿# Apartment Price Tracker
Сервис для отслеживания изменений цен на квартиры с использованием технологии ASP.NET Core и C#.

Вторая задача находится в этом же решении, я не захотел второй репозиторий на git создавать
## Запуск проекта
1. Склонируйте репозиторий:

    ```bash
    git clone https://github.com/TheGamer137/ApartmentPriceTracker.git
    ```

2. Обновите бд через PM в Infrastructure:

    ```bash
    Update-Database
    ```

3. Запустите проект:

    ```bash
    dotnet run
    ```

## Конфигурация

Конфигурация приложения находится в файле `appsettings.json`. Вы можете настроить подключение к базе данных и другие параметры в этом файле.
### Допущения
HTML-парсинг осуществляется с использованием библиотеки Selenium.