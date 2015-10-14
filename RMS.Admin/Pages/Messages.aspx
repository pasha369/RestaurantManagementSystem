<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="RMS.Admin.Pages.Messages" MasterPageFile="~/Site.Master" Async="true" %>


<asp:Content runat="server" ContentPlaceHolderID="Title"></asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="wrapper">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <!--overview start-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header"><i class="fa fa-laptop"></i>Review</h3>
                <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i><a id="A1" href="~/Pages/Dashboard.aspx" runat="server">Home</a></li>
                    <li><i class="fa fa-laptop"></i>Messages</li>
                    <li><i class="fa fa-laptop"></i>Review</li>
                </ol>
            </div>
        </div>

        <asp:ObjectDataSource runat="server"
            ID="dsReviews"
            DeleteMethod="CheckSpam"
            SelectMethod="GetAllReview"
            DataObjectTypeName="DataModel.Model.Review"
            TypeName="DataAccess.Concrete.MessageManager"></asp:ObjectDataSource>


        <div class="input-group search">
            <div class="col-md-10 search-field">
                <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender
                    runat="server"
                    TargetControlID="txtSearch"
                    ID="AutoCompleteExtender"
                    MinimumPrefixLength="2"
                    CompletionInterval="1000"
                    CompletionSetCount="10"
                    ServiceMethod="GetCompletionList"
                    CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListItemCssClass="autocomplete_listItem"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                </ajaxToolkit:AutoCompleteExtender>
            </div>
            <div class="input-group-btn col-md-2">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default" Text="search" />
            </div>
        </div>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="grvReview" runat="server"  AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped table-hover ">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chbxSelectAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chbxSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Author" DataField="Author" />
                        <asp:BoundField HeaderText="Comment" DataField="Comment" />
                        <asp:BoundField HeaderText="Date" DataField="ReviewTime" />
                    </Columns>
                </asp:GridView>
                <asp:DropDownList runat="server" ID="ddlAuthor" ItemType="DataModel.Model.Review" 
                    OnSelectedIndexChanged="ddlAuthor_OnSelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Button runat="server" Text="Apply" ID="btnApply" CssClass="btn btn-default " OnClick="btnApplyClick" />
                <asp:Button runat="server" Text="Spam" ID="btnSpam" CssClass="btn btn-default " OnClick="btnSpamClick" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
