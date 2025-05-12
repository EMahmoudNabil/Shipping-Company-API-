using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Entities
{
    public class OrderReport
    {
        public int Id { get; set; }
        public string ReportDetails { get; set; } = string.Empty;
        public DateTime ReportDate { get; set; } = DateTime.Now;
        //----------- Obj From Order and ForeignKey OrderId ---------------------------------
        [Required, ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
