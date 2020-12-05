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
    public partial class Aerolinea : System.Web.UI.Page
    {
        IEnumerable<Models.Aerolinea> aerolineas = new ObservableCollection<Models.Aerolinea>();
        AerolineaManager aerolineaManager = new AerolineaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        private async void InicializarControles()
        {

            aerolineas = await aerolineaManager.ObtenerAerolineas(VG.usuarioActual.CadenaToken);
            grdAerolineas.DataSource = aerolineas.ToList();
            grdAerolineas.DataBind();

        }


        async protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Aerolinea aerolineaIngresado = new Models.Aerolinea();
                Models.Aerolinea aerolinea = new Models.Aerolinea()
                {

                    AER_RUC = txtAerolineaRuc.Text.ToString(),
                    AER_NOMBRE = txtAerolineaNombre.Text.ToString(),
                    AER_ESTADO = drpEstado.SelectedValue.ToString()

                };

                aerolineaIngresado =
                    await aerolineaManager.Ingresar(aerolinea, VG.usuarioActual.CadenaToken);

                if (aerolineaIngresado != null)
                {
                    lblStatus.Text = "Aerolinea ingresada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar la aerolinea";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }

        }

        protected async void grdAerolineas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoAerolineaEliminado = string.Empty;
            Label lblCode = (Label)grdAerolineas.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoAerolinea");

            string codigoAerolinea = lblCode.Text;

            codigoAerolineaEliminado = await aerolineaManager.Eliminar(codigoAerolinea, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoAerolineaEliminado))
            {
                lblStatus.Text = "Aerolinea eliminada correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar la aerolinea";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }

        protected void grdAerolineas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAerolineas.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected void grdAerolineas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAerolineas.EditIndex = -1;
            InicializarControles();

        }

        protected async void grdAerolineas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdAerolineas.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoAerolinea");

            string aerRuc = (grdAerolineas.Rows[e.RowIndex].FindControl("txtAerolineaRucEdit") as TextBox).Text;
            string aerNombre = (grdAerolineas.Rows[e.RowIndex].FindControl("txtAerolineaNombreEdit") as TextBox).Text;
            string aerEstado = (grdAerolineas.Rows[e.RowIndex].FindControl("drpEstadoEdit") as DropDownList).Text;


            if (ValidarModificar(aerRuc, aerNombre))
            {
                Models.Aerolinea aerolineaModificado = new Models.Aerolinea();
                Models.Aerolinea aerolinea = new Models.Aerolinea()
                {
                    AER_CODIGO = Convert.ToInt32(lblCode.Text),
                    AER_RUC = aerRuc,
                    AER_NOMBRE = aerNombre,
                    AER_ESTADO = aerEstado
                };

                aerolineaModificado =
                    await aerolineaManager.Actualizar(aerolinea, VG.usuarioActual.CadenaToken);

                if (aerolineaModificado != null)
                {
                    lblStatus.Text = "Aerolinea modificada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar la aerolinea";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdAerolineas.EditIndex = -1;
                InicializarControles();
            }

        }

        private bool ValidarInsertar()
        {

            if (txtAerolineaRuc.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el Ruc de la Aerolinea";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtAerolineaNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre de la Aerolinea";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            return true;
        }


        private bool ValidarModificar(string aerRuc, string aerNombre)
        {

            if (aerRuc.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el Ruc de la Aerolinea";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (aerNombre.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre de la Aerolinea";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
    }
}