using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMC
{
    public static class MetodosMedicos
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

        /// <summary>
        /// Modifica los datos del medico usando el codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="especialidad"></param>
        /// <param name="costo"></param>
        public static void modificarMedico(string codigo, string nombre, string apellido, string especialidad, int costo)
        {
            Medicos actual = Nodos.ListaMedicos;
            while (actual != null)
            {
                if (codigo == actual.codigo)
                {
                    actual.nombre = nombre;
                    actual.apellido = apellido;
                    actual.especialidad = especialidad;
                    actual.costo = costo;
                }
                actual = actual.sig;
            }
        }

        public static bool eliminarMedico(string codigo)
        {
            Medicos actual = Nodos.ListaMedicos;

            if (actual == null)
            {
                Nodos.ListaMedicos = null;
                return false;
            }

            if (actual.sig == null && actual.codigo == codigo)
            {
                Nodos.ListaMedicos = null;
                return true;
            }
            
            while (actual.sig != null)
            {
                if (actual.sig.codigo == codigo)
                {
                    actual.sig = actual.sig.sig;
                    return true;
                }
                actual = actual.sig;
            }

            return true;
        }


    }
}
