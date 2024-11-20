using BiblioCore.Data.Models;
using BiblioCore.Data;

namespace BiblioCore.Data.Repository.Sql
{
	public class SqlLivreRepository : ILivreRepository
	{
		public Task<LivreModel?> Create(LivreModel model)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<LivreModel>> Get()
		{
			throw new NotImplementedException();
		}

		public Task<LivreModel?> Get(int id)
		{
			throw new NotImplementedException();
		}

		public Task<LivreModel?> Update(int id, LivreModel model)
		{
			throw new NotImplementedException();
		}
	}
}
