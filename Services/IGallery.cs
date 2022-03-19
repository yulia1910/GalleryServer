using Gallery1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery1.Services
{
    public interface IGallery
    {
        Task<List<PictureModel>> GetGallery(int page, int limit);
    }
}
