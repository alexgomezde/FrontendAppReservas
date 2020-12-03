<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Avion.aspx.cs" Inherits="AppReservasSW.Views.Avion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grdAviones" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
        AutoGenerateColumns="False" OnRowDeleting="grdAviones_RowDeleting" OnRowEditing="grdAviones_RowEditing" OnRowCancelingEdit="grdAviones_RowCancelingEdit"
         OnRowUpdating="grdAviones_RowUpdating" OnRowDataBound="grdAviones_RowDataBound"> 
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
                    <asp:Label ID="lblCodigoAvion" Text='<%# Eval("AVI_CODIGO") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="Aeropuerto">
                <ItemTemplate>
                    <asp:Label ID="lblAeropuerto" Text='<%# Eval("AER_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpAeropuertoEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Codigo Fabricante">
                <ItemTemplate>
                    <asp:Label ID="lblAvionFabricante" Text='<%# Eval("AVI_FABRICANTE") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpAvionFabricanteEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Avion Tipo">
                <ItemTemplate>
                    <asp:Label ID="lblAvionTipo" Text='<%# Eval("AER_TIPO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpAvionTipoEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Avion Capacidad">
                <ItemTemplate>
                   <asp:Label ID="lblAvionCapacidad" Text='<%# Eval("AVI_CAPACIDAD") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAvionCapacidadEdit" Text='<%# Eval("AVI_CAPACIDAD") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label ID="lblEstado" Text='<%# Eval("AVI_ESTADO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpEstadoEdit" runat="server" SelectedValue='<%# Bind("AVI_ESTADO") %>'>
                        <asp:ListItem Value="D">Disponible</asp:ListItem>
                        <asp:ListItem Value="N">No Disponible</asp:ListItem>
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
            <asp:Label ID="Label1" runat="server" Text="Código Aeropuerto" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpAeropuertoCodigo" runat="server" CssClass="form-control col-md-8">
             </asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label2" runat="server" Text="Avion Fabricante" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:TextBox ID="txtAvionFabricante" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label3" runat="server" Text="Avion Tipo" CssClass="col-form-label col-md-4" ></asp:Label>
           <asp:TextBox ID="txtAvionTipo" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label4" runat="server" Text="Avion Capacidad" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtAvionCapacidad" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label  runat="server" Text="Estado" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpEstado" runat="server" CssClass="form-control col-md-8">
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
