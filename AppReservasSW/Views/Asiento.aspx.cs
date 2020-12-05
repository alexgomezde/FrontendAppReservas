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
    public partial class Asiento : System.Web.UI.Page
    {
        IEnumerable<Models.Asiento> asientos = new ObservableCollection<Models.Asiento>();
        AsientoManager asientoManager = new AsientoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        private async void InicializarControles()
        {

            asientos = await asientoManager.ObtenerAsientos(VG.usuarioActual.CadenaToken);
            grdAsientos.DataSource = asientos.ToList();
            grdAsientos.DataBind();
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Asiento asientoIngresado = new Models.Asiento();
                Models.Asiento asiento = new Models.Asiento()
                {

                    ASI_LETRA = txtLetraAsiento.Text,
                    ASI_FILA = Convert.ToInt32(txtFilaAsiento.Text),
                    ASI_ESTADO = drpDisponibilidad.SelectedValue.ToString()
                };

                asientoIngresado =
                    await asientoManager.Ingresar(asiento, VG.usuarioActual.CadenaToken);

                if (asientoIngresado != null)
                {
                    lblStatus.Text = "Asiento ingresado correctamente";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el asiento";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        protected async void grdAsientos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoAsientoEliminado = string.Empty;
            Label lblCode = (Label)grdAsientos.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoAsiento");

            string codigoAsiento = lblCode.Text;

            codigoAsientoEliminado = await asientoManager.Eliminar(codigoAsiento, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoAsientoEliminado))
            {
                lblStatus.Text = "Asiento eliminado correctamente";
                lblStatus.ForeColor = Color.Green;
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar el asiento";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }

        protected void grdAsientos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAsientos.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected void grdAsientos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAsientos.EditIndex = -1;
            InicializarControles();
        }

        protected async void grdAsientos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdAsientos.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoAsiento");

            string asiLetra = (grdAsientos.Rows[e.RowIndex].FindControl("txtLetraAsientoEdit") as TextBox).Text;
            string asiFila = (grdAsientos.Rows[e.RowIndex].FindControl("txtFilaAsientoEdit") as TextBox).Text;
            string asiEstado = (grdAsientos.Rows[e.RowIndex].FindControl("drpDisponibilidadEdit") as DropDownList).Text;

            if (ValidarModificar(asiLetra, asiFila, asiEstado))
            {
                Models.Asiento asientoModificado = new Models.Asiento();
                Models.Asiento asiento = new Models.Asiento()
                {
                    ASI_CODIGO = Convert.ToInt32(lblCode.Text),
                    ASI_LETRA = asiLetra,
                    ASI_FILA = Convert.ToInt32(asiFila),
                    ASI_ESTADO =asiEstado
                };

                asientoModificado =
                    await asientoManager.Actualizar(asiento, VG.usuarioActual.CadenaToken);

                if (asientoModificado != null)
                {
                    lblStatus.Text = "Asiento modificado correctamente";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el asiento";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdAsientos.EditIndex = -1;
                InicializarControles();
            }

        }

        protected void grdAsientos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private bool ValidarInsertar()
        {

            if (txtLetraAsiento.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la letra del asiento";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if(txtLetraAsiento.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Letra Asiento no puede ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtFilaAsiento.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el número de fila del asiento";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtFilaAsiento.Text.All(char.IsNumber) == false)
            {
                lblStatus.Text = "Fila del asiento debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (drpDisponibilidad.SelectedValue.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el estado del asiento";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }

        private bool ValidarModificar(string asiLetra, string asiFila, string asiEstado)
        {

            if (asiLetra.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la letra del asiento";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (asiLetra.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Letra Asiento no puede ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (asiFila.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el número de fila del asiento";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (asiFila.All(char.IsNumber) == false)
            {
                lblStatus.Text = "Fila del asiento debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (asiEstado.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el estado del asiento";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
    }
}