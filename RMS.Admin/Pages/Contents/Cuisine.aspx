<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cuisine.aspx.cs" Inherits="RMS.Admin.Pages.Contents.Cuisine" MasterPageFile="~/Site.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="Title"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="wrapper">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <!--overview start-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header"><i class="fa fa-laptop"></i>CUISINE</h3>
                <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i><a id="A1" href="~/Pages/Dashboard.aspx" runat="server">Home</a></li>
                    <li><i class="fa fa-laptop"></i>Table</li>
                </ol>
            </div>
        </div>


        <asp:ObjectDataSource runat="server"
            ID="odsCuisine"
            DeleteMethod="Delete"
            SelectMethod="Get"
            InsertMethod="Add"
            UpdateMethod="Update"
            DataObjectTypeName="DataModel.Model.Cuisine"
            TypeName="DataAccess.Concrete.CuisineManager"></asp:ObjectDataSource>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvCuisines" runat="server" DataSourceID="odsCuisine"
                    AutoGenerateColumns="False" DataKeyNames="Id"
                    CssClass="table table-striped table-hover " OnRowCommand="gvCuisines_OnRowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Operations
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="btn-group">


                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-success" CommandName="EditCuisine"
                                        CommandArgument='<%#Eval("Id") %>'>
                                        <i class="icon_check_alt2" ></i>
                                    </asp:LinkButton>


                                    <asp:LinkButton ID="btnDel" CommandName="Delete" runat="server" CssClass="btn btn-danger"><i class="icon_close_alt2"></i></asp:LinkButton>
                                </div>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            <asp:LinkButton ID="btnPopupAdd" CssClass="btn btn-primary" runat="server" OnClick="btnPopupAdd_OnClick"><i class="icon_plus_alt2">	</i></asp:LinkButton>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvCuisines" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>


        <asp:UpdatePanel runat="server">
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
                                <asp:FormView runat="server" ID="fvCuisineEdit" DefaultMode="Edit" DataKeyNames="Id">
                                    <EditItemTemplate>
                                        <div class="container">
                                            <div class="form-group">
                                                <label>Name:</label>
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"
                                                    Text='<%# Bind("Name") %>'></asp:TextBox>
                                            </div>
                                        </div>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <div class="form-group">
                                            Name:   
                                            <asp:TextBox ID="txtName" runat="server" 
                                                Text='<%# Bind("Name") %>' CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </InsertItemTemplate>
                                </asp:FormView>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                                <asp:Button runat="server" ID="btnEdit" Text="Save"
                                    class="btn btn-default" OnClick="btnSave_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </div>

</asp:Content>

