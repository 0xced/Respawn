using System.Data.Common;
using System.Runtime.CompilerServices;
using Npgsql;
using NPoco;
using Testcontainers.PostgreSql;
using Testcontainers.Xunit;
using Xunit.Abstractions;

namespace Respawn.DatabaseTests;

public class PostgresFixture(IMessageSink messageSink) : DbContainerFixture<PostgreSqlBuilder, PostgreSqlContainer>(messageSink)
{
    public override DbProviderFactory DbProviderFactory => NpgsqlFactory.Instance;

    protected override PostgreSqlBuilder Configure(PostgreSqlBuilder builder)
    {
        return builder.WithName("Respawn.PostgresTests").WithReuse(true);
    }

    public override string ConnectionString => new NpgsqlConnectionStringBuilder(base.ConnectionString) { IncludeErrorDetail = true }.ConnectionString;

    public IDatabase CreateDatabase([CallerMemberName] string dbName = "")
    {
        ExecuteCreateDb(dbName);
        var connectionString = new NpgsqlConnectionStringBuilder(ConnectionString) { Database = dbName }.ConnectionString;
        return new TestDatabase(new Database(connectionString, DatabaseType.PostgreSQL, DbProviderFactory), () => ExecuteDropDb(dbName));
    }

    private void ExecuteCreateDb(string dbName) => ExecuteNonQuery($"drop database if exists \"{dbName}\"; create database \"{dbName}\";");

    private void ExecuteDropDb(string dbName) => ExecuteNonQuery($"drop database \"{dbName}\" with (force)");

    private void ExecuteNonQuery(string sql)
    {
        using var command = CreateCommand(sql);
        command.ExecuteNonQuery();
    }
}