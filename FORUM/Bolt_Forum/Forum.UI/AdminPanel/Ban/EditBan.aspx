<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="EditBan.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Ban.EditBan" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
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
    </script>
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID ="lblSubTitle" runat="server"></asp:Label>
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
            <br />
            <table class="form-table">
                <tr runat="server" id="userType">
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Ban_FieldUser] %>
                    </td>
                    <td>
                        <asp:Label ID="lblUser" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="IPType">
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
                            style="color: Red"> *</span>
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
                        <asp:TextBox ID="txtEndIP" runat="server" CssClass="txtmid"></asp:TextBox><span style="color: Red">*</span>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEndIP" runat="server"
                            ControlToValidate="txtEndIP" ValidationExpression="^(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])\.(25[0-5]|2[0-4]\d|[1]\d\d|[1-9]\d|[1-9])$"
                            Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEndIP" runat="server" ControlToValidate="txtEndIP" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
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
                        <asp:CustomValidator ID="CustomValidatorExpireRequired" runat="server" ControlToValidate="txtExpire" Display="Dynamic"
                         ClientValidationFunction="CheckExpireRequired" ValidationGroup="Save" ValidateEmptyText="true" ></asp:CustomValidator>
                         <asp:CustomValidator ID="CustomValidatorExpireFormat" runat="server" ControlToValidate="txtExpire" Display="Dynamic"
                         ClientValidationFunction="CheckExpireFormat" ValidationGroup="Save" ValidateEmptyText="true" ></asp:CustomValidator>
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
                        <span style="color: Red"><b>* <%=Proxy[EnumText.enumForum_Public_RequiredField] %></b></span>
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
</asp:Content>
