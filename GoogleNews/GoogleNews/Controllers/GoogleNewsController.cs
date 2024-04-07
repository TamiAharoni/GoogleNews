using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace
{
    public class GoogleNewsController : Controller
    {
        private readonly GoogleNewsDAL newsDAL;

        /// <summary>
        /// Defination of object DAL.
        /// </summary>
        /// <param name="_newsDAL"></param>
        public GoogleNewsController(GoogleNewsDAL _newsDAL)
        {
            newsDAL = _newsDAL;
        }

        /// <summary>
        /// Function that return akk data by call to DAL.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                var news = await newsDAL.GetAllNews();
                return Ok(news);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// The function get title and return Item of title.
        /// by used object of DAL.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetItem(string title)
        {
            try
            {
                var newsItem = await newsDAL.GetItem(title);
                if (newsItem != null)
                {
                    return Ok(newsItem);
                }
                else
                {
                    return NotFound("Not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
            return BadRequest();
        }
    }
}