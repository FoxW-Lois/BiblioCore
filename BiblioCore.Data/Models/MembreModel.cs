namespace BiblioCore.Data.Models
{
	internal class MembreModel
	{
		public int Id { get; set; }
		public string LastName { get; set; } = "";
		public string FirstName { get; set; } = "";
		public int LivreModelId { get; set; }
	}
}
