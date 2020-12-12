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
    public partial class Reserva : System.Web.UI.Page
    {
        IEnumerable<Models.Reserva> reservaciones = new ObservableCollection<Models.Reserva>();
        ReservaManager reservaManager = new ReservaManager();

        IEnumerable<Models.Usuario> usuarios = new ObservableCollection<Models.Usuario>();
        UsuarioManager usuarioManager = new UsuarioManager();

        IEnumerable<Models.Habitacion> habitaciones = new ObservableCollection<Models.Habitacion>();
        HabitacionManager habitacionManager = new HabitacionManager();

        IEnumerable<Models.Vuelo> vuelos = new ObservableCollection<Models.Vuelo>();
        VueloManager vueloManager = new VueloManager();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        private async void InicializarControles()
        {

            reservaciones = await reservaManager.ObtenerReservas(VG.usuarioActual.CadenaToken);
            grdReservas.DataSource = reservaciones.ToList();
            grdReservas.DataBind();


            habitaciones = await habitacionManager.ObtenerHabitaciones(VG.usuarioActual.CadenaToken);

            drpCodigosHabitacion.Items.Clear();
            foreach (Models.Habitacion habitacion in habitaciones)
            {
                drpCodigosHabitacion.Items.Insert(0, new ListItem(habitacion.HAB_DESCRIPCION, Convert.ToString(habitacion.HAB_CODIGO)));
            }


            vuelos = await vueloManager.ObtenerVuelos(VG.usuarioActual.CadenaToken);

            drpVuelo.Items.Clear();
            foreach (Models.Vuelo vuelo in vuelos)
            {
                drpVuelo.Items.Insert(0, new ListItem(Convert.ToString(vuelo.VUE_CODIGO), Convert.ToString(vuelo.VUE_CODIGO)));
            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Reserva reservaIngresada = new Models.Reserva();
                Models.Reserva reserva = new Models.Reserva()
                {


                    USU_CODIGO = Convert.ToInt32(txtUsuarioCodigo.Text),
                    HAB_CODIGO = Convert.ToInt32(drpCodigosHabitacion.SelectedValue.ToString()),
                    VUE_CODIGO = Convert.ToInt32(drpVuelo.SelectedValue.ToString()),
                    RES_COSTO = Convert.ToDecimal(txtCosto.Text),
                    RES_FECHA_INGRESO = Convert.ToDateTime(txtFechaIngreso.Text),
                    RES_FECHA_SALIDA = Convert.ToDateTime(txtFechaSalida.Text),
                    RES_FECHA_VUELO = Convert.ToDateTime(txtFechaVuelo.Text),
                    RES_ESTADO = drpEstado.SelectedValue.ToString()

                };

                reservaIngresada =
                    await reservaManager.Ingresar(reserva, VG.usuarioActual.CadenaToken);

                if (reservaIngresada != null)
                {
                    lblStatus.Text = "Reserva ingresada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar la reservación";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }



        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }


        private bool ValidarInsertar()
        {

            if (txtUsuarioCodigo.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del usuario";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }



            if (txtFechaIngreso.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la fecha de ingreso";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtFechaSalida.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la fecha de salida";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }


        private bool ValidarModificar(string usuCodigo, string habCodigo, string vueCodigo, string resCosto, string fecIngreso, string fecSalida, string fecVuelo, string hotCodHotel)
        {

            if (usuCodigo.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del usuario";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (habCodigo.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (resCosto.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el costo de la reservación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (fecIngreso.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la fecha de ingreso";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (fecSalida.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la fecha de salida";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (fecVuelo.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la fecha de vuelo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }

        protected async void grdReservas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoReservaEliminada = string.Empty;
            Label lblCode = (Label)grdReservas.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoReserva");

            string codigoReserva = lblCode.Text;

            codigoReservaEliminada = await reservaManager.Eliminar(codigoReserva, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoReservaEliminada))
            {
                lblStatus.Text = "Habitación eliminada correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        protected void grdReservas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdReservas.EditIndex = e.NewEditIndex;
            InicializarControles();

        }

        protected void grdReservas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdReservas.EditIndex = -1;
            InicializarControles();
        }

        protected async void grdReservas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdReservas.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoReserva");

            string usuCodigo = (grdReservas.Rows[e.RowIndex].FindControl("txtUsuCodigo") as TextBox).Text;
            string habCodigo = (grdReservas.Rows[e.RowIndex].FindControl("drpHabitacionEdit") as DropDownList).Text;
            string vueCodigo = (grdReservas.Rows[e.RowIndex].FindControl("drpVueloEdit") as DropDownList).Text;
            string resCosto = (grdReservas.Rows[e.RowIndex].FindControl("txtResCosto") as TextBox).Text;
            string fecIngreso = (grdReservas.Rows[e.RowIndex].FindControl("txtResFechaIngreso") as TextBox).Text;
            string fecSalida = (grdReservas.Rows[e.RowIndex].FindControl("txtResFechaSalida") as TextBox).Text;
            string fecVuelo = (grdReservas.Rows[e.RowIndex].FindControl("txtResFechaVuelo") as TextBox).Text;
            string hotCodHotel = (grdReservas.Rows[e.RowIndex].FindControl("drpEstadoEdit") as DropDownList).Text;
            


            if (ValidarModificar(usuCodigo, habCodigo, vueCodigo, resCosto, fecIngreso, fecSalida, fecVuelo, hotCodHotel))
            {
                Models.Reserva reservaModificada = new Models.Reserva();
                Models.Reserva reserva = new Models.Reserva()
                {
                    RES_CODIGO = Convert.ToInt32(lblCode.Text),
                    USU_CODIGO = Convert.ToInt32(usuCodigo),
                    HAB_CODIGO = Convert.ToInt32(habCodigo),
                    VUE_CODIGO = Convert.ToInt32(vueCodigo),
                    RES_COSTO = Convert.ToDecimal(resCosto),
                    RES_FECHA_INGRESO = Convert.ToDateTime(fecIngreso),
                    RES_FECHA_SALIDA = Convert.ToDateTime(fecSalida),
                    RES_FECHA_VUELO = Convert.ToDateTime(fecVuelo),
                    RES_ESTADO = hotCodHotel
                };

                reservaModificada =
                    await reservaManager.Actualizar(reserva, VG.usuarioActual.CadenaToken);

                if (reservaModificada != null)
                {
                    lblStatus.Text = "Reserva modificada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar la reservación";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdReservas.EditIndex = -1;
                InicializarControles();
            }

        }

        protected async void grdReservas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {

                DropDownList ddList = (DropDownList)e.Row.FindControl("drpHabitacionEdit");

                habitaciones = await habitacionManager.ObtenerHabitaciones(VG.usuarioActual.CadenaToken);

                foreach (Models.Habitacion habitacion in habitaciones)
                {
                    ddList.Items.Insert(0, new ListItem(habitacion.HAB_DESCRIPCION, Convert.ToString(habitacion.HAB_CODIGO)));
                }

                DropDownList ddList2 = (DropDownList)e.Row.FindControl("drpVueloEdit");

                vuelos = await vueloManager.ObtenerVuelos(VG.usuarioActual.CadenaToken);

                foreach (Models.Vuelo vuelo in vuelos)
                {
                    ddList2.Items.Insert(0, new ListItem(Convert.ToString(vuelo.VUE_CODIGO), Convert.ToString(vuelo.VUE_CODIGO)));
                }
            }
        }
    }
}