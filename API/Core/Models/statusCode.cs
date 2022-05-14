using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 20/06/2021
    /// created by: VXKHANH
    /// </summary>
    public enum statusCode 
    {
        /// <summary>
        /// Lỗi exception
        /// </summary>
        exception = 500,

        /// <summary>
        /// Lỗi dữ liệu không hợp lệ
        /// </summary>
        notValid = 400,

        /// <summary>
        /// Mã thành công
        /// </summary>
        success = 200,

        /// <summary>
        /// Đã thêm dữ liệu mới
        /// </summary>
        created = 201,

        /// <summary>
        /// Không có dữ liệu
        /// </summary>
        noContent = 204,

        /// <summary>
        /// Thực hiện thất bại
        /// </summary>
        fail = 401
    }
}
