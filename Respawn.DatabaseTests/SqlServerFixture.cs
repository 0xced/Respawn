using System.Data.Common;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using NPoco;
using Testcontainers.MsSql;
using Testcontainers.Xunit;
using Xunit.Abstractions;

namespace Respawn.DatabaseTests;

public class SqlServerFixture(IMessageSink messageSink) : DbContainerFixture<MsSqlBuilder, MsSqlContainer>(messageSink)
{
    public override DbProviderFactory DbProviderFactory => SqlClientFactory.Instance;
    
    protected override MsSqlBuilder Configure(MsSqlBuilder builder)
    {
        return builder.WithName("Respawn.SqlServerTests").WithReuse(true);
    }

    public IDatabase CreateDatabase([CallerMemberName] string dbName = "")
    {
        var script =
            $"""
            IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{dbName}') alter database [{dbName}] set single_user with rollback immediate;
            DROP DATABASE IF EXISTS [{dbName}];
            create database [{dbName}];
            """;
        using var command = CreateCommand(script);
        command.ExecuteNonQuery();

        var connectionString = new SqlConnectionStringBuilder(ConnectionString) { InitialCatalog = dbName }.ConnectionString;
        var database = new Database(connectionString, DatabaseType.SqlServer2012, DbProviderFactory);
        database.OpenSharedConnection();
        return database;
    }
}