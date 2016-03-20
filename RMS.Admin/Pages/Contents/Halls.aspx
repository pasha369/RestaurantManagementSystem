<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Halls.aspx.cs" Inherits="RMS.Admin.Pages.Contents.Halls" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="Title"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="wrapper">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!--overview start-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header"><i class="fa fa-laptop"></i>Halls</h3>
                <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i><a id="A1" href="~/Pages/Dashboard.aspx" runat="server">Home</a></li>
                    <li><i class="fa fa-laptop"></i>Hall</li>
                </ol>
            </div>
        </div>

        <asp:ObjectDataSource runat="server"
            ID="dsRestoraunts"
            SelectMethod="Get"
            DataObjectTypeName="DataModel.Model.Restaurant"
            TypeName="DataAccess.Concrete.RestaurantManager"></asp:ObjectDataSource>

        <asp:ObjectDataSource runat="server"
            ID="dsHall"
            SelectMethod="Get"
            DeleteMethod="Delete"
            DataObjectTypeName="DataModel.Model.Hall"
            TypeName="DataAccess.Concrete.HallManager"></asp:ObjectDataSource>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvHall" runat="server" DataSourceID="dsHall" AutoGenerateColumns="False"
                    DataKeyNames="Id" CssClass="table table-striped table-hover" OnRowCommand="gvHall_OnRowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Restaurant">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Restaurant.Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Name" DataField="Number" />

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Operations
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="btn-group">


                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-success" CommandArgument='<%#Bind("Id") %>' CommandName="Modify">
                                <i class="icon_check_alt2">	</i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDel" CommandName="Delete" runat="server" CssClass="btn btn-danger"><i class="icon_close_alt2"></i>	</asp:LinkButton>
                                </div>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinkButton ID="btnAdd" CssClass="btn btn-primary" runat="server" OnClick="btnAdd_OnClick"><i class="icon_plus_alt2">	</i></asp:LinkButton>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvHall" EventName="RowCommand"/>
            </Triggers>
        </asp:UpdatePanel>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div id="pop" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title">Hall</h4>
                            </div>
                            <div class="modal-body">
                                <asp:FormView runat="server" ID="fvHall" DefaultMode="Insert" DataKeyNames="Id" OnPreRender="fvHall_OnPreRender">
                                    <EditItemTemplate>
                                        <div class="container">
                                        <div class="form-group">
                                            <label>Restaurant:</label>   
                                            <asp:DropDownList runat="server"
                                                ID="ddlRestoraunt"
                                                DataValueField="Id"
                                                DataTextField="Name"
                                                CssClass="form-control"
                                                Enabled="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Number: </label>
                                            <asp:TextBox ID="txtNumber" runat="server" 
                                                Text='<%# Bind("Number")%>' CssClass="form-control"></asp:TextBox>
                                        </div>
                                            </div>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <div class="form-group">
                                            <label>Restaurant:</label>   
                                            <asp:DropDownList runat="server"
                                                ID="ddlRestoraunt"
                                                DataValueField="Id"
                                                DataTextField="Name"
                                                CssClass="form-control"
                                                Enabled="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Number: </label>
                                            <asp:TextBox ID="txtNumber" runat="server" 
                                                Text='<%# Bind("Number")%>' CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </InsertItemTemplate>
                                </asp:FormView>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                                <asp:Button runat="server" ID="btnSave" Text="Save"
                                    class="btn btn-default" OnClick="btnSave_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click"/>
            </Triggers>
        </asp:UpdatePanel>


    </div>
</asp:Content>
