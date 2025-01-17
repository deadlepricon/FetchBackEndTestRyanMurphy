using System;
namespace FetchBackEndTestRyanMurphy.Models
{
    public class Receipt
	{
        public string Retailer {get; set;}
        public string PurchaseDate { get; set; }
        public string PurchaseTime { get; set; }
        public List<Item> Items { get; set; }
        public double Total { get; set; }

        public Receipt()
		{
		}
	}
}