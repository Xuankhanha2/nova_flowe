using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 18/07/2021
    /// created by: VXKHANH
    /// Lớp cửa hàng/ nhà sản xuất sản phẩm
    /// </summary>
    public class Provider : BaseClass
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Required, Duplicate]
        public Guid providerId { get; set; }

        /// <summary>
        /// tên của hàng/ Nhà cung cấp
        /// </summary>
        [Required]
        public string providerName { get; set; }

    }
}
