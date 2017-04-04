<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectForum.aspx.cs" Inherits="Forum.UI.SelectForum" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var lastchooseId = "";

        function forumchoosed(forumId) {
            if (lastchooseId != "") {
                var lastchooseitem = document.getElementById(lastchooseId);
                lastchooseitem.className = "row2";
            }
            var chooseitem = document.getElementById(forumId);
            chooseitem.className = "row2 choose_forum";
            lastchooseId = forumId;

            var item = document.getElementById("chooseForum");
            item.value = forumId;
        }

        function checkHideValueAndConfirm() {
            var chooseforumitem = document.getElementById("chooseForum");
            var confirmitem = document.getElementById("confirm");
            if (chooseforumitem.value == "") {
                return false;
            }
            else {
                return confirm(confirmitem.value);
            }
        }

        function closeWindow() {
            window.parent.WindowClose('divMoveTopic', 'divThickOuter');
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form name="form1" method="post" id="form1" runat="server">
    <input type="hidden" id="confirm" value='<%=Proxy[EnumText.enumForum_Forums_ConfirmMoveTopic]%>' />
    <input type="hidden" id="chooseForum" value="" runat="server" />
    <div class="divMsg">
        <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="false"></asp:Label>
    </div>
    <table class="tb_forum" cellspacing="0" width="100%">
        <asp:Repeater ID="repeaterCategory" runat="server" OnItemDataBound="repeaterCategory_ItemDataBound"
            EnableViewState="false">
            <ItemTemplate>
                <tr>
                    <th>
                        <p>
                            <%#System.Web.HttpUtility.HtmlEncode(Eval("Name").ToString())%>
                        </p>
                    </th>
                </tr>
                <asp:Repeater ID="repeaterForum" runat="server" OnItemDataBound="repeaterForum_ItemDataBound"
                    EnableViewState="false">
                    <ItemTemplate>
                        <asp:PlaceHolder ID="forumName" runat="server" EnableViewState="false"></asp:PlaceHolder>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="row5" align="center">
                <asp:Button ID="btnMove" runat="server" OnClick="btnMove_Click" OnClientClick="javascript:return checkHideValueAndConfirm();"
                    CssClass="btn" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
