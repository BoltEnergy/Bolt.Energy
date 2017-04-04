<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectForum1.aspx.cs" Inherits="Forum.UI.SelectForum1" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--    <link href="App_Themes/StyleTemplate_Default2/Style.css" rel="stylesheet" type="text/css" />--%>
    <link href="CSS/inner/common.css" rel="stylesheet" type="text/css" />
    <link href="CSS/inner/container.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/inner/layout.css" rel="Stylesheet" type="text/css" />

    <script src="JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var lastchooseId = "";

        function forumchoosed(forumId) {
            if (lastchooseId != "") {
                var lastchooseitem = document.getElementById(lastchooseId);
                lastchooseitem.className = "trStyle2";
            }
            var chooseitem = document.getElementById(forumId);
            chooseitem.className = "trHightLight";
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
<body style="overflow-y:scroll">
    <form name="form1" id="form1" runat="server">
    <div id="Content" style="margin-left: 0px;">
        <div class="divMsg">
            <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="false"></asp:Label>
        </div>
        <div class="divh-header">
            <div style="float: left; font-size: 14px; font-weight: bold">
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </div>
            <div style="float:right">
               <b>[<a href="javascript:closeWindow();"><%=Proxy[EnumText.enumForum_Topic_FieldClose] %></a>]</b>
            </div>
            <div style="clear: none">
            </div>
        </div>
        <br />
        <input type="hidden" id="confirm" value='<%=Proxy[EnumText.enumForum_Forums_ConfirmMoveTopic]%>' />
        <input type="hidden" id="chooseForum" value="" runat="server" />
        <div class="divContent" style="padding-left:0px">
          <div class="divTopButton">
                <asp:Button ID="btnMove1" runat="server" OnClick="btnMove_Click" OnClientClick="javascript:return checkHideValueAndConfirm();"
                    CssClass="mbtn" />
            </div>
            <div class="divTable">
                <table class="the-table" cellpadding='0' cellspacing='0' style="width:auto;">
                    <asp:Repeater ID="repeaterCategory1" runat="server" OnItemDataBound="repeaterCategory1_ItemDataBound"
                        EnableViewState="false">
                        <ItemTemplate>
                            <%--<tr align="center">
                                <th class="divTopButton">
                                    <%#System.Web.HttpUtility.HtmlEncode(Eval("Name").ToString())%>
                                </th>
                            </tr>--%>
                            <tr align="center">
                                <td style="padding:0 0"><div style="background-color: #9fcdcd;border-bottom:solid 1px #009999;border-top:solid 1px #009999;padding:3px;margin-top:5px;color:#fff;font-weight:bold;">
                                        <%#System.Web.HttpUtility.HtmlEncode(Eval("Name").ToString())%>
                                </div></td>
                            </tr>
                            <asp:Repeater ID="repeaterForum" runat="server" OnItemDataBound="repeaterForum_ItemDataBound"
                                EnableViewState="false">
                                <ItemTemplate>
                                    <asp:PlaceHolder ID="forumName" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                </ItemTemplate>
                            </asp:Repeater>
                            
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="divButtomButton">
                <asp:Button ID="btnMove" runat="server" OnClick="btnMove_Click" OnClientClick="javascript:return checkHideValueAndConfirm();"
                    CssClass="mbtn" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
