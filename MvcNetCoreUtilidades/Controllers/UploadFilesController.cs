using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private IWebHostEnvironment hostEnvironment;

        public UploadFilesController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            SubirFichero(IFormFile fichero)
        {
            //NECESITAMOS LA RUTA A NUESTRO WWWROOT DEL SERVER
            string rootFolder =
                this.hostEnvironment.WebRootPath;
            //COMENZAMOS ALMACENANDO EL FICHERO EN LOS 
            //ELEMENTOS TEMPORALES
            string fileName = fichero.FileName;
            //LAS RUTAS DE FICHEROS NO DEBO ESCRIBIRLAS, TENGO QUE GENERAR
            //DICHAS RUTAS CON EL SISTEMA DONDE ESTOY TRABAJANDO
            string path = Path.Combine(rootFolder, "uploads", fileName);
            //PARA SUBIR EL FICHERO SE UTILIZA Stream CON IFormFile
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            return View();
        }
    }
}
