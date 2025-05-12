using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities
{
   public class SpecialCityCost
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        //----------- Obj From CitySetting and ForeignKey CitySettingId ------------------------
        [ForeignKey(nameof(CitySetting))]
        public int CitySettingId { get; set; }
        public virtual CitySetting? CitySetting { get; set; }
        //----------- Obj From User and ForeignKey MerchantId ---------------------------------
        [ForeignKey(nameof(Merchant))]
        public string MerchantId { get; set; } = string.Empty;
        public virtual ApplicationUser? Merchant { get; set; }
    }
}