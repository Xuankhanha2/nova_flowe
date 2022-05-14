using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    /// <summary>
    /// created date: 18/07/2021
    /// created by: VXKHANH
    /// Lớp tin tức
    /// </summary>
    public class News : BaseClass
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [required,duplicate]
        public Guid newsId { get; set; }

        /// <summary>
        /// Tiêu đề tin tức
        /// </summary>
        [required]
        public string newsTitle { get; set; }

        /// <summary>
        /// Mô tả về tin tức
        /// </summary>
        public string newsDescription { get; set; }


        /// <summary>
        /// Nội dung tin tức
        /// </summary>
        public string newsContent { get; set; }

        /// <summary>
        /// Tin hot 
        /// </summary>
        public int? hotNews { get; set; }

        /// <summary>
        /// Ảnh tin
        /// </summary>
        public string newsImage { get; set; }

        /// <summary>
        /// Nguồn tin
        /// </summary>
        public string newsBy { get; set; }
    }
}
