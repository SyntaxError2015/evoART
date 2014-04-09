using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace PhotoManipulator
{
    public static class ImageResizer
    {
        #region Constant fields

        private const int MINIMUM_WIDTH = 1200;
        private const int MINIMUM_HEIGHT = 1000;

        private const int THUMB_MIN_SIZE_WIDTH = 400;
        private const int THUMB_MIN_SIZE_HEIGTH = 300;

        private const int HEXAGON_MIN_SIZE_UNIT = 200;

        #endregion

        #region Private methods

        public static Image ResizeImage(Image image, Size size)
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

        private static Image ResizeForHexagonShape(Image image)
        {
            var newSize = new Size();

            if (image.Width > image.Height)
            {
                newSize.Height = HEXAGON_MIN_SIZE_UNIT;
                newSize.Width = (int)((image.Width) * ((float)newSize.Height / image.Height));
            }

            else
            {
                newSize.Width = HEXAGON_MIN_SIZE_UNIT;
                newSize.Height = (int)((image.Height) * ((float)newSize.Width / image.Width));
            }

            return ResizeImage(image, newSize);
        }

        private static PointF[] GenerateHexagonPointsForImage(Image image)
        {
            var radius = (int)((float)8 / 10 * ((image.Width > image.Height) ? image.Width / 3 : image.Height / 3));
            var polygon = new PointF[6];

            for (var i = 0; i < polygon.Length; i++)
            {
                var x = (float)(((float)image.Width / 2) + radius * Math.Cos(2 * Math.PI * i / polygon.Length));
                var y = (float)(((float)image.Height / 2) + radius * Math.Sin(2 * Math.PI * i / polygon.Length));

                polygon[i] = new PointF(x, y);
            }

            return polygon;
        }

        private static int[] GetPolygonCorners(PointF[] polygon)
        {
            var left = polygon[0].X;
            var top = polygon[0].Y;
            var right = polygon[0].X;
            var bottom = polygon[0].Y;

            for (var i = 1; i < polygon.Length; i++)
            {
                if (polygon[i].X < left)
                    left = polygon[i].X;

                if (polygon[i].X > right)
                    right = polygon[i].X;

                if (polygon[i].Y < top)
                    top = polygon[i].Y;

                if (polygon[i].Y > bottom)
                    bottom = polygon[i].Y;
            }

            return new int[] { (int)left - 2, (int)top - 2, (int)right + 2, (int)bottom + 3 };
        }

        #endregion

        #region Public methods

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
                newSize.Height = (int)((image.Height) * ((float)newSize.Width / image.Width));
            }

            return ResizeImage(image, newSize);
        }

        /// <summary>
        /// Crop an image into a hexagon shaped model, having its content start point as the center of the image
        /// </summary>
        /// <param name="image">The image to be cropped</param>
        /// <returns>An Image entity</returns>
        public static Image CreateHexagonImage(Image image)
        {
            image = ResizeForHexagonShape(image);

            var hexPoints = GenerateHexagonPointsForImage(image);
    
            var normalize = GetPolygonCorners(hexPoints);

            for (var i = 0; i < hexPoints.Length; i++)
            {
                hexPoints[i].X -= normalize[0];
                hexPoints[i].Y -= normalize[1];
            }

            var rect = new Rectangle(-normalize[0], -normalize[1], image.Width, image.Height);

            var clip = new GraphicsPath();
            clip.AddPolygon(hexPoints);

            var img = new Bitmap(normalize[2] - normalize[0], normalize[3] - normalize[1]);

            var g = Graphics.FromImage(img);

            // Preset for high quality image
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Draw foreground image into clipping region
            g.SetClip(clip, CombineMode.Replace);
            g.DrawImage(image, rect);
            g.ResetClip();

            g.Save();

            return img;
        }

        #endregion
    }
}
