using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vanharmelen_FinalProject.Models
{
    public class UpsertInvoicesModel
    {
        public Invoice Invoice { get; set; }

        public List<Customer> Customers { get; set; }
    }
}