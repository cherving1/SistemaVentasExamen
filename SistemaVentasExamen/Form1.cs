using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SistemaVentasExamen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //lamar al mapeado objeto relacional a traves deun objeto

        VentasDataContext ventas = new VentasDataContext();
        private void listar()
        {
            var consulta = from V in ventas.Vendedor select V;
            dgvVendedor.DataSource = consulta;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Vendedor vendedor = new Vendedor();
            vendedor.CodVendedor = txtCodVendedor.Text;
            vendedor.Apellidos = txtApellidos.Text;
            vendedor.Nombres = txtNombres.Text;

            try
            {
                ventas.Vendedor.InsertOnSubmit(vendedor);
                ventas.SubmitChanges();

                txtCodVendedor.Clear();
                txtApellidos.Clear();
                txtNombres.Clear();
                listar(); // Asegúrate de que esta función exista y haga lo que necesitas

                MessageBox.Show("Registro guardado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //  obtener el codigo vendedor que se quiere eliminar
            var CodVendedorEliminar =(from V in ventas.Vendedor where V.CodVendedor.Contains(txtCodVendedor.Text)select V ).First();
            ventas.Vendedor.DeleteOnSubmit(CodVendedorEliminar);
            try
            {
                ventas.SubmitChanges();
                listar();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var vendedorAActualizar = (from V in ventas.Vendedor
                                           where V.CodVendedor == txtCodVendedor.Text
                                           select V).FirstOrDefault();

                if (vendedorAActualizar != null)
                {
                    vendedorAActualizar.Apellidos = txtApellidos.Text;
                    vendedorAActualizar.Nombres = txtNombres.Text;

                    ventas.SubmitChanges();

                    txtCodVendedor.Clear();
                    txtApellidos.Clear();
                    txtNombres.Clear();
                    listar();

                    MessageBox.Show("Registro actualizado correctamente.");
                }
                else
                {
                    MessageBox.Show("No se encontró ningún vendedor con el código proporcionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message);
            }
        }

    }
}
