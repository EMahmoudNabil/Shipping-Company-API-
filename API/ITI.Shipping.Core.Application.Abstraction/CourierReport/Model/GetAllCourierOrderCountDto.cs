using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Application.Abstraction.CourierReport.Model
{
    public class GetAllCourierOrderCountDto
    {
        public required string CourierName { get; set; }
        public int OrdersCount { get; set; } = 0;
    }
}
