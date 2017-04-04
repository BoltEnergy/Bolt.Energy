<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Com.Comm100.Forum.UI.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <%--   <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>--%>
    <link href="CSS/inner/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:LinkButton ID="lbaa" runat="server"></asp:LinkButton>
        <div>

            <div class="container">
                <div class="row">
                    <div class="three columns">
                        <div class="logo">
                            <a href="#">
                                <img src="images/logo.png" alt="bolt" />
                            </a>
                        </div>
                        <div class="clearfix"></div>
                        <h2>Login</h2>
                        <p>
                            <label for="txtUserName">Email: </label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txt"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredFieldValidatorEmail" runat="server" ControlToValidate="txtUserName"
                                Display="Dynamic" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regularExpressionValidatorEmail" runat="server"
                                Display="Dynamic" ControlToValidate="txtUserName" ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*"
                                ValidationGroup="Login"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <label for="txtPassword">Password: </label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="txt"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredFieldValidatorPassword" runat="server" ControlToValidate="txtPassword"
                                Display="Dynamic" ValidationGroup="Login"></asp:RequiredFieldValidator>
                        </p>
                        <p class="divMsg">
                            <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
                        </p>
                        <div class="clear"></div>
                        <p>


                            <asp:Button ID="btnLogin" runat="server" CssClass="btn-form" OnClick="btnLogin_Click" Text="Log In"
                                ValidationGroup="Login" />
                        </p>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
<%--<script type="text/javascript" >
        $('link[rel=stylesheet][href~="styleTemplate_Default/stylesheet.css"]').remove();
    </script>--%>
</html>
