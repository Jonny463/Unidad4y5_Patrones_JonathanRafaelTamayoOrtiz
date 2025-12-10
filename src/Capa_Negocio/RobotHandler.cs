using ProyectoV7.Capa_Datos;
using ProyectoV7.Capa_Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoV7.Capa_Negocio
{
    public class RobotHandler
    {
        private readonly RobotPool<ProxyRobot> pool;
        public RobotHandler()
        {
            pool = RobotPool<ProxyRobot>.Instance;
        }

        public ProxyRobot ActivarRobot(int tarea)
        {
            var robot = pool.ActivarRobot(); //Crea memento
            if (robot == null) return null;

            robot.AsignarTarea(tarea); //No crea memento
            return robot;
        }
        public void CambiarTarea(int id, int tarea)
        {
            var robot = pool.BuscarRobot(id);
            if (robot == null) return;

            pool._historial.Push(robot.CrearMemento()); //Crea memento
            robot.AsignarTarea(tarea); //No crea memento
            Console.WriteLine($"Tarea de la unidad {robot.IdRobot}: {robot.GetTareaRobot()}");
        }
        public void DevolverRobot(int id)
        {
            pool.DevolverRobot(id); //Crea memento
        }
        public void DeshacerUltimaModificacion()
        {
            var id = pool.Deshacer();
            if (id == 0) //Error al deshacer
            {
                Console.WriteLine("La operacion no de pudo deshacer");
                return;
            }
            var robot = pool.BuscarRobot(id);
            Console.WriteLine($"Operación deshecha para robot ID {robot.IdRobot}");
            ReporteRobot(id);
        }
        public ProxyRobot Buscar(int id)
        {
            return pool.BuscarRobot(id);
        }
        public void VerificarCoordinador()
        {
            pool.VerificarCoordinador();
        }
        public void ListaRobotsEnUso()
        {
            Console.Write("Lista de robots en uso: ");
            foreach (var robot in pool.RobotsActivos)
            {
                Console.Write(robot.IdRobot + "\t");
            }
        }
        public void ReporteGeneral()
        {
            Console.WriteLine("\nEstatus del grupo de robots:\n" +
                $"Robots activos: {pool.RobotsActivos.Count}\n" +
                $"Robots disponibles: {pool.RobotsPool.Count}");

            if (pool.RobotsActivos.Count > 0)
                ListaRobotsEnUso();
        }
        public void ReporteRobot(int id)
        {
            var robot = Buscar(id);
            Console.WriteLine($"\nRobot ID: {robot.IdRobot}\nTarea: {robot.GetTareaRobot()}\nEstatus: {robot.isActivo()}");
        }
        public int RobotsDisponibles()
        {
            return pool.RobotsPool.Count;
        }
    }
}
