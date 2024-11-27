using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	public interface IApiAuteurRepository
	{
		IEnumerable<AuteurModel> Get();
		IEnumerable<AuteurModel> Get(int id);
	}
}
