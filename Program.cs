using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;


namespace ConsoleAppNETCore
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter Username");
            
            string endpoint = "https://api.github.com/users/glitchkn1ght/repos";
            string token = "b3545e2d00e8a66b8a2b7129d44f87ba6d168d03";
           // string user = Console.ReadLine();// + "/repos";

            Console.WriteLine(endpoint);

            await ProcessRepositories(endpoint, token);


        }

        private static async Task ProcessRepositories(string end, string tok )
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "glitchkn1ght");
            // client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", tok);

            var streamTask = client.GetStreamAsync(end);
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

     
            foreach(Repository r in repositories)
            {
                Console.WriteLine(r.name);
                Console.WriteLine(r.owner.id);
            }
        }

        
    }
    public class Repository
    {
        public int id { get; set; }
        public string name { get; set; }
        public RepoOwner owner { get; set; }    
    }
    public class RepoOwner
    {
        public string login { get; set; }
        public int id { get; set; }
    }
}
