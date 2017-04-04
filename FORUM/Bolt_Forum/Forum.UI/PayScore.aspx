<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayScore.aspx.cs" Inherits="Com.Comm100.Forum.UI.PayScore" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background: #fff">
    <form id="form1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="False"></asp:Label>
    </div>
    <div class="alertMsg" id="divpayAttachmsg">
        <%=Proxy[EnumText.enumForum_PayScore_ErrorCurrentUserHaveNotEnoughScore]%>
    </div>
    <table class="tb_forum" cellspacing='0' width="100%">
        <tr>
            <td class="row1" width="40%" align="right">
                <p>
                    <b>
                        <%=Proxy[EnumText.enumForum_PayScore_SubTitleNeedPayScore]%></b></p>
            </td>
            <td class="row2" width="60%">
                <p>
                    <input type="text" class="txt" id="tbAttachScore" readonly="readonly" />
                </p>
            </td>
        </tr>
        <tr>
            <td class="row1" width="40%" align="right">
                <p>
                    <b>
                        <%=Proxy[EnumText.enumForum_PayScore_SubTitleCurrentYourScore]%></b></p>
            </td>
            <td class="row2" width="60%">
                <p>
                    <input type="text" class="txt" id="tbUserScore" readonly="readonly" />
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="row5" align="center">
                <p>
                    <input id="hdPayAttachId" type="hidden" value="-1" />
                    <asp:Button ID="btnPay" runat="server" CssClass="btn" OnClick="btnPay_Click" />

                    <script type="text/javascript">
                        function ShowPostContent() {
                            closeWindow();
                            window.parent.location.href = "topic.aspx?topicId=" + '<%=TopicId%>' +
                                          "&forumId=" + '<%=ForumId%>' +
                                          "&siteId=" + '<%=SiteId%>&b=1';
                        }
                        function IfShowPayButton(aScore, uScore, aId) {
                            /*set hide Id*/
                            document.getElementById('hdPayAttachId').value = aId;
                            /*set score */
                            document.getElementById('tbAttachScore').value = aScore;
                            document.getElementById('tbUserScore').value = uScore;
                            /*if show pay button*/
                            var btnPay = document.getElementById('btnPay');
                            var divpayAttachmsg = document.getElementById('divpayAttachmsg');
                            aScore = parseInt(aScore);
                            uScore = parseInt(uScore);
                            if (aScore > uScore) {
                                btnPay.disabled = 'disabled';
                                divpayAttachmsg.style.display = "";
                            }
                            else {
                                btnPay.disabled = '';
                                divpayAttachmsg.style.display = "none";
                            }
                        }
                    </script>

                    <asp:Button ID="btnClose" runat="server" OnClientClick="javascript:closeWindow();return false;"
                        CssClass="btn" />
                </p>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function closeWindow() {
            window.parent.WindowClose('divPaySroce', 'divThickOuter');
        }

        IfShowPayButton('<%=AttachScore%>', '<%=UserScore%>', '<%=AttachId%>');
    </script>

    </form>
</body>
</html>
