using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace WebAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Gestione persone");
                Console.WriteLine("1. Visualizzazione persone");
                Console.WriteLine("2. Visualizzazione singola persona");
                Console.WriteLine("3. Inserimento persona");
                Console.WriteLine("4. Modifica persona");
                Console.WriteLine("5. Eliminazione persona");
                Console.WriteLine("0. Uscita");
                Console.WriteLine();
                Console.Write("> ");
                string scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        ViewPeople();
                        break;
                    case "2":
                        ViewPerson();
                        break;
                    case "3":
                        InsertPerson();
                        break;
                    case "4":
                        UpdatePerson();
                        break;
                    case "5":
                        DeletePerson();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Scelta non riconosciuta");
                        break;

                }
            }
        }


        private static void ViewPeople()
        {
            HttpClient client = new HttpClient();
            string jsonString = client.GetStringAsync("http://localhost:50046/api/people").Result;
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(jsonString);
            foreach (Person person in people)
                Console.WriteLine("{0} {1} {2}", person.Id, person.Name, person.Age);
        }

        private static void ViewPerson()
        {
            Console.Write("Inserire l'Id della persona da visualizzare ");
            string id = Console.ReadLine();

            HttpClient client = new HttpClient();
            string jsonString = client.GetStringAsync("http://localhost:50046/api/people/" + id).Result;
            Person person = JsonConvert.DeserializeObject<Person>(jsonString);
            Console.WriteLine("{0} {1} {2}", person.Id, person.Name, person.Age);
        }

        private static void InsertPerson()
        {
            Console.Write("Inserire il nome della persona ");
            string name = Console.ReadLine();
            Console.Write("Inserire l'età della persona ");
            int age = int.Parse(Console.ReadLine());

            Person person = new Person()
            {
                Name = name,
                Age = age
            };

            string jsonString = JsonConvert.SerializeObject(person);

            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            client.PostAsync("http://localhost:50046/api/people", content);
        }

        private static void UpdatePerson()
        {
            Console.Write("Inserire l'Id della persona da modificare ");
            string id = Console.ReadLine();

            Console.Write("Inserire il nome della persona ");
            string name = Console.ReadLine();
            Console.Write("Inserire l'età della persona ");
            int age = int.Parse(Console.ReadLine());

            Person person = new Person()
            {
                Id = int.Parse(id),
                Name = name,
                Age = age
            };

            string jsonString = JsonConvert.SerializeObject(person);

            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            client.PutAsync("http://localhost:50046/api/people/" + id, content);

        }

        private static void DeletePerson()
        {
            Console.Write("Inserire l'Id della persona da eliminare ");
            string id = Console.ReadLine();

            HttpClient client = new HttpClient();
            client.DeleteAsync("http://localhost:50046/api/people/" + id);
        }

    }
}
