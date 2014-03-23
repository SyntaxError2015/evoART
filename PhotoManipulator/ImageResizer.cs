using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace PhotoManipulator
{
    public static class ImageResizer
    {
        private const int MINIMUM_WIDTH = 1200;
        private const int MINIMUM_HEIGHT = 1000;
        private const int THUMB_MIN_SIZE = 400;

        private static Image ResizeImage(Image image, Size size)
        {
            if (image.Width < size.Width || image.Height < size.Height)
                return image;

            var bmpPhoto = new Bitmap(size.Width, size.Height, PixelFormat.Format24bppRgb);

            var g = Graphics.FromImage(bmpPhoto);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var rect = new Rectangle(0, 0, size.Width, size.Height);
            
            g.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            //g.FillPolygon(new SolidBrush(Color.Red), HexagonPoints(image), FillMode.Winding);

            image = bmpPhoto;

            g.Dispose();

            return image;
        }

        /// <summary>
        /// Resize an image in order to reduce it's size in MB
        /// </summary>
        /// <param name="image">The Image entity to be resized</param>
        /// <returns>An Image entity</returns>
        public static Image ResizeImage(Image image)
        {
            var newSize = new Size();

            if (image.Width > image.Height)
            {
                
                newSize.Width = MINIMUM_WIDTH;
                newSize.Height = (int) ((image.Height)*((float) newSize.Width/image.Width));
            }

            else
            {
                newSize.Height = MINIMUM_HEIGHT;
                newSize.Width = (int)((image.Width) * ((float)newSize.Height / image.Height));
            }

            return ResizeImage(image, newSize);
        }

        /// <summary>
        /// Create a thumbnail for an image
        /// </summary>
        /// <param name="image">The Image entity to be resized</param>
        /// <returns>An Image entity</returns>
        public static Image CreateThumbnail(Image image)
        {
            var newSize = new Size();

            if (image.Width > image.Height)
            {
                newSize.Height = THUMB_MIN_SIZE;
                newSize.Width = (int)((image.Width) * ((float)newSize.Height / image.Height));
            }

            else
            {
                newSize.Width = THUMB_MIN_SIZE;
                newSize.Height = (image.Height) * (newSize.Width / image.Width);
            }

            return ResizeImage(image, new Size(300, 200));
        }

        public static Image CreateHexagonShapedImage(Image image)
        {
            return null; //return HexagonPoints(image);
        }

        private static Point[] HexagonPoints(Image image)
        {
            var points = new Point[6];

            var half = image.Height/2;
            var quart = image.Width/4;

            points[0] = new Point(quart, 0);
            points[1] = new Point(image.Width - quart, 0);
            points[2] = new Point(image.Width, half);
            points[3] = new Point(image.Width - quart, image.Height);
            points[4] = new Point(quart, image.Height);
            points[5] = new Point(0, half);

            return points;
        }
    }
}
