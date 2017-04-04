<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="BanUser.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Ban.BanUser" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Import Namespace="Com.Comm100.Forum.Bussiness" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function SelectUser(o, name) {

            if (o != null) {
                var lists = document.getElementsByTagName("input");
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i].id == 'radioSelect') {
                        lists[i].checked = false;
                    }
                }
            }
            var hdName = document.getElementById("<%=hdUserName.ClientID %>");
            hdName.value = name;
            o.checked = true;
        }
        function CheckExpireRequired(object, args) {
            var txtExpire = document.getElementById("<%=txtExpire.ClientID %>").value;
            if (document.getElementById("<%=ddlExpire.ClientID %>").selectedIndex != 0) {
                if (txtExpire != "") {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            else {
                args.IsValid = true;
            }
        }
        function CheckExpireFormat(object, args) {
            var txtExpire = document.getElementById("<%=txtExpire.ClientID %>").value;
            if (document.getElementById("<%=ddlExpire.ClientID %>").selectedIndex != 0) {
                var reg = /^\d*$/
                if (reg.test(txtExpire)) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            else {
                args.IsValid = true;
            }
        }
        function SelectExprie() {
            //debugger;
            //alert(1);
            var ddl = document.getElementById("<%=ddlExpire.ClientID%>");
            var txt = document.getElementById("<%=txtExpire.ClientID%>");
            if (ddl != null && txt != null) {
                if (ddl.options[0].selected == true) {
                    //txt.value = "100";
                    txt.style.display = "none";
                }
                else {
                    //txt.value = "";
                    txt.style.display = "";
                }
            }
        }
        //SelectExprie();
    </script>

    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" OnClick="btnSave_Click"
                ValidationGroup="Save" />
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn" OnClick="btnCancel_Click"
                CausesValidation="false" />
        </div>
        <div class="divTable">
            <%--<div class="divCpFirstTitle" style="padding-left: 30px;">
                <div style="float: left; text-align: left; display: table-cell; vertical-align: middle; 
                    width: 94%">
                    <span><%=Proxy[EnumText.enumForum_Ban_ColumnBanMode] %></span>
                    <asp:RadioButtonList ID="rlistType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rlistType_SelectedIndexChanged"
                        AutoPostBack="True" RepeatLayout="Flow">
                    </asp:RadioButtonList>
                </div>
            </div>--%>
            <br />
            <table class="form-table">
                <tr runat="server" id="userType">
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Ban_FieldUser] %>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" CssClass="txtmid"></asp:TextBox>
                        <asp:Button ID="btnSelectUser" runat="server" CssClass="mbtn" OnClientClick="showWindow('divThickInner','divThickOuter'); return false;">
                        </asp:Button>
                        <span style="color: Red">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUser" runat="server" ControlToValidate="txtUser"
                            Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <%--<tr runat="server" id="IPType">
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Ban_FieldSelectType] %>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rlistIpMode" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rlistIpMode_SelectedIndexChanged" AutoPostBack="True"
                            RepeatLayout="Flow">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr runat="server" id="IPSimple">
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Ban_FieldIp] %>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIP" runat="server" CssClass="txtmid"></asp:TextBox>
                        <span style="color: Red">*</span>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorIP" runat="server"
                            ControlToValidate="txtIP" ValidationExpression="^(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])$"
                            Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorIP" runat="server" ControlToValidate="txtIP" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="IPAdvanced1">
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Ban_FieldStartIp] %>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartIP" runat="server" CssClass="txtmid"></asp:TextBox><span
                            class="alertMsg" style="color: Red"> *</span>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorStartIP" runat="server"
                            ControlToValidate="txtStartIP" ValidationExpression="^(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])$"
                            Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartIP" runat="server" ControlToValidate="txtStartIP" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="IPAdvanced2">
                    <td class="ttd">
                     <%=Proxy[EnumText.enumForum_Ban_FieldEndIp] %>  
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndIP" runat="server" CssClass="txtmid"></asp:TextBox><span class="alertMsg"
                            style="color: Red"> *</span>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEndIP" runat="server"
                            ControlToValidate="txtEndIP" ValidationExpression="^(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])$"
                            Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEndIP" runat="server" ControlToValidate="txtEndIP" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>--%>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Ban_FieldExpireTime] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtExpire" runat="server" CssClass="txtmid"></asp:TextBox>
                        <asp:DropDownList ID="ddlExpire" runat="server">
                        </asp:DropDownList>
                        <span style="color: Red">*</span>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidatorExpire" runat="server"
                            ControlToValidate="txtExpire" ValidationExpression="^\d*$" Display="Dynamic"
                            ValidationGroup="Save"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorExpireTime" runat="server" ControlToValidate="txtExpire" 
                        Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                        <asp:CustomValidator ID="CustomValidatorExpireRequired" runat="server" ControlToValidate="txtExpire"
                            Display="Dynamic" ClientValidationFunction="CheckExpireRequired" ValidationGroup="Save"
                            ValidateEmptyText="true"></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomValidatorExpireFormat" runat="server" ControlToValidate="txtExpire"
                            Display="Dynamic" ClientValidationFunction="CheckExpireFormat" ValidationGroup="Save"
                            ValidateEmptyText="true"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Ban_FieldNotes] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" CssClass="areanormal"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2">
                        <span style="color: Red"><b>
                            <%=Proxy[EnumText.enumForum_Public_RequiredField] %></b></span>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" OnClick="btnSave_Click"
                ValidationGroup="Save" />
            <asp:Button ID="btnCancel2" runat="server" CssClass="mbtn" OnClick="btnCancel_Click"
                CausesValidation="false" />
        </div>
    </div>
    <div id="divScript" runat="server">
    </div>
    <input type="hidden" runat="server" id="hdUserName" value="" />
