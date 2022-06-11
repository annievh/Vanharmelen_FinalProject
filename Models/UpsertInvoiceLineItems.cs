using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vanharmelen_FinalProject.Models
{
    public class UpsertInvoiceLineItems
    {
        public InvoiceLineItem InvoiceLineItem { get; set; }

        public List<Invoice> Invoices { get; set; }

        public List<Product> Products { get; set; }

    }
}