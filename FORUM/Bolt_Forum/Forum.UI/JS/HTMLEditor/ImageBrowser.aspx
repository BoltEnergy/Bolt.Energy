<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageBrowser.aspx.cs" Inherits="Com.Comm100.Forum.UI.HTMLEditor.ImageBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="tiny_mce_popup.js"></script>
	<script type="text/javascript" src="utils/mctabs.js"></script>
	<script type="text/javascript" src="utils/form_utils.js"></script>
    <script language="javascript" type="text/javascript">
        function cancelUpload(showUpload) {
            if (showUpload) {
                document.getElementById("divUpload").style.display = "";
                document.getElementById("divInput").style.display = "none";
                document.getElementById("txtFileName").value = "";
            }
            else {
                document.getElementById("divUpload").style.display = "none";
                document.getElementById("divInput").style.display = "";
            }
        }
        window.onload = new function() { parent.document.getElementById("upload_tab").style.display = ""; };
    </script>
</head>
<body style="position:absolute;top:-8px;left:-8px;">
<form id="form1" runat="server">

         <table border="0" cellpadding="4" cellspacing="0">
          <tr>
            <td class="nowrap"><label for="src">{#advanced_dlg.image_src}</label>
              <%-- <span style="vertical-align:bottom"><img src="../../images/help.gif" alt="{#advanced_dlg.help_upload}"/></span>--%>
            </td>
            <td><table border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td>
                  <div id="divUpload">
                  <asp:FileUpload id="fileImage" runat="server" Width="185px" /><br />
                  <asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" />                  
                  <input type="hidden" id="hdnSiteId" value="" runat="server" />
                  </div>
                  <div id="divInput" style="display:none">
                    <asp:TextBox ID="txtFileName" Width="133px" runat="server"></asp:TextBox>
                    <input type="button" value="Cancel" onclick="javascript:cancelUpload(true);" />
                  </div>
                  <asp:PlaceHolder ID="phScript" runat="server"></asp:PlaceHolder>
                  </td>
                </tr>
              </table></td>
          </tr>
<%--		  <tr>
			<td><label for="image_list">{#advanced_dlg.image_list}</label></td>
			<td><select id="image_list" name="image_list" onchange="document.getElementById('src').value=this.options[this.selectedIndex].value;document.getElementById('alt').value=this.options[this.selectedIndex].text;"></select></td>
		  </tr>--%>
          <tr>
            <td class="nowrap"><label for="alt">{#advanced_dlg.image_alt}</label></td>
            <td><input id="alt" name="alt" type="text" value="" style="width: 190px" runat="server" /></td>
          </tr>
          <tr>
            <td class="nowrap"><label for="align">{#advanced_dlg.image_align}</label></td>
            <td><select id="align" name="align"  runat="server">
                <option value="">{#not_set}</option>
                <option value="baseline">{#advanced_dlg.image_align_baseline}</option>
                <option value="top">{#advanced_dlg.image_align_top}</option>
                <option value="middle">{#advanced_dlg.image_align_middle}</option>
                <option value="bottom">{#advanced_dlg.image_align_bottom}</option>
                <option value="text-top">{#advanced_dlg.image_align_texttop}</option>
                <option value="text-bottom">{#advanced_dlg.image_align_textbottom}</option>
                <option value="left">{#advanced_dlg.image_align_left}</option>
                <option value="right">{#advanced_dlg.image_align_right}</option>
              </select></td>
          </tr>
          <tr>
            <td class="nowrap"><label for="width">{#advanced_dlg.image_dimensions}</label></td>
            <td><input id="width" name="width" type="text" value="" size="3" maxlength="5" runat="server" />
              x
              <input id="height" name="height" type="text" value="" size="3" maxlength="5" runat="server" /></td>
          </tr>
          <tr>
            <td class="nowrap"><label for="border">{#advanced_dlg.image_border}</label></td>
            <td><input id="border" name="border" type="text" value="" size="3" maxlength="3" runat="server" /></td>
          </tr>
          <tr>
            <td class="nowrap"><label for="vspace">{#advanced_dlg.image_vspace}</label></td>
            <td><input id="vspace" name="vspace" type="text" value="" size="3" maxlength="3" runat="server" /></td>
          </tr>
          <tr>
            <td class="nowrap"><label for="hspace">{#advanced_dlg.image_hspace}</label></td>
            <td><input id="hspace" name="hspace" type="text" value="" size="3" maxlength="3" runat="server" /></td>
          </tr>
        </table>   
</form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var lastHref = top.window.opener.location.href.toLowerCase();
    var match = "&siteid=";
    var match2 = "?siteid=";
    var start = -1;
    var end = -1;
    if (lastHref.indexOf(match) >= 0) {
        start = lastHref.indexOf(match) + 8;
    }
    else if (lastHref.indexOf(match2) >= 0) {
        start = lastHref.indexOf(match2) + 8;
    }
    if (start > -1) {
        end = lastHref.indexOf("&", start);
        if (end > start) {
            document.getElementById("hdnSiteId").value = lastHref.substring(start, end);
        }
        else {
            document.getElementById("hdnSiteId").value = lastHref.substring(start);
        }
    }
    

</script>