using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace DomainModel.Tests
{
    public class WebServiceTests
    {
        private const string postsApi = "http://localhost:5001/api/posts";

        private const string commentsApi = "http://localhost:5001/api/comments";

        private const string favoriteApi = "http://localhost:5001/api/favorites";

        //private const string ProductsApi = "http://localhost:5001/api/products";

        /* /api/posts */
        
        [Fact]
        public void ApiPosts_GetWithNoArguments_OkAndAllQuestions()
        {
            var (data, statusCode) = GetObject(postsApi);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(5, data.GetValue("items").Count());
            Assert.Equal("What is the fastest way to get the value of π?",
                data.GetValue("items").First()["name"]);
           
        }


        /* /api/comments */

        [Fact]
        public void ApiComments_GetFromPost19_OkAndAllComments()
        {
            var (data, statusCode) = GetObject(commentsApi+"/fromPost/19");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(5, data.GetValue("items").Count());
            Assert.Equal(17, data.GetValue("items").First()["score"]);
            Assert.Equal("84538", data.GetValue("items").Last()["qa_userid"]);
        }

        /* /api/favorites */

        
        //Den her test har problemer men er vidst tæt på at virke
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
        /*
        [Fact]
        public void ApiFavorites_CreateNewFavoriteAndGetIt()
        {
            Favorite testFavorite = new Favorite
            {
                PostId = 19,
                SOVA_UserUsername = "007",
                Note = "I want a dry solution"
            };
            PostData(favoriteApi, testFavorite);

            var (data, statusCode) = GetObject(favoriteApi+"/007");
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("I want a dry solution", data.GetValue("items").First()["note"]);
        }
        /*
        [Fact]
        public void ApiAnswerToQuestions_GetWithID_OkAndOneQuestionAndAnswers()
        {
            var (data, statusCode) = GetObject($"{postsApi}/answersToQuestion/19");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Beverages", data["name"]);
        }

        /*
        [Fact]
        public void ApiCategories_GetWithInvalidCategoryId_NotFound()
        {
            var (category, statusCode) = GetObject($"{CategoriesApi}/0");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void ApiCategories_PostWithCategory_Created()
        {
            var newCategory = new
            {
                Name = "Created",
                Description = ""
            };
            var (category, statusCode) = PostData(CategoriesApi, newCategory);

            Assert.Equal(HttpStatusCode.Created, statusCode);

            DeleteData($"{CategoriesApi}/{category["id"]}");
        }

        [Fact]
        public void ApiCategories_PutWithValidCategory_Ok()
        {

            var data = new
            {
                Name = "Created",
                Description = "Created"
            };
            var (category, _) = PostData($"{CategoriesApi}", data);

            var update = new
            {
                Id = category["id"],
                Name = category["name"] + "Updated",
                Description = category["description"] + "Updated"
            };

            var statusCode = PutData($"{CategoriesApi}/{category["id"]}", update);

            Assert.Equal(HttpStatusCode.OK, statusCode);

            var (cat, status) = GetObject($"{CategoriesApi}/{category["id"]}");

            Assert.Equal(category["name"] + "Updated", cat["name"]);
            Assert.Equal(category["description"] + "Updated", cat["description"]);

            DeleteData($"{CategoriesApi}/{category["id"]}");
        }

        [Fact]
        public void ApiCategories_PutWithInvalidCategory_NotFound()
        {
            var update = new
            {
                Id = -1,
                Name = "Updated",
                Description = "Updated"
            };

            var statusCode = PutData($"{CategoriesApi}/-1", update);

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void ApiCategories_DeleteWithValidId_Ok()
        {

            var data = new
            {
                Name = "Created",
                Description = "Created"
            };
            var (category, _) = PostData($"{CategoriesApi}", data);

            var statusCode = DeleteData($"{CategoriesApi}/{category["id"]}");

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void ApiCategories_DeleteWithInvalidId_NotFound()
        {

            var statusCode = DeleteData($"{CategoriesApi}/-1");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        /* /api/products */
        /*
        [Fact]
        public void ApiProducts_ValidId_CompleteProduct()
        {
            var (product, statusCode) = GetObject($"{ProductsApi}/1");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Chai", product["name"]);
            Assert.Equal("Beverages", product["categoryName"]);
        }

        [Fact]
        public void ApiProducts_InvalidId_CompleteProduct()
        {
            var (product, statusCode) = GetObject($"{ProductsApi}/-1");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void ApiProducts_CategoryValidId_ListOfProduct()
        {
            var (products, statusCode) = GetArray($"{ProductsApi}/category/1");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(12, products.Count);
            Assert.Equal("Chai", products.First()["productName"]);
            Assert.Equal("Beverages", products.First()["categoryName"]);
            Assert.Equal("Lakkalikööri", products.Last()["productName"]);
        }

        [Fact]
        public void ApiProducts_CategoryInvalidId_EmptyListOfProductAndNotFound()
        {
            var (products, statusCode) = GetArray($"{ProductsApi}/category/1000001");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
            Assert.Equal(0, products.Count);
        }

        [Fact]
        public void ApiProducts_NameContained_ListOfProduct()
        {
            var (products, statusCode) = GetArray($"{ProductsApi}/name/ant");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(3, products.Count);
            Assert.Equal("Chef Anton's Cajun Seasoning", products.First()["name"]);
            Assert.Equal("Guaraná Fantástica", products.Last()["name"]);
        }

        [Fact]
        public void ApiProducts_NameNotContained_EmptyListOfProductAndNotFound()
        {
            var (products, statusCode) = GetArray($"{ProductsApi}/name/RAWDATA");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
            Assert.Equal(0, products.Count);
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

        (JObject, HttpStatusCode) PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            /*if (response.StatusCode != HttpStatusCode.OK)
            {
                data = new {error = "error"};
            }*/
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
