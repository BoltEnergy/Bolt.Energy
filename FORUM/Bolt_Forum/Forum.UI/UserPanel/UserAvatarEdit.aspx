<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UserAvatarEdit.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.UserAvatarEdit" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        //if one of radiobuttons choosed,return true;
        function BtnSaveClick() {
            var rbtns = document.getElementsByTagName("input");
            for (var i = 0; i < rbtns.length; i++) {
                if (rbtns[i].name == "rbtnTemp") {
                    if (rbtns[i].checked == true)
                        return true;
                }
            }
            return false;
        }
        function changePanel(index) {
            var divCustom = document.getElementById("Custom");
            var divSystem = document.getElementById("System");

            var btnSave1 = document.getElementById("<%=btnSave1.ClientID%>");
            var btnUpload = document.getElementById("<%=btnUpload.ClientID%>");

            if (index == 0) {
                divCustom.style.display = "";
                divSystem.style.display = "none";
                //btnUpload.focus();
            }
            else {
                divCustom.style.display = "none";
                divSystem.style.display = "";
                btnSave1.focus();
            }
        }

    </script>

    <script type="text/javascript">
        window.onload = function RadioButtonChooseIndex() {
            var AvatorIndex = "<%=AvatorIndex%>";
            var rbtnChoose = document.getElementById("rbtnAvator" + AvatorIndex);
            if (rbtnChoose != null)
                rbtnChoose.checked = true;

            if ("<%=IfShowCustom %>" == "display:none") {
                document.getElementById("<%=rblMode.ClientID %>_0").checked = false;
                document.getElementById("<%=rblMode.ClientID %>_1").checked = true;
            }
        }
    </script>

    <div class="divMsg">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_User_TitleEditAvatar]%>&nbsp;
                        <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_User_SubtitleEditAvatar]%>')"
                            onmouseout="CloseHelp()" />
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0' cellpadding="0">
            <tr>
                <td colspan='2' class="row6">
                    <p>
                        <b>
                            <div class="c_b_list">
                                <asp:RadioButtonList ID="rblMode" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="Custom"></asp:ListItem>
                                    <asp:ListItem Value="System"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </b>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan='2' class="row7">
                    <table border='0' id="System" style="<%=IfShowSystem%>;"  cellspacing='0' cellpadding="0" class="tb_inner">
                        <tr align="center">
                            <td width="20%" class="row2" align="right">
                                <p>
                                    <b>
                                        <%=Proxy[EnumText.enumForum_User_LabelSystemAvatars]%></b></p>
                            </td>
                            <td class="row2">
                                <asp:DataList ID="dlstViewAvators" runat="server" RepeatLayout="Table" RepeatColumns="5"
                                    RepeatDirection="Horizontal" ItemStyle-BorderWidth='0' EnableViewState="false">
                                    <ItemTemplate>
                                        <td>
                                            <p>
                                                <input id='rbtnAvator<%# Container.ItemIndex %>' type="radio" name="rbtnTemp" value='<%# Container.DataItem %>' />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <img src='<%# Container.DataItem %>' width="60" height="60" />
                                            </p>
                                        </td>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="row5" align="center">
                                <p>
                                    <asp:Button ID="btnSave1" runat="server" CssClass="btn" OnClick="btnSave1_Click"
                                        OnClientClick="return BtnSaveClick();" />
                                </p>
                            </td>
                        </tr>
                    </table>
                    <table id="Custom" border='0'  style="<%=IfShowCustom%>" cellpadding='0' cellspacing='0'>
                        <tr>
                            <td class="row2" width="30%" align="right">
                                <p>
                                    <b>
                                        <%=Proxy[EnumText.enumForum_User_LabelCurrentAvatar]%></b></p>
                            </td>
                            <td class="row2" >
                                <p>
                                    <asp:Image ID="ImgPicture" runat="server" EnableViewState="false" />
                                    <%--<img id="ImgPicture" runat="server" alt="My Avatar" enableviewstate="false"/>--%>
                                </p>
                            </td>
                        </tr>
                        <tr id="NewAvatar" runat="server">
                            <td class="row2" width="30%" align="right">
                                <p>
                                    <b>
                                        <%=Proxy[EnumText.enumForum_User_FieldNewAvatar]%></b></p>
                            </td>
                            <td class="row2" >
                                <p>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="txtnormal" />
                                    <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_User_HelpUploadDescription]%>')"
                                        onmouseout="CloseHelp()" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="row5" align="center">
                                <p>
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn" OnClick="btnUpload_Click" />
                                    <asp:Button ID="btnDefault" runat="server" CssClass="btn" OnClick="btnDefault_Click" />
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
