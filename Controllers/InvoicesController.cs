using Vanharmelen_FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books_FinalProject.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public ActionResult All(string id, int sortBy = 0, bool isDesc = false)
        {
            BooksEntities context = new BooksEntities();

            List<Invoice> invoices;
            switch (sortBy)
            {
                case 1:
                    {
                        if (isDesc)
                            invoices = context.Invoices.OrderByDescending(i => i.CustomerID).ToList();
                        else
                            invoices = context.Invoices.OrderBy(i => i.CustomerID).ToList();
                        break;

                    }
                case 2:
                    {
                        if (isDesc)
                            invoices = context.Invoices.OrderByDescending(i => i.InvoiceDate).ToList();
                        else
                            invoices = context.Invoices.OrderBy(i => i.InvoiceDate).ToList();
                        break;
                    }
                case 3:
                    {
                        if (isDesc)
                            invoices = context.Invoices.OrderByDescending(i => i.ProductTotal).ToList();
                        else
                            invoices = context.Invoices.OrderBy(i => i.ProductTotal).ToList();
                        break;
                    }
                case 4:
                    {
                        if (isDesc)
                            invoices = context.Invoices.OrderByDescending(i => i.SalesTax).ToList();
                        else
                            invoices = context.Invoices.OrderBy(i => i.SalesTax).ToList();
                        break;
                    }
                case 5:
                    {
                        if (isDesc)
                            invoices = context.Invoices.OrderByDescending(i => i.Shipping).ToList();
                        else
                            invoices = context.Invoices.OrderBy(i => i.Shipping).ToList();
                        break;
                    }
                case 6:
                    {
                        if (isDesc)
                            invoices = context.Invoices.OrderByDescending(i => i.InvoiceTotal).ToList();
                        else
                            invoices = context.Invoices.OrderBy(i => i.InvoiceTotal).ToList();
                        break;
                    }
                default:
                    {
                        if (isDesc)
                            invoices = context.Invoices.OrderByDescending(i => i.InvoiceID).ToList();
                        else
                            invoices = context.Invoices.OrderBy(i => i.InvoiceID).ToList();
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
                    invoices = invoices.Where(i =>
                        i.InvoiceID == lookUp ||
                        i.CustomerID == lookUp ||
                        i.ProductTotal == lookUp ||
                        i.SalesTax == lookUp ||
                        i.Shipping == lookUp ||
                        i.InvoiceTotal == lookUp ||
                        i.InvoiceDate.ToOADate() == lookUp 
                            ).ToList();
                }

            }
            return View(invoices);
        }

        [HttpGet]

        public ActionResult Upsert(int id = 0)
        {
            BooksEntities context = new BooksEntities();
            Invoice invoice = context.Invoices.Where(i => i.InvoiceID == id).FirstOrDefault();

            List<Customer> customers = context.Customers.ToList();

            UpsertInvoicesModel viewModel = new UpsertInvoicesModel()
            {
                Invoice = invoice,
                Customers = customers,
            };
            return View(viewModel);
        }


        [HttpPost]

        public ActionResult Upsert(UpsertInvoicesModel model)
        {
            Invoice newInvoice = model.Invoice;

            BooksEntities context = new BooksEntities();

            try
            {
                if (context.Invoices.Where(i => i.InvoiceID == newInvoice.InvoiceID).Count() > 0)
                {
                    var invoiceToSave = context.Invoices.Where(i =>
                                        i.InvoiceID == newInvoice.InvoiceID
                                        ).FirstOrDefault();

                    invoiceToSave.CustomerID = newInvoice.CustomerID;
                    invoiceToSave.InvoiceDate = newInvoice.InvoiceDate;
                    invoiceToSave.ProductTotal = newInvoice.ProductTotal;
                    invoiceToSave.SalesTax = newInvoice.SalesTax;
                    invoiceToSave.Shipping = newInvoice.Shipping;
                    invoiceToSave.InvoiceTotal = newInvoice.InvoiceTotal;
                }
                else
                {
                    context.Invoices.Add(newInvoice);
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