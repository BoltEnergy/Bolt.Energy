function $postJSON(url, data, asyncTF, callbackFn) {
    return $.ajax({
        type: "POST",
        url: url,
        data: data,
        async: asyncTF,
        success: callbackFn,
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('textStatus : ' + textStatus);
            console.log('errorThrown : ' + errorThrown);
        },
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    });
};


$(document).ready(function () {

    $(".deletepost").click(function () {

        var r = confirm("Are you sure you want to delete ?");
        if (r == true) {

            var topicid = $(this).attr("topicid");
            var url = 'ajaxcall.aspx/DeleteMyTopic';
            var data = "{topicid: " + topicid + "}";
            $postJSON(url, data, true,
           function (msg) {
               if (msg.d && msg.d == 'ok') {
                   $("#clsdeleteerror").text('Topic deleted successfully.');
                   $("#tr" + topicid).remove()
               }
               else {
                   $("#clsdeleteerror").text('Some error occured. Please try again.');
               }
           });
        }
    });
});
