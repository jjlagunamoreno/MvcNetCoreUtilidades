using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        //private IWebHostEnvironment hostEnvironment;
        private HelperPathProvider helperPath;

        public UploadFilesController(HelperPathProvider helperPath)
        {
            this.helperPath = helperPath;
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            // 🔥 VALIDACIÓN: COMPROBAR SI SE HA SUBIDO UN ARCHIVO 🔥
            if (fichero == null || fichero.Length == 0)
            {
                ViewData["MENSAJE"] = "⚠️ Debes seleccionar un archivo antes de subir.";
                return View();
            }

            string fileName = fichero.FileName;

            // OBTENER LA RUTA COMPLETA DONDE SE GUARDARÁ EL ARCHIVO
            string path = this.helperPath.MapPath(fileName, Folders.Images);

            string Urlpath = 
                this.helperPath.MapUrlPath(fileName, Folders.Images);

            // 🔥 VERIFICAR SI LA CARPETA EXISTE, SI NO, CREARLA 🔥
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // PARA SUBIR EL FICHERO SE UTILIZA Stream CON IFormFile
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }

            ViewData["MENSAJE"] = "✅ Fichero subido correctamente a " + path;
            ViewData["URL"] = Urlpath;
            return View();
        }
    }
}
