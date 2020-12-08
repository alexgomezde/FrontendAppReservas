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
    public partial class Factura : System.Web.UI.Page
    {
        IEnumerable<Models.Factura> facturas = new ObservableCollection<Models.Factura>();
        FacturaManager facturaManager = new FacturaManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        private async void InicializarControles()
        {

            facturas = await facturaManager.ObtenerFacturas(VG.usuarioActual.CadenaToken);
            grdFacturas.DataSource = facturas.ToList();
            grdFacturas.DataBind();
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Factura facturaIngresado = new Models.Factura();
                Models.Factura factura = new Models.Factura()
                {

                    PAG_CODIGO = Convert.ToInt32(txtPagoCodigo.Text),
                    FAC_COMPROBANTE = txtFacComprobante.Text,
                    FAC_ESTADO = drpDisponibilidad.SelectedValue.ToString()
                };

                facturaIngresado =
                    await facturaManager.Ingresar(factura, VG.usuarioActual.CadenaToken);

                if (facturaIngresado != null)
                {
                    lblStatus.Text = "Factura ingresada correctamente";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar la factura";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        protected async void grdFacturas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoFacturaEliminado = string.Empty;
            Label lblCode = (Label)grdFacturas.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoFactura");

            string codigoFactura = lblCode.Text;

            codigoFacturaEliminado = await facturaManager.Eliminar(codigoFactura, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoFacturaEliminado))
            {
                lblStatus.Text = "Factura eliminada correctamente";
                lblStatus.ForeColor = Color.Green;
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar la factura";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }

        protected void grdFacturas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdFacturas.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected void grdFacturas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdFacturas.EditIndex = -1;
            InicializarControles();
        }

        protected async void grdFacturas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdFacturas.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoFactura");

            string facCodigoPago = (grdFacturas.Rows[e.RowIndex].FindControl("txtPagoCodigoEdit") as TextBox).Text;
            string facComprobante = (grdFacturas.Rows[e.RowIndex].FindControl("txtFacComprobanteEdit") as TextBox).Text;
            string facEstado = (grdFacturas.Rows[e.RowIndex].FindControl("drpDisponibilidadEdit") as DropDownList).Text;

            if (ValidarModificar(facCodigoPago, facComprobante, facEstado))
            {
                Models.Factura facturaModificado = new Models.Factura();
                Models.Factura factura = new Models.Factura()
                {
                    FAC_CODIGO = Convert.ToInt32(lblCode.Text),
                    PAG_CODIGO = Convert.ToInt32(facCodigoPago),
                    FAC_COMPROBANTE = facComprobante,
                    FAC_ESTADO = facEstado
                };

                facturaModificado =
                    await facturaManager.Actualizar(factura, VG.usuarioActual.CadenaToken);

                if (facturaModificado != null)
                {
                    lblStatus.Text = "Factura modificada correctamente";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar la factura";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdFacturas.EditIndex = -1;
                InicializarControles();
            }

        }

        protected void grdFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private bool ValidarInsertar()
        {

            if (txtPagoCodigo.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del pago";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtPagoCodigo.Text.All(char.IsNumber) == false)
            {
                lblStatus.Text = "El código de pago debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtFacComprobante.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el comprobante";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtFacComprobante.Text.All(char.IsNumber) == false)
            {
                lblStatus.Text = "El comprobante debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (drpDisponibilidad.SelectedValue.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el estado de comprobante";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            return true;
        }

        private bool ValidarModificar(string facCodigoPago, string facComprobante, string facEstado)
        {

            if (facCodigoPago.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el código del pago";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            if (facCodigoPago.All(char.IsNumber) == false)
            {
                lblStatus.Text = "El código de pago debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            if (facComprobante.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el comprobante de factura";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (facComprobante.All(char.IsNumber) == false)
            {
                lblStatus.Text = "El comprobante debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (facEstado.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el estado de factura";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }


    }
}