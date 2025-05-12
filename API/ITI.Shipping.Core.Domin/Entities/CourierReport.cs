using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities
{
    public class CourierReport
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //----------- Obj From User and ForeignKey UserId ---------------------------------
        [ForeignKey(nameof(Courier))]
        public string CourierId { get; set; } = string.Empty;
        public virtual ApplicationUser? Courier { get; set; }
        //----------- Obj From Order and ForeignKey OrderId --------------------------------
        [ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}