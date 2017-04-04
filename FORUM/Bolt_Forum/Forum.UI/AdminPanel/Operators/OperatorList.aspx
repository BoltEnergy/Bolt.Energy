<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="OperatorList.aspx.cs" Inherits="Forum.UI.AdminPanel.Operators.OperatorList" ValidateRequest="false" %>
<%@ Import Namespace="Com.Comm100.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="WCW" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="divTitle"><span class="TitleLabel"><%=Proxy[EnumText.enumForum_Operator_TitleListPage]%></span></div>
<div class="divSubTitle"><asp:Label ID="lblSubTitle" runat="server" Text=""><%=Proxy[EnumText.enumForum_Operator_SubtitleListPage]%></asp:Label></div>
<br/>
<div class="divMsg"><asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
<div class="divContent">
    <div class="divTopButton">
        <asp:Button ID="btnNewOperator" runat="server" CssClass="lbtn" onclick="btnNewOperator_Click"  />
    </div>
    <br/>
    <div class="divTable">
        <asp:GridView ID="gdvOperatorList" runat="server" CssClass="the-table" 
            AutoGenerateColumns="False" AllowSorting="True" GridLines="None" 
            OnSorting="gdvOperatorList_Sorting" OnRowDataBound="gdvOperatorList_RowDataBound" 
            EnableViewState="False" >
            <Columns>
                <asp:BoundField DataField="Id" SortExpression="Id">
                    <HeaderStyle Width="8%"/>
                    <ItemStyle Width="8%" />
                </asp:BoundField>
                
                <asp:BoundField DataField="Email" SortExpression="Email">
                    <HeaderStyle Width="30%" />
                    <ItemStyle Width="30%" />
                </asp:BoundField>
                
                <asp:BoundField DataField="DisplayName" SortExpression="Name">
                    <HeaderStyle Width="20%"/>
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                
                <asp:BoundField DataField="Description" >
                    <HeaderStyle Width="18%" />
                    <ItemStyle Width="18%" />
                </asp:BoundField>
                
                <asp:TemplateField>
                    <HeaderStyle Width="60"  CssClass="cth"/>
                    <ItemStyle Width="60" CssClass="ctd"/>
                    <ItemTemplate>
                        <%#Convert.ToBoolean(Eval("IfAdmin")) == true ? Proxy[EnumText.enumForum_Public_Yes] : Proxy[EnumText.enumForum_Public_No]%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <HeaderStyle Width="60"  CssClass="cth"/>
                    <ItemStyle Width="60" CssClass="ctd"/>
                    <ItemTemplate>
                        <%#Convert.ToBoolean(Eval("IfActive")) == true ? Proxy[EnumText.enumForum_Public_Yes] : Proxy[EnumText.enumForum_Public_No]%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <HeaderStyle Width="70"  CssClass="cth"/>
                    <ItemStyle Width="70"  CssClass="ctd"/>
                    <ItemTemplate><a href="ResetPasswordByAdmin.aspx?action=edit&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>"><img alt='<%= Proxy[EnumText.enumForum_Operator_ColumnEdit]%>' src="../../images/database_edit.gif" /></a>
                    </ItemTemplate>                      
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <HeaderStyle Width="30"  CssClass="cth"/>
                    <ItemStyle Width="30" CssClass="ctd"/>
                    <ItemTemplate><a href="OperatorEdit.aspx?action=edit&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>"><img alt='<%=Proxy[EnumText.enumForum_Operator_ColumnEdit] %>' src="../../images/database_edit.gif" /></a>
                    </ItemTemplate> 
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <HeaderStyle Width="40"  CssClass="cth"/>
                    <ItemStyle Width="40" CssClass="ctd"/>
                    <ItemTemplate ><a href="OperatorList.aspx?action=delete&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>"><img alt='<%= Proxy[EnumText.enumForum_Operator_ColumnDelete]%>' onclick="return confirm('<%= Proxy[EnumText.enumForum_Operator_ConfirmAreYouSureDelete]%>');"  src="../../images/database_delete.gif" /></a>
                    </ItemTemplate>
                </asp:TemplateField>                
            </Columns>
        </asp:GridView>
    </div>
    <div>
    <WCW:ASPNetPager ID="aspnetPager" runat="server" 
        onchangepagesize="aspnetPager_ChangePageSize" 
        onpaging="aspnetPager_Paging"></WCW:ASPNetPager>
    </div>
    <br/>
    <asp:PlaceHolder ID="phClearSession" runat="server" EnableViewState="false"></asp:PlaceHolder>
</div>
    <script type="text/javascript">
        initTabletrColor("ctl00_ContentPlaceHolder1_gdvOperatorList", "trStyle1", "trStyle2");
    </script>
</asp:Content>
