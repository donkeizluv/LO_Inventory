//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LO_Inventory
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderList
    {
        public string ItemCode { get; set; }
        public int Quanlity { get; set; }
        public string CabinetName { get; set; }
        public string Provider { get; set; }
        public Nullable<int> Price { get; set; }
        public System.DateTime InputDate { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Note { get; set; }
        public string Username { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int CabinetId { get; set; }
        public Nullable<int> UserId { get; set; }
        public int ProviderId { get; set; }
    }
}
