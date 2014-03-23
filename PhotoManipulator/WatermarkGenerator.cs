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
        public static Image AddStringWatermark(Image image, string watermarkText)
        {
            var result = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            var g = Graphics.FromImage(result);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            Font font = new Font("Arial", (float)image.Width / 100, FontStyle.Bold);
            var size = g.MeasureString(watermarkText, font);

            var strFormat = new StringFormat { Alignment = StringAlignment.Center };

            var yPixlesFromBottom = (int)(image.Height * .05);
            var yPosFromBottom = ((image.Height -
                       yPixlesFromBottom) - (size.Height / 2));
            float xCenterOfImg = (float)image.Width / 2;
            
            var semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            g.DrawString(watermarkText, font, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), strFormat);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            g.DrawString(watermarkText, font, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), strFormat);
            g.Save();

            return result;
        }

        public static Image AddImageWatermark(Image image, Image watermarkImage)
        {


            return null;
        }
    }
}
