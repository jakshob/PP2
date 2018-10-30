using System;
using System.Linq;
using Xunit;

namespace DomainModel.Tests
{
    public class DataServiceTests
    {
        /* Posts */

        [Fact]
        public void Post_Object_HasIdNameAndDescription()
        {
            var p = new Question();
            Assert.Equal(0, p.Id);
            Assert.Equal(0, p.Posttype);
            //Assert.Null(p.Body);
        }

        [Fact]
        public void AnswerToPost19_Has21Results()
        {
            var service = new DataService();
            var answers = service.GetAnswersToQuestions(19); 
            Assert.Equal(21, answers.Count);
            
        }

        [Fact]
        public void GetAllPosts_CheckCount_andFirstName()
        {
            var service = new DataService();
            var questions = service.GetQuestions();
            Assert.Equal(2237, questions.Count);
            Assert.Equal("What is the fastest way to get the value of π?", questions.First().Name);
        }

        [Fact]
        public void GetOnePost_CheckReturn_OnlyOnePost()
        {
            var service = new DataService();
            var questions = service.GetQuestion(19);
            Assert.Equal(19, questions.Id);
            Assert.Equal("What is the fastest way to get the value of π?", questions.Name);
		}

		[Fact]
		public void GetOneComment_CheckReturn_OnlyOneComment() {
			var service = new DataService();
			var comments = service.GetComment(120);
			Assert.Equal(120, comments.Id);
			Assert.Equal(1820, comments.QA_UserId);
		}
		
		[Fact]
		public void GetAllComments_CheckCount_andFirstAuthorid() {
			var service = new DataService();
			var comments = service.GetCommentsByPostId(22106846,0,0);
			Assert.Equal(10, comments.Count);
			Assert.Equal(33534974, comments.First().Id);
		}


