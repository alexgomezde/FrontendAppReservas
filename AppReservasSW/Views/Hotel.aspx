 <%@  Page Async="true" Title="" Language="C#" MasterPageFile="../Site.Master" AutoEventWireup="true" CodeBehind="Hotel.aspx.cs" Inherits="AppReservasSW.Views.Hotel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:GridView ID="grdHoteles" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" 
        OnRowDeleting="grdHoteles_RowDeleting" OnRowEditing="grdHoteles_RowEditing" OnRowCancelingEdit="grdHoteles_RowCancelingEdit" 
        OnRowUpdating="grdHoteles_RowUpdating" >
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
                    <asp:Label ID="lblCodigoHotel" Text='<%# Eval("HOT_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHotCodigoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <%# Eval("HOT_NOMBRE")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHotNombre" Text='<%# Eval("HOT_NOMBRE") %>' runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHotNombreFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Correo Electrónico">
                <ItemTemplate>
                    <%# Eval("HOT_EMAIL")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHotEmail" Text='<%# Eval("HOT_EMAIL") %>' runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHotEmailFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Dirección">
                <ItemTemplate>
                    <%# Eval("HOT_DIRECCION")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHotDireccion" Text='<%# Eval("HOT_DIRECCION") %>' runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHotDireccionFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Teléfono">
                <ItemTemplate>
                    <%# Eval("HOT_TELEFONO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHotTelefono" Text='<%# Eval("HOT_TELEFONO") %>' runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHotTelefonoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Categoría">
                <ItemTemplate>
                    <asp:Label ID="lblCategoria" Text='<%# Eval("HOT_CATEGORIA") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpCategoriaEdit" runat="server" SelectedValue='<%# Bind("HOT_CATEGORIA") %>'>
                        <asp:ListItem Value="5">5 Estrellas</asp:ListItem>
                        <asp:ListItem Value="4">4 Estrellas</asp:ListItem>
                        <asp:ListItem Value="3">3 Estrellas</asp:ListItem>
                        <asp:ListItem Value="2">2 Estrellas</asp:ListItem>
                        <asp:ListItem Value="1">1 Estrella</asp:ListItem>
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
            <asp:Label ID="Label2" runat="server" Text="Nombre" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label3" runat="server" Text="Teléfono" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label4" runat="server" Text="Correo Electrónico" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>
    
        <div class="form-inline mb-3">
            <asp:Label ID="Label5" runat="server" Text="Dirección" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label6" runat="server" Text="Categoría" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpCategoria" runat="server" CssClass="form-control col-md-8">
                <asp:ListItem Selected="True" Value="5">5 Estrellas</asp:ListItem>
                <asp:ListItem Value="4">4 Estrellas</asp:ListItem>
                <asp:ListItem Value="3">3 Estrellas</asp:ListItem>
                <asp:ListItem Value="2">2 Estrellas</asp:ListItem>
                <asp:ListItem Value="1">1 Estrella</asp:ListItem>
             </asp:DropDownList>
        </div>

        <div class="form-inline mt-3 mb-1">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary col-md-12" OnClick="btnIngresar_Click" />
        </div>
        
        <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>

    </div>

    

</asp:Content>
