<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RMS.Admin.Pages.Errors.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <!-- Bootstrap CSS -->    
    <link href="~/Content/Styles/bootstrap.min.css" rel="stylesheet">
    <!-- bootstrap theme -->
    <link href="~/Content/Styles/bootstrap-theme.css" rel="stylesheet">
    <!--external css-->
    <!-- font icon -->
    <link href="~/Content/Styles/elegant-icons-style.css" rel="stylesheet" />
    <link href="~/Content/Styles/font-awesome.css" rel="stylesheet" />
    <!-- Custom styles -->
    <link href="~/Content/Styles/style.css" rel="stylesheet">
    <link href="~/Content/Styles/style-responsive.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 -->
    <!--[if lt IE 9]>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div class="page-404">
        <p class="text-404"><asp:Label runat="server" ID="lblCode"></asp:Label></p>

        <h2><asp:Label runat="server" ID="lblErrorWhy"></asp:Label></h2>
        <p><asp:Label runat="server" ID="lblErrorWhat"></asp:Label> <br><a href="~/Pages/Dashboard.aspx" runat="server">Return Home</a></p>
    </div>
    </div>
    </form>
</body>
</html>
