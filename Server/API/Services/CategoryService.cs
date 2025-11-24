using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    /// <summary>
    /// Created date: 20/07/2021
    /// Created by: VXKHANH
    /// Lớp service của danh mục sản phẩm
    /// </summary>
    public class CategoryService : BaseService , ICategoryService
    {
        ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository _categoryRepository) : base(_categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
    }
}
