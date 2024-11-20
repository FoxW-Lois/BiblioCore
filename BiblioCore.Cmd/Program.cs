using BiblioCore.Data.Models;
using BiblioCore.Data.Repository;
using BiblioCore.Data.Repository.Api;
using System.Text;

namespace BiblioCore.Cmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! \n");

            Thread.Sleep(2000); // Attente du server
            ILivreRepository repository = new ApiLivreRepository("http://localhost:5049/api/");
            GetLivres(repository);
            GetLivre(repository);
            //CreateLivre(repository);
            //UpdateLivre(repository);
            //DeleteLivre(repository);
            Console.ReadLine();
        }
        private static void CreateLivre(ILivreRepository repository)
        {
            var Livre = new LivreModel() { Id = 4, Titre = "Soleil" };
            Livre = repository.Create(Livre).Result;
            if (Livre == null)
                Console.WriteLine("Livre introuvable");

            else
                Console.WriteLine($"{Livre.Id}{Livre.Titre} \n");
        }

        private static void DeleteLivre(ILivreRepository repository)
        {
            int id = GetId("Id du Livre à supprimer :");
            if (id == 0)
                return;
            repository.Delete(id).Wait();
        }

        private static void UpdateLivre(ILivreRepository repository)
        {
            int id = GetId("Id de la Livrene à modifier :");
            if (id == 0)
                return;
            var model = repository.Get(id).Result;
            if (model == null)
            {
                Console.WriteLine("Président introuvable");
            }
            else
            {
                model.Titre = "Trouduc";
                model = repository.Update(id, model).Result;
                Console.WriteLine($"{model.Id}{model.Titre} \n");
            }
        }


        private static void GetLivre(ILivreRepository repository)
        {
            while (true)
            {
                int id = GetId("Id de la Livre :");
                if (id == 0)
                    return;
                var model = repository.Get(id).Result;
                if (model == null)
                    Console.WriteLine("Livre introuvable");
                else
                    Console.WriteLine($"{model.Id} {model.Titre}\n");
            }
        }

        private static int GetId(string message)
        {
            do
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int id))
                    return id;
            } while (true);
        }

        private static void GetLivres(ILivreRepository repository)
        {
            var models = repository.Get().Result;
            if (models == null)
            {
                Console.WriteLine("Erreur Api");
                return;
            }
            Console.WriteLine(string.Join("\n", models.Select(x => $"{x.Id}{x.Titre} ")));
        }
    }
}

