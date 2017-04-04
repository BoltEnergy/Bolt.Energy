function showWindow(_in, _out) {
    var ThickInner = document.getElementById(_in);
    var ThickOuter = document.getElementById(_out);
    //ThickInner.style.display = "block";
    //ThickOuter.style.display = "block";
    var x, y, innerY;
    var canViewHeight = document.body.clientHeight;
    var broswerHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : window.innerWidth;

    var de = document.documentElement;

    x = window.innerWidth ||
        self.innerWidth ||
        (de && de.clientWidth) ||
        document.body.clientWidth;
    //document.documentElement.clientWidth ? document.documentElement.clientWidth : window.innerWidth;
    y = window.innerHeight ||
        self.innerHeight ||
        (de && de.clientHeight) ||
        document.body.clientHeight;
    //canViewHeight >= broswerHeight ? canViewHeight : broswerHeight;

    if (typeof window.pageYOffset != 'undefined') {
        innerY = window.pageYOffset;
    }
    else if (typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat') {
        innerY = document.documentElement.scrollTop;
    }
    else if (typeof document.body != 'undefined') {
        innerY = document.body.scrollTop;
    }

    ThickInner.style.position = "absolute";
    ThickInner.style.zIndex = 99999;

    var _height, _width;
    ThickInner.style.display = "block";
    _height = ThickInner.offsetHeight;
    _width = ThickInner.offsetWidth;
    //_width = ThickInner.style.width.replace("px", "");
    
    //ThickInner.style.width = _width + 'px';
    ThickInner.style.left = (x - _width) / 2 + 'px';
    if (_height == "")
        ThickInner.style.top = innerY + 'px';
    else {
        //ThickInner.style.height = _height + 'px';
        ThickInner.style.top = innerY + broswerHeight / 2 - _height / 2 + 'px';
    }
    //alert(ThickOuter.offsetParent.offsetTop);
    ThickOuter.style.position = "absolute";
    ThickOuter.style.left = 0 + 'px';
    ThickOuter.style.top = 0 + 'px';  //ThickOuter.offsetParent.offsetTop==0 ? '0px' : '-' + ThickOuter.offsetParent.offsetTop + 'px';
    ThickOuter.style.width = (x < 800 ? 800 : x) + 'px';
    ThickOuter.style.height = OverlaySize() + 'px'; //(ThickInner.scrollHeight > OverlaySize() ? ThickInner.scrollHeight+20 : OverlaySize()) + ThickOuter.offsetParent.offsetTop + 'px';
    ThickOuter.style.zIndex = 99998;
    ThickOuter.style.display = "block";
}

function WindowClose(_in, _out) {
    var i = document.getElementById(_in);
    var o = document.getElementById(_out);

    if (i != null) i.style.display = "none";
    if (o != null) o.style.display = 'none';
}


function OverlaySize() {
    if (window.innerHeight && window.scrollMayY) {
        return window.innerHeight + window.scrollMaxY;
    }
    else if (document.body.scrollHeight > document.body.offsetHeight) {
    return document.body.scrollHeight;
    }
    else {
        return document.documentElement.clientHeight > document.body.offsetHeight ? document.documentElement.clientHeight : document.body.offsetHeight;
    }
}

//function showWindow(_in, _out) {

//    var ThickInner = document.getElementById(_in);
//    var ThickOuter = document.getElementById(_out);

//    var x, y, innerY;
//    var canViewHeight = document.body.clientHeight;
//    var broswerHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : window.innerWidth;

//    var de = document.documentElement;

//    x = GetWindowX(de);

//    //document.documentElement.clientWidth ? document.documentElement.clientWidth : window.innerWidth;
//    y = GetWindowY(de);
//    //canViewHeight >= broswerHeight ? canViewHeight : broswerHeight;

//    innerY = GetInnerY();

//    ThickInner.style.position = "absolute";
//    ThickInner.style.zIndex = 99999;

//    var _height, _width;

//    _height = ThickInner.style.height.replace("px", "");
//    _width = ThickInner.style.width.replace("px", "");

//    setInterval(function() { SetThickInner(de, ThickInner, _height, _width); }, 100);
//    ThickInner.style.display = "block";

//    ThickOuter.style.position = "absolute";
//    setInterval(function() { SetThickOuter(de, ThickOuter, 0, -200); }, 100);

//    ThickOuter.style.zIndex = 99998;
//    ThickOuter.style.display = "block";
//}


//function SetThickOuter(_document, _o, _left, _top) {
//    var x = GetWindowX(_document);
//    _o.style.left = _left + 'px';
//    _o.style.top = _top + 'px';
//    _o.style.width = x < 800 ? 800 : x + 'px';
//    _o.style.height = OverlaySize() + 'px';
//}

//function SetThickInner(_document, _o, _height, _width) {
//    var broswerHeight = GetBroswerHeight();
//    var innerY = GetInnerY();
//    _o.style.width = _width + 'px';
//    _o.style.height = _height + 'px';
//    _o.style.left = (x - _width) / 2 + 'px';
//    _o.style.top = innerY + broswerHeight / 2 - _height / 2 + 'px';
//}

//function GetWindowX(_document) {
//    x = window.innerWidth ||
//        self.innerWidth ||
//        (_document && _document.clientWidth) ||
//        document.body.clientWidth;
//    return x;
//}

//function GetWindowY(_document) {
//    y = window.innerHeight ||
//        self.innerHeight ||
//        (_document && _document.clientHeight) ||
//        document.body.clientHeight;
//    return y;
//}

//function GetBroswerHeight() {
//    var broswerHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : window.innerWidth;
//    return broswerHeight;
//}

//function GetInnerY() {
//    if (typeof window.pageYOffset != 'undefined') {
//        innerY = window.pageYOffset;
//    }
//    else if (typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat') {
//        innerY = document.documentElement.scrollTop;
//    }
//    else if (typeof document.body != 'undefined') {
//        innerY = document.body.scrollTop;
//    }
//    return innerY;
//}

//function WindowClose(_in, _out) {
//    var i = document.getElementById(_in);
//    var o = document.getElementById(_out);

//    if (i != null) i.style.display = "none";
//    if (o != null) o.style.display = 'none';
//}


//function OverlaySize() {
//    if (window.innerHeight && window.scrollMayY) {
//        return window.innerHeight + window.scrollMaxY;
//    }
//    else if (document.body.scrollHeight > document.body.offsetHeight) {
//        return document.body.scrollHeight;
//    }
//    else {
//        return document.body.offsetHeight;
//    }
//}