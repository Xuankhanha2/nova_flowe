using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created by: KhanhVX
    /// created date: 1/5/2022
    /// Lớp ánh xạ đến bảng hóa đơn
    /// </summary>
    public class Order:BaseClass
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        [required]
        [duplicate]
        public Guid orderId { get; set; }

        /// <summary>
        /// Id của khách hàng
        /// </summary>
        [required]
        public Guid customerId { get; set; }

        /// <summary>
        /// Trạng thái của hóa đơn
        /// </summary>
        [required]
        public int status { get; set; }

        /// <summary>
        /// chứa danh sách order detail
        /// </summary>
        public List<OrderDetail> orderDetails { get; set; }
    }
}
