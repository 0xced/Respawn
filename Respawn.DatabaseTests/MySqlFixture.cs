using System.Data.Common;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using NPoco;
using Testcontainers.MySql;
using Testcontainers.Xunit;
using Xunit.Abstractions;

namespace Respawn.DatabaseTests;

public class MySqlFixture(IMessageSink messageSink) : DbContainerFixture<MySqlBuilder, MySqlContainer>(messageSink)
{
    public override DbProviderFactory DbProviderFactory => MySqlClientFactory.Instance;

    protected override MySqlBuilder Configure(MySqlBuilder builder)
    {
        return builder.WithName("Respawn.MySqlTests").WithUsername("root").WithReuse(true);
    }

    public IDatabase CreateDatabase([CallerMemberName] string dbName = "")
    {
        var script =
            $"""
              DROP DATABASE IF EXISTS `{dbName}`;
              CREATE DATABASE `{dbName}`;
              -- CREATE USER IF NOT EXISTS '{MySqlBuilder.DefaultUsername}'@'%' IDENTIFIED BY '{MySqlBuilder.DefaultPassword}';
              -- GRANT ALL PRIVILEGES ON `{dbName}`.* TO '{MySqlBuilder.DefaultUsername}'@'%';
              """;
        using var command = CreateCommand(script);
        command.ExecuteNonQuery();

        var connectionString = new MySqlConnectionStringBuilder(ConnectionString)
        {
            // UserID = MySqlBuilder.DefaultUsername,
            Database = dbName
        }.ConnectionString;
        var database = new Database(connectionString, DatabaseType.MySQL, DbProviderFactory);
        database.OpenSharedConnection();
        return database;
    }
}