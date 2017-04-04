<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMessages.aspx.cs" Inherits="Com.Comm100.Forum.UI.SendMessages" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function closeWindow() {
            window.parent.WindowClose('divSendMessageToUser', 'divThickOuter');
        }
    </script>

</head>
<body style="background: #fff;">
    <form id="form1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="False"></asp:Label>
    </div>
    <table class="tb_forum" cellspacing="0" width="100%">
        <tr>
            <td class="row1" width="20%" align="right">
                <p>
                    <b>
                        <%=Proxy[EnumText.enumForum_SendMessages_SubTitleRecipeintName]%></b></p>
            </td>
            <td class="row2">
                <p>
                    <asp:Label ID="lbUserName" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td class="row1" align="right">
                <p>
                    <b>
                        <%=Proxy[EnumText.enumForum_SendMessages_SubTitleSubject]%></b></p>
            </td>
            <td class="row2">
                <p>
                    <asp:TextBox ID="txtSubject" runat="server" class="txt" Width="320" /><span class="require">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtSubject" runat="server" ControlToValidate="txtSubject"
                        Display="Dynamic"><%=Proxy[EnumText.enumForum_SendMessages_ErrorMessageSubjectIsRequired]%></asp:RequiredFieldValidator>
                </p>
            </td>
        </tr>
        <tr>
            <td class="row1" align="right" width="20%">
                <p>
                    <b>
                        <%=Proxy[EnumText.enumForum_SendMessages_SubTitleMessage]%></b></p>
            </td>
            <td class="row2">
                <p>
                    <div style="width: 70%">
                        <asp:TextBox ID="txtMessage" Rows="10" Columns="52" CssClass="txt" runat="server"
                            TextMode="MultiLine"></asp:TextBox>
                        <%--<textarea rows="10" cols="52" class="txt" runat="server" id="txtMessage"></textarea>--%>
                    </div>
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
                    <span class="require"><b>*<%=Proxy[EnumText.enumForum_Public_RequiredField]%></b></span>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="row5" align="center">
                <p>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click" /><%--OnClientClick="window.close();return false;"--%>
                </p>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
