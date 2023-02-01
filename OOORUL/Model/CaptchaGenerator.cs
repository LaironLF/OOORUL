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

            // | Подготовим ужаснейшие параметры текста
            var CaptchaFont = new Font("Times New Roman", 20, FontStyle.Italic);
            var CaptchaColor = colors[random.Next(colors.Length)];

            // | Начинаем рисовать каптчу
            Graphics g = Graphics.FromImage((Image)captchaImage);
            g.Clear(Color.DarkGray);


            // | Рисуем каждый символ отдельно
            for(int i = 0; i < captchaText.Length; i++)
            {
                int Xpos = i * (Width / captchaText.Length);
                int Ypos = random.Next(0, Height - Height * 3 / 4);
                var captchaCords = new Point(Xpos, Ypos);
                g.DrawString(
                    Convert.ToString(captchaText[i]),
                    CaptchaFont,
                    CaptchaColor,
                    captchaCords);
            }

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
