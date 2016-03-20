<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestaurantEdit.aspx.cs" Inherits="RMS.Admin.Pages.Forms.RestaurantEdit" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="Title"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="wrapper">
        <section class="panel">
            <header id="panel-header" class="panel-heading">
                EDIT RESTAURANT
            </header>
            <div class="panel-body">
                <asp:ObjectDataSource runat="server" ID="odsEditCuisine"
                    UpdateMethod="Update"
                    SelectMethod="Get"
                    DataObjectTypeName="DataModel.Model.Restaurant"
                    TypeName="DataAccess.Concrete.RestaurantManager">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="id" Type="Int32" QueryStringField="Id" DefaultValue="-1" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:FormView runat="server" ID="fvRestorauntEdit" DefaultMode="Edit" DataKeyNames="Id" DataSourceID="odsEditCuisine">
                    <EditItemTemplate>

                        <div class="container">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Name:</label>
                                        <asp:TextBox ID="txtName" runat="server"
                                            Text='<%# Bind("Name") %>' CssClass="form-control"></asp:TextBox>


                                    </div>

                                    <div class="form-group">
                                        <label>Cuisine List:</label>
                                        <asp:DropDownList runat="server" DataValueField="Id" DataTextField="Name"
                                            CssClass="form-control" ID="ddlCuisines" />
                                        <asp:LinkButton ID="btnAddCusine" CssClass="btn btn-primary" runat="server"
                                            OnClick="btnAddCusine_OnClick">
                                            <i class="icon_plus_alt2"></i>
                                        </asp:LinkButton>

                                        <asp:GridView runat="server" ID="gvCuisines" AutoGenerateColumns="False"
                                            CssClass="table table-responsive" OnRowCommand="gvCuisines_OnRowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="Cuisine" DataField="Name" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDel" runat="server" CssClass="btn btn-danger" CommandArgument='<%#Eval("Id") %>' CommandName="Remove"><i class="icon_close_alt2"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Country:</label>
                                        <asp:DropDownList runat="server" DataValueField="Id"
                                            CssClass="form-control" DataTextField="Name" ID="ddlCountry" />
                                    </div>
                                    <div class="form-group">
                                        <label>City:</label>
                                        <asp:DropDownList runat="server" DataValueField="Id"
                                            CssClass="form-control" DataTextField="Name" ID="ddlCity" />
                                    </div>

                                    <div class="form-group">
                                        <label>Street Address:</label>
                                        <asp:TextBox runat="server" ID="txtStreet"
                                            CssClass="form-control"
                                            Text='<%#Eval("Adress.Street") %>'>
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Phone:</label>
                                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("PhoneNumber") %>' CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone"
                                            Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label>Description:</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDesc" CssClass="form-control"
                                        Text='<%#Bind("Description") %>'></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-default" OnClick="btnCancel_OnClick" Text="Cancel" />
                                    <asp:Button runat="server" ID="btnEdit" Text="Save"
                                        CssClass="btn btn-default" CausesValidation="True" OnClick="btnSave_OnClick" />
                                </div>
                            </div>
                        </div>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
        </section>
    </div>

</asp:Content>
