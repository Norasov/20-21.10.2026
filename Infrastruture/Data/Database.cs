using Npgsql;

namespace Infrastruture.Data;

public static class Database
{
    public static string connectionString = "Host=localhost;Port=5432;Database=booknest;Username=postgres;Password=p000";
    public static NpgsqlConnection Connection()=>new NpgsqlConnection(connectionString);
}
