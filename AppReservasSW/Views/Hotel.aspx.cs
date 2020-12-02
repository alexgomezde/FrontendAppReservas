using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasSW.Models;
using AppReservasSW.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;
using Microsoft.Ajax.Utilities;
using System.Windows;

namespace AppReservasSW.Views
{
    public partial class Hotel : System.Web.UI.Page
    {
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
            hoteles = await hotelManager.ObtenerHoteles(VG.usuarioActual.CadenaToken);
            grdHoteles.DataSource = hoteles.ToList();
            grdHoteles.DataBind();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Hotel hotelIngresado = new Models.Hotel();
                Models.Hotel hotel = new Models.Hotel()
                {
                    HOT_NOMBRE = txtNombre.Text,
                    HOT_EMAIL = txtEmail.Text,
                    HOT_DIRECCION = txtDireccion.Text,
                    HOT_CATEGORIA = drpCategoria.SelectedValue.ToString(),
                    HOT_TELEFONO = txtTelefono.Text
                };

                hotelIngresado =
                    await hotelManager.Ingresar(hotel, VG.usuarioActual.CadenaToken);

                if (hotelIngresado != null)
                {
                    lblStatus.Text = "Hotel ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el hotel";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        protected async void grdHoteles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#deleteConfirmationModal').modal('show'); });</script>", false);

            string codigoHotelEliminado = string.Empty;

            Label lblCode = (Label)grdHoteles.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoHotel");

            string codigoHotel = lblCode.Text;

            codigoHotelEliminado = await hotelManager.Eliminar(codigoHotel, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoHotelEliminado))
            {
                lblStatus.Text = "Hotel eliminado correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar el hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
            
        }

        protected void grdHoteles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdHoteles.EditIndex = e.NewEditIndex;
            InicializarControles();

            //DropDownList ddList = (DropDownList)grdHoteles.Rows[e.NewEditIndex].FindControl("drpCategoriaEdit");
            //Label lblSelectedCat = (Label)grdHoteles.Rows[e.NewEditIndex].FindControl("lblCategoria");

            //if (ddList != null)
            //{
            //    for (int i = 1; i < 6; i++)
            //    {
            //        ddList.Items.Insert(0, new ListItem(i + " Estrellas", Convert.ToString(i)));
            //    }

            //}
            

            //ddList.SelectedValue = lblSelectedCat.Text;
        }

        protected void grdHoteles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdHoteles.EditIndex = -1;
            InicializarControles();
        }

        protected async void grdHoteles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label lblCode = (Label)grdHoteles.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoHotel");
            
            string hotNombre = (grdHoteles.Rows[e.RowIndex].FindControl("txtHotNombre") as TextBox).Text.Trim();
            string hotEmail = (grdHoteles.Rows[e.RowIndex].FindControl("txtHotEmail") as TextBox).Text.Trim();
            string hotDireccion = (grdHoteles.Rows[e.RowIndex].FindControl("txtHotDireccion") as TextBox).Text.Trim();
            string hotCategoria = (grdHoteles.Rows[e.RowIndex].FindControl("drpCategoriaEdit") as DropDownList).Text;
            string hotTelefono = (grdHoteles.Rows[e.RowIndex].FindControl("txtHotTelefono") as TextBox).Text.Trim();

            if (ValidarModificar(hotNombre, hotEmail, hotDireccion, hotCategoria, hotTelefono))
            {
                Models.Hotel hotelModificado = new Models.Hotel();
                Models.Hotel hotel = new Models.Hotel()
                {
                    HOT_CODIGO = Convert.ToInt32(lblCode.Text),
                    HOT_NOMBRE = hotNombre,
                    HOT_EMAIL = hotEmail,
                    HOT_DIRECCION = hotDireccion,
                    HOT_CATEGORIA = hotCategoria,
                    HOT_TELEFONO = hotTelefono
                };

                hotelModificado =
                    await hotelManager.Actualizar(hotel, VG.usuarioActual.CadenaToken);

                if (hotelModificado != null)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#successModal').modal('show'); });</script>", false);
                    lblStatus.Text = "Hotel modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el hotel";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdHoteles.EditIndex = -1;
                InicializarControles();

            }
        }

        protected void grdHoteles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool ValidarInsertar()
        {

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#agregarModal').modal('show'); });</script>", false);
                lblStatus.Text = "Debe ingresar el nombre del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtEmail.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el email del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtDireccion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la direccion del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtTelefono.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el telefono del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }


        private bool ValidarModificar(string nombre, string email, string direccion, string categoria, string telefono)
        {

            if (nombre.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (email.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el email del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (direccion.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la direccion del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (telefono.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el telefono del hotel";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }


    }    
}