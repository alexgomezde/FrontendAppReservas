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
    public partial class Aeropuerto : System.Web.UI.Page
    {
        IEnumerable<Models.Aeropuerto> aeropuertos = new ObservableCollection<Models.Aeropuerto>();
        AeropuertoManager aeropuertoManager = new AeropuertoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        private async void InicializarControles()
        {

            aeropuertos = await aeropuertoManager.ObtenerAeropuertos(VG.usuarioActual.CadenaToken);
            grdAeropuertos.DataSource = aeropuertos.ToList();
            grdAeropuertos.DataBind();
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Aeropuerto aeropuertoIngresado = new Models.Aeropuerto();
                Models.Aeropuerto aeropuerto = new Models.Aeropuerto()
                {

                    AEP_NOMBRE = txtNombreAeropuerto.Text,
                    PAIS_CODIGO = Convert.ToInt32(txtPais.Text)
                };

                aeropuertoIngresado =
                    await aeropuertoManager.Ingresar(aeropuerto, VG.usuarioActual.CadenaToken);

                if (aeropuertoIngresado != null)
                {
                    lblStatus.Text = "Aeropuerto ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el aeropuerto";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        protected async void grdAeropuertos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoAeropuertoEliminado = string.Empty;
            Label lblCode = (Label)grdAeropuertos.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoAeropuerto");

            string codigoAeropuerto = lblCode.Text;

            codigoAeropuertoEliminado = await aeropuertoManager.Eliminar(codigoAeropuerto, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoAeropuertoEliminado))
            {
                lblStatus.Text = "Aeropuerto eliminado correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar el aeropuerto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }

        protected void grdAeropuertos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAeropuertos.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected void grdAeropuertos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAeropuertos.EditIndex = -1;
            InicializarControles();
        }

        protected async void grdAeropuertos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdAeropuertos.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoAeropuerto");

            string aepNombre = (grdAeropuertos.Rows[e.RowIndex].FindControl("txtNombreAeropuertoEdit") as TextBox).Text;
            string aepPais = (grdAeropuertos.Rows[e.RowIndex].FindControl("txtPaisEdit") as TextBox).Text;

            if (ValidarModificar(aepNombre, aepPais))
            {
                Models.Aeropuerto aeropuertoModificado = new Models.Aeropuerto();
                Models.Aeropuerto aeropuerto = new Models.Aeropuerto()
                {
                    AEP_CODIGO = Convert.ToInt32(lblCode.Text),
                    AEP_NOMBRE = aepNombre,
                    PAIS_CODIGO = Convert.ToInt32(aepPais)
                };

                aeropuertoModificado =
                    await aeropuertoManager.Actualizar(aeropuerto, VG.usuarioActual.CadenaToken);

                if (aeropuertoModificado != null)
                {
                    lblStatus.Text = "Aeropuerto modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el aeropuerto";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdAeropuertos.EditIndex = -1;
                InicializarControles();
            }

        }

        protected void grdAeropuertos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private bool ValidarInsertar()
        {

            if (txtNombreAeropuerto.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del aeropuerto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtPais.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el país";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }

        private bool ValidarModificar(string aepNombre, string aepPais)
        {

            if (aepNombre.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del Aeropuerto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (aepPais.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del país";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }


    }
}