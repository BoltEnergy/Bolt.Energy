<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditTopicOrPost.aspx.cs" Inherits="Forum.UI.EditTopic" ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Src="~/UserControl/HTMLEditor.ascx" TagName="HTMLEditor" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/NavigationBar.ascx" TagName="NavigationBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="JS/Common/RepeatSubmit.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="JS/DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript">
        function ShowPollCreation() {
            var ifCheck = document.getElementById("<%=chkPollCreation.ClientID%>").checked;
            if (ifCheck == true) {
                document.getElementById("<%=chkPollCreation.ClientID%>").checked = true;
                document.getElementById("divPollCreation").style.display = "";
            }
            else {
                document.getElementById("<%=chkPollCreation.ClientID%>").checked = false;
                document.getElementById("divPollCreation").style.display = "none";
            }
            var txtMulitiple = document.getElementById("<%=txtMulitiple.ClientID%>");
            if (txtMulitiple.value == "") {
                txtMulitiple.value = "1";
            }
        }

        function ShowPollDate() {
            var IfChecked = document.getElementById('<%=chkPollDate.ClientID%>').checked;
            var date = '<%=DateTime.Now.ToString("MM-dd-yyyy")%>';
            if (IfChecked)
                document.getElementById("tdPollDate").style.display = "";
            else
                document.getElementById("tdPollDate").style.display = "none";
            document.getElementById('<%=txtPollDate.ClientID%>').value = date;
        }

        function ChangeParentDiv(parent) {
            var parentId = parent.substring(parent.length, parent.length - 1);
            var file = "file" + parentId;
            var span = "spanUpload" + parentId;
            var parentSpan = document.getElementById(span);
            document.getElementById(file).style.display = "none";
            ShowSpanDelete(parentId);
        }

        function SetFileInfo(parent) {
            var parentId = parent.substring(parent.length, parent.length - 1);
            var spanInfo = "spanInfo";
            var file = "file";
            var info = document.getElementById(file + parentId).value;
            var filename = info.substring(info.lastIndexOf('\\') + 1, info.length);
            document.getElementById(spanInfo + parentId).innerHTML = filename;
        }

        function ShowSpanDelete(parentId) {
            var span = "spanDelete" + parentId;
            var parentSpanDelete = document.getElementById(span);
            parentSpanDelete.innerHTML = "<img src='Images/database_delete.gif' onclick='javascript:if(window.confirm(\"" + "<%=Proxy[EnumText.enumForum_EditTopicOrPost_ConfirmDeleteTheAttachment] %>" + "\")){Close(this);}' style='cursor:hand'/>";
        }

        function ShowSubUpload(parent) {
            var divUpload = parent.substring(parent.indexOf('divUpload'), parent.length - 1);
            var parentId = parent.substring(parent.length, parent.length - 1);
            var subId = parseInt(parentId) + 1;
            var subDiv = divUpload + subId;
            document.getElementById(subDiv).style.display = "";
        }

        function ChangeUpload(parent) {

            ShowSubUpload(parent);
            ChangeParentDiv(parent);
            SetFileInfo(parent);
        }

        function Close(obj) {
            obj.parentNode.parentNode.parentNode.style.display = "none";
        }

        function ChangeCheckBox(checkbox, textbox) {
            var ifCheck = document.getElementById(checkbox).checked;
            if (ifCheck == true) {
                document.getElementById(checkbox).checked = true;
                document.getElementById(textbox).disabled = false;

            }
            else {
                document.getElementById(checkbox).checked = false;
                document.getElementById(textbox).disabled = true;
            }
        }
        function checkTimeFormate() {
            var txtStartTime = document.getElementById("<%=txtPollDate.ClientID%>").value;
            var reg = /(^\s*$)|(^\s*(((0?2-29-(19|20)(([02486][048])|([13579][26]))))|((((0?[1-9])|([0-2]))-((0?[1-9])|(1\d)|(2[0-8])))|((((0?[13578])|(1[02]))-31)|(((0?[1,3-9])|(1[0-2]))-(29|30))))-((20[0-9][0-9])|(19[0-9][0-9])))\s*$)/

            if (reg.test(txtStartTime)) {
                return true;
            }
            else {
                return false;
            }
        }
        function TextKeyUp(txtId) {
            //            var text = document.getElementById(txtId);
            //            text.value = text.value.replace(/\D/g, '');
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2 hide">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <asp:Label ID="lblEdit" runat="server" class="TitleName" EnableViewState="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>



        <table class="tb_forum" cellspacing="0" width="100%" style="display:none;">
            <tr>
                <td class="row1">
                    <p>
                        &nbsp;
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <span class="require">*<%=Proxy[EnumText.enumForum_Public_RequiredField]%>
                        </span>
                    </p>
                </td>
            </tr>
            <%--Advanced--%>
            <tr runat="server" id="trAdvancedTitle">
                <th colspan='2' style="text-align: left">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_EditTopicOrPost_TitleAdvancedOptions]%></b>
                    </p>
                </th>
            </tr>
            <%--Post setting--%>
            <tr runat="server" id="trpostsetting">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_EditTopicOrPost_TitlePostSettings]%></b>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <input type="radio" runat="server" id="rdNormal" name="radioPost" onclick="setPayForView();"
                            disabled="disabled" /><%=Proxy[EnumText.enumForum_EditTopicOrPost_FiledNormal]%>&nbsp;
                    <input type="radio" runat="server" id="rdReply" name="radioPost" onclick="setPayForView();"
                        disabled="disabled" />
                        <%=Proxy[EnumText.enumForum_EditTopicOrPost_FiledNeedReplyToView]%>&nbsp;
                    <input type="radio" runat="server" name="radioPost" id="radioPay" onclick="setPayForView();"
                        disabled="disabled" />
                        <%=Proxy[EnumText.enumForum_EditTopicOrPost_FiledPayScoreToView]%>
                        <%-- <span id="Pay" style="<%=IfShowPayScoreText%>">
                    <%--<input type="text" runat="server" id="txtPay" class="txt" />
                    <%=Proxy[EnumText.enumForum_EditTopicOrPost_FiledHowManyScoreRequired]%>
                </span>--%>
                        <br />
                    </p>
                </td>
            </tr>
            <tr runat="server" id="trpollcreation">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <input id="chkPollCreation" type="checkbox" runat="server" value="Poll creation"
                                onclick="ShowPollCreation();" visible="false" /><%=Proxy[EnumText.enumForum_EditTopicOrPost_TitlePollCreation]%></b>
                    </p>
                </td>
                <td class="row2" style="text-align: left">
                    <p>
                        <b>
                            <div id="divPollCreation" style="<%=IfShowPollConection%>">
                                <fieldset>
                                    <legend>
                                        <%=Proxy[EnumText.enumForum_EditTopicOrPost_SubTitlePollOptions]%></legend>
                                    <center>
                                    <div><textarea id="tbPollOptions" disabled="disabled" runat="server" cols="67" rows="10"></textarea>
                                    </div>
                                </center>
                                </fieldset>
                                <br />
                                <fieldset>
                                    <legend>
                                        <%=Proxy[EnumText.enumForum_EditTopicOrPost_SubTitleOtherOptions]%></legend>
                                    <div>
                                        <table border="0" class="tb_inner" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td width="140" align="right">
                                                    <p>
                                                        <%=Proxy[EnumText.enumForum_EditTopicOrPost_FiledMulitipleChoice]%>
                                                        <br />
                                                    </p>
                                                </td>
                                                <td>
                                                    <p>
                                                        <asp:TextBox ID="txtMulitiple" CssClass="txt" Enabled="false" runat="server" Text="1" Style="width: 50px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtMulitiple" runat="server" ControlToValidate="txtMulitiple"
                                                            Display="Dynamic" ValidationGroup="save">
                                                <%=Proxy[EnumText.enumForum_EditTopicOrPost_ErrorMulitipleChoiceIsRequired]%></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtMulitiple" runat="server" ControlToValidate="txtMulitiple"
                                                            ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="save">
                                                <%=Proxy[EnumText.enumForum_EditTopicOrPost_ErrorPleaseInputOneNumber]%></asp:RegularExpressionValidator>
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <p>
                                                        <input type="checkbox" id="chkPollDate" runat="server" onclick="ShowPollDate();" />
                                                        <%=Proxy[EnumText.enumForum_EditTopicOrPost_FiledPollDateTo]%>
                                                        <br />
                                                    </p>
                                                </td>
                                                <td>&nbsp;
                                            <div id="tdPollDate">
                                                <asp:TextBox ID="txtPollDate" runat="server" Width="100px" CssClass="txt"> </asp:TextBox>
                                                <img onclick="WdatePicker({el:$dp.$('ctl00_ContentPlaceHolder1_txtPollDate')})" src="images/datePicker.gif"
                                                    align="absmiddle" style="cursor: pointer;" />
                                                &nbsp;
                                                    <asp:RequiredFieldValidator ID="rfvPollDate" runat="server" Display="Dynamic" ControlToValidate="txtPollDate"
                                                        ValidationGroup="save">
                                                    <%=Proxy[EnumText.enumForum_AddOrEditTopic_Page_PollDateIsRequired]%></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revDate" runat="server" ControlToValidate="txtPollDate" Display="Dynamic"
                                                    ValidationGroup="save" ValidationExpression="(^\s*$)|(^\s*(((0?2-29-(19|20)(([02486][048])|([13579][26]))))|((((0?[1-9])|(1[0-2]))-((0?[1-9])|(1\d)|(2[0-8])))|((((0?[13578])|(1[02]))-31)|(((0?[1,3-9])|(1[0-2]))-(29|30))))-((20[0-9][0-9])|(19[0-9][0-9])))\s*$)">
                                                    <%=Proxy[EnumText.enumForum_EditTopicOrPost_ErrorPollDateIsErrorFormat]%></asp:RegularExpressionValidator>
                                            </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </fieldset>
                                <br />
                            </div>
                        </b>
                    </p>
                </td>
            </tr>
            <tr id="trAttachment11" runat="server" style="border-right: solid 1px #aaa">
                <td class="row1" width="20%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_EditTopicOrPost_SubTitleUploadAttachment]%></b>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <table class="tb_attachment">
                            <asp:Repeater ID="rptAttachments1" runat="server" OnItemDataBound="rptAttachments_ItemDataBound"
                                OnItemCommand="rptAttachments_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <p>
                                                <asp:HiddenField ID="hdAttachId" runat="server" />
                                                <%#Eval("Name")%>
                                                <asp:ImageButton ID="imgDelete" runat="server" ImageUrl='<%# this.ImagePath + "/database_delete.gif" %>'
                                                    ToolTip="<%#Proxy[EnumText.enumForum_AddOrEditTopic_Page_ToolTipDelete]%>" CommandName="Delete"
                                                    CommandArgument='<%#Eval("Id")%>' />
                                            </p>
                                        </td>
                                        <td class="td_width2" align="left">
                                            <div style='<%#this.StyleIfScoreEnabled%>'>
                                                <%=Proxy[EnumText.enumForum_AddTopic_FiledDownLoadScore]%>&nbsp;
                                            <asp:TextBox ID="tbScore" runat="server" CssClass="txt txt_width" />
                                                <span class="require">*</span>
                                                <asp:RequiredFieldValidator ID="rfvDownloadScore" runat="server" Display="Dynamic"
                                                    ControlToValidate="tbScore" ValidationGroup="edit">
                                                <%=Proxy[EnumText.enumForum_AddTopic_ErrorPayForDownlaodIsRequired]%>
                                                </asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvDownloadScore" runat="server" Type="Integer" ValueToCompare="0"
                                                    ControlToValidate="tbScore" Operator="GreaterThanEqual" ValidationGroup="edit" Display="Dynamic">
                                                    <%=Proxy[EnumText.enumForum_AddTopic_ErrorPlesaeInputNumber]%>
                                                </asp:CompareValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="attachment_sep_row">
                                            <p>
                                                <%=Proxy[EnumText.enumForum_AddTopic_FiledDescription]%>&nbsp;
                                            <asp:TextBox ID="tbDescription" runat="server" CssClass="txt txt_width2" />
                                            </p>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div id="divAttachments1" class="pos_top_10 pos_bottom_10">
                            <asp:FileUpload ID="file1" runat="server" />
                            <asp:Button ID="btnUpload1" CssClass="btn" ValidationGroup="edit" runat="server" OnClick="btnUpload_Click" />
                        </div>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                        <asp:Button ID="btnSubmit11" runat="server" Visible="false" OnClick="btnSubmit_Click"
                            CssClass="btn" CausesValidation="false" OnClientClick="return TopicSave();" />
                        
                    <asp:Button ID="btnCancel11" runat="server" Visible="false" OnClick="btnCancel_Click"
                        CssClass="btn" CausesValidation="false" />&nbsp;
                    </p>
                </td>
            </tr>
        </table>

        <div class="bolt_new_topic">
            <div class="contant-area">
                <h2>Edit Post</h2>
                <h5>Topic</h5>
                <p>
                    <asp:TextBox ID="txtSubject" runat="server" Width="82%" class="text"></asp:TextBox>
                    <span class="require">*</span>
                    <asp:RequiredFieldValidator ID="RequiredtxtSubject" runat="server" ControlToValidate="txtSubject"
                        Display="Dynamic" ValidationGroup="save"></asp:RequiredFieldValidator>
                </p>
                <h5>Topic Detail</h5>
                <!--HTML Editor-->
                <p>
                    <div style="width: 82%" style="margin-bottom: 20px;">
                        <uc:HTMLEditor ID="HTMLEditor" runat="server" />
                    </div>
                </p>
                <div id="trAttachment" runat="server">
                    <h5><%=Proxy[EnumText.enumForum_AddTopic_TitleUploadAttachment]%></h5>

                    <div id="divAttachments" class="pos_top_10 pos_bottom_10">
                        <asp:Repeater ID="rptAttachments" runat="server" OnItemDataBound="rptAttachments_ItemDataBound"
                            OnItemCommand="rptAttachments_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdAttachId" runat="server" />
                                        <%#Eval("Name")%>
                                        <asp:ImageButton ID="img" runat="server" ImageUrl='<%# this.ImagePath + "/database_delete.gif" %>'
                                            ToolTip="<%#Proxy[EnumText.enumForum_AddOrEditTopic_Page_ToolTipDelete]%>" CommandName="Delete"
                                            CommandArgument='<%#Eval("Id")%>' ImageAlign="Bottom" />
                                    </td>
                                    <td class="td_width2" align="left">
                                        <div style='<%#this.StyleIfScoreEnabled%>'>
                                            <%=Proxy[EnumText.enumForum_AddTopic_FiledDownLoadScore]%>&nbsp;
                                                <asp:TextBox ID="tbScore" runat="server" CssClass="txt txt_width" />
                                            <span class="require">*</span>
                                            <asp:RequiredFieldValidator ID="rfvDownloadScore" runat="server" Display="Dynamic"
                                                ControlToValidate="tbScore" ValidationGroup="add">
                                                <%=Proxy[EnumText.enumForum_AddTopic_ErrorPayForDownlaodIsRequired]%>
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvDownloadScore" runat="server" Type="Integer" ValueToCompare="0"
                                                Display="Dynamic" ControlToValidate="tbScore" Operator="GreaterThanEqual" ValidationGroup="add">
                                                    <%=Proxy[EnumText.enumForum_AddTopic_ErrorPlesaeInputNumber]%>
                                            </asp:CompareValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <%=Proxy[EnumText.enumForum_AddTopic_FiledDescription]%>&nbsp;
                                            <asp:TextBox ID="tbDescription" runat="server" CssClass="txt txt_width2" />
                                        <div class="attachment_sep_row">
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:FileUpload ID="file" runat="server" />
                        <asp:Button ID="btnUpload" class="reg-btn" ValidationGroup="add" runat="server" OnClick="btnUpload_Click" />
                    </div>
                </div>
                <div class="reg-btn-outer">
                    <p>
                        <%-- OnClientClick="javascript:if (Page_ClientValidate('add')) {return checkRepeatSubmit(this);}" ValidationGroup="add" CausesValidation="false"--%>
                        <asp:Button ID="btnSubmit" runat="server" Visible="false" OnClick="btnSubmit_Click"
                            class="reg-btn" OnClientClick="return TopicSave();" CausesValidation="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Visible="false" OnClick="btnCancel_Click"
                            class="reg-btn" CausesValidation="false" />&nbsp;
                    </p>

                </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function InitPollDate() {
            var o = document.getElementById('<%=chkPollDate.ClientID%>');
            if (o != null) {
                var IfChecked = o.checked;
                if (IfChecked)
                    document.getElementById("tdPollDate").style.display = "";
                else
                    document.getElementById("tdPollDate").style.display = "none";
            }
        }
        InitPollDate();

        function TopicSave() {
            if (Page_ClientValidate('edit') == false)
                return false;
            if (Page_ClientValidate('save') == false)
                return false;
        }
    </script>

</asp:Content>
