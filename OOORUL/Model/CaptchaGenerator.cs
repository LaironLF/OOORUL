using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.Model
{
    internal class CaptchaGenerator
    {
        private static readonly string captchaSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        Random random = new Random();

        public string captchaText;
        public Bitmap captchaImage;
        Brush[] colors = { 
            Brushes.Black,
            Brushes.Red,
            Brushes.Blue,
            Brushes.Green 
        };



        public void Generate(int width, int height)
        {
            GenerateRandomString();
            GenerateBitmapCaptcha(width, height);
        }

        private void GenerateBitmapCaptcha(int Width, int Height)
        {
            // | Создаём битмап
            captchaImage = new Bitmap(Width, Height);

            // | Генерируем ужаснейшие координаты текста
            int Xpos = random.Next(0, Width - Width * 3/4);
            int Ypos = random.Next(0, Height - Height*3/4);
            var captchaCords = new PointF(Xpos, Ypos);

            // | Подготовим ужаснейшие параметры текста
            var CaptchaFont = new Font("Times New Roman", 20, FontStyle.Italic);
            var CaptchaColor = colors[random.Next(colors.Length)];
            

            // | НАЧИНАЕМ РИСОВАТЬ УЖАСНЕЙШУЮ КАПТЧУ УХАХАХАХАХ

            Graphics g = Graphics.FromImage((Image)captchaImage);   // | Создаём перо - инструмент с которого начинаются страдания людей
            g.Clear(Color.DarkGray);                                // | Закрашиваем фон, чтобы людям жизнь мёдом не казалась
            g.DrawString(                                           // | Начинаем рисовать каптчу, с которой начнутся страдания людей
                captchaText,                                        
                CaptchaFont,
                CaptchaColor,
                captchaCords);
            g.DrawLine(Pens.Black, new Point(0, Height/2), new Point(Width-1, Height/2-1)); // | добавим линию, а то чёт ещё слишком просто

            // | О, и не забудем ещё навалить белых точек, аля шумы,
            // | тогда вообще все офигеют от жизни
            
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (random.Next() % 20 == 0)
                        captchaImage.SetPixel(i, j, Color.White);

        }

        private void GenerateRandomString()
        {
            int lenght = random.Next(4, 10);
            captchaText = "";
            for (int i = 0; i < lenght; i++)
                captchaText += captchaSymbols[random.Next(0, captchaSymbols.Length)];
        }
    }
}
