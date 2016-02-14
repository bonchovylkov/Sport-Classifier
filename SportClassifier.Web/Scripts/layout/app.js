var APP = APP || {};

APP.namespace = function (ns) {
    var objects = ns.split("."),
            parent = APP,
            startIndex = 0,
            i;

    if (objects[0] === "APP")
        startIndex = 1;

    for (i = startIndex; i < objects.length; i++) {
        var property = objects[startIndex];
        if (typeof parent[property] === "undefined")
            parent[property] = {};
        parent = parent[property];
    }

    return parent;
};