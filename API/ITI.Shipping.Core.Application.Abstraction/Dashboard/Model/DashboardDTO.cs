using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Dashboard.DTO;
public class EmpDashboardDTO
{
    public int TotalDelivered { get; set; }
    public int TotalPending { get; set; }
    public int TotalCancelled { get; set; }
    public int TotalInProcessing { get; set; }
    public int TotalAwaitingConfirmation { get; set; }
    public int TotalRejected { get; set; }
    public int TotalReceived { get; set; }
    public int TotalShipped { get; set; }
    public int TotalReturned { get; set; }
    public int TotalPayed { get; set; }
    public int TotalUpdated { get; set; }
}

public class MerchantDashboardDTO
{
    public int TotalDelivered { get; set; }
    public int TotalPending { get; set; }
    public int TotalAwaitingConfirmation { get; set; }
    public int TotalCancelledByTheRecipient { get; set; }
    public int TotalRejectedWithPayed { get; set; }
    public int TotalPostponed { get; set; }
    public int TotalDeliveredToTheRepresentative { get; set; }
    public int TotalRejectedWithPartialPayment { get; set; }
    public int TotalRejectedAndAotPaid { get; set; }
    public int TotalPartiallyDelivered { get; set; }
    public int TotalCantAccess{ get; set; }
}
