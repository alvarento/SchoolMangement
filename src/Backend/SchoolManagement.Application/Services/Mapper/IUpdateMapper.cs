namespace SchoolManagement.Application.Services.Mapper
{
	public interface IUpdateMapper<TSource, TEntity>
	{
		TEntity Map(TSource request, TEntity entity);
	}
}
