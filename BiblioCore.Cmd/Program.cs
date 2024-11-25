using BiblioCore.Data.Models;
using BiblioCore.Data.Repository;
using BiblioCore.Data.Repository.Api;

namespace BiblioCore.Cmd
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Bienvenue sur l'API BiblioCore !\n");
			Thread.Sleep(1000); // Attente du server
			ILivreRepository repository = new ApiLivreRepository("http://localhost:5049/api/");

			while (true)
			{
                Console.WriteLine("+---------------------------------------+\n" +
								"|       Que voulez-vous faire ?         |\n" +
								"+---------------------------------------+\n" +
								"|                                       |\n" +
								"|       Voir tous les livres : 1        |\n" +
								"|        Voir un seul livre : 2         |\n" +
								"|         Ajouter un livre : 3          |\n" +
								"|         Modifier un livre : 4         |\n" +
								"|        Supprimer un livre : 5         |\n" +
								"|   Assigner un rayon à un livre : 6    |\n" +
								"|    Lister les livres par rayon : 7    |\n" +
								"|   Lister les livres par auteur : 8    |\n" +
								"|   Rechercher un livre par titre : 9   |\n" +
								"|        Emprunter un livre : 10        |\n" +
								"|         Rendre un livre : 11          |\n" +
								"|   Lister les livres empruntés : 12    |\n" +
								"|                                       |\n" +
								"+---------------------------------------+\n"
				);

                if (!int.TryParse(Console.ReadLine(), out int Action) || Action < 1 || Action > 7)
					Console.Write("Cette Action n'est pas valide. Veuillez réessayer\n\n");

				switch (Action) {
					case 1 :
						Console.WriteLine("\n");
						GetLivres(repository);
						Console.WriteLine("\n");
					break;

					case 2:
						Console.WriteLine("\n");
						GetLivre(repository);
						Console.WriteLine("\n");
					break;

					case 3:
						Console.WriteLine("\n");
						CreateLivre(repository);
						Console.WriteLine("\n");
					break;

					case 4:
						Console.WriteLine("\n");
						UpdateLivre(repository);
						Console.WriteLine("\n");
					break;

					case 5:
						Console.WriteLine("\n");
						DeleteLivre(repository);
						Console.WriteLine("\n");
					break;

					case 6:
						Console.WriteLine("\n");
						AssignRayonToLivre(repository);
						Console.WriteLine("\n");
					break;

                    case 7:
                        Console.WriteLine("\n");
                        ListLivreByRayon(repository);
                        Console.WriteLine("\n");
                    break;
                }
			}
		}

		// ------------------------------ Fonctions CRUD ------------------------------

		private static void GetLivres(ILivreRepository repository)
		{
			var models = repository.Get().Result;
			if (models == null)
			{
				Console.WriteLine("Erreur Api");
				return;
			}

			Console.WriteLine(
							"+----------------------------------------------------+\n" +
							"|\n" +
							string.Join("\n", models.Select(x => $"|    - Livre n°{x.Id} : {x.Titre}, Rayon : {x.RayonModelId}")) +
							"\n|\n" +
							"+----------------------------------------------------+");
		}

		private static void GetLivre(ILivreRepository repository)
		{
			while (true)
			{
				int id = GetId("+----------------------------------------------------+\n" +
								"|    - Id du Livre à voir : ");
				if (id == 0)
					return;
				var model = repository.Get(id).Result;
				if (model == null)
				{
					Console.WriteLine("+----------------------------------------------------+\n");
					Console.WriteLine("Livre introuvable\n");
				}
				else
					Console.WriteLine(
							"+----------------------------------------------------+\n" +
							"|\n" +
							$"|    Livre n°{model.Id} : {model.Titre}" +
							"\n|\n" +
							"+----------------------------------------------------+\n");

				//ContinueOrNo();
				do
				{
					Console.WriteLine("Voulez-vous continuer ? (y/n)");
					string reponse = Console.ReadLine();
					if (reponse == "y")
					{
						Console.WriteLine("\n");
						break;
					}
					else if (reponse == "n")
						return;
					else
					{
						Console.WriteLine("Erreur veuillez réessayer");
					}
				} while (true);
			}
		}

		private static void CreateLivre(ILivreRepository repository)
		{
			while (true)
			{
				Console.Write("+----------------------------------------------------+\n" +
									"|   Entrez un titre pour le nouveau Livre : ");
				string lTitre = Console.ReadLine();
				Console.WriteLine("+----------------------------------------------------+");

				Console.Write("|   Entrez l'Id de l'auteur du nouveau Livre : ");
				if (!int.TryParse(Console.ReadLine(), out int lAuteurModelId) && lAuteurModelId == 0)
				{
					Console.WriteLine("L'Id doit être exclusivement composé chiffre");
					return;
				}
				if (lAuteurModelId == 0)
				{
					return;
				}
				Console.WriteLine("+----------------------------------------------------+\n");

				var Livre = new LivreModel() { Titre = lTitre, AuteurModelId = lAuteurModelId };
				Livre = repository.Create(Livre).Result;

				if (Livre == null)
					Console.WriteLine("Livre introuvable\n");
				else
				{
					//Récupère en base de données le nom de l'auteur correspondant à AuteurModelId
					//string NomAuteur = ;
					Console.WriteLine($"Le livre n°{Livre.Id} : {Livre.Titre}, Auteur : {Livre.AuteurModelId}" /*NomAuteur */ + " a été ajouté\n");

				}

				//ContinueOrNo();
				do
				{
					Console.WriteLine("Voulez-vous continuer ? (y/n)");
					string reponse = Console.ReadLine();
					if (reponse == "y")
					{
						Console.WriteLine("\n");
						break;
					}
					else if (reponse == "n")
						return;
					else
					{
						Console.WriteLine("Erreur veuillez réessayer");
					}
				} while (true);
			}
		}

		private static void UpdateLivre(ILivreRepository repository)
		{
			int id = GetId("+----------------------------------------------------+\n" +
							"|   Id du Livre à modifier : ");
			if (id == 0)
				return;

			var model = repository.Get(id).Result;

			if (model == null)
			{
				Console.WriteLine("+----------------------------------------------------+\n");
				Console.WriteLine("Livre introuvable\n");
			}
			else
			{
				Console.Write("+----------------------------------------------------+\n" +
									"|   Entrez le nouveau titre du Livre : ");
				string lNouveauTitre = Console.ReadLine();
				Console.WriteLine("+----------------------------------------------------+");

				model.Titre = lNouveauTitre;
				model = repository.Update(id, model).Result;
				Console.WriteLine($"Mise à jour du livre n°{model.Id} : {model.Titre} \n");
			}
		}

		private static void DeleteLivre(ILivreRepository repository)
		{
			int id = GetId("+----------------------------------------------------+\n" +
							"|   Id du Livre à supprimer : ");
			if (id == 0)
				return;
			Console.WriteLine("+----------------------------------------------------+\n");
			repository.Delete(id).Wait();
			Console.WriteLine("Suppression effectuée\n");
		}


		// ----------------------------- Fonctionnalités ------------------------------

		private static void AssignRayonToLivre(ILivreRepository repository)
		{
			while (true)
			{
				int id = GetId("+----------------------------------------------------+\n" +
								"|   Id du Livre à ranger : ");
				if (id == 0)
					return;

				Console.WriteLine("+----------------------------------------------------+\n");

				var model = repository.Get(id).Result;

				if (model == null)
					Console.WriteLine("Livre introuvable");
				else
				{
					Console.Write("+----------------------------------------------------+\n" +
										"|   Entrez l'Id du Rayon du Livre : ");

					if (!int.TryParse(Console.ReadLine(), out int lRayonModelId))
					{
						Console.WriteLine("L'Id doit être exclusivement composé chiffre");
						return;
					}
					Console.WriteLine("+----------------------------------------------------+");

					model.RayonModelId = lRayonModelId;
					model = repository.AssignRayon(id, model, lRayonModelId).Result;
					Console.WriteLine($"Mise à jour du livre n°{model.Id} : {model.Titre}, Rayon : {model.RayonModelId} \n");
				}

				//ContinueOrNo();
				do
				{
					Console.WriteLine("Voulez-vous continuer ? (y/n)");
					string reponse = Console.ReadLine();
					if (reponse == "y")
					{
						Console.WriteLine("\n");
						break;
					}
					else if (reponse == "n")
						return;
					else
					{
						Console.WriteLine("Erreur veuillez réessayer");
					}
				} while (true);
			}
		}

		private static void ListLivreByRayon(ILivreRepository repository)
		{
			while (true)
			{
				int id = GetId("+----------------------------------------------------+\n" +
								"|   Id du Rayon à afficher : ");
				if (id == 0)
					return;

				var model = repository.ListByRayonId(id).Result;
				if (model == null || !model.Any())
				{
					Console.WriteLine("+----------------------------------------------------+\n");
					Console.WriteLine("Rayon introuvable ou aucun livre trouvé\n");
				}
				else
				{
					//if (!int.TryParse(Console.ReadLine(), out int lRayonModelId))
					//{
					//	Console.WriteLine("L'Id doit être exclusivement composé chiffre");
					//	return;
					//}
					//livres.RayonModelId = lRayonModelId;
					//model = repository.ListRayon(id, model, lRayonModelId).Result;
					Console.WriteLine(
							"+----------------------------------------------------+\n" +
							"|\n" +
							string.Join("\n", model.Select(l => $"|    - Livre n°{l.Id} : {l.Titre}, Rayon : {l.RayonModelId}")) +
							"\n|\n" +
							"+----------------------------------------------------+");
				}

				//ContinueOrNo();
				do
				{
					Console.WriteLine("Voulez-vous continuer ? (y/n)\n");
					string reponse = Console.ReadLine();
					if (reponse == "y")
					{
						Console.WriteLine("\n");
						break;
					}
					else if (reponse == "n")
						return;
					else
					{
						Console.WriteLine("Erreur veuillez réessayer");
					}
				} while (true);
			}
		}

        // ----------------------------------------------------------------------------

        private static int GetId(string message)
		{
			do
			{
				Console.Write(message);
				if (int.TryParse(Console.ReadLine(), out int id))
					return id;
			} while (true);
		}

		//private static void ContinueOrNo()
		//{
		//	do
		//	{
		//		Console.WriteLine("Voulez-vous continuer ? (y/n)\n");
		//		string reponse = Console.ReadLine();
		//		if (reponse == "y")
		//		{
		//			Console.WriteLine("\n");
		//			break;
		//		}
		//		else if (reponse == "n")
		//			return;
		//		else
		//		{
		//			Console.WriteLine("Erreur veuillez réessayer");
		//		}
		//	} while (true);
		//}
	}
}

