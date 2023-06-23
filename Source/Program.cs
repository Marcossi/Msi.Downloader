/*
Codigo generado por el señor:

Necesito hacer una aplicacion de consola en .net para windows lo mas pequeña y reducida posible. Quiero una aplicacion de consola.
Al ejecutarla me liste todos los ficheros con extension .urls que haya en el mismo directorio y me pida que seleccione uno, asignandole un numero a cada fichero.
Estos ficheros son de texto plano que solo contiene una lista de URLs, una url en cada linea.
La aplicación leera el fichero seleccionado, y descargará las urls en una carpeta que tiene el mismo nombre del fichero (quitandole la extension .urls)
Evita poner comentarios de código. Quiero que durante la descarga se muestren mensajes indicando las urls descargadas
*/

using System;
using System.IO;
using System.Net;

namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.urls");

        if (files.Length == 0)
        {
            Console.WriteLine("No se encontraron archivos con la extensión .urls en el directorio actual.");
            return;
        }

        Console.WriteLine("Archivos encontrados:");

        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
        }

        Console.Write("Seleccione un archivo (ingrese el número correspondiente): ");
        string input = Console.ReadLine();
        int selectedIndex;

        if (!int.TryParse(input, out selectedIndex) || selectedIndex < 1 || selectedIndex > files.Length)
        {
            Console.WriteLine("Selección inválida.");
            return;
        }

        string selectedFile = files[selectedIndex - 1];
        string outputFolder = Path.GetFileNameWithoutExtension(selectedFile);

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        Console.WriteLine($"Descargando URLs del archivo {selectedFile}...");

        string[] urls = File.ReadAllLines(selectedFile);

        using (WebClient client = new WebClient())
        {
            for (int i = 0; i < urls.Length; i++)
            {
                string url = urls[i];
                string fileName = Path.GetFileName(url);
                string filePath = Path.Combine(outputFolder, fileName);

                try
                {
                    client.DownloadFile(url, filePath);
                    Console.WriteLine($"Descargada URL {url}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al descargar URL {url}: {ex.Message}");
                }
            }
        }

        Console.WriteLine("Descarga completa.");
    }
}
