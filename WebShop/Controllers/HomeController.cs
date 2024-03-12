using PagedList;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.ModelExtention;
using WebShop.Models;
using System.Data.Entity;
using WebShop.ViewModels;
using Microsoft.AspNet.Identity;


namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ShoppingCart s = ShoppingCart.GetShoppingCart();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.ShoppingCartCount = s.TotalCount();
            return View(db.Products.OrderBy(x => x.Date).Take(6).ToList());
        }

        public ActionResult _Images(int id)
        {
            return PartialView(db.Images.Where(a => a.ProductId == id).ToList());
        }

        public ActionResult _Brand()
        {
            return PartialView(db.Brands.ToList());
        }

        public ActionResult ShopBrand(int id, int? Page)
        {
            ShoppingCart s = ShoppingCart.GetShoppingCart();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.ShoppingCartCount = s.TotalCount();

            var getbrand = db.Products.ToList().OrderByDescending(m => m.Date).Where(a => a.BrandId == id);
            return View(getbrand.ToList().ToPagedList(Page ?? 1, 6));
        }

        public ActionResult _Category()
        {
            return PartialView(db.Categories.ToList());
        }

        public ActionResult _SubCategory()
        {
            return PartialView(db.Categories.ToList());
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Shop(int id, int? Page)
        {
            ShoppingCart s = ShoppingCart.GetShoppingCart();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.ShoppingCartCount = s.TotalCount();

            var getcategory = db.Products.ToList().OrderByDescending(m => m.Date).Where(a => a.SubCategoryId == id);
            return View(getcategory.ToList().ToPagedList(Page ?? 1, 6));


        }

        public ActionResult Shop_Details(int? id)
        {
            ShoppingCart s = ShoppingCart.GetShoppingCart();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.ShoppingCartCount = s.TotalCount();

            var getpost = db.Products.Include(i => i.Rating).FirstOrDefault(i => i.ProductId == id);

            bool UserHasRated = false;
            double totalRating = 0;
            foreach (var rating in getpost.Rating)
            {
                totalRating += rating.ratingValue;
                if (User.Identity.IsAuthenticated)
                {
                    if (rating.User.UserName == User.Identity.Name)
                    {
                        UserHasRated = true;
                    }
                }
            }

            totalRating = (totalRating / getpost.Rating.Count);
            ViewBag.totalRating = totalRating;

            ViewBag.HasRated = UserHasRated;

            return View(getpost);
        }

        public ActionResult AddToCart(int id)
        {
            ShoppingCart s = ShoppingCart.GetShoppingCart();
            s.Update(db, id, 0, 1);

            return RedirectToAction("Shop", new { id = 1 });
        }

        public ActionResult Indkobkurv()
        {
            ShoppingCart s = ShoppingCart.GetShoppingCart();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.ShoppingCartCount = s.TotalCount();
            return View(s);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            ShoppingCart s = ShoppingCart.GetShoppingCart();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.ShoppingCartCount = s.TotalCount();
            return View(new VM_Order_Checkout());
        }

        [HttpPost]
        public ActionResult Checkout(VM_Order_Checkout viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel != null)
                {
                    OrderDetail orderDetails = new OrderDetail();
                    orderDetails.Address = viewModel.Address;
                    orderDetails.City = viewModel.City;
                    orderDetails.Email = viewModel.Email;
                    orderDetails.Name = viewModel.Name;
                    orderDetails.ZipCode = viewModel.ZipCode;

                    db.OrderDetails.Add(orderDetails);
                    db.SaveChanges();
                    return RedirectToAction("callpaypal", "home", new { id = orderDetails.Id });
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public int Basket_Product_Remove(int id, int variant)
        {
            ShoppingCart cart = ShoppingCart.GetShoppingCart();
            cart.Delete(id, variant);

            return 1;
        }

        public int Basket_Product_Update(int id, int amount)
        {
            ShoppingCart cart = ShoppingCart.GetShoppingCart();

            if (amount != 0)
                cart.Update(db, id, 0, amount, false);
            else
                cart.Delete(id, 0);

            return 1;
        }

        public ActionResult Product(int id)
        {
            ShoppingCart cart = ShoppingCart.GetShoppingCart();
            cart.Update(db, id, 0, 1);


            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //Rating
        public ActionResult AddRating(string RatingButton, int productId)
        {
            Product product = db.Products.Find(productId);
            if (product == null)
            {

            }
            
            int ratingValue = Convert.ToInt32(RatingButton);
            product.Rating.Add(new Models.Rating() { ratingValue = ratingValue, UserId = User.Identity.GetUserId()});
            //ViewBag.UserHasRated = db.Ratings.Any(i => i.)

            db.SaveChanges();
            return Redirect("/Home/Shop_Details/" + productId);
        }

        //Search
        public ActionResult Search(string searchText, int? SubcategoryId, int? Page, string description)
        {
            IEnumerable<Product> Products = db.Products.ToList();

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                Products = Products.Where(m => m.Title.ToLower().Contains(searchText) || m.Description.Contains(searchText)).ToList();
                ViewBag.SearchText = searchText;
            }

            if (SubcategoryId != null)
            {
                Products = Products.Where(m => m.SubCategoryId == SubcategoryId);
                ViewBag.SubCategoryName = db.SubCategories.Find(SubcategoryId).Name;
                ViewBag.SubCategoryId = SubcategoryId;
            }
            if (!string.IsNullOrEmpty(description))
            {
                description = description.ToLower();
                Products = Products.Where(m => m.Description.ToLower().Contains(description)).ToList();
                ViewBag.Description = description;
            }
            return View(Products.ToList().OrderByDescending(m => m.Date).ToList().ToPagedList(Page ?? 1, 6));
        }

        //CallPayPal
        public ActionResult CallPayPal(int? id)
        {
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            string payerId = Request.Params["PayerID"];
            if (string.IsNullOrEmpty(payerId))
            {
                // paypal transaction
                ShoppingCart cart = ShoppingCart.GetShoppingCart();
                var total = cart.TotalPrice(true);
                var tax = cart.TotalPrice() * 0.2M;
                var shipping = cart.ShippingFee();
                var subtotal = (total - tax) - shipping;

                // get orderDetail
                OrderDetail orderDetail = db.OrderDetails.Find(id);
                Session["OrderId"] = orderDetail.Id;

                // List of items
                List<Item> items = new List<Item>();

                // foreach orderitem in cart
                foreach (var orderItem in cart.Items)
                {
                    var item = new Item
                    {
                        name = orderItem.Name,
                        currency = "DKK",
                        price = Math.Round((Convert.ToDecimal(orderItem.Price) * 0.8M), 2).ToString().Replace(",", "."),
                        quantity = orderItem.Quantity.ToString(),
                        sku = orderItem.ProductId.ToString()
                    };
                    items.Add(item);
                }

                // create Payment
                var payment = new Payment();
                payment.intent = "sale";
                payment.payer = new Payer { payment_method = "paypal" };
                payment.transactions = new List<Transaction>
                    {
                    new Transaction
                    {
                        description = "Transaction from My Store",
                        invoice_number = new Random().Next(100000, 999999).ToString(),
                        amount = new Amount
                        {
                            currency = "DKK",
                            total = Math.Round(total, 2).ToString().Replace(",", "."),
                            details = new Details
                            {
                                shipping = Math.Round(Convert.ToDecimal(shipping), 2).ToString().Replace(",", "."),
                                subtotal = Math.Round(subtotal, 2).ToString().Replace(",", "."),
                                tax = Math.Round(tax, 2).ToString().Replace(",", ".")
                            }
                        },
                        item_list = new ItemList { items = items }
                    }

                };
                payment.redirect_urls = new RedirectUrls
                {
                    return_url = "http://localhost:58618/Shop/CallPayPal",
                    cancel_url = "http://localhost:58618/Shop/Cancel"

                };

                // call PayPal with Payment
                try
                {
                    // call PayPal REST API
                    var payload = Payment.Create(apiContext, payment);
                    if (payload != null)
                    {
                        orderDetail.TransactionId = payload.id;
                        orderDetail.PaymentLogMessage = payload.state;
                        orderDetail.Status = OrderDetail.OrderStatus.PaymentWindow;
                        // send user to PayPal window
                        return Redirect(payload.GetApprovalUrl());
                    }
                }
                catch (Exception ex)
                {
                    // log error
                    Response.Write(ex);
                    orderDetail.Status = OrderDetail.OrderStatus.Error;
                }
                db.SaveChanges();
            }
            else
            {
                // paypal payload return
                // send order confirm mail
                // log success payment
                return RedirectToAction("reciept");
            }

            return HttpNotFound();
        }

        //Reciept
        public ActionResult Reciept()
        {
            ShoppingCart.GetShoppingCart().Clear();
            OrderDetail order = db.OrderDetails.Find((int)Session["Id"]);
            order.Status = OrderDetail.OrderStatus.Payed;
            db.SaveChanges();
            return View(order);
        }

        // Cancel 
        public ActionResult Cancel()
        {
            OrderDetail order = db.OrderDetails.Find((int)Session["OrderId"]);
            order.Status = OrderDetail.OrderStatus.Cancled;
            db.SaveChanges();
            return View();
        }

        //public ActionResult CallPayPal(int? id)
        //{
        //    APIContext apiContext = Helpers.PayPalConfig.GetApiContext();
        //    if (apiContext != null)
        //    {
        //        Response.Write(apiContext.RequestId);
        //    }
        //    Return HttpNotFound();
        //}

        public ActionResult Error()
        {
            return View();

        }

        public ActionResult _Users()
        {
            return PartialView(db.Users.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}