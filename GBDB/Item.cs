//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GBDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int ItemID { get; set; }
        public int SellerID { get; set; }
        public int CategoryID { get; set; }
        public int AltCategoryID { get; set; }
        public string ItemTitle { get; set; }
        public int Quantity { get; set; }
        public decimal StartingBid { get; set; }
        public decimal ReservePrice { get; set; }
        public int BidCount { get; set; }
        public int HighBidderID { get; set; }
        public decimal CurrentBid { get; set; }
        public System.DateTime StartingDate { get; set; }
        public System.DateTime EndingDate { get; set; }
        public byte FeaturedListing { get; set; }
        public byte ShowcaseListing { get; set; }
        public bool Highlight { get; set; }
        public bool BoldfaceTitle { get; set; }
        public int ColoredTitle { get; set; }
        public bool CreditCardLogo { get; set; }
        public bool HasCounter { get; set; }
        public bool HasPicture { get; set; }
        public string Thumbnail { get; set; }
        public decimal BuyNowPrice { get; set; }
        public string Condition { get; set; }
        public string Subtitle { get; set; }
        public Nullable<decimal> FixedPrice { get; set; }
        public Nullable<bool> HasQuickLook { get; set; }
        public string SKU { get; set; }
        public string SerialNumber { get; set; }
        public bool EligibleForImmediateCheckout { get; set; }
        public string Char_Manufacturer { get; set; }
        public string Char_Model { get; set; }
        public string Char_Caliber { get; set; }
        public string Char_Gauge { get; set; }
        public string Char_BarrelLength { get; set; }
        public string Char_Capacity { get; set; }
        public string Char_SlideFinish { get; set; }
        public string Char_FrameFinish { get; set; }
        public string Char_Grips { get; set; }
        public string Char_NumberOfRounds { get; set; }
    }
}