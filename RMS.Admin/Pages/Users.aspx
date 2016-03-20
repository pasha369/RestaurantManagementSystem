<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="RMS.Admin.Pages.Users" MasterPageFile="~/Site.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="Title"></asp:Content>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Content">

    <asp:ScriptManager runat="server" />

    <section class="wrapper">

        <!--overview start-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header"><i class="fa fa-laptop"></i>Users</h3>
                <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i><a id="A1" href="~/Pages/Dashboard.aspx" runat="server">Home</a></li>
                    <li><i class="fa fa-laptop"></i>Table</li>
                    <li><i class="fa fa-laptop"></i>Users</li>
                </ol>
            </div>
        </div>

        <asp:ObjectDataSource runat="server"
            ID="dsUsers"
            SelectMethod="GetAllApproved"
            DeleteMethod="Delete"
            DataObjectTypeName="DataModel.Model.UserInfo"
            TypeName="DataAccess.Concrete.User.UserManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource runat="server"
            ID="dsClients"
            SelectMethod="Get"
            DeleteMethod="Delete"
            DataObjectTypeName="DataModel.Model.ClientInfo"
            TypeName="DataAccess.Concrete.User.ClientManager"></asp:ObjectDataSource>


        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#Users">Users</a></li>
            <li><a data-toggle="tab" href="#Clients">Clients</a></li>
        </ul>

        <div class="tab-content">
            <div id="Users" class="tab-pane fade in active">
                <asp:UpdatePanel runat="server" ID="udpClients">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_OnRowCommand" DataSourceID="dsUsers" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped table-advance table-hover">
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:BoundField HeaderText="Phone" DataField="Phone" />
                                <asp:BoundField HeaderText="Address" />
                                <asp:BoundField HeaderText="Login" DataField="Login" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="btnToBlackLst" runat="server" CommandArgument='<%#Bind("Id") %>' 
                                                CommandName="ToBlackLst" CssClass="btn btn-danger" OnLoad="btnToBlackLst_Load"><i class="icon_balance"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnDel" runat="server" CssClass="btn btn-danger" CommandName="Delete" ><i class="icon_close_alt2"></i>	</asp:LinkButton>
                                        </div>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />

                    </Triggers>
                </asp:UpdatePanel>

                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
                    TargetControlID="btnAdd"
                    PopupControlID="pnlAddUser"
                    CancelControlID="btnClose"
                    PopupDragHandleControlID="panel-header">
                </ajaxToolkit:ModalPopupExtender>
                <asp:LinkButton ID="btnAdd" CssClass="btn btn-primary" runat="server"><i class="icon_plus_alt2">	</i></asp:LinkButton>
            </div>
            <div id="Clients" class="tab-pane fade">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvClients" OnRowCommand="gvClients_OnRowCommand" runat="server" DataSourceID="dsClients" AutoGenerateColumns="False" CssClass="table table-striped table-advance table-hover" DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%#Eval("UserInfo.Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Restaurant">
                                    <ItemTemplate>
                                        <%#Eval("Restaurant.Name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-success" CommandName="Modify" CommandArgument='<%#Bind("Id") %>'><i class="icon_check_alt2">	</i></asp:LinkButton>
                                            <asp:LinkButton ID="btnDel" runat="server" CssClass="btn btn-danger" CommandName="Delete" ><i class="icon_close_alt2"></i>	</asp:LinkButton>
                                        </div>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:LinkButton ID="btnAddClient" CssClass="btn btn-primary" runat="server" OnClick="btnAddClient_OnClick"><i class="icon_plus_alt2" >	</i></asp:LinkButton>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <div id="Employers" class="tab-pane fade">

                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                    TargetControlID="btnAdd"
                    PopupControlID="pnlAddUser"
                    CancelControlID="btnClose"
                    PopupDragHandleControlID="panel-header">
                </ajaxToolkit:ModalPopupExtender>
                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server"><i class="icon_plus_alt2">	</i></asp:LinkButton>
            </div>

            <asp:Panel runat="server" ID="pnlAddUser" Style="display: none">
                <section class="panel">
                    <header id="panel-header" class="panel-heading">
                        ADD USER
                    <asp:Button runat="server" ID="btnClose" CssClass="align-right btn btn-sm btn-close" Text="X" />
                    </header>
                    <div class="panel-body">

                        <asp:ObjectDataSource runat="server"
                            ID="dsUser"
                            InsertMethod="AddUser"
                            DataObjectTypeName="DataModel.Model.UserInfo"
                            TypeName="DataAccess.Concrete.User.UserDataManager"></asp:ObjectDataSource>
                        <div class="container">
                            <asp:FormView ID="Formview1" runat="server" DataKeyNames="Id" DefaultMode="Insert" DataSourceID="dsUser" CssClass="form-horizontal">
                                <InsertItemTemplate>
                                    <div class="form-group">
                                        Name:   
                                    <asp:TextBox ID="Name" runat="server" Text='<%# Bind("Name") %>' CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        Phone:   
                                    <asp:TextBox ID="Phone" runat="server" Text='<%# Bind("Phone") %>' CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <asp:Button ID="Button1" runat="server" CommandName="Insert" Text="Save" CssClass="btn btn-default" />

                                </InsertItemTemplate>

                            </asp:FormView>
                        </div>
                    </div>
                </section>

            </asp:Panel>




            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="pop" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        &times;</button>
                                    <h4 class="modal-title">Edit cuisine</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:FormView runat="server" ID="fvRestaurateur" DefaultMode="Edit" DataKeyNames="Id">
                                        <EditItemTemplate>
                                            <div class="container">
                                                <div class="form-group">
                                                    <label>Restaurant:</label>
                                                    <asp:DropDownList runat="server"
                                                        ID="ddlRestaurant"
                                                        DataValueField="Id"
                                                        DataTextField="Name"
                                                        Enabled="True" 
                                                        CssClass="form-control"
                                                        
                                                        OnDataBinding="ddlRestaurant_OnDataBinding"/>
                                                </div>
                                                <div class="form-group">
                                                    <label>User:</label>
                                                    <asp:DropDownList runat="server"
                                                        ID="ddlUsers"
                                                        DataValueField="Id"
                                                        DataTextField="Name"
                                                        Enabled="True" 
                                                        CssClass="form-control"
                                                        OnDataBinding="ddlUsers_OnDataBinding"/>
                                                </div>
                                            </div>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <div class="form-group">
                                                <label>Restaurant:</label>
                                                <asp:DropDownList runat="server"
                                                    ID="ddlRestaurant"
                                                    DataValueField="Id"
                                                    DataTextField="Name"
                                                    Enabled="True" 
                                                    CssClass="form-control"
                                                    OnDataBinding="ddlRestaurant_OnDataBinding"/>
                                            </div>
                                            <div class="form-group">
                                                <label>User:</label>
                                                <asp:DropDownList runat="server"
                                                    ID="ddlUsers"
                                                    DataValueField="Id"
                                                    DataTextField="Name"
                                                    Enabled="True" 
                                                    CssClass="form-control"
                                                    OnDataBinding="ddlUsers_OnDataBinding"/>
                                            </div>
                                        </InsertItemTemplate>
                                    </asp:FormView>

                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                    <asp:Button runat="server" ID="btnEdit" Text="Save"
                                        CssClass="btn btn-default" OnClick="btnSave_OnClick" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </section>

</asp:Content>
