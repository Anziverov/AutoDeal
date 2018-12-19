using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PhotoSauce.MagicScaler;

namespace AutoDeal.Models
{
    public static class PictureMiniatureConverter
    {
        public static void Convert(string outputPath, string imgPath, int width, int height)
        {
            using (var outStream = new FileStream(outputPath, FileMode.Create))
            {
                MagicImageProcessor.ProcessImage(imgPath,outStream, new ProcessImageSettings { Width = width, Height = height });       
            }
        }
    }
}
