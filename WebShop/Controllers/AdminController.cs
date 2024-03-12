using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    [Authorize(Roles = "User, Administrator")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Index

        public ActionResult Admin()
        {
            return View(db.Todolists.ToList());
        }

        #endregion

        #region Category

        [Route("admin/Category_Index")]
        public ActionResult Category_Index()
        {
            return View("Category_Index", db.Categories.Select(i => new VM_Category_List { CategoryId = i.CategoryId, Title = i.Title }).ToList());
        }

        [Route("admin/Category_Create")]
        public ActionResult Category_Create()
        {
            ViewBag.SubCategoryId = new SelectList(db.Categories, "CategoryId", "Title");
            return View();
        }

        [Route("admin/Category_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Category_Create(VM_Category_Create CategoryType)
        {
            if (ModelState.IsValid)
            {
                Category model = new Category();
                model.Title = CategoryType.Title;
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Category_Index");
            }

            return View(CategoryType);
        }

        [Route("admin/Category_Edit")]
        public ActionResult Category_Edit(int? id)
        {
            if (id.HasValue)
            {
                Category model = db.Categories.Find(id);
                if (model != null)
                {
                    VM_Category_Edit viewModel = new VM_Category_Edit();
                    viewModel.CategoryId = model.CategoryId;
                    viewModel.Title = model.Title;
                    ViewBag.SubCategoryId = new SelectList(db.Categories, "CategoryId", "Title");
                    return View(viewModel);
                }

            }

            return RedirectToAction("Index");
        }

        [Route("admin/Category_Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Category_Edit(VM_Category_Edit viewModel)
        {
            if (ModelState.IsValid)
            {
                Category model = db.Categories.Find(viewModel.CategoryId);

                if (model != null)
                {
                    model.Title = viewModel.Title;
                    db.SaveChanges();
                    return RedirectToAction("Category_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vil du dø");
                }
            }
            ViewBag.SubCategoryId = new SelectList(db.Categories, "CategoryId", "Title");
            return View(viewModel);
        }

        [Route("admin/Category_Delete")]
        public ActionResult Category_Delete(int id)
        {
            Category CategoryType = db.Categories.Find(id);
            db.Categories.Remove(CategoryType);
            db.SaveChanges();

            return RedirectToAction("Category_Index");

        }
        #endregion

        #region SubCategory

        [Route("admin/SubCategory_Index")]
        public ActionResult SubCategory_Index()
        {
            return View("SubCategory_Index", db.SubCategories.Select(i => new VM_SubCategory_List { Name = i.Name, SubCategoryId = i.SubCategoryId, Category = i.Category, CategoryId = i.CategoryId, Description = i.Description }).ToList());
        }

        [Route("admin/SubCategory_Create")]
        public ActionResult SubCategory_Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title");
            return View();
        }

        [Route("admin/SubCategory_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubCategory_Create(VM_SubCategory_Create SubCategoryType)
        {
            if (ModelState.IsValid)
            {
                SubCategory model = new SubCategory();
                model.Name = SubCategoryType.Name;
                model.Description = SubCategoryType.Description;
                model.CategoryId = SubCategoryType.CategoryId;
                model.Category = SubCategoryType.Category;
                db.SubCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("SubCategory_Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", SubCategoryType.CategoryId);
            return View(SubCategoryType);
        }

        [Route("admin/SubCategory_Edit")]
        public ActionResult SubCategory_Edit(int? id)
        {
            if (id.HasValue)
            {
                SubCategory model = db.SubCategories.Find(id);
                if (model != null)
                {
                    VM_SubCategory_Edit viewModel = new VM_SubCategory_Edit();
                    viewModel.SubCategoryId = model.SubCategoryId;
                    viewModel.Name = model.Name;
                    viewModel.Description = model.Description;
                    viewModel.Category = model.Category;
                    viewModel.CategoryId = model.CategoryId;
                    ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", viewModel.CategoryId);
                    return View(viewModel);
                }

            }

            return RedirectToAction("Index");
        }

        [Route("admin/SubCategory_Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubCategory_Edit(VM_SubCategory_Edit viewModel)
        {
            if (ModelState.IsValid)
            {
                SubCategory model = db.SubCategories.Find(viewModel.SubCategoryId);

                if (model != null)
                {
                    model.Name = viewModel.Name;
                    model.Description = viewModel.Description;
                    model.Category = viewModel.Category;
                    model.CategoryId = viewModel.CategoryId;
                    db.SaveChanges();
                    return RedirectToAction("SubCategory_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vil du dø");
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Title", viewModel.CategoryId);
            return View(viewModel);
        }

        [Route("admin/SubCategory_Delete")]
        public ActionResult SubCategory_Delete(int id)
        {
            SubCategory SubCategoryType = db.SubCategories.Find(id);
            db.SubCategories.Remove(SubCategoryType);
            db.SaveChanges();

            return RedirectToAction("Category_Index");

        }

        #endregion

        #region Brand

        [Route("admin/Brand_Index")]
        public ActionResult Brand_Index()
        {
            return View("Brand_Index", db.Brands.Select(i => new VM_Brand_List { BrandId = i.BrandId, Name = i.Name }).ToList());
        }

        [Route("admin/Brand_Create")]
        public ActionResult Brand_Create()
        {
            return View();
        }

        [Route("admin/Brand_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brand_Create(VM_Brand_Create BrandType)
        {
            if (ModelState.IsValid)
            {
                Brand model = new Brand();
                model.Name = BrandType.Name;
                db.Brands.Add(model);
                db.SaveChanges();
                return RedirectToAction("Brand_Index");
            }

            return View(BrandType);
        }

        [Route("admin/Brand_Edit")]
        public ActionResult Brand_Edit(int? id)
        {
            if (id.HasValue)
            {
                Brand model = db.Brands.Find(id);
                if (model != null)
                {
                    VM_Brand_Edit viewModel = new VM_Brand_Edit();
                    viewModel.BrandId = model.BrandId;
                    viewModel.Name = model.Name;
                    return View(viewModel);
                }

            }

            return RedirectToAction("Index");
        }

        [Route("admin/Brand_Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brand_Edit(VM_Brand_Edit viewModel)
        {
            if (ModelState.IsValid)
            {
                Brand model = db.Brands.Find(viewModel.BrandId);

                if (model != null)
                {
                    model.Name = viewModel.Name;
                    db.SaveChanges();
                    return RedirectToAction("Brand_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vil du dø");
                }
            }
            return View(viewModel);
        }

        [Route("admin/Brand_Delete")]
        public ActionResult Brand_Delete(int id)
        {
            Brand BrandType = db.Brands.Find(id);
            db.Brands.Remove(BrandType);
            db.SaveChanges();

            return RedirectToAction("Brand_Index");

        }

        #endregion

        #region Image

        [Route("admin/Image_Index")]
        public ActionResult Image_Index()
        {
            return View("Image_Index", db.Images.Select(i => new VM_Image_List { ImageId = i.ImageId, Images = i.Images, ProductId = i.ProductId, Products = i.Products }).ToList());
        }

        [Route("admin/Image_Create")]
        public ActionResult Image_Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title");
            return View();
        }

        [Route("admin/Image_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Image_Create(VM_Image_Create ImageType, HttpPostedFileBase Images)
        {
            if (ModelState.IsValid)
            {
                Image model = new Image();
                model.ProductId = ImageType.ProductId;
                model.Products = ImageType.Products;
                if (Images != null)
                {
                    string newFileName = Guid.NewGuid().ToString();
                    string FileExtension = Path.GetExtension(Images.FileName);
                    if (FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".png")
                    {
                        string physicalpath = Server.MapPath("~/content/Images/") + newFileName + FileExtension;
                        Images.SaveAs(physicalpath);
                        model.Images = newFileName + FileExtension;
                        db.Images.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Image_Index");
                    }

                }
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title", ImageType.ProductId);
            return View(ImageType);
        }

        [Route("admin/Image_Edit")]
        public ActionResult Image_Edit(int? id)
        {
            if (id.HasValue)
            {
                Image model = db.Images.Find(id);
                if (model != null)
                {
                    VM_Image_Edit viewModel = new VM_Image_Edit();
                    viewModel.ImageId = model.ImageId;
                    viewModel.Images = model.Images;
                    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title", viewModel.ProductId);
                    return View(viewModel);
                }

            }

            return RedirectToAction("Index");
        }

        [Route("admin/Image_Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Image_Edit(VM_Image_Edit viewModel, HttpPostedFileBase Images)
        {
            if (ModelState.IsValid)
            {
                Image model = db.Images.Find(viewModel.ImageId);
                model.ProductId = viewModel.ProductId;
                model.Products = viewModel.Products;
                model.Images = viewModel.Images;
                model.ImageId = viewModel.ImageId;
                if (model != null)
                {
                    if (Images != null)
                    {
                        string newFileName = Guid.NewGuid().ToString();
                        string FileExtension = Path.GetExtension(Images.FileName);
                        if (FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".png")
                        {
                            string physicalpath = Server.MapPath("~/content/Images/") + newFileName + FileExtension;
                            Images.SaveAs(physicalpath);
                            model.Images = newFileName + FileExtension;
                        }
                    }
                    return RedirectToAction("Image_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vil du dø");
                }

                db.SaveChanges();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title", viewModel.ProductId);
            return View(viewModel);
        }

        [Route("admin/Image_Delete")]
        public ActionResult Image_Delete(int id)
        {
            Image ImageType = db.Images.Find(id);
            db.Images.Remove(ImageType);
            db.SaveChanges();

            return RedirectToAction("Image_Index");

        }

        #endregion

        #region Size

        [Route("admin/Size_Index")]
        public ActionResult Size_Index()
        {
            return View("Size_Index", db.Sizes.Select(i => new VM_Size_List { Name = i.Name, SizeId = i.SizeId }).ToList());
        }

        [Route("admin/Size_Create")]
        public ActionResult Size_Create()
        {
            return View();
        }

        [Route("admin/Size_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Size_Create(VM_Size_Create SizeType)
        {
            if (ModelState.IsValid)
            {
                Size model = new Size();
                model.Name = SizeType.Name;
                db.Sizes.Add(model);
                db.SaveChanges();
                return RedirectToAction("Size_Index");
            }

            return View(SizeType);
        }

        [Route("admin/Size_Edit")]
        public ActionResult Size_Edit(int? id)
        {
            if (id.HasValue)
            {
                Size model = db.Sizes.Find(id);
                if (model != null)
                {
                    VM_Size_Edit viewModel = new VM_Size_Edit();
                    viewModel.SizeId = model.SizeId;
                    viewModel.Name = model.Name;
                    return View(viewModel);
                }

            }

            return RedirectToAction("Index");
        }

        [Route("admin/Size_Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Size_Edit(VM_Size_Edit viewModel)
        {
            if (ModelState.IsValid)
            {
                Size model = db.Sizes.Find(viewModel.SizeId);

                if (model != null)
                {
                    model.Name = viewModel.Name;
                    db.SaveChanges();
                    return RedirectToAction("Size_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vil du dø");
                }
            }
            return View(viewModel);
        }

        [Route("admin/Size_Delete")]
        public ActionResult Size_Delete(int id)
        {
            Size SizeType = db.Sizes.Find(id);
            db.Sizes.Remove(SizeType);
            db.SaveChanges();

            return RedirectToAction("Size_Index");

        }

        #endregion

        #region ProductSizes

        [Route("admin/ProductSizes_Index")]
        public ActionResult ProductSizes_Index()
        {
            return View("ProductSizes_Index", db.Sizes.Select(i => new VM_ProductSizes_List { }).ToList());
        }

        [Route("admin/ProductSizes_Create")]
        public ActionResult ProductSizes_Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeId");
            return View();
        }

        [Route("admin/ProductSizes_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductSizes_Create(VM_ProductSizes_Create ProductSizesType)
        {
            if (ModelState.IsValid)
            {
                ProductSizes model = new ProductSizes();

                ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title", model.ProductId);
                ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeId", model.SizeId);

                db.ProductSizesess.Add(model);
                db.SaveChanges();
                return RedirectToAction("ProductSizes_Index");
            }

            return View(ProductSizesType);
        }

        [Route("admin/ProductSizes_Edit")]
        public ActionResult ProductSizes_Edit(int? id)
        {
            if (id.HasValue)
            {
                Size model = db.Sizes.Find(id);
                if (model != null)
                {
                    VM_ProductSizes_Edit viewModel = new VM_ProductSizes_Edit();
                    return View(viewModel);
                }

            }

            return RedirectToAction("Index");
        }

        [Route("admin/ProductSizes_Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductSizes_Edit(VM_ProductSizes_Edit viewModel)
        {
            if (ModelState.IsValid)
            {
                ProductSizes model = db.ProductSizesess.Find();

                if (model != null)
                {
                    db.SaveChanges();
                    return RedirectToAction("ProductSizes_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Vil du dø");
                }
            }
            return View(viewModel);
        }

        [Route("admin/ProductSizes_Delete")]
        public ActionResult ProductSizes_Delete(int id)
        {
            ProductSizes ProductSizesType = db.ProductSizesess.Find(id);
            db.ProductSizesess.Remove(ProductSizesType);
            db.SaveChanges();

            return RedirectToAction("ProductSizes_Index");

        }


        #endregion

        #region Product

        [Route("admin/Product_Index")]
        public ActionResult Product_Index()
        {
            return View("Product_Index", db.Products.Select(i => new VM_Product_List { ProductId = i.ProductId, Title = i.Title, Description = i.Description, Date = i.Date, Image = i.Image, Price = i.Price, Quantity = i.Quantity, BrandId = i.BrandId, Brands = i.Brands, SubCategory = i.SubCategory, SubCategoryId = i.SubCategoryId }).ToList());
        }

        [Route("admin/Product_Create")]
        public ActionResult Product_Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            //ViewBag.SubCategoryId = db.SubCategories.ToList().Select(x => new SelectListItem() { Value = x.SubCategoryId.ToString(), Text = string.Format("{0} : {1}", x.Name, x.Category.Title) });
            return View();
        }

        [Route("admin/Product_Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product_Create(VM_Product_Create ProductType, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                Product model = new Product();
                model.Title = ProductType.Title;
                model.Quantity = ProductType.Quantity;
                model.Price = ProductType.Price;
                model.Date = DateTime.Now;
                model.Description = ProductType.Description;
                model.Brands = ProductType.Brands;
                model.BrandId = ProductType.BrandId;
                model.SubCategoryId = ProductType.SubCategoryId;
                model.SubCategory = ProductType.SubCategory;
                if (Image != null)
                {
                    string newFileName = Guid.NewGuid().ToString();
                    string FileExtension = System.IO.Path.GetExtension(Image.FileName);
                    if (FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".png")
                    {
                        string physicalpath = Server.MapPath("~/content/Images/") + newFileName + FileExtension;
                        Image.SaveAs(physicalpath);
                        model.Image = newFileName + FileExtension;
                        db.Products.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Product_Index");
                    }
                }
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", ProductType.BrandId);
            ViewBag.SubCategoryId = db.SubCategories.ToList().Select(x => new SelectListItem() { Value = x.SubCategoryId.ToString(), Text = string.Format("{0} : {1}", x.Name, x.Category.Title) });
            return View(ProductType);
        }

        [Route("admin/Product_Edit")]
        public ActionResult Product_Edit(int? id)
        {
            if (id.HasValue)
            {
                Product model = db.Products.Find(id);
                if (model != null)
                {
                    VM_Product_Edit viewModel = new VM_Product_Edit();
                    viewModel.ProductId = model.ProductId;
                    viewModel.Title = model.Title;
                    viewModel.Date = DateTime.Now;
                    viewModel.Image = model.Image;
                    viewModel.Description = model.Description;
                    viewModel.Quantity = model.Quantity;
                    viewModel.Price = model.Price;
                    ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", model.BrandId);
                    ViewBag.SubCategoryId = db.SubCategories.ToList().Select(x => new SelectListItem() { Value = x.SubCategoryId.ToString(), Text = string.Format("{0} : {1}", x.Name, x.Category.Title) });
                    return View(viewModel);
                }
            }
            return RedirectToAction("Index");
        }

        [Route("admin/Product_Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product_Edit(VM_Product_Edit viewModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Product model = db.Products.Find(viewModel.ProductId);
                model.Title = viewModel.Title;
                model.Date = DateTime.Now;
                model.Description = viewModel.Description;
                model.Quantity = viewModel.Quantity;
                model.Price = viewModel.Price;
                model.Brands = viewModel.Brands;
                model.BrandId = viewModel.BrandId;
                model.Image = viewModel.Image;
                model.Active = true;
                model.ProductId = viewModel.ProductId;
                model.SubCategory = viewModel.SubCategory;
                model.SubCategoryId = viewModel.SubCategoryId;
                //model.Categories = viewModel.Categories;
                //model.CategoryId = viewModel.CategoryId;
                if (model != null)
                {
                    if (image != null)
                    {
                        string newFileName = Guid.NewGuid().ToString();
                        string FileExtension = Path.GetExtension(image.FileName);
                        if (FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".png")
                        {
                            string physicalpath = Server.MapPath("~/content/Images/") + newFileName + FileExtension;
                            image.SaveAs(physicalpath);
                            model.Image = newFileName + FileExtension;

                            return RedirectToAction("Product_Index");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Vil du dø");
                }
                db.SaveChanges();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", viewModel.BrandId);
            ViewBag.SubCategoryId = db.SubCategories.ToList().Select(x => new SelectListItem() { Value = x.SubCategoryId.ToString(), Text = string.Format("{0} : {1}", x.Name, x.Category.Title) });
            return View(viewModel);
        }

        [Route("admin/Product_Delete")]
        public ActionResult Product_Delete(int id)
        {
            Product ProductType = db.Products.Find(id);
            db.Products.Remove(ProductType);
            db.SaveChanges();

            return RedirectToAction("Product_Index");

        }

        #endregion

        #region ToDoList

        public ActionResult ToDo_Create()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<ActionResult> ToDo_Create([Bind(Include = "TodolistId,Description,IsDone")] Todolist todo)
        {
            if (ModelState.IsValid)
            {
                db.Todolists.Add(todo);
                await db.SaveChangesAsync();
                return RedirectToAction("Admin");
            }

            return View(todo);
        }

        public ActionResult ToDo_Delete(int id)
        {
            Todolist TodolistType = db.Todolists.Find(id);
            db.Todolists.Remove(TodolistType);
            db.SaveChanges();

            return RedirectToAction("Admin");

        }

        public ActionResult ToDoEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todolist todo = db.Todolists.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return PartialView(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToDoEdit([Bind(Include = "TodolistId")] Todolist todo)
        {
            if (ModelState.IsValid)
            {
                Todolist model = db.Todolists.Find(todo.TodolistId);
                model.IsDone = true;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(todo);
        }
        #endregion

        //driekte fra controller
        public ActionResult Index()
        {
            var productSizesess = db.ProductSizesess.Include(p => p.Product).Include(p => p.Size);
            return View(productSizesess.ToList());
        }

        // GET: ProductSizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSizes productSizes = db.ProductSizesess.Find(id);
            if (productSizes == null)
            {
                return HttpNotFound();
            }
            return View(productSizes);
        }

        // GET: ProductSizes/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title");
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeId");
            return View();
        }

        // POST: ProductSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,SizeId")] ProductSizes productSizes)
        {
            if (ModelState.IsValid)
            {
                db.ProductSizesess.Add(productSizes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title", productSizes.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeId", productSizes.SizeId);
            return View(productSizes);
        }

        // GET: ProductSizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSizes productSizes = db.ProductSizesess.Find(id);
            if (productSizes == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title", productSizes.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeId", productSizes.SizeId);
            return View(productSizes);
        }

        // POST: ProductSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,SizeId")] ProductSizes productSizes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSizes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title", productSizes.ProductId);
            ViewBag.SizeId = new SelectList(db.Sizes, "SizeId", "SizeId", productSizes.SizeId);
            return View(productSizes);
        }

        // GET: ProductSizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSizes productSizes = db.ProductSizesess.Find(id);
            if (productSizes == null)
            {
                return HttpNotFound();
            }
            return View(productSizes);
        }

        // POST: ProductSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSizes productSizes = db.ProductSizesess.Find(id);
            db.ProductSizesess.Remove(productSizes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //end
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