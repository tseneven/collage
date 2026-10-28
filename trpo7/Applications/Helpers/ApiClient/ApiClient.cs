using System.Net.Http;

namespace trpo7.Applications.Helpers.ApiClient
{
    public sealed class ApiClient
    {
        public static ApiClient Instance { get; } = new();

        public HttpClient Client { get; } = new()
        {
            BaseAddress = new Uri("http://localhost:5278/api/v1/")
        };
        
        private ApiClient(){}
    }
}

