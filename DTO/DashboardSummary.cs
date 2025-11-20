using System;

namespace BTL_LTTQ.DTO
{
    public class DashboardSummary
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalItems { get; set; }
        public decimal AverageOrderValue
        {
            get
            {
                if (TotalOrders == 0)
                {
                    return 0;
                }

                return Math.Round(TotalRevenue / TotalOrders, 0);
            }
        }
    }
}

