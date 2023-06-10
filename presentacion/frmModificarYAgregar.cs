using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class frmModificarYAgregar : Form
    {
        Articulo Articulo = null;
        public frmModificarYAgregar()
        {
            InitializeComponent();
            Text = "Cargar Artículo";
            this.lblTitulo.Text = "Agregar";

        }
        public frmModificarYAgregar(Articulo articuloEnviado)
        {
            InitializeComponent();
            this.Articulo = articuloEnviado;
            Text = "Modificar Artículo";
            btnModificarYAgregar.Text = "Modificar";
            this.lblTitulo.Text = "Modificar";
        }

        private void btnModificarYAgregar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            
            try
            {
                if (Helper.ValidarCodigo(txbCodigoArticulo.Text))
                {
                    Articulo.Codigo = txbCodigoArticulo.Text.ToUpper();
                }
                else
                {
                    MessageBox.Show("Estimado Usiario:\nEL CODIGO DEBE SER DE LA SIGUIENTE MANERA (LETRA-NÚMERO-NÚMERO)");
                    return;
                }
                if (txbNombre.Text.Length != 0 && txbDescripcion.Text.Length != 0)
                {
                    Articulo.Nombre = txbNombre.Text;
                    Articulo.Descripcion = txbDescripcion.Text;
                }
                else
                {
                    MessageBox.Show("Estimado Usiario:\n ES OBLIGATORIO CARGAR EL (NOMBRE) Y (DESCRIPCIÓN)");
                    return;
                }
                Articulo.Marca = (Marca)cbxMarca.SelectedItem;
                Articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                Articulo.UrlImagen = txbImagen.Text;
                if(Helper.ValidarPrecio(txbPrecio.Text))
                {
                    Articulo.Precio = decimal.Parse(txbPrecio.Text);
                }
                else
                {
                    MessageBox.Show("Estimado Usuario:\nINGRESE SOLO NUMEROS MAYORES A 0.\n ");
                    return;
                }


                
                if (this.Articulo.Id !=0)
                {
                    articuloNegocio.Modificar(Articulo);
                    MessageBox.Show("Artículo Modificado Correctamente");
                    this.Close();
                }
                else
                {
                    articuloNegocio.Agregar(Articulo);
                    MessageBox.Show("Articulo Agregado Correctamente");
                    this.Close();
                }

            }
            catch (Exception )
            {

                throw;
            }                        

        }

        private void frmModificarYAgregar_Load(object sender, EventArgs e)
        {
            try
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                cbxMarca.DataSource = marcaNegocio.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                cbxCategoria.DataSource = categoriaNegocio.Listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";
                if (Articulo !=null)
                {
                    this.txbCodigoArticulo.Text = Articulo.Codigo;
                    this.txbNombre.Text = Articulo.Nombre;
                    this.txbDescripcion.Text = Articulo.Descripcion;
                    this.cbxMarca.SelectedValue = (int)Articulo.Marca.Id;
                    this.cbxCategoria.SelectedValue = (int)Articulo.Categoria.Id;
                    this.txbImagen.Text = Articulo.UrlImagen;
                    Helper.CargarImagen(pcbModificarYAgregar, Articulo.UrlImagen);
                    this.txbPrecio.Text = Articulo.Precio.ToString();                   
                }
                else
                {
                    Articulo = new Articulo();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            

        }

        private void txbImagen_MouseLeave(object sender, EventArgs e)
        {
            Helper.CargarImagen(pcbModificarYAgregar,txbImagen.Text);
        }
    }
}
