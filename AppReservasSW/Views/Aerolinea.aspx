<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aerolinea.aspx.cs" Inherits="AppReservasSW.Views.Aerolinea" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grdAerolineas" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
        AutoGenerateColumns="False" OnRowDeleting="grdAerolineas_RowDeleting" OnRowEditing="grdAerolineas_RowEditing" OnRowCancelingEdit="grdAerolineas_RowCancelingEdit"
         OnRowUpdating="grdAerolineas_RowUpdating"> 
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
                    <asp:Label ID="lblCodigoAerolinea" Text='<%# Eval("AER_CODIGO") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="Aerolinea Ruc">
                <ItemTemplate>
                    <asp:Label ID="lblAerolineaRuc" Text='<%# Eval("AER_RUC") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                     <asp:TextBox ID="txtAerolineaRucEdit" Text='<%# Eval("AER_RUC") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Nombre Aerolinea">
                <ItemTemplate>
                    <asp:Label ID="lblAerolineaNombre" Text='<%# Eval("AER_NOMBRE") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                  <asp:TextBox ID="txtAerolineaNombreEdit" Text='<%# Eval("AER_NOMBRE") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label ID="lblEstado" Text='<%# Eval("AER_ESTADO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpEstadoEdit" runat="server" SelectedValue='<%# Bind("AER_ESTADO") %>'>
                        <asp:ListItem Value="A">Activo</asp:ListItem>
                        <asp:ListItem Value="N">No Activo</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
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
            <asp:Label ID="Label1" runat="server" Text="Aerolinea Ruc" CssClass="col-form-label col-md-4"></asp:Label>
             <asp:TextBox ID="txtAerolineaRuc" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label2" runat="server" Text="Aerolinea Nombre" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:TextBox ID="txtAerolineaNombre" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label  runat="server" Text="Estado" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpEstado" runat="server" CssClass="form-control col-md-8">
                <asp:ListItem Selected="True" Value="A">Activa</asp:ListItem>
                <asp:ListItem Value="N">No Activa</asp:ListItem>
             </asp:DropDownList>
        </div>

        <div class="form-inline mt-3 mb-1">
            <asp:Button ID="btnAgregar" CssClass="btn btn-primary col-md-12 mt-3" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
            <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
        </div>

    </div>

</asp:Content>

