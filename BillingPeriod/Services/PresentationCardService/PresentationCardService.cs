using BillingPeriod.Models;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace BillingPeriod.Services.PresentationCardService
{
    public class PresentationCardService : IPresentationCardService
    {
        public string ImagePath { get; } = "C:\\Users\\Desarrollador\\Documents\\mvc core\\BillingPeriod\\BillingPeriod\\wwwroot\\Images\\";

        public string SavePresentationCardAsImage(PresentationCard presentationCard)
        {
            try
            {
                // Verificar si el directorio de imágenes existe y crearlo si no
                if (!Directory.Exists(ImagePath))
                {
                    Directory.CreateDirectory(ImagePath);
                }

                // Crear una nueva imagen en blanco
                Bitmap cardImage = new Bitmap(600, 400);
                Graphics graphics = Graphics.FromImage(cardImage);

                // Establecer un fondo con gradiente
                Color startColor = Color.FromArgb(200, 230, 255);
                Color endColor = Color.FromArgb(150, 200, 255);
                LinearGradientBrush backgroundBrush = new LinearGradientBrush(new Rectangle(0, 0, cardImage.Width, cardImage.Height), startColor, endColor, LinearGradientMode.Vertical);
                graphics.FillRectangle(backgroundBrush, 0, 0, cardImage.Width, cardImage.Height);

                // Definir las fuentes y pinceles para el texto
                Font titleFont = new Font("Arial", 24, FontStyle.Bold);
                Font textFont = new Font("Arial", 16, FontStyle.Regular);
                SolidBrush textBrush = new SolidBrush(Color.Black);

                // Definir las posiciones para el texto
                int x = 50;
                int y = 50;
                int spacing = 30;

                // Escribir los datos de la tarjeta de presentación en la imagen
                graphics.DrawString("Name: " + presentationCard.Name, titleFont, textBrush, x, y);
                y += spacing;
                graphics.DrawString("Department: " + presentationCard.Department, textFont, textBrush, x, y);
                y += spacing;
                graphics.DrawString("Street: " + presentationCard.Street, textFont, textBrush, x, y);
                y += spacing;
                graphics.DrawString("Region: " + presentationCard.Region, textFont, textBrush, x, y);
                y += spacing;
                graphics.DrawString("Postal Code: " + presentationCard.PostalCode, textFont, textBrush, x, y);
                y += spacing;
                graphics.DrawString("City: " + presentationCard.City, textFont, textBrush, x, y);
                y += spacing;
                graphics.DrawString("State: " + presentationCard.State, textFont, textBrush, x, y);
                y += spacing;
                graphics.DrawString("Phone: " + presentationCard.Phone, textFont, textBrush, x, y);
                y += spacing;
                graphics.DrawString("Email: " + presentationCard.Email, textFont, textBrush, x, y);

                // Guardar la imagen en un archivo
                string imageName = Guid.NewGuid().ToString() + ".png";
                string imagePath = Path.Combine(ImagePath, imageName);
                cardImage.Save(imagePath, ImageFormat.Png);

                return imageName;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y registrarla si es necesario
                Console.WriteLine("Error al guardar la tarjeta de presentación como imagen: " + ex.Message);
                return null;
            }
        }
    }
}
