<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="TemplateSetting.aspx.cs" Inherits="Forum.UI.AdminPanel.Styles.StyleSettings" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript">
    function setSystemTemplate(_templateId) {
        var hdCurrentId = document.getElementById('<%= hdCurrentId.ClientID %>');
        var lists = document.getElementsByTagName("input");
        
        //set Init Value
        for (var i = 0; i < lists.length; i++) {
            if (lists[i].type == 'radio') {
                lists[i].checked = false;
            }            
        }
        
        //set Current Template
        for (var i = 0; i < lists.length; i++) {
            if (lists[i].type == 'radio') {                               
                if (lists[i].name == _templateId)
                    lists[i].checked = true;
                else
                    lists[i].checked = false;
            }
        }
    }
    function setCurrentId(_current, _id) {
        var hdCurrentId = document.getElementById('<%= hdCurrentId.ClientID %>');
        
        if (_current != null) {
            //set all checked is false
            var lists = document.getElementsByTagName("input");
            

            for (var i = 0; i < lists.length; i++) {
                if (lists[i].type == 'radio') {
                    lists[i].checked = false;
                }                
            }
            //set current object checked is true
            _current.checked = 'true';
        }
        //set hidden.value
        if (hdCurrentId != null) {
            hdCurrentId.value = _id;
        }
        else
            hdCurrentId.value = null;
    }

    function OpenWindow(_url) {
        var lists = document.getElementsByTagName("input");
        
        var hdCurrentId = document.getElementById('<%= hdCurrentId.ClientID %>');        
        
        var url = _url + "&TemplateID=" + hdCurrentId.value.toString();
        window.open(url);
    }

    function ImgAutoSize(img, FitWidth, FitHeight) {
        var imgObj = new Image();
        imgObj.src = img.src;
        if (imgObj.width / imgObj.height >= FitWidth / FitHeight) {
            if (imgObj.width > FitWidth) {

                img.height = (imgObj.height * FitWidth) / imgObj.width;
                img.width = FitWidth;
            }
            else {
                img.height = imgObj.height;
                img.width = imgObj.width;
            }
        }
        else {
            if (imgObj.height > FitHeight) {
                img.width = (FitHeight * imgObj.width) / imgObj.height;
                img.height = FitHeight;
            }
            else {
                img.height = imgObj.height;
                img.width = imgObj.width;
            }
        }

    }
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <span class="TitleLabel"><%=Proxy[EnumText.enumForum_Styles_TitleTemplateSetting]%></span>
   </div>
   
   <div class="divSubTitle">
        <%=Proxy[EnumText.enumForum_Styles_SubtitleTemplateSetting]%>
    </div>
    <br />
   <div class="divMsg">
        <asp:Label id="lblError" runat="server" CssClass="ErrorLabel" EnableViewState ="false"></asp:Label>
        <asp:Label id="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
   
   </div>
   
   <div class="divContent">  
   <div style="width:100%;">
            <div class="divTopButton">            
                <input value='<%=Proxy[EnumText.enumForum_Styles_ButtonPrivew] %>' onclick="OpenWindow('../../default.aspx?siteid=<%= SiteId %>')" type="button" class="mbtn" />
                <asp:Button ID="btnSave2" runat="server" OnClick="btnSave_Click" CssClass="mbtn" />
                <asp:Button ID="btnReturn1" runat="server" CssClass="mbtn" 
                    onclick="btnCancel_Click" />
            </div>
    <div class="divTable">
        
            <br />
             <fieldset class="fieldsetCp" style="background-color:#f8f8f8">
                                    <legend><%=Proxy[EnumText.enumForum_Styles_TitleTemplateSetting]%></legend>
                                    <br />
            <asp:Repeater ID="repeater" runat="server">
                <HeaderTemplate>
                <table style= "padding:5px;margin:auto;">
                <tr>
                </HeaderTemplate>
                
                <ItemTemplate>
                    <td style = "width: 110px; height :150px; padding: 5px; text-align:center">
                        <div>
                        <img alt="" src='<%# string.Format("../../images/{0}",Eval("ThumbnailPath")) %>'  onload = "ImgAutoSize(this,300,400)" />
                        <%--<asp:Image ID="image" runat="server" height='300px' width='400px' ImageUrl='<%# string.Format("~/images/{0}",Eval("ThumbnailPath")) %>' OnLoad = "ImgAutoSize(this,300,400)" />--%>
                        <br />
                        <div style="margin-bottom:30px;margin-right:20px;">
                        <center><input type="radio" onclick='setCurrentId(this,<%# Eval("id") %>)' templateId='<%# Eval("id") %>' name='<%# Eval("id") %>'/> </center>                         
                        </div>
                        </div>
                    </td>  
                    <%#Container.ItemIndex % 2 == 1 ? "</tr><tr>" : "" %>                  
                </ItemTemplate>                
                <FooterTemplate>
                </tr>
                </table>
                </FooterTemplate>
            </asp:Repeater> 
            </fieldset>  
            <br />         
        </div> 
    </div>
    <div  class="divButtomButton">           
            <input value='<%=Proxy[EnumText.enumForum_Styles_ButtonPrivew]%>' onclick="OpenWindow('../../default.aspx?siteid=<%= SiteId %>')" type="button" class="mbtn" />
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="mbtn" />
            <asp:Button ID="btnReturn2" runat="server" CssClass="mbtn"
                onclick="btnCancel_Click" />
    </div>
   </div>
    <input type="hidden" runat="server" id="hdCurrentId" controlType="switchvalue"/>   
</asp:Content>
