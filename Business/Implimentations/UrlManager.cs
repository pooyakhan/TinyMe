using Business.Interfaces;
using Data;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implimentations
{
    public class UrlManager : IUrlManager
    {

            public Task<ShortUrl> ShortenUrl(string longUrl, string ip, string segment = "")
            {
                return Task.Run(() =>
                {
                    using (var context = new MyDbContext())
                    {
                        ShortUrl url;

                        url = context.ShortUrls.Where(u => u.LongUrl == longUrl).FirstOrDefault();
                        if (url != null)
                        {
                            return url;
                        }

                        if (!string.IsNullOrEmpty(segment))
                        {
                            if (context.ShortUrls.Where(u => u.Segment == segment).Any())
                            {
                                throw new TinyMeConflictException();
                            }
                        }
                        else
                        {
                            segment = this.NewSegment();
                        }

                        if (string.IsNullOrEmpty(segment))
                        {
                            throw new ArgumentException("Segment is empty");
                        }

                        url = new ShortUrl()
                        {
                            Added = DateTime.Now,
                            Ip = ip,
                            LongUrl = longUrl,
                            NumOfClicks = 0,
                            Segment = segment
                        };

                        context.ShortUrls.Add(url);

                        context.SaveChanges();

                        return url;
                    }
                });
            }

            public Task<Stat> Click(string segment, string referer, string ip)
            {
                return Task.Run(() =>
                {
                    using (var context = new MyDbContext())
                    {
                        ShortUrl url = context.ShortUrls.Where(u => u.Segment == segment).FirstOrDefault();
                        if (url == null)
                        {
                            throw new TinyMeNotFoundException();
                        }

                        url.NumOfClicks = url.NumOfClicks + 1;

                        Stat stat = new Stat()
                        {
                            ClickDate = DateTime.Now,
                            Ip = ip,
                            Referer = referer,
                            ShortUrl = url
                        };

                        context.Stats.Add(stat);

                        context.SaveChanges();

                        return stat;
                    }
                });
            }

            private string NewSegment()
            {
                using (var context = new MyDbContext())
                {
                    int i = 0;
                    while (true)
                    {
                        string segment = Guid.NewGuid().ToString().Substring(0, 6);
                        if (!context.ShortUrls.Where(u => u.Segment == segment).Any())
                        {
                            return segment;
                        }
                        if (i > 30)
                        {
                            break;
                        }
                        i++;
                    }
                    return string.Empty;
                }
            }
        }
    }

