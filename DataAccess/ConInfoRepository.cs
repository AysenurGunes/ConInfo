using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConInfo.DataAccess
{
	public class ConInfoRepository<TEntity, TContext> : IConInfo<TEntity>
		where TEntity : class, new()
		where TContext : ConInfoDbContext, new()
	{
		private readonly ConInfoDbContext _dbContext;
		private readonly DbSet<TEntity> Table;
		public ConInfoRepository()
		{
			_dbContext = new TContext();
			Table= _dbContext.Set<TEntity>();
		}
		public int Add(TEntity entity)
		{
			try
			{

				_dbContext.Entry(entity).State = EntityState.Added;
				_dbContext.SaveChanges();
				return StatusCodes.Status200OK;
			}
			catch (Exception)
			{

				return StatusCodes.Status400BadRequest;
			}
		}

		public int Delete(TEntity entity)
		{
			try
			{
				//var entit = _dbContext.Set<TEntity>().Where(expression).FirstOrDefault();
				_dbContext.Entry(entity).State = EntityState.Modified;
				_dbContext.SaveChanges();
				return StatusCodes.Status200OK;
			}
			catch (Exception)
			{
				return StatusCodes.Status400BadRequest;
			}
		}

		public int Edit(TEntity entity)
		{
			try
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
				_dbContext.SaveChanges();
				return StatusCodes.Status200OK;
			}
			catch (Exception)
			{
				return StatusCodes.Status400BadRequest;
			}
		}

		public IList<TEntity> GetAll()
		{
			try
			{
				return Table.AsNoTracking().ToList();

			}
			catch (Exception)
			{
				return new List<TEntity>();
			}
		}

		public TEntity GetByID(Expression<Func<TEntity, bool>> expression)
		{
			try
			{
				return _dbContext.Set<TEntity>().Where(expression).FirstOrDefault();
			}
			catch (Exception)
			{
				return new TEntity();
			}
		}
		public IList<TEntity> GetSpecial(Expression<Func<TEntity, bool>> expression)
		{
			try
			{
				return _dbContext.Set<TEntity>().Where(expression).ToList();
			}
			catch (Exception)
			{
				return new List<TEntity>();
			}
		}


	}
}
