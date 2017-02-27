using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace WebAPIClient
{
    public class Crud<T> where T : new()
    {
        private readonly string _baseApiUrl;
        public HttpClient Client { get; set; }

        public Crud(string baseApiUrl)
        {
            _baseApiUrl = baseApiUrl;
            Client = new HttpClient();
        }

        public List<T> GetList()
        {
            string jsonString = Client.GetStringAsync(_baseApiUrl).Result;
            List<T> list = JsonConvert.DeserializeObject<List<T>>(jsonString);
            return list;
        }

        public T Get(object id)
        {
            string jsonString = Client.GetStringAsync(_baseApiUrl + "/" + id).Result;
            T entity = JsonConvert.DeserializeObject<T>(jsonString);
            return entity;
        }

        public void Insert(T entity)
        {
            string jsonString = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            Client.PostAsync(_baseApiUrl, content);
        }

        public void Update(object id, T entity)
        {
            string jsonString = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            Client.PutAsync(_baseApiUrl + "/" + id, content);
        }

        public void Delete(object id)
        {
            Client.DeleteAsync(_baseApiUrl + "/" + id);
        }

    }
}
