using System;
using System.Web.Script.Serialization;

namespace SHPOT_ViewModel
{
    public class BusinessRatingVM
    {
        public int BusinessRatingID { get; set; }

        public int BusinessID { get; set; }

        [ScriptIgnore]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Review { get; set; }

        public string Rating { get; set; }
    }
}