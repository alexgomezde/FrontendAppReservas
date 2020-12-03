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
    public partial class TipoPago : System.Web.UI.Page
    {
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
            tipoPagos = await tipoPagoManager.ObtenerTipoPagos(VG.usuarioActual.CadenaToken);
            grdTipoPago.DataSource = tipoPagos.ToList();
            grdTipoPago.DataBind();



        }

        protected void grdTipoPago_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdTipoPago.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected async void grdTipoPago_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoTipoPagoEliminado = string.Empty;
            Label lblCode = (Label)grdTipoPago.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoTipoPago");

            string codigoTipoPago = lblCode.Text;

            codigoTipoPagoEliminado = await tipoPagoManager.Eliminar(codigoTipoPago, VG.usuarioActual.CadenaToken);


            if (!string.IsNullOrEmpty(codigoTipoPagoEliminado))
            {
                lblStatus.Text = "Tipo Pago eliminado correctamente";
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar el tipo pago";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }

        protected void grdTipoPago_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdTipoPago.EditIndex = -1;
            InicializarControles();
        }

        protected async void grdTipoPago_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdTipoPago.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoTipoPago");

            string TPA_DESC = (grdTipoPago.Rows[e.RowIndex].FindControl("txtTPAdesc") as TextBox).Text.Trim();

            if (ValidarModificar (TPA_DESC))
            {
                Models.TipoPago tipoPagoModificado = new Models.TipoPago();
                Models.TipoPago pago = new Models.TipoPago()
                {
                    TPA_CODIGO = Convert.ToInt32(lblCode.Text),
                    TPA_DESCRIPCION = TPA_DESC
                };

                tipoPagoModificado =
                    await tipoPagoManager.Actualizar(pago, VG.usuarioActual.CadenaToken);


                if(tipoPagoModificado != null)
                {
                    lblStatus.Text = "Tipo Pago modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el tipo de pago";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
                grdTipoPago.EditIndex = -1;
                InicializarControles();
            }

            


        }

        private bool ValidarModificar(string TPA_DESC)
        {

            if (TPA_DESC.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar LA DESCRIPCION DEL PAGO";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

           

            return true;
        }

        private bool ValidarInsertar()
        {

            if (txtDescripcion.Text.IsNullOrWhiteSpace())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#agregarModal').modal('show'); });</script>", false);
                lblStatus.Text = "Debe ingresar la descripcion del pago";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.TipoPago tipoPagoIngresado = new Models.TipoPago();
                Models.TipoPago Tipopago = new Models.TipoPago()
                {
                    TPA_DESCRIPCION = txtDescripcion.Text
                };

                tipoPagoIngresado = await tipoPagoManager.Ingresar(Tipopago, VG.usuarioActual.CadenaToken);

                if (tipoPagoIngresado != null)
                {

                    lblStatus.Text = "Tipo Pago ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar ";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

            }
            
        }




    }
}