

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shipping.Api.Core.Domain.Models;

    public class OrderReport
    {
        public int Id { get; set; }
        public string ReportDetails { get; set; } = string.Empty;
        public DateTime ReportDate { get; set; } = DateTime.Now;
        //----------- Obj From Order and ForeignKey OrderId ---------------------------------
        [ ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }

