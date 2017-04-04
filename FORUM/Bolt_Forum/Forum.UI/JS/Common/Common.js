/*common function*/
var maxDays = 31;
var maxMonths = 12;


function isMaxlength(obj, maxlength) {
    return obj.value.length + 1 <= maxlength;
}

function openWindow(url) {
    window.open(url);
}

function isTextEmpty(obj, txtErrorMessage) {
    if (obj.value.length == 0)
        errorMessage.innerText = "Can not be empty";
}

function ctrlvStop(evt) {
    if (!window.event) {
        var keyCode = evt.keyCode;
        var key = String.fromCharCode(keyCode).toLowerCase();
        if (evt.ctrlKey && key == "v") {
            evt.preventDefault();
            evt.stopPropagation();
        }
    }
}

function getToday() {
    var today = new Date();
    var year = today.getFullYear() + "";
    var month = (today.getMonth() + 1) + "";
    var day = today.getDay() + "";
    if (month.length == 1) month = "0" + month;
    if (day.length == 1) day = "0" + day;
    var todayDate = year + "-" + month + "-" + day;
    return todayDate;
}
function getTodayTo() {
    var today = new Date();
    var year = today.getFullYear() + "";
    var month = (today.getMonth() + 1) + "";
    var day = today.getDay() + "";
    if (month.length == 1) month = "0" + month;
    if (day.length == 1) day = "0" + day;
    var todayDate = month + "-" + day + "-" + year;
    return todayDate;
}

function validateDaysUnit(startDate, endDate) {
    var re = /-/g;
    var fstartDate = startDate.replace(re, "/");
    var fendDate = endDate.replace(re, "/");
    var n1 = new Date(fstartDate).getTime();
    var n2 = new Date(fendDate).getTime();
    var n3 = new Date().getTime();
    var days = (n2 - n1) / (24 * 60 * 60 * 1000);
    if (days < 0) {
        alert("The End Date selected is prior to the Start Date.");
        return false;
    }
    if (n1 > n3) {
        alert("The Start Date selected is in the future.");
        return false;
    }
    if (days + 1 > maxDays) {
        alert("Select an end date no more than " + maxDays + " days after the start date.");
        return false;
    }
    return true;
}

function validateMonthsUnit(startYear, startMonth, endYear, endMonth) {
    var now = new Date();
    if (endYear * 12 + Number(endMonth) - startYear * 12 - Number(startMonth) < 0) {
        alert("The End Date selected is prior to the Start Date.");
        return false;
    }
    if (startYear * 12 + Number(startMonth) - now.getFullYear() * 12 - now.getMonth() - 1 > 0) {
        alert("The Start Date selected is in the future.");
        return false;
    }
    if (endYear * 12 + Number(endMonth) - startYear * 12 - Number(startMonth) + 1 > maxMonths) {
        alert("Select an end date no more than " + maxMonths + " months after the start date.");
        return false;
    }
    return true;
}


function changeBtnEnableByCheckBox(cbxId, btnId) {
    //debugger;
    var cbxObj = document.getElementById(cbxId);
    var btnObj = document.getElementById(btnId);
    if (!cbxObj.checked) btnObj.disabled = true;
    else btnObj.disabled = false;
}


function changeDivStyleBySelect(selObj, divIdarray) {
    if (selObj != null && selObj.tagName == "SELECT") {
        for (var i = 0; i < divIdarray.length; i++) {
            var objdiv = document.getElementById(divIdarray[i]);
            if (objdiv == null || objdiv.tagName != "DIV")
                continue;
            else
                objdiv.style.display = "none";
        }
        var currdiv = document.getElementById(divIdarray[selObj.selectedIndex]);
        if (currdiv != null)
            currdiv.style.display = "block";
    }
}

function changeDivStyleByRadio(radObj, divIdarray) {
    //debugger;
    if (radObj != null && radObj.tagName == "INPUT" && radObj.type == "radio") {
        var radObjarray = document.getElementsByName(radObj.name);
        for (var i = 0; i < radObjarray.length; i++) {
            var objdiv = document.getElementById(divIdarray[i]);
            if (radObjarray[i].checked) {
                if (objdiv == null || objdiv.tagName != "DIV")
                    continue;
                else
                    objdiv.style.display = "block";
            }
            else {
                if (objdiv == null || objdiv.tagName != "DIV")
                    continue;
                else
                    objdiv.style.display = "none";
            }
        }
    }
}

function changeDivStyleByCheckBox(cbxObj, divId) {
    if (cbxObj != null && cbxObj.tagName == "INPUT" && cbxObj.type == "checkbox") {
        var divObj = document.getElementById(divId);
        if (divId != null && cbxObj.checked)
            divObj.style.display = "block";
        else if (divId != null && !cbxObj.checked)
            divObj.style.display = "none";
    }
}

