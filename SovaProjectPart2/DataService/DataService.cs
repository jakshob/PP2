using System;
using System.Collections.Generic;
using System.Linq;


namespace WebServer
{
	public class DataService {


		public List<Post> GetPosts() {
			using (var db = new SovaContext()) {
				/* var postList = new List<Post>();
                 foreach (Post p in db.Posts) {
                     if (string.IsNullOrEmpty(p.Name)){
                         p.Name = " empty ";
                     }
                     postList.Add(p);
                 }
                 */
				return db.Posts.ToList();
			}
		}

		public List<Post> GetQuestions() {
			using (var db = new SovaContext()) {

				var postList = new List<Post>();
				foreach (Post p in db.Posts) {
					if (p.Posttype == 1) {
						postList.Add(p);
					}
				}
				return postList;

			}
		}

		public List<Post> GetAnswersToQuestions(int inputId) {
			using (var db = new SovaContext()) {

				var postList = new List<Post>();
				foreach (Post p in db.Posts) {
					if (p.ParentId == inputId) {
						postList.Add(p);
					}
				}
				return postList;

			}
		}



		public Post GetPost(int inputCatId) {

			using (var db = new SovaContext()) {
				return db.Posts.Find(inputCatId);
			}
		}

		public List<Comment> GetComments(int inputId, int page, int pagesize) {
			using (var db = new SovaContext()) {
				var commentList = new List<Comment>;
				foreach (Comment c in db.Comments) {
					if(c.PostId == inputId) {
						commentList.Add(c);
					}
				}
				return commentList;
			}
		}
		public Comment GetComment(int id) {
			using (var db = new SovaContext()) {
				return db.Comments.Find(id);
			}
		}
        /*
        public Category CreateCategory(string categoryName, string catDescription)
        {

            using (var db = new NorthwindContext())
            {
                /*
                int newId = Convert.ToInt32(from cat in db.Categories
                             orderby cat.Id descending
                             select cat.Id);
                
                */
                /*

                Category newCat;
                db.Categories.Add(newCat = new Category()
                {

                    //ÆNDRE! Det må ikke være hardcoded
                    Id = db.Categories.Max(x => x.Id) + 1,
                    Name = categoryName,
                    Description = catDescription

                });
                db.SaveChanges();
                return newCat;
            }

        }

        public bool DeleteCategory(int inputCatId)
        {

            using (var db = new NorthwindContext())
            {

                var record = db.Categories.FirstOrDefault(r => r.Id == inputCatId);
                if (record != null)
                {

                    var cat = db.Categories.First(c => c.Id == inputCatId);
                    db.Categories.Remove(cat);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public bool UpdateCategory(int inputCatId, string updateName, string updateDesc)
        {

            using (var db = new NorthwindContext())
            {

                var record = db.Categories.FirstOrDefault(r => r.Id == inputCatId);
                if (record != null)
                {
                    var chosenCategory = from cat in db.Categories
                                         where cat.Id == inputCatId
                                         select cat;

                    foreach (Category cat in chosenCategory)
                    {
                        cat.Name = updateName;
                        cat.Description = updateDesc;
                    }
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Product GetProduct(int inputProductId)
        {

            using (var db = new NorthwindContext())
            {
                Product tempProduct = db.Products.Find(inputProductId);
                tempProduct.Category = db.Categories.Find(tempProduct.CategoryId);
                return tempProduct;
            }

        }

        public List<Product> GetProductByName(string searchQueryString)
        {
            using (var db = new NorthwindContext())
            {
                var productList = new List<Product>();
                foreach (Product p in db.Products)
                {
                    if (p.Name.ToLower().Contains(searchQueryString))
                    {
                        productList.Add(p);
                    }
                }
                return productList;
            }
        }

        public List<Product> GetProductByCategory(int inputCategoryId)
        {

            using (var db = new NorthwindContext())
            {

                //OrderBy fordi databasen var lidt fucked med underlige tegn og rækkefølger..
                var listByCategory = db.Products.OrderBy(x => x.Id)
                    .Where(p => p.CategoryId == inputCategoryId);


                foreach (Product prod in listByCategory)
                {

                    prod.Category = db.Categories.Find(prod.CategoryId);
                }

                var outputListByCategory = listByCategory.ToList();


                return outputListByCategory;
            }

        }

        public Order GetOrder(int inputOrderId)
        {

            using (var db = new NorthwindContext())
            {
                Order tempOrder = db.Orders.Find(inputOrderId);
                tempOrder.OrderDetails = GetOrderDetailsByOrderId(inputOrderId);
                return tempOrder;
            }
        }

        public List<OrderDetails> GetOrderDetailsByOrderId(int inputOrderId)
        {
            using (var db = new NorthwindContext())
            {
                var tempList = db.OrderDetailsTable.Where(x => x.OrderId == inputOrderId).ToList();
                foreach (OrderDetails ordet in tempList)
                {
                    ordet.Product = db.Products.Find(ordet.ProductId);
                    ordet.Order = db.Orders.Find(ordet.OrderId);
                    ordet.Product.Category = db.Categories.Find(ordet.Product.CategoryId);
                }
                return tempList;
            }
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int inputProductId)
        {
            using (var db = new NorthwindContext())
            {
                var tempList = db.OrderDetailsTable.Where(x => x.ProductId == inputProductId).OrderBy(x => x.OrderId).ToList();
                foreach (OrderDetails ordet in tempList)
                {
                    ordet.Product = db.Products.Find(ordet.ProductId);
                    ordet.Order = db.Orders.Find(ordet.OrderId);
                    ordet.Product.Category = db.Categories.Find(ordet.Product.CategoryId);
                }
                return tempList;
            }
        }

        public List<Order> GetOrders()
        {
            using (var db = new NorthwindContext())
            {
                var tempList = new List<Order>();
                foreach (Order ord in db.Orders)
                {
                    var tempOrd = GetOrder(ord.Id);
                    tempList.Add(tempOrd);
                }
                return tempList;
            }
        }
        */
    }
}
