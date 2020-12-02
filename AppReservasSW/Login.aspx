<%@ Page Async="true" Title="Ingresar" Language="C#" MasterPageFile="~/Externo.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppReservasSW.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
body {font-family: Arial, Helvetica, sans-serif; background-color: #dfdde0; color: white}


input[type=text], input[type=password] {
    width: 100%;
    padding: 12px 20px;
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #fff;
    box-sizing: border-box;
}

.button {
    /*background-color: #243054;*/
    background-color: white;
    color: #243054;
    padding: 14px 20px;
    margin: 8px 0;
    border-color: white;
    cursor: pointer;
    width: 100%;
}

button:hover {
    opacity: 0.8;
}

.cancelbtn {
    width: 100%;
    padding: 10px 18px;
    color: white;
    background-color: #898989;
}

.imgcontainer {
    text-align: center;
    margin: 24px 0 12px 0;
}

img.avatar {
    width: 10%;
    border-radius: 10%;
}

/* Clear floats */
.clearfix::after {
    content: "";
    clear: both;
    display: table;
}

.container {
    padding: 16px;
}

span.psw {
    float: right;
    padding-top: 16px;
}

#left-login {
    background-image: linear-gradient(to bottom, rgba(245, 246, 252, 0.52), rgba(117, 19, 93, 0.73)),
    url("Images/background.jpg");
    background-repeat: no-repeat, repeat;
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    height: 500px;
    padding: 200px 0;
}

#login {
    padding: 100px 60px;
    background-color: #243054;
    height: 500px;
    
}

.form-control {
    background-color: transparent;
    border-radius: 0;
    border-color: white;
    height: 60px;
    border: 10px solid white;
}

.form-control::placeholder { 
  color: white;

}

p{
  font-weight: bold;
  font-size: 20px;
  margin-bottom: 20px;
}

.btn-register{
    font-size: 20px;
    text-align: center;
    border: 2px solid white;
    height: 50px;
    width:200px;
    color: white;
    padding: 10px 20px;
    display: block;
    margin: 0 auto;
}

.btn-register:hover{
    color: white;
    text-decoration: none;
}

/* Change styles for span and cancel button on extra small screens */
@media screen and (max-width: 300px) {
    span.psw {
       display: block;
       float: none;
        text-align: left;
    }
    .cancelbtn {
       width: 100%;
    }


}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <section >
      <div class="container">
          <div class="row">
              <div class="col-md-6" id="left-login">
                  <p class="text-center mt-5">¿No tiene cuenta?</p>
                  <a href="Register.aspx" class="btn-register">Registrarme</a>
              </div>
              <div class="col-md-6" id="login">
                  <asp:TextBox Placeholder="Ingrese Identificación"  runat="server" ID="Identificacion" CssClass="form-control" />
  
    
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Identificacion" CssClass="text-danger" ErrorMessage="El campo de nombre de usuario es obligatorio." />
              <br />
            <asp:TextBox Placeholder="Ingrese Contraseña" runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="El campo de password es obligatorio." />
            <asp:Button type="button" CssClass="button" ID="btnLogin" OnClick="btnIngresar_Click"  runat="server" Text="Ingresar"/> 
            <asp:Button type="button" CssClass="cancelbtn" ID="btnCancelar"  runat="server" Text="Cancelar" />

              <asp:PlaceHolder runat="server" ID="ErrorMessage"  Visible="false">
                                <p class="text-danger">
                                    <asp:Literal runat="server"  ID="FailureText" />
                                </p>
                            </asp:PlaceHolder>
             <br><br>
           
              </div>
              

          </div>
      </div>
  </section>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

</asp:Content>
