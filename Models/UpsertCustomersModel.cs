using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vanharmelen_FinalProject.Models
{
    public class UpsertCustomersModel
    {
        public Customer Customer { get; set; }
        
        public List<State> States { get; set; }
    }
}