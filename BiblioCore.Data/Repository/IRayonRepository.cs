using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	public interface IRayonRepository
	{
		Task<IEnumerable<RayonModel>> Get();
		Task<RayonModel?> Get(int id);

		Task<RayonModel?> Create(RayonModel model);
		Task<RayonModel?> Update(int id, RayonModel model);
		Task Delete(int id);

		Task<IEnumerable<LivreModel>> ListByRayonId(int rayonId);
	}
}
