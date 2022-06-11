using Vanharmelen_FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books_FinalProject.Controllers
{
    public class StatesController : Controller
    {
        // GET: States
        public ActionResult All(string id, int sortBy = 0, bool isDesc = false)
        {
            BooksEntities context = new BooksEntities();
            List<State> states;

            switch (sortBy)
            {
                case 1:
                    {
                        if (isDesc)
                            states = context.States.OrderByDescending(s => s.StateName).ToList();
                        else
                            states = context.States.OrderBy(s => s.StateName).ToList();
                        break;

                    }
                default:
                    {
                        if (isDesc)
                            states = context.States.OrderByDescending(s => s.StateCode).ToList();
                        else
                            states = context.States.OrderBy(s => s.StateCode).ToList();
                        break;
                    }
            }
            if (!string.IsNullOrWhiteSpace(id))
            {

                id = id.Trim().ToLower();
                states = states.Where(s =>
                            s.StateName.Contains(id) 
                                ).ToList();


            }
            return View(states);
        }

        //[HttpGet]

        //public ActionResult Upsert(string id)
        //{
        //    BooksEntities context = new BooksEntities();
        //    State states = context.States.Where(s => s.StateCode == id).FirstOrDefault();


        //    return View(states);
        //}


        //[HttpPost]

        //public ActionResult Upsert(State state)
        //{
        //    Customer newCustomer = model.Customer;

        //    BooksEntities context = new BooksEntities();

        //    bool isNewCustomer = false;//for email
        //    try
        //    {
        //        if (context.Customers.Where(c => c.CustomerID == newCustomer.CustomerID).Count() > 0)
        //        {
        //            var customerToSave = context.Customers.Where(c =>
        //                                c.CustomerID == newCustomer.CustomerID
        //                                ).FirstOrDefault();

        //            customerToSave.Name = newCustomer.Name;
        //            customerToSave.Address = newCustomer.Address;
        //            customerToSave.City = newCustomer.City;
        //            customerToSave.State = newCustomer.State;
        //            customerToSave.ZipCode = newCustomer.ZipCode;
        //        }
        //        else
        //        {
        //            context.Customers.Add(newCustomer);
        //            isNewCustomer = true;
        //        }
        //        context.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    return RedirectToAction("All");
        //}
    }
}

