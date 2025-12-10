using ProyectoV7.Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoV7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string opc = "";
            int id = 0;

            do
            {   //Intenta crear una nuava instancia con cada ciclo. Singleton devuelve la ya existente
                var coordinador = new RobotHandler();

                Console.WriteLine("===Menu principal===\n");
                coordinador.VerificarCoordinador();
                coordinador.ReporteGeneral();
                Console.WriteLine("\n\nEliga una opcion:");
                Console.WriteLine("1.- Activar un robot");
                Console.WriteLine("2.- Desactivar un robot");
                Console.WriteLine("3.- Cambiar la tarea de un robot");
                Console.WriteLine("4.- Ver informe de un robot");
                Console.WriteLine("5.- Deshacer la ultima operacion");
                Console.WriteLine("0.- Salir\n");
                Console.Write("Opcion: ");
                opc = Console.ReadLine();

                switch (opc)
                {
                    case "1": //Activar robot
                        if (coordinador.RobotsDisponibles() == 0)
                        {
                            Console.WriteLine("No es posible activar un robot en este momento");
                            break;
                        }
                        Console.WriteLine("\nSeleccione una nueva tarea:\n" +
                                $"1.- Limpieza\n2.- Vigilancia\n3.- Paqueteria\n4.- Terminator");
                        if (int.TryParse(Console.ReadLine(), out int tarea) && (tarea >= 0 && tarea <= 4))
                        {
                            var r1 = coordinador.ActivarRobot(tarea);
                            if (r1 != null)
                                Console.WriteLine($"Robot {r1.IdRobot} activado");
                        }
                        else
                            Console.WriteLine("Esa opcion no es valida");
                        break;

                    case "2": //Devolver robot
                        Console.WriteLine("Ingrese el ID del robot que desea devolver:");
                        int.TryParse(Console.ReadLine(), out id);
                        var r2 = coordinador.Buscar(id);
                        if (r2 == null)
                        {
                            Console.WriteLine("No se pudo encontrar al robot");
                            break;
                        }
                        coordinador.DevolverRobot(id);
                        Console.WriteLine($"Robot {r2.IdRobot} devuelto a la piscina");
                        break;

                    case "3": //Cambiar la tarea de un robot
                        Console.WriteLine("Ingrese el ID del robot al que desea asignar una nueva tarea:");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("\nSeleccione una nueva tarea:\n" +
                                $"1.- Limpieza\n2.- Vigilancia\n3.- Paqueteria\n4.- Terminator");
                        var r3 = coordinador.Buscar(id);
                        if (r3 == null)
                        {
                            Console.WriteLine("No se pudo encontrar al robot");
                            break;
                        }
                        if (int.TryParse(Console.ReadLine(), out int nuevaTarea) && (nuevaTarea >= 0 && nuevaTarea <= 4))
                        {
                            coordinador.CambiarTarea(id, nuevaTarea);
                        }
                        else
                            Console.WriteLine("Esa opcion no es valida");
                        break;

                    case "4": //Muestra el estado de un solo robot
                        Console.WriteLine("Ingrese el ID del robot al que desea visualizar:");
                        int.TryParse(Console.ReadLine(), out id);
                        var r4 = coordinador.Buscar(id);
                        if (r4 == null)
                        {
                            Console.WriteLine("No se pudo encontrar al robot");
                            break;
                        }
                        coordinador.ReporteRobot(r4.IdRobot);
                        break;

                    case "5": //Deshacer la ultima operacion
                        coordinador.DeshacerUltimaModificacion();
                        break;

                    case "0": //Salir
                        Console.WriteLine("Saliendo. Presione cualquier tecla para continuar...");
                        break;

                    default:
                        Console.WriteLine("Esa opcion no es valida.");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            } while (opc != "0");
        }
    }
}