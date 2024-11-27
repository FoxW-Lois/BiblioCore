using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	public interface IApiMembreRepository
	{
		IEnumerable<MembreModel> Get();
		IEnumerable<MembreModel> Get(int id);
	}
}
