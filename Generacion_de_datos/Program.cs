using System;
using System.IO;

namespace Generacion_de_datos
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Solicitar número de nodos
                Console.Write("Ingrese el número de nodos: ");
                int numNodos = int.Parse(Console.ReadLine());

                //OPCIÓN 1
                // Determinar el número de servidores (5% de los nodos)
                //int numHubs = (int)Math.Ceiling(numNodos * 0.05);

                //OPCIÓN 2
                Console.Write("Ingrese el número de servidores: ");
                int numHubs = int.Parse(Console.ReadLine());

                // Solicitar capacidad
                Console.Write("Ingrese la capacidad: ");
                int capacidad = int.Parse(Console.ReadLine());

                // Solicitar tipo de demanda
                Console.WriteLine("Seleccione el nivel de demanda:");
                Console.WriteLine("1. Demanda baja (1-40)");
                Console.WriteLine("2. Demanda media (41-80)");
                Console.WriteLine("3. Demanda alta (81-120)");
                Console.Write("Ingrese su elección (1/2/3): ");
                int opcionDemanda = int.Parse(Console.ReadLine());

                int demandaMin = 0, demandaMax = 0;
                //ancho y alto 547; 521
                // Determinar rango de demanda según la opción seleccionada
                switch (opcionDemanda)
                {
                    case 1:
                        demandaMin = 1;
                        demandaMax = 40;
                        break;
                    case 2:
                        demandaMin = 41;
                        demandaMax = 80;
                        break;
                    case 3:
                        demandaMin = 81;
                        demandaMax = 120;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Terminando programa.");
                        return;
                }

                // Generar dataset
                Random random = new Random();
                string dataset = $"{numNodos} {numHubs} {capacidad}\n";

                for (int i = 1; i <= numNodos; i++)
                {
                    int posicionX = random.Next(0, 546); // Coordenadas entre 0 y 100
                    int posicionY = random.Next(0, 521);
                    int demanda = random.Next(demandaMin, demandaMax + 1);

                    dataset += $"{i} {posicionX} {posicionY} {demanda}\n";
                }

                // Obtener ruta de la carpeta de Descargas
                string carpetaDescargas = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

                // Formatear el nombre del archivo según los datos
                string nombreArchivo = $"Phub_{(numNodos >= 1000 ? (numNodos / 1000) + "K" : numNodos.ToString())}_{numHubs}";
                string rutaArchivo = Path.Combine(carpetaDescargas, nombreArchivo + ".txt");

                // Guardar el dataset en un archivo txt
                File.WriteAllText(rutaArchivo, dataset);

                Console.WriteLine($"Dataset generado exitosamente en: {rutaArchivo}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
        }
    }
}
