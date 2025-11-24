using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public interface IProductRepository:IBaseRepository
    {

        /// <summary>
        /// created date: 8/9/2021
        /// created by: vux xuan khanh
        /// Hàm lấy danh sách sản phẩm được sắp xếp the tiêu chí dược truyền vào và được lọc theo danh mục sản phẩm 
        /// </summary>
        /// <param name="categoryId">id của anh mục</param>
        /// <param name="order">Tiêu chí sắp xếp</param>
        /// <returns></returns>
        public IEnumerable<Product> getOrderedProduct(Guid categoryId, string order);

        /// <summary>
        /// created by: vxkhanh
        /// created date: 12/11/2021
        /// Hàm lấy danh sách sản phẩm theo danh mục sản phẩm
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns></returns>
        public IEnumerable<Product> getProductsByCategory(Guid categoryId, int? orderVal);

        /// <summary>
        /// created by: vxkhanh
        /// created date: 16/11/2021
        /// Hàm tìm kiếm sản phẩm 
        /// </summary>
        /// <param name="key">Từ khóa</param>
        /// <returns></returns>
        public IEnumerable<Product> searchProduct(string key);
    }
}
