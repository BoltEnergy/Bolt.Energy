<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserView.aspx.cs" Inherits="Forum.UI.AdminPanel.Users.UserView" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .WordBreak
        {
            word-break: break-all !important;
            word-wrap: break-word;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <link href="../../CSS/inner/common.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/inner/container.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/inner/layout.css" rel="stylesheet" type="text/css" />
    <div id="Content" style="margin-left: 10px; width: 500px">
        <div class="divTitle">
            <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
        </div>
        <div class="divSubTitle">          
             <asp:Label ID="lblSubTitle" runat="server" ></asp:Label>
        </div>
        <br />
          <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
        <div class="divContent">
            <div class="divTopButton">
            </div>
            <br />
            <div class="divCpFirstTitle">
                <%=Proxy[EnumText.enumForum_User_FieldBasicInformation]%>
            </div>
            <div class="divTable">
                <table id="tbInfor" class="form-table" width="100%">
                    <tr id="trAvatar" runat="server">
                        <td class="ttd" width="30%">
                            <%=Proxy[EnumText.enumForum_User_FieldAvatar]%>
                        </td>
                        <td class="ctd">
                            <asp:Image ID="imgPicture" runat="server" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr id="trUserName" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldUserName]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblUserName" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="DisplayName" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldDisplayName]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblDisplayName" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trEmail" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldEmail]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblEmail" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trAge" runat="server">
                        <td class="ttd">
                           <%=Proxy[EnumText.enumForum_User_FieldAge]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblAge" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trGender" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldGender]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblGender" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trOccupation" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldOccupation]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblOccupation" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trCompany" runat="server">
                        <td class="ttd">
                           <%=Proxy[EnumText.enumForum_User_FieldCompany]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblCompany" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trPhone" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldPhoneNumber]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblPhone" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trInterests" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldInterests]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblInterests" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trFax" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldFaxNumber]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblFax" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trHomePage" runat="server">
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldHomePage]%> 
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblHomePage" runat="server" EnableViewState="False" CssClass="WordBreak"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="divCpFirstTitle">
                <%=Proxy[EnumText.enumForum_User_FieldStatisticalInformation]%></div>
            <div class="divTable">
                <table class="form-table" width="100%">
                    <tr>
                        <td class="ttd" width="30%">
                           <%=Proxy[EnumText.enumForum_User_FieldJoined]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblJoined" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldLastVisit]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblLastVisit" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldLastLoginIP] %>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblLastLoginIP" runat="server" EnableViewState="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldPosts]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblPosts" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldScore]%>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblScore" runat="server" EnableViewState="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_FieldReputation]%>
                        </td>
                        <td class="ctd">
                            <%--<asp:Label ID="lblReputation" runat="server" EnableViewState="false"></asp:Label>--%>
                            <asp:PlaceHolder runat="server" ID="PHUserReputations"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="divButtomButton">
                <input onclick="javascript:window.close();return false;" type="button" value='<%=Proxy[EnumText.enumForum_User_FieldClose]%>'
                    class="mbtn" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
