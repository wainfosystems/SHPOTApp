using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SHPOT_DAL
{
    public class FavouritePlace
    {
        [Key]
        public int FavouritePlaceID { get; set; }
        
        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Business")]
        public int BusinessID { get; set; }

        public virtual Business Business { get; set; }

        public virtual User User { get; set; }
    }
}