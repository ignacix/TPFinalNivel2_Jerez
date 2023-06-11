using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;
using System.Globalization;

namespace presentacion
{
    public partial class Form1 : Form
    {
        public List<Articulo> lista;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            dgvPrincipal.DataSource = articuloNegocio.Listar();
            Articulo selecionado = dgvPrincipal.CurrentRow.DataBoundItem as Articulo;
            OcultarColumnas();
            Helper.CargarImagen(pictureBox1, selecionado.UrlImagen);
            CargarDatos(selecionado);
            ActualizarGrilla();
            cbxCampo.Items.Add("Categoria");
            cbxCampo.Items.Add("Marca");            
        }
        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            cbxCampo.SelectedIndex = -1;
            cbxCriterio.SelectedIndex = -1;
            txbBuscar.Text = "";
            ActualizarGrilla();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmModificarYAgregar frmModificarYAgregar = new frmModificarYAgregar();
            frmModificarYAgregar.ShowDialog();
            ActualizarGrilla();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo seleccionado = (Articulo)dgvPrincipal.CurrentRow.DataBoundItem;
                frmModificarYAgregar frmModificarYAgregar = new frmModificarYAgregar(seleccionado);
                frmModificarYAgregar.ShowDialog();
                ActualizarGrilla();                                                    
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Por favor SELECCIONE UN ARTICULO");
                return;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articuloActual = (Articulo)dgvPrincipal.CurrentRow.DataBoundItem;
                DialogResult decision = MessageBox.Show($"Esta seguro que desea borrar el articulo {articuloActual.Nombre}?", "Eliminar", MessageBoxButtons.YesNo);
                if (decision == DialogResult.Yes)
                {
                    negocio.Eliminar(articuloActual.Id);
                    MessageBox.Show("Articulo Eliminado");
                    ActualizarGrilla();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Por favor SELECCIONE UN ARTICULO");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }            
        }

        private void dgvPrincipal_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPrincipal.CurrentRow != null)
            {
                Articulo selecionado = dgvPrincipal.CurrentRow.DataBoundItem as Articulo;            
                Helper.CargarImagen(pictureBox1,selecionado.UrlImagen);
                CargarDatos(selecionado);
            }
        }

        private void CargarDatos(Articulo art)
        {
            txbMostrarNombre.Text = $"{art.Nombre}";
            txbMostrarDescripcion.Text = $"Descripción: {art.Descripcion}\r\nMarca: {art.Marca}\r\nPrecio: {art.Precio}";
        }

        
        private void OcultarColumnas()
        {
            dgvPrincipal.Columns["Id"].Visible = false;
            dgvPrincipal.Columns["Descripcion"].Visible = false;
            dgvPrincipal.Columns["UrlImagen"].Visible = false;
        }

        private void ActualizarGrilla()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                lista = articuloNegocio.Listar();
                dgvPrincipal.DataSource = lista;
                OcultarColumnas();
                Articulo seleccionado = (Articulo)dgvPrincipal.CurrentRow.DataBoundItem;
                CargarDatos(seleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCampo.SelectedItem != null)
            {

                string opcion = cbxCampo.SelectedItem.ToString();

                if (opcion == "Categoria")
                {
                    cbxCriterio.Items.Clear();
                    cbxCriterio.Items.Add("Celulares");
                    cbxCriterio.Items.Add("Televisores");
                    cbxCriterio.Items.Add("Media");
                    cbxCriterio.Items.Add("Audio");
                }
                else
                {
                    cbxCriterio.Items.Clear();
                    cbxCriterio.Items.Add("Samsung");
                    cbxCriterio.Items.Add("Apple");
                    cbxCriterio.Items.Add("Sony");
                    cbxCriterio.Items.Add("Huawei");
                    cbxCriterio.Items.Add("Motorola");
                }
            }
        }

        private void cbxCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            if (cbxCriterio.SelectedItem != null)
            {
                string filtroCampo = cbxCampo.SelectedItem.ToString();
                string filtroCategoria = cbxCriterio.SelectedItem.ToString();

                if(filtroCampo == "Categoria")
                {
                    listaFiltrada = lista.FindAll(x => x.Categoria.ToString() == filtroCategoria);
                }
                else
                {
                    listaFiltrada = lista.FindAll(x => x.Marca.ToString() == filtroCategoria);
                }

                dgvPrincipal.DataSource = null;
                dgvPrincipal.DataSource = listaFiltrada;
                OcultarColumnas();
            }            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txbBuscar.Text;

            listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Codigo.ToUpper().Contains(filtro.ToUpper()));
            dgvPrincipal.DataSource = null;
            dgvPrincipal.DataSource = listaFiltrada;            
            OcultarColumnas();           
        }

    }
}
