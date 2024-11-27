using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	public interface IApiLivreRepository
	{
		IEnumerable<LivreModel> Get();
		IEnumerable<LivreModel> Get(int id);
	}
}
