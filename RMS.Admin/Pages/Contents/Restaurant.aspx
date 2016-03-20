<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Restaurant.aspx.cs" Inherits="RMS.Admin.Pages.Contents.Restaurant" MasterPageFile="~/Site.Master" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="DataModel.Model" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Title"></asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="wrapper">

        <!--overview start-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header"><i class="fa fa-laptop"></i>Restaurants</h3>
                <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i><a id="A1" href="~/Pages/Dashboard.aspx" runat="server">Home</a></li>
                    <li><i class="fa fa-laptop"></i>Table</li>
                    <li><i class="fa fa-laptop"></i>Users</li>
                </ol>
            </div>
        </div>

        <asp:ObjectDataSource runat="server"
            ID="dsRestoraunts"
            DeleteMethod="Delete"
            SelectMethod="Get"
            InsertMethod="Add"
            DataObjectTypeName="DataModel.Model.Restaurant"
            TypeName="DataAccess.Concrete.RestaurantManager"></asp:ObjectDataSource>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvRestoraunt" runat="server" DataSourceID="dsRestoraunts" AutoGenerateColumns="False"
                    DataKeyNames="Id" CssClass="table table-striped table-hover " >
                    <Columns>
                        <asp:BoundField HeaderText="Restaurant name" DataField="Name" />
                        <asp:TemplateField HeaderText="Cuisine">
                            <ItemTemplate>
                                <span><%# String.Join(", ", values: (Eval("Cuisines") as List<Cuisine>).Select(c => c.Name))%></span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Phone number" DataField="PhoneNumber" />
                        <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <%#Eval("Adress.Country.Name")%>,
                                <%#Eval("Adress.City.Name")%>,
                                <%#Eval("Adress.Street") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Operations
                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:HyperLink ID="btnEdit" runat="server"  CssClass="btn btn-success"
                                     NavigateUrl='<%#"~/Pages/Forms/RestaurantEdit.aspx?Id=" + Eval("Id") %>'>
                                        <i class="icon_check_alt2" ></i>
                                </asp:HyperLink>

                                <asp:LinkButton ID="btnDel" runat="server"  CssClass="btn btn-danger" CommandName="Delete"><i class="icon_close_alt2"></i>	</asp:LinkButton>


                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HyperLink ID="btnAdd" CssClass="btn btn-primary"  runat="server" NavigateUrl="~/Pages/Forms/RestaurantAdd.aspx">
                    <i class="icon_plus_alt2" ></i>
                </asp:HyperLink>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvRestoraunt" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>

    </div>

</asp:Content>
