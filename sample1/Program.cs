using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>("output.jpg"))
            {
                Console.WriteLine($"Dimensions de output.jpg : {image.Width} x {image.Height}");
            }
        }
    }
}

/*
using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "images/photo.jpg"; // chemin de l’image d’origine
            string outputPath = "images/photo_resized.jpg"; // chemin de l’image de sortie

            using (Image image = Image.Load(inputPath))
            {
                image.Mutate(x => x.Resize(100, 100)); // redimensionner en 100x100

                image.Save(outputPath); // sauvegarder
            }

            Console.WriteLine("Image redimensionnée !");
        }
    }
}
*/