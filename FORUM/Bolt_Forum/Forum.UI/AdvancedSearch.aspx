<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="AdvancedSearch.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdvancedSearch"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Src="~/UserControl/NavigationBar.ascx" TagName="NavigationBar" TagPrefix="uc" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <style type="text/css">
        #tbSuject
        {
            width: 350px;
        }
        #htmlEditor
        {
            width: 352px;
        }
        .radio
        {
            border: none;
            border-bottom: none;
            border-bottom-color: Blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var comm100_languageName = "<%=LanguageName%>";

    </script>

    <script language="javascript" type="text/javascript" src="JS/DatePicker/WdatePicker.js"></script>

    <div>
        <div class="divMsg">
            <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
        </div>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <div style="float: left">
                            <%=Proxy[EnumText.enumForum_Topic_TitleSerach]%>
                            <span style="font-size: 10px;">&nbsp; </span>
                        </div>
                        <div style="float: left; margin-top: 3px">
                            <img src="Images/help.gif" alt="" onmouseover="showHelp('divHelp','<%=GetTooltipString(Proxy[EnumText.enumForum_Topic_HelpSerach].Replace("'","\\'"))%>');"
                                onmouseout="closeHelp('divHelp');" />
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing="0" width="100%">
            <tr>
                <td class="row1" width="20%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_AdvancedSearch_FieldKeyWord]%></b></p>
                </td>
                <td class="row2" width="80%" valign="middle">
                    <p>
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="txt" Width="360px"> </asp:TextBox>
                        <img src="Images/help.gif" style="margin-bottom: -2px" alt="" onmouseover="showHelp('divHelp','<%=GetTooltipString(Proxy[EnumText.enumForum_Topic_HelpKeyWords].Replace("'","\\'"))%>');"
                            onmouseout="closeHelp('divHelp');" />
                        <span class="require">*<asp:Label ID="lblRequired" runat="server"></asp:Label></span>
                        <asp:RequiredFieldValidator ID="requiredFieldSubject" runat="server" ControlToValidate="txtSubject"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Topic_FieldForum]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:ListBox ID="listboxForum" runat="server" SelectionMode="Single" Rows="12" Width="360px"
                            Font-Size="10"></asp:ListBox>
                        <img src="Images/help.gif" style="margin-bottom: -2px" alt="" onmouseover="showHelp('divHelp','<%=GetTooltipString(Proxy[EnumText.enumForum_Topic_HelpForum].Replace("'","\\'"))%>');"
                            onmouseout="closeHelp('divHelp');" />
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_AdvancedSearch_TitleTimeRange]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <select runat="server" id="dlTimeRange">
                        </select></p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p><b>
                        <%=Proxy[EnumText.enumForum_AdvancedSearch_TitleSearchWithIn]%></b></p>
                </td>
                <td class="row2">
                    <p><asp:RadioButtonList ID="rdSearchIn" runat="server" CssClass="radio" RepeatLayout="Flow" >
                    </asp:RadioButtonList></p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b><%=Proxy[EnumText.enumForum_AdvancedSearch_TitleDisplayAs]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:RadioButtonList ID="rdDisplay" runat="server" CssClass="radio" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                        </asp:RadioButtonList></p>
                </td>
            </tr>
            <tr>
                <td class="row1">
                    <p>
                        &nbsp;</p>
                </td>
                <td class="row2">
                    <p>
                        <span class="require">* <%=Proxy[EnumText.enumForum_Public_RequiredField]%></span></p>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                            OnClientClick="" />
                        &nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn" OnClick="btnReset_Click"
                            ValidationGroup="a" /></p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
