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
        [required]
        [duplicate]
        public Guid orderDetailId { get; set; }

        /// <summary>
        /// khóa chính
        /// </summary>
        [required]
        public Guid orderId { get; set; }

        /// <summary>
        /// id của sản phẩm
        /// </summary>
        [required]
        public Guid productId { get; set; }

        /// <summary>
        /// số lượng sản phẩm
        /// </summary>
        [required]
        public int quantity { get; set; }
    }
}
