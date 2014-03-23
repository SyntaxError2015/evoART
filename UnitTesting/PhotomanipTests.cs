using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoManipulator;

namespace UnitTesting
{
    [TestClass]
    public class PhotomanipTests
    {
        private const string FILE = @"C:\Users\Coddo\Desktop\test.JPG";
        private const string RESIZED = @"C:\Users\Coddo\Desktop\resized.JPG";
        private const string THUMB = @"C:\Users\Coddo\Desktop\thumb.JPG";
        private const string HEX = @"C:\Users\Coddo\Desktop\hex.png";

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
            var hex = ImageResizer.CreateThumbnail(Image.FromFile(FILE));

            if (File.Exists(HEX))
                File.Delete(HEX);

            hex.Save(HEX, ImageFormat.Png);
        }
    }
}
