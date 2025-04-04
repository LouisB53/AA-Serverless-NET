using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace ResizeFunction
{
    public static class ResizeHttpTrigger
    {
        [FunctionName("ResizeHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Request body length: {req.Body.Length}");
            log.LogInformation("Resize function triggered.");

            try
            {
                // Récupérer les paramètres de requête
                if (!int.TryParse(req.Query["w"], out int width) ||
                    !int.TryParse(req.Query["h"], out int height))
                {
                    return new BadRequestObjectResult("Missing or invalid 'w' or 'h' parameters.");
                }

                // Lire le corps de la requête (image)
                using var inputStream = new MemoryStream();
                await req.Body.CopyToAsync(inputStream);
                inputStream.Position = 0;

                using var image = Image.Load(inputStream);
                image.Mutate(x => x.Resize(width, height));

                using var outputStream = new MemoryStream();
                image.Save(outputStream, new JpegEncoder());
                outputStream.Position = 0;

                return new FileContentResult(outputStream.ToArray(), "image/jpeg");
            }
            catch (Exception ex)
            {
                log.LogError($"Error processing image: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }
    }
}
