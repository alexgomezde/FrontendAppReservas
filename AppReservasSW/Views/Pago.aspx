<%@ Page Async="true" Language="C#" MasterPageFile="../Site.Master" AutoEventWireup="true" CodeBehind="Pago.aspx.cs" Inherits="AppReservasSW.Views.Pago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
        
    <asp:GridView ID="grdPago" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
            AutoGenerateColumns="False"  OnRowDeleting="grdPago_RowDeleting" OnRowEditing="grdPago_RowEditing" 
        OnRowCancelingEdit="grdPago_RowCancelingEdit" OnRowUpdating="grdPago_RowUpdating" OnRowDataBound="grdPago_RowDataBound" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

        <Columns>
            <asp:TemplateField HeaderText="Código">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoPago" Text='<%# Eval("PAG_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtcodigoPago" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Código Reserva">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoReserva" Text='<%# Eval("RES_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpCodigoReservaEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtResCodigoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha de Pago">
                <ItemTemplate>
                    <%# Eval("PAG_FECHA")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPagFecha" Text='<%# Eval("PAG_FECHA") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPagFechaFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Tipo de Pago">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoTipoPago" Text='<%# Eval("TPA_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpTipoPagoEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTipoPagoCodigoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label ID="lblEstadoPago" Text='<%# Eval("PAG_ESTADO") %>' runat="server" CssClass="GridViewEditRow" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpEstadoEdit" runat="server">
                        <asp:ListItem Value="A">Activo</asp:ListItem>
                        <asp:ListItem Value="N">Anulado</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtEstadoPagoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="/Images/edit_icon.png" runat="server" CommandName="Edit" ToolTip="Editar" type="button" />
                    <asp:ImageButton ImageUrl="/Images/trash_icon.png" runat="server" CommandName="Delete" ToolTip="Eliminar" type="button" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ImageUrl="/Images/check.png" runat="server" CommandName="Update" ToolTip="Actualizar" />
                    <asp:ImageButton ImageUrl="/Images/close.png" runat="server" CommandName="Cancel" ToolTip="Cancelar" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
      

    <div class="col-md-6">
        <div class="form-inline mb-3">
            <asp:Label ID="Label9" runat="server" Text="Reservas" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:DropDownList ID="drpCodigoReservas" runat="server" CssClass="form-control col-md-8">
             </asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label3" runat="server" Text="Fecha Pago" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtpagoFecha" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3"> 
            <asp:Label ID="Label10" runat="server" Text="Tipo de pago" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpTipoPago" runat="server" CssClass="form-control col-md-8"></asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label  runat="server" Text="Pago Estado" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpDisponibilidad" runat="server" CssClass="form-control col-md-8">
                <asp:ListItem Selected="True" Value="D">Disponible</asp:ListItem>
                <asp:ListItem Value="N">No Disponible</asp:ListItem>
             </asp:DropDownList>
        </div>
        <p></p>


        <div class="form-inline mt-3 mb-1">
            <asp:Button ID="btnAgregar" CssClass="btn btn-primary col-md-12 mt-3" runat="server" Text="Agregar" OnClick="btnIngresar_Click"  />
            <br />
            <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
        </div>
        
        

    </div>
 
</asp:Content>

