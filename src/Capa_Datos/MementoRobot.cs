using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoV7.Capa_Datos
{
    public class MementoRobot
    {
        public int IdRobot { get; }
        public TareaRobot Tarea { get; }
        public bool EstatusActividad { get; }
        public MementoRobot(int idRobot, bool estatus, TareaRobot tarea)
        {
            IdRobot = idRobot;
            EstatusActividad = estatus;
            Tarea = tarea;
        }
    }
}
