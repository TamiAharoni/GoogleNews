using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace
{
    public class GoogleNewsController : Controller
    {
        private readonly GoogleNewsDAL _newsDAL;

        public GoogleNewsController(GoogleNewsDAL newsDAL)
        {
            _newsDAL = newsDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                var news = await _newsDAL.GetAllNews();
                return Ok(news);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(string title)
        {
            if (title == "null")
            {
                return Ok(null);
            }
            else
            {
                try
                {
                    var newsItem = await _newsDAL.GetItem(title);
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
            }
            return BadRequest();
        }
    }
}