</asp:Content>
<asp:Content ID="ContentThickBox" runat="server" ContentPlaceHolderID="cphThickBox">
    <div id="divThickInner" style="position: absolute; width:550px; width: 700px; display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
            <div style="height: 550px;overflow-y: scroll; ">
                <div class="divh-header">
                    <div style="float: left; font-size: 1.1em;">
                        <span class="divh-header-spantitle">
                            <%=Proxy[EnumText.enumForum_Ban_TitleSelectUser] %></span>
                    </div>
                   <%-- <div style="float: right;">
                        <span class="divh-header-spanclose">[<a class="linkClose" href="#" onclick="javascript:WindowClose('divThickInner','divThickOuter');"><%=Proxy[EnumText.enumForum_User_FieldClose] %></a>]</span>
                    </div>--%>
                </div>
                <br />
                <div class="divh-table">
                    <div class="divContent">
                        <div class="divTopButton">
                            <asp:Button ID="btnQueryUser1" runat="server" CssClass="mbtn" OnClick="btnQueryUser_Click" />
                        </div>
                        <br />
                        <div class="divTable" style="text-align: center;">
                            <center>
                                <table class="form-table">
                                    <tr>
                                        <td>
                                            <div class="ttd">
                                                <%=Proxy[EnumText.enumForum_UserGroups_FieldEmailOrDisplayName] %></div>
                                        </td>
                                        <td class="ctd">
                                            <asp:TextBox ID="txtKeyWord" runat="server" CssClass="txtmid"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trUserType" runat="server" visible="false">
                                        <td class="ttd">
                                            <%=Proxy[EnumText.enumForum_UserGroups_FieldUserType] %>
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
                            <asp:Button ID="btnQueryUser2" runat="server" CssClass="mbtn" OnClick="btnQueryUser_Click" />
                        </div>
                    </div>
                    <br />
                    <div class="divContent">
                        <div class="divTopButton">
                            <asp:Button ID="btnSelectTop" runat="server" OnClick="btnSelect_Click" CssClass="mbtn" />
                            <input type="button" value="<%=Proxy[EnumText.enumForum_User_FieldClose] %>" class="mbtn" onclick="javascript:WindowClose('divThickInner','divThickOuter');" />
                        </div>
                        <br />
                        <div class="divTable">
                            <table class="the-table" cellpadding='0' cellspacing='0'>
                                <tr>
                                    <th width="5%">
                                        <%--<input type="checkbox" id="selAll" onclick="selectAll(this);" />--%>
                                    </th>
                                    <th width="150px">
                                        <asp:LinkButton ID="lbtnUserEmail" CommandName="UserEmail" runat="server" OnClick="UserSort"
                                            CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnEmail]%></asp:LinkButton>
                                        <asp:Image ID="imgUserEmail" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th width="150px">
                                        <asp:LinkButton ID="lbtnUserUserType" CommandName="UserUserType" runat="server" OnClick="UserSort"
                                            CausesValidation="false"><%=Proxy[EnumText.enumForum_UserGroups_ColumnUserType]%></asp:LinkButton>
                                        <asp:Image ID="imgUserUserType" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th width="150px">
                                        <asp:LinkButton ID="lbtnUserDisplayName" CommandName="UserDisplayName" runat="server"
                                            OnClick="UserSort" CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnDisplayName]%></asp:LinkButton>
                                        <asp:Image ID="imgUserDisplayName" runat="server" Visible="true" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th>
                                        <asp:LinkButton ID="lbtnUserJoinedTime" CommandName="UserJoinedTime" runat="server"
                                            OnClick="UserSort" CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnJoinedTime]%></asp:LinkButton>
                                        <asp:Image ID="imgUserJoinedTime" Visible="false" runat="server" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                </tr>
                                <asp:Repeater ID="rpUser" runat="server">
                                    <AlternatingItemTemplate>
                                        <tr class="trStyle2" onmousedown="highLightRow(this)">
                                            <td>
                                                <input type="radio" id="radioSelect" onclick='SelectUser(this,"<%#Eval("DisplayName") %>");' />
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
                                                <input type="radio" id="radioSelect" onclick='SelectUser(this,"<%#Eval("DisplayName") %>");' />
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
                            <wcw:ASPNetPager ID="aspnetPager1" runat="server" OnChangePageSize="aspnetPager1_ChangePageSize"
                                OnPaging="aspnetPager1_Paging" EnableViewState="true" PageSize="10">
                            </wcw:ASPNetPager>
                        </div>
                        <br />
                        <div class="divButtomButton">
                            <asp:Button ID="btnSelectButtom" runat="server" OnClick="btnSelect_Click" CssClass="mbtn" />
                            <input type="button" value="<%=Proxy[EnumText.enumForum_User_FieldClose] %>" class="mbtn" onclick="javascript:WindowClose('divThickInner','divThickOuter');" />
                        </div>
                    </div>
                </div>
                <br />
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
