using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	internal interface IApiAuteurRepository
	{
		IEnumerable<AuteurModel> Get();
		IEnumerable<AuteurModel> Get(int id);
	}
}
