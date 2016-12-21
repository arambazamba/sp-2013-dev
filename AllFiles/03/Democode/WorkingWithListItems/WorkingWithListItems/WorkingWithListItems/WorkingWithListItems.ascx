<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkingWithListItems.ascx.cs" Inherits="WorkingWithListItems.VisualWebPart1.WorkingWithListItems" %>
<div id="workingWithListItems">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlControls" runat="server">
                <p>Do you have a complaint about the new Contoso intranet portal? Use the form below to let us know what you think.</p>
                <p>Title: </p>
                <asp:TextBox ID="txtTitle" Width="200px" runat="server" />
                <br />
                <p>Description: </p>
                <asp:TextBox ID="txtDescription" Width="200px" TextMode="MultiLine" Rows="10" runat="server" />
                <br />
                <p>Priority: </p>
                <asp:RadioButtonList ID="rblPriority" runat="server"></asp:RadioButtonList>
                <hr />
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
            </asp:Panel>
            <asp:Panel ID="pnlConfirm" runat="server">
                <asp:Literal ID="litMessage" Mode="Transform" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
