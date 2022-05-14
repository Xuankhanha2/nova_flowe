using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 18/07/2021
    /// created by: VXKHANH
    /// Lớp danh mục sản phẩm
    /// </summary>
    #region Properties
    public class Category:BaseClass
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [required, duplicate]
        public Guid categoryId { get; set; }

        /// <summary>
        /// Tên danh mục 
        /// </summary>
        [required]
        public string categoryName { get; set; }

        /// <summary>
        /// id của danh mục cha nếu có
        /// </summary>
        public Guid? parentId { get; set; }

        /// <summary>
        /// icon của danh mục - có thể là 1 thẻ <i/> của fontawsome
        /// </summary>
        public string categoryIcon { get; set; }
    }
    #endregion
}
