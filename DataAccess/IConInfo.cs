using System.Linq.Expressions;

namespace ConInfo.DataAccess
{
	public interface IConInfo<T>
	{
		IList<T> GetAll();
		T GetByID(Expression<Func<T, bool>> expression);
		IList<T> GetSpecial(Expression<Func<T, bool>> expression);

		int Add(T entity);
		int Edit(T entity);
		int Delete(T entity);
	}
}
