<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Table.aspx.cs" Inherits="RMS.Admin.Pages.Contents.Table" MasterPageFile="~/Site.Master" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Title"></asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Content">
    <div class="wrapper">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!--overview start-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header"><i class="fa fa-laptop"></i>Dinner table</h3>
                <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i><a id="A1" href="~/Pages/Dashboard.aspx" runat="server">Home</a></li>
                    <li><i class="fa fa-laptop"></i>Dinner table</li>
                </ol>
            </div>
        </div>

        <asp:ObjectDataSource runat="server"
            ID="dsRestoraunts"
            SelectMethod="GetAll"
            DataObjectTypeName="DataModel.Model.Restaurant"
            TypeName="DataAccess.Concrete.RestaurantManager"></asp:ObjectDataSource>

        <asp:ObjectDataSource runat="server"
            ID="dsTable"
            InsertMethod="Add"
            DeleteMethod="Delete"
            SelectMethod="GetAll"
            UpdateMethod="Update"
            DataObjectTypeName="DataModel.Model.DinnerTable"
            TypeName="DataAccess.Concrete.DinnerTableManager"></asp:ObjectDataSource>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvTables" runat="server" DataSourceID="dsTable" 
                    AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped table-hover "
                    OnRowCommand="gvTables_OnRowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Restaurant">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Restaurant.Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hall">
                            <ItemTemplate>
                                <span><%# Eval("Hall.Number") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Name" DataField="Number" />

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Operations
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-success" 
                                        CommandArgument='<%#Bind("Id") %>' CommandName="Modify">
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
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="upTable" runat="server">
            <ContentTemplate>
                <div id="pop" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title">Table</h4>
                            </div>
                            <div class="modal-body">
                                <asp:FormView runat="server" ID="fvTable" DefaultMode="Edit" DataKeyNames="Id">
                                    <EditItemTemplate>

                                                <div class="form-group">
                                                    <label>Restaurant:</label>   
                                            <asp:DropDownList runat="server"
                                                ID="ddlRestaurant"
                                                DataValueField="Id"
                                                DataTextField="Name"
                                                CssClass="form-control"
                                                DataSourceID="dsRestoraunts"
                                                AutoPostBack="True"
                                                Enabled="True"
                                                OnSelectedIndexChanged="ddlRestaurant_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Hall: </label>  
                                            <asp:DropDownList runat="server"
                                                ID="ddlHall"
                                                DataValueField="Id"
                                                DataTextField="Number"
                                                CssClass="form-control"
                                                Enabled="True"
                                                OnDataBinding="ddlHall_OnDataBinding">
                                            </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Number: </label>
                                                    <asp:TextBox ID="txtNumber" runat="server" 
                                                        Text='<%# Bind("Number")%>' CssClass="form-control"></asp:TextBox>
                                                </div>
                                 
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                   <label>Restaurant:</label>   
                                            <asp:DropDownList runat="server"
                                                ID="ddlRestaurant"
                                                DataValueField="Id"
                                                DataTextField="Name"
                                                CssClass="form-control"
                                                DataSourceID="dsRestoraunts"
                                                AutoPostBack="True"
                                                Enabled="True"
                                                OnSelectedIndexChanged="ddlRestaurant_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                <label>Hall:</label>     
                                            <asp:DropDownList runat="server"
                                                ID="ddlHall"
                                                DataValueField="Id"
                                                DataTextField="Number"
                                                CssClass="form-control"
                                                Enabled="True"
                                                OnDataBinding="ddlHall_OnDataBinding">
                                            </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Number: </label>
                                                    <asp:TextBox ID="txtNumber" runat="server" 
                                                        Text='<%# Bind("Number")%>' CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlRestaurant" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </InsertItemTemplate>
                                </asp:FormView>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                                <asp:Button runat="server" ID="btnSave" Text="Save"
                                    CssClass="btn btn-default" OnClick="btnSave_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

