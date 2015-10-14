<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RMS.Admin.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Bootstrap CSS -->
    <link href="~/Content/Styles/bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap theme -->
    <link href="~/Content/Styles/bootstrap-theme.css" rel="stylesheet" />
    <!--external css-->
    <!-- font icon -->
    <link href="~/Content/Styles/elegant-icons-style.css" rel="stylesheet" />
    <link href="~/Content/Styles/font-awesome.css" rel="stylesheet" />
    <!-- Custom styles -->
    <link href="~/Content/Styles/style.css" rel="stylesheet" />
    <link href="~/Content/Styles/style-responsive.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 -->
    <!--[if lt IE 9]>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <![endif]-->
</head>

<body class="login-img3-body">

    <div class="container">

        <form class="login-form" runat="server">

            <asp:Login RenderOuterTable="False" runat="server" ID="ctrLogin" OnAuthenticate="ctrLogin_OnAuthenticate">
                <LayoutTemplate>
                    <div class="login-wrap">
                        <p class="login-img"><i class="icon_lock_alt"></i></p>
                        <div class="input-group">
                            <span class="input-group-addon"><i class="icon_profile"></i></span>
                            <asp:TextBox ID="UserName" CssClass="form-control" placeholder="Username" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-group">
                            <span class="input-group-addon"><i class="icon_key_alt"></i></span>
                            <asp:TextBox ID="Password" TextMode="Password" class="form-control" placeholder="Password" runat="server"></asp:TextBox>
                        </div>
                        <label class="checkbox">
                            <input id="RememberMe" type="checkbox" value="remember-me">
                            Remember me
                <span class="pull-right"><a href="#">Forgot Password?</a></span>
                        </label>
                        <asp:Button runat="server" CssClass="btn btn-primary btn-lg btn-block" type="submit" Text="Login" CommandName="Login"></asp:Button>
                    </div>
                </LayoutTemplate>
            </asp:Login>

        </form>

    </div>


</body>
</html>
