using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class CaptchaCall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Bitmap objBitmap = new Bitmap(130, 80);
        Graphics objGraphics = Graphics.FromImage(objBitmap);
        objGraphics.Clear(Color.White);
        Random objRandom = new Random();
        objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
        Brush objBrush =
            default(Brush);
        HatchStyle[] aHatchStyles = new HatchStyle[]  
            {  
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,  
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,  
                    HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal  
            };
        RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
        string captchaText = string.Format("{0:0}", objRandom.Next(1000, 9999));
        Session["ChkCaptcha"] = captchaText.ToLower();
        Font objFont = new Font("Arial", 25, FontStyle.Italic);
        objGraphics.DrawString(captchaText, objFont, Brushes.Red, 20, 20);
        objBitmap.Save(Response.OutputStream, ImageFormat.Gif);
    }
}