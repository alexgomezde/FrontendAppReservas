<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Asiento.aspx.cs" Inherits="AppReservasSW.Views.Asiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grdAsientos" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"  AutoGenerateColumns="False"
         OnRowDeleting="grdAsientos_RowDeleting" OnRowEditing="grdAsientos_RowEditing" OnRowCancelingEdit="grdAsientos_RowCancelingEdit"
         OnRowUpdating="grdAsientos_RowUpdating" OnRowDataBound="grdAsientos_RowDataBound">
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
                    <asp:Label ID="lblCodigoAsiento" Text='<%# Eval("ASI_CODIGO") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Letra Asiento">
                <ItemTemplate>
                    <%# Eval("ASI_LETRA") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtLetraAsientoEdit" Text='<%# Eval("ASI_LETRA")%>' runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fila Asiento">
                <ItemTemplate>
                    <%# Eval("ASI_FILA") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFilaAsientoEdit" Text='<%# Eval("ASI_FILA")%>' runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado Asiento">
                <ItemTemplate>
                    <asp:Label ID="lblCategoria" Text='<%# Eval("ASI_ESTADO") %>' runat="server" CssClass="GridViewEditRow" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpDisponibilidadEdit" runat="server">
                        <asp:ListItem Value="D">Disponible</asp:ListItem>
                        <asp:ListItem Value="N">No Disponible</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHotCategoriaFooter" runat="server" />
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
            <asp:Label ID="Label1" runat="server" Text="Letra Asiento" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtLetraAsiento" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label3" runat="server" Text="Fila Asiento" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtFilaAsiento" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label  runat="server" Text="Estado" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpDisponibilidad" runat="server" CssClass="form-control col-md-8">
                <asp:ListItem Selected="True" Value="D">Disponible</asp:ListItem>
                <asp:ListItem Value="N">No Disponible</asp:ListItem>
             </asp:DropDownList>
        </div>

        <div class="form-inline mt-3 mb-1">
            <asp:Button ID="btnAgregar" CssClass="btn btn-primary col-md-12 mt-3" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
            <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
        </div>

    </div>
</asp:Content>
