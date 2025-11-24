using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 1/5/2022
    /// created by: KhanhVX
    /// Lớp ánh xạ đến bảng Chi tiết hóa đơn
    /// </summary>
    public class OrderDetail: BaseClass
    {
        /// <summary>
        /// khoas chính
        /// </summary>
        [Required]
        [Duplicate]
        public Guid orderDetailId { get; set; }

        /// <summary>
        /// khóa chính
        /// </summary>
        [Required]
        public Guid orderId { get; set; }

        /// <summary>
        /// id của sản phẩm
        /// </summary>
        [Required]
        public Guid productId { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// Giá sản phẩm sau khi discount
        /// </summary>
        public double? price { get; set; }

        /// <summary>
        /// số lượng sản phẩm
        /// </summary>
        [Required]
        public int quantity { get; set; }
    }
}
