using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	internal interface IApiLivreRepository
	{
		IEnumerable<LivreModel> Get();
		IEnumerable<LivreModel> Get(int id);
	}
}
