using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GMC
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Esto es en caso de hacerlo con interfaz grafica.
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
             
            Nodos.inicializarListas();

            // prueba de la funcion agendarCita
            GestorCitas.agendarCita("17/07/2024", "10:00", new Paciente(31690875, "Omar", "Zabala"), "lmi603");
            
            // prueba de la funcion eliminar medico
            GestorMedicos.eliminarMedico("lmi603");

            // Prueba de la funcion agregarMedico
            GestorMedicos.agregarMedico("lmi603", "Hector", "Lavoe", "Cantante", 100);
            
            imprimirCitas(Nodos.ListaCitas);

            Console.ReadLine();
        }

        // [Funciones de prueba]
        static void imprimirCitas(Citas citas)
        {
            Citas actual = citas;
            while (actual != null)
            {
                Console.WriteLine("Cita: "+actual.codigo);
                Console.WriteLine("Estado: " + actual.estado);
                Console.WriteLine("C.I: "+actual.paciente.cedula);
                Console.WriteLine("Nombre: "+actual.paciente.nombre);
                Console.WriteLine("Apellido: "+actual.paciente.apellido+"\n");
                Console.WriteLine("Medico:  ");
                imprimirMedico(actual.medico);
                Console.WriteLine("Fecha: "+actual.fecha);
                Console.WriteLine("Hora: "+actual.hora);
                Console.WriteLine("");

                actual = actual.sig;
            }
        }

        static void imprimirMedico(string codigo)
        {
            Medicos medicos = Nodos.ListaMedicos;
            bool stop = false;
            while (medicos != null && !stop )
            {
                if (medicos.codigo == codigo)
                {
                    Console.WriteLine("Nombre: "+medicos.nombre);
                    Console.WriteLine("Apellido: "+medicos.apellido);
                    Console.WriteLine("Especialiadad: "+medicos.especialidad);
                    Console.WriteLine("Costo: "+medicos.costo);
                    stop = true;
                }
                medicos = medicos.sig;

            }
            if (!stop)
            {
                Console.WriteLine("Medico no disponible");
            }

        }
    }

    

}
