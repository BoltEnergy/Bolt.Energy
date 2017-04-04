<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"  ValidateRequest="false"
    CodeBehind="AddTopic.aspx.cs" Inherits="Forum.UI.AddTopic"  %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Src="~/UserControl/HTMLEditor.ascx" TagName="HTMLEditor" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/NavigationBar.ascx" TagName="NavigationBar" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="JS/Common/RepeatSubmit.js" type="text/javascript"></script>
    <link href="CSS/inner/style.css" rel="Stylesheet" type="text/css" />
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
            parentSpanDelete.innerHTML = "<img src='Images/database_delete.gif' onclick='javascript:if(window.confirm(\"" +
            "<%=Proxy[EnumText.enumForum_AddTopic_ConfirmDeleteThisAttachment]%>" + "\")){Close(this);}' style='cursor:hand'/>";
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
            //   text.value = text.value.replace(/\D/g,'');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="False"></asp:Label>
    </div>

     <%--<header class="b-header">
        <div class="wrap">
            <a href="javascript:;">
                <img src="images/logo.png" alt="Try Discourse" id="site-logo">
            </a>
        </div>
    </header>--%>
    <div class="bolt_new_topic">
        <div class="contant-area">
            <h2><%=Proxy[EnumText.enumForum_Topic_TitleAddTopic]%></h2>
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
                <div style="width: 82%" style="margin-bottom:20px;">
                    <uc:HTMLEditor ID="HTMLEditor" runat="server" />
                </div>
            </p>
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
            <div class="reg-btn-outer">
                <p>
                    <%-- OnClientClick="javascript:if (Page_ClientValidate('add')) {return checkRepeatSubmit(this);}" ValidationGroup="add" CausesValidation="false"--%>
                    <asp:Button ID="btnSubmit" runat="server" Visible="false" OnClick="btnSubmit_Click"
                            class="reg-btn" OnClientClick="return TopicSave();" CausesValidation="false" />
                        
                        <asp:Button ID="btnCancel" runat="server" Visible="false" OnClick="btnCancel_Click"
                            class="reg-btn" CausesValidation="false" />&nbsp;</p>
                
            </div>
        </div>
    </div>

    <!--Hided Code Not in use-->
    <div class="pos_bottom_10" style="display:none;">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_Topic_TitleAddTopic]%>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing="0" width="100%">
            <tr>
                <td class="row1" width="20%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Topic_FieldSubject] %></b></p>
                </td>
                <td class="row2" width="80%" valign="middle">
                    <p>
                        <!--hide by techtier-->
                        <%--<asp:TextBox ID="txtSubject" runat="server" Width="69%" CssClass="txt"></asp:TextBox>
                        <span class="require">*</span>
                        <asp:RequiredFieldValidator ID="RequiredtxtSubject" runat="server" ControlToValidate="txtSubject"
                            Display="Dynamic" ValidationGroup="save"></asp:RequiredFieldValidator>--%>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Topic_FieldContent] %></b></p>
                </td>
                <td class="row2" valign="middle">
                    <!--hide by techtier -->
                   <%-- <p>
                        <div style="width: 70%">
                            <uc:HTMLEditor ID="HTMLEditor" runat="server" />
                        </div>
                    </p>--%>
                </td>
            </tr>
            <tr>
                <td class="row1">
                    <p>
                        &nbsp;</p>
                </td>
                <td class="row2">
                    <p>
                        <span class="require">*
                            <%=Proxy[EnumText.enumForum_Public_RequiredField] %></span></p>
                </td>
            </tr>
            <!--Updated on 6 /3/2017 bu techtier for hiding the advance option-->
            <tr style="display:none;">
                <th colspan="2" colspan="2" style="text-align: left">
                    <p>
                        <%=Proxy[EnumText.enumForum_AddTopic_TitleAdvancedOptions]%>
                    </p>
                </th>
            </tr>
            <tr >
                <!--Updated on 6 /3/2017 bu techtier for hiding post setting type-->
                <td class="row1" align="right" style="display:none;">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_AddTopic_TitlePostSettings]%>
                        </b>
                    </p>
                </td>
                <td class="row2" valign="middle" style="display:none;">
                    <p>

                        <script type="text/javascript" language="javascript">
                            function setPayForView() {
                                var txtpay = document.getElementById('<%=txtPay.ClientID%>');
                                var radioPay = document.getElementById('<%=radioPay.ClientID%>');
                                if (radioPay == null)
                                    return;
                                if (radioPay.checked == true) {
                                    document.getElementById("Pay").style.display = "";
                                    radioPay.checked = true;
                                }
                                else if (radioPay.checked == false) {
                                    document.getElementById("Pay").style.display = "none";
                                    radioPay.checked = false;
                                }

                                txtpay.value = '0';
                            }
                        </script>

                        <input type="radio" runat="server" id="rdNormal" checked="true" name="radioPost"
                            onclick="setPayForView();" /><%=Proxy[EnumText.enumForum_AddTopic_FiledPostNormal]%>&nbsp;
                        <span id="fdReply" runat="server">
                            <input type="radio" runat="server" id="rdReply" name="radioPost" onclick="setPayForView();" />
                            <%=Proxy[EnumText.enumForum_AddTopic_FiledPostNeedReplayToView]%>&nbsp; </span>
                        <span id="fdpay" runat="server">
                            <input type="radio" runat="server" id="radioPay" name="radioPost" onclick="setPayForView();" />
                            <%=Proxy[EnumText.enumForum_AddTopic_FiledPostNeedPayScoreToView]%>
                            <span id="Pay" style="display: none">&nbsp;&nbsp;&nbsp;&nbsp;<input type="text" runat="server"
                                id="txtPay" class="txt" value="0" style="width: 50px;" />
                                <%=Proxy[EnumText.enumForum_AddTopic_FiledHowManyScoreRequired]%>
                                <span class="require">*</span>
                                <asp:RequiredFieldValidator ValidationGroup="save" ID="rfvtxtPay" ControlToValidate="txtPay"
                                    Display="Dynamic" runat="server">
                                <%=Proxy[EnumText.enumForum_AddTopic_ErrorPayForTopicIsRequired]%>
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvtxtPay" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtPay" Operator="GreaterThanEqual" ValidationGroup="save">
                                 <%=Proxy[EnumText.enumForum_AddTopic_ErrorPlesaeInputNumber]%>
                                </asp:CompareValidator>
                            </span></span>
                        <br />
                    </p>
                </td>
            </tr>
            <%--Polls--%>
            <!--Updated on 6 /3/2017 bu techtier for hiding create Polling option-->
            <tr style="display:none;">
                <td class="row1" align="right">
                    <p>
                        <input id="chkPollCreation" type="checkbox" runat="server" value="Poll creation"
                            onclick="ShowPollCreation();" />
                        <b>
                            <%=Proxy[EnumText.enumForum_AddTopic_TiltePollCreation]%></b></p>
                </td>
                <td class="row2" style="text-align: left" valign="middle">
                    <p>
                        <div id="divPollCreation" style="<%=IfShowPollConection%>">
                            <fieldset>
                                <
                                    >
                                    <%=Proxy[EnumText.enumForum_AddTopic_SubTitlePollOptions]%></>
                                <center>
                                    <div>
                                        <textarea id="tbPollOptions" runat="server" cols="67" rows="10"></textarea></div>
                                </center>
                            </fieldset>
                            <br />
                            <fieldset>
                                <legend>
                                    <%=Proxy[EnumText.enumForum_AddTopic_SubTitleOtherOptions]%></legend>
                                <div>
                                    <table border="0" class="tb_inner" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td width="140" align="right">
                                                <%=Proxy[EnumText.enumForum_AddTopic_SubTitleMulitipleChoice]%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMulitiple" CssClass="txt" runat="server" Text="1" Style="width: 50px" />
                                                <span class="require">*</span>
                                                <asp:RequiredFieldValidator ID="rfvtxtMulitiple" runat="server" Display="Dynamic"
                                                    ControlToValidate="txtMulitiple" ValidationGroup="save"><%--ValidationGroup="add"--%>
                                                    <%=Proxy[EnumText.enumForum_AddTopic_ErrorMulitipleChoiceIsRequired]%></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvtxtMulitiple" runat="server" Type="Integer" Display="Dynamic"
                                                    ValueToCompare="1" ControlToValidate="txtMulitiple" Operator="GreaterThanEqual"
                                                    ValidationGroup="save"><%--ValidationGroup="add"--%>
                                                    <%=Proxy[EnumText.enumForum_AddTopic_ErrorPlesaeInputNumber]%>
                                                </asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <p>
                                                    <input type="checkbox" id="chkPollDate" runat="server" onclick="ShowPollDate();" />
                                                    <%=Proxy[EnumText.enumForum_AddTopic_FiledPollDateTo]%></p>
                                            </td>
                                            <td>
                                                <div id="tdPollDate" style="display: none;">
                                                    <asp:TextBox ID="txtPollDate" runat="server" Width="100px" CssClass="txt"> </asp:TextBox>
                                                    <img onclick="WdatePicker({el:$dp.$('ctl00_ContentPlaceHolder1_txtPollDate')})" src="images/datePicker.gif"
                                                        align="absmiddle" style="cursor: pointer; width: 16px; height: 22px;" />
                                                    &nbsp;
                                                    <asp:RequiredFieldValidator ID="rfvPollDate" runat="server" Display="Dynamic" ControlToValidate="txtPollDate"
                                                        ValidationGroup="save">
                                                    <%=Proxy[EnumText.enumForum_AddOrEditTopic_Page_PollDateIsRequired]%></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revDate" runat="server" ControlToValidate="txtPollDate"
                                                        ValidationGroup="save" ValidationExpression="(^\s*$)|(^\s*(((0?2-29-(19|20)(([02486][048])|([13579][26]))))|((((0?[1-9])|(1[0-2]))-((0?[1-9])|(1\d)|(2[0-8])))|((((0?[13578])|(1[02]))-31)|(((0?[1,3-9])|(1[0-2]))-(29|30))))-((20[0-9][0-9])|(19[0-9][0-9])))\s*$)">
                                                    <%=Proxy[EnumText.enumForum_AddTopic_ErrorPollDateIsErrorFormat]%></asp:RegularExpressionValidator></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                            <br />
                        </div>
                    </p>
                </td>
            </tr>
            <%--Attachment--%>
            <tr id="trAttachment" style="border-right: solid 1px #aaa" runat="server">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_AddTopic_TitleUploadAttachment]%></b></p>
                </td>
                <td class="row2" valign="middle">
                    <p>
                        <table class="tb_attachment">
                            <!--hide by techtier-->
                            
                        </table>
                        <div id="divAttachments" class="pos_top_10 pos_bottom_10">
                          
                        </div>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                       
                </td>
            </tr>
        </table>
    </div>
    <!------------------------->

    <script language="javascript" type="text/javascript">
        // setPayForView();
        function InitPayForView() {
            var txtpay = document.getElementById('<%=txtPay.ClientID%>');
            var radioPay = document.getElementById('<%=radioPay.ClientID%>');
            if (radioPay == null)
                return;
            if (radioPay.checked == true) {
                document.getElementById("Pay").style.display = "";
                radioPay.checked = true;
            }
            else if (radioPay.checked == false) {
                document.getElementById("Pay").style.display = "none";
                radioPay.checked = false;
            }
        }
        function InitPollDate() {
            var IfChecked = document.getElementById('<%=chkPollDate.ClientID%>').checked;
            if (IfChecked)
                document.getElementById("tdPollDate").style.display = "";
            else
                document.getElementById("tdPollDate").style.display = "none";
            if ('<%=IsPostBack %>'.toLowerCase() == 'false') {
                var date = '<%=DateTime.Now.ToString("MM-dd-yyyy")%>';
                document.getElementById('<%=txtPollDate.ClientID%>').value = date;
            }
        }
        InitPayForView();
        InitPollDate();
    </script>

    <script language="javascript" type="text/javascript">
        function TopicSave() {
            if (Page_ClientValidate('add') == false)
                return false;
            if (Page_ClientValidate('save') == false)
                return false;
        }
    </script>

</asp:Content>
