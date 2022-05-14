using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public interface IProductService:IBaseService
    {

        /// <summary>
        /// created by: vxkhanh
        /// created date 12/11/2011
        /// Hàm lấy danh sách sản phẩm theo danh mục sản phẩm
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns></returns>
        public ServiceResult getProductsByCategory(Guid categoryId, int? orderVal);

        /// <summary>
        /// created by: vxkhanh
        /// created date: 16/11/2021
        /// Hàm tìm kiếm sản phẩm
        /// </summary>
        /// <param name="key">Từ khóa</param>
        /// <returns>ServiceResult</returns>
        public ServiceResult searchProduct(string key);
    }
}
