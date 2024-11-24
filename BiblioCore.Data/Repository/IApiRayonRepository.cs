using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	internal interface IApiRayonRepository
	{
		IEnumerable<RayonModel> Get();
		IEnumerable<RayonModel> Get(int id);
	}
}
