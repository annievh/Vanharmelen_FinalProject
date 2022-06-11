using Vanharmelen_FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books_FinalProject.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult All(string id, int sortBy = 0, bool isDesc = false)
        {
            BooksEntities context = new BooksEntities();
            List<Customer> customers;
            switch (sortBy)
            {
                case 1:
                    {
                        if (isDesc)
                            customers = context.Customers.OrderByDescending(c => c.Name).ToList();
                        else
                            customers = context.Customers.OrderBy(c => c.Name).ToList();
                        break;

                    }
                case 2:
                    {
                        if (isDesc)
                            customers = context.Customers.OrderByDescending(c => c.Address).ToList();
                        else
                            customers = context.Customers.OrderBy(c => c.Address).ToList();
                        break;
                    }
                case 3:
                    {
                        if (isDesc)
                            customers = context.Customers.OrderByDescending(c => c.City).ToList();
                        else
                            customers = context.Customers.OrderBy(c => c.City).ToList();
                        break;
                    }
                case 4:
                    {
                        if (isDesc)
                            customers = context.Customers.OrderByDescending(c => c.State).ToList();
                        else
                            customers = context.Customers.OrderBy(c => c.State).ToList();
                        break;
                    }
                case 5:
                    {
                        if (isDesc)
                            customers = context.Customers.OrderByDescending(c => c.ZipCode).ToList();
                        else
                            customers = context.Customers.OrderBy(c => c.ZipCode).ToList();
                        break;
                    }
                default:
                    {
                        if (isDesc)
                            customers = context.Customers.OrderByDescending(c => c.CustomerID).ToList();
                        else
                            customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
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
                    customers = customers.Where(c =>
                        c.CustomerID == lookUp 
                            ).ToList();
                }
                else
                {
                    customers = customers.Where(c =>
                        c.Name.Contains(id) ||
                        c.Address.Contains(id) ||
                        c.City.Contains(id) ||
                        c.State.Contains(id) ||
                        c.ZipCode.Contains(id)
                            ).ToList();
                }
            }
            customers = customers.Where(c => c.isDeleted == false).ToList();
            return View(customers);
        }

        [HttpGet]

        public ActionResult Upsert(string id)
        {
            BooksEntities context = new BooksEntities();
            int ID = Convert.ToInt32(id);
            Customer customer = context.Customers.Where(c => c.CustomerID == ID).FirstOrDefault();

            List<State> states = context.States.ToList();

            UpsertCustomersModel viewModel = new UpsertCustomersModel()
            {
                Customer = customer,
                States = states,
            };
            return View(viewModel);
        }


        [HttpPost]

        public ActionResult Upsert(UpsertCustomersModel model)
        {
            Customer newCustomer = model.Customer;

            BooksEntities context = new BooksEntities();

            bool isNewCustomer = false;//for email
            try
            {
                if (context.Customers.Where(c => c.CustomerID == newCustomer.CustomerID).Count() > 0)
                {
                    var customerToSave = context.Customers.Where(c =>
                                        c.CustomerID == newCustomer.CustomerID
                                        ).FirstOrDefault();

                    customerToSave.Name = newCustomer.Name;
                    customerToSave.Address = newCustomer.Address;
                    customerToSave.City = newCustomer.City;
                    customerToSave.State = newCustomer.State;
                    customerToSave.ZipCode = newCustomer.ZipCode;
                }
                else
                {
                    context.Customers.Add(newCustomer);
                    isNewCustomer = true;
                }
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("All");
        }

        [HttpGet]

        public ActionResult Delete(string id)
        {
            BooksEntities context = new BooksEntities();
            int customerId = 0;
            if (int.TryParse(id, out customerId))
            {
                try
                {
                    Customer customer = context.Customers.Where(c => c.CustomerID.ToString() == id).FirstOrDefault();
                    customer.isDeleted = false;

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                // not successful parsing
            }
            return RedirectToAction("All");
        }
    }
}