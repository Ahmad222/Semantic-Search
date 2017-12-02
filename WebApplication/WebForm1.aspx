 <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Registration Form Validation in JavaScript Source Code</title>
<script language="javascript" type="text/javascript">
    function validate() {
        var summary = "";
        summary += isvaliduser();
        summary += isvalidFirstname();
        summary += isvalidLocation();
        if (summary != "") {
            alert(summary);
            return false;
        }
        else {
            return true;
        }
    }
    function isvaliduser() {
        var uid;
        var temp = document.getElementById("<%=txtuser.ClientID %>");
        uid = temp.value;
        if (uid == "") {
            return ("Please Enter UserName" + "\n");
        }
        else {
            return "";
        }
    }
    function isvalidFirstname() {
        var uid;
        var temp = document.getElementById("<%=txtfname.ClientID %>");
        uid = temp.value;
        if (uid == "") {
            return ("Please enter firstname" + "\n");
        }
        else {
            return "";
        }
    }
    function isvalidLocation() {
        var uid;
        var temp = document.getElementById("<%=txtlocation.ClientID %>");
        uid = temp.value;
        if (uid == "") {
            return ("Please enter Location" + "\n");
        }
        else {
            return "";
        }
    }
</script>
</head>
<body>
<form id="form1" runat="server">
<table align="center">
<tr>
<td>UserName</td>
<td><asp:TextBox ID="txtuser" runat="server" /></td>
</tr>
<tr>
<td>First Name</td>
<td><asp:TextBox ID="txtfname" runat="server" /></td>
</tr>
<tr>
<td>Location</td>
<td><asp:TextBox ID="txtlocation" runat="server" /></td>
</tr>
<tr>
<td></td>
<td>
<asp:Button ID="btnsubmit" runat="server" Text="Save"
OnClientClick ="javascript:validate()" />
</td>
</tr>
</table>
</form>
</body>
</html>