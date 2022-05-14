using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 24/06/2021
    /// created by: VXKHANH
    /// </summary>
    public class BaseClass
    {
        #region Contructor
        public BaseClass()
        {
            this.selectedItem = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        public DateTime? createdDate { get; set; }

        /// <summary>
        /// Người tạo 
        /// </summary>
        public string createdBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi gần nhất
        /// </summary>
        public DateTime? modifiedDate { get; set; }

        /// <summary>
        /// Biến dử dụng để xác định phần tử đươc chọn trong lưới dữ liệu
        /// </summary>
        public bool? selectedItem { get; set; }

        /// <summary>
        /// Attribute Bắt buộc không để trống
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class required:Attribute {

        }

        /// <summary>
        /// Attribute Không được phép trùng nhau
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class duplicate: Attribute
        {

        }

        #endregion
    }
}
