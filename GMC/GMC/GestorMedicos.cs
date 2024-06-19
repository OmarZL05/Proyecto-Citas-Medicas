using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMC
{
    public static class GestorMedicos
    {
        
        public static void agregarMedico(string codigo, string nombre, string apellido, string especialidad, int costo)
        {
            Medicos nvo_Nodo = new Medicos(codigo, nombre, apellido, especialidad, costo);
            Medicos actual = Nodos.ListaMedicos;
            if (actual == null)
            {
                Nodos.ListaMedicos = nvo_Nodo;
                return;
            }

            while (actual.sig != null) // Inserccion al final
            {
                actual = actual.sig;
            }
            actual.sig = nvo_Nodo;
        }

        public static void eliminarMedico(string codigo)
        {
            Medicos actual = Nodos.ListaMedicos;

            if (actual == null || (actual.sig == null && actual.codigo == codigo))
            {
                Nodos.ListaMedicos = null;
                return;
            }
            
            while (actual.sig != null)
            {
                if (actual.sig.codigo == codigo)
                {
                    actual.sig = actual.sig.sig;
                }
                actual = actual.sig;
            }
           
        }


    }
}
