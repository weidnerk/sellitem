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
                Console.WriteLine(CreateSQL(args[0]));
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

        static string CreateSQL(string listingId)
        {
            //string sql = string.Format("exec sp_SoldUpdate '{0}', '{1}', @i_paid", listingId, DateTime.Today.ToShortDateString());
            string sql = string.Format("exec sp_SoldUpdate '{0}', '{1}', @i_paid, '@order_num'", listingId, DateTime.Now.ToString());
            return sql;
        }

        static async Task<PostedListing> getListing(string listingItemId)
        {
            var postedListing = await db.GetPostedListingFromListId(listingItemId);
            return postedListing;
        }
    }
}
