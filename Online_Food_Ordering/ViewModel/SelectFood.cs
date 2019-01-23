using Online_Food_Ordering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering.ViewModel
{
    public class SelectFood
    {
        public IEnumerable<Food> Foods { get; set; }
        public  Order Order { get; set; }
    }
}