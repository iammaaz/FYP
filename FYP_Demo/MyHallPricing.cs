//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FYP_Demo
{
    using System;
    using System.Collections.Generic;
    
    public partial class MyHallPricing
    {
        public int HallInfoID { get; set; }
        public int PricePerPerson { get; set; }
        public int RentPerHour { get; set; }
        public string AdditionalPricingInfo { get; set; }
        public Nullable<int> ReservationDeposit { get; set; }
        public Nullable<int> CleaningFee { get; set; }
        public string CancellationPolicy { get; set; }
    
        public virtual HallInfo HallInfo { get; set; }
    }
}
