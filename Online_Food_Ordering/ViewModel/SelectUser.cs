using Online_Food_Ordering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Food_Ordering.ViewModel
{
    public class SelectUser
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable <Bill> Bill { get; set; }
    }
}