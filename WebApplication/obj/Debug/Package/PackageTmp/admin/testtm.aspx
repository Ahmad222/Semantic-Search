<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testtm.aspx.cs" Inherits="WebApplication.testtm" %>

<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 185px;
        }
        .style3
        {
            width: 78px;
        }
        .style4
        {
        }
        .style5
        {
            width: 1478px;
            height: 268px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td colspan="6">
                    <img alt="keoo" class="style5" src="images/keo.png" /></td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#76CDE7">Home</asp:LinkButton>
                </td>
                <td colspan="2">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="#76CDE7">Logout</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4" colspan="6">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" 
                        BorderStyle="Ridge" ForeColor="#66CCFF" Width="139px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Width="251px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="Label_noInternet" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" colspan="2" style="width: 8%">
    
                    &nbsp;</td>
                <td class="style3" colspan="2">
    
        <cc1:GMap ID="GMap1" runat="server" Height="450px" Width="1335px" />
    
                </td>
                <td class="style3" colspan="2">
    
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
