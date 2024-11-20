namespace BiblioCore.Data.Models
{
	public class LivreModel
	{
		public int Id { get; set; }
		public string Titre { get; set; } = "";
		public bool IsDispo { get; set; } = true;
		public string AuteurModelId { get; set; } = "";
		public string RayonModelId { get; set; } = "";
	}
}
