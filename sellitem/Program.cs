using dsmodels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sellitem
{
    class Program
    {
        static DataModelsDB db = new DataModelsDB();

        static void Main(string[] args)
        {

            if (args == null || args.Count() == 0)
            {
                Console.WriteLine("please provide a listing id");
            }
            else
            {
                Process.Start("chrome.exe", "https://www.ebay.com/sh/ord/?filter=status:AWAITING_SHIPMENT");
                Process.Start("chrome.exe", "https://www.samsclub.com/sams/account/addressbook/addressBook.jsp?&locale=en_US&DPSLogout=true&_requestid=344908");
                Task.Run(async () =>
                {
                    var listing = await getListing(args[0]);
                    Console.WriteLine(listing.SourceUrl);
                    Process.Start("chrome.exe", listing.SourceUrl);
                }).Wait();
            }
        }

        static async Task<PostedListing> getListing(string listingItemId)
        {
            var postedListing = await db.GetPostedListing(listingItemId);
            return postedListing;
        }
    }
}
