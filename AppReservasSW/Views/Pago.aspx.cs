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
    public partial class Pago : System.Web.UI.Page
    {
        IEnumerable<Models.Pago> pagos = new ObservableCollection<Models.Pago>();
        PagoManager pagoManager = new PagoManager();

        IEnumerable<Models.Reserva> reservas = new ObservableCollection<Models.Reserva>();
        ReservaManager reservaManager = new ReservaManager();

        IEnumerable<Models.TipoPago> tipoPagos = new ObservableCollection<Models.TipoPago>();
        TipoPagoManager tipoPagoManager = new TipoPagoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        private async void InicializarControles()
        {

            pagos = await pagoManager.obtenerPagos(VG.usuarioActual.CadenaToken);
            grdPago.DataSource = pagos.ToList();
            grdPago.DataBind();


            reservas = await reservaManager.ObtenerReservas(VG.usuarioActual.CadenaToken);

            drpCodigoReservas.Items.Clear();
            foreach (Models.Reserva reserva in reservas)
            {
                drpCodigoReservas.Items.Insert(0, new ListItem(Convert.ToString(reserva.RES_CODIGO), Convert.ToString(reserva.RES_CODIGO)));

            }
            drpTipoPago.Items.Clear();
            tipoPagos = await tipoPagoManager.ObtenerTipoPagos(VG.usuarioActual.CadenaToken);
            foreach (Models.TipoPago tipoPago in tipoPagos)
            {
                drpTipoPago.Items.Insert(0, new ListItem (tipoPago.TPA_DESCRIPCION, Convert.ToString(tipoPago.TPA_CODIGO)));
            }


            //aca agregar el tipo de pago automatico

            //reservas = await reservaManager.ObtenerReservas(VG.usuarioActual.CadenaToken);

            //drpCodigoReservas.Items.Clear();
            //foreach (Models.Reserva reserva in reservas)
            //{
            //    drpCodigoReservas.Items.Insert(0, new ListItem(Convert.ToString(reserva.RES_CODIGO), Convert.ToString(reserva.RES_CODIGO)));

            //}
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Pago pagoIngresada = new Models.Pago();
                Models.Pago pago = new Models.Pago()
                {
                
                    PAG_FECHA = Convert.ToDateTime(txtpagoFecha.Text),
                    TPA_CODIGO = Convert.ToInt32(drpTipoPago.SelectedValue.ToString()),
                    RES_CODIGO = Convert.ToInt32(drpCodigoReservas.SelectedValue.ToString()),
                    PAG_ESTADO = drpDisponibilidad.SelectedValue.ToString()
                    
                };

                pagoIngresada =
                    await pagoManager.Ingresar(pago, VG.usuarioActual.CadenaToken);

                if (pagoIngresada != null)
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

        private bool ValidarInsertar()
        {

            if (txtpagoFecha.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del usuario";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }



            if (drpTipoPago.SelectedValue.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el tipo de pago";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (drpCodigoReservas.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el codigo de la reservacion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }

        private bool ValidarModificar(string RES_CODIGO, string PAG_FECHA, string TPA_CODIGO, string PAG_ESTADO)
        {

            //if (PAG_CODIGO.IsNullOrWhiteSpace())
            //{
            //    lblStatus.Text = "Debe ingresar el código del usuario";
            //    lblStatus.ForeColor = Color.Maroon;
            //    lblStatus.Visible = true;
            //    return false;
            //}

            if (RES_CODIGO.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código de la reservacion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (TPA_CODIGO.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe debe ingresar el tipo de pago";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            return true;
        }

        protected async void grdPago_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoPagoEliminado = string.Empty;
            Label lblCode = (Label)grdPago.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoPago");

            string codigoPago = lblCode.Text;

            codigoPagoEliminado = await pagoManager.Eliminar(codigoPago, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoPagoEliminado))
            {
                lblStatus.Text = "Pago eliminado correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar el pago";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        protected void grdPago_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdPago.EditIndex = e.NewEditIndex;
            InicializarControles();

        }

        protected void grdPago_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdPago.EditIndex = -1;
            InicializarControles();
        }

        protected async void grdPago_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdPago.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoPago");


            string RES_CODIGO = (grdPago.Rows[e.RowIndex].FindControl("drpCodigoReservaEdit") as DropDownList).Text.Trim();
            string PAG_FECHA = (grdPago.Rows[e.RowIndex].FindControl("txtPagFecha") as TextBox).Text.Trim();
            string TPA_CODIGO = (grdPago.Rows[e.RowIndex].FindControl("drpTipoPagoEdit") as DropDownList).Text.Trim();
            string PAG_ESTADO = (grdPago.Rows[e.RowIndex].FindControl("drpEstadoEdit") as DropDownList).Text;



            if (ValidarModificar(RES_CODIGO, PAG_FECHA, TPA_CODIGO, PAG_ESTADO))
            {
                Models.Pago PagoModificado = new Models.Pago();
                Models.Pago pago = new Models.Pago()
                {
                    PAG_CODIGO = Convert.ToInt32(lblCode.Text),
                    RES_CODIGO = Convert.ToInt32(RES_CODIGO),
                    PAG_FECHA = Convert.ToDateTime(PAG_FECHA),
                    TPA_CODIGO = Convert.ToInt32(TPA_CODIGO),
                    PAG_ESTADO = PAG_ESTADO
                };

                PagoModificado =
                    await pagoManager.Actualizar(pago, VG.usuarioActual.CadenaToken);

                if (PagoModificado != null)
                {
                    lblStatus.Text = "Pago modificada correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el pago";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdPago.EditIndex = -1;
                InicializarControles();
            }

        }

        protected async void grdPago_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {

                DropDownList ddList = (DropDownList)e.Row.FindControl("drpCodigoReservaEdit");

                reservas = await reservaManager.ObtenerReservas(VG.usuarioActual.CadenaToken);

                foreach (Models.Reserva reserva in reservas)
                {
                    ddList.Items.Insert(0, new ListItem(Convert.ToString(reserva.RES_CODIGO), Convert.ToString(reserva.RES_CODIGO)));
                }

            }


            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("drpTipoPagoEdit");

                tipoPagos = await tipoPagoManager.ObtenerTipoPagos(VG.usuarioActual.CadenaToken);

                foreach (Models.TipoPago tipoPago in tipoPagos)
                {
                    ddList.Items.Insert(0, new ListItem(tipoPago.TPA_DESCRIPCION, Convert.ToString(tipoPago.TPA_CODIGO)));
                }
            }

        }


    }
}