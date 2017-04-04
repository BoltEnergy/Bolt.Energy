<%@ Page Title="Register" Language="C#" MasterPageFile="~/MainMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Pre_Register.aspx.cs" Inherits="Forum.UI.Pre_Register" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function changestate() {
            var btnNext = document.getElementById("<%=btnToRegister.ClientID%>");
            var chk = document.getElementById("chkAgree");

            if (chk.checked == true)
                btnNext.style.color = "Black";
            else
                btnNext.style.color = "Gray";
            btnNext.disabled = !chk.checked;
        }
        window.onload = function init() {
            var btnNext = document.getElementById("<%=btnToRegister.ClientID%>");
            var chk = document.getElementById("chkAgree");

            chk.checked = false;
            btnNext.disabled = true;
        }
    </script>

    <style type="text/css">
        #tbSuject
        {
            width: 350px;
        }
        #htmlEditor
        {
            width: 352px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_Register_TitlePreRegister]%></div>
                </div>
            </div>
        </div>
        <div id="divRulesAndPolicies" runat="server">
            <table class="tb_forum" cellspacing='0'>
                <tr>
                    <td colspan='2' class="row4">
                        <p style="text-align: center">
                            <b>
                                <%=Proxy[EnumText.enumForum_Register_SubtitlePreRegister]%>
                            </b>
                        </p>
                        <br />
                        <br />
                        <asp:Label ID="lblContent" runat="server" Text="" EnableViewState="false"></asp:Label><br />
                        <br />
                        <p style="text-align: center">
                            <input id="chkAgree" type="checkbox"  onclick="changestate()" />
                            <%=Proxy[EnumText.enumForum_Register_ContentEnd]%><br />
                        </p>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="row5" align="center">
                        <p>
                            &nbsp;
                            <asp:Button ID="btnToRegister" runat="server" Enabled="false"  CssClass="btn" ForeColor="Gray"
                                OnClick="btnToRegister_Click" />
                        </p>
                    </td>
                </tr>
            </table>
        </div>
        <%--<div id="divNotAllow" style="display:none" runat="server">
            <table class="tb_forum" cellpadding='0' cellspacing='0'>
            <tr>
                <td class="tdMyLeft" style="text-align: left; padding-left: 100px;">
                    <br />                    
                        <span class="spanNormal" ><asp:Label ID="lblNotAllowRegisterMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
                        </span>
                </td>
            </tr>
                
        </table>
        </div>--%>
    </div>
    <!--
    <div class="divPageDesn">
        <span class="borderFont">Register</span>
    </div>
    <div class="divContent">
        <div class="divCenter">
            <div class="divRules">
                <div class="divRulesTopic">
                    <b>Rules and Policies</b>
                </div>
                <div class="divRulesContent">
                    <br />
                </div>
                <br />
                <div class="divNextBtn">
                    <br />
                    <br />-->
    <!--<input id="btnToRegister" type="button" value="next" class="mbtn"
                        onclick="return btnToRegister_onclick()" />
                        -->
    <!--</div>
            </div>
        </div>
    </div>-->
</asp:Content>
