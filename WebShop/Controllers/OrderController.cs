//using PayPal.Api;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using WebShop.ModelExtention;
//using WebShop.Models;
//using WebShop.ViewModels;

//namespace WebShop.Controllers
//{
//    public class OrderController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        [Route("shop/kurv")]
//        public ActionResult Basket()
//        {
//            ShoppingCart cart = ShoppingCart.GetShoppingCart();
//            if (cart.Items != null && cart.Items.Count > 0)
//                return View(cart);

//            return RedirectToAction("index", "home");
//        }



     

//        // shop/callpaypal

//        // shop/cancel

//        // Leverings oplysninger

//        //[Route("shop/reciept")]
//        // Success Url


//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}