using Gallery1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gallery1.Services
{
    public class GalleryService : IGallery
    {
        private readonly HttpClient client;

        public GalleryService(HttpClient httpCleint)
        {
            client = httpCleint;
        }
        public async Task<List<PictureModel>> GetGallery(int page, int limit)
        {
           // var url = string.Format("https://picsum.photos/v2/list/{0}/{1}", 1, 100);
            var url = string.Format("https://picsum.photos/v2/list?page=1&limit=100");
          //   /api/v2/PublicHolidays/{0}/{1}", year, countryCode);
            var result = new List<PictureModel>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                

                  var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<List<PictureModel>>(stringResponse,
                   new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
