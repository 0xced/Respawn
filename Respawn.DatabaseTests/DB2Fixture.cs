using System.Data.Common;
using System.Runtime.CompilerServices;
using Npgsql;
using NPoco;
using Testcontainers.PostgreSql;
using Testcontainers.Xunit;
using Xunit.Abstractions;

namespace Respawn.DatabaseTests;

public class DB2Fixture(IMessageSink messageSink) : DbContainerFixture<PostgreSqlBuilder, PostgreSqlContainer>(messageSink)
{
    public override DbProviderFactory DbProviderFactory => NpgsqlFactory.Instance;

    protected override PostgreSqlBuilder Configure(PostgreSqlBuilder builder)
    {
        return builder.WithName("Respawn.DB2Tests").WithReuse(true);
    }

    public override string ConnectionString => new NpgsqlConnectionStringBuilder(base.ConnectionString) { IncludeErrorDetail = true }.ConnectionString;

    public IDatabase CreateDatabase([CallerMemberName] string dbName = "")
    {
        var script =
            $"""
             drop database if exists "{dbName}";
             create database "{dbName}";
             """;
        using var command = CreateCommand(script);
        command.ExecuteNonQuery();

        var connectionString = new NpgsqlConnectionStringBuilder(ConnectionString) { Database = dbName }.ConnectionString;
        var database = new Database(connectionString, DatabaseType.PostgreSQL, DbProviderFactory);
        database.OpenSharedConnection();
        return database;
    }
}