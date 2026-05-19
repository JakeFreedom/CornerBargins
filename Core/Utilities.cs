using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Utilities
    {

        public static Dictionary<Core.ENUMS.ImageType, byte[]> CreateItemImages(byte[] originalImage, List<ENUMS.ImageType> imageType)
        {
            List<byte[]> imagesToReturn = new List<byte[]>();
            Dictionary<Core.ENUMS.ImageType, byte[]> i = new Dictionary<ENUMS.ImageType, byte[]>();
            foreach(ENUMS.ImageType type in imageType)
            {
                //This will control how many images we make
                switch(type)
                {
                    case ENUMS.ImageType.NORMAL: //Full Size
                        //imagesToReturn.Add(GetResizedImage(900, 1200, originalImage));
                        i.Add(ENUMS.ImageType.NORMAL, GetResizedImage(900, 1200, originalImage));
                        break;

                    case ENUMS.ImageType.HALF_SIZE: // 350x350
                         //imagesToReturn.Add(GetResizedImage(350, 350, originalImage));
                        i.Add(ENUMS.ImageType.HALF_SIZE, GetResizedImage(350,350, originalImage));
                        break;

                    case ENUMS.ImageType.THUMB_NAIL: // 200x200
                        //imagesToReturn.Add(GetResizedImage(200, 200, originalImage));
                        i.Add(ENUMS.ImageType.THUMB_NAIL, GetResizedImage(200, 200, originalImage));
                        break;
                    case ENUMS.ImageType.ICON: //64x64
                        i.Add(ENUMS.ImageType.ICON, GetResizedImage(64, 64, originalImage));
                        break;
                }
            }

            if (i.Count > 0)
                return i;
            else
                return new Dictionary<ENUMS.ImageType, byte[]>();
        }

        private static bool ThumbnailCallback() { return false; }
        public static byte[] GetResizedImage(int maxHeight, int maxWidth, byte[] originalImage)
        {
            //Image thumb = i.GetThumbnailImage(128, 128, null, IntPtr.Zero);
            //thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            MemoryStream ms = new MemoryStream(originalImage);
            Image i = System.Drawing.Image.FromStream(ms);

            double aspectRatio = (double)i.Width / i.Height;
            int _height = maxHeight;
            int _width = maxWidth;
            Bitmap b = null;
            if (aspectRatio > 1)
                _height = (int)(_width / aspectRatio);
            else
                _width = (int)(_height * aspectRatio);

            b = new Bitmap(i, _width, _height);


            Image myThumbnail = b.GetThumbnailImage(_width, _height, myCallback, IntPtr.Zero);
            //myThumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);


            myThumbnail.Save($"C:\\pwp\\temp.png");

            //return ms.ToArray();
            return File.ReadAllBytes($"C:\\pwp\\temp.png");
        }
    }
}
    