function changeCheckBoxStyleByCheckBox(cbxObj, cbxId) {
    if (cbxObj != null && cbxObj.tagName == "INPUT" && cbxObj.type == "checkbox") {
        var cbx1 = document.getElementById(cbxId);
        if (cbxId != null && cbxObj.checked) {
            cbx1.disabled = false;
            cbx1.parentElement.disabled = false;
        }
        else if (cbxId != null && !cbxObj.checked) {
            cbx1.checked = false;
            cbx1.disabled = true;
        }
    }
}

function previewImageByTextBox(txtObj) {
    var obj1 = document.getElementById(txtObj);
    var td = "<div style='float:left;'><div style='text-align:right;'><img src='" +"http://" + (obj1.value).replace(/(^\s*)|(\s*$)/g,'') + "' /><br />" + "</div></div>";
    var nw = window.open("about:blank", "", "left=150,top=150,height=350,width=500,toolbar=no,location=no,menubar=no,status=no");
    //debugger;
    nw.document.write("<title>Preview Image</title>");   
    nw.document.write(td);
    nw.document.bgColor = "silver";
    nw.document.close();
}

function preview2ImageByTextBox(txtObj1,txtObj2) {
    var obj1 = document.getElementById(txtObj1);
    var obj2 = document.getElementById(txtObj2);
    var td = "<div style='float:left;position:relative; width:400px;'>"
            + "<div style='float:right; position:absolute; top:20px;right:20px;'><img src='" +"http://" + (obj1.value).replace(/(^\s*)|(\s*$)/g,'') + "' /></div>" 
            + "<div style='float:right; position:absolute; top:20px;right:20px;'><img src='" +"http://" + (obj2.value).replace(/(^\s*)|(\s*$)/g,'') + "' /></div>"
            + "</div>";
    var nw = window.open("about:blank", "", "left=150,top=150,height=350,width=500,toolbar=no,location=no,menubar=no,status=no");
    //debugger;
    nw.document.write("<title>Preview Image</title>");
    nw.document.write(td);
    nw.document.bgColor = "silver";
    nw.document.close();
}

function preview2ImageByURL(urlObj1, urlObj2) {
    var td = "<div style='float:left;position:relative; width:400px;'>"
            + "<div style='float:right; position:absolute; top:20px;right:20px;'><img src='" + urlObj1 + "' /></div>"
            + "<div style='float:right; position:absolute; top:20px;right:20px;'><img src='" + urlObj2 + "' /></div>"
            + "</div>";
    var nw = window.open("about:blank", "", "left=150,top=150,height=350,width=500,toolbar=no,location=no,menubar=no,status=no");
    //debugger;
    nw.document.write("<title>Preview Image</title>");
    nw.document.write(td);
     nw.document.bgColor = "silver";   
    nw.document.close();
}


function CopyAll(objName) {
    //debugger;
    var obj = document.getElementById(objName);
    obj.focus();
    obj.select();
    if (document.all) {
        var therange = obj.createTextRange();
        therange.execCommand("Copy");
    }
}

function nTabs(TabId, Num) {
    //debugger;
    var thisTab = document.getElementById(TabId);
    if (thisTab.className == "liactive") return;
    var tabsName = thisTab.parentNode.id;
    var tabList = document.getElementById(tabsName).getElementsByTagName("li");
    for (i = 1; i < tabList.length; i++) {
        if (i - 1 == Num) {
            thisTab.className = "liactive";
            var pans = thisTab.getElementsByTagName("SPAN");
            pans[0].className = "spanactive";
            document.getElementById(tabsName + "_Content" + (i - 1)).style.display = "block";
        } else {
            tabList[i].className = "linormal";
            var pans = tabList[i].getElementsByTagName("SPAN");
            pans[0].className = "spannormal";
            document.getElementById(tabsName + "_Content" + (i - 1)).style.display = "none";
        }
    }
    location.href = "#Top";
    //debugger;
}

function setUniqueRadioButton(nameregex, current) {
    var re = new RegExp(nameregex);
    var objArray = document.getElementsByTagName("INPUT");
    for (var i = 0; i < objArray.length; i++) {
        if (objArray[i].type == "radio") {
            if (re.test(objArray[i].name))
                objArray[i].checked = false;
        }
    }
    current.checked = true;

}

