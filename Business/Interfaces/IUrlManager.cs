using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUrlManager
    {
        Task<ShortUrl> ShortenUrl(string longUrl, string ip, string segment = "");
        Task<Stat> Click(string segment, string referer, string ip);
    }
}
