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
    
    public partial class TransactionList
    {
        public int TransactionsId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string ItemCode { get; set; }
        public int Quanlity { get; set; }
        public System.DateTime InputDate { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public Nullable<int> Price { get; set; }
        public string Note { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
    }
}
