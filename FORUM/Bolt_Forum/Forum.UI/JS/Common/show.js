function show(d) {
    var de = document.documentElement;
    var w = window.innerWidth || self.innerWidth || (de && de.clientWidth) || document.body.clientWidth;
    var h = window.innerHeight || self.innerHeight || (de && de.clientHeight) || document.body.clientHeight;

    var yScrolltop;

    if (self.pageYOffset) {
        yScrolltop = self.pageYOffset;
    }
    else if (document.documentElement && document.documentElement.scrollTop) {
        yScrolltop = document.documentElement.scrollTop;
    }
    else if (document.body) {
        yScrolltop = document.body.scrollTop;
    }

    width = d.style.width.replace("px", "");
    height = d.style.height.replace("px", "");

    d.style.position = "absolute";
    d.style.top = (yScrolltop + (h - height) / 2) + 'px';
    d.style.left = ((w - width) / 2) + 'px';
    d.style.display = "block";
    d.style.zIndex = 100000;
}