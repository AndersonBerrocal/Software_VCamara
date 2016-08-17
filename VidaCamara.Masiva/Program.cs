using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace VidaCamara.Masiva
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = string.Format("{0} - {1}", "Carga masiva de archivos y/o nominas", DateTime.Now.ToLongDateString());
            try
            {
                var pathFolder = args[0].ToString();
                if (Directory.Exists(pathFolder.ToString()))
                    listarDirectoryFiles(pathFolder);
                else
                    Console.WriteLine("No existe el direcorio especificado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR => {0} - {1}", ex.Message.ToString());
            }
            Console.WriteLine("*********** Fin de proceso ****************");
            Environment.Exit(0);
        }

        private static void listarDirectoryFiles(string pathFolder)
        {
            var fileReader = new Logica.FileReader();
            var fileCounter = 0;
            var divider = 0;
            Console.WriteLine("************** Iniciando Carga ****************");
            var listDirectoryFiles = new DirectoryInfo(pathFolder);
            Console.WriteLine("*************  Cantidad de archivos encontrados en: {0} son: {1} *******",pathFolder, listDirectoryFiles.EnumerateFiles().Count());
            fileReader.lineMessageLog.AppendLine(string.Format("******************** Inicio de carga {0} **************", DateTime.Now.ToString()));
            foreach (var file in listDirectoryFiles.GetFiles(string.Format("*.{0}", "CAM")))
            {
                var response = fileReader.loadFileAndSave(file.FullName);
                ++fileCounter;
                ++divider;
                if(divider == 200)
                {
                    writeLog(fileReader.lineMessageLog);
                    fileReader.lineMessageLog = new StringBuilder();
                    divider = 0;
                }
                Console.WriteLine("{0} numero de archivo: {1}",file.FullName,fileCounter);
            }
            fileReader.lineMessageLog.AppendLine("*************** fin de carga *****************");
            writeLog(fileReader.lineMessageLog);
        }
        private static void writeLog(StringBuilder lines) {
            try
            {
                var name = string.Format("info _{0}", DateTime.Now.ToString("yyyyMMdd"));
                var rootDirectory = ConfigurationManager.AppSettings["PathLog"].ToString();
                if (!Directory.Exists(rootDirectory))
                    Directory.CreateDirectory(rootDirectory);
                using (var file = File.AppendText(string.Format("{0}{1}.txt", rootDirectory,name )))
                {
                    file.WriteLine(lines);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
