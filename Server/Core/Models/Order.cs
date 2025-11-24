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
        [Required]
        [Duplicate]
        public Guid orderId { get; set; }

        /// <summary>
        /// mã hóa đơn
        /// </summary>
        public int? orderCode { get; set; }

        /// <summary>
        /// Id của khách hàng
        /// </summary>
        [Required]
        public Guid customerId { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string customerName { get; set; }

        /// <summary>
        /// Số điện thoại của khách hàng
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ khách hàng
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Trạng thái của hóa đơn
        /// </summary>
        [Required]
        public int status { get; set; }

        /// <summary>
        /// chứa danh sách order detail
        /// </summary>
        public List<OrderDetail> orderDetails { get; set; }
    }
}
