var list = $("nav>ul li > a"); 
$("nav > a").click(function(event){
    $("nav>ul").slideToggle();
});
list.click(function (event) {
    var submenu = this.parentNode.getElementsByTagName("ul").item(0);
    if(submenu!=null){
        event.preventDefault();
        $(submenu).slideToggle();
    }
});
