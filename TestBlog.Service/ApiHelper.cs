using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace TestBlog.Service
{
    public class ApiHelper<T>
    {
        public RestClient client { get; set; }
        public string EndPoint { get; set; }
        
        public ApiHelper(string endPoint)
        {
            client = new RestClient("http://localhost:56383/api");
            this.EndPoint = endPoint;
        }

        public IEnumerable<T> GetItems()
        {
            var request = new RestRequest(EndPoint, Method.GET);
            var response = client.Execute<IEnumerable<T>>(request);
            return response.Data;
        }
        public IEnumerable<T> GetItem(int id)
        {
            var request = new RestRequest(EndPoint + "/" + id, Method.GET);
            var response = client.Execute<IEnumerable<T>>(request);
            return response.Data;
        }
        public IEnumerable<T> GetComment(int id)
        {
            var request = new RestRequest("blog/comment", Method.GET);
            request.AddParameter("id", id);
            var response = client.Execute<IEnumerable<T>>(request);
            return response.Data;
        }
        public IEnumerable<T> GetItems(string applicationUserId)
        {
            var request = new RestRequest(EndPoint + "/byuser/" , Method.GET);
            request.AddParameter("appId", applicationUserId);
            var response = client.Execute<IEnumerable<T>>(request);
            return response.Data;
        }
        public IEnumerable<T> SearchItem(string searchData, string id)
        {
            var request = new RestRequest(EndPoint + "/search", Method.GET);
            request.AddParameter("data", searchData);
            request.AddParameter("id", id);
            var response = client.Execute<IEnumerable<T>>(request);
            return response.Data;
        }
        public T UpdateItem(T data)
        {
            var request = new RestRequest(EndPoint + "/update", Method.PUT);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.Execute<T>(request);
            return response.Data;
        }
        public T PostBlog(T data)
        {
            var request = new RestRequest(EndPoint + "/insertblog", Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.Execute<T>(request);
            return response.Data;
        }
        public T PostComment(T data)
        {
            var request = new RestRequest("blog/insertcomment", Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.Execute<T>(request);
            return response.Data;
        }

        public IEnumerable<T> GetComments(string userId)
        {
            var request = new RestRequest("blog/insertcomment", Method.GET);
            request.AddParameter("userId", userId);
            var response = client.Execute<IEnumerable<T>>(request);
            return response.Data;
        }

        public HttpStatusCode DeleteItem(string id)
        {
            var request = new RestRequest(EndPoint + "/deleteblog/" + id, Method.DELETE);
            var response = client.Execute(request);
            return response.StatusCode;
        }
    }
}
