1 Настроены сервисы аутентификации и авторизации.
AddIdentity в Program.cs, хранение данных в SQLite (ApplicationDbContext), роли Admin/User в БД.

2 Страницы авторизации и регистрации + серверные методы.
Login и Register через SignInManager / UserManager, валидация, CSRF- токены.

3 Ограничение доступа для разных категорий.
[Authorize(Roles = "Admin")], скрытие кнопок в интерфейсе через User.IsInRole, страница AccessDenied.