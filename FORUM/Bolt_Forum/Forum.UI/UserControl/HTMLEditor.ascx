<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTMLEditor.ascx.cs"
    Inherits="Com.Comm100.Forum.UI.UserControl.HTMLEditor" %>

<script type="text/javascript" src="<%=ResolveUrl("~/JS/HTMLEditor/tiny_mce.js") %>"></script>

<%--<script type="text/javascript" src="<%=ResolveUrl("~/JS/HTMLEditor/tiny_mce_gzip.js") %>"></script>

<script type="text/javascript">
    tinyMCE_GZ.init({
        mode: "textareas",
        theme: "advanced",
        plugins: "emotions",
        languages: "en",
        disk_cache: true,
        debug: false
    });
</script>--%>

<script type="text/javascript">
    tinyMCE.init({
        // General options
        <%=modeHtmlCode%>
        
        theme: "advanced",
        plugins: "emotions",
        language:"<%=LanguageName%>",

        // Theme options
        theme_advanced_buttons1: "fontselect,fontsizeselect,forecolor,|,undo,redo,|,emotions,|,code",
        theme_advanced_buttons2: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,bullist,numlist,|,outdent,indent,| <%=ImageAndLink %>",
        theme_advanced_buttons3: "",

        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        
        content_css:"<%=StylePath %>",
        
        relative_urls: false,
        remove_script_host: false,
        document_base_url: "",
//        forced_root_block: false,
//        force_br_newlines: true,
//        force_p_newlines: false,
        
        <%=Limit?"":"valid_elements : '@[id|style|title|lang|xml::lang],a[rel|rev|charset|hreflang|tabindex|accesskey|type|name|href|target|title|class],blockquote,cite,strong/b,em/i,u,p[align],-ol[type|compact],-ul[type|compact],-li,br,img[longdesc|usemap|src|border|alt=|title|hspace|vspace|width|height|align],-div,-span,-h1,-h2,-h3,-h4,-h5,-h6,hr[size|noshade]'," %>
       // valid_elements : '@[id|style|title|lang|xml::lang],a[rel|rev|charset|hreflang|tabindex|accesskey|type|name|href|target|title|class],blockquote,cite,strong/b,em/i,u,p[align],-ol[type|compact],-ul[type|compact],-li,br,img[longdesc|usemap|src|border|alt=|title|hspace|vspace|width|height|align],-div,-span,-h1,-h2,-h3,-h4,-h5,-h6,hr[size|noshade]',
        //theme_advanced_statusbar_location : "bottom"

        setup: function(ed) {
                ed.onKeyUp.add(function(ed, e) {
                    checkLength(tinyMCE.activeEditor, <%= MaxLength %>);
                });
                ed.onPaste.add(function(ed, e) {
                    checkLength(tinyMCE.activeEditor, <%= MaxLength %>);
                });
                ed.onPostProcess.add(function(ed,o){
                    // javascript:
                    o.content=o.content.replace(/<[a-z][^>]*\s*(href|src)\s*=[^>]+/ig,function($0){
                        return $0=$0.replace(/\s*(href|src)\s*=\s*("\s*(javascript|vbscript):[^"]+"|'\s*(javascript|vbscript):[^']+'|(javascript|vbscript):[^\s]+)/ig,"");
                    });
                    //o.content=o.content.replace(/\s*(href|src)\s*=\s*("\s*(javascript|vbscript):[^"]+"|'\s*(javascript|vbscript):[^']+'|(javascript|vbscript):[^\s]+)/ig,"");
                    // css
                    o.content=o.content.replace(/<[a-z][^>]*\s*style\s*=[^>]+/ig,function($0){
                        //return $0=$0.replace(/\s*style\s*=\s*("[^"]+(expression)[^"]+"|'[^']+(expression)[^']+'|[^\s]+(expression)[^\s]+)\s*/ig,"");
                        return $0=$0.replace(/\s*style\s*=\s*("[^"]+(expression|javascript|vbscript)[^"]+"|'[^']+(expression|javascript|vbscript)[^']+'|[^\s]+(expression|javascript|vbscript)[^\s]+)\s*/ig,"");
                    });
                    //o.content=o.content.replace(/\s*style\s*=\s*("[^"]+(expression)[^"]+"|'[^']+\2[^']+'|[^\s]+\2[^\s]+)\s*/ig,"");
                });
            }
       
    }); 
    var buffer = "";
        function checkLength(editor, maxLength) {
            if(editor.getContent().length > maxLength){
            //if (editor.getBody().innerText.length > maxLength) {
                alert("The text that you have entered is too long ("+editor.getContent().length+" characters). Please shorten it to "+maxLength+" characters long.");
                //alert("You have typed the maximum text allowed!");
                editor.setContent(buffer);
            }
            else {
                buffer = editor.getContent();
            }            
        }
        function goToTheEnd(ed) {
        //var ed = tinyMCE.activeEditor;
        var root = ed.dom.getRoot();
        var lastnode = root.lastChild;
        if (tinymce.isGecko) {
            lastnode.innerHTML = "&nbsp;";
            lastnode = lastnode.lastChild;
        }
        //alert(lastnode);
        ed.selection.select(lastnode);
        ed.selection.collapse(false);
    }  
</script>

<div align="center">
    <textarea id="editor"  name="editor" rows="15" cols="60" style="width: 100%;background-color:#fff" runat="server">
	</textarea>
</div>
