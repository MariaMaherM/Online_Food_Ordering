//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Online_Food_Ordering.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int ID_Order { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Food_ID { get; set; }
        public Nullable<int> Bill_ID { get; set; }
        public Nullable<int> User_ID { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual Food Food { get; set; }
        public virtual User User { get; set; }
    }
}
