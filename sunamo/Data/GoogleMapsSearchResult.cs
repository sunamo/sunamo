using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoSmartTools.Data
{
    public class GoogleMapsSearchResult
    {
        public string name;
        public int ratingCount;
        public float rating;
        public string phone;
        public string address;
        public string uri = "";
        public bool ReservationRequired = false;
        public string OpeningHours;
    }
}