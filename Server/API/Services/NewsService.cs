using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// </summary>
    public class NewsService : BaseService, INewsService
    {
        INewsRepository newsRepository;
        public NewsService(INewsRepository _newsRepository) : base(_newsRepository)
        {
            newsRepository = _newsRepository;
        }
    }
}
