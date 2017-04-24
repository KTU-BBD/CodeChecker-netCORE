using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodeChecker.Models.ServiceViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CodeChecker.Services.CodeSubmit
{
    public class CodeSubmitService
    {
        private readonly AppSettings _settings;
        private HttpClient _client;

        public CodeSubmitService(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CodeResultViewModel> SubmitCode(CodeSubmitViewModel submitCode)
        {
            var result = await _client.PostAsync(_settings.Microservice.Uri, new JsonContent(submitCode));
            var results = JsonConvert.DeserializeObject<CodeResultViewModel>(await result.Content.ReadAsStringAsync());

            return results;

        }
    }
}