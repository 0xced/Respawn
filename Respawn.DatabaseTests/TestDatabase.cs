using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NPoco;
using NPoco.Linq;

namespace Respawn.DatabaseTests;

public sealed class TestDatabase : IDatabase
{
    private readonly IDatabase _database;
    private readonly Action _cleanup;

    public TestDatabase(IDatabase database, Action cleanup)
    {
        _database = database;
        _cleanup = cleanup;
        OpenSharedConnection();
    }

    public void Dispose()
    {
        _database.Dispose();
        _cleanup();
    }

    public MapperCollection Mappers
    {
        get => _database.Mappers;
        set => _database.Mappers = value;
    }
    public IPocoDataFactory PocoDataFactory
    {
        get => _database.PocoDataFactory;
        set => _database.PocoDataFactory = value;
    }
    public DatabaseType DatabaseType => _database.DatabaseType;
    public List<IInterceptor> Interceptors => _database.Interceptors;
    public string ConnectionString => _database.ConnectionString;
    public DbParameter CreateParameter() => _database.CreateParameter();
    public void AddParameter(DbCommand cmd, object value) => _database.AddParameter(cmd, value);
    public DbCommand CreateCommand(DbConnection connection, CommandType commandType, string sql, params object[] args) => _database.CreateCommand(connection, commandType, sql, args);
    public ITransaction GetTransaction() => _database.GetTransaction();
    public ITransaction GetTransaction(IsolationLevel isolationLevel) => _database.GetTransaction(isolationLevel);
    public void SetTransaction(DbTransaction tran) => _database.SetTransaction(tran);
    public void BeginTransaction() => _database.BeginTransaction();
    public void BeginTransaction(IsolationLevel isolationLevel) => _database.BeginTransaction(isolationLevel);
    public void AbortTransaction() => _database.AbortTransaction();
    public void CompleteTransaction() => _database.CompleteTransaction();
    public IDatabase OpenSharedConnection() => _database.OpenSharedConnection();
    public void CloseSharedConnection() => _database.CloseSharedConnection();
    public DbConnection Connection => _database.Connection;
    public DbTransaction Transaction => _database.Transaction;
    public IDictionary<string, object> Data => _database.Data;
    public int CommandTimeout
    {
        get => _database.CommandTimeout;
        set => _database.CommandTimeout = value;
    }
    public int OneTimeCommandTimeout
    {
        get => _database.OneTimeCommandTimeout;
        set => _database.OneTimeCommandTimeout = value;
    }
    public async Task<T> SingleAsync<T>(string sql, params object[] args) => await _database.SingleAsync<T>(sql, args);
    public async Task<T> SingleAsync<T>(Sql sql) => await _database.SingleAsync<T>(sql);
    public async Task<T> SingleOrDefaultAsync<T>(string sql, params object[] args) => await _database.SingleOrDefaultAsync<T>(sql, args);
    public async Task<T> SingleOrDefaultAsync<T>(Sql sql) => await _database.SingleOrDefaultAsync<T>(sql);
    public async Task<T> SingleByIdAsync<T>(object primaryKey) => await _database.SingleByIdAsync<T>(primaryKey);
    public async Task<T> SingleOrDefaultByIdAsync<T>(object primaryKey) => await _database.SingleOrDefaultByIdAsync<T>(primaryKey);
    public async Task<T> FirstAsync<T>(string sql, params object[] args) => await _database.FirstAsync<T>(sql, args);
    public async Task<T> FirstAsync<T>(Sql sql) => await _database.FirstAsync<T>(sql);
    public async Task<T> FirstOrDefaultAsync<T>(string sql, params object[] args) => await _database.FirstOrDefaultAsync<T>(sql, args);
    public async Task<T> FirstOrDefaultAsync<T>(Sql sql) => await _database.FirstOrDefaultAsync<T>(sql);
    public IAsyncEnumerable<T> QueryAsync<T>(string sql, params object[] args) => _database.QueryAsync<T>(sql, args);
    public IAsyncEnumerable<T> QueryAsync<T>(Sql sql) => _database.QueryAsync<T>(sql);
    public IAsyncQueryProviderWithIncludes<T> QueryAsync<T>() => _database.QueryAsync<T>();
    public async Task<List<T>> FetchAsync<T>(string sql, params object[] args) => await _database.FetchAsync<T>(sql, args);
    public async Task<List<T>> FetchAsync<T>(Sql sql) => await _database.FetchAsync<T>(sql);
    public async Task<List<T>> FetchAsync<T>() => await _database.FetchAsync<T>();
    public async Task<Page<T>> PageAsync<T>(long page, long itemsPerPage, string sql, params object[] args) => await _database.PageAsync<T>(page, itemsPerPage, sql, args);
    public async Task<Page<T>> PageAsync<T>(long page, long itemsPerPage, Sql sql) => await _database.PageAsync<T>(page, itemsPerPage, sql);
    public async Task<List<T>> FetchAsync<T>(long page, long itemsPerPage, string sql, params object[] args) => await _database.FetchAsync<T>(page, itemsPerPage, sql, args);
    public async Task<List<T>> FetchAsync<T>(long page, long itemsPerPage, Sql sql) => await _database.FetchAsync<T>(page, itemsPerPage, sql);
    public async Task<List<T>> SkipTakeAsync<T>(long skip, long take, string sql, params object[] args) => await _database.SkipTakeAsync<T>(skip, take, sql, args);
    public async Task<List<T>> SkipTakeAsync<T>(long skip, long take, Sql sql) => await _database.SkipTakeAsync<T>(skip, take, sql);
    public async Task<TRet> FetchMultipleAsync<T1, T2, TRet>(Func<List<T1>, List<T2>, TRet> cb, string sql, params object[] args) => await _database.FetchMultipleAsync(cb, sql, args);
    public async Task<TRet> FetchMultipleAsync<T1, T2, T3, TRet>(Func<List<T1>, List<T2>, List<T3>, TRet> cb, string sql, params object[] args) => await _database.FetchMultipleAsync(cb, sql, args);
    public async Task<TRet> FetchMultipleAsync<T1, T2, T3, T4, TRet>(Func<List<T1>, List<T2>, List<T3>, List<T4>, TRet> cb, string sql, params object[] args) => await _database.FetchMultipleAsync(cb, sql, args);
    public async Task<TRet> FetchMultipleAsync<T1, T2, TRet>(Func<List<T1>, List<T2>, TRet> cb, Sql sql) => await _database.FetchMultipleAsync(cb, sql);
    public async Task<TRet> FetchMultipleAsync<T1, T2, T3, TRet>(Func<List<T1>, List<T2>, List<T3>, TRet> cb, Sql sql) => await _database.FetchMultipleAsync(cb, sql);
    public async Task<TRet> FetchMultipleAsync<T1, T2, T3, T4, TRet>(Func<List<T1>, List<T2>, List<T3>, List<T4>, TRet> cb, Sql sql) => await _database.FetchMultipleAsync(cb, sql);
    public async Task<(List<T1>, List<T2>)> FetchMultipleAsync<T1, T2>(string sql, params object[] args) => await _database.FetchMultipleAsync<T1, T2>(sql, args);
    public async Task<(List<T1>, List<T2>, List<T3>)> FetchMultipleAsync<T1, T2, T3>(string sql, params object[] args) => await _database.FetchMultipleAsync<T1, T2, T3>(sql, args);
    public async Task<(List<T1>, List<T2>, List<T3>, List<T4>)> FetchMultipleAsync<T1, T2, T3, T4>(string sql, params object[] args) => await _database.FetchMultipleAsync<T1, T2, T3, T4>(sql, args);
    public async Task<(List<T1>, List<T2>)> FetchMultipleAsync<T1, T2>(Sql sql) => await _database.FetchMultipleAsync<T1, T2>(sql);
    public async Task<(List<T1>, List<T2>, List<T3>)> FetchMultipleAsync<T1, T2, T3>(Sql sql) => await _database.FetchMultipleAsync<T1, T2, T3>(sql);
    public async Task<(List<T1>, List<T2>, List<T3>, List<T4>)> FetchMultipleAsync<T1, T2, T3, T4>(Sql sql) => await _database.FetchMultipleAsync<T1, T2, T3, T4>(sql);
    public async Task<T> ExecuteScalarAsync<T>(string sql, params object[] args) => await _database.ExecuteScalarAsync<T>(sql, args);
    public async Task<T> ExecuteScalarAsync<T>(Sql sql) => await _database.ExecuteScalarAsync<T>(sql);
    public async Task<int> ExecuteAsync(string sql, params object[] args) => await _database.ExecuteAsync(sql, args);
    public async Task<int> ExecuteAsync(Sql sql) => await _database.ExecuteAsync(sql);
    public async Task<object> InsertAsync(string tableName, string primaryKeyName, object poco) => await _database.InsertAsync(tableName, primaryKeyName, poco);
    public async Task<object> InsertAsync<T>(T poco) => await _database.InsertAsync(poco);
    public async Task InsertBulkAsync<T>(IEnumerable<T> pocos, InsertBulkOptions options = null) => await _database.InsertBulkAsync(pocos, options);
    public async Task<int> InsertBatchAsync<T>(IEnumerable<T> pocos, BatchOptions options = null) => await _database.InsertBatchAsync(pocos, options);
    public async Task<int> UpdateAsync(object poco) => await _database.UpdateAsync(poco);
    public async Task<int> UpdateAsync(object poco, IEnumerable<string> columns) => await _database.UpdateAsync(poco, columns);
    public async Task<int> UpdateAsync<T>(T poco, Expression<Func<T, object>> fields) => await _database.UpdateAsync(poco, fields);
    public async Task<int> UpdateBatchAsync<T>(IEnumerable<UpdateBatch<T>> pocos, BatchOptions options = null) => await _database.UpdateBatchAsync(pocos, options);
    public async Task<int> DeleteAsync(object poco) => await _database.DeleteAsync(poco);
    public IAsyncUpdateQueryProvider<T> UpdateManyAsync<T>() => _database.UpdateManyAsync<T>();
    public IAsyncDeleteQueryProvider<T> DeleteManyAsync<T>() => _database.DeleteManyAsync<T>();
    public async Task<bool> IsNewAsync<T>(T poco) => await _database.IsNewAsync(poco);
    public async Task SaveAsync<T>(T poco) => await _database.SaveAsync(poco);
    public void BuildPageQueries<T>(long skip, long take, string sql, ref object[] args, out string sqlCount, out string sqlPage) => _database.BuildPageQueries<T>(skip, take, sql, ref args, out sqlCount, out sqlPage);
    public int Execute(string sql, params object[] args) => _database.Execute(sql, args);
    public int Execute(Sql sql) => _database.Execute(sql);
    public int Execute(string sql, CommandType commandType, params object[] args) => _database.Execute(sql, commandType, args);
    public T ExecuteScalar<T>(string sql, params object[] args) => _database.ExecuteScalar<T>(sql, args);
    public T ExecuteScalar<T>(Sql sql) => _database.ExecuteScalar<T>(sql);
    public T ExecuteScalar<T>(string sql, CommandType commandType, params object[] args) => _database.ExecuteScalar<T>(sql, commandType, args);
    public List<object> Fetch(Type type, string sql, params object[] args) => _database.Fetch(type, sql, args);
    public List<object> Fetch(Type type, Sql Sql) => _database.Fetch(type, Sql);
    public IEnumerable<object> Query(Type type, string sql, params object[] args) => _database.Query(type, sql, args);
    public IEnumerable<object> Query(Type type, Sql Sql) => _database.Query(type, Sql);
    public List<T> Fetch<T>() => _database.Fetch<T>();
    public List<T> Fetch<T>(string sql, params object[] args) => _database.Fetch<T>(sql, args);
    public List<T> Fetch<T>(Sql sql) => _database.Fetch<T>(sql);
    public List<T> Fetch<T>(long page, long itemsPerPage, string sql, params object[] args) => _database.Fetch<T>(page, itemsPerPage, sql, args);
    public List<T> Fetch<T>(long page, long itemsPerPage, Sql sql) => _database.Fetch<T>(page, itemsPerPage, sql);
    public Page<T> Page<T>(long page, long itemsPerPage, string sql, params object[] args) => _database.Page<T>(page, itemsPerPage, sql, args);
    public Page<T> Page<T>(long page, long itemsPerPage, Sql sql) => _database.Page<T>(page, itemsPerPage, sql);
    public List<T> SkipTake<T>(long skip, long take, string sql, params object[] args) => _database.SkipTake<T>(skip, take, sql, args);
    public List<T> SkipTake<T>(long skip, long take, Sql sql) => _database.SkipTake<T>(skip, take, sql);
    public List<T> FetchOneToMany<T>(Expression<Func<T, IList>> many, string sql, params object[] args) => _database.FetchOneToMany(many, sql, args);
    public List<T> FetchOneToMany<T>(Expression<Func<T, IList>> many, Sql sql) => _database.FetchOneToMany(many, sql);
    public List<T> FetchOneToMany<T>(Expression<Func<T, IList>> many, Func<T, object> idFunc, string sql, params object[] args) => _database.FetchOneToMany(many, idFunc, sql, args);
    public List<T> FetchOneToMany<T>(Expression<Func<T, IList>> many, Func<T, object> idFunc, Sql sql) => _database.FetchOneToMany(many, idFunc, sql);
    public IEnumerable<T> Query<T>(string sql, params object[] args) => _database.Query<T>(sql, args);
    public IEnumerable<T> Query<T>(Sql sql) => _database.Query<T>(sql);
    public IQueryProviderWithIncludes<T> Query<T>() => _database.Query<T>();
    public T SingleById<T>(object primaryKey) => _database.SingleById<T>(primaryKey);
    public T Single<T>(string sql, params object[] args) => _database.Single<T>(sql, args);
    public T SingleInto<T>(T instance, string sql, params object[] args) => _database.SingleInto(instance, sql, args);
    public T SingleOrDefaultById<T>(object primaryKey) => _database.SingleOrDefaultById<T>(primaryKey);
    public T SingleOrDefault<T>(string sql, params object[] args) => _database.SingleOrDefault<T>(sql, args);
    public T SingleOrDefaultInto<T>(T instance, string sql, params object[] args) => _database.SingleOrDefaultInto(instance, sql, args);
    public T First<T>(string sql, params object[] args) => _database.First<T>(sql, args);
    public T FirstInto<T>(T instance, string sql, params object[] args) => _database.FirstInto(instance, sql, args);
    public T FirstOrDefault<T>(string sql, params object[] args) => _database.FirstOrDefault<T>(sql, args);
    public T FirstOrDefaultInto<T>(T instance, string sql, params object[] args) => _database.FirstOrDefaultInto(instance, sql, args);
    public T Single<T>(Sql sql) => _database.Single<T>(sql);
    public T SingleInto<T>(T instance, Sql sql) => _database.SingleInto(instance, sql);
    public T SingleOrDefault<T>(Sql sql) => _database.SingleOrDefault<T>(sql);
    public T SingleOrDefaultInto<T>(T instance, Sql sql) => _database.SingleOrDefaultInto(instance, sql);
    public T First<T>(Sql sql) => _database.First<T>(sql);
    public T FirstInto<T>(T instance, Sql sql) => _database.FirstInto(instance, sql);
    public T FirstOrDefault<T>(Sql sql) => _database.FirstOrDefault<T>(sql);
    public T FirstOrDefaultInto<T>(T instance, Sql sql) => _database.FirstOrDefaultInto(instance, sql);
    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(Sql Sql) => _database.Dictionary<TKey, TValue>(Sql);
    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(string sql, params object[] args) => _database.Dictionary<TKey, TValue>(sql, args);
    public bool Exists<T>(object primaryKey) => _database.Exists<T>(primaryKey);
    public TRet FetchMultiple<T1, T2, TRet>(Func<List<T1>, List<T2>, TRet> cb, string sql, params object[] args) => _database.FetchMultiple(cb, sql, args);
    public TRet FetchMultiple<T1, T2, T3, TRet>(Func<List<T1>, List<T2>, List<T3>, TRet> cb, string sql, params object[] args) => _database.FetchMultiple(cb, sql, args);
    public TRet FetchMultiple<T1, T2, T3, T4, TRet>(Func<List<T1>, List<T2>, List<T3>, List<T4>, TRet> cb, string sql, params object[] args) => _database.FetchMultiple(cb, sql, args);
    public TRet FetchMultiple<T1, T2, TRet>(Func<List<T1>, List<T2>, TRet> cb, Sql sql) => _database.FetchMultiple(cb, sql);
    public TRet FetchMultiple<T1, T2, T3, TRet>(Func<List<T1>, List<T2>, List<T3>, TRet> cb, Sql sql) => _database.FetchMultiple(cb, sql);
    public TRet FetchMultiple<T1, T2, T3, T4, TRet>(Func<List<T1>, List<T2>, List<T3>, List<T4>, TRet> cb, Sql sql) => _database.FetchMultiple(cb, sql);
    public (List<T1>, List<T2>) FetchMultiple<T1, T2>(string sql, params object[] args) => _database.FetchMultiple<T1, T2>(sql, args);
    public (List<T1>, List<T2>, List<T3>) FetchMultiple<T1, T2, T3>(string sql, params object[] args) => _database.FetchMultiple<T1, T2, T3>(sql, args);
    public (List<T1>, List<T2>, List<T3>, List<T4>) FetchMultiple<T1, T2, T3, T4>(string sql, params object[] args) => _database.FetchMultiple<T1, T2, T3, T4>(sql, args);
    public (List<T1>, List<T2>) FetchMultiple<T1, T2>(Sql sql) => _database.FetchMultiple<T1, T2>(sql);
    public (List<T1>, List<T2>, List<T3>) FetchMultiple<T1, T2, T3>(Sql sql) => _database.FetchMultiple<T1, T2, T3>(sql);
    public (List<T1>, List<T2>, List<T3>, List<T4>) FetchMultiple<T1, T2, T3, T4>(Sql sql) => _database.FetchMultiple<T1, T2, T3, T4>(sql);
    public object Insert<T>(string tableName, string primaryKeyName, bool autoIncrement, T poco) => _database.Insert(tableName, primaryKeyName, autoIncrement, poco);
    public object Insert<T>(string tableName, string primaryKeyName, T poco) => _database.Insert(tableName, primaryKeyName, poco);
    public object Insert<T>(T poco) => _database.Insert(poco);
    public void InsertBulk<T>(IEnumerable<T> pocos, InsertBulkOptions options = null) => _database.InsertBulk(pocos, options);
    public int InsertBatch<T>(IEnumerable<T> pocos, BatchOptions options = null) => _database.InsertBatch(pocos, options);
    public int Update(string tableName, string primaryKeyName, object poco, object primaryKeyValue) => _database.Update(tableName, primaryKeyName, poco, primaryKeyValue);
    public int Update(string tableName, string primaryKeyName, object poco) => _database.Update(tableName, primaryKeyName, poco);
    public int Update(string tableName, string primaryKeyName, object poco, object primaryKeyValue, IEnumerable<string> columns) => _database.Update(tableName, primaryKeyName, poco, primaryKeyValue, columns);
    public int Update(string tableName, string primaryKeyName, object poco, IEnumerable<string> columns) => _database.Update(tableName, primaryKeyName, poco, columns);
    public int Update(object poco, IEnumerable<string> columns) => _database.Update(poco, columns);
    public int Update(object poco, object primaryKeyValue, IEnumerable<string> columns) => _database.Update(poco, primaryKeyValue, columns);
    public int Update(object poco) => _database.Update(poco);
    public int Update<T>(T poco, Expression<Func<T, object>> fields) => _database.Update(poco, fields);
    public int Update(object poco, object primaryKeyValue) => _database.Update(poco, primaryKeyValue);
    public int Update<T>(string sql, params object[] args) => _database.Update(sql, args);
    public int Update<T>(Sql sql) => _database.Update(sql);
    public int UpdateBatch<T>(IEnumerable<UpdateBatch<T>> pocos, BatchOptions options = null) => _database.UpdateBatch(pocos, options);
    public IUpdateQueryProvider<T> UpdateMany<T>() => _database.UpdateMany<T>();
    public int Delete(string tableName, string primaryKeyName, object poco) => _database.Delete(tableName, primaryKeyName, poco);
    public int Delete(string tableName, string primaryKeyName, object poco, object primaryKeyValue) => _database.Delete(tableName, primaryKeyName, poco, primaryKeyValue);
    public int Delete(object poco) => _database.Delete(poco);
    public int Delete<T>(string sql, params object[] args) => _database.Delete<T>(sql, args);
    public int Delete<T>(Sql sql) => _database.Delete(sql);
    public int Delete<T>(object pocoOrPrimaryKey) => _database.Delete(pocoOrPrimaryKey);
    public IDeleteQueryProvider<T> DeleteMany<T>() => _database.DeleteMany<T>();
    public void Save<T>(T poco) => _database.Save(poco);
    public bool IsNew<T>(T poco) => _database.IsNew(poco);
}