		/*
        [Fact]
        public void GetCategory_ValidId_ReturnsCategoryObject()
        {
            var service = new DataService();
            var category = service.GetCategory(1);
            Assert.Equal("Beverages", category.Name);
        }

        [Fact]
        public void CreateCategory_ValidData_CreteCategoryAndRetunsNewObject()
        {
            var service = new DataService();
            var category = service.CreateCategory("Test", "CreateCategory_ValidData_CreteCategoryAndRetunsNewObject");
            Assert.True(category.Id > 0);
            Assert.Equal("Test", category.Name);
            Assert.Equal("CreateCategory_ValidData_CreteCategoryAndRetunsNewObject", category.Description);

            // cleanup
            service.DeleteCategory(category.Id);
        }

        [Fact]
        public void DeleteCategory_ValidId_RemoveTheCategory()
        {
            var service = new DataService();
            var category = service.CreateCategory("Test", "DeleteCategory_ValidId_RemoveTheCategory");
            var result = service.DeleteCategory(category.Id);
            Assert.True(result);
            category = service.GetCategory(category.Id);
            Assert.Null(category);
        }

        [Fact]
        public void DeleteCategory_InvalidId_ReturnsFalse()
        {
            var service = new DataService();
            var result = service.DeleteCategory(-1);
            Assert.False(result);
        }

        [Fact]
        public void UpdateCategory_NewNameAndDescription_UpdateWithNewValues()
        {
            var service = new DataService();
            var category = service.CreateCategory("TestingUpdate", "UpdateCategory_NewNameAndDescription_UpdateWithNewValues");

            var result = service.UpdateCategory(category.Id, "UpdatedName", "UpdatedDescription");
            Assert.True(result);

            category = service.GetCategory(category.Id);

            Assert.Equal("UpdatedName", category.Name);
            Assert.Equal("UpdatedDescription", category.Description);

            // cleanup
            service.DeleteCategory(category.Id);
        }

        [Fact]
        public void UpdateCategory_InvalidID_ReturnsFalse()
        {
            var service = new DataService();
            var result = service.UpdateCategory(-1, "UpdatedName", "UpdatedDescription");
            Assert.False(result);
        }

        // products 

        [Fact]
        public void Product_Object_HasIdNameUnitPriceQuantityPerUnitAndUnitsInStock()
        {
            var product = new Product();
            Assert.Equal(0, product.Id);
            Assert.Null(product.Name);
            Assert.Equal(0.0, product.UnitPrice);
            Assert.Null(product.QuantityPerUnit);
            Assert.Equal(0, product.UnitsInStock);
        }

        [Fact]
        public void GetProduct_ValidId_ReturnsProductWithCategory()
        {
            var service = new DataService();
            var product = service.GetProduct(1);
            Assert.Equal("Chai", product.Name);
            Assert.Equal("Beverages", product.Category.Name);
        }

        [Fact]
        public void GetProduct_NameSubString_ReturnsProductsThatMachesTheSubString()
        {
            var service = new DataService();
            var products = service.GetProductByName("ant");
            Assert.Equal(3, products.Count);
            Assert.Equal("Chef Anton's Cajun Seasoning", products.First().Name);
            Assert.Equal("Guaraná Fantástica", products.Last().Name);
        }

        [Fact]
        public void GetProductsByCategory_ValidId_ReturnsProductWithCategory()
        {
            var service = new DataService();
            var products = service.GetProductByCategory(1);
            Assert.Equal(12, products.Count);
            Assert.Equal("Chai", products.First().Name);
            Assert.Equal("Beverages", products.First().Category.Name);
            Assert.Equal("Lakkalikööri", products.Last().Name);
        }


        // orders 
        [Fact]
        public void Order_Object_HasIdDatesAndOrderDetails()
        {
            var order = new Order();
            Assert.Equal(0, order.Id);
            Assert.Equal(new DateTime(), order.Date);
            Assert.Equal(new DateTime(), order.Required);
            Assert.Null(order.OrderDetails);
            Assert.Null(order.ShipName);
            Assert.Null(order.ShipCity);
        }

        [Fact]
        public void GetOrder_ValidId_ReturnsCompleteOrder()
        {
            var service = new DataService();
            var order = service.GetOrder(10248);
            Assert.Equal(3, order.OrderDetails.Count);
            Assert.Equal("Queso Cabrales", order.OrderDetails.First().Product.Name);
            Assert.Equal("Dairy Products", order.OrderDetails.First().Product.Category.Name);
        }

        [Fact]
        public void GetOrders()
        {
            var service = new DataService();
            var orders = service.GetOrders();
            Assert.Equal(830, orders.Count);
        }

        // orderdetails 
        [Fact]
        public void OrderDetails_Object_HasOrderProductUnitPriceQuantityAndDiscount()
        {
            var orderDetails = new OrderDetails();
            Assert.Equal(0, orderDetails.OrderId);
            Assert.Null(orderDetails.Order);
            Assert.Equal(0, orderDetails.ProductId);
            Assert.Null(orderDetails.Product);
            Assert.Equal(0.0, orderDetails.UnitPrice);
            Assert.Equal(0.0, orderDetails.Quantity);
            Assert.Equal(0.0, orderDetails.Discount);
        }

        [Fact]
        public void GetOrderDetailByOrderId_ValidId_ReturnsProductNameUnitPriceAndQuantity()
        {
            var service = new DataService();
            var orderDetails = service.GetOrderDetailsByOrderId(10248);
            Assert.Equal(3, orderDetails.Count);
            Assert.Equal("Queso Cabrales", orderDetails.First().Product.Name);
            Assert.Equal(14, orderDetails.First().UnitPrice);
            Assert.Equal(12, orderDetails.First().Quantity);
        }

        [Fact]
        public void GetOrderDetailByProductId_ValidId_ReturnsOrderDateUnitPriceAndQuantity()
        {
            var service = new DataService();
            var orderDetails = service.GetOrderDetailsByProductId(11);
            Assert.Equal(38, orderDetails.Count);
            Assert.Equal("1996-07-04", orderDetails.First().Order.Date.ToString("yyyy-MM-dd"));
            Assert.Equal(14, orderDetails.First().UnitPrice);
            Assert.Equal(12, orderDetails.First().Quantity);
        }
*/
	}
}
