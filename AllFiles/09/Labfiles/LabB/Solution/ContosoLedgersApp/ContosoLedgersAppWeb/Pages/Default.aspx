<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ContosoLedgersAppWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="../Scripts/sp.ui.controls.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
</head>
<body>
    <div id="chrome-control-placeholder"></div>

    <form id="form1" runat="server">
    <div>

        <h1>Contoso Ledger Summaries</h1>
        <table id="total-numbers-table">
            <tr>
                <td>
                    Total number of sales:
                </td>
                <td>
                    <asp:Label ID="lblTotalNumberOfSales" runat="server" Text="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Total number of purchases:
                </td>
                <td>
                    <asp:Label ID="lblTotalNumberOfPurchases" runat="server" Text="0"></asp:Label>
                </td>
            </tr>
        </table>
        <h2>Sales Summary</h2>
        <asp:Table ID="SalesTable" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                    Region
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Local Currency
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Number of Sales
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Total Value in Local Currency
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Exchange Rate
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Total Value in US Dollars
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <h2>Purchases Summary</h2>
        <asp:Table ID="PurchasesTable" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                    Region
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Local Currency
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Number of Purchases
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Total Value in Local Currency
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Exchange Rate
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    Total Value in US Dollars
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>


    </div>
    </form>
</body>
</html>
