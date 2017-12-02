<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search user.aspx.cs" Inherits="WebApplication.admin.search_user" %>

<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="css/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="css/coin-slider.css" />
<script type="text/javascript" src="js/cufon-yui.js"></script>
<script type="text/javascript" src="js/cufon-titillium-250.js"></script>
<script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="js/script.js"></script>
<script type="text/javascript" src="js/coin-slider.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main indexpage">
  <div class="header">
    <div class="header_resize">
      <div class="menu_nav">
      </div>
      </div>
    </div>
  </div>
    <div>
    <div class="main indexpage">
  <div class="header">
    <div class="header_resize">
      <div class="menu_nav">
        <ul>
   
           <li><a href="about.html"><span><asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Back</asp:LinkButton></span></a></li>
           <li><a href="blog.html"><span><asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Logout</asp:LinkButton></span></a></li>
              
              </a></li>
        </ul>
      </div>
      <div class="logo">
        <h1><a href="index.html"><span>Semantic</span> <small>Search engine</small></a></h1>
      </div>
      <div class="clr"></div>
      <div class="slider">
        <div id="coin-slider"> <a href="#">
    
        <cc1:GMap ID="GMap1" runat="server" Height="500px" Width="850px" />
    
        &nbsp;&nbsp; </a> </div></a> </div>
        <div class="clr"></div>
      </div>
      <div class="clr"></div>
    </div>
  </div>
  <div class="content">
    <div class="content_resize">
      <div class="mainbar">
        <div class="article">
          <div class="clr"></div>
          <div class="post_content">
    
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            CellPadding="4"  ForeColor="#333333" 
            GridLines="None" onselectedindexchanging="GridView2_SelectedIndexChanging" 
                  AllowPaging="True" onpageindexchanging="GridView2_PageIndexChanging" 
                  onselectedindexchanged="GridView2_SelectedIndexChanged" PageSize="7">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                    SelectImageUrl="~/admin/images/enter.gif" />
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="ID" />
                <asp:BoundField DataField="Word_search1" HeaderText="Word_search1" 
                    ReadOnly="True" SortExpression="Word_search1" />
                <asp:BoundField DataField="long" HeaderText="long" ReadOnly="True" 
                    SortExpression="long" />
                <asp:BoundField DataField="latid" HeaderText="latid" ReadOnly="True" 
                    SortExpression="latid" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    
          </div>
          <div class="clr"></div>
        </div>
        <div class="article">
          <div class="clr"></div>
        </div>
      </div>
      <div class="sidebar">
        <div class="clr"></div>
          <table style="width:100%;">
              <tr>
                  <td>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" onselectedindexchanging="GridView1_SelectedIndexChanging" 
           >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                    SelectImageUrl="~/admin/images/enter.gif" />
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="ID" />
                <asp:BoundField DataField="First_Name" HeaderText="First_Name" ReadOnly="True" 
                    SortExpression="First_Name" />
                <asp:BoundField DataField="last_name" HeaderText="last_name" ReadOnly="True" 
                    SortExpression="last_name" />
                <asp:BoundField DataField="date_of_birth" HeaderText="date_of_birth" 
                    ReadOnly="True" SortExpression="date_of_birth" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    
                  </td>
              </tr>
          </table>
    <asp:HiddenField ID="HiddenField2" runat="server" />
    
              <a href="#" class="rm">
    <asp:HiddenField ID="HiddenField1" runat="server" />
                </a>
      </div>
      <div class="clr"></div>
    </div>
  </div>
    
        <br />
    
        <br />
    
    </div>
    </form>
    </form>
</body>
</html>
