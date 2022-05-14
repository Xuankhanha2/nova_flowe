using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 10/7/2021
    /// created by: VXKHANH
    /// Lớp sản phẩm
    /// </summary>
    public class Product:BaseClass
    {
        #region Properties

        /// <summary>
        /// Khóa chính
        /// </summary>
        [required,duplicate]
        public Guid productId { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        [required]
        public string productName { get; set; }

        /// <summary>
        /// Mã danh mục sản phẩm
        /// </summary>
        public Guid? categoryId { get; set; }

        /// <summary>
        /// Giá của sản phẩm
        /// </summary>
        [required]
        public double? price { get; set; }

        /// <summary>
        /// giảm giá (%)
        /// </summary>
        public double? discount { get; set; }

        /// <summary>
        /// Sản phẩm đang hot
        /// </summary>
        public bool? hot { get; set; }

        /// <summary>
        /// Ảnh chính của sản phẩm
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// Mô tả sản phẩm
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Mã cửa hàng phân phối sản phẩm
        /// </summary>
        public Guid? providerId { get; set; }

        /// <summary>
        /// Trạng thái của sản phẩm ( true - còn hàng ; false - hết hàng )
        /// </summary>
        public bool? status { get; set; }
        #endregion 


    }
}
