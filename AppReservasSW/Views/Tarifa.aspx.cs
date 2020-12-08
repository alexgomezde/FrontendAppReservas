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
    public partial class Tarifa : System.Web.UI.Page
    {
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

            tarifas = await tarifaManager.ObtenerTarifas(VG.usuarioActual.CadenaToken);
            grdTarifa.DataSource = tarifas.ToList();
            grdTarifa.DataBind();
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Tarifa tarifaIngresado = new Models.Tarifa();
                Models.Tarifa tarifa = new Models.Tarifa()
                {
                
                    TAR_CLASE = txtClase.Text,
                    TAR_PRECIO = Convert.ToDecimal(txtPrecio.Text),
                    TAR_IMPUESTO = Convert.ToInt32(txtImpuesto.Text),
                    TAR_ESTADO = drpEstado.SelectedValue.ToString()

                };

                tarifaIngresado =
                    await tarifaManager.Ingresar(tarifa, VG.usuarioActual.CadenaToken);

                if (tarifaIngresado != null)
                {
                    lblStatus.Text = "Tarifa ingresada correctamente";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar la tarifa";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }


        protected async void grdTarifa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string codigoTarifaEliminado = string.Empty;
            Label lblCode = (Label)grdTarifa.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoTarifa");

            string codigoTarifa = lblCode.Text;

            codigoTarifaEliminado = await tarifaManager.Eliminar(codigoTarifa, VG.usuarioActual.CadenaToken);

            if (!string.IsNullOrEmpty(codigoTarifaEliminado))
            {
                lblStatus.Text = "Tarifa eliminada correctamente";
                lblStatus.ForeColor = Color.Green;
                lblStatus.Visible = true;
                InicializarControles();
            }
            else
            {
                lblStatus.Text = "Hubo un error al eliminar la tarifa";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }

        }


        protected void grdTarifa_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdTarifa.EditIndex = e.NewEditIndex;
            InicializarControles();
        }

        protected void grdTarifa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdTarifa.EditIndex = -1;
            InicializarControles();
        }



        protected async void grdTarifa_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCode = (Label)grdTarifa.Rows[e.RowIndex].Cells[0].FindControl("lblCodigoTarifa");

            string tarClase = (grdTarifa.Rows[e.RowIndex].FindControl("txtClaseEdit") as TextBox).Text;
            string tarPrecio = (grdTarifa.Rows[e.RowIndex].FindControl("txtPrecioEdit") as TextBox).Text;
            string tarImpuesto = (grdTarifa.Rows[e.RowIndex].FindControl("txtImpuestoEdit") as TextBox).Text;
            string tarEstado = (grdTarifa.Rows[e.RowIndex].FindControl("drpEstadoEdit") as DropDownList).Text;

            if (ValidarModificar(tarClase, tarPrecio, tarImpuesto, tarEstado))
            {
                Models.Tarifa tarifaModificado = new Models.Tarifa();
                Models.Tarifa tarifa = new Models.Tarifa()
                {
                    TAR_CODIGO = Convert.ToInt32(lblCode.Text),
                    TAR_CLASE = tarClase,
                    TAR_PRECIO = Convert.ToDecimal(tarPrecio),
                    TAR_IMPUESTO = Convert.ToInt32(tarImpuesto),
                    TAR_ESTADO = tarEstado
                };

                tarifaModificado =
                    await tarifaManager.Actualizar(tarifa, VG.usuarioActual.CadenaToken);

                if (tarifaModificado != null)
                {
                    lblStatus.Text = "Tarifa modificada correctamente";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar la tarifa";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }

                grdTarifa.EditIndex = -1;
                InicializarControles();
            }

        }


        protected void grdTarifa_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
       

        private bool ValidarInsertar()
        {

            if (txtClase.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la clase";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtClase.Text.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Fila clase no acepta números";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtPrecio.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el precio";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtPrecio.Text.All(char.IsNumber) == false)
            {
                lblStatus.Text = "Fila precio debe de ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            if (txtImpuesto.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el impuesto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtImpuesto.Text.All(char.IsNumber) == false)
            {
                lblStatus.Text = "Fila impuesto debe de ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            
            if (drpEstado.SelectedValue.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el estado de la tarifa";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
 
        private bool ValidarModificar(string tarClase, string tarPrecio, string tarImpuesto, string tarEstado)
        {

            if (tarClase.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la clase de la tarifa";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (tarClase.All(char.IsNumber) == true)
            {
                lblStatus.Text = "Fila de clase no acepta números";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            if (tarPrecio.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe  ingresar el precio";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (tarPrecio.All(char.IsNumber) == false)
            {
                lblStatus.Text = "Fila del precio debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            if (tarImpuesto.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe  ingresar el impuesto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (tarImpuesto.All(char.IsNumber) == false)
            {
                lblStatus.Text = "Fila del impuesto debe ser un número";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            if (tarEstado.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el estado de la tarifa";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
    }
}