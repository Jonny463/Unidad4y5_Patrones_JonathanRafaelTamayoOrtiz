using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoV7.Capa_Datos
{
    public class Robot : IRobot
    {
        public int IdRobot { get; set; }
        public TareaRobot Tarea { get; set; }
        public bool EstatusActividad { get; set; } //El robot esta activo (true) o inactivo (false)

        public TareaRobot GetTareaRobot() //Devuelve la tarea que esta realizando un robot
        {
            return Tarea;
        }
        public bool getEstatusActividad() //Devuelve un valor booleano
        {
            return EstatusActividad;
        }
        public string isActivo() //Devuelve un valor string correspondiente a su estatus
        {
            if (EstatusActividad) //activo = (true) e inactivo = (false)
            {
                return "Activo";
            }
            return "Inactivo";
        }
        public void AsignarTarea(int tipo)
        {
            if (EstatusActividad) // Si el robot esta acivo, asigna la tarea que corresponde
            {
                Tarea = (TareaRobot)tipo;
            }
            else // Si el robot esta inactivo, lo marca como 0
            {
                Tarea = TareaRobot.Inactivo;
            }
        }
        public void AsignarEstatus(bool estatus)
        {
            EstatusActividad = estatus;
            if (!estatus)
            {
                Tarea = TareaRobot.Inactivo;
            }
        }
        //Metodos memento
        public MementoRobot CrearMemento()
        {
            return new MementoRobot(IdRobot, EstatusActividad, Tarea);
        }

        public void RestaurarMemento(MementoRobot memento)
        {
            if (memento.IdRobot != this.IdRobot) return; //Verificar que el memento corresponde a este robot

            this.EstatusActividad = memento.EstatusActividad;
            this.Tarea = memento.Tarea;
        }
    }
}
