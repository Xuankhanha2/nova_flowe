using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    /// <summary>
    /// Created date: 20/7/2021
    /// Created by: VXKHANH
    /// Lớp điều khiển cho News
    /// </summary>
    public class NewsController : BaseController<News>
    {
        INewsService newsService;
        public NewsController(INewsService _newsService) : base(_newsService)
        {
            newsService = _newsService;
        }
    }
}
