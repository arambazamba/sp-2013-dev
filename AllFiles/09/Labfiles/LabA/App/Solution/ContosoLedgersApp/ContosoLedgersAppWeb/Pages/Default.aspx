<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ContosoLedgersAppWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Total number of sales: 
            <asp:Label ID="lblTotalNumberOfSales" runat="server" Text="0"></asp:Label></p>
        <p>Total number of purchases:
            <asp:Label ID="lblTotalNumberOfPurchases" runat="server" Text="0"></asp:Label> 
        </p>
    </div>
    </form>
</body>
</html>
