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
            añadir_BtnAgregar.Enabled = false;
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

        private void verificar()
        {
            añadir_BtnAgregar.Enabled = false;
            if (añadir_Nombre.TextLength > 0 && añadir_Apellido.TextLength > 0 && añadir_Especialidad.TextLength > 0 && añadir_Codigo.TextLength == 6)
            {
                Medicos actual = Nodos.ListaMedicos;
                while (actual != null)
                {
                    if (actual.codigo == añadir_Codigo.Text)
                    {
                        errorProvider1.SetError(añadir_Codigo, "Este codigo ya esta en uso");
                        añadir_BtnAgregar.Enabled = false;
                        return;
                    }
                    actual = actual.sig;
                }

                errorProvider1.SetError(añadir_Codigo, "");
                añadir_BtnAgregar.Enabled = true;

            }
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void especialidad_TextChanged(object sender, EventArgs e)
        {
            verificar();
        }

        private void nombre_TextChanged(object sender, EventArgs e)
        {
            verificar();
        }

        private void apellido_TextChanged(object sender, EventArgs e)
        {
            verificar();
        }

        private void codigo_TextChanged(object sender, EventArgs e)
        {
            verificar();
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

            actual = null;
        }

        private void elim_Codigo_TextChanged(object sender, EventArgs e)
        {
            elim_btnBuscar.Enabled = false;
            if (elim_Codigo.TextLength == 6)
            {
                elim_btnBuscar.Enabled = true;
            }
        }

        private void mod_Codigo_TextChanged(object sender, EventArgs e)
        {
            mod_btnBuscar.Enabled = false;
            if (mod_Codigo.TextLength == 6)
            {
                mod_btnBuscar.Enabled = true;
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
            bool encontrado = false;
            while (actual != null)
            {
                if (actual.codigo == elim_Codigo.Text)
                {
                    elim_Nombre.Text = actual.nombre;
                    elim_Apellido.Text = actual.apellido;
                    elim_Especialidad.Text = actual.especialidad;
                    elim_Costo.Value = actual.costo;
                    encontrado = true;
                }
                actual = actual.sig;
            }
            if (encontrado)
            {
                MessageBox.Show("Encontrado");
                elim_btnEliminar.Enabled = true;
            }
            else
            {
                MessageBox.Show("No se ha encontrado");
            }
        }

        private void elim_btnEliminar_Click(object sender, EventArgs e)
        {
            if (MetodosMedicos.eliminarMedico(elim_Codigo.Text))
            {
                elim_btnEliminar.Enabled = false;
                elim_Nombre.Text = string.Empty;
                elim_Apellido.Text = string.Empty;
                elim_Especialidad.Text = string.Empty;
                elim_Costo.Value = 0;
                MessageBox.Show("Medico eliminado correctamente");
                listarMedicos();
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void añadir_BtnAgregar_Click(object sender, EventArgs e)
        {
            MetodosMedicos.agregarMedico(añadir_Codigo.Text, añadir_Nombre.Text, añadir_Apellido.Text, añadir_Especialidad.Text, int.Parse(añadir_Costo.Value.ToString()));
            añadir_Codigo.Text = string.Empty;
            añadir_Nombre.Text = string.Empty;
            añadir_Apellido.Text = string.Empty;
            añadir_Especialidad.Text = string.Empty;
            añadir_Costo.Value = 0;
            MessageBox.Show("Medico añadido");
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
            actual = null;
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
        }
    }
}
