using Vanharmelen_FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books_FinalProject.Controllers
{
    public class OrderOptionsController : Controller
    {
        // GET: OrderOptions
        public ActionResult All(string id, int sortBy = 0, bool isDesc = false)
        {
            BooksEntities context = new BooksEntities();
            List<OrderOption> orderOptions;
            switch (sortBy)
            {
                case 1:
                    {
                        if (isDesc)
                            orderOptions = context.OrderOptions.OrderByDescending(o => o.FirstBookShipCharge).ToList();
                        else
                            orderOptions = context.OrderOptions.OrderBy(o => o.FirstBookShipCharge).ToList();
                        break;

                    }
                case 2:
                    {
                        if (isDesc)
                            orderOptions = context.OrderOptions.OrderByDescending(o => o.AdditionalBookShipCharge).ToList();
                        else
                            orderOptions = context.OrderOptions.OrderBy(o => o.AdditionalBookShipCharge).ToList();
                        break;
                    }
                default:
                    {
                        if (isDesc)
                            orderOptions = context.OrderOptions.OrderByDescending(o => o.SalesTaxRate).ToList();
                        else
                            orderOptions = context.OrderOptions.OrderBy(o => o.SalesTaxRate).ToList();
                        break;
                    }
            }
            if (!string.IsNullOrWhiteSpace(id))
            {

                int lookUp = 0;
                // if id is an int
                if (int.TryParse(id, out lookUp))
                {
                    orderOptions = orderOptions.Where(o =>
                        o.SalesTaxRate == lookUp ||
                        o.FirstBookShipCharge == lookUp||
                        o.AdditionalBookShipCharge == lookUp
                            ).ToList();
                }
            }
            return View(orderOptions);
        }
        [HttpGet]

        public ActionResult Upsert(int id)
        {
            BooksEntities context = new BooksEntities();
            OrderOption orderOption = context.OrderOptions.Where(o => o.SalesTaxRate == id).FirstOrDefault();

            return View(orderOption);
        }


        [HttpPost]

        public ActionResult Upsert(OrderOption orderOption)
        {

            BooksEntities context = new BooksEntities();
            try
            {
                if (context.OrderOptions.Where(o => o.SalesTaxRate == orderOption.SalesTaxRate).Count() > 0)
                {
                    var orderOptionToSave = context.OrderOptions.Where(o =>
                                        o.SalesTaxRate == orderOption.SalesTaxRate
                                        ).ToList()[0];

                    orderOptionToSave.FirstBookShipCharge = orderOption.FirstBookShipCharge;
                    orderOptionToSave.AdditionalBookShipCharge = orderOption.AdditionalBookShipCharge;
                }
                else
                {
                    context.OrderOptions.Add(orderOption);
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