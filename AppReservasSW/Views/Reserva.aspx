<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reserva.aspx.cs" Inherits="AppReservasSW.Views.Reserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:GridView ID="grdReservas" CssClass="table table-dark table-sm text-center mt-1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
        AutoGenerateColumns="False" OnRowDeleting="grdReservas_RowDeleting" OnRowEditing="grdReservas_RowEditing" OnRowCancelingEdit="grdReservas_RowCancelingEdit"
        OnRowUpdating="grdReservas_RowUpdating" OnRowDataBound="grdReservas_RowDataBound" >
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

        <Columns>

            <asp:TemplateField HeaderText="Código">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoReserva" Text='<%# Eval("RES_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="lblCodigoReservaFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            

            <asp:TemplateField HeaderText="Código Usuario">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoUsuario" Text='<%# Eval("USU_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtUsuCodigo" runat="server" CssClass="GridViewEditRow">
                    </asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Código Habitación">
                <ItemTemplate>
                    <asp:Label ID="lblCodigoHabitacion" Text='<%# Eval("HAB_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpHabitacionEdit" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHabCodigoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <%--<asp:TemplateField HeaderText="Código Vuelo">
                <ItemTemplate>
                    <%# Eval("VUE_CODIGO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtVueCodigo" Text='<%# Eval("VUE_CODIGO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtVueCodigoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Código Vuelo">
                <ItemTemplate>
                    <asp:Label ID="lbCodigoVuelo" Text='<%# Eval("VUE_CODIGO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpVueloEdit" runat="server" CssClass="GridViewEditRow">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>





            <asp:TemplateField HeaderText="Costo">
                <ItemTemplate>
                    <%# Eval("RES_COSTO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtResCosto" Text='<%# Eval("RES_COSTO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtResCostoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha Ingreso">
                <ItemTemplate>
                    <%# Eval("RES_FECHA_INGRESO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtResFechaIngreso" Text='<%# Eval("RES_FECHA_INGRESO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtResFechaIngresoFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha Salida">
                <ItemTemplate>
                    <%# Eval("RES_FECHA_SALIDA")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtResFechaSalida" Text='<%# Eval("RES_FECHA_SALIDA") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtResFechaSalidaFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha Vuelo">
                <ItemTemplate>
                    <%# Eval("RES_FECHA_VUELO")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtResFechaVuelo" Text='<%# Eval("RES_FECHA_VUELO") %>' runat="server" CssClass="GridViewEditRow" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtResFechaVueloFooter" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label ID="lblCategoria" Text='<%# Eval("RES_ESTADO") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpEstadoEdit" runat="server" SelectedValue='<%# Bind("RES_ESTADO") %>'>
                        <asp:ListItem Value="A">Activa</asp:ListItem>
                        <asp:ListItem Value="D">Desactivada</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtResEstadoFooter" runat="server" />
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
            <asp:Label ID="Label1" runat="server" Text="Usuario" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtUsuarioCodigo" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label6" runat="server" Text="Código Habitación" CssClass="col-form-label col-md-4" ></asp:Label>
            <asp:DropDownList ID="drpCodigosHabitacion" runat="server" CssClass="form-control col-md-8">
             </asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label2" runat="server" Text="Código Vuelo" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpVuelo" runat="server" CssClass="form-control col-md-8"></asp:DropDownList>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label11" runat="server" Text="Costo" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>
    
        <div class="form-inline mb-3">
            <asp:Label ID="Label12" runat="server" Text="Fecha Ingreso" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label7" runat="server" Text="Fecha Salida" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtFechaSalida" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label ID="Label14" runat="server" Text="Fecha Vuelo" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:TextBox ID="txtFechaVuelo" runat="server" CssClass="form-control col-md-8"></asp:TextBox>
        </div>

        <div class="form-inline mb-3">
            <asp:Label  runat="server" Text="Estado" CssClass="col-form-label col-md-4"></asp:Label>
            <asp:DropDownList ID="drpEstado" runat="server" CssClass="form-control col-md-8">
                <asp:ListItem Selected="True" Value="A">Activa</asp:ListItem>
                <asp:ListItem Value="D">Desactivada</asp:ListItem>
             </asp:DropDownList>
        </div>

        <div class="form-inline mt-3 mb-1">
            <asp:Button ID="btnAgregar" CssClass="btn btn-primary col-md-12 mt-3" runat="server" Text="Agregar" OnClick="btnIngresar_Click"  />
            <asp:Label ID="lblStatus" runat="server" Text="Label" ForeColor="#006600" Visible="False"></asp:Label>
        </div>

    </div>


</asp:Content>
