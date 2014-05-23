using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HgSmartControl.Client
{
    public static class Utility
    {
        private static Dictionary<string, Image> imageCache = new Dictionary<string, Image>();
        //public static void DownloadImage(string url, Action<Bitmap> callbck)
        //{
        //    WebClient client = new WebClient();
        //    client.DownloadDataAsync(new Uri(url), null);
        //}

        public static void DownloadImage(string url, NetworkCredential credential, Action<Image> callback)
        {
            if (imageCache.ContainsKey(url))
            {
                callback(imageCache[url]);
            }
            else
            {
                //Thread t = new Thread(() =>
                //{
                //    Monitor.Enter(imageCache);
                    try
                    {
                        WebRequest req = WebRequest.Create(url);
                        req.Credentials = credential;
                        Stream stream = req.GetResponse().GetResponseStream();
                        Image img = Image.FromStream(stream);
                        if (!imageCache.ContainsKey(url)) imageCache.Add(url, img);
                        callback(img);
                        stream.Close();
                    }
                    catch { }
                //    Monitor.Exit(imageCache);
                //});
                //t.Start();
            }
        }
    }
}
