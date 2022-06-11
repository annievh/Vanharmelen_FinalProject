using Vanharmelen_FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books_FinalProject.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult All(string id, int sortBy = 0, bool isDesc = false)
        {
            BooksEntities context = new BooksEntities();
            List<Product> products;
            switch (sortBy)
            {
                case 1:
                    {
                        if (isDesc)
                            products = context.Products.OrderByDescending(p => p.Description).ToList();
                        else
                            products = context.Products.OrderBy(p => p.Description).ToList();
                        break;

                    }
                case 2:
                    {
                        if (isDesc)
                            products = context.Products.OrderByDescending(p => p.UnitPrice).ToList();
                        else
                            products = context.Products.OrderBy(p => p.UnitPrice).ToList();
                        break;
                    }
                case 3:
                    {
                        if (isDesc)
                            products = context.Products.OrderByDescending(p => p.OnHandQuantity).ToList();
                        else
                            products = context.Products.OrderBy(p => p.OnHandQuantity).ToList();
                        break;
                    }
                default:
                    {
                        if (isDesc)
                            products = context.Products.OrderByDescending(p => p.ProductCode).ToList();
                        else
                            products = context.Products.OrderBy(p => p.ProductCode).ToList();
                        break;
                    }
            }
            if (!string.IsNullOrWhiteSpace(id))
            {

                id = id.Trim().ToLower();
                int lookUp = 0;
                // if id is an int
                if (int.TryParse(id, out lookUp))
                {
                    products = products.Where(p =>
                        p.UnitPrice == lookUp||
                        p.OnHandQuantity == lookUp
                            ).ToList();
                }
                else
                {
                    products = products.Where(p =>
                        p.ProductCode.Contains(id) ||
                        p.Description.Contains(id)
                            ).ToList();
                }
            }
            return View(products);
        }
        [HttpGet]

        public ActionResult Upsert(string id)
        {
            BooksEntities context = new BooksEntities();
            Product product = context.Products.Where(p => p.ProductCode == id).FirstOrDefault();

            return View(product);
        }


        [HttpPost]

        public ActionResult Upsert(Product product)
        {

            BooksEntities context = new BooksEntities();
            try
            {
                if (context.Products.Where(p => p.ProductCode == product.ProductCode).Count() > 0)
                {
                    var productToSave = context.Products.Where(p =>
                                        p.ProductCode == product.ProductCode
                                        ).ToList()[0];

                    productToSave.Description = product.Description;
                    productToSave.UnitPrice = product.UnitPrice;
                    productToSave.OnHandQuantity = product.OnHandQuantity;
                }
                else
                {
                    context.Products.Add(product);
                }
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("All");
        }
    }
}