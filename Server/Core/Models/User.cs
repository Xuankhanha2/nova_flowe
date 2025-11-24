using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// Created date:  18/07/2021
    /// created by: VXKHANH
    /// Lớp người dùng - sử dụng để đắng nhập trang Admin
    /// </summary>
    public class User : BaseClass
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        [Required, Duplicate]
        public Guid userId { get; set; }

        /// <summary>
        /// Tên của người dùng
        /// </summary>
        [Required]
        public string? fullName { get; set; }

        /// <summary>
        /// tên tài khoản
        /// </summary>
        [Required, Duplicate]
        public string? username { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required]
        public string? password { get; set; }
        #endregion
    }
}
