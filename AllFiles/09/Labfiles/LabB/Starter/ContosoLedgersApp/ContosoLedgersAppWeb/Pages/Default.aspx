<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ContosoLedgersAppWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
</head>
<body>
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

        <!-- Display Sales and Purchase Summary Tables Here -->

    </div>
    </form>
</body>
</html>
