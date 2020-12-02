<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vuelo.aspx.cs" Inherits="AppReservasSW.Views.Vuelo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grdVuelos" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
        AutoGenerateColumns="False" OnRowDeleting="grdVuelos_RowDeleting" OnRowEditing="grdVuelos_RowEditing" OnRowCancelingEdit="grdVuelos_RowCancelingEdit"
         OnRowUpdating="grdVuelos_RowUpdating" OnRowDataBound="grdVuelos_RowDataBound"> 
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
                    <asp:Label ID="lblCodigoVuelo" Text='<%# Eval("VUE_CODIGO") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Código Asiento">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoAsiento" Text='<%# Eval("VUE_CODIGO_ASI") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCodAsiEdit" Text='<%# Eval("VUE_CODIGO_ASI") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Aeropuerto Origen">
                <ItemTemplate>
                    <asp:Label ID="lblAeropuertoOrigen" Text='<%# Eval("AER_ORIGEN_COD") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpAeropuertoOrigenEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Aeropuerto Destino">
                <ItemTemplate>
                    <asp:Label ID="lblAeropuertoDestino" Text='<%# Eval("AER_DESTINO_COD") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpAeropuertoDestinoEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Código Avión">
                <ItemTemplate>
                    <%# Eval("AVI_CODIGO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCodAviEdit" Text='<%# Eval("AVI_CODIGO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Código Tarifa">
                <ItemTemplate>
                    <%# Eval("TAR_CODIGO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtTarCodEdit" Text='<%# Eval("TAR_CODIGO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label ID="lblEstado" Text='<%# Eval("VUE_ESTADO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpEstadoEdit" runat="server" SelectedValue='<%# Bind("VUE_ESTADO") %>'>
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
            <asp:Label ID="Label1" runat="server" Text="Código Asiento" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtCodigoAsiento" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label6" runat="server" Text="Aeropuerto Origen" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:DropDownList ID="drpAeropuertoOrigen" runat="server" CssClass="form-control col-md-8">
             </asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label2" runat="server" Text="Aeropuerto Destino" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:DropDownList ID="drpAeropuertoDestino" runat="server" CssClass="form-control col-md-8">
             </asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label11" runat="server" Text="Código Avión" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtCodigoAvion" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>
    
        <div class="form-inline mb-3">
            <asp:Label ID="Label12" runat="server" Text="Código Tarifa" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtCodigoTarifa" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
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
