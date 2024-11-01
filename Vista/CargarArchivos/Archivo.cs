using System.IO;

namespace Vista.CargarArchivos
{
    public class Archivos
    {
        public static string UploadFile(IFormFile file, int id, string carpeta)
        {
            string basePath = $"/Imagenes/{carpeta}/{id}";
            string path = CrearRuta(basePath);
            string fileName = CrearNombre(file, path);
            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return $"{basePath}/{fileName}";
        }

        private static string CrearRuta(string basePath)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
            return path;
        }

        private static string CrearNombre(IFormFile file, string path)
        {
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;
            return fileName;
        }

        public static void BorrarImagen(int id)
        {
            string basePath = $"/Imagenes/Serie/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            Directory.Delete(path, true);
        }
    }
}
