using BiblioCore.Data.Models;
using BiblioCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BiblioCore.Data.Repository.Sql
{
	public class SqlRayonRepository : IRayonRepository
	{
		private readonly DataContext context;

		public SqlRayonRepository(DataContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<RayonModel>> Get()
		{
			return await context.Set<RayonModel>().ToListAsync(); // Générique
		}

		public async Task<RayonModel?> Get(int id)
		{
			return await context.Set<RayonModel>().FirstOrDefaultAsync(x => x.Id == id); // Pas d'exception générique
		}

		public async Task<RayonModel?> Create(RayonModel model)
		{
			context.Set<RayonModel>().Add(model); // Rend le repository générique
			await context.SaveChangesAsync(); // Execution de la requete
			return model;
		}

		public async Task<RayonModel?> Update(int id, RayonModel model)
		{
			var entity = await context.Set<RayonModel>().FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
				return null;
			context.Entry(entity).CurrentValues.SetValues(model);
			await context.SaveChangesAsync();
			return entity;
		}

		public async Task Delete(int id)
		{
			var model = await context.Set<RayonModel>().FirstOrDefaultAsync(x => x.Id == id);
			if (model == null)
				return;
			context.Set<RayonModel>().Remove(model);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<LivreModel>> ListByRayonId(int rayonId)
		{
			return await context.Set<LivreModel>().Where(x => x.RayonModelId == rayonId).ToListAsync();
		}
	}
}
