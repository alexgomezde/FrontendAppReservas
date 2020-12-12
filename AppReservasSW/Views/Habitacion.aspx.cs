using AppReservasSW.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Microsoft.Ajax.Utilities;
using System.Data;

namespace AppReservasSW.Views
{
    public partial class Habitacion : System.Web.UI.Page
    {
        IEnumerable<Models.Habitacion> habitaciones = new ObservableCollection<Models.Habitacion>();
        HabitacionManager habitacionManager = new HabitacionManager();
        IEnumerable<Models.Hotel> hoteles = new ObservableCollection<Models.Hotel>();
        HotelManager hotelManager = new HotelManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        private async void InicializarControles()
        {

            habitaciones = await habitacionManager.ObtenerHabitaciones(VG.usuarioActual.CadenaToken);
            grdHabitaciones.DataSource = habitaciones.ToList();
            grdHabitaciones.DataBind();

            hoteles = await hotelManager.ObtenerHoteles(VG.usuarioActual.CadenaToken);

            drpNombreHoteles.Items.Clear();
            foreach (Models.Hotel hotel in hoteles)
            {
                drpNombreHoteles.Items.Insert(0, new ListItem(hotel.HOT_NOMBRE, Convert.ToString(hotel.HOT_CODIGO)));
            }
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Habitacion habitacionIngresada = new Models.Habitacion();
                Models.Habitacion habitacion = new Models.Habitacion()
                {
                    HOT_CODIGO = Convert.ToInt32(drpNombreHoteles.SelectedValue.ToString()),
                    HAB_NUMERO = Convert.ToInt32(txtNumeroHabitacion.Text),
                    HAB_CAPACIDAD = Convert.ToInt32(txtCapacidad.Text),
                    HAB_TIPO = txtTipo.Text,
                    HAB_DESCRIPCION = txtDescripcion.Text,
                    HAB_ESTADO = drpDisponibilidad.SelectedValue.ToString(),
                    HAB_PRECIO = Convert.ToDecimal(txtPrecio.Text)
                };

                habitacionIngresada =
                    await habitacionManager.Ingresar(habitacion, VG.usuarioActual.CadenaToken);

                if (habitacionIngresada != null)
                {
                    lblStatus.Text = "Habitación ingresada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar la habitación";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        protected async void grdHabitaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoHabitacionEliminada = string.Empty;
            Label lblCode = (Label)grdHabitaciones.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoHabitacion");

            string codigoHabitacion = lblCode.Text;

            codigoHabitacionEliminada = await habitacionManager.Eliminar(codigoHabitacion, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoHabitacionEliminada))
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

        protected async void grdHabitaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdHabitaciones.EditIndex = e.NewEditIndex;
            InicializarControles();

        }

        protected void grdHabitaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdHabitaciones.EditIndex = -1;
            InicializarControles();
        }



        protected async void grdHabitaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdHabitaciones.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoHabitacion");

            string hotCodHotel = (grdHabitaciones.Rows[e.RowIndex].FindControl("drpNombreHotelesEdit") as DropDownList).Text;
            string habNumero = (grdHabitaciones.Rows[e.RowIndex].FindControl("txtHabNumHab") as TextBox).Text;
            string habCapacidad = (grdHabitaciones.Rows[e.RowIndex].FindControl("txtHabCapacidad") as TextBox).Text.Trim();
            string habTipo = (grdHabitaciones.Rows[e.RowIndex].FindControl("txtHabTipo") as TextBox).Text.Trim();
            string habDescripcion = (grdHabitaciones.Rows[e.RowIndex].FindControl("txtHabDescripcion") as TextBox).Text.Trim();
            string habEstado = (grdHabitaciones.Rows[e.RowIndex].FindControl("drpDisponibilidadEdit") as DropDownList).Text;
            string habPrecio = (grdHabitaciones.Rows[e.RowIndex].FindControl("txtHabPrecio") as TextBox).Text.Trim();

            if (ValidarModificar(hotCodHotel, habNumero, habCapacidad, habTipo, habDescripcion, habEstado, habPrecio))
            {
                Models.Habitacion habitacionModificada = new Models.Habitacion();
                Models.Habitacion habitacion = new Models.Habitacion()
                {
                    HAB_CODIGO = Convert.ToInt32(lblCode.Text),
                    HOT_CODIGO = Convert.ToInt32(hotCodHotel),
                    HAB_NUMERO = Convert.ToInt32(habNumero),
                    HAB_CAPACIDAD = Convert.ToInt32(habCapacidad),
                    HAB_TIPO = habTipo,
                    HAB_DESCRIPCION = habDescripcion,
                    HAB_ESTADO = habEstado,
                    HAB_PRECIO = Convert.ToDecimal(habPrecio)
                };

                habitacionModificada =
                    await habitacionManager.Actualizar(habitacion, VG.usuarioActual.CadenaToken);

                if (habitacionModificada != null)
                {
                    lblStatus.Text = "Habitación modificada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar la habitación";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdHabitaciones.EditIndex = -1;
                InicializarControles();
            }
        }

        private bool ValidarInsertar()
        {

            if (txtNumeroHabitacion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el número de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtCapacidad.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la capacidad de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtTipo.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el tipo de habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtDescripcion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la descripción de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtPrecio.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el precio de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }


        private bool ValidarModificar(string hotCodHotel, string habNumero, string habCapacidad, string habTipo, string habDescripcion, string habEstado, string habPrecio)
        {
            if (hotCodHotel.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (habNumero.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el número de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (habCapacidad.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la capacidad de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (habTipo.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el tipo de habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (habDescripcion.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la descripción de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (habPrecio.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el precio de la habitación";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (habPrecio.All(char.IsLetter) == true)
            {
                lblStatus.Text = "Fila del precio debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            return true;
        }

        protected async void grdHabitaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {

                DropDownList ddList = (DropDownList)e.Row.FindControl("drpNombreHotelesEdit");

                hoteles = await hotelManager.ObtenerHoteles(VG.usuarioActual.CadenaToken);

                foreach (Models.Hotel hotel in hoteles)
                {
                    ddList.Items.Insert(0, new ListItem(hotel.HOT_NOMBRE, Convert.ToString(hotel.HOT_CODIGO)));
                }

            }
        }
    }
}