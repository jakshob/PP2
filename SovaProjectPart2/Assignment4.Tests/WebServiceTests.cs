using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
// using Webservice.Models;
/* MASSIVE problemer med at få Webservice.Models namespacet til at virke. På nuværende tidspunkt kan det kun virke
ved at duplikere Webservice og Webservice/Models mapperne ind under Assignment4.Tests mappen. */

namespace DomainModel.Tests
{
    public class WebServiceTests
    {
        private const string postsApi = "http://localhost:5001/api/posts";

        private const string commentsApi = "http://localhost:5001/api/comments";

        private const string favoriteApi = "http://localhost:5001/api/favorites";

        

        /* /api/posts */
        
        [Fact]
        public void ApiPosts_GetWithNoArguments_OkAndAllQuestions()
        {
            var (data, statusCode) = GetObject(postsApi);
			//var (data, statusCode) = GetQuestions(postsApi);

			Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(5, data.GetValue("items").Count());
            Assert.Equal("What is the fastest way to get the value of π?",
                data.GetValue("items").First()["title"]);
			
		}


        /* /api/comments */

        [Fact]
        public void ApiComments_GetFromPost19_OkAndAllComments()
        {
            var (data, statusCode) = GetObject(commentsApi+"/fromPost/19");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(5, data.GetValue("items").Count());
            Assert.Equal(17, data.GetValue("items").First()["score"]);
            Assert.Equal("84538", data.GetValue("items").Last()["qa_UserId"]);
        }

        /* /api/favorites */


        //Den her test har problemer men er vidst tæt på at virke
        /*

        [Fact]
        public void ApiFavorites_CreateNewFavoriteAndGetIt()
        {
            var newFavorite = new
            {
                SOVAUser_Username = "Mogens",
                PostId = 19,
                Note = "I want a drier solution"
            };
            var (data, statusCode) = PostData(favoriteApi, newFavorite);

            Assert.Equal(HttpStatusCode.Created, statusCode);

        }
        */

        // Helpers

        (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) GetObject(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
		}

		(Webservice.Models.QuestionResponse, HttpStatusCode) GetQuestions(string url) {
			var client = new HttpClient();
			var response = client.GetAsync(url).Result;
			var data = response.Content.ReadAsStringAsync().Result;
			return (JsonConvert.DeserializeObject<Webservice.Models.QuestionResponse>(data), response.StatusCode);
		}

		(JObject, HttpStatusCode) PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.Created)
            {
                var tempdata = new {error = "error"};
				data = JsonConvert.SerializeObject(tempdata);

			}
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        HttpStatusCode PutData(string url, object content)
        {
            var client = new HttpClient();
            var response = client.PutAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json")).Result;
            return response.StatusCode;
        }

        HttpStatusCode DeleteData(string url)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
            return response.StatusCode;
        }
        
    }
}
