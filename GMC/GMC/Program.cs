using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMC
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
             * */

            Listas.cargar();
            agregarCita("17/07/2024", "10:00", "lmi603", new Paciente(31690875, "Omar", "Zabala"));
            Listas.eliminarMedico("lmi603");
            Listas.agregarMedico("lmi603", "Hector", "Lavoe", "Cantante", 100);
            imprimirCitas(Listas.ListaCitas);
            
            Console.ReadLine();
        }

        static void imprimirCitas(Citas citas)
        {
            Citas actual = citas;
            while (actual != null)
            {
                Console.WriteLine("[");
                Console.WriteLine("Cita: "+actual.codigo);
                Console.WriteLine("Estado: " + actual.estado);
                Console.WriteLine("C.I: "+actual.paciente.cedula);
                Console.WriteLine("Nombre: "+actual.paciente.nombre);
                Console.WriteLine("Apellido: "+actual.paciente.apellido+"\n");
                Console.WriteLine("Medico:  ");
                imprimirMedico(actual.medico);
                Console.WriteLine("Fecha: "+actual.fecha);
                Console.WriteLine("Hora: "+actual.hora);
                Console.WriteLine("]\n");

                actual = actual.sig;
            }
        }

        static void imprimirMedico(string codigo)
        {
            Medicos medicos = Listas.ListaMedicos;
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

        static void agregarCita(string fecha, string hora, string medico, Paciente paciente)
        {
            Citas actual = Listas.ListaCitas;
            Citas nvo_Nodo = new Citas(Listas.cantidadCitas++, "En Espera", fecha, hora, paciente, medico);
            while (actual.sig != null)
            {
                actual = actual.sig;
            }

            actual.sig = nvo_Nodo;

        }

    }

    

}
