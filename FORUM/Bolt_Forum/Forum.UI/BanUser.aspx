<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BanUser.aspx.cs" Inherits="Com.Comm100.Forum.UI.BanUser" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function closeWindow() {
            window.parent.WindowClose('divBanOneUser', 'divThickOuter');
        }
    </script>

</head>
<body style="background: #fff">
    <form id="form1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <table class="tb_forum" cellspacing="0" width="100%">
            <tr>
                <td class="row1" width="20%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_BanUser_SubTitleBanUser]%></b></p>
                </td>
                <td class="row2" width="80%" valign="middle">
                    <p>
                        <asp:Label ID="lbBanUser" runat="server" Text="" />
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_BanUser_SubTitleExpireTime]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtExpire" runat="server" CssClass="txt"></asp:TextBox>
                        <asp:DropDownList ID="ddlExpire" runat="server">
                        </asp:DropDownList>
                        <span class="require">*</span>
                        <%-- exprie Time--%>
                        <asp:RequiredFieldValidator ID="rfvExprieTime" runat="server" Display="Dynamic" ControlToValidate="txtExpire">
                            <%=Proxy[EnumText.enumForum_BanUser_ErrorExprieTimeIsRequired]%>
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvExprieTime" runat="server" Type="Integer" ValueToCompare="0"
                            ControlToValidate="txtExpire" Operator="GreaterThanEqual" Display="Dynamic">
                            <%=Proxy[EnumText.enumForum_BanUser_ErrorPleaseInputOneNumber]%>
                        </asp:CompareValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_BanUser_SubTitleNotes]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="taNotes" Columns="40" name="S1" Rows="8" TextMode="MultiLine" CssClass="txtnormal"
                            runat="server"></asp:TextBox>
                        <%--<textarea id="taNotes" cols="40" name="S1" rows="8" class="txtnormal" runat="server"></textarea>--%>
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
                            <%=Proxy[EnumText.enumForum_Public_RequiredField]%></span>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click" /></p>
                </td>
            </tr>
        </table>
    </div>
    <div style="font-weight: bold">
        <%--  <asp:RegularExpressionValidator ID="revExprieTime" runat="server" Display="Dynamic"
            ControlToValidate="txtExpire" ValidationExpression="^\d*$">
           
        </asp:RegularExpressionValidator>--%>
        <%--notes--%>
        <%--  <asp:RequiredFieldValidator ID="rfvNote" runat="server" Display="Dynamic" ControlToValidate="taNotes">
            <%=Proxy[EnumText.enumForum_BanUser_ErrorNotesIsRequired]%>
        </asp:RequiredFieldValidator>--%>
    </div>

    <script language="javascript" type="text/javascript">
        function SelectExprie() {
            var ddl = document.getElementById("<%=ddlExpire.ClientID%>");
            var txt = document.getElementById("<%=txtExpire.ClientID%>");
            if (ddl.options[0].selected == true) {
                txt.value = "10";
                txt.style.display = "none";
            }
            else {
                txt.value = "";
                txt.style.display = "";
            }
        }
        SelectExprie();
    </script>

    </form>
</body>
</html>
