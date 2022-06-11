using Vanharmelen_FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books_FinalProject.Controllers
{
    public class InvoiceLineItemsController : Controller
    {
        // GET: InvoiceLineItems
        public ActionResult All(string id, int sortBy = 0, bool isDesc = false)
        {
            BooksEntities context = new BooksEntities();

            List<InvoiceLineItem> invoiceLineItems;
            switch (sortBy)
            {
                case 1:
                    {
                        if (isDesc)
                            invoiceLineItems = context.InvoiceLineItems.OrderByDescending(i => i.ProductCode).ToList();
                        else
                            invoiceLineItems = context.InvoiceLineItems.OrderBy(i => i.ProductCode).ToList();
                        break;

                    }
                case 2:
                    {
                        if (isDesc)
                            invoiceLineItems = context.InvoiceLineItems.OrderByDescending(i => i.UnitPrice).ToList();
                        else
                            invoiceLineItems = context.InvoiceLineItems.OrderBy(i => i.UnitPrice).ToList();
                        break;
                    }
                case 3:
                    {
                        if (isDesc)
                            invoiceLineItems = context.InvoiceLineItems.OrderByDescending(i => i.Quantity).ToList();
                        else
                            invoiceLineItems = context.InvoiceLineItems.OrderBy(i => i.Quantity).ToList();
                        break;
                    }
                case 4:
                    {
                        if (isDesc)
                            invoiceLineItems = context.InvoiceLineItems.OrderByDescending(i => i.ItemTotal).ToList();
                        else
                            invoiceLineItems = context.InvoiceLineItems.OrderBy(i => i.ItemTotal).ToList();
                        break;
                    }
                default:
                    {
                        if (isDesc)
                            invoiceLineItems = context.InvoiceLineItems.OrderByDescending(i => i.InvoiceID).ToList();
                        else
                            invoiceLineItems = context.InvoiceLineItems.OrderBy(i => i.InvoiceID).ToList();
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
                    invoiceLineItems = invoiceLineItems.Where(i =>
                        i.InvoiceID == lookUp ||
                        i.UnitPrice == lookUp||
                        i.Quantity == lookUp ||
                        i.ItemTotal == lookUp
                            ).ToList();
                }
                else
                {
                    invoiceLineItems = invoiceLineItems.Where(i =>
                        i.ProductCode.Contains(id) 
                            ).ToList();
                }
            }

            return View(invoiceLineItems);
        }

        [HttpGet]

        public ActionResult Upsert(int id = 0)
        {
            BooksEntities context = new BooksEntities();
            InvoiceLineItem invoiceLineItem = context.InvoiceLineItems.Where(i => i.InvoiceID == id).FirstOrDefault();

            List<Invoice> invoices = context.Invoices.ToList();
            List<Product> products = context.Products.ToList();

            UpsertInvoiceLineItems viewModel = new UpsertInvoiceLineItems()
            {
                InvoiceLineItem = invoiceLineItem,
                Invoices = invoices,
                Products = products,
            };
            return View(viewModel);
        }


        [HttpPost]

        public ActionResult Upsert(UpsertInvoiceLineItems model)
        {
            InvoiceLineItem newInvoiceLineItem = model.InvoiceLineItem;

            BooksEntities context = new BooksEntities();

            try
            {
                if (context.InvoiceLineItems.Where(i => i.InvoiceID == newInvoiceLineItem.InvoiceID).Count() > 0)
                {
                    var invoiceLineItemToSave = context.InvoiceLineItems.Where(i =>
                                        i.InvoiceID == newInvoiceLineItem.InvoiceID
                                        ).FirstOrDefault();

                    invoiceLineItemToSave.ProductCode = newInvoiceLineItem.ProductCode;
                    invoiceLineItemToSave.UnitPrice = newInvoiceLineItem.UnitPrice;
                    invoiceLineItemToSave.Quantity = newInvoiceLineItem.Quantity;
                    invoiceLineItemToSave.ItemTotal = newInvoiceLineItem.ItemTotal;
                }
                else
                {
                    context.InvoiceLineItems.Add(newInvoiceLineItem);
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