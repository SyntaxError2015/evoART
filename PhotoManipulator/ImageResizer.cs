using System.Drawing.Imaging;
using System.Drawing;

namespace PhotoManipulator
{
    public static class ImageResizer
    {
        private const int MINIMUM_WIDTH = 1200;
        private const int MINIMUM_HEIGHT = 800;
        private const int THUMB_MIN_SIZE = 400;

        private static Image ResizeImage(Image image, Size size)
        {
            if (image.Width < size.Width || image.Height < size.Height)
                return image;

            var bmpPhoto = new Bitmap(size.Width, size.Height, PixelFormat.Format24bppRgb);

            var g = Graphics.FromImage(bmpPhoto);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

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
                newSize.Height = MINIMUM_HEIGHT;
                newSize.Width = (int)((image.Width) * ((float)newSize.Height / image.Height));
            }

            else
            {
                newSize.Width = MINIMUM_WIDTH;
                newSize.Height = (image.Height) * (newSize.Width / image.Width);
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
    }
}
