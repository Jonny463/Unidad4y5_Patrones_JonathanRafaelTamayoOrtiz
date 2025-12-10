using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoV7.Capa_Datos
{
    public class ProxyRobot : IRobot
    {
        private Robot robotRemoto;

        public int IdRobot { get; set; }
        public TareaRobot Tarea { get; set; }
        public bool EstatusActividad { get; set; } //El robot esta activo (true) o inactivo (false)

        private void ContactoRobotRemoto() //Conexion con el robot remoto
        {
            if (robotRemoto == null)
            {
                robotRemoto = new Robot(); //Nueva conexion
                robotRemoto.IdRobot = this.IdRobot;
            }
        }

        public TareaRobot GetTareaRobot() //Devuelve la tarea que esta realizando un robot
        {
            ContactoRobotRemoto();
            Tarea = robotRemoto.GetTareaRobot();
            return Tarea;
        }
        public bool getEstatusActividad() //Devuelve un valor booleano
        {
            ContactoRobotRemoto();
            EstatusActividad = robotRemoto.getEstatusActividad();
            return EstatusActividad;
        }
        public string isActivo() //Devuelve un valor string correspondiente a su estatus
        {
            ContactoRobotRemoto();
            return robotRemoto.isActivo();
        }
        public void AsignarTarea(int tipo)
        {
            ContactoRobotRemoto();
            robotRemoto.AsignarTarea(tipo);
            Tarea = robotRemoto.Tarea;
        }
        public void AsignarEstatus(bool estatus)
        {
            ContactoRobotRemoto();
            robotRemoto.AsignarEstatus(estatus);
            EstatusActividad = estatus;
        }
        //Metodos memento
        public MementoRobot CrearMemento()
        {
            ContactoRobotRemoto();
            return robotRemoto.CrearMemento();
        }
        public void RestaurarMemento(MementoRobot memento)
        {
            ContactoRobotRemoto();
            robotRemoto.RestaurarMemento(memento);
            Tarea = robotRemoto.Tarea;
            EstatusActividad = robotRemoto.EstatusActividad;
        }
    }
}
