using BiblioCore.Data.Models;

namespace BiblioCore.Data.Repository
{
	public interface IAuteurRepository
	{
		Task<IEnumerable<AuteurModel>> Get();
		Task<AuteurModel?> Get(int id);

		Task<AuteurModel?> Create(AuteurModel model);
		Task<AuteurModel?> Update(int id, AuteurModel model);
		Task Delete(int id);
	}
}