function selectAll(cbxObj) {
    //debugger;
    var tabObj = cbxObj.parentNode.parentNode.parentNode.parentNode;
    if (tabObj != null && tabObj.tagName == "TABLE") {
        var objArray = tabObj.getElementsByTagName("INPUT");
        for (var i = 0; i < objArray.length; i++) {
            if (objArray[i] != null && objArray[i].type == "checkbox")
                objArray[i].checked = cbxObj.checked;
        }
    }
}

function GetCheckedItemIds(checkBoxName, hiddenId) {
    //debugger;
    var ids = "";
    
    var objArray = document.getElementsByName(checkBoxName);
  
    for (var i = 0; i < objArray.length; i++) {
        if (objArray[i] && objArray[i].checked == true) {
            if (ids.length > 0) {
                ids += "," + objArray[i].id;
            } else {
                ids += objArray[i].id;
            }          
        }
    }
  
    
    if (document.getElementById(hiddenId))
        document.getElementById(hiddenId).value = ids;   
}

function initTabletrColor(tabId,style1,style2) {
    var tabObj = document.getElementById(tabId);
    if (tabObj == null) return;
    var trArray = tabObj.getElementsByTagName("TR");
    for (var i = 1; i < trArray.length; i++) {
        if (trArray[i] != null && i % 2 == 0) trArray[i].className = style1;
        else if (trArray[i] != null && i % 2 != 0) trArray[i].className = style2;
    }
}

var curRow;
var curRowclassName;
var className = "trHightLight";
function highLightRow(rowObj) {

    if (curRow) {
        curRow.className = curRowclassName;
        curRowclassName = rowObj.className;
        rowObj.className = className;
    }
    else {
        curRowclassName = rowObj.className;
        rowObj.className = className;
    }
    curRow = rowObj;
    
}


function isKeyTrigger(e, keyCode) {
    var argv = isKeyTrigger.arguments;
    var argc = isKeyTrigger.arguments.length;
    var bCtrl = false;
    if (argc > 2) bCtrl = argv[2];
    var bAlt = false;
    if (argc > 3) bAlt = argv[3];
    var nav4 = window.Event ? true : false;
    if (typeof e == 'undefinded') e = event;
    if (bCtrl && !((typeof e.ctrlKey != 'undefinded') ? e.ctrlKey : e.modifiers & Event.CONTROL_MASK > 0)) return false;
    if (bAlt && !((typeof e.altKey != 'undefinded') ? e.altKey : e.modifiers & Event.ALT_MASK > 0)) return false;
    var whichcode = 0;
    if (nav4) whichcode = e.which;
    else if (e.type == "keypress" || e.type == "keydown") whichcode = e.keyCode;
    else whichcode = e.button;

    return (whichcode == keyCode);
}

function Enter(e,objId) {
    debugger;
    var ie = navigator.appName == "Microsoft Internet Explorer" ? true : false;
    if (ie) {
        if (event.keyCode == 13) document.getElementById(objId).click();
    }
    else {
        if (isKeyTrigger(e, 13, false)) document.getElementById(objId).click();
    }
}

function replaceHttps2Http() {
    var aArray = document.getElementsByTagName("a");
    var text;
    for (var i = 0; i < aArray.length; i++) {
        text = aArray[i].innerHTML;
        aArray[i].href = aArray[i].href.replace("https://", "http://");
        aArray[i].innerHTML = text;

    }
}

function checkMaxLength(obj, maxlength,alertMessage) {
    if (obj.value.length > maxlength) {
        obj.value = obj.value.substring(0, maxlength);
        alert(alertMessage);
    }
}

function changeRememberEmailFormatVerify(obj,requiredObjId, formatObjId) {
    var email = obj.value;
    if (email.length > 0 && requiredObjId.length > 0) {
        document.getElementById(requiredObjId).style.display = "none";
    }
    var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (reg.test(email) && formatObjId.length > 0) {
        document.getElementById(formatObjId).style.display = "none";
    }
}
function changeRememberEmailCompireVerify(obj, compireObjId, requiredObjId, formatObjId) {
    var email = obj.value;
    if (email.length > 0 && requiredObjId.length > 0) {
        document.getElementById(requiredObjId).style.display = "none";
    }
    if (email == document.getElementById(compireObjId).value && formatObjId.length > 0) {
        document.getElementById(formatObjId).style.display = "none";
    }
}

function lTrim(str) {
    if (str.charAt(0) == " ") {
        str = str.slice(1);
        str = lTrim(str);
    }
    return str;
}
function rTrim(str) {
    var iLength;
    iLength = str.length;
    if (str.charAt(iLength - 1) == " ") {
        str = str.slice(0, iLength - 1);
        str = rTrim(str);
    }
    return str;
}

function $(_id) {
    return document.getElementById(_id);
}
