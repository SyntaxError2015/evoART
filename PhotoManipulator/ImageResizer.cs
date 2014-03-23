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

        private const int THUMB_MIN_SIZE_WIDTH = 400;
        private const int THUMB_MIN_SIZE_HEIGTH = 400;

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
                newSize.Height = (int)((image.Height) * ((float)newSize.Width / image.Width));
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
                newSize.Height = THUMB_MIN_SIZE_HEIGTH;
                newSize.Width = (int)((image.Width) * ((float)newSize.Height / image.Height));
            }

            else
            {
                newSize.Width = THUMB_MIN_SIZE_WIDTH;
                newSize.Height = (image.Height) * (newSize.Width / image.Width);
            }

            return ResizeImage(image, newSize);
        }

        public static Image CreateHexagonShapedImage(Bitmap image)
        {
            var img = new Bitmap(radius + 100, radius + 100);
            
            var g = Graphics.FromImage(img);
            var points = GenerateHexagonPointsForImage(image);

            g.DrawPolygon(new Pen(Color.Pink), points);
            g.DrawPolygon(new Pen(Color.Transparent), new PointF[] {new PointF(image.Width, 0), points[0], points[1]});

            image = img;

            return image;
        }

        private const int radius = 300;

        private static PointF[] GenerateHexagonPointsForImage(Image image)
        {
            var polygon = new PointF[6];

            for (var i = 0; i < polygon.Length; i++)
            {
                var x = (float) (((float)image.Width / 2) + radius * Math.Cos(2 * Math.PI * i / polygon.Length));
                var y = (float) (((float)image.Height / 2) + radius * Math.Sin(2 * Math.PI * i / polygon.Length));

                polygon[i] = new PointF(x, y);
            }

            return polygon;
        }

    }
}
