<%@ Page Async="true"  Title=""  Language="C#" MasterPageFile="../Site.Master" 
    AutoEventWireup="true" CodeBehind="Tarifa.aspx.cs" Inherits="AppReservasSW.Views.Tarifa" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:GridView ID="grdTarifa" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" 
        OnRowDeleting="grdTarifa_RowDeleting" OnRowEditing="grdTarifa_RowEditing" OnRowCancelingEdit="grdTarifa_RowCancelingEdit" 
        OnRowUpdating="grdTarifa_RowUpdating" >
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
           <%-- <asp:TemplateField HeaderText="Código">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoTarifa" Text='<%# Eval("TAR_CODIGO") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Código">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoTarifa" Text='<%# Eval("TAR_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTarCodigoFooter" runat="server" />
                </FooterTemplate>
               </asp:TemplateField>


                         <asp:TemplateField HeaderText="Clase">
                <ItemTemplate>
                    <asp:Label ID="lblClase" Text='<%# Eval("TAR_CLASE") %>' runat="server" CssClass="GridViewEditRow" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpClaseEdit" runat="server">
                        <asp:ListItem Value="P">Preferencial</asp:ListItem>
                        <asp:ListItem Value="B">Basica</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHotCategoriaFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>






    <%--        <asp:TemplateField HeaderText="Clase">
                <ItemTemplate>
                    <%# Eval("TAR_CLASE") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtClaseEdit" Text='<%# Eval("TAR_CLASE")%>' runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>--%>




           <%-- <asp:TemplateField HeaderText="Precio">
                <ItemTemplate>
                    <%# Eval("TAR_PRECIO") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPrecioEdit" Text='<%# Eval("TAR_PRECIO")%>' runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>--%>


            <asp:TemplateField HeaderText="Precio">
                <ItemTemplate>
                    <%# Eval("TAR_PRECIO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox  ID="txtPrecioEdit" Text='<%# Eval("TAR_PRECIO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTarPrecioFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>





                 <asp:TemplateField HeaderText="Impuesto">
                <ItemTemplate>
                    <%# Eval("TAR_IMPUESTO") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtImpuestoEdit" Text='<%# Eval("TAR_IMPUESTO")%>' runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label ID="lblCategoria" Text='<%# Eval("TAR_ESTADO") %>' runat="server" CssClass="GridViewEditRow" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpEstadoEdit" runat="server">
                        <asp:ListItem Value="A">Activa</asp:ListItem>
                        <asp:ListItem Value="N">No Activa</asp:ListItem>
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
            <asp:Label  runat="server" Text="Estado" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpClase" runat="server" CssClass="form-control col-md-8">
                <asp:ListItem Selected="True" Value="P">Preferencial</asp:ListItem>
                <asp:ListItem Value="B">Basica</asp:ListItem>
             </asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label3" runat="server" Text="Precio" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

              <div class="form-inline mb-3">
            <asp:Label ID="Label2" runat="server" Text="Impuesto" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtImpuesto" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
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

