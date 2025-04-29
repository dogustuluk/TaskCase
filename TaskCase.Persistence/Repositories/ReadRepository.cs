using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TaskCase.Application.Common.GenericObjects;
using TaskCase.Application.Repositories;
using TaskCase.Domain.Entities;
using TaskCase.Persistence.Context;

namespace TaskCase.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : IEntity
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate)
    {
        int count = await Table.CountAsync(predicate);
        return count;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        bool exist = await Table.AnyAsync(predicate);
        if (exist)
            return exist;
        else
            return false;
    }

    public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate, string? include)
    {
        IQueryable<T> query = Table.AsQueryable();
        if (!string.IsNullOrEmpty(include))
            query = query.Include(include);
        if (predicate != null)
            query = query.Where(predicate);
        //if (query.Count() > 0)
        //    return query;
        //else
        //    throw new ArgumentNullException();
        return query;
    }

    public async Task<List<T>> GetAllSqlAsync(string table, string sqlQuery, string? include)
    {
        string sql = $"SELECT * FROM {table} WHERE {sqlQuery}";
        var query = Table.FromSqlRaw(sql);
        if (!string.IsNullOrEmpty(include))
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await Table.FirstAsync(x => x.Id == id);
        if (entity != null) return entity;
        else throw new ArgumentNullException("id bulunamadı");
    }

    public async Task<T> GetByGuidAsync(Guid guid)
    {
        var entity = await Table.FirstAsync(x => x.Guid == guid);
        if (entity != null) return entity;
        else throw new ArgumentNullException("guid bulunamadı");
    }

    public async Task<T> GetByIdOrGuidAsync(object keyValue)
    {
        if (keyValue is Guid guid)
        {
            var entity = await Table.FirstOrDefaultAsync(x => x.Guid == guid);
            if (entity != null) return entity;
            else throw new ArgumentNullException("Guid bulunamadı");
        }
        else if (keyValue is int id)
        {
            var entity = await Table.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null) return entity;
            else throw new ArgumentNullException("Id bulunamadı");
        }
        else throw new ArgumentException("Geçersiz anahtar değeri.");

    }


    public async Task<T> GetByEntityAsync(object value, string? fieldName = null)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        T entity = null;

        if (int.TryParse(value.ToString(), out int id))
            entity = await Table.FirstOrDefaultAsync(x => x.Id == id);

        else if (Guid.TryParse(value.ToString(), out Guid guid))
            entity = await Table.FirstOrDefaultAsync(x => x.Guid == guid);

        else if (!string.IsNullOrEmpty(fieldName))
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = Expression.PropertyOrField(parameter, fieldName);
            object? typedValue = value.ToString();
            var constant = Expression.Constant(typedValue);
            var equal = Expression.Equal(member, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            entity = await Table.FirstOrDefaultAsync(lambda);
        }

        if (entity != null)
            return entity;

        else
            throw new ArgumentNullException($"{fieldName ?? "Belirtilen değer"} bulunamadı");

    }

    public async Task<IQueryable<T>> GetDataAsync(Expression<Func<T, bool>> predicate, string? include, int take, string orderBy)
    {
        IQueryable<T> query = Table.AsQueryable();
        if (!string.IsNullOrEmpty(include))
            query = query.Include(include);
        if (predicate != null)
            query = query.Where(predicate);
        if (!string.IsNullOrEmpty(orderBy))
            query = query.OrderBy(orderBy);
        query = query.Take(take);
        return query;
    }

    public async Task<PaginatedList<T>> GetDataPagedAsync(Expression<Func<T, bool>> predicate, string? include, int pageIndex, int take, string orderBy, bool? isTrack = false)
    {
        var query = Table.AsQueryable();

        if (!string.IsNullOrEmpty(include))
            query = query.Include(include);

        if (predicate != null)
            query = query.Where(predicate);

        if (!string.IsNullOrEmpty(orderBy))
            query = query.OrderBy(orderBy);

        if (isTrack == true)
            query = query.AsNoTracking();

        return await CreatePaginatedList.CreateAsync<T>(query, pageIndex, take);
    }




    public async Task<PaginatedList<T>> GetDataPagedSqlAsync(string table, string sqlQuery, string? include, int pageIndex, int take, string orderBy)
    {
        string queryString = $"Select * from {table} Where {sqlQuery}";

        var query = Table.FromSqlRaw(queryString).AsQueryable();
        if (!string.IsNullOrEmpty(include))
            query = query.Include(include);
        if (!string.IsNullOrEmpty(orderBy))
            query = await GetSortedDataAsync(query, orderBy);
        //Take için kontrol et.
        return await CreatePaginatedList.CreateAsync<T>(query, pageIndex, take);

    }

    public async Task<List<T>> GetDataSqlAsync(string table, string sqlQuery, string? include, int pageIndex, int take, string orderBy)
    {
        // string queryString = $"SELECT * FROM {table} WHERE {sqlQuery} ORDER BY {orderBy} OFFSET {pageIndex * take} ROWS FETCH NEXT {take} ROWS ONLY";
        string queryString = $"SELECT * FROM \"{table}\" WHERE {sqlQuery} ORDER BY {orderBy} OFFSET {pageIndex * take} LIMIT {take}";

        if (!string.IsNullOrEmpty(include))
            return await Table.FromSqlRaw(queryString).Include(include).ToListAsync();
        else
            return await Table.FromSqlRaw(queryString).ToListAsync();

    }

    public async Task<T> GetEntityWithIncludeAsync(Expression<Func<T, bool>> predicate, string? include)
    {
        var query = Table.AsQueryable().Where(predicate);
        if (!string.IsNullOrEmpty(include))
        {
            foreach (string b in include.Split(','))
            {
                query = query.Include(b);
            }
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> GetSingleEntityAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        IQueryable<T> query = Table;
        if (!tracking)
            query = query.AsNoTracking();
        var result = await query.SingleOrDefaultAsync(method);
        if (result != null) return result;
        else throw new InvalidOperationException("Eşleşen bir entity bulunamadı");
    }

    public async Task<IQueryable<T>> GetSortedDataAsync(IQueryable<T> query, string orderBy)
    {
        //query = query.OrderBy(orderBy);
        //return query ?? throw new NullReferenceException();
        IQueryable<T> queryData = Table.AsQueryable();
        queryData = queryData.OrderBy(orderBy);
        if (queryData != null) return queryData;
        else throw new NullReferenceException();
    }

    public string? GetValue(string table, string column, string sqlQuery)
    {
        string sql = $"SELECT TOP 1 CONVERT(nvarchar, {column}) as [Value] FROM {table} WHERE {sqlQuery}";

        var result = _context.Set<string>().FromSqlRaw(sql).FirstOrDefault();

        if (result != null) return result;
        else throw new ArgumentNullException();
    }

    public async Task<string?> GetValueAsync(string table, string column, string sqlQuery, int dbType)
    {
        string sql;

        if (dbType == 1) // postgreSql
        {
            // PostgreSQL query
            sql = $"SELECT \"{column}\" as \"Value\" FROM \"{table}\" WHERE {sqlQuery} LIMIT 1";

        }
        else if (dbType == 2) //sql server
        {
            // SQLServer query
            sql = $"SELECT TOP 1 {column} as [Value] FROM {table} WHERE {sqlQuery}";
        }
        else
        {
            throw new ArgumentException("Invalid database type.");
        }

        FormattableString formattedSqlQuery = FormattableStringFactory.Create(sql);

        string result = await _context.Database.SqlQuery<string>(formattedSqlQuery).FirstOrDefaultAsync();

        if (result != null) return result;
        else throw new ArgumentNullException();
        //string sql = $"SELECT TOP 1 CONVERT(nvarchar, {column}) as [Value] FROM {table} WHERE {sqlQuery}";

        //FormattableString formattedSqlQuery = FormattableStringFactory.Create(sql);

        //string result = await _context.Database.SqlQuery<string>(formattedSqlQuery).FirstOrDefaultAsync();

        //if (result != null) return result;
        //else throw new ArgumentNullException();
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true)
    {
        IQueryable<T> query = Table;
        if (!tracking)
            query = query.AsNoTracking();
        if (query != null) return query;
        else throw new InvalidOperationException("Bulunamadı");
    }

    public async Task<IQueryable<TResult>> GetAllSpecificPropertiesAsync<TResult>(IQueryable<T>? prefilteredQuery,
Expression<Func<T, bool>>? predicate, string? include, Expression<Func<T, TResult>> selector)
    {
        IQueryable<T> query = prefilteredQuery ?? Table.AsQueryable();

        if (!string.IsNullOrEmpty(include))
            query = query.Include(include);

        if (predicate != null)
            query = query.Where(predicate);

        return query.Select(selector);
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate, string? include)
    {
        IQueryable<T> query = Table.AsQueryable();
        if (!string.IsNullOrEmpty(include))
            query = query.Include(include);
        if (predicate != null)
            query = query.Where(predicate);
        //if (query.Count() > 0)
        //    return query;
        //else
        //    throw new ArgumentNullException();
        return query;
    }

}
