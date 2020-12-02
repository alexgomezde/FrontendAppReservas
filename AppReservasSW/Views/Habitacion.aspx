<%@ Page Async="true" Title="" Language="C#" MasterPageFile="../Site.Master" AutoEventWireup="true" CodeBehind="Habitacion.aspx.cs" Inherits="AppReservasSW.Views.Habitacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
        
    <asp:GridView ID="grdHabitaciones" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
            AutoGenerateColumns="False"  OnRowDeleting="grdHabitaciones_RowDeleting" OnRowEditing="grdHabitaciones_RowEditing" 
        OnRowCancelingEdit="grdHabitaciones_RowCancelingEdit" OnRowUpdating="grdHabitaciones_RowUpdating" OnRowDataBound="grdHabitaciones_RowDataBound" >
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
                    <asp:Label ID="lblCodigoHabitacion" Text='<%# Eval("HAB_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHabCodigoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Código Hotel">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoHotel" Text='<%# Eval("HOT_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpNombreHotelesEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHotCodigoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Número Habitación">
                <ItemTemplate>
                    <%# Eval("HAB_NUMERO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHabNumHab" Text='<%# Eval("HAB_NUMERO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHabNumHabFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Capacidad">
                <ItemTemplate>
                    <%# Eval("HAB_CAPACIDAD")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHabCapacidad" Text='<%# Eval("HAB_CAPACIDAD") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHabCapacidadFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Tipo">
                <ItemTemplate>
                    <%# Eval("HAB_TIPO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHabTipo" Text='<%# Eval("HAB_TIPO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHabTipoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Descripción">
                <ItemTemplate>
                    <%# Eval("HAB_DESCRIPCION")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHabDescripcion" Text='<%# Eval("HAB_DESCRIPCION") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHabDescripcionFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Categoría">
                <ItemTemplate>
                    <asp:Label ID="lblCategoria" Text='<%# Eval("HAB_ESTADO") %>' runat="server" CssClass="GridViewEditRow" />
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

            <asp:TemplateField HeaderText="Precio">
                <ItemTemplate>
                    <%# Eval("HAB_PRECIO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHabPrecio" Text='<%# Eval("HAB_PRECIO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHabPrecioFooter" runat="server" />
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
            <asp:Label ID="Label9" runat="server" Text="Hotel" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:DropDownList ID="drpNombreHoteles" runat="server" CssClass="form-control col-md-8">
                <asp:ListItem Selected="True" Value="D">Disponible</asp:ListItem>
                <asp:ListItem Value="N">No Disponible</asp:ListItem>
             </asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label3" runat="server" Text="Número" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtNumeroHabitacion" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3"> 
            <asp:Label ID="Label10" runat="server" Text="Teléfono" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label11" runat="server" Text="Capacidad" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>
    
        <div class="form-inline mb-3">
            <asp:Label ID="Label12" runat="server" Text="Tipo" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label1" runat="server" Text="Descripción" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label  runat="server" Text="Estado" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpDisponibilidad" runat="server" CssClass="form-control col-md-8">
                <asp:ListItem Selected="True" Value="D">Disponible</asp:ListItem>
                <asp:ListItem Value="N">No Disponible</asp:ListItem>
             </asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label2" runat="server" Text="Precio" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mt-3 mb-1">
            <asp:Button ID="btnAgregar" CssClass="btn btn-primary col-md-12 mt-3" runat="server" Text="Agregar" OnClick="btnIngresar_Click"  />
            <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
        </div>
        
        

    </div>
 
</asp:Content>
