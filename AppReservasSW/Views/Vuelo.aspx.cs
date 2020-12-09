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
    public partial class Vuelo : System.Web.UI.Page
    {
        IEnumerable<Models.Vuelo> vuelos = new ObservableCollection<Models.Vuelo>();
        VueloManager vueloManager = new VueloManager();

        IEnumerable<Models.Aeropuerto> aeropuertos = new ObservableCollection<Models.Aeropuerto>();
        AeropuertoManager aeropuertoManager = new AeropuertoManager();

        IEnumerable<Models.Asiento> asientos = new ObservableCollection<Models.Asiento>();
        AsientoManager asientoManager = new AsientoManager();

        IEnumerable<Models.Avion> aviones = new ObservableCollection<Models.Avion>();
        AvionManager avionManager = new AvionManager();

        IEnumerable<Models.Tarifa> tarifas = new ObservableCollection<Models.Tarifa>();
        TarifaManager tarifaManager = new TarifaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }

        }

        private async void InicializarControles()
        {

            vuelos = await vueloManager.ObtenerVuelos(VG.usuarioActual.CadenaToken);
            grdVuelos.DataSource = vuelos.ToList();
            grdVuelos.DataBind();

            aeropuertos = await aeropuertoManager.ObtenerAeropuertos(VG.usuarioActual.CadenaToken);

            drpAeropuertoOrigen.Items.Clear();
            drpAeropuertoDestino.Items.Clear();

            foreach (Models.Aeropuerto aeropuerto in aeropuertos)
            {
                drpAeropuertoOrigen.Items.Insert(0, new ListItem(aeropuerto.AEP_CODIGO + " - " + aeropuerto.AEP_NOMBRE, Convert.ToString(aeropuerto.AEP_CODIGO)));
                drpAeropuertoDestino.Items.Insert(0, new ListItem(aeropuerto.AEP_CODIGO + " - " + aeropuerto.AEP_NOMBRE, Convert.ToString(aeropuerto.AEP_CODIGO)));
            }

            asientos = await asientoManager.ObtenerAsientos(VG.usuarioActual.CadenaToken);

            drpAsientos.Items.Clear();

            foreach (Models.Asiento asiento in asientos)
            {
                drpAsientos.Items.Insert(0, new ListItem( asiento.ASI_CODIGO + " - " + asiento.ASI_LETRA + asiento.ASI_FILA, Convert.ToString(asiento.ASI_CODIGO)));
            }

            aviones = await avionManager.ObtenerAviones(VG.usuarioActual.CadenaToken);

            drpAviones.Items.Clear();

            foreach (Models.Avion avion in aviones)
            {
                drpAviones.Items.Insert(0, new ListItem(avion.AVI_CODIGO + " - " + avion.AVI_FABRICANTE, Convert.ToString(avion.AVI_CODIGO)));
            }

            tarifas = await tarifaManager.ObtenerTarifas(VG.usuarioActual.CadenaToken);

            drpTarifas.Items.Clear();

            foreach (Models.Tarifa tarifa in tarifas)
            {
                drpTarifas.Items.Insert(0, new ListItem(tarifa.TAR_CODIGO + " - " + tarifa.TAR_CLASE, Convert.ToString(tarifa.TAR_CODIGO)));
            }


        }


        async protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Vuelo vueloIngresado = new Models.Vuelo();
                Models.Vuelo vuelo = new Models.Vuelo()
                {

                    VUE_CODIGO_ASI = Convert.ToInt32(drpAsientos.SelectedValue.ToString()),
                    AER_ORIGEN_COD = Convert.ToInt32(drpAeropuertoOrigen.SelectedValue.ToString()),
                    AER_DESTINO_COD = Convert.ToInt32(drpAeropuertoDestino.SelectedValue.ToString()),
                    AVI_CODIGO = Convert.ToInt32(drpAviones.SelectedValue.ToString()),
                    TAR_CODIGO = Convert.ToInt32(drpTarifas.SelectedValue.ToString()),
                    VUE_ESTADO = drpEstado.SelectedValue.ToString()

                };

                vueloIngresado =
                    await vueloManager.Ingresar(vuelo, VG.usuarioActual.CadenaToken);

                if (vueloIngresado != null)
                {
                    lblStatus.Text = "Vuelo ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el vuelo";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }

        }

        protected async void grdVuelos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoVueloEliminado = string.Empty;
            Label lblCode = (Label)grdVuelos.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoVuelo");

            string codigoVuelo = lblCode.Text;

            codigoVueloEliminado = await vueloManager.Eliminar(codigoVuelo, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoVueloEliminado))
            {
                lblStatus.Text = "Vuelo eliminado correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar el vuelo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }

        protected void grdVuelos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdVuelos.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected void grdVuelos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdVuelos.EditIndex = -1;
            InicializarControles();

        }

        protected async void grdVuelos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdVuelos.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoVuelo");

            string codAsiento = (grdVuelos.Rows[e.RowIndex].FindControl("drpCodigoAsientoEdit") as DropDownList).Text;
            string aerOrigen = (grdVuelos.Rows[e.RowIndex].FindControl("drpAeropuertoOrigenEdit") as DropDownList).Text;
            string aerDestino = (grdVuelos.Rows[e.RowIndex].FindControl("drpAeropuertoDestinoEdit") as DropDownList).Text;
            string codAvion = (grdVuelos.Rows[e.RowIndex].FindControl("drpAvionEdit") as DropDownList).Text;
            string codTarifa = (grdVuelos.Rows[e.RowIndex].FindControl("drpTarifaEdit") as DropDownList).Text;
            string estado = (grdVuelos.Rows[e.RowIndex].FindControl("drpEstadoEdit") as DropDownList).Text;


            if (ValidarModificar(codAsiento, aerOrigen, aerDestino, codAvion, codTarifa, estado))
            {
                Models.Vuelo vueloModificado = new Models.Vuelo();
                Models.Vuelo vuelo = new Models.Vuelo()
                {
                    VUE_CODIGO = Convert.ToInt32(lblCode.Text),
                    VUE_CODIGO_ASI = Convert.ToInt32(codAsiento),
                    AER_ORIGEN_COD = Convert.ToInt32(aerOrigen),
                    AER_DESTINO_COD = Convert.ToInt32(aerDestino),
                    AVI_CODIGO = Convert.ToInt32(codAvion),
                    TAR_CODIGO = Convert.ToInt32(codTarifa),
                    VUE_ESTADO = estado
                };

                vueloModificado =
                    await vueloManager.Actualizar(vuelo, VG.usuarioActual.CadenaToken);

                if (vueloModificado != null)
                {
                    lblStatus.Text = "Vuelo modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el vuelo";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdVuelos.EditIndex = -1;
                InicializarControles();
            }

        }

        protected async void grdVuelos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {

                
                DropDownList ddList = (DropDownList)e.Row.FindControl("drpAeropuertoOrigenEdit");
                DropDownList ddList2 = (DropDownList)e.Row.FindControl("drpAeropuertoDestinoEdit");

                aeropuertos = await aeropuertoManager.ObtenerAeropuertos(VG.usuarioActual.CadenaToken);

                foreach (Models.Aeropuerto aeropuerto in aeropuertos)
                {
                    ddList.Items.Insert(0, new ListItem(aeropuerto.AEP_CODIGO + " - " + aeropuerto.AEP_NOMBRE, Convert.ToString(aeropuerto.AEP_CODIGO)));
                    ddList2.Items.Insert(0, new ListItem(aeropuerto.AEP_CODIGO + " - " + aeropuerto.AEP_NOMBRE, Convert.ToString(aeropuerto.AEP_CODIGO)));
                }

                DropDownList ddListAsientos = (DropDownList)e.Row.FindControl("drpCodigoAsientoEdit");

                asientos = await asientoManager.ObtenerAsientos(VG.usuarioActual.CadenaToken);

                foreach (Models.Asiento asiento in asientos)
                {
                    ddListAsientos.Items.Insert(0, new ListItem(asiento.ASI_CODIGO + " - " + asiento.ASI_LETRA + asiento.ASI_FILA, Convert.ToString(asiento.ASI_CODIGO)));
                    
                }

                DropDownList ddAviones = (DropDownList)e.Row.FindControl("drpAvionEdit");

                aviones = await avionManager.ObtenerAviones(VG.usuarioActual.CadenaToken);

                foreach (Models.Avion avion in aviones)
                {
                    ddAviones.Items.Insert(0, new ListItem(avion.AVI_CODIGO + " - " + avion.AVI_FABRICANTE, Convert.ToString(avion.AVI_CODIGO)));
 
                }


                DropDownList ddlTarifas = (DropDownList)e.Row.FindControl("drpTarifaEdit");

                tarifas = await tarifaManager.ObtenerTarifas(VG.usuarioActual.CadenaToken);

                foreach (Models.Tarifa tarifa in tarifas)
                {
                    ddlTarifas.Items.Insert(0, new ListItem(tarifa.TAR_CODIGO + " - " + tarifa.TAR_CLASE, Convert.ToString(tarifa.TAR_CODIGO)));

                }

            }

        }

        private bool ValidarInsertar()
        {

            return true;
        }


        private bool ValidarModificar(string codAsiento, string aerOrigen, string aerDestino, string codAvion, string codTarifa, string estado)
        {

            if (codAsiento.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del asiento";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (codAvion.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del avión";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (codTarifa.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la tarifa del vuelo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }

    }
}