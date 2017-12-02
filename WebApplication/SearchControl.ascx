<%@ Control Language="c#" AutoEventWireup="true"%>
<%@ Import Namespace="Searcharoo.Common" %>
<script runat="server">
/// <summary>Size of the searchable catalog (number of unique words)</summary>
public int WordCount = -1;

/// <summary>Word/s displayed in search input box</summary>
public string Word = "";

/// <summary>
/// Error message - on Home Page version ONLY
/// ie. ONLY when IsSearchResultsPage = true
/// </summary>
public string _ErrorMessage;

/// <summary>Whether the standalone home page version, or the on Search Results page</summary>
private bool _IsSearchResultsPage;

/// <summary>Whether the control is placed at the Header or Footer</summary>
protected bool _IsFooter;

/// <summary>
/// Value is either
///   false: being displayed on the 'home page' - only thing on the page
///   true:  on the Results page (at the top _and_ bottom)
/// <summary>
public bool IsSearchResultsPage
{
	get {return _IsSearchResultsPage;}
	set {
		_IsSearchResultsPage = value;
		if (_IsSearchResultsPage) 
		{
			pnlHomeSearch.Visible = false;
			pnlResultsSearch.Visible = true;
		}
		else
		{
			pnlHomeSearch.Visible = true;
			pnlResultsSearch.Visible = false;	
		}
	}
}
/// <summary>
/// Footer control has more 'display items' than the one shown
/// in the Header - setting this property shows/hides them
/// </summary>
public bool IsFooter
{
	set {
		_IsFooter = value;
		pHeading.Visible = !_IsFooter;
		rowFooter1.Visible = _IsFooter;
		rowFooter2.Visible = _IsFooter;
		rowSummary.Visible = !_IsFooter;
	}
}
/// <summary>
/// Error message to be displayed if search input box is empty
/// </summary>
public string ErrorMessage
{
	set {
		_ErrorMessage = value;
	}
}
/// <summary>
/// Nothing actually happens on the User Control Page_Load () 
/// ... for now
/// <summary>
protected void Page_Load (object sender, EventArgs ea) 
{	
}

/// <summary>
/// Was originally used in Searcharoo3.aspx to generate the top and bottom 
/// search boxes from a single User Control 'instance'. Decided not to use
/// that approach - but left this in for reference.
/// <summary>
[Obsolete("Render control to string to embed mulitple times in a page; but no longer required.")]
public override string ToString() 
{
	System.IO.StringWriter writer = new System.IO.StringWriter();
	System.Web.UI.HtmlTextWriter buffer = new System.Web.UI.HtmlTextWriter(writer);
	this.Render(buffer);
	return writer.ToString();
}
</script>
<%--
Panel that is visible when the search page is first visited
--%>
<asp:Panel id="pnlHomeSearch" runat="server">
<form method="get" action="Search.aspx" style="margin:0px;padding:0px ;">
<center>
<p class="heading">&nbsp;<span style="color:lightblue">Arabic-English semantic Search<sup>1</sup></span><p></p>
    <p>
    </p>
    <table align="center" bgcolor="lightblue" bordercolor="#dcdcdc" cellpadding="4" 
        cellspacing="0" frame="box" rules="none" style="BORDER-COLLAPSE: collapse">
        <tr>
            <td>
                <p class="intro">
                    Search for ...<br><font color="red"><%=_ErrorMessage%></font>
                    <input name="<%=Preferences.QuerystringParameterName%>" 
                id="<%=Preferences.QuerystringParameterName%>1" size="40" value="<%=Word%>" />
                    </br>
                </p>
            </td>
        </tr>
        <tr>
            <td align="center">
                <input type="submit" value="Search" class="button" style="width: 139px" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="copyright">
                    &nbsp;</p>
            </td>
        </tr>
    </table>
    </p>
</center></form>
</asp:Panel>
<%--
Panel that is visible when search results are being shown
--%>
<asp:Panel id="pnlResultsSearch" runat="server">
<form method="get" id="bottom" action="Search.aspx" style="margin:0px;padding:0px;">
        <center>
        <p class="heading" id="pHeading" runat="server"><span style="color:lightblue">
            Arabic-English semantic Search<sup>1</sup></span></p>
        <table cellspacing=0 cellpadding=4 frame=box bordercolor=#dcdcdc rules=none style="BORDER-COLLAPSE: collapse" width="100%" bgcolor="lihgtblue">
            <tr>
                <td>
                <p>Search for :
                    <input type="text" name="<%=Preferences.QuerystringParameterName%>" id="<%=Preferences.QuerystringParameterName%>2" width="400" value="<%=Word%>" />
                    <input type="submit" value="Search" class="button" style="width: 82px" />
                </p>
                </td>
            </tr>
			<tr id="rowSummary" visible="true" runat="server"><td><p class="copyright">Searching <%=WordCount%> words</p></td></tr>
            <tr id="rowFooter1" visible="false" runat="server"><td>&nbsp;</td></tr>
            <tr id="rowFooter2" visible="false" runat="server"><td><p class="copyright">&nbsp;</p></td></tr>
        </table>
        </center>
    </form>
</asp:Panel>