<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectUser.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserControl.selectUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/inner/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/inner/layout.css" rel="Stylesheet" type="text/css" />
    <link href="../css/inner/container.css" rel="Stylesheet" type="text/css" />

    <script src="../JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var lastchooseId = "";

        function forumchoosed(forumId) {
            window.close();
        }

        function checkHideValueAndConfirm() {

        }

        function closeWindow() {
            window.close();
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="Content" style="margin-left: 10px; width: 800px">
        <div class="divContent">
            <div class="divTopButton">
                <asp:Button ID="btnQuery1" Text="Query" runat="server" CssClass="mbtn" />
            </div>
            <br />
            <div class="divTable" style="text-align: center;">
                <center>
                    <table class="form-table">
                        <tr>
                            <td>
                                <div class="ttd">
                                    Email/Display Name:</div>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtKeyWord" runat="server" CssClass="txtmid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ttd">
                                User Type:
                            </td>
                            <td class="ctd">
                                <asp:DropDownList ID="ddlUserType" runat="server">                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="ttd">
                            </td>
                            <td class="ctd">                                
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
            <br />
            <div class="divButtomButton">
                <asp:Button ID="btnQuery2" Text="Query" runat="server" CssClass="mbtn" OnClick="btnQuery_Click" />
            </div>
        </div>
        <br />                
        <div class="divContent">

            <div class="divTopButton">
                <asp:Button ID="btnSelectTop" runat="server" Text="Select" 
                OnClick="btnSelect_Click" CssClass="mbtn" />
            </div>
             
            <div class="divTable">
                <table class="form-table" cellpadding='0' cellspacing='0'>
                    <tr>
                        <th width="5%">
                        </th>
                        <th width="30px">
                            Id
                        </th>
                        <th width="150px">
                            Email
                        </th>
                        <th width="150px">
                            User Type
                        </th>
                        <th width="150px">
                            Display Name
                        </th>
                        <th>
                            Joind Time
                        </th>
                    </tr>
                    <asp:Repeater ID="rpUser" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input type="checkbox" id="chbUser" runat="server" value='<%# Eval("id") %>' />
                                    <%--<asp:CheckBox ID="chbUser" runat="server" />--%>
                                </td>
                                <td id="Td2" onclick="javascript:forumchoosed(4);" class="tdForumNoChoosed">
                                    <%# Eval("id") %>
                                </td>
                                <td id="Td1" onclick="javascript:forumchoosed(4);" class="tdForumNoChoosed">
                                    <%# Eval("email") %>
                                </td>
                                <td id="Td7" onclick="javascript:forumchoosed(4);" class="tdForumNoChoosed">
                                    <%--<%# Eval("UserType") %>--%>
                                </td>
                                <td onclick="javascript:forumchoosed(4);" class="tdForumNoChoosed">
                                    <%# Eval("displayname") %>
                                </td>
                                <td onclick="javascript:forumchoosed(4);" class="tdForumNoChoosed">
                                    <%# Convert.ToDateTime(Eval("joinedTime")).ToString("MM-dd-yyyy hh:mm:ss") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>                    
                </table>
            </div>
            <div class="divButtomButton">
                <asp:Button ID="btnSelectButtom" runat="server" Text="Select"
                OnClick="btnSelect_Click" CssClass="mbtn" />
            </div>
        </div>
    </div>
    
           <script type="text/javascript">

               var os = document.getElementsByTagName('input');
               for (var i = 0; i < os.length; i++) {
                   if (document.URL.toLowerCase().indexOf('type=single') < 0)
                       continue;
                   if (os[i].type == 'checkbox') {
                       //alert();
                       os[i].style.display = "none";
                       //os[i].innerHTML = "a";
                       //os[i].type = 'radio';//<input id="Radio1" type=""
                   }
                   else if (os[i].type == 'radio') {
                       os[i].style.display = "";
                   }
               }
            </script>
           
    </form>
</body>
</html>

