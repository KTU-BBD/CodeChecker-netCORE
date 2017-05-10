using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodeChecker.Models.Models;
using Newtonsoft.Json;
using Xunit;
using CodeChecker.Models.UserViewModels;
using CodeChecker.Services;

namespace CodeChecker.Tests.Tests.Front
{
    public class UserControllerTests : BaseTest
    {
        [Fact]
        public async Task CurrentUserEndpoint()
        {
            var response = await LoggedInAs("arvydas@daubaris.lt").GetAsync("/api/front/user/current");
            var results = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<PersonalProfileViewModel>(results);

            Assert.Equal("Arvydas@Daubaris.lt", obj.Email);
            Assert.Equal("Arvydas", obj.UserName);
        }

        [Fact]
        public async Task Topsers()
        {
            var response = await LoggedInAs("arvydas@daubaris.lt").GetAsync("/api/front/user/GetTopUsers/5");
            var results = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ICollection<TopUserViewModel>>(results);

            Assert.Equal(obj.Count, 5);

            foreach(var item in obj)
            {
                Assert.True(item.Rating > 0);
            }
        }

        [Fact]
        public async Task ChangePasswordIncorrect()
        {
            PersonalProfilePasswordViewModel request = new PersonalProfilePasswordViewModel()
            {
                CurrentPassword = "P@ssw0rd!1",
                ConfirmPassword = "P@ssw0rd!1",
                Password = "P@ssw0rd!1",
            };
            
            var response = await LoggedInAs("arvydas@daubaris.lt").PostAsync("/api/front/user/ChangePassword", new JsonContent(request));
            var results = await response.Content.ReadAsStringAsync();

            Assert.Equal("Incorrect password.", results);

        }

        [Fact]
        public async Task ChangePassword()
        {
            PersonalProfilePasswordViewModel request = new PersonalProfilePasswordViewModel()
            {
                CurrentPassword = "P@ssw0rd!",
                ConfirmPassword = "P@ssw0rd!",
                Password = "P@ssw0rd!"
            };

            var response = await LoggedInAs("arvydas@daubaris.lt").PostAsync("/api/front/user/ChangePassword", new JsonContent(request));
            var results = await response.Content.ReadAsStringAsync();

            Assert.Equal("Password changed successfully",results);

        }
    }
}
