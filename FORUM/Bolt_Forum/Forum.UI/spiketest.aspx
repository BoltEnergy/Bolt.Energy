<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="spiketest.aspx.cs" Inherits="Com.Comm100.Forum.UI.spiketest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="" cellspacing="0" width="100%">
            <tr>
                <th width="50">
                    Status
                </th>
                <th>
                    Subject
                </th>
                <th width="150">
                    Author
                </th>
                <th width="180">
                    Last Post
                </th>
                <th width="50">
                    Replies
                </th>
                <th width="50">
                    Views
                </th>
            </tr>
           
            <tr id="ctl00_ContentPlaceHolder1_trAnnouncementTitle">
                <td colspan="6">
                    <p>
                        <font style="font-size: 22px;">Annoucements11111111111111111111111111111111</font></p>
                </td>
            </tr>
            <tr class="trOdd " onmousemove="changeCSS(this,'trOnMouseOverStyle');" onmouseout="changeCSS(this,'trOdd ');">
                <td class="row_announcements" align="center">
                    <p>
                        <img src="App_Themes/StyleTemplate_Defalut1/images/announcement-topic.GIF" alt='Annoucement'
                            title='Annoucement' /></p>
                </td>
                <td class="row_announcements">
                    <p>
                        <span title='aaa111111111111111'><a href='aaa111111111111111_t695.aspx?siteId=200000&forumId=46'>
                            <strong>aaa111111111111111</strong> </a></span>
                    </p>
                </td>
                <td class="row_announcements" align="center">
                    <p>
                        <span title='admin'><a class='user_link' href='User_Profile.aspx?userId=1&Siteid=200000'
                            target='_blank'>admin</a> </span>
                    </p>
                </td>
                <td class="row_announcements" align="center">
                    <p>
                        <a class='date_link' href='aaa111111111111111_t695.aspx?siteId=200000&forumId=46&postId=-1&goToPost=true&a=1#bottom'>
                            6 Hours ago</a>
                    </p>
                    <p>
                        by&nbsp;<span title='admin'><a class='user_link' href='User_Profile.aspx?userId=1&Siteid=200000'
                            target='_blank'>admin</a></span>
                    </p>
                </td>
                <td class="row_announcements" align="center">
                    <p>
                        3</p>
                </td>
                <td class="row_announcements_end" align="center">
                    <p>
                        25</p>
                </td>
            </tr>
            
        </table>
    </div>
    </form>
</body>
</html>
