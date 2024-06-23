using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMC
{
    public static class MetodosCitas
    {
        public static void agendarCita(string fecha, string hora, Paciente paciente, string codigoMedico)
        {
            // Nota: Al usar (Nodos.cantidadCitas++) estoy primero usando su valor, y luego aumentandolo
            Citas nvo_Nodo = new Citas(Nodos.cantidadCitas++, "En Espera", fecha, hora, paciente, codigoMedico);
            Citas actual = Nodos.ListaCitas;
            if (actual == null)
            {
                Nodos.ListaCitas = nvo_Nodo;
                return;
            }
            
            while (actual.sig != null)
            {
                actual = actual.sig;
            }
            actual.sig = nvo_Nodo;
            
        }

        public static void cancelarCita(int codigoCita) {
            Citas actual = Nodos.ListaCitas;
            while (actual != null)
            {
                if (actual.codigo == codigoCita)
                {
                    actual.estado = "Cancelada";
                }
                actual = actual.sig;
            }
        }



    }
}
