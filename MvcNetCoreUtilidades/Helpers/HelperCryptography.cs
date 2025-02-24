using System.Security.Cryptography;
using System.Text;

namespace MvcNetCoreUtilidades.Helpers
{
    public class HelperCryptography
    {
        //TENDREMOS UNA PROPIEDAD PARA ALMACENAR EL 
        //SALT QUE HEMOS CREADO DINAMICAMENTE
        public static string Salt { get; set; }

        //CADA VEZ QUE REALIZAMOS UN CIFRADO, GENERAMOS UN SALT DISTINTO
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 30; i++)
            {
                //GENERAMOS UN NUMERO ALEATORIO CON CODIGOS ASCII
                int aleat = random.Next(1,255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        //CREAMOS UN METODO PARA CIFRAR DE FORMA EFICIENTE
        public static string CifrarContenido 
            (string contenido, bool comparar)
        {
            if (comparar == false)
            {
                //CREAMOS UN NUEVO SALT PARA EL CIFRADO
                //Y LO ALMACENAMOS EN LA PROPIEDAD
                Salt = GenerateSalt();
            }
            //EL SALT LO PODEMOS UTILIZAR EN MÚLTIPLES LUGARES
            //AL INICIO, FINAL, CON INSERT
            string contenidoSalt = contenido + Salt;
            //CREAMOS UN OBJETO GRANDE PARA CIFRAR
            SHA256 managed = SHA256.Create();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenidoSalt);
            //CIFRAMOS EL CONTENIDO CON n ITERACIONES
            for (int i=1; i<=22;i++)
            {
                //REALIZAMOS EL CIFRADO SOBRE EL CIFRADO
                salida = managed.ComputeHash(salida);
            }
            //DEBEMOS LIBERAR LA MEMORIA
            managed.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        //COMENZAMOS CREANDO UN METODO static PARA CIFRAR UN 
        //CONTENIDO.  SIMPLEMENTE DEVOLVEMOS EL TEXTO CIFRADO
        public static string EncriptarTextoBasico(string contenido)
        {
        
            //NECESITAMOS UN ARRAY DE BYTES PARA CONVERTIR 
            //EL CONTENIDO DE ENTRADA A byte[]
            byte[] entrada;
            //AL CIFRAR EL CONTENIDO, NOS DEVUELVE BYTES[] DE SALIDA
            byte[] salida;
            //NECESITAMOS UNA CLASE QUE NOS PERMITE CONVERTIR DE 
            //STRING A BYTE[] Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS UN OBJETO PARA CIFRAR EL CONTENIDO
            SHA1 managed = SHA1.Create();
            //CONVERTIMOS EL CONTENIDO DE ENTRADA A byte[]
            entrada = encoding.GetBytes(contenido);
            //LOS OBJETOS PARA CIFRAR CONTIENEN UN METODO LLAMADO 
            //ComputedHash QUE RECIBEN UN ARRAY DE BYTES E 
            //INTERNAMENTE HACEN COSAS Y DEVUELVE OTRO ARRAY DE BYTES
            salida = managed.ComputeHash(entrada);
            //CONVERTIMOS SALIDA A STRING
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
