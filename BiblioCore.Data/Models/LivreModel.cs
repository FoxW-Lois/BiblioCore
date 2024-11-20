namespace BiblioCore.Data.Models
{
	public class LivreModel
	{
		public int Id { get; set; }
		public string Titre { get; set; } = "";
		public bool IsDispo { get; set; } = false;
		public string AuteurModelId { get; set; } = "";
		public string RayonModelId { get; set; } = "";
	}
}
