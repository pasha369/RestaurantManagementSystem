<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Address.aspx.cs" Inherits="RMS.Admin.Pages.Contents.Address" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="Title"></asp:Content>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Content">
    <div class="wrapper">

        <asp:ObjectDataSource ID="odsCountry" runat="server"
            SelectMethod="GetAll"
            InsertMethod="Add"
            DeleteMethod="Delete"
            UpdateMethod="Update"
            DataObjectTypeName="DataModel.Model.Country"
            TypeName="DataAccess.Concrete.CountryManager"></asp:ObjectDataSource>


        <asp:ObjectDataSource ID="odsCities" runat="server"
            SelectMethod="GetAll"
            InsertMethod="Add"
            DeleteMethod="Delete"
            UpdateMethod="Update"
            DataObjectTypeName="DataModel.Model.City"
            TypeName="DataAccess.Concrete.CityManager"></asp:ObjectDataSource>



        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#contry">Contry</a></li>
            <li><a data-toggle="tab" href="#city">City</a></li>
        </ul>

        <div class="tab-content">

            <div id="contry" class="tab-pane fade in active">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView runat="server"
                            ID="gvCounties"
                            DataSourceID="odsCountry" AutoGenerateColumns="False"
                            DataKeyNames="Id" OnRowCommand="gvCounties_OnRowCommand"
                            CssClass="table table-striped table-hover ">
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Bind("Id") %>' CommandName="Modify" Text="Edit" CssClass="btn btn-success"><i class="icon_check_alt2">	</i></asp:LinkButton>
                                        <asp:LinkButton runat="server" CommandName="Delete" CssClass="btn btn-danger"><i class="icon_close_alt2"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:LinkButton ID="btnPopupAdd" CssClass="btn btn-primary"
                            runat="server" OnClick="btnPopupAdd_OnClick"><i class="icon_plus_alt2"></i></asp:LinkButton>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div id="city" class="tab-pane fade">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvCities" runat="server" DataSourceID="odsCities" AutoGenerateColumns="False"
                            DataKeyNames="Id" OnRowCommand="gvCities_OnRowCommand"
                            CssClass="table table-striped table-hover ">
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:TemplateField HeaderText="Name" >
                                    <ItemTemplate>
                                         <%#Eval("Country.Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Modify" Text="Edit"
                                            CommandArgument='<%#Bind("Id") %>' CssClass="btn btn-success"><i class="icon_check_alt2">	</i></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" CssClass="btn btn-danger"><i class="icon_close_alt2"></i></asp:LinkButton>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:LinkButton ID="lbtnAddCity" CssClass="btn btn-primary" runat="server" OnClick="lbtnAddCity_OnClick"><i class="icon_plus_alt2"></i></asp:LinkButton>                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>


        </div>




        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="pop" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title">Edit country</h4>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:FormView ID="fvAddressEdit" runat="server" DataKeyNames="Id" DefaultMode="Edit" CssClass="form-horizontal">
                                            <EditItemTemplate>
                                                <div class="container">
                                                    <div class="form-group">
                                                        <label>Name:</label>
                                                        <asp:TextBox ID="txtName" runat="server"
                                                            Text='<%# Bind("Name") %>' CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <div class="container">
                                                    <div class="form-group">
                                                        Name:   
                                                    <asp:TextBox ID="txtName" runat="server"
                                                        Text='<%# Bind("Name") %>' CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </InsertItemTemplate>
                                        </asp:FormView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                                <asp:Button runat="server" ID="btnSaveCountry" Text="Save"
                                    CssClass="btn btn-default" OnClick="btnSaveCountry_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div id="popCity" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title">Edit country</h4>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:FormView ID="fvCity" runat="server" DataKeyNames="Id" DefaultMode="Edit" CssClass="form-horizontal">
                                            <EditItemTemplate>
                                                <div class="container">
                                                    <div class="form-group">
                                                        <label>Country:</label>
                                                        <asp:DropDownList ID="ddlCountry" runat="server"
                                                            DataSourceID="odsCountry"
                                                            DataValueField="Id"
                                                            DataTextField="Name"
                                                            CssClass="form-control"
                                                            Enabled="True"
                                                            OnPreRender="ddlCountry_OnPreRender">
                                                        </asp:DropDownList>
                                                        <label>Name:</label>
                                                        <asp:TextBox ID="txtName" runat="server"
                                                            Text='<%# Bind("Name") %>' CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <div class="container">

                                                    <div class="form-group">
                                                        <label>Country:</label>
                                                        <asp:DropDownList ID="ddlCountry" runat="server"
                                                            DataSourceID="odsCountry"
                                                            DataValueField="Id"
                                                            DataTextField="Name"
                                                            CssClass="form-control"
                                                            Enabled="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        Name:   
                                                    <asp:TextBox ID="txtName" runat="server"
                                                        Text='<%# Bind("Name") %>' CssClass="form-control"></asp:TextBox>
                                                    </div>
                                            </InsertItemTemplate>
                                        </asp:FormView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                                <asp:Button runat="server" ID="btnSaveCity" Text="Save"
                                    CssClass="btn btn-default" OnClick="btnSaveCity_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </div>
</asp:Content>
