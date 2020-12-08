using AppReservasSW.Controllers;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AppReservasSW.Views
{
    public partial class Pais : System.Web.UI.Page
    {
        IEnumerable<Models.Pais> pais = new ObservableCollection<Models.Pais>();
        PaisManager paisManager = new PaisManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }
        private async void InicializarControles()
        {
            pais = await paisManager.ObtenerPaises(VG.usuarioActual.CadenaToken);
            grdPais.DataSource = pais.ToList();
            grdPais.DataBind();
        }

        protected async void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Pais paisIngresado = new Models.Pais();
                Models.Pais pais = new Models.Pais()
                {

                    PAIS_NOMBRE = txtNombre.Text,
            

                };

                paisIngresado =
                    await paisManager.Ingresar(pais, VG.usuarioActual.CadenaToken);

                if (paisIngresado != null)
                {
                    lblStatus.Text = "Pais ingresada correctamente";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el pais";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }


        protected async void grdPais_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoTarifaEliminado = string.Empty;
            Label lblCode = (Label)grdPais.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoPais");

            string codigoTarifa = lblCode.Text;

            codigoTarifaEliminado = await paisManager.Eliminar(codigoTarifa, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoTarifaEliminado))
            {
                lblStatus.Text = "Pais eliminada correctamente";
                lblStatus.ForeColor = Color.Green;
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar el pais";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }


        protected void grdPais_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdPais.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected void grdPais_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdPais.EditIndex = -1;
            InicializarControles();
        }



        protected async void grdPais_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdPais.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoPais");

            string paisNombre = (grdPais.Rows[e.RowIndex].FindControl("txtPaisNombreEdit") as TextBox).Text;
       
            if (ValidarModificar(paisNombre))
            {
                Models.Pais paisModificado = new Models.Pais();
                Models.Pais pais = new Models.Pais()
                {
                    PAIS_CODIGO = Convert.ToInt32(lblCode.Text),
                    PAIS_NOMBRE = paisNombre
                };

                paisModificado =
                    await paisManager.Actualizar(pais, VG.usuarioActual.CadenaToken);

                if (paisModificado != null)
                {
                    lblStatus.Text = "Pais modificada correctamente";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el pais";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdPais.EditIndex = -1;
                InicializarControles();
            }

        }


        protected void grdPais_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        private bool ValidarInsertar()
        {

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Fila de nombre no acepta números";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

           
            return true;
        }

        private bool ValidarModificar(string paisNombre)
        {

            if (paisNombre.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del pais";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (paisNombre.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Fila de nombre no acepta números";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }










    }
}
