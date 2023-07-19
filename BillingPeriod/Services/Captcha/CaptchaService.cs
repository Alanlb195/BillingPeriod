using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace BillingPeriod.Services.Captcha
{
    public class CaptchaService : ICaptchaService
    {
        public string GenerateCaptchaText(int length, bool useLetters)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";

            var chars = useLetters ? letters : numbers;
            var random = new Random();
            var captcha = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                captcha.Append(chars[random.Next(chars.Length)]);
            }

            return captcha.ToString();
        }

        public byte[] GenerateCaptchaImage(string captchaText)
        {
            using var bitmap = new Bitmap(150, 50);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);


            // Dibuja el texto del CAPTCHA en la imagen
            using var font = new Font(FontFamily.GenericMonospace, 24, FontStyle.Bold);
            graphics.DrawString(captchaText, font, Brushes.Black, new PointF(10, 10));

            // Dibuja líneas curvas de colores aleatorios alrededor del texto
            for (int i = 0; i < 10; i++)
            {
                Color randomColor = GetRandomColor();
                using var pen = new Pen(Color.FromArgb(128, randomColor), 3);

                int x1 = RandomNumber(0, bitmap.Width);
                int y1 = RandomNumber(0, bitmap.Height);
                int x2 = RandomNumber(0, bitmap.Width);
                int y2 = RandomNumber(0, bitmap.Height);
                graphics.DrawBezier(pen, x1, y1, x1 + 30, y1 + 30, x2 - 30, y2 - 30, x2, y2);
            }


            // Genera la imagen en formato PNG
            using var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private Color GetRandomColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

    }
}
