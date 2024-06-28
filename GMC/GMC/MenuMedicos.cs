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
            elim_Nombre.ReadOnly = true;
            elim_Apellido.ReadOnly = true;
            elim_Especialidad.ReadOnly = true;
            elim_Costo.ReadOnly = true;
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

            errorProvider1.SetError(Costo, "");
            if (Costo.Value == 0)
            {
                errorProvider1.SetError(Costo, "¿Piensas regalar tus servicios?");
            }

            errorProvider1.SetError(Codigo, "");

            if (Codigo.TextLength <= 2)
            {
                errorProvider1.SetError(Codigo, "El codigo debe ser de 6 digitos");
                return;
            }

            Medicos actual = Nodos.ListaMedicos;
            while (actual != null)
            {
                if (actual.codigo.ToLower() == Codigo.Text.ToLower()) 
                {
                    if (Codigo == añadir_Codigo)
                    {
                        errorProvider1.SetError(Codigo, "Este codigo ya esta en uso");
                        Boton.Enabled = false;
                        return;
                    }
                    Boton.Enabled = true;
                    return;
                }

                actual = actual.sig;
            }


            int contLetras = 0;
            int contNumeros = 0;
            for (int i = 0; i < Codigo.TextLength; i++)
            {

                char caracter = Codigo.Text.ElementAt(i);
                if (Char.IsLetter(caracter) && contLetras <= 3)
                {
                    contLetras++;
                }
                if (Char.IsNumber(caracter) && contLetras == 3 && contNumeros <= 3)
                {
                    contNumeros++;
                }
                
            }

            if (Codigo.TextLength == 6 && (contLetras != 3 || contNumeros != 3))
            {
                errorProvider1.SetError(Codigo, "NOTA: El codigo debe estar formado por 3 caracteres alfabeticos y 3 numeros.");
                return;
            }
            Console.WriteLine(contLetras + " : " + contNumeros);


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
            elim_btnEliminar.Enabled = false;
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
            mod_btnModificar.Enabled = false;
            MessageBox.Show("Modificado");
            listarMedicos();
        }

        private void Ganancias_BtnBuscar_Click(object sender, EventArgs e)
        {
            Medicos actualMedicos = Nodos.ListaMedicos;
            Citas actualCitas = Nodos.ListaCitas;
            int citas = 0;
            dataGridView2.Rows.Clear();
            label24.Text = "Ganancias";
            Ganancias_CostoMedico.Text = "Precio de la consulta: ";
            Ganancias_Ganancias.Value = 0;

            bool encontrado = false;
            while (actualMedicos != null)
            {
                if (actualMedicos.codigo.ToLower() == Ganancias_Codigo.Text.ToLower())
                {
                    encontrado = true;
                    break;
                }
                actualMedicos = actualMedicos.sig;
            }

            if (!encontrado) {
                MessageBox.Show("Codigo invalido");
                return;
            }

            while (actualCitas != null)
            {

                if (actualCitas.medico != Ganancias_Codigo.Text && Ganancias_UsarRango.Checked)
                {
                    DateTime fechaCita = DateTime.Parse(actualCitas.fecha);
                    DateTime fechaMinima = Ganancias_Desde.Value;
                    DateTime fechaMaxima = Ganancias_Hasta.Value;

                    int Comparacion1 = DateTime.Compare(fechaCita, fechaMinima);
                    int Comparacion2 = DateTime.Compare(fechaCita, fechaMaxima);

                    if (Comparacion1 >= 0 && Comparacion2 <= 0)
                    {
                        dataGridView2.Rows.Add(actualCitas.fecha, actualCitas.hora);
                        citas++;
                    }
                } else if (actualCitas.medico == Ganancias_Codigo.Text) {
                    dataGridView2.Rows.Add(actualCitas.fecha, actualCitas.hora);
                    citas++;
                }
                actualCitas = actualCitas.sig;
            }

            label24.Text = "Ganancias de " + actualMedicos.nombre;
            Ganancias_CostoMedico.Text = "Precio de la consulta: " + actualMedicos.costo;
            Ganancias_Ganancias.Value = (citas * actualMedicos.costo);

        }

    }
}
