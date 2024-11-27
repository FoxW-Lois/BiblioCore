using BiblioCore.Data.Models;
using System.Net.Http.Json;

namespace BiblioCore.Data.Repository.Api
{
	public class ApiMembreRepository : IMembreRepository
	{
		private readonly string url;
		private readonly HttpClient client; // Fait les requêtes HTTP

		public ApiMembreRepository(string url)
		{
			this.url = url;
			client = new HttpClient();
			client.BaseAddress = new Uri(url);
		}

		public async Task<IEnumerable<MembreModel>?> Get()
		{
			var reponse = await client.GetAsync($"{url}membre"); // Correspond à l'url de l'action du controller
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<IEnumerable<MembreModel>>();
			return null;
		}
		
		public async Task<MembreModel?> Get(int id)
		{
			var reponse = await client.GetAsync($"{url}membre/{id}");
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<MembreModel>();
			return null;
		}

		//public async Task<LivreModel?> BorrowOneLivre(int id, LivreModel model, int MembreModelId)
		//{
		//	var response = await client.PutAsJsonAsync($"{url}membre/{id}", model);
		//	if (response.IsSuccessStatusCode)
		//		return await response.Content.ReadFromJsonAsync<LivreModel>();
		//	return null;
		//}
	}
}
