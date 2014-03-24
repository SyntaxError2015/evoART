using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoManipulator.UnitTests
{
    //[TestClass]
    public class PhotomanipTests
    {
        private static readonly string FILE = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\test2.jpg";
        //private static readonly string FILE2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\tes3.jpg";
        //private static readonly string FILE3 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\tes5.jpg";
        private static readonly string RESIZED = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\resized.JPG";
        private static readonly string THUMB = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\thumb.JPG";
        private static readonly string HEX = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\hex.png";
        private static readonly string WTMKTXT = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\wtmktxt.jpg";
        private static readonly string WTMKIMG = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\wtmkimg.jpg";

        [TestMethod]
        public void ResizeImage()
        {
            var resizedImage = ImageResizer.ResizeImage(Image.FromFile(FILE));

            if (File.Exists(RESIZED))
                File.Delete(RESIZED);

            resizedImage.Save(RESIZED, ImageFormat.Jpeg);
        }

        [TestMethod]
        public void CreateThumbnail()
        {
            var thumbnail = ImageResizer.CreateThumbnail(Image.FromFile(FILE));

            if (File.Exists(THUMB))
                File.Delete(THUMB);

            thumbnail.Save(THUMB, ImageFormat.Jpeg);
        }

        [TestMethod]
        public void CreateHexThumbnail()
        {
            var hex = ImageResizer.CreateHexagonImage(Image.FromFile(FILE));

            if (File.Exists(HEX))
                File.Delete(HEX);

            hex.Save(HEX, ImageFormat.Png);
        }

        [TestMethod]
        public void PlaceTextWatermark()
        {
            const string text = "Copyright @ 2014 - Codoban Claudiu";

            var img = WatermarkGenerator.AddStringWatermark(Image.FromFile(RESIZED), text);

            if (File.Exists(WTMKTXT))
                File.Delete(WTMKTXT);

            img.Save(WTMKTXT, ImageFormat.Jpeg);
        }

        [TestMethod]
        public void PlaceImageWatermark()
        {
            var wtmk = Image.FromFile(HEX);

            var img = WatermarkGenerator.AddImageWatermark(Image.FromFile(FILE), wtmk);

            if (File.Exists(WTMKIMG))
                File.Delete(WTMKIMG);

            img.Save(WTMKIMG, ImageFormat.Jpeg);
        }
    }
}
