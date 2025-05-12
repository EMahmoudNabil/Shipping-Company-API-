using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Pramter_Helper;
public class Pramter
{
    private const int MaxPageSize = 10;
    private int? pageSize ;

    public int? PageSize
    {
        get { return pageSize; }
        set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
    }
    public int? PageNumber { get; set; } = 1;
}