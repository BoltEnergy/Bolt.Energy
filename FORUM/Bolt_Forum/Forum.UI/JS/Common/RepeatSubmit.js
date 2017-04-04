var clickedButtonIds = ",";
function checkRepeatSubmit(btn) {
    var btnId = btn.id;
    var temp = "," + btnId + ",";
    if (clickedButtonIds.indexOf(temp, 0) >= 0) {
        return false;
    }
    else {
        clickedButtonIds += btnId + ",";
        return true;
    }
}