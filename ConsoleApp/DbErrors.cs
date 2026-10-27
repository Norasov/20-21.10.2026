using Npgsql;

namespace ConsoleApp;

public static class DbErrors
{
    public static string Describe(Exception ex) => ex switch
    {
        PostgresException {SqlState: PostgresErrorCodes.UniqueViolation}
            => "Такая запись уже существует.",
        PostgresException {SqlState: PostgresErrorCodes.ForeignKeyViolation}
            => "Операция невозможна: запись связана с другими данными или указан несуществующий автор/жанр.",
        PostgresException {SqlState: PostgresErrorCodes.NotNullViolation}
            => "Не заполнено обязательное поле.",
        NpgsqlException
            => $"Ошибка базы данных: {ex.Message}",
        
        _
            => $"Непредвиденная ошибка: {ex.Message}"
    };
}
