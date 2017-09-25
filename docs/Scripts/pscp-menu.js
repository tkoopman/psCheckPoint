var menuCount = 1;
var urlParams;
(window.onpopstate = function () {
    var match,
        pl     = /\+/g,  // Regex for replacing addition symbol with a space
        search = /([^&=]+)=?([^&]*)/g,
        decode = function (s) { return decodeURIComponent(s.replace(pl, " ")); },
        query  = window.location.search.substring(1);

    urlParams = {};
    while (match = search.exec(query))
       urlParams[decode(match[1])] = decode(match[2]);
})();

var displayName;

function createMenu(jsonTitle, jsonFile = "content.json") {
    displayName = jsonTitle;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var chapters = JSON.parse(this.responseText);
            var menu = document.getElementById("menu-content");
            if (chapters.length != 0) {
                for (var i = 0; i < chapters.length; i++) {
                    var chapter = chapters[i];
                    ProcessChapter(chapter, menu, "", false);
                }

                SetActiveMenu(urlParams["Menu"]);
            }
        }
    };
    xmlhttp.open("GET", jsonFile, true);
    xmlhttp.send();
}

function ProcessChapter(chapter, menu, location, hasCmdlets) {
    var SubName = "m" + menuCount++;
    var l = location + "/" + SubName;
    var m = document.createElement("LI");
    m.setAttribute("data-toggle", "collapse");
    m.setAttribute("data-target", "#" + SubName);
    m.className = "collapsed";
    m.id = SubName + "title";

    menu.appendChild(m);
    m.innerHTML = '<a href="#">' + chapter["Name"] + ' <span class="arrow"></span></a>';

    var c = document.createElement("UL");
    c.className = "sub-menu collapse";
    c.id = SubName;
    menu.appendChild(c);

    if (chapter["Cmdlets"].length != 0) {
        for (var i = 0; i < chapter["Cmdlets"].length; i++) {
            var cmdlet = chapter["Cmdlets"][i];
            hasCmdlets = ProcessCmdlet(cmdlet, c, l) || hasCmdlets;
        }
    }

    if (chapter["SubChapters"].length != 0) {
        for (var i = 0; i < chapter["SubChapters"].length; i++) {
            var sub = chapter["SubChapters"][i];
            hasCmdlets = ProcessChapter(sub, c, l, false) || hasCmdlets;
        }
    }

    if (!hasCmdlets) {
        m.innerHTML += ' <i class="fa fa-exclamation-circle" aria-hidden="true"></i>';
    }

    return hasCmdlets;
}

function ProcessCmdlet(cmdlet, menu, location) {
    var m = document.createElement("LI");
    m.id = makeSafeName(cmdlet["Name"]);
    m.className = "cmdlet"
    if (m.id === urlParams.get("API")) {
        m.className += " active";
    }

    var qs;
    if (cmdlet["Class"] == null) {
        qs = '?Content=NotImplemented.html';
    } else {
        qs = '?Content=md/' + cmdlet["Cmdlet"] + '.md';
    }
    qs += '&Menu=' + location;
    qs += '&API=' + m.id;
    m.innerHTML = '<a href="' + qs + '">' + ((cmdlet[displayName] == null) ? cmdlet["Name"] : cmdlet[displayName]) + '</a>';
    if (cmdlet["Class"] == null) {
        m.innerHTML += ' <i class="fa fa-exclamation-circle" aria-hidden="true"></i>';
    }
    m.setAttribute("onclick", "document.location.search = '" + qs + "'");
    menu.appendChild(m);

    return (cmdlet["Class"] != null);
}

function makeSafeName(name) {
    return name.replace(/[^a-z0-9]/g, '');
}

function ShowContent() {
    var name = urlParams["Content"];
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var content = document.getElementById("content");
            if (name.endsWith(".md")) {
                var converter = new showdown.Converter(),
                    text = this.responseText,
                    html = converter.makeHtml(text);

                content.innerHTML = html;
            } else {
                content.innerHTML = this.responseText;
            }
        }
    };
    xmlhttp.open("GET", name, true);
    xmlhttp.send();
}

function SetActiveMenu(location) {
    var menus = location.split("/");
    for (var i = 1; i < menus.length; i++) {
        var menu = document.getElementById(menus[i] + "title");
        menu.className += " active";
        $("#" + menus[i]).collapse('show');
    }
}
