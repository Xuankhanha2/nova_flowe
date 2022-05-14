using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 29/06/2021
    /// created by: VXKHANH
    /// Lớp khách hàng
    /// </summary>
    public class Customer:BaseClass
    {
        #region Properties
        /// <summary>
        /// Khóa chính của lớp khách hàng
        /// </summary>
        [duplicate]
        [required]
        public Guid customerId { get; set; }

        /// <summary>
        /// Họ tên khách hàng
        /// </summary>
        [required]
        public string customerName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [required]
        public string phoneNumber { get; set; }

        /// <summary>
        /// địa chỉ 
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Ngày sinh 
        /// </summary>
        public DateTime? dateOfBirth { get; set; }

        /// <summary>
        /// Giới tính (0 - Nữ, 1-Nam, 2 - Khách)
        /// </summary>
        public int? gender { get; set; }

        /// <summary>
        /// Tên tài khoản 
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string password { get; set; }
        #endregion

        #region Constructor
        public Customer()
        {
            this.customerId = new Guid();
        }
        #endregion
    }
}
