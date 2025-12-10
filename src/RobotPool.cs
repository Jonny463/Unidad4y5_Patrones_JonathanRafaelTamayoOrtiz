using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoV7.Capa_Datos;

namespace ProyectoV7.Capa_Persistencia
{
    public class RobotPool<T> where T : class, IRobot, new()
    {
        //Singleton
        private static RobotPool<T> _instance; //Una sola instancia de la piscina
        private static readonly object _lock = new object();
        public string IdCoordinador { get; set; }

        //Object pool
        public readonly Stack<T> RobotsPool = new Stack<T>(); //Stack de todos los robots a la espera de instrucciones
        public readonly List<T> RobotsActivos = new List<T>(); //Lista de robots en uso

        //Memento
        public readonly Stack<MementoRobot> _historial = new Stack<MementoRobot>(); // Historial

        private RobotPool(int maxPool)
        {
            IdCoordinador = "12345";
            //Instanciar los robots pertenecientes al pool
            for (int i = 1; i <= maxPool; i++)
            {
                var robot = new T();
                robot.IdRobot = i;
                robot.AsignarEstatus(false);
                robot.AsignarTarea(0);

                RobotsPool.Push(robot);
            }
        }
        public static RobotPool<T> Instance // Inicializa la instancia del coordinador
        {                                   // Lo cual inicializa la piscina de objetos
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RobotPool<T>(10);
                            Console.WriteLine("Nueva instancia creada");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Instancia existente devuelta");
                }
                return _instance;
            }
        }
        public void VerificarCoordinador()
        {
            Console.WriteLine($"Coordinador ID: {IdCoordinador}");
        }
        public T ActivarRobot() //Activa un robot
        {
            if (RobotsPool.Count > 0) //Verifica que hay robots disponibles
            {
                var robot = RobotsPool.Pop();
                _historial.Push(robot.CrearMemento());

                robot.AsignarEstatus(true);
                RobotsActivos.Add(robot);
                return robot;
            }
            return null;
        }
        public void DevolverRobot(int id) //Devuelve el robot al pool
        {
            var robot = RobotsActivos.FirstOrDefault(r => r.IdRobot == id);
            if (robot == null) return;
            _historial.Push(robot.CrearMemento());
            robot.AsignarEstatus(false);
            robot.AsignarTarea(0);
            //Modifica las listas de robots
            RobotsActivos.Remove(robot);
            RobotsPool.Push(robot);
        }
        public T BuscarRobot(int id) //Busca un robot especifico
        {
            return RobotsPool.Concat(RobotsActivos).FirstOrDefault(r => r.IdRobot == id);
        }
        public int Deshacer() //Deshace la ultima accion
        {   //Devuelve el ID del robot afectado
            if (_historial.Count == 0)
            {
                Console.WriteLine("No hay historial para deshacer");
                return 0;
            }
            var memento = _historial.Pop();
            var robot = BuscarRobot(memento.IdRobot);
            if (robot == null)
            {
                Console.WriteLine($"Robot ID: {memento.IdRobot} no encontrado");
                return 0;
            }

            bool estadoActual = robot.getEstatusActividad();
            robot.RestaurarMemento(memento);
            bool estadoNuevo = robot.getEstatusActividad();

            //Modificar listas
            if (estadoActual && !estadoNuevo)
            {
                //De Activo a Inactivo
                var r = RobotsActivos.FirstOrDefault(x => x.IdRobot == robot.IdRobot);
                if (r != null)
                {
                    RobotsActivos.Remove(r);
                    RobotsPool.Push(r);
                    Console.WriteLine($"Robot ID {robot.IdRobot} desactivado");
                }
            }
            else if (!estadoActual && estadoNuevo)
            {
                //De Inactivo a Activo
                var temp = new Stack<T>();
                T encontrado = null;
                while (RobotsPool.Count > 0) //Almacena robots disponibles
                {
                    var r = RobotsPool.Pop();
                    if (r.IdRobot == robot.IdRobot && encontrado == null)
                        encontrado = r;
                    else
                        temp.Push(r);
                }
                while (temp.Count > 0) //Los regresa una vez encuentra el que busca
                    RobotsPool.Push(temp.Pop());

                if (encontrado != null)
                {
                    RobotsActivos.Add(encontrado);
                    Console.WriteLine($"Robot ID {robot.IdRobot} restaurado");
                }
            }
            return robot.IdRobot;
        }
    }
}
