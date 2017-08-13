(function (n, t) {
    "use strict";
    var r = {},
    i = {},
    u = {
        init: function (r, u) {
            if (typeof t == "undefined") return !1;
            u.html("");
            i = {};
            var f = {
                area: function (t) {
                    var i = n('<table class="form" data-id="' + t.id + '"  data-type="' + t.type + '"><\/table>');
                    return i.css("margin-top", t.margintop + "px"),
                    i
                },
                field: function (r, u) {
                    var e = parseInt(r.attr("data-type")),
                    f = n("<tr><\/tr>");
                    r.append(f);
                    n.each(u,
                    function (u, o) {
                        var s = t.components[o.type].renderTable(o);
                        f.find("td").length >= e && (f = n("<tr><\/tr>"), r.append(f)); !s[0].callback || (i[s[0].callback.data.type] == undefined && (i[s[0].callback.data.type] = {}), i[s[0].callback.data.type][s[0].callback.data.id] = s[0].callback);
                        f.append(s)
                    })
                }
            };
            n.each(r.formData,
            function (r, e) {
                var o = f.area(e.area);
                f.field(o, e.fields);
                u.append(o);
                n.each(i,
                function (n, i) {
                    t.components[n].makeCallback(i)
                })
            })
        },
        get: function (i) {
            var r = [];
            return i.find(".custmerTd").each(function () {
                var u = n(this),
                f = u.attr("data-type"),
                i = t.components[f].getValue(u);
                i.value == undefined && (i.vaule = "");
                r.push(i)
            }),
            r
        },
        set: function (i, r) {
            n.each(i.data,
            function (n, i) {
                var u = r.find('[data-value="' + i.field + '"]');
                t.components[i.type].setValue(u, i)
            })
        },
        valid: function (i) {
            var r = !0;
            return i.find(".custmerTd").each(function () {
                var i = n(this),
                f = i.attr("data-type"),
                u = t.components[f].validTable(i);
                if (!u) return r = !1,
                u
            }),
            r
        }
    };
    n.fn.formRendering = function (t, i) {
        var f = n(this);
        r = n.extend(r, i);
        switch (t) {
            case "init":
                u.init(r, f);
                break;
            case "get":
                return u.valid(f) ? u.get(f) : !1;
            case "set":
                u.set(r, f)
        }
    }
})(window.jQuery, window.learun)