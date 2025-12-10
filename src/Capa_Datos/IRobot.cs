using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoV7.Capa_Datos
{
    public interface IRobot
    {
        int IdRobot { get; set; }
        TareaRobot Tarea { get; set; }
        bool EstatusActividad { get; set; }
        TareaRobot GetTareaRobot();
        bool getEstatusActividad();
        string isActivo();
        void AsignarTarea(int tipo);
        void AsignarEstatus(bool estatus);
        MementoRobot CrearMemento();
        void RestaurarMemento(MementoRobot memento);
    }
}
