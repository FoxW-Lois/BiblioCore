using BiblioCore.Data.Models;
using System.Net.Http.Json;

namespace BiblioCore.Data.Repository.Api
{
	public class ApiAuteurRepository : IAuteurRepository
	{
		private readonly string url;
		private readonly HttpClient client; // Fait les requêtes HTTP

		public ApiAuteurRepository(string url)
		{
			this.url = url;
			client = new HttpClient();
			client.BaseAddress = new Uri(url);
		}

		public async Task<AuteurModel?> Create(AuteurModel model)
		{
			var response = await client.PostAsJsonAsync($"{url}auteur", model);
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<AuteurModel>();
			return null;
		}

		public async Task Delete(int id)
		{
			var response = await client.DeleteAsync($"{url}auteur/{id}");
		}

		public async Task<IEnumerable<AuteurModel>?> Get()
		{
			var reponse = await client.GetAsync($"{url}auteur"); // Correspond à l'url de l'action du controller
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<IEnumerable<AuteurModel>>();
			return null;
		}

		public async Task<AuteurModel?> Get(int id)
		{
			var reponse = await client.GetAsync($"{url}auteur/{id}");
			if (reponse.IsSuccessStatusCode)
				return await reponse.Content.ReadFromJsonAsync<AuteurModel>();
			return null;
		}

        public async Task<IEnumerable<LivreModel>?> ListByAuteurId(int auteurId)
        {
            var response = await client.GetAsync($"{url}auteur?auteurId={auteurId}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<IEnumerable<LivreModel>>();
            return null;
        }

        public async Task<AuteurModel?> Update(int id, AuteurModel model)
		{
			var response = await client.PutAsJsonAsync($"{url}auteur/{id}", model);
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadFromJsonAsync<AuteurModel>();
			return null;
		}
	}
}
