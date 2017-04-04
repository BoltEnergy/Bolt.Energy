<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AbusePost.aspx.cs" Inherits="Com.Comm100.Forum.UI.AbusePost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function closeWindow() {
            window.parent.WindowClose('divAbuse', 'divThickOuter');
        }
    </script>

</head>
<body style="background: #fff;">
    <form id="form1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
    </div>
    <table class="tb_forum" cellspacing="0" width="100%">
        <tr>
            <td class="row1" width="20%" align="right">
                <p>
                    <b>
                        <%=Proxy[EnumText.enumForum_AbusePost_FiledNotes]%></b></p>
            </td>
            <td class="row2" width="80%" valign="middle">
                <p>
                    <asp:TextBox ID="taAbuseReason" runat="server" TextMode="MultiLine" CssClass="txt"
                        Width="350" Height="130" EnableViewState="false"></asp:TextBox>
                    <span class="require">*</span>
                    <asp:RequiredFieldValidator ID="rfvAbuseReason" runat="server" Display="Dynamic"
                        ControlToValidate="taAbuseReason">
                        <%=Proxy[EnumText.enumForum_AbusePost_ErrorNotesIsRequired]%>
                    </asp:RequiredFieldValidator>
                </p>
            </td>
        </tr>
        <tr>
            <td class="row1">
                <p>
                    &nbsp;</p>
            </td>
            <td class="row2">
                <p>
                    <span class="require">*
                        <%=Proxy[EnumText.enumForum_Public_RequiredField]%></span></p>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="row5" align="center">
                <p>
                    <asp:Button ID="btnAbuseReason" runat="server" CssClass="btn" OnClick="btnAbuseReasonSubmit_Click" /></p>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        
    </script>

    </form>
</body>
</html>
