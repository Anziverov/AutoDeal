using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoDeal.Models
{
    public  sealed class PicturesConverter // сделать ли класс статическим
    {
        readonly string rootPath = @"/images/Машины/";
        readonly string rootMiniaturesPath = @"/images/Машины/Miniatures/";
        string imagesPath;
        List<string> imageList; //Отдельная переменная для миниатюр
        public List<string> ImageList { get { return imageList; } set { imageList = value; } }
        public PicturesConverter()
        {

        }
        public PicturesConverter( string images) //упорядочить методы, разделить на методы, либо на принимаемые значения
        {
            imagesPath = images;
            imageList = imagesPath.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();    
            for (int i = 0; i < imageList.Count;i++)
            {
                if(imageList[i].Length != 0 || imageList[i] != "") // Костыль? 
                    imageList[i] = rootPath + imageList[i];
            }
        }
        public List<string> PicturesConverterMini(string images, int miniaturesAmount = 0)
        {
            imagesPath = images;
            imageList = imagesPath.Split(';',StringSplitOptions.RemoveEmptyEntries).ToList(); 
            for (int i = 0; i < imageList.Count; i++)
            {
                if (imageList[i].Length != 0 || imageList[i] != "") // Костыль? 
                    imageList[i] = rootMiniaturesPath + imageList[i];
            }
            return imageList;
        }
    }
}
