<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMasterPage.Master"
    CodeBehind="Topic.aspx.cs" Inherits="Forum.UI.Topic" ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/HTMLEditor.ascx" TagName="HTMLEditor" TagPrefix="uc" %>

<script runat="server">

    protected void RepeaterTopic_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="JS/Common/RepeatSubmit.js" type="text/javascript"></script>

    <script src="JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function editPost(postId) {
            window.location.href = "EditTopicOrPost.aspx?postId=" + postId + "&topicId=<%=TopicId%>&forumId=<%=ForumId%>&siteId=<%=SiteId%>";
        }

        function deletePost(postId) {
            if (confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmDeletePost]%>')) {
                if('<%=IfAnnoucement%>'.toLowerCase() == 'true')
                {
                    if(!confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmDeleteAnnoucementPost]%>'))
                        return;
                }
                window.location.href = "Topic.aspx?action=delete&postId=" + postId + "&topicId=<%=TopicId%>&siteId=<%=SiteId%>&forumId=<%=ForumId%>";
            }
            else
                return;
        }
        
        function FavoriteTopic()
        {
            if(!confirm('<%=Proxy[EnumText.enumForum_Topic_Page_ConfirmFavoriate]%>')){return false;}
        }
        
        function unFavoriteTopic()
        {
            if(!confirm('<%=Proxy[EnumText.enumForum_Topic_Page_ConfirmUnFavoriate]%>')){return false;}
        }

        function markAsAnswer(postId) {
            if (confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmMarkAsAnswer]%>')) {
                window.location.href = "Topic.aspx?action=mark&postId=" + postId + "&topicId=<%=TopicId%>&siteId=<%=SiteId%>&forumId=<%=ForumId%>&goToAnswer=true&a=1#Post" + postId;
            }
            else
                return;
        }

        function markAsAnswerInsteadOfOld(postId) {
            if (confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmMarkAsAnswerInsteadOfOld]%>')) {
                window.location.href = "Topic.aspx?action=mark&postId=" + postId + "&topicId=<%=TopicId%>&siteId=<%=SiteId%>&forumId=<%=ForumId%>&goToAnswer=true&a=1#Post" + postId;
            }
            else
                return;
        }

        function quote(postId) {
            //        var quoteFomat='<table border="0" cellpadding="0" cellspacing="0">' + 
            //                            '<tr>' + 
            //                                '<td class="advance_left_top">' + 
            //                                '</td>' + 
            //                                '<td class="advance_top" width="100%">' + 
            //                                '</td>' + 
            //                                '<td class="advance_right_top">'+ 
            //                                '</td>' + 
            //                            '</tr>'
            //                            '<tr>' + 
            //                                '<td class="advance_left">' + 
            //                                    '&nbsp;' + 
            //                                '</td>' + 
            //                                '<td class="advance_content advance_quote">';
            //            var quoteEndTag =   '</td>' + 
            //                                '<td class="advance_right">' + 
            //                                    '&nbsp;' +
            //                                '</td>' + 
            //                            '</tr>' + 
            //                            '<tr>' + 
            //                                '<td class="advance_left_bot">' + 
            //                                '</td>' + 
            //                                '<td class="advance_bot" width="100%">' + 
            //                                '</td>' + 
            //                                '<td class="advance_right_bot">' + 
            //                                '</td>' + 
            //                            '</tr>' + 
            //                        '</table>';

            var quoteFomat = "<blockquote><div>";

            var quoteAuthor = '<%=Proxy[EnumText.enumForum_Topic_LabelQuoteDeletedAuthor]%>';
            if (document.getElementById("author" + postId) != null) {
                quoteAuthor = document.getElementById("author" + postId).innerHTML;
            }
            var quotePostTime = document.getElementById("postTime" + postId).innerHTML;

            var quoteIndex = "<cite>" + '<%=Proxy[EnumText.enumForum_Topic_FieldQuote]%>' + quoteAuthor + " " + quotePostTime + "</cite>";
            var quoteContent = document.getElementById("postContent" + postId).innerHTML;
            var quoteEndTag = "</div></blockquote><p></p>";

            var quoteStr = quoteFomat + quoteIndex + quoteContent + quoteEndTag;
            //alert(quoteStr);

            //var originalContent = tinyMCE.get("ctl00_ContentPlaceHolder1_HTMLEditor_editor").getContent();
            if('<%=IfReplay_ContentEditorDisplay()%>'.toLowerCase() == "true")
            {
                tinyMCE.get("ctl00_ContentPlaceHolder1_HTMLEditor_editor").setContent(quoteStr);//originalContent + quoteStr);
                window.scrollTo(0, document.body.scrollHeight);
                window.setTimeout(function() { tinyMCE.get("ctl00_ContentPlaceHolder1_HTMLEditor_editor").focus(); }, 0);
                goToTheEnd(tinyMCE.get("ctl00_ContentPlaceHolder1_HTMLEditor_editor"));
            }
        }

        function deleteTopicConfirm() {
       
            if(confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmDeleteTopic]%>') == false)
                return false;
            if('<%=IfAnnoucement%>'.toLowerCase() == 'true')
            {
                if(confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmDeleteAnnoucement]%>') == false)
                    return false;
            }
            return true;
                
        }

        function closeTopicConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmCloseTopic]%>');
        }

        function reopenTopicConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmReopenTopic]%>');
        }
        
        function moveTopic()
        {
            document.getElementById('frMoveTopic').src ="SelectForum.aspx?topicId=<%=TopicId%>&siteId=<%=SiteId%>&forumId=<%=ForumId%>";
        }

        function stickyTopicConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmStickyTopic]%>');
        }

        function unStickyTopicConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmUnStickyTopic]%>');
        }
        
        function featuredTopicConfirm(){
            return confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmFeaturedThisTopic]%>');
        }
        
        function unFeaturedTopicConfirm(){
            return confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmUnfeaturedThisTopic]%>');
        }

        function SubmitPoll(postId) {
            var hd = document.getElementById("hdPollId");
            if (hd.value != "") {
                window.location.href = "Topic.aspx?action=poll&optionId=" + hd.value +
                                       "&topicId=<%=TopicId%>&siteId=<%=SiteId%>&forumId=<%=ForumId%>";
            }
            else {
                alert('<%=Proxy[EnumText.enumForum_Topic_ErrorYouShouldChooseOnePoll]%>');
            }
        }

        function ChoosePollId(pollId, ifMuli, ifChecked, item) {
            var hd = document.getElementById("hdPollId");
            if(ifMuli && ifChecked)
            {
                var MaxCount = <%=PollMulitipleChoiceCount%>;
                if (GetChoosedPollCount() >= MaxCount) {
                    item.checked = false;
                    alert('<%=Proxy[EnumText.enumForum_Topic_ErrorMulitipleChoiceIs]%>' + MaxCount);
                    return;
                }
            }
            var arry = hd.value.split(';');
            if (ifMuli) {
                if (!ifChecked) {
                    //alert(pollId);
                    var tvalue = "";
                    var arry = hd.value.split(';');
                    for (var i = 0; i < arry.length; i++) {
                        if (arry[i] != pollId && arry[i] != "")
                            tvalue += arry[i] + ";";
                    }
                    hd.value = tvalue;

                }
                else {
                    hd.value += pollId + ";";
                }
            }
            else {
                hd.value = pollId;
            }
        }
        function GetChoosedPollCount() {
            var hd = document.getElementById("hdPollId");
            var count = 0;
            var arry = hd.value.split(';');
            for (var i = 0; i < arry.length; i++) {
                if (arry[i] != "")
                    count++;
            }
            return count;
        }
        
        function TextKeyDown(txtId) {
            //            var text = document.getElementById(txtId);
            //   text.value = text.value.replace(/\D/g,'');
        }
        
        function UnloginQuickReply() {
            //debugger;
            
            var objbtn = document.getElementById('<%=btnQuickReplyHidden.ClientID %>');
            if (objbtn != null)
                objbtn.click();
            
        }
        function IfShowDropDownList(ifShow)
        {
            if(navigator.userAgent.indexOf("MSIE") <=0)
                return;
            if(navigator.userAgent.indexOf("MSIE 6.0")>0)// ie 6. display footer 
            {
                var ddlForumJump = document.getElementById("ctl00_ForumFooter1_ddlForumJump");
                
                if(ifShow=='true')
                    ddlForumJump.style.display='';
                else
                    ddlForumJump.style.display='none';
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ThickBox" runat="server">

    <div id="divThickOuter" class="thick_outer">
    </div>
    <div id="divEmpty">
    </div>
    <%--Move Topic--%>
    <div id="divMoveTopic" style="height: 500px; width: 490px; display: none">
        <table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'>
            <tr>
                <td class='top-left'></td>
                <td class='boxy-top'></td>
                <td class='top-right'></td>
            </tr>
            <tr>
                <td class='left'></td>
                <td class='boxy-inner'>
                    <div class="title-bar">
                        <span class="spanHover" onclick="WindowClose('divMoveTopic','divThickOuter');IfShowDropDownList('true');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=Proxy[EnumText.enumForum_Topic_TitleMoveTopic]%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<iframe width="450px" id="frMoveTopic" height="380px" frameborder='0' scrolling="yes"
                            src="#"></iframe>--%>
                    </div>
                </td>
                <td class='right'></td>
            </tr>
            <tr>
                <td class='bottom-left'></td>
                <td class='bottom'></td>
                <td class='bottom-right'></td>
            </tr>
        </table>
    </div>
    <div id="divThickInner" style="display: none; height: 360px; width: 700px;">
        <table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'>
            <tr>
                <td class='top-left'></td>
                <td class='boxy-top'></td>
                <td class='top-right'></td>
            </tr>
            <tr>
                <td class='left'></td>
                <td class='boxy-inner'>
                    <div class="title-bar">
                        <span class="spanHover" onclick="javascript:WindowClose('divThickInner','divThickOuter');IfShowDropDownList('true');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=Proxy[EnumText.enumForum_Topic_TitleQuickReply]%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="tb_forum" cellspacing='0'>
                            <tr>
                                <td class="row1" width="20%" align="right">
                                    <p>
                                        <b>
                                            <%=Proxy[EnumText.enumForum_Topic_FieldSubject]%></b>
                                    </p>
                                </td>
                                <td class="row2" width="80%">
                                    <p>
                                        <asp:TextBox ID="txtSubject1" runat="server" Width="69%" CssClass="txt"></asp:TextBox>
                                        <span class="require">*
                                            <asp:RequiredFieldValidator ID="RequiredTxtSubject1" runat="server" ControlToValidate="txtSubject1"
                                                Display="Dynamic" ValidationGroup="swiftReply"></asp:RequiredFieldValidator>
                                        </span>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="row1" align="right">
                                    <p>
                                        <b>
                                            <%=Proxy[EnumText.enumForum_Topic_FieldContent]%></b>
                                    </p>
                                </td>
                                <td class="row2">
                                    <p>
                                        <div style="width: 70%">
                                            <asp:TextBox ID="txtContent" runat="server" Width="100%" TextMode="MultiLine" Height='200'></asp:TextBox>
                                        </div>
                                        <asp:Label ID="draftEditInfo2" runat="server"></asp:Label>
                                        <span class="require">*<%=Proxy[EnumText.enumForum_Public_RequiredField]%></span>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="row5" align="center">
                                    <p>
                                        <asp:Button ID="btnSwiftReplySubmit" runat="server" OnClick="btnSwiftReplySubmit_Click"
                                            OnClientClick="javascript:if (Page_ClientValidate('swiftReply')) {return checkRepeatSubmit(this);}"
                                            ValidationGroup="swiftReply" CausesValidation="false" CssClass="btn" />
                                    </p>
                                    <%--    <asp:Button ID="btnSwiftReplySaveDraft" runat="server" OnClick="btnSwiftSaveDraft_Click"
                            ValidationGroup="swiftReply" CssClass="btn" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td class='right'></td>
            </tr>
            <tr>
                <td class='bottom-left'></td>
                <td class='bottom'></td>
                <td class='bottom-right'></td>
            </tr>
        </table>
    </div>
    <%-- Vote Result--%>
    <div id="divVoteResult" style="display: none; height: 250px; width: 550px;">
        <table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'>
            <tr>
                <td class='top-left'></td>
                <td class='boxy-top'></td>
                <td class='top-right'></td>
            </tr>
            <tr>
                <td class='left'></td>
                <td class='boxy-inner'>
                    <div class="title-bar">
                        <span class="spanHover" onclick="javascript:WindowClose('divVoteResult','divThickOuter');IfShowDropDownList('true');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=TopicSubject%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="pos_bottom_10">
                            <table class="tb_forum" cellspacing='0' width="100%">
                                <tr>
                                    <th>
                                        <%=Proxy[EnumText.enumForum_Topic_SubTitleOption]%>
                                    </th>
                                    <th style="width: 200px">
                                        <%=Proxy[EnumText.enumForum_Topic_FiledPecent]%>
                                    </th>
                                    <th width="60px">
                                        <%=Proxy[EnumText.enumForum_Topic_FiledPer]%>
                                    </th>
                                </tr>
                                <asp:Repeater runat="server" ID="rptPollsResult">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="row1">
                                                <p>
                                                    <%#System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("OptionText").ToString()))%>
                                                </p>
                                            </td>
                                            <td class="row1">
                                                <div class="poll_result1">
                                                    <span class="iblock"><span class="iblock" style="width: <%#AllVotesResults==0? 0: Convert.ToInt32(Eval("Votes"))*100/this.AllVotesResults%>%;">&nbsp; </span>
                                                </div>
                                                <div class="clear">
                                                </div>
                                                </span>
                                            </td>
                                            <td class="row2">
                                                <center>
                                                    <p>
                                                        <%#AllVotesResults == 0 ? "0" : (Convert.ToDouble(Eval("Votes")) * 100 / this.AllVotesResults).ToString("F2")%>%</p>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td class="row5" align="right" colspan="3">
                                        <p>
                                            <b>
                                                <%=Proxy[EnumText.enumForum_Topic_SubTitleTotalNumJoinTheVote]%></b> <span class="totleNum">
                                                    <%=AllVotesHistories%></span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            <div class="divCenter pos_top_10">
                                <asp:Button ID="btnClose" runat="server" OnClientClick="javascript:WindowClose('divVoteResult','divThickOuter');IfShowDropDownList('true');return false;"
                                    CssClass="btn" />
                            </div>
                        </div>
                    </div>
                </td>
                <td class='right'></td>
            </tr>
            <tr>
                <td class='bottom-left'></td>
                <td class='bottom'></td>
                <td class='bottom-right'></td>
            </tr>
        </table>
    </div>
    <%--Pay Score--%>
    <div id="divPaySroce" style="display: none; height: 130px; width: 650px;">
        <table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'>
            <tr>
                <td class='top-left'></td>
                <td class='boxy-top'></td>
                <td class='top-right'></td>
            </tr>
            <tr>
                <td class='left'></td>
                <td class='boxy-inner'>
                    <div class="title-bar">
                        <span class="spanHover" onclick="javascript:WindowClose('divPaySroce','divThickOuter');IfShowDropDownList('true');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=Proxy[EnumText.enumForum_PayScore_TitlePayScore]%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<iframe id='iframePayScore' width="610px" height="130px" frameborder='0' scrolling="no">
                        </iframe>--%>
                    </div>
                </td>
                <td class='right'></td>
            </tr>
            <tr>
                <td class='bottom-left'></td>
                <td class='bottom'></td>
                <td class='bottom-right'></td>
            </tr>
        </table>

        <%--<script type="text/javascript" language="javascript">
            function GetAttachId(id) {
                var ifGuest = '<%=IfGuest %>';
                if (ifGuest.toLowerCase() == 'true') {
                    window.location = 'login.aspx?siteId=<%=SiteId%>';
                    return false;
                }
                showWindow('divPaySroce', 'divThickOuter');
                IfShowDropDownList('false');
                document.getElementById('iframePayScore').src =
                    "PayScore.aspx?attachId=" + id + "&topicId=<%=TopicId%>&forumId=<%=ForumId%>"
                    + "&siteId=" + "<%=SiteId%>" + "&time=" + new Date().getMilliseconds();
            }
        </script>--%>
    </div>
    <%--Abuse--%>
    <div id="divAbuse" style="display: none; height: 350px; width: 650px;">
        <table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'>
            <tr>
                <td class='top-left'></td>
                <td class='boxy-top'></td>
                <td class='top-right'></td>
            </tr>
            <tr>
                <td class='left'></td>
                <td class='boxy-inner'>
                    <div class="title-bar">
                        <span class="spanHover" onclick="javascript:WindowClose('divAbuse','divThickOuter');IfShowDropDownList('true');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=Proxy[EnumText.enumForum_AbusePost_TitleAbuse] %>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- <iframe id='iframeAbusePost' width="610px" height="220px" scrolling="no" frameborder='0'>
                        </iframe>--%>
                    </div>
                </td>
                <td class='right'></td>
            </tr>
            <tr>
                <td class='bottom-left'></td>
                <td class='bottom'></td>
                <td class='bottom-right'></td>
            </tr>
        </table>

        <%--<script language="javascript" type="text/javascript">
            function GetAbuseId(id) {
                document.getElementById('iframeAbusePost').src =
                    "AbusePost.aspx?postId=" + id + "&topicId=" + "<%=TopicId%>"
                    + "&forumId=" + "<%=ForumId%>"
                    + "&siteId=" + "<%=SiteId%>";
            }
            function IfGoToLogin(ifGuest, postId) {
                if (ifGuest.toLowerCase() == 'true') {
                    window.location = 'login.aspx?siteId=' + "<%=SiteId%>";
                    return false;
                }
                else {
                    showWindow('divAbuse', 'divThickOuter');
                    IfShowDropDownList('false');
                    GetAbuseId(postId);
                }
            }
        </script>--%>
    </div>
    <%-- Send Message--%>
    <div id="divSendMessageToUser" style="display: none; width: 650px; height: 320px;">
        <table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'>
            <tr>
                <td class='top-left'></td>
                <td class='boxy-top'></td>
                <td class='top-right'></td>
            </tr>
            <tr>
                <td class='left'></td>
                <td class='boxy-inner'>
                    <div class="title-bar">
                        <span class="spanHover" onclick="javascript:WindowClose('divSendMessageToUser','divThickOuter');IfShowDropDownList('true');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=Proxy[EnumText.enumForum_Topic_TitleSendMessage]%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<iframe id='iframeSendMessageToUser' width="610px" height="310px" frameborder='0'
                            src="#" scrolling="no"></iframe>--%>
                    </div>
                </td>
                <td class='right'></td>
            </tr>
            <tr>
                <td class='bottom-left'></td>
                <td class='bottom'></td>
                <td class='bottom-right'></td>
            </tr>
        </table>

        <%--<script language="javascript" type="text/javascript">
            function SetSendMessageToUserId(userId) {
                var iframeSendMessageToUser = document.getElementById('iframeSendMessageToUser');
                iframeSendMessageToUser.src = "SendMessages.aspx?userId=" + userId + "&siteId=" + "<%=SiteId%>";
            }
        </script>--%>
    </div>
    <%--Ban User--%>
    <div id="divBanOneUser" style="display: none; width: 650px; height: 300px;">
        <table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'>
            <tr>
                <td class='top-left'></td>
                <td class='boxy-top'></td>
                <td class='top-right'></td>
            </tr>
            <tr>
                <td class='left'></td>
                <td class='boxy-inner'>
                    <div class="title-bar">
                        <span class="spanHover" onclick="javascript:WindowClose('divBanOneUser','divThickOuter');IfShowDropDownList('true');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=Proxy[EnumText.enumForum_BanUser_TitleBanUser]%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<iframe id='iframeBanOneUser' width="610px" height="280px" frameborder='0' scrolling="no"
                            src="#"></iframe>--%>
                    </div>
                </td>
                <td class='right'></td>
            </tr>
            <tr>
                <td class='bottom-left'></td>
                <td class='bottom'></td>
                <td class='bottom-right'></td>
            </tr>
        </table>

        <%--<script language="javascript" type="text/javascript">
            function SetBanUserIframeWithUserId(userId) {
                var iframeBanOneUser = document.getElementById('iframeBanOneUser');
                iframeBanOneUser.src = "BanUser.aspx?userId=" + userId + "&forumId=" + "<%=ForumId%>" + "&siteId=" + "<%=SiteId%>";
            }
        </script>--%>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/inner/style.css" rel="Stylesheet" type="text/css" />

    <!DOCTYPE HTML>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Bolt.Energy</title>

    </head>

    <body>

        <div class="bolt_new_topic">
            <div class="contant-area">
                <h2>
                    <asp:Image ID="imgStatus" runat="server" Visible="false" Style="vertical-align: bottom"
                        EnableViewState="false" />
                    <asp:Label ID="lblTopicTitle" runat="server" EnableViewState="false"></asp:Label>

                </h2>
                <div>
                    <div class="bolt_admin" style="display: none;">
                        <%=Proxy[EnumText.enumForum_Topic_FieldModerators]%>
                        <asp:Repeater ID="RepeaterModerator" runat="server" OnItemDataBound="RepeaterModerator_ItemDataBound"
                            EnableViewState="false">
                            <ItemTemplate>
                                <a class="user_link">
                                    <%#System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("DisplayName").ToString()))%>
                                </a>
                                <asp:Literal ID="Literal1" runat="server" EnableViewState="false"></asp:Literal>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <%--<div class="bolt_date">Jan 17</div>--%>
                    <div class="topic-body">
                        <div>
                            <%--<img src="images/icon_permote.jpg">--%>
                            <asp:Button ID="Promotebtn" runat="server" OnClick="Promotebtn_Click" ToolTip="Promote topic" Class="btnstyle" />
                        </div>
                        <div class="clear"></div>

                        <asp:Repeater ID="RepeaterTopic" runat="server" OnItemDataBound="RepeaterTopic_ItemDataBound"
                            EnableViewState="false" OnItemCommand="RepeaterTopic_ItemCommand">
                            <ItemTemplate>
                                <div class="row">
                                    <%--First Infor--%>
                                    <tr>
                                        <td class='<%#Convert.ToBoolean(Eval("IfAnswer"))? "post_top1 answer_post_top1":"post_top1"%>'
                                            align="center">
                                            <div id="dvinitial" runat="server">
                                                <%#"<a class='user_link'>"
                                                                                             + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString().Substring(0,1).ToUpper())) + "</a>"%>
                                            </div>
                                            <div class="bolt_admin">
                                                <asp:PlaceHolder ID="placeHolderLastPost" runat="server" EnableViewState="false"></asp:PlaceHolder>

                                                <%#Convert.ToBoolean(Eval("IfPostUserOrOperatorDeleted")) ? System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString())) : "<a class='bolt_admin'  id=\"author" + Eval("PostId") + "\" target=\"_blank\">"
                                                                                                + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Eval("PostUserOrOperatorName").ToString()))) + "</a>"%>
                                            </div>
                                            <div class="bolt_date">

                                                <%#"<span class=\"timeSpan\" id=\"postTime" + Eval("PostId") + "\">" + Com.Comm100.Framework.Common.DateTimeHelper.DateFormate(Convert.ToDateTime(Eval("PostTime"))) + "</span>"%>
                                            </div>
                                        </td>
                                        <td class='<%#Convert.ToBoolean(Eval("IfAnswer"))? "post_top2 answer_post_top2":"post_top2"%>'></td>
                                    </tr>

                                    <%--Sec Infor--%>
                                    <tr>
                                        <td class='<%#Convert.ToBoolean(Eval("IfAnswer"))? "post_middle1 answer_post_middle1":"post_middle1"%>'
                                            valign="top"></td>
                                        <td class='<%#Convert.ToBoolean(Eval("IfAnswer"))? "post_middle2 answer_post_middle2":"post_middle2"%>'
                                            valign="top">
                                            <div style="display: inline">

                                                <div style="width: calc(100% - 43px); float: right;">
                                                    <asp:PlaceHolder ID="PHContent" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                    <asp:PlaceHolder ID="PHUserNeverPayTopic" runat="server" EnableViewState="false" />
                                                    <asp:PlaceHolder ID="PHUserNeverReplytopic" runat="server" EnableViewState="false" />
                                                    <asp:PlaceHolder ID="PHReplyInfor" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                    <%--<br />--%>
                                                    <%--Poll Infor--%>
                                                    <asp:Panel ID="divVoteInfo" runat="server" Visible="false">
                                                        <div class="poll">
                                                            <div class="poll_title">
                                                                <%-- <p>--%>
                                                                <%-- <img src='<%=ImagePath + "/poll.gif"%>' />--%>
                                                                <asp:PlaceHolder ID="PHPollDateToInfor" runat="server" EnableViewState="false"><b>
                                                                    <%=Proxy[EnumText.enumForum_Topic_FiledPollDateTo]%>
                                                                </b>&nbsp;
                                                            <asp:Label ID="lblPollDateTo" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; </asp:PlaceHolder>
                                                                <b>
                                                                    <%=Proxy[EnumText.enumForum_Topic_FiledMulitipleChoice]%>
                                                                </b>&nbsp;<asp:Label ID="lblMulitipleChoice" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <b>
                                                            <%=Proxy[EnumText.enumForum_Topic_SubTitleTotalNumJoinTheVote]%>
                                                        </b>&nbsp;<asp:Label ID="lbTotalVoteNum" runat="server" />
                                                                <%--</p>--%>
                                                            </div>
                                                            <asp:Repeater ID="rptPollOptions" runat="server" EnableViewState="false" OnItemDataBound="rptPollOptions_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <div class="poll_content">
                                                                        <div class="poll_option">
                                                                            <asp:PlaceHolder runat="server" ID="divChooseOption">
                                                                                <%#IfPollMulitipleChoice ? "<input type='checkbox' name='Polls' onclick='ChoosePollId(" + Eval("Id") + ",true,this.checked,this)'/>"
                                                        : "<input type='radio' name='Polls' onclick='ChoosePollId(" + Eval("Id") + ",false,true,this)'>"%>
                                                                            </asp:PlaceHolder>
                                                                            <%#System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("OptionText").ToString()))%>
                                                                        </div>
                                                                        <div class="poll_result" runat="server" id="pollVoteResult">
                                                                            <span class="iblock"><span class="iblock" style="width: <%#AllVotesResults==0? 0: Convert.ToInt32(Eval("Votes"))*100/this.AllVotesResults%>px;"></span></span>&nbsp;
                                                                <%#AllVotesResults==0? "0": (Convert.ToDouble(Eval("Votes"))*100/this.AllVotesResults).ToString("F2")%>%
                                                                        </div>
                                                                        <div class="clear">
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <%--  <br />--%>
                                                            <%-- <p>--%>
                                                            <input type="button" class="btn" runat="server" id="btnVote" value="Submit vote"
                                                                onclick="SubmitPoll();" />
                                                            <input type="button" class="btn" value="View results" runat="server" id="btnView"
                                                                onclick="javascript:showWindow('divVoteResult','divThickOuter');IfShowDropDownList('false');" />
                                                            <input id="hdPollId" type="hidden" value="" />
                                                            <%--</p>--%>
                                                        </div>
                                                        <%--<br />--%>
                                                    </asp:Panel>
                                                    <%--Attachments List--%>
                                                    <asp:Panel class="divAttachment" runat="server" ID="divAttachmentList" Visible="false">
                                                        <fieldset class="attachment">
                                                            <legend><b>
                                                                <%=Proxy[EnumText.enumForum_Topic_FiledAttachment]%></b></legend>
                                                            <div class="attachment_inner">
                                                                <asp:Repeater runat="server" ID="rptAttachments" EnableViewState="false" OnItemDataBound="rptAttachments_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <%-- <p>--%>
                                                                        <%--<img src='<%= this.ImagePath + "/icon_topic_attach.gif" %>' alt="Attachment" />--%>
                                                                        <asp:PlaceHolder ID="PHAttachmentItem" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                                        <%--</p>--%>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                            <br />
                                                        </fieldset>
                                                        <br />
                                                    </asp:Panel>
                                                    <asp:PlaceHolder ID="placeHolderSignature" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                    <%--This post was edited removed--%>
                                                    <asp:PlaceHolder ID="placeHolderLastEdit" runat="server" EnableViewState="false" Visible="false"></asp:PlaceHolder>
                                                </div>

                                            </div>
                                        </td>
                                    </tr>
                                    <%--Tool Bar--%>
                                    <tr>
                                        <td class='<%#Convert.ToBoolean(Eval("IfAnswer"))? "post_bottom1 answer_post_bottom1":"post_bottom1"%>'>
                                            <%--Top Removed-- %>
                            <%-- <div class="top">
                                <a href="#">
                                    <%=Proxy[EnumText.enumForum_Public_TextTop]%></a>
                           </div>--%>
                                            <%--Numbering Removed-- %>
                            <%--<div class="layer">
                                <%#Eval("Layer")%><%=Proxy[EnumText.enumForum_Public_TextSharp]%>
                            </div>--%>
                                            <div class="clear">
                                            </div>
                                        </td>
                                        <td class='<%#Convert.ToBoolean(Eval("IfAnswer"))? "post_bottom2 answer_post_bottom2":"post_bottom2"%>'>
                                            <asp:Literal ID="ltranswermarked" runat="server"></asp:Literal>
                                            <div class="buttons" style="float: right; display: block;">
                                                <asp:Panel ID="divForumToolBar" runat="server" CssClass="forumToolBar">
                                                    <ul class="buttons_menu">
                                                        <asp:PlaceHolder ID="PHApproveAbuseButton" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="PHRefuseAbuseButton" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="PHApprovalModerationButton" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="PHUnApprovalModerationButton" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        <!--Updated on 2/3/2017 by techtier singh for Hide the abuse tab in post----->
                                                        <asp:PlaceHolder ID="PHAbuseButton" runat="server" EnableViewState="false" Visible="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="PHCloseButton" runat="server" EnableViewState="false" />
                                                        <asp:PlaceHolder ID="PHReOpenButton" runat="server" EnableViewState="false" />
                                                        <asp:PlaceHolder ID="placeHolderEdit" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="placeHolderDelete" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="placeHolderMarkAsAnswer" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="placeHolderUnmark" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        

                                                        <asp:PlaceHolder ID="placeHolderQuote" runat="server" EnableViewState="false" Visible="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="placeHolderQuickReply" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="PHResotre" runat="server" EnableViewState="false" Visible="false"></asp:PlaceHolder>
                                                        <asp:PlaceHolder ID="PHDeletePermanently" runat="server" EnableViewState="false"
                                                            Visible="false"></asp:PlaceHolder>
                                                    </ul>
                                                </asp:Panel>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </td>
                                    </tr>

                                    <%if (Count < 1)
                                      {
                                          Count = Count + 1;
                                    %>

                                    <div class="topic-map">
                                        <section class="map map-collapsed">
                                            <ul class="clearfix">
                                                <li>
                                                    <h4>created</h4>
                                                    <div class="<%#("topic-avatar-small bolt-"+ Com.Comm100.Forum.UI.Common.WebUtility.GetInitialClass(Convert.ToString(Eval("PostUserOrOperatorName"))))%>">
                                                        <a href="javascript:;" data-user-card="rich">
                                                            <%#System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString().Substring(0,1).ToUpper()))%> 
                                                        </a>
                                                    </div>
                                                    <span title="<%=fullPostTime%>" data-time="1360011041693" data-format="tiny" class="relative-date">
                                                        <%#Com.Comm100.Framework.Common.DateTimeHelper.DateFormate(Convert.ToDateTime(Eval("PostTime")))%></span></li>
                                                <li>
                                                    <h4>last reply</h4>
                                                    <div class="<%=("topic-avatar-small bolt-"+ Com.Comm100.Forum.UI.Common.WebUtility.GetInitialClass(LastPostedUserName))%>">
                                                        <a href="javascript:;" data-user-card="rich"><%=LastPostedUserName %> </a>
                                                    </div>
                                                    <span title="<%=fullDateTime %>" data-time="1484646803403" data-format="tiny" class="relative-date"><%=LastPostTime%></span></li>
                                                <li><span class="number"><%=Replies %></span>
                                                    <h4>replies</h4>
                                                </li>
                                                <li class="secondary"><span class="number"><%=Views%></span>
                                                    <h4>views</h4>
                                                </li>
                                                <li class="secondary"><span class="number"><%=Tpromotion%></span>
                                                    <h4>promotions</h4>
                                                </li>
                                            </ul>
                                        </section>
                                    </div>
                                    <div class="bolt_add_comments">
                                        <div class="bolt_comments_counter">
                                            <h5><%=recCount-1%> Comments<h5>
                                                <h6>Post your opinion</h6>
                                        </div>
                                        <div class="bolt_post_comments">
                                            <textarea id="HTMLEditor" name="HTMLEditor" cols="" rows=""></textarea>
                                        </div>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Reply" OnClick="btnSubmit_Click" OnClientClick="javascript:if (Page_ClientValidate('reply')) {return checkRepeatSubmit(this);}"
                                            class="postratingButton" ValidationGroup="reply" CausesValidation="false" />
                                    </div>
                                    <%}%>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="pos_bottom_5" style="display: none;">
                            <div class="toolbar">

                                <% if (aspnetPager.Visible)
                                   {  %>
                                <div class="pager">
                                    <cc1:ASPNetPager ID="aspnetPager" runat="server" OnPaging="aspnetPager_Paging" EnableViewState="True"
                                        Mode="LinkButton" PageSize="50" ItemsName="posts" ItemName="post">
                                    </cc1:ASPNetPager>
                                </div>
                                <%} %>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </body>
    </html>

    <div class="pos_bottom_5" style="display: none;">
        <div class="toolbar">
            <div class="buttons">
                <ul class="buttons_menu">
                    <li><a class="new_topic_link" id="hyperLnkNewTopic" runat="server" enableviewstate="false">
                        <span class="new_topic">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_NewTopic]%></span>
                        <li class="li_end"></li>
                    </a></li>
                    <li class="next"><a class="post_reply_link" id="lnkBtnLoggedinReply" runat="server"
                        href="#" onclick="javascript:window.location='#aReplySubject';return false;"
                        visible="false"><span class="post_reply">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_PostReply]%></span>
                        <li class="li_end"></li>
                    </a></li>
                    <!--Promoted-->
                    <li class="next"><a class="post_reply_link" id="lnkBtnUnLoggedinReply" runat="server"
                        onserverclick="lnkBtnUnLoggedinReply_Click" visible="false"><span class="post_reply">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_PostReply]%></span>
                        <li class="li_end"></li>
                    </a></li>
                    <%--favorite--%>
                    <li class="next"><a class="favourite_link" id="imgBtnFavorite" runat="server" visible="false"
                        onserverclick="Favorite_Click" onclick="return FavoriteTopic();"><span class="favourite">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_Favourite]%></span>
                        <li class="li_end"></li>
                    </a></li>
                    <%--Unfavorite--%>
                    <li class="next"><a class="favourite_link" id="imgBtnUnFavorite" runat="server" visible="false"
                        onserverclick="UnFavorite_Click" onclick="return unFavoriteTopic();"><span class="favourite">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_UnFavourite]%></span>
                        <li class="li_end"></li>
                    </a></li>
                </ul>
            </div>
            <!---updated on 3/3/2017 by techtier for marked as feature ,move and sticky is hide in the tab-->
            <div class="buttons" style="float: right">
                <ul class="buttons_menu">
                    <li style="display: none;"><a class="featured_link" id="lnkBtnFeatured" runat="server" enableviewstate="false"
                        onserverclick="lnkBtnFeatured_Click" onclick="javascript:return featuredTopicConfirm();">
                        <span class="featured">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_Featured]%></span>
                        <li class="li_end" style="display: none;"></li>
                    </a></li>
                    <li><a class="featured_link" id="lnkBtnUnFeatured" runat="server" visible="false"
                        enableviewstate="false" onserverclick="lnkBtnUnFeatured_Click" onclick="javascript:return unFeaturedTopicConfirm();">
                        <span class="featured">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_UnFeatured]%></span>
                        <li class="li_end"></li>
                    </a></li>
                    <li class="next"><a class="delete_link" id="lnkBtnDelete" runat="server" visible="false"
                        enableviewstate="false" onserverclick="lnkBtnDelete_Click" onclick="javascript:return deleteTopicConfirm();">
                        <span class="delete">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_Delete]%></span>
                        <li class="li_end"></li>
                    </a></li>
                    <li class="next"><a class="close_link" id="lnkBtnClose" runat="server" visible="false"
                        onserverclick="lnkBtnClose_Click" onclick="javascript:return closeTopicConfirm();"
                        enableviewstate="false"><span class="close">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_Close]%></span>
                        <li class="li_end"></li>
                    </a></li>
                    <li class="next"><a class="close_link" id="lnkBtnReopen" runat="server" visible="false"
                        onserverclick="lnkBtnReopen_Click" onclick="javascript:return reopenTopicConfirm();"
                        enableviewstate="false"><span class="close">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_ReOpen]%></span>
                        <li class="li_end"></li>
                    </a></li>
                    <li class="next" style="display: none;"><a class="move_link" id="lnkBtnMove" runat="server" visible="false"
                        href="#" onclick="javascript:moveTopic();showWindow('divMoveTopic', 'divThickOuter');IfShowDropDownList('false');return false;"
                        enableviewstate="false"><span class="move">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_Move]%></span>
                        <li class="li_end" style="display: none;"></li>
                    </a></li>
                    <li class="next" style="display: none;"><a class="sticky_link" id="lnkBtnSticky" runat="server" visible="false"
                        onserverclick="lnkBtnSticky_Click" onclick="javascript:return stickyTopicConfirm();"
                        enableviewstate="false"><span class="sticky">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_Sticky]%></span>
                        <li class="li_end" style="display: none;"></li>
                    </a></li>
                    <li class="next"><a class="sticky_link" id="lnkBtnUnSticky" runat="server" visible="false"
                        onserverclick="lnkBtnUnSticky_Click" onclick="javascript:return unStickyTopicConfirm();"
                        enableviewstate="false"><span class="sticky">
                            <%=Proxy[EnumText.enumForum_Topic_Page_Button_UnSticky]%></span>
                        <li class="li_end"></li>
                    </a></li>
                </ul>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="divMsg" style="width: 50%">
        <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_5">
        <div class="toolbar1">
            <div class="go_to_answer">
                <asp:LinkButton ID="lnkBtnGoToAnswer" CssClass="answer" runat="server" Visible="false"
                    OnClick="lnkBtnGoToAnswer_Click" EnableViewState="false">
                    <%=Proxy[EnumText.enumForum_Topic_LinkGotoAnswer]%>
                </asp:LinkButton>
            </div>
            <% if (aspnetPagertop.Visible)
               {  %>
            <div class="pager">
                <cc1:ASPNetPager ID="aspnetPagertop" runat="server" OnPaging="aspnetPager_Paging"
                    EnableViewState="True" Mode="LinkButton" PageSize="10" ItemsName="" ItemName="">
                </cc1:ASPNetPager>
            </div>
            <%} %>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="pos_bottom_10" style="display: none;">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <asp:Label ID="lblTopicTitle2" runat="server" EnableViewState="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing="0" width="100%">
            <tr>
                <th width="22%">
                    <%=Proxy[EnumText.enumForum_Topic_ColumnAuthor] %>
                </th>
                <th>
                    <%=Proxy[EnumText.enumForum_Topic_ColumnMessage]%>
                </th>
            </tr>
            <!--Repeater code-->
            <!----------------->
        </table>
    </div>
    <div class="divMsg" style="width: 50%">
        <asp:Label ID="lblMessageAttachment" CssClass="errorMsg" runat="server" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblReplyError" CssClass="errorMsg" runat="server" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10" id="divpostreplay" style="display: none">
        <asp:Panel ID="panelReplyTable" runat="server" Visible="false" EnableViewState="true">
            <div class="cat2">
                <div class="top_cat2">
                    <div class="top_cat2_left">
                        <div class="top_cat2_right">
                            <%=Proxy[EnumText.enumForum_Public_TextPost]%>
                        </div>
                    </div>
                </div>
            </div>
            <table class="tb_forum" cellspacing='0' id="tableEdit" width="100%">
                <tr>
                    <td class="row1" width="20%" align="right">
                        <p>
                            <b><a name="aReplySubject"></a>
                                <%=Proxy[EnumText.enumForum_Topic_FieldSubject]%></b>
                        </p>
                    </td>
                    <td class="row2" width="80%" valign="middle">
                        <p>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="txt" Width="69%"></asp:TextBox>
                            <span class="require">* </span>
                            <asp:RequiredFieldValidator ID="RequiredTxtSubject" runat="server" ControlToValidate="txtSubject"
                                Display="Dynamic" ValidationGroup="reply"></asp:RequiredFieldValidator>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td class="row1" align="right">
                        <p>
                            <b>
                                <%=Proxy[EnumText.enumForum_Topic_FieldContent]%></b>
                        </p>
                    </td>
                    <td class="row2">
                        <p>
                            <div style="width: 70%">
                                <uc:HTMLEditor ID="HTMLEditor" runat="server" EnableViewState="true" />
                            </div>
                            <asp:Label ID="draftEditInfo" runat="server"></asp:Label>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </td>
                </tr>
                <tr id="trUploadAttachmentList" runat="server">
                    <td class="row1" align="right">
                        <p>
                            <b>
                                <%=Proxy[EnumText.enumForum_Topic_FiledUploadAttachment]%></b>
                        </p>
                    </td>
                    <td class="row2">
                        <table class="tb_attachment">
                            <asp:Repeater ID="rptPostAttachmentsList" runat="server" OnItemDataBound="rptPostAttachmentsList_ItemDataBound"
                                OnItemCommand="rptPostAttachmentsList_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hdAttachId" runat="server" />
                                            <%#Eval("Name")%>
                                            <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="App_Themes/StyleTemplate_Default/images/database_delete.gif"
                                                CommandName="Delete" CommandArgument='<%#Eval("Id")%>' ValidationGroup="reply" />
                                        </td>
                                        <td class="td_width2" align="left">
                                            <div>
                                                <!--chanegd by techtier-->
                                                <%=Proxy[EnumText.enumForum_Topic_TitleDownload]%>
                                                <asp:TextBox ID="tbScore" runat="server" CssClass="txt txt_width" />
                                                <span class="require">*</span>
                                                <%-- <asp:RegularExpressionValidator ID="revDownLoadScore" runat="server" Display="Dynamic"
                                            ValidationExpression="^\d*$" ValidationGroup="reply"><%=Proxy[EnumText.enumForum_Topic_ErrorPleaseInputOneNumber]%>
                                        </asp:RegularExpressionValidator>--%>
                                                <asp:CompareValidator ID="cvDownloadScore" runat="server" Type="Integer" ValueToCompare="0"
                                                    ControlToValidate="tbScore" Operator="GreaterThanEqual" ValidationGroup="reply"
                                                    Display="Dynamic" Visible="false">
                                            <%=Proxy[EnumText.enumForum_Topic_ErrorPleaseInputOneNumber]%>
                                                </asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="rfvDownloadScore" runat="server" Display="Dynamic"
                                                    Visible="false" ValidationGroup="reply"><%=Proxy[EnumText.enumForum_Topic_ErrorPayForDownloadIsRequired]%>
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <%=Proxy[EnumText.enumForum_Topic_ToolTipDescription]%>&nbsp;
                                            <asp:TextBox ID="tbDescription" runat="server" CssClass="txt txt_width2" />
                                            <div class="attachment_sep_row">
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div id="divAttachments">
                            <asp:FileUpload ID="file" runat="server" />
                            <asp:Button ID="btnUpload" CssClass="btn" runat="server" OnClick="btnUpload_Click"
                                ValidationGroup="reply" />
                            <div id="divUploadTempAttachmentList">
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="row1">
                        <p>
                            &nbsp;
                        </p>
                    </td>
                    <td class="row2">
                        <p>
                            <span class="require">*
                                <%=Proxy[EnumText.enumForum_Public_RequiredField]%>
                            </span>
                        </p>
                    </td>

                </tr>
                <tr>

                    <td colspan="2" class="row5" align="center">
                        <p>
                            <%--  <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="javascript:if (Page_ClientValidate('reply')) {return checkRepeatSubmit(this);}"
                                CssClass="btn" ValidationGroup="reply" CausesValidation="false" visible="false"/>--%>
                            <asp:Button ID="btnSaveDraft" runat="server" OnClick="btnSaveDraft_Click" CssClass="btn"
                                Style="margin-left: 32px;" ValidationGroup="reply" />
                        </p>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="pos_bottom_10" stye="display:none;">
        <%--LEGEND TABLE HIDED--%>
        <%-- <table class="tb_legend" cellspacing="0" width="100%">
            <tr>
                <td class="cat3" style="display:none;">
                    <%=Proxy[EnumText.enumForum_Public_TextLEGEND]%>
                </td>
            </tr>
            <tr style="display:none;" >
                <td class="row3">
                    <table cellpadding="1" cellspacing="1" width='100%'>
                        <tr>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="26" align="center">
                                            <img src='<%=this.ImagePath + "/status/32_32/close.gif" %>' alt='<%=Proxy[EnumText.enumForum_Topic_NoteClosedTopic]%>' />
                                        </td>
                                        <td>
                                            <%=Proxy[EnumText.enumForum_Topic_NoteClosedTopic] %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="26" align="center">
                                            <img src='<%=this.ImagePath + "/status/32_32/mark.gif"%>' alt='<%=Proxy[EnumText.enumForum_Topic_NoteMarkedTopic]%>' />
                                        </td>
                                        <td>
                                            <%=Proxy[EnumText.enumForum_Topic_NoteMarkedTopic]%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="26" align="center">
                                            <img src='<%=this.ImagePath + "/status/32_32/normal.gif" %>' alt='<%=Proxy[EnumText.enumForum_Topic_NoteNormalTopic]%>' />
                                        </td>
                                        <td>
                                            <%=Proxy[EnumText.enumForum_Topic_NoteNormalTopic]%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="26" align="center">
                                            <img src="<%=ImagePath %>/status/participate_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>'
                                                title='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>' align="absmiddle" />
                                        </td>
                                        <td>
                                            <%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
    </div>
    <input type="hidden" runat="server" id="currentAspNetPage" />
    <div style="display: none">
        <asp:Button ID="btnQuickReplyHidden" runat="server" Text="Button" OnClick="btnQuickReplyHidden_Click" />
    </div>

    <script runat="server">
        /*for tool bar if show when topic moved*/
        public string ToolBarStyle(bool ifAnswer)
        {
            string style = "style='";
            if (ifAnswer)
                style += "background-color:#d7e7ee;";
            if (this.IfMovedTopic)
                style += "display:none;";
            return style + "'";
        }
    </script>

    <script type="text/javascript">
        /*for ie8 'gotoanswer' bug*/
        var url = document.URL.toLowerCase();
        if (url.indexOf("gotopost=true") > -1 && url.indexOf("#bottom") < 0 && url.indexOf("#post") < 0) {
            var startIndex = url.indexOf("postid=") + 7;
            var endIndex = url.indexOf("&gotopost");
            var postId = url.substring(startIndex, endIndex);
            window.location.hash = "Post" + postId;
        }
    </script>

</asp:Content>
