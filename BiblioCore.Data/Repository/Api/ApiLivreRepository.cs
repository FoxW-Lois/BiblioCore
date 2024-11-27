using BiblioCore.Data.Models;
using System.Net.Http.Json;

namespace BiblioCore.Data.Repository.Api
{
	public class ApiLivreRepository : ILivreRepository
	{
		private readonly string url;
		private readonly HttpClient client; // Fait les requêtes HTTP

		public ApiLivreRepository(string url)
		{
			this.url = url;
			client = new HttpClient();
			client.BaseAddress = new Uri(url);
		}

		public async Task<LivreModel?> AssignRayon(int id, LivreModel model, int RayonModelId)
		{
			var response = await client.PutAsJsonAsync($"{url}livre/{id}", model);
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<LivreModel>();
			return null;
		}

		public async Task<LivreModel?> Create(LivreModel model)
		{
			var response = await client.PostAsJsonAsync($"{url}livre", model);
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<LivreModel>();
			return null;
		}

		public async Task Delete(int id)
		{
			var response = await client.DeleteAsync($"{url}livre/{id}");
		}

		public async Task<IEnumerable<LivreModel>?> Get()
		{
			var reponse = await client.GetAsync($"{url}livre"); // Correspond à l'url de l'action du controller
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<IEnumerable<LivreModel>>();
			return null;
		}

		public async Task<LivreModel?> Get(int id)
		{
			var reponse = await client.GetAsync($"{url}livre/{id}");
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<LivreModel>();
			return null;
		}

		public async Task<LivreModel?> ListRayon(int id, LivreModel model, int rayonModelId)
		{
			var reponse = await client.GetAsync($"{url}rayon/{rayonModelId}");
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<LivreModel>();
			return null;
		}

		//public async Task<LivreModel?> SearchByTitre(string titre)
		//{
		//	var reponse = await client.GetAsync($"{url}livre/{titre}");
		//	if (reponse.IsSuccessStatusCode)
		//		return await reponse.Content.ReadFromJsonAsync<LivreModel>();
		//	return null;
		//}

		public async Task<LivreModel?> Update(int id, LivreModel model)
		{
			var response = await client.PutAsJsonAsync($"{url}livre/{id}", model);
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<LivreModel>();
			return null;
		}

		public async Task<LivreModel?> BorrowOneLivre(int id, LivreModel model, int MembreModelId, bool isDispo)
		{
			var response = await client.PutAsJsonAsync($"{url}livre/{id}", model);
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<LivreModel>();
			return null;
		}

        public async Task<LivreModel?> ReturnOneLivre(int id, LivreModel model, int MembreModelId, bool isDispo)
        {
            var response = await client.PutAsJsonAsync($"{url}livre/{id}", model);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<LivreModel>();
            return null;
        }
    }
}
