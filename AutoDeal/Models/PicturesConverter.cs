using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoDeal.Models
{
    public class PicturesConverter
    {
        string imagesPath;
        public string[] ImagerContainer { get; set; }
        public PicturesConverter( string images)
        {
            imagesPath = images;
            ImagerContainer = imagesPath.Split(';');
            for (int i = 0; i < ImagerContainer.Length;i++)
            {
                ImagerContainer[i] = @"/images/Машины/" + ImagerContainer[i] + ".jpeg";
            }
        }
    }
}
