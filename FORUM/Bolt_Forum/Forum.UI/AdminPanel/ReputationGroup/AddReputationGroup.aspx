<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AddReputationGroup.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.ReputationGroup.AddReputationGroup" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .spanMsg
        {
            font-weight:bold;
            color:Red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn" 
                CausesValidation="false" onclick="btnCancel_Click" />
        </div>
        <br/>
        <div class="divTable">
            <table class="form-table">
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Reputation_FieldName]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtName" runat="server" CssClass="txtmid"></asp:TextBox>
                        <span class="spanMsg">
                            *
                            <asp:RequiredFieldValidator ID="ValidRequiredName" runat="server"
                                ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Reputation_FieldDescription]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode ="MultiLine" Rows="10" Columns="10" CssClass="areanormal"></asp:TextBox>                        
                    </td>
                    <td class="rtd"></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Reputation_FieldReputationRange] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtBegin" runat="server" CssClass="txtshort"></asp:TextBox>
                        ~
                        <asp:TextBox ID="txtExpire" runat="server" CssClass="txtshort"></asp:TextBox>
                        <span class="spanMsg">
                            *
                        <asp:RequiredFieldValidator ID="ValidRequiredRangeStart" runat="server" ControlToValidate="txtBegin" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="ValidRangeStart" runat="server" ControlToValidate="txtBegin" Display="Dynamic" Type="Integer" MaximumValue="2147483647" MinimumValue="-2147483646"></asp:RangeValidator>    
                        <asp:RequiredFieldValidator ID="ValidRequiredRangeEnd" runat="server" ControlToValidate="txtExpire" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="ValidRangeEnd" runat="server" ControlToValidate="txtExpire" Display="Dynamic" Type="Integer" MaximumValue="2147483647" MinimumValue="-2147483646"></asp:RangeValidator>    
                        </span>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Reputation_FieldRank]%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIcoRepeat" runat="server" CssClass="txtshort" ></asp:TextBox>
                        <img src="../../Images/user reputation.gif"  align="absmiddle"/>
                        <span class="spanMsg">
                            *
                            <asp:RequiredFieldValidator ID="ValidRequiredIcoRepeat" runat="server" ControlToValidate="txtIcoRepeat" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="ValidRangeIcoRepeat" runat="server" ControlToValidate="txtIcoRepeat" Display="Dynamic" Type="Integer" MaximumValue="20" MinimumValue="1"></asp:RangeValidator>
                        </span>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td class="rtd">
                <b><%=Proxy[EnumText.enumForum_Public_RequiredField] %></b>
                </td>
                </tr>
            </table>
        </div>
        <br/>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2"  runat="server" CssClass="mbtn" 
            OnClick="btnSave_Click"/>
            <asp:Button ID="btnCancel2" runat="server" CssClass="mbtn" CausesValidation="false" onclick="btnCancel_Click"/>
        </div>
    </div>
</asp:Content>
