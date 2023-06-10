using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace presentacion
{
    public class Helper
    {
        //public static bool 


        public static void CargarImagen(PictureBox pictureBox, string url)
        {
            
            
                try
                {
                    pictureBox.Load(url);

                }
                catch (Exception)
                {

                    pictureBox.Load("https://react.semantic-ui.com/images/wireframe/image.png");
                }
            
        }



        public static bool ValidarPrecio(string cadena)
        {
            if (decimal.TryParse(cadena, out decimal valorDecimal))
            {
                if(valorDecimal<=0)
                {
                    return false;
                }
                
                return true;
                
            }
            else
            {
                return false;
            }
        }

        public static bool ValidarCodigo(string codigo)
        {
            if (codigo.Length != 3)
            {
                return false;
            }
            if (char.IsLetter(codigo[0]) && char.IsNumber(codigo[1]) && char.IsNumber(codigo[2]))
            {
                return true;
            }
            return false;
        }
        
    }
}
