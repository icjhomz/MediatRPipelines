﻿using Microsoft.EntityFrameworkCore;

namespace Sample.MediatRPipelines.Persistence.Repository;

public class EntityFrameworkRepository<T> : IRepository<T>
    where T : class
{
    private readonly SampleDbContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityFrameworkRepository(SampleDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
