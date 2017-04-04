<%@ Page Title="Forums" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ForumAdd.aspx.cs" Inherits="Forum.UI.AdminPanel.Forums.ForumAdd"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Import Namespace="Com.Comm100.Forum.Bussiness" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function categoryNeed(source, args) {
            args.IsValid = document.getElementById("<%=ddlCategory.ClientID %>").selectedIndex >= 0;
        }
        function addModerator(name, id) {
            var listBoxM = document.getElementById("<%=listboxModerator.ClientID %>");
            listBoxM.add(new Option(name, id));
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
    </div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" OnClick="btnSave_Click" CssClass="mbtn">
            </asp:Button>
            <asp:Button ID="btnCancel1" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                CssClass="mbtn"></asp:Button>
        </div>
        <div class="divTable">
            <br />
            <table class="form-table">
                <tr>
                    <td class="ttd">
                        <asp:Label ID="lblName" runat="server"><%=Proxy[EnumText.enumForum_Forums_FieldName]%></asp:Label>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtName" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <span style="color: Red">*</span>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                            Display="Dynamic"><%=Proxy[EnumText.enumForum_Forums_ErrorNameRequired]%></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <asp:Label ID="lblDescription" runat="server"><%=Proxy[EnumText.enumForum_Forums_FieldDescription]%></asp:Label>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="areanormal" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <asp:Label ID="lblCategory" runat="server"><%=Proxy[EnumText.enumForum_Forums_FieldCategory]%></asp:Label>
                    </td>
                    <td class="ctd">
                        <asp:DropDownList ID="ddlCategory" runat="server">
                        </asp:DropDownList>
                        <span style="color: Red">*</span>
                    </td>
                    <td class="rtd">
                        <asp:CustomValidator ID="cCategory" runat="server" ControlToValidate="ddlCategory"
                            ClientValidationFunction="categoryNeed" ValidateEmptyText="true" Display="Dynamic"><%=Proxy[EnumText.enumForum_Forums_ErrorCategoryRequired]%> </asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <asp:Label ID="lblModerator" runat="server"><%=Proxy[EnumText.enumForum_Forums_Moderator]%> </asp:Label>
                    </td>
                    <td class="ctd">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="listboxModerator" runat="server" SelectionMode="Multiple" Width="180px"
                                            Rows="7"></asp:ListBox>
                                    </td>
                                    <td style="padding-left: 5px">
                                        <asp:Button ID="btnAddModerator" runat="server" class="lbtn" OnClientClick="javascript:showWindow('divThickInner', 'divThickOuter');return false;"
                                            CausesValidation="false" />
                                        <br />
                                        <br />
                                        <asp:Button ID="btnRemoveModerator" runat="server" class="lbtn" OnClick="btnRemoveModerator_Click"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td class="rtd">
                        <span style="color: Red">*</span>
                        <asp:Label ID="lblModeratorReuired" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="checkBoxReply" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="checkBoxPay" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2" class="rtd">
                        <asp:Label ID="lblRequired" runat="server"><%=Proxy[EnumText.enumForum_Forums_FieldRequireField]%></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" OnClick="btnSave_Click" CssClass="mbtn" />
            <asp:Button ID="btnCancel2" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                CssClass="mbtn" />
        </div>
    </div>
    <div id="divScript" runat="server">
    </div>
</asp:Content>
<asp:Content ID="ContentThickBox" ContentPlaceHolderID="cphThickBox" runat="server">
    <%-- <div id="" style="position: absolute; display: none; height:500px;
        width: 700px; border: solid 3px #aaa; padding: 5px 5px; background-color: White; overflow-y:scroll">

        <br />
    </div>--%>
    <div id="divThickInner" style="position: absolute; height: 550px; width: 700px; display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
            <div style="overflow-y: scroll; height:550px">
                <div class="divh-header" >
                    <div style="float: left; font-size: 1.1em;">
                        <span class="divh-header-spantitle">
                            <asp:Label ID="lblSelectUserTitle" runat="server"></asp:Label></span>
                    </div>
                </div>
                <br />
                <div class="divh-table">
                    <div class="divContent">
                        <div class="divTopButton">
                            <asp:Button ID="btnQueryUser1" runat="server" CssClass="mbtn" OnClick="btnQueryUser_Click"
                                OnClientClick="return true;" CausesValidation="false" />
                        </div>
                        <br />
                        <div class="divTable" style="text-align: center;">
                            <center>
                                <table class="form-table">
                                    <tr>
                                        <td>
                                            <div class="ttd">
                                                <%=Proxy[EnumText.enumForum_User_QueryEmailOrDisplayName] %>
                                            </div>
                                        </td>
                                        <td class="ctd">
                                            <asp:TextBox ID="txtKeyWord" runat="server" CssClass="txtmid"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trUserType" runat="server" visible="false">
                                        <td class="ttd">
                                            <%=Proxy[EnumText.enumForum_User_FieldUserType] %>
                                        </td>
                                        <td class="ctd">
                                            <asp:DropDownList ID="ddlUserType" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                        <br />
                        <div class="divButtomButton">
                            <asp:Button ID="btnQueryUser2" runat="server" CssClass="mbtn" OnClick="btnQueryUser_Click"
                                OnClientClick="return true;" CausesValidation="false" />
                        </div>
                    </div>
                    <br />
                    <div class="divContent">
                        <div class="divTopButton">
                            <asp:Button ID="btnSelectTop" runat="server" OnClick="btnSelect_Click" OnClientClick="return true;"
                                CausesValidation="false" CssClass="mbtn" />
                            <input type="button" class="mbtn" onclick="javascript:WindowClose('divThickInner','divThickOuter');"
                                value='<%=Proxy[EnumText.enumForum_Topic_FieldClose] %>' />
                        </div>
                        <br />
                        <div class="divTable">
                            <table class="the-table" cellpadding='0' cellspacing='0'>
                                <tr>
                                    <th width="5%">
                                        <%--<input type="checkbox" id="selAll" onclick="selectAll(this);" />--%>
                                    </th>
                                    <%--<th width="30px">
                                    <%=Proxy[EnumText.enumForum_User_ColumnId]%>
                                </th>--%>
                                    <th width="30%">
                                        <asp:LinkButton ID="lbtnUserEmail" CommandName="UserEmail" runat="server" OnClick="UserSort"
                                            CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnEmail]%></asp:LinkButton>
                                        <asp:Image ID="imgUserEmail" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th width="20%">
                                        <asp:LinkButton ID="lbtnUserUserType" CommandName="UserUserType" runat="server" OnClick="UserSort"
                                            CausesValidation="false"><%=Proxy[EnumText.enumForum_UserGroups_ColumnUserType]%></asp:LinkButton>
                                        <asp:Image ID="imgUserUserType" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th width="20%">
                                        <asp:LinkButton ID="lbtnUserDisplayName" CommandName="UserDisplayName" runat="server"
                                            OnClick="UserSort" CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnDisplayName]%></asp:LinkButton>
                                        <asp:Image ID="imgUserDisplayName" runat="server" Visible="true" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th width="25%">
                                        <asp:LinkButton ID="lbtnUserJoinedTime" CommandName="UserJoinedTime" runat="server"
                                            OnClick="UserSort" CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnJoinedTime]%></asp:LinkButton>
                                        <asp:Image ID="imgUserJoinedTime" Visible="false" runat="server" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                </tr>
                                <asp:Repeater ID="rpUser" runat="server" OnItemDataBound="rpUser_ItemDataBound">
                                    <AlternatingItemTemplate>
                                        <tr class="trStyle2" onmousedown="highLightRow(this)">
                                            <td>
                                                <input type="checkbox" id="checkBoxUser" runat="server" value='<%#Eval("Id") %>' />
                                            </td>
                                            <%--<td>
                                            <%# Eval("id") %>
                                        </td>--%>
                                            <td>
                                                <span title='<%#GetTooltipString(Eval("email").ToString()) %>'>
                                                    <%#Server.HtmlEncode(Convert.ToString(Eval("email")))%></span>
                                            </td>
                                            <td>
                                                <%#GetItemType((UserOrOperator)Container.DataItem)%>
                                            </td>
                                            <td>
                                                <span title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("DisplayName"))), 12)%></span>
                                            </td>
                                            <td>
                                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <ItemTemplate>
                                        <tr class="trStyle1" onmousedown="highLightRow(this)">
                                            <td>
                                                <input type="checkbox" id="checkBoxUser" runat="server" value='<%#Eval("Id") %>' />
                                            </td>
                                            <%--<td>
                                            <%# Eval("id") %>
                                        </td>--%>
                                            <td>
                                                <span title='<%#GetTooltipString(Eval("email").ToString()) %>'>
                                                    <%#Server.HtmlEncode(Convert.ToString(Eval("email")))%></span>
                                            </td>
                                            <td>
                                                <%#GetItemType((UserOrOperator)Container.DataItem)%>
                                            </td>
                                            <td>
                                                <span title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("DisplayName"))), 12)%></span>
                                            </td>
                                            <td>
                                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <div style="text-align: center;">
                            <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                                OnPaging="aspnetPager_Paging" EnableViewState="true" PageSize="10">
                            </wcw:ASPNetPager>
                        </div>
                        <br />
                        <div class="divButtomButton">
                            <asp:Button ID="btnSelectButtom" runat="server" OnClick="btnSelect_Click" OnClientClick="return true;"
                                CausesValidation="false" CssClass="mbtn" />
                            <input type="button" class="mbtn" onclick="javascript:WindowClose('divThickInner','divThickOuter');"
                                value='<%=Proxy[EnumText.enumForum_Topic_FieldClose] %>' />
                        </div>
                    </div>
                </div>
            </div>
            <b class="R_inner_bottom"><b class="R_inner_4"></b><b class="R_inner_3"></b><b class="R_inner_2">
            </b><b class="R_inner_1"></b></b>
        </div>
        <b class="R_outer_bottom"><b class="R_outer_4"></b><b class="R_outer_3"></b><b class="R_outer_2">
        </b><b class="R_outer_1"></b></b>
    </div>
    <div id="divThickOuter" style="position: absolute; filter: alpha(opacity=50); -moz-opacity: 0.5;
        opacity: 0.5; border: 0px; display: none; background-color: #ccc">
    </div>
</asp:Content>
