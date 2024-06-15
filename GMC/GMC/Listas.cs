using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMC
{
    public static class Listas
    {
        public static int cantidadCitas = 0;
        public static Citas ListaCitas;
        public static Medicos ListaMedicos;

        public static void cargar()
        {
            Medicos medico4 = new Medicos("csi504", "Celena", "Sánchez", "Internista", 50);
            Medicos medico3 = new Medicos("lmi603", "Luisa", "Marín", "Internista", 60, medico4);
            Medicos medico2 = new Medicos("mmn302", "María", "Marcano", "Nefrologia", 30, medico3);
            ListaMedicos = new Medicos("jgc501", "José", "Gómez", "Cardiologia", 50, medico2);

            ListaCitas = new Citas(cantidadCitas++, "En Espera", "12/07/2024", "10:00", new Paciente(10555555, "José", "Gómez"), "jgc501");
            Citas cita2 = new Citas(cantidadCitas++, "En Espera", "12/07/2024", "10:45", new Paciente(8555555, "María", "López"), "jgc501");
            Citas cita3 = new Citas(cantidadCitas++, "En Espera", "14/07/2024", "10:00", new Paciente(11111111, "Luis", "Gonzáles"), "mmn302");
            Citas cita4 = new Citas(cantidadCitas++, "En Espera", "14/07/2024", "10:45", new Paciente(9999999, "Luis", "Hernandez"), "mmn302");
            Citas cita5 = new Citas(cantidadCitas++, "En Espera", "12/07/2024", "10:00", new Paciente(8888888, "Juan", "Gonzáles"), "lmi603");
            Citas cita6 = new Citas(cantidadCitas++, "En Espera", "14/07/2024", "11:30", new Paciente(7777777, "Angela", "Fernandez"), "mmn302");
            Citas cita7 = new Citas(cantidadCitas++, "En Espera", "14/07/2024", "10:00", new Paciente(6666666, "Elena", "Martinez"), "csi504");
            Citas cita8 = new Citas(cantidadCitas++, "En Espera", "16/07/2024", "10:00", new Paciente(5555555, "Irene", "Marcano"), "lmi603");
            Citas cita9 = new Citas(cantidadCitas++, "En Espera", "14/07/2024", "11:30", new Paciente(4444444, "Juan", "Zapata"), "mmn302");

            ListaCitas.sig = cita2;
            cita2.sig = cita3;
            cita3.sig = cita4;
            cita4.sig = cita5;
            cita5.sig = cita6;
            cita6.sig = cita7;
            cita7.sig = cita8;
            cita8.sig = cita9;

            
        }

    }

    public class Citas
    {
        public int codigo;
        public string estado, fecha, hora, medico;
        public Paciente paciente;
        public Citas sig;

        public Citas()
        {
            this.sig = null;
        }

        public Citas(int codigo, string estado, string fecha, string hora, Paciente paciente, string medico)
        {
            this.codigo = codigo;
            this.estado = estado;
            this.fecha = fecha;
            this.hora = hora;
            this.paciente = paciente;
            this.medico = medico;
        }

    }

    public class Medicos
    {
        public string codigo, nombre, apellido, especialidad;
        public int costo;
        public Medicos sig;

        public Medicos()
        {
            this.sig = null;
        }

        public Medicos(string codigo, string nombre, string apellido, string especialidad, int costo)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.apellido = apellido;
            this.especialidad = especialidad;
            this.costo = costo;
            this.sig = null;
        }

        public Medicos(string codigo, string nombre, string apellido, string especialidad, int costo, Medicos sig)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.apellido = apellido;
            this.especialidad = especialidad;
            this.costo = costo;
            this.sig = sig;
        }

    }

    public class Paciente
    {
        public string nombre, apellido;
        public int cedula;

        public Paciente(int cedula, string nombre, string apellido)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.apellido = apellido;
        }

    }


}
