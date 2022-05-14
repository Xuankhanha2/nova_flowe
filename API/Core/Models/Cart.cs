using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 02/11/2021
    /// created by: VXKHANH
    /// Lớp giỏ hàng 
    /// </summary>
    public class Cart:BaseClass
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        [required, duplicate]
        public Guid cartId { get; set; }

        /// <summary>
        /// khóa ngoại - id của sản phẩm
        /// </summary>
        [required]
        public Guid productId { get; set; }

        /// <summary>
        /// Khóa ngoại - id của khách hàng
        /// </summary>
        [required]
        public Guid customerId { get; set; }

        /// <summary>
        /// Số lượng của sản phẩm
        /// </summary>
        public int? quantity { get; set; }

        /// <summary>
        /// Tổng tiền cần thanh toán
        /// </summary>
        public double? total { get; set; }
        #endregion 
    }
}
