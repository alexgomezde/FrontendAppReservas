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
    public partial class Avion : System.Web.UI.Page
    {
        IEnumerable<Models.Avion> aviones = new ObservableCollection<Models.Avion>();
        AvionManager avionManager = new AvionManager();

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

            aviones = await avionManager.ObtenerAviones(VG.usuarioActual.CadenaToken);
            grdAviones.DataSource = aviones.ToList();
            grdAviones.DataBind();

            aeropuertos = await aeropuertoManager.ObtenerAeropuertos(VG.usuarioActual.CadenaToken);

            drpAeropuertoCodigo.Items.Clear();
      
            foreach (Models.Aeropuerto aeropuerto in aeropuertos)
            {
                drpAeropuertoCodigo.Items.Insert(0, new ListItem(aeropuerto.AEP_NOMBRE, Convert.ToString(aeropuerto.AEP_CODIGO)));
                
            }
        }


        async protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Avion avionIngresado = new Models.Avion();
                Models.Avion avion = new Models.Avion()
                {

                    AER_CODIGO = Convert.ToInt32(drpAeropuertoCodigo.SelectedValue.ToString()),
                    AVI_FABRICANTE = txtAvionFabricante.Text.ToString(),
                    AVI_TIPO = txtAvionTipo.Text.ToString(),
                    AVI_CAPACIDAD = Convert.ToInt32(txtAvionCapacidad.Text.ToString()),
                    AVI_ESTADO = drpEstado.SelectedValue.ToString()

                };

                avionIngresado =
                    await avionManager.Ingresar(avion, VG.usuarioActual.CadenaToken);

                if (avionIngresado != null)
                {
                    lblStatus.Text = "Avion ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el avion";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }

        }

        protected async void grdAviones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoAvionEliminado = string.Empty;
            Label lblCode = (Label)grdAviones.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoAvion");

            string codigoAvion = lblCode.Text;

            codigoAvionEliminado = await avionManager.Eliminar(codigoAvion, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoAvionEliminado))
            {
                lblStatus.Text = "Avion eliminado correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar el avion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }

        protected void grdAviones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAviones.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected void grdAviones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAviones.EditIndex = -1;
            InicializarControles();

        }

        protected async void grdAviones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdAviones.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoAvion");

            string aerCodigo = (grdAviones.Rows[e.RowIndex].FindControl("drpAeropuertoEdit") as TextBox).Text;
            string aviFabricante = (grdAviones.Rows[e.RowIndex].FindControl("drpAvionFabricanteEdit") as DropDownList).Text;
            string aviTipo = (grdAviones.Rows[e.RowIndex].FindControl("drpAvionTipoEdit") as DropDownList).Text;
            string aviCapacidad = (grdAviones.Rows[e.RowIndex].FindControl("txtAvionCapacidadEdit") as TextBox).Text;
            string aviEstado = (grdAviones.Rows[e.RowIndex].FindControl("drpEstadoEdit") as DropDownList).Text;


            if (ValidarModificar(aviFabricante, aviTipo, aviCapacidad))
            {
                Models.Avion avionModificado = new Models.Avion();
                Models.Avion avion = new Models.Avion()
                {
                    AVI_CODIGO = Convert.ToInt32(lblCode.Text),
                    AER_CODIGO = Convert.ToInt32(aerCodigo),
                    AVI_FABRICANTE = aviFabricante,
                    AVI_TIPO = aviTipo,
                    AVI_CAPACIDAD = Convert.ToInt32(aviCapacidad),
                    AVI_ESTADO = aviEstado
                };

                avionModificado =
                    await avionManager.Actualizar(avion, VG.usuarioActual.CadenaToken);

                if (avionModificado != null)
                {
                    lblStatus.Text = "Avion modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el avion";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdAviones.EditIndex = -1;
                InicializarControles();
            }

        }

        protected async void grdAviones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {

                DropDownList ddList = (DropDownList)e.Row.FindControl("drpAeropuertoEdit");

                aeropuertos = await aeropuertoManager.ObtenerAeropuertos(VG.usuarioActual.CadenaToken);

                foreach (Models.Aeropuerto aeropuerto in aeropuertos)
                {
                    ddList.Items.Insert(0, new ListItem(aeropuerto.AEP_NOMBRE, Convert.ToString(aeropuerto.AEP_CODIGO)));
                }

            }

        }

        private bool ValidarInsertar()
        {

            if (txtAvionFabricante.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del Fabricante";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtAvionTipo.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el tipo de avión";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtAvionCapacidad.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la capacidad del avion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }


        private bool ValidarModificar(string aviFabricante, string aviTipo, string aviCapacidad)
        {

            if (aviFabricante.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del Fabricante";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (aviTipo.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el Tipo de avion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (aviCapacidad.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la capacidad del Avion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
    }
}