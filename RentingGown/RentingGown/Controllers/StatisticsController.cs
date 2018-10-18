using RentingGown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentingGown.Controllers
{
    public class StatisticsController : Controller
    {
        private static RentingGownDB db = new RentingGownDB();
        // GET: Statistics
        public ActionResult Statistics()
        {
            
            ViewBag.colors = string.Join(",", PopularColors().Keys.Take(5)).Trim();
            ViewBag.colorsValues = string.Join(",", PopularColors().Values.Take(5)).Trim();

            ViewBag.catgories = string.Join(",", PopularCatgory().Keys.Take(5)).Trim();
            ViewBag.catgoriesValues = string.Join(",", PopularCatgory().Values.Take(5)).Trim();

            ViewBag.favouriteLength = string.Join(",", FavouriteLength().Keys.Take(5)).Trim();
            ViewBag.favouriteLengthValues = string.Join(",", FavouriteLength().Values.Take(5)).Trim();

            ViewBag.priceRange = string.Join(",", PriceRange().Keys.Take(5)).Trim();
            ViewBag.priceRangeValues = string.Join(",", PriceRange().Values.Take(5)).Trim();

            ViewBag.favouriteLight = string.Join(",", FavouriteLight().Keys.Take(5)).Trim();
            ViewBag.favouriteLightValues = string.Join(",", FavouriteLight().Values.Take(5)).Trim();

            return View();
        }

        public static Dictionary<string, string> PopularColors()
        {
            Dictionary<string, string> colors = new Dictionary<string, string>();
            foreach (Colors color in db.Colors)
            {
                int sum = 0;
                foreach (Rents rent in db.Rents)
                {
                    if (rent.Gowns.color == color.id_color)
                        sum++;

                }
                if (sum > 0)
                    colors.Add("'"+color.color+"'", sum.ToString());
            }
            List<KeyValuePair<string, string>> myList = colors.ToList();

           myList.Sort((pair1,pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();
            colors = myList.ToDictionary(x => x.Key, x => x.Value); 
            return colors;
        }

        public static Dictionary<string, string> PopularCatgory()
        {
            Dictionary<string, string> catgories = new Dictionary<string, string>();
            foreach (Catgories catgory in db.Catgories)
            {
                int sum = 0;
                foreach (Rents rent in db.Rents)
                {
                    if (rent.Gowns.id_catgory == catgory.id_catgory)
                        sum++;

                }
                if (sum > 0)
                    catgories.Add("'" + catgory.catgory + "'", sum.ToString());
            }
            List<KeyValuePair<string, string>> myList = catgories.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();
            catgories = myList.ToDictionary(x => x.Key, x => x.Value);
            return catgories;
        }

        public static Dictionary<string, string> FavouriteLength()
        {
            Dictionary<string, string> favouriteLength = new Dictionary<string, string>();
           int sum = 0;
            foreach (Rents rent in db.Rents)
            {
                
                if (db.Gowns.Where(p => p.id_gown == rent.id_gown).FirstOrDefault().is_long == true)
                    sum++;
               
            }
            if (sum > 0)
                favouriteLength.Add("'" + "ארוך" + "'", sum.ToString());
            favouriteLength.Add("'" + "קצר" + "'", (db.Rents.Count()-sum).ToString());
            List<KeyValuePair<string, string>> myList = favouriteLength.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();
            favouriteLength = myList.ToDictionary(x => x.Key, x => x.Value);
            return favouriteLength;
        }
        public static Dictionary<string, string> FavouriteLight()
        {
            Dictionary<string, string> favouriteLight = new Dictionary<string, string>();
            int sum = 0;
            foreach (Rents rent in db.Rents)
            {

                if (db.Gowns.Where(p => p.id_gown == rent.id_gown).FirstOrDefault().is_light == true)
                    sum++;

            }
            if (sum > 0)
                favouriteLight.Add("'" + "בהיר" + "'", sum.ToString());
            favouriteLight.Add("'" + "כהה" + "'", (db.Rents.Count() - sum).ToString());
            List<KeyValuePair<string, string>> myList = favouriteLight.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();
            favouriteLight = myList.ToDictionary(x => x.Key, x => x.Value);
            return favouriteLight;
        }
        public static Dictionary<string, string> PriceRange()
        {
            Dictionary<string, string> priceRange = new Dictionary<string, string>();
            priceRange.Add("'" + "100-200" + "'","0" );
            priceRange.Add("'" + "200-300" + "'","0" );
            priceRange.Add("'" + "300-400" + "'","0" );
            priceRange.Add("'" + "400-500" + "'","0" );
            int price = 0;
            foreach (Rents rent in db.Rents)
            {
                if (price <=300)
                    price += 100;
                else price = 0;
                if (db.Gowns.Where(p => p.id_gown == rent.id_gown).FirstOrDefault().price > price && db.Gowns.Where(p => p.id_gown == rent.id_gown).FirstOrDefault().price < (price+100))
                priceRange["'"+price.ToString() + "-" + (price + 100).ToString()+"'"]=(int.Parse(priceRange["'"+price.ToString() + "-" + (price + 100).ToString()+"'"])+1).ToString();
      
            }
            List<KeyValuePair<string, string>> myList = priceRange.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();
            priceRange = myList.ToDictionary(x => x.Key, x => x.Value);
            return priceRange;
        }
    }
}