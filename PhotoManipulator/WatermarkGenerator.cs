using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace PhotoManipulator
{
    public class WatermarkGenerator
    {
        /// <summary>
        /// Add a string as watermark to an image
        /// </summary>
        /// <param name="image">The image in which to place watermark</param>
        /// <param name="watermarkText">The text that is to be written on the image</param>
        /// <returns>A System.Drawing.Image instance representing the image that contains the watermark</returns>
        public static Image AddStringWatermark(Image image, string watermarkText)
        {
            var result = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            var g = Graphics.FromImage(result);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            var font = new Font("Arial", (float)image.Width / 75, FontStyle.Bold);
            var size = g.MeasureString(watermarkText, font);

            var strFormat = new StringFormat { Alignment = StringAlignment.Center };

            var yPixlesFromBottom = (int)(image.Height * .05);
            var yPosFromBottom = ((image.Height -
                       yPixlesFromBottom) - (size.Height / 2));
            var xCenterOfImg = (float)image.Width / 2;

            var semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            g.DrawString(watermarkText, font, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), strFormat);

            var semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            g.DrawString(watermarkText, font, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), strFormat);
            g.Save();

            g.Dispose();

            return result;
        }

        /// <summary>
        /// Add an image as watermark to another image
        /// </summary>
        /// <param name="image">The base image</param>
        /// <param name="watermarkImage">The watermark image</param>
        /// <returns>A System.Drawing.Image instance representing the image that contains the watermark</returns>
        public static Image AddImageWatermark(Image image, Image watermarkImage)
        {
            var result = new Bitmap(image);

            var g = Graphics.FromImage(result);

            var imgAttributes = new ImageAttributes();
            var colorMap = new ColorMap
            {
                OldColor = Color.FromArgb(255, 0, 255, 0),
                NewColor = Color.FromArgb(0, 0, 0, 0)
            };

            ColorMap[] remapTable = { colorMap };

            imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = { 
                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},
                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
            };

            var wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            var xPosOfWm = ((image.Width - watermarkImage.Width) - 10);
            const int yPosOfWm = 10;

            g.DrawImage(watermarkImage, new Rectangle(xPosOfWm, yPosOfWm, watermarkImage.Width, watermarkImage.Height),
                0, 0, watermarkImage.Width, watermarkImage.Height, GraphicsUnit.Pixel, imgAttributes);
            g.Save();

            g.Dispose();

            return result;
        }
    }
}
