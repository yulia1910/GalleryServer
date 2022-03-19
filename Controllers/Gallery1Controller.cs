using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gallery1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gallery1.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace Gallery1.Controllers
{
    // [Route("api/[controller]")]
    [Route("[controller]")]
  
    [ApiController]
    public class Gallery1Controller : ControllerBase
    {
        private readonly IGallery galleryService;
        private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache;
        public Gallery1Controller(IGallery service, IMemoryCache cache)
        {
            memoryCache = cache;
            galleryService = service;
        }

        [HttpGet("{start}")]
        [AllowAnonymous]
        [Route("")]
        [Route("index", Order = 1)]
        public async Task<IActionResult> GetGallery(int start)
        {
            var cacheKey = "picturesList";
            //checks if cache entries exists
            if (!memoryCache.TryGetValue(cacheKey, out List<PictureModel> picturesList))
            {
                //calling the server
                picturesList = await galleryService.GetGallery(1, 100);

                //setting up cache options
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(30)
                };
                //setting cache entries
                memoryCache.Set(cacheKey, picturesList, cacheExpiryOptions);
            }
            if (start < picturesList.Count)
            {
                return Ok(picturesList.GetRange(start, 5));
            }
            else
            {
                return NotFound();
            }

        }
    }
}
