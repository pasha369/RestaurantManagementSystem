<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlackList.aspx.cs" Inherits="RMS.Admin.Pages.BlackList" MasterPageFile="~/Site.Master"%>


<asp:Content runat="server" ContentPlaceHolderID="Title"></asp:Content>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Content">

    <asp:ScriptManager runat="server" />

    <section class="wrapper">

        <!--overview start-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header"><i class="fa fa-laptop"></i>BLACK LIST</h3>
                <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i><a id="A1" href="~/Pages/Dashboard.aspx" runat="server">Home</a></li>
                    <li><i class="fa fa-laptop"></i>Black list</li>
                </ol>
            </div>
        </div>


        <asp:ObjectDataSource runat="server"
            ID="dsBanUsers"
            SelectMethod="GetBlackList"
            DataObjectTypeName="DataModel.Model.UserInfo"
            TypeName="DataAccess.Concrete.User.UserDataManager"></asp:ObjectDataSource>

        <asp:GridView ID="gvBlackList" runat="server" DataSourceID="dsBanUsers" AutoGenerateColumns="False" OnRowCommand="gvBlackList_RowCommand" CssClass="table table-striped table-advance table-hover">
            <Columns>
                <asp:BoundField HeaderText="Name" DataField="Name" />
                <asp:BoundField HeaderText="Login" DataField="Login" />
                <asp:TemplateField>
                    <ItemTemplate>
                        
                            <asp:LinkButton ID="btnDel" runat="server" CssClass="btn btn-danger" CommandName="Remove" CommandArgument='<%# Eval("Id") %>' ><i class="icon_close_alt2"></i></asp:LinkButton>


                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>



    </section>

</asp:Content>
