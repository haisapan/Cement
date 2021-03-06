//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cement
    {
        public long Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductionId { get; set; }
        public string ProductionName { get; set; }
        public double SendWeight { get; set; }
        public double ReceiveWeight { get; set; }
        public double SendUnitPrice { get; set; }
        public double ReceiveUnitPrice { get; set; }
        public double TransferUnitPriceInContract { get; set; }
        public double ReceiveTransferUnitPrice { get; set; }
        public string DriverName { get; set; }
        public double PaidAmount { get; set; }
        public Nullable<double> OverDraft { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string Remark { get; set; }
        public Nullable<double> PaidTransferAmount { get; set; }
    }
}
