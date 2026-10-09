<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForm.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Login </title>
</head>
<body>
    <form id="form1" runat="server">
       <h1>Student Registration Login</h1>
        <p>Username:
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        </p>
        <p>Password:
            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button  ID="btbLogin" Text="Login" OnClick="btnLogin_Click" runat="server"/>
        </p>
        <p>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </p>

         <p>
            <asp:TextBox
                ID="txtToken"
                runat="server"
                TextMode="MultiLine"
                Rows="8"
                Columns="80"
                ReadOnly="true">
            </asp:TextBox>
        </p>
    </form>
</body>
</html>
