<%@ Page Async="true" Language="C#" MasterPageFile="../Site.Master" AutoEventWireup="true" CodeBehind="TipoPago.aspx.cs" Inherits="AppReservasSW.Views.TipoPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:GridView ID="grdTipoPago" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" 
        
        OnRowEditing="grdTipoPago_RowEditing" OnRowDeleting="grdTipoPago_RowDeleting" OnRowCancelingEdit="grdTipoPago_RowCancelingEdit" OnRowUpdating="grdTipoPago_RowUpdating"
         >
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
                    <asp:Label ID="lblCodigoTipoPago" Text='<%# Eval("TPA_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTPACodigoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Descripcion">
                <ItemTemplate>
                    <%# Eval("TPA_DESCRIPCION")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtTPAdesc" Text='<%# Eval("TPA_DESCRIPCION") %>' runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTPAdescFooter" runat="server" />
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
            <asp:Label ID="Label3" runat="server" Text="Descripción" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>
          <div class="form-inline mt-3 mb-1">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary col-md-12" OnClick="btnIngresar_Click" />
        </div>
        
        <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
         </div>
</asp:Content>
