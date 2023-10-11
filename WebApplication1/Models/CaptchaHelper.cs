using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public static class CaptchaHelper
    {
        public static byte[] ConvertToByteArray(this Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
                return stream.ToArray();
            }
        }

        public static Bitmap GetCaptchaImage(out string captchaText)
        {
            Bitmap objBitmap = new Bitmap(130, 50);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(Color.White);
            Random objRandom = new Random();
            objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
            objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
            objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
            Brush objBrush =
                default(Brush);
            //create background style  
            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
            };
            //create rectangular area  
            RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
            objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
            objGraphics.FillRectangle(objBrush, oRectangleF);
            //Generate the image for captcha  

            //    captchaText = string.Format("{0:X}", objRandom.Next(1000000, 9999999));

            captchaText = GetRandomAlphaNumeric();

            //add the captcha value in session  
            //Session["CaptchaVerify"] = captchaText.ToLower();
            Font objFont = new Font("Courier New", 15, FontStyle.Bold);
            //Draw the image for captcha  

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            objGraphics.MultiplyTransform(new Matrix(1, 0, -1, 1, 20, 40));
            objGraphics.RotateTransform(-10);

            objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 0, drawFormat);

            //objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20);
            return objBitmap;
        }


        public static string GetRandomAlphaNumeric()
        {
            Random random = new Random();
            var chars = "A1BC2DE3FGH4IJKL5MNOPQ6RSTUV7WX7Y9Z0123456789";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(6).ToArray());

        

        }
    }
}