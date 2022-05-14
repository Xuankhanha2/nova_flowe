using Core.Interfaces;
using Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infastructure.Repositories
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public class ProductRepository:BaseRepository, IProductRepository
    {
        /// <summary>
        /// created date: 8/9/2021
        /// created by: vux xuan khanh
        /// Hàm lấy danh sách sản phẩm được sắp xếp the tiêu chí dược truyền vào và được lọc theo danh mục sản phẩm 
        /// </summary>
        /// <param name="categoryId">id của anh mục</param>
        /// <param name="order">Tiêu chí sắp xếp</param>
        /// <returns></returns>
        public IEnumerable<Product> getOrderedProduct(Guid categoryId, string order)
        {
            return null;
        }

        /// <summary>
        /// created by: vxkhanh
        /// created date: 12/11/2021
        /// Hàm lấy danh sách sản phẩm theo danh mục sản phẩm
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns></returns>
        public IEnumerable<Product> getProductsByCategory(Guid categoryId, int? orderVal)
        {
            string procName = $"procGetProductsByCategory";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("categoryId", categoryId.ToString());
            //Nếu orderVal null thì gán mặc định = 0;
            if (orderVal == null)
                orderVal = 0;
            //orderVal ( 1 - sắp theo tên z-a; 2 - sắp xếp theo giá tăng dần; 3 - sắp xếp theo giá giảm dần; mặc định - sắp xếp theo tên a-z)
            dynamicParameters.Add($"orderVal", orderVal);
            var products = dbConnection.Query<Product>(procName, dynamicParameters, commandType: CommandType.StoredProcedure);
            return products;
        }

        /// <summary>
        /// created by: vxkhanh
        /// created date: 16/11/2021
        /// Hàm tìm kiếm sản phẩm 
        /// </summary>
        /// <param name="key">Từ khóa</param>
        /// <returns></returns>
        public IEnumerable<Product> searchProduct(string key)
        {
            string procName = "procSearchProduct";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"productName", key);
            var products = dbConnection.Query<Product>(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return products;
        }
    }

}
