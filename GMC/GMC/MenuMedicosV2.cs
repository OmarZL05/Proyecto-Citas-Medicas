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
    public partial class MenuMedicosV2 : Form
    {
        public MenuMedicosV2()
        {
            InitializeComponent();
            Codigo.MaxLength = 6;
            Costo.Maximum = 10000;
            btnAdd_Mod.Text = "Agregar";
        }

        private void MenuMedicos_Load(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            btnAdd_Mod.Enabled = false;
            btnEliminar.Enabled = false;
            listarMedicos();
        }

        /// <summary>
        /// Este modulo se encarga de limpiar el dataGridView1 y añadirle los medicos
        /// </summary>
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

        /// <summary>
        /// Este modulo se encarga de verificar si los textBox se encuentran en orden, ademas de controlar los botones
        /// </summary>
        private void verificarCodigo()
        {
            btnBuscar.Enabled = false;
            btnAdd_Mod.Enabled = false;
            btnEliminar.Enabled = false;
            btnAdd_Mod.Text = "Agregar";
            errorProvider1.SetError(Codigo, "");
            if (Codigo.TextLength == 6)
            {
                Medicos actual = Nodos.ListaMedicos;
                while (actual != null)
                {
                    if (actual.codigo == Codigo.Text)
                    {
                        btnBuscar.Enabled = true;
                        btnAdd_Mod.Enabled = false;
                        return;
                    }
                    actual = actual.sig;
                }

                if (Nombre.TextLength > 0 && Apellido.TextLength > 0 && Especialidad.TextLength > 0 && Codigo.TextLength == 6)
                {
                    btnBuscar.Enabled = false;
                    btnAdd_Mod.Text = "Agregar";
                    btnAdd_Mod.Enabled = true;
                }

                if (Nombre.TextLength == 0 && Apellido.TextLength == 0 && Especialidad.TextLength == 0 && Codigo.TextLength == 6)
                {
                    errorProvider1.SetError(Codigo, "No hay ningun medico con este codigo. Rellene los campos para agregarlo");
                }

            }
            
        }
        
        // [Zona de TextChanged]
        private void Especialidad_TextChanged(object sender, EventArgs e)
        {
            verificarCodigo();
        }

        private void Nombre_TextChanged(object sender, EventArgs e)
        {
            verificarCodigo();
        }

        private void Apellido_TextChanged(object sender, EventArgs e)
        {
            verificarCodigo();
        }

        private void Codigo_TextChanged(object sender, EventArgs e)
        {
            verificarCodigo();
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

                    if (actual.especialidad.StartsWith(listar_Especialidad.Text, true, null))
                    {
                        dataGridView1.Rows.Add(actual.codigo, actual.nombre, actual.apellido, actual.especialidad, actual.costo);
                    }
                    actual = actual.sig;
                }
            }

            actual = null;
        }

        // [Zona de Botones]
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Medicos actual = Nodos.ListaMedicos;
            while (actual != null)
            {
                if (actual.codigo == Codigo.Text)
                {
                    Nombre.Text = actual.nombre;
                    Apellido.Text = actual.apellido;
                    Especialidad.Text = actual.especialidad;
                    Costo.Value = actual.costo;
                    btnEliminar.Enabled = true;
                    btnAdd_Mod.Text = "Modificar";
                    btnAdd_Mod.Enabled = true;
                    return;
                }
                actual = actual.sig;
            }

            MessageBox.Show("No se ha encontrado");
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MetodosMedicos.eliminarMedico(Codigo.Text);
            btnVaciarCampos_Click(null, null);
            MessageBox.Show("Medico eliminado correctamente");
            listarMedicos();
            
        }

        private void btnAdd_Mod_Click(object sender, EventArgs e)
        {
            string codigo, nombre, apellido, especialidad, costo;
            codigo = Codigo.Text;
            nombre = Nombre.Text;
            apellido = Apellido.Text;
            especialidad = Especialidad.Text;
            costo = Costo.Value.ToString();

            if (btnAdd_Mod.Text == "Agregar")
            {
                btnVaciarCampos_Click(null, null);
                MetodosMedicos.agregarMedico(codigo, nombre, apellido, especialidad, int.Parse(costo));
                MessageBox.Show("Medico añadido");
            }

            if (btnAdd_Mod.Text == "Modificar")
            {
                btnVaciarCampos_Click(null, null);
                MetodosMedicos.modificarMedico(codigo, nombre, apellido, especialidad, int.Parse(costo));
                MessageBox.Show("Modificado");
            }

            listarMedicos();
        }

        private void btnVaciarCampos_Click(object sender, EventArgs e)
        {
            Codigo.Text = string.Empty;
            Nombre.Text = string.Empty;
            Apellido.Text = string.Empty;
            Especialidad.Text = string.Empty;
            Costo.Value = 0;
        }

        private void especialidad_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
