using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDeal.Models
{
    public sealed class PicturesOpener
    {
        string picturePath;
        public PicturesOpener(string pic)
        {
            picturePath = @"E:\Машины" + pic;
            
        }
        public string GetFile()
        {
            using (StreamReader sr = new StreamReader(picturePath))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
