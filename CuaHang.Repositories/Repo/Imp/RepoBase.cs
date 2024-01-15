using CuaHang.Models.Context;
using CuaHang.Repositories.Repo.IRepo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CuaHang.Repositories.Repo.Imp
{
    public class RepoBase<TEntity> : IRepoBase<TEntity> where TEntity : class
    {
        //public readonly AppDBContext context;

        #region Private Variables
        protected IDbContext _IdbContext = null;
        protected DbSet<TEntity> _dbSet;
        protected DbContext _dbContext;

        public RepoBase(IDbContext dbContext)
        {
            _IdbContext = dbContext;
            _dbContext = (DbContext)dbContext;
        }
        #endregion Private Variables
        #region Public/Protected Properties4

        protected DbSet<TEntity> DBSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = _dbContext.Set<TEntity>() as DbSet<TEntity>;
                }

                return _dbSet;
            }
        }
        #endregion

        #region Public
        public void ClearTrackedChanges()
        {
            var changedEntriesCopy = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }

        #region count
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = DBSet.Where(predicate);
            return await query.CountAsync();
        }

        public async Task<int> CountAsync(List<string> includes, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = BuildQueryable(includes, predicate);
            return await query.CountAsync();
        }
        public async Task<int> CountAsync(string include, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query;
            if (!string.IsNullOrWhiteSpace(include))
            {
                query = BuildQueryable(new List<string> { include }, predicate);
                return await query.CountAsync();
            }
            else
            {
                return await CountAsync(predicate);
            }
        }
        #endregion count
        #region create 
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            DBSet.Add(entity);
            await _IdbContext.CommitChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entity)
        {
            DBSet.AddRange(entity);
            await _IdbContext.CommitChangesAsync();

            return entity;
        }
        #endregion create 
        #region Delete
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            DBSet.Remove(entity);
            await _IdbContext.CommitChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var dataEntity = await DBSet.FindAsync(id);
            if (dataEntity != null)
            {
                DBSet.Remove(dataEntity);
                await _IdbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dataEntity = await DBSet.FindAsync(id);
            if (dataEntity != null)
            {
                DBSet.Remove(dataEntity);
                await _IdbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = predicate != null ? DBSet.Where(predicate) : DBSet;
            var dataEntities = query;
            if (dataEntities != null)
            {
                DBSet.RemoveRange(dataEntities);
                await _IdbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }

        #endregion Delete
        public async Task<List<TEntity>> ExecuteStoredProcedureAsync(string spName, params object[] parameters)
        {
            var resultSet = _dbContext.Set<TEntity>().FromSqlRaw(spName, parameters);
            await _IdbContext.CommitChangesAsync();

            return await resultSet.ToListAsync();
        }

        public async Task ExecuteStoredProcedureNoReturnAsync(string spName, params object[] parameters)
        {

            _dbContext.Database.ExecuteSqlRaw(spName, parameters);
            await _IdbContext.CommitChangesAsync();
        }

        public async Task<List<TEntity>> ExecuteStoredProcedureWithSqlParamListAsync(string spName, List<SqlParameter> parameters)
        {
            var resultSet = _dbContext.Set<TEntity>().FromSqlRaw(spName, parameters);
            await _IdbContext.CommitChangesAsync();

            return await resultSet.ToListAsync();
        }
        #region Get
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DBSet.AsNoTracking().ToListAsync();
        }
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DBSet.FirstOrDefaultAsync(predicate);        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = predicate != null ? DBSet.Where(predicate) : DBSet;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(List<string> includes, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = BuildQueryable(includes, predicate);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string include, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query;
            if (!string.IsNullOrWhiteSpace(include))
            {
                query = BuildQueryable(new List<string> { include }, predicate);
                return await query.ToListAsync();
            }
            else
            {
                return await GetAllAsync(predicate);
            }
        }
        public async Task<TEntity> GetByIDAsync(string id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIDAsync(int id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIDAsync(short id)
        {
            return await DBSet.FindAsync(id);

        }

        public async Task<TEntity> GetByIDAsync(List<string> includes, Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = BuildQueryable(includes, predicate);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetByIDAsync(byte id)
        {
            return await DBSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? DBSet.Where(predicate) : DBSet.AsQueryable();
        }
        public async Task<IQueryable<TEntity>> SqlQueryAsync(string query, params object[] parameters)
        {
            var result = _dbContext.Set<TEntity>().FromSqlRaw(query, parameters);
            await _IdbContext.CommitChangesAsync();

            return result.AsQueryable();
        }
        #endregion get
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _IdbContext.CommitChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(string id, TEntity entity)
        {
            var data = await DBSet.FindAsync(id);
            if (data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IdbContext.CommitChangesAsync();
                return entity;
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            var data = await DBSet.FindAsync(id);
            if (data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IdbContext.CommitChangesAsync();
                return entity;
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, params object[] keyValues)
        {
            var data = await DBSet.FindAsync(keyValues);
            if (data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IdbContext.CommitChangesAsync();
                return entity;
            }
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _IdbContext.CommitChangesAsync();
            return entities;
        }
        protected IQueryable<TEntity> BuildQueryable(List<string> includes, Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = DBSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includes != null && includes.Count > 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include.Trim());
                }
            }

            return query;
        }
        #endregion Public
    }
}
