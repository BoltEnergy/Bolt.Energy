<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="HeaderFooterSetting.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Styles.HeaderFooterSetting" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Src="../../UserControl/HTMLEditor.ascx" TagName="HTMLEditor" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function PreviewLogo(ifAdvanced, ifDefault) {
            var radioEasyMode = document.getElementById("ctl00_ContentPlaceHolder1_rlistMode_0");
            var radioAdvancedMode = document.getElementById("ctl00_ContentPlaceHolder1_rlistMode_1");
            var ctl00_ContentPlaceHolder1_imgLogo = document.getElementById("ctl00_ContentPlaceHolder1_imgLogo");
            //ifAdvanced?
            if (radioEasyMode.checked == false && radioAdvancedMode.checked == true)
            { ifAdvanced = true; }
            else if (radioEasyMode.checked == true && radioAdvancedMode.checked == false)
            { ifAdvanced = false; }

            if (ifAdvanced == false) {
                //window.open("../../default.aspx?ifAdvancedMode=false&siteId=<%=SiteId%>");
                return true;
            }
            else {
                window.open('../../default.aspx?ifAdvancedMode=true&siteid=<%=SiteId%>');
                return false;
            }

            return true;
        }

        function ShowModeChanged() {
            var btnPreivew1 = document.getElementById("<%=btnPreview1.ClientID %>");
            var btnPreivew2 = document.getElementById("<%=btnPreview2.ClientID %>");

            var radioAdvancedMode = document.getElementById("ctl00_ContentPlaceHolder1_rlistMode_1");

            btnPreivew1.disabled = !radioAdvancedMode.checked;
            btnPreivew2.disabled = !radioAdvancedMode.checked;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" Text="" CssClass="TitleLabel" runat="server"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server" Text=""></asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divMsg">
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnPreview1" runat="server" CssClass="mbtn" EnableViewState="False"
                OnClientClick="return PreviewLogo();" OnClick="btnPreview_Click" />
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" EnableViewState="False"
                OnClick="btnSave_Click" />
            <asp:Button ID="btnReturn1" runat="server" CssClass="mbtn" 
                onclick="btnCancel_Click" />
        </div>
        <br />
        <div class="divCpFirst">
            <div class="divCpFirstTitle">
                <div style="float: left; text-align: left; display: table-cell; vertical-align: middle;
                    width: 94%">
                    <span style="vertical-align: middle;"><b>
                        <%=Proxy[EnumText.enumForum_Styles_FieldSelectType]%></b></span>
                    <asp:RadioButtonList ID="rlistMode" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        EnableViewState="True" AutoPostBack="True" 
                        onselectedindexchanged="rlistMode_SelectedIndexChanged">
                        <asp:ListItem Value="0"></asp:ListItem>
                        <asp:ListItem Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="divEasyMode" class="divCpFirstContent" runat="server">
                <fieldset class="fieldsetCp" style="padding-bottom: 10px;">
                    <legend>
                        <%=Proxy[EnumText.enumForum_Styles_LabelLogo]%></legend>
                    <br />
                    <div style="text-align: left;">
                        <img id="imgLogo" alt="" runat="server" src="" />
                    </div>
                    <br />
                    <asp:PlaceHolder ID="phUploadLogo" runat="server">
                        <div style="text-align: left">
                            <asp:FileUpload ID="fileUploadLogo" runat="server" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" class="mbtn" 
                              onclick="btnUpload_Click" />
                        </div>
                        <br />
<%--                        <div style="color: Red; text-align:left">
                            <span><%=Proxy[EnumText.enumForum_Styles_FieldUploadDescription]%></span>                    
                        </div>--%>
                    </asp:PlaceHolder>
                    
                    <asp:PlaceHolder ID="phUploadButton" runat="server">
                    <div>
                        <input type="button" style="float: left;" value="<%= BtnEditLogoText %>"
                        class="sllbtn"  onclick="javascript:window.open('<%= GetEditLogoUrl %>');" />
                    </div>
                    <div style="color: Red; text-align:left">
                        &nbsp;&nbsp;&nbsp;&nbsp;<span><%=Proxy[EnumText.enumForum_Styles_NoteEditLogo] %></span>
                    </div>
                    </asp:PlaceHolder>
                </fieldset>
            </div>
            <div id="divAdvancedMode"  class="divCpFirstContent" runat="server">
                <table width="100%" style="table-layout:fixed;">
                    <tr>
                        <td>
                            <fieldset class="fieldsetCp" style="margin: 0 0 0 0; padding: 0 0 0 0;">
                                <legend><%=Proxy[EnumText.enumForum_Styles_LabelHeaderFooter]%></legend><b>
                                    <%=Proxy[EnumText.enumForum_Styles_LabelPageHeader]%></b>
                                <div style="margin-top: 5px; margin-bottom: 5px; margin-right: 5%; margin-left: 5%; overflow:hidden;">
                                    <uc1:HTMLEditor ID="txtPageHeader" runat="server" Cols="60" Limit="true" />
                                </div>
                                <b>
                                    <%=Proxy[EnumText.enumForum_Styles_LabelPageFooter]%></b>
                                <div style="margin-top: 5px; margin-bottom: 5px; margin-right: 5%; margin-left: 5%; overflow:hidden;">
                                    <textarea id="txtPageFooter" runat="server" name="editor" rows="15" cols="60" style="width: 100%"></textarea>
                                </div>
                                <br />
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="phScript" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnPreview2" runat="server" CssClass="mbtn" EnableViewState="False"
                OnClientClick="return PreviewLogo();" OnClick="btnPreview_Click" />
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" EnableViewState="False"
                OnClick="btnSave_Click" />
            <asp:Button ID="btnReturn2" runat="server" CssClass="mbtn" 
                onclick="btnCancel_Click" />
            <input type="hidden" id="hdnControlPrefix" value="<%=ControlPrefix %>" />
            <asp:HiddenField ID="hdnIfPreview" runat="server" Value="0" />
            <asp:HiddenField ID="hdnIfUpload" runat="server" Value="0" />
        </div>
    </div>
</asp:Content>
