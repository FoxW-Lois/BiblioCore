using BiblioCore.Data.Models;
using System.Net.Http.Json;

namespace BiblioCore.Data.Repository.Api
{
	public class ApiRayonRepository : IRayonRepository
	{
		private readonly string url;
		private readonly HttpClient client; // Fait les requêtes HTTP

		public ApiRayonRepository(string url)
		{
			this.url = url;
			client = new HttpClient();
			client.BaseAddress = new Uri(url);
		}

		public async Task<RayonModel?> Create(RayonModel model)
		{
			var response = await client.PostAsJsonAsync($"{url}rayon", model);
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<RayonModel>();
			return null;
		}

		public async Task Delete(int id)
		{
			var response = await client.DeleteAsync($"{url}rayon/{id}");
		}

		public async Task<IEnumerable<RayonModel>?> Get()
		{
			var reponse = await client.GetAsync($"{url}rayon"); // Correspond à l'url de l'action du controller
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<IEnumerable<RayonModel>>();
			return null;
		}

		public async Task<RayonModel?> Get(int id)
		{
			var reponse = await client.GetAsync($"{url}rayon/{id}");
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<RayonModel>();
			return null;
		}

		public async Task<IEnumerable<LivreModel>?> ListByRayonId(int rayonId)
		{
			var response = await client.GetAsync($"{url}rayon?rayonId={rayonId}");
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<IEnumerable<LivreModel>>();
			return null;
		}

		public async Task<RayonModel?> Update(int id, RayonModel model)
		{
			var response = await client.PutAsJsonAsync($"{url}rayon/{id}", model);
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<RayonModel>();
			return null;
		}
	}
}
