using System.ComponentModel.DataAnnotations;

namespace Cheaper_Effort.Models
{
    public class Discount
    {
        public uint Id { get; set; } //id of the claimed discount

        [Required]
        [EnumDataType(typeof(Discounts))] //Discount - table, Discounts - enum
        public Discounts DiscountsType{ get; set; } //discount category: 5, 10 or 15


        public string Claimer { get; set; } = String.Empty; //person who claims the discount

        public string Code { get; set; } = String.Empty; //random string for claiming in markets    

        public DateTime DateClaimed { get; set; } //date to check for expiration

    }
}
