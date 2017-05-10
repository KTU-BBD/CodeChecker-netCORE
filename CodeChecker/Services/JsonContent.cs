using System.Net.Http;
using System.Text;
using CodeChecker.Models.UserViewModels;
using Newtonsoft.Json;

namespace CodeChecker.Services
{
    public class JsonContent : StringContent
    {
        private PersonalProfilePasswordViewModel request;

        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }

    }
}