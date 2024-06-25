using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMC
{
    public partial class MenuMedicos : Form
    {
        public MenuMedicos()
        {
            InitializeComponent();
            añadir_Codigo.MaxLength = 6;
            mod_Codigo.MaxLength = 6;
            elim_Codigo.MaxLength = 6;
            añadir_Costo.Maximum = 10000;
            mod_Costo.Maximum = 10000;
            elim_Costo.Maximum = 10000;
        }

        private void MenuMedicos_Load(object sender, EventArgs e)
        {
            añadir_TextChanged(null, null);
            mod_TextChanged(null, null);
            elim_btnBuscar.Enabled = false;
            elim_btnEliminar.Enabled = false;
            mod_btnBuscar.Enabled = false;
            mod_btnModificar.Enabled = false;
            listarMedicos();
        }

        private void listarMedicos()
        {
            dataGridView1.Rows.Clear();
            Medicos actual = Nodos.ListaMedicos;
            while (actual != null)
            {
                dataGridView1.Rows.Add(actual.codigo, actual.nombre, actual.apellido, actual.especialidad, actual.costo);
                actual = actual.sig;
            }
            actual = null;
        }

        private void verificarCampos(TextBox Codigo, TextBox Nombre, TextBox Apellido, TextBox Especialidad, NumericUpDown Costo, Button Boton)
        {
            Boton.Enabled = false;

            if (Costo.Value == 0)
            {
                errorProvider1.SetError(Costo, "¿Piensas regalar tus servicios?");
            } else {
                errorProvider1.SetError(Costo, "");
            }

            if (Codigo.TextLength > 2)
            {
                errorProvider1.SetError(Codigo, "");
                Medicos actual = Nodos.ListaMedicos;
                while (actual != null)
                {
                    if (actual.codigo == Codigo.Text && Codigo == añadir_Codigo)
                    {
                        errorProvider1.SetError(Codigo, "Este codigo ya esta en uso");
                        Boton.Enabled = false;
                        return;
                    }
                    else if (actual.codigo == Codigo.Text && Codigo == mod_Codigo)
                    {
                        Boton.Enabled = true;
                        return;
                    }

                    actual = actual.sig;
                }
            } else if (Codigo.TextLength < 2) {
                errorProvider1.SetError(Codigo, "El codigo debe ser de 6 digitos");
            }

            if (Nombre.TextLength > 0 && Apellido.TextLength > 0 && Especialidad.TextLength > 0 && Codigo.TextLength == 6)
            {
                Boton.Enabled = true;
            }

        }

        private void añadir_TextChanged(object sender, EventArgs e)
        {
            verificarCampos(añadir_Codigo, añadir_Nombre, añadir_Apellido, añadir_Especialidad, añadir_Costo, añadir_BtnAgregar);
        }
        private void mod_TextChanged(object sender, EventArgs e)
        {
            verificarCampos(mod_Codigo, mod_Nombre, mod_Apellido, mod_Especialidad, mod_Costo, mod_btnBuscar);
        }

        private void listar_Especialidad_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Medicos actual = Nodos.ListaMedicos;
            if (listar_Especialidad.TextLength <= 0)
            {
                listarMedicos();
            }
            else
            {
                while (actual != null)
                {
                    
                    if(actual.especialidad.StartsWith(listar_Especialidad.Text, true, null)) {
                        dataGridView1.Rows.Add(actual.codigo, actual.nombre, actual.apellido, actual.especialidad, actual.costo);
                    }
                    actual = actual.sig;
                }
            }
        }

        private void elim_Codigo_TextChanged(object sender, EventArgs e)
        {
            elim_btnBuscar.Enabled = false;
            if (elim_Codigo.TextLength == 6)
            {
                elim_btnBuscar.Enabled = true;
            }
        }

        private void elim_btnBuscar_Click(object sender, EventArgs e)
        {
            elim_btnEliminar.Enabled = false;
            elim_Nombre.Text = string.Empty;
            elim_Apellido.Text = string.Empty;
            elim_Especialidad.Text = string.Empty;
            elim_Costo.Value = 0;

            Medicos actual = Nodos.ListaMedicos;
            while (actual != null)
            {
                if (actual.codigo == elim_Codigo.Text)
                {
                    elim_Nombre.Text = actual.nombre;
                    elim_Apellido.Text = actual.apellido;
                    elim_Especialidad.Text = actual.especialidad;
                    elim_Costo.Value = actual.costo;
                    MessageBox.Show("Encontrado");
                    elim_btnEliminar.Enabled = true;
                    return;
                }
                actual = actual.sig;
            }
            
            MessageBox.Show("No se ha encontrado");
            
        }

        private void elim_btnEliminar_Click(object sender, EventArgs e)
        {
            MetodosMedicos.eliminarMedico(elim_Codigo.Text);
            elim_btnEliminar.Enabled = false;
            elim_Nombre.Text = string.Empty;
            elim_Apellido.Text = string.Empty;
            elim_Especialidad.Text = string.Empty;
            elim_Costo.Value = 0;
            MessageBox.Show("Medico eliminado correctamente");
            listarMedicos();
            
        }

        private void añadir_BtnAgregar_Click(object sender, EventArgs e)
        {
            /*
             * ¿Por qué utilizo (int) añadir_Costo.Value y no simplemente, añadir_Costo.Value?
             * Sensillo, porque me da la gana.
             * Hago eso debido a que el NumericUpDown (El cuadro de numeros) devuelve un decimal y para no complicarme, solo hago un cast
             * NT: Para facilitarme la vida, los costos estan en enteros, aunque es cierto que lo ideal seria trabajarlos en reales.
            */
            MetodosMedicos.agregarMedico(añadir_Codigo.Text, añadir_Nombre.Text, añadir_Apellido.Text, añadir_Especialidad.Text, (int) añadir_Costo.Value);
            string msg = "Medico añadido.";
            if (añadir_Costo.Value == 0)
            {
                msg += "\nGracias por ser tan generoso. TKM\nATT: Omar";
            }
            añadir_Codigo.Text = string.Empty;
            añadir_Nombre.Text = string.Empty;
            añadir_Apellido.Text = string.Empty;
            añadir_Especialidad.Text = string.Empty;
            añadir_Costo.Value = 0;
            MessageBox.Show(msg);
            listarMedicos();
        }

        private void mod_btnBuscar_Click(object sender, EventArgs e)
        {
            Medicos actual = Nodos.ListaMedicos;
            while (actual != null)
            {
                if (actual.codigo == mod_Codigo.Text)
                {
                    mod_btnModificar.Enabled = true;
                    mod_Nombre.Text = actual.nombre;
                    mod_Apellido.Text = actual.apellido;
                    mod_Especialidad.Text = actual.especialidad;
                    mod_Costo.Value = actual.costo;
                    MessageBox.Show("Encontrado");
                    listarMedicos();
                    return;
                }
                                
                actual = actual.sig;
            }
            MessageBox.Show("No encontrado");
        }

        private void mod_btnModificar_Click(object sender, EventArgs e)
        {
            MetodosMedicos.modificarMedico(mod_Codigo.Text, mod_Nombre.Text, mod_Apellido.Text, mod_Especialidad.Text, int.Parse(mod_Costo.Value.ToString()));
            mod_Codigo.Text = string.Empty;
            mod_Nombre.Text = string.Empty;
            mod_Apellido.Text = string.Empty;
            mod_Especialidad.Text = string.Empty;
            mod_Costo.Value = 0;
            MessageBox.Show("Modificado");
            listarMedicos();
        }

        private void Ganancias_BtnBuscar_Click(object sender, EventArgs e)
        {
            Medicos actualMedicos = Nodos.ListaMedicos;
            Citas actualCitas = Nodos.ListaCitas;
            int citas = 0;
            dataGridView2.Rows.Clear();

            while (actualCitas != null)
            {
                if (actualCitas.medico == Ganancias_Codigo.Text && Ganancias_UsarRango.Checked)
                {
                    DateTime fechaCita = DateTime.Parse(actualCitas.fecha);
                    DateTime fechaMinima = Ganancias_Desde.Value;
                    DateTime fechaMaxima = Ganancias_Hasta.Value;

                    // FechaMinima / FechaCita / FechaMaxima
                    //  24-06-24   / 12-07-24  / 13-07-24
                    //  Anterior(-1), Igual (0), Posterior(1)
                    int Comparacion1 = DateTime.Compare(fechaCita, fechaMinima);
                    int Comparacion2 = DateTime.Compare(fechaCita, fechaMaxima);

                    Console.WriteLine("Comp1: " + Comparacion1 + " Comp2: " + Comparacion2);

                    if (Comparacion1 < 0 && Comparacion2 > 0)
                    {
                        Console.WriteLine("Se te fue el tren araña");
                    }
                    else if (Comparacion1 >= 0 && Comparacion2 <= 0)
                    {
                        dataGridView2.Rows.Add(actualCitas.fecha);
                        citas++;
                    }
                } else if (actualCitas.medico == Ganancias_Codigo.Text) {
                    dataGridView2.Rows.Add(actualCitas.fecha);
                    citas++;
                }
                actualCitas = actualCitas.sig;
            }

            while (actualMedicos != null)
            {
                if (actualMedicos.codigo == Ganancias_Codigo.Text)
                {
                    label24.Text = "Ganancias de " + actualMedicos.nombre;
                    Ganancias_Ganancias.Value = (citas*actualMedicos.costo);
                }
                actualMedicos = actualMedicos.sig;
            }

        }

    }
}
