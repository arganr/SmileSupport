using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.WaiveLateCharges
{
    [StringIdAttribute("DXSoechiWebApp.Tools.LocalizationRes")]
    public enum TicketStatus
    {
        [DefaultString("Incomplete")]
        Incomplete,
        [DefaultString("Submit")]
        Submit,
        [DefaultString("Reject")]
        Reject,
        [DefaultString("Need Approval Required")]
        NeedApproval,
        [DefaultString("Complete")]
        Complete,
        [DefaultString("Reserve")]
        Reserve,
    }  
}