(function (n, t) {
	"use strict";
	function i(n) {
		return '<div class="item_field_label"><span>' + n.name + '<\/span><\/div><div class="item_field_value">' + n.text + '<\/div><div class="item_field_remove"><i  title="移除控件" class="del fa fa-close"><\/i><\/div>'
	}
	function r(i, r) {
		var u = n(".field_option"),
        e = r.field,
        f;
		i.type == 0 ? (f = e == "" ? t.createGuid() : e, u.find("#control_field").parents(".field_control").html('<input id="control_field" type="text" class="form-control" disabled data-text="' + f + '" value="' + f + '" />'), r.field = f) : (u.find("#control_field").parents(".field_control").html('<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>'), u.find("#control_field").comboBox({
			data: i.dbFields,
			id: "column",
			text: "remark",
			maxHeight: "300px",
			allowSearch: !0
		}), u.find("#control_field").comboBoxSetValue(e), u.find("#control_field").change(function () {
			var t = n(this).attr("data-value");
			r.field = t
		}))
	}
	function v(t) {
		var i = [];
		i.push({
			id: "NotNull",
			text: "不能为空！"
		});
		i.push({
			id: "Num",
			text: "必须为数字！"
		});
		i.push({
			id: "NumOrNull",
			text: "数字或空！"
		});
		i.push({
			id: "Email",
			text: "必须为E-mail格式！"
		});
		i.push({
			id: "EmailOrNull",
			text: "E-mail格式或空！"
		});
		i.push({
			id: "EnglishStr",
			text: "必须为字符串！"
		});
		i.push({
			id: "EnglishStrOrNull",
			text: "字符串或空！"
		});
		i.push({
			id: "Phone",
			text: "必须电话格式！"
		});
		i.push({
			id: "PhoneOrNull",
			text: "电话格式或者空！"
		});
		i.push({
			id: "Fax",
			text: "必须为传真格式！"
		});
		i.push({
			id: "Mobile",
			text: "必须为手机格式！"
		});
		i.push({
			id: "MobileOrNull",
			text: "手机格式或者空！"
		});
		i.push({
			id: "MobileOrPhone",
			text: "电话格式或手机格式！"
		});
		i.push({
			id: "MobileOrPhoneOrNull",
			text: "电话格式或手机格式或空！"
		});
		i.push({
			id: "Uri",
			text: "必须为网址格式！"
		});
		i.push({
			id: "UriOrNull",
			text: "网址格式或空！"
		});
		n("#control_verify").comboBox({
			data: i,
			id: "id",
			text: "text",
			maxHeight: "200px",
			allowSearch: !0
		}).change(function () {
			var i = n(this).attr("data-value");
			t.verify = i
		});
		n("#control_verify").comboBoxSetValue(t.verify)
	}
	function e(t) {
		var i = [];
		i.push({
			id: "NotNull",
			text: "不能为空！"
		});
		n("#control_verify").comboBox({
			data: i,
			id: "id",
			text: "text"
		}).change(function () {
			var i = n(this).attr("data-value");
			t.verify = i
		});
		n("#control_verify").comboBoxSetValue(t.verify)
	}
	function l(t) {
		var i = [];
		i.push({
			id: "dataItem",
			text: "数据字典"
		});
		i.push({
			id: "dataTable",
			text: "数据库表"
		});
		n("#control_dataSource").comboBox({
			data: i,
			id: "id",
			text: "text",
			description: ""
		}).change(function () {
			var i = n(this).attr("data-value");
			t.dataSource = i;
			i == "dataItem" ? (n(".dataDb").hide(), n(".dataItem").show()) : (p(t), n(".dataItem").hide(), n(".dataDb").show())
		});
		n("#control_dataSource").comboBoxSetValue(t.dataSource)
	}
	function y(t) {
		n("#control_default").comboBox({
			description: "无则不填"
		}).change(function () {
			var i = n(this).attr("data-value");
			t.defaultValue = i
		});
		n("#control_default").comboBoxSetValue(t.defaultValue)
	}
	function a(i) {
		function r(t) {
			n("#control_dataItem").comboBoxTree({
				data: t,
				maxHeight: "180px",
				click: function (t) {
					i.dataItemCode = t.value;
					i.dataItemCodeId = t.id;
					i.dataSource == "dataItem" && (n("#control_default").comboBox({
						url: "../../SystemManage/DataItemDetail/GetDataItemTreeJson?EnCode=" + i.dataItemCode,
						id: "value",
						text: "text",
						maxHeight: "200px",
						description: "无则不填"
					}), n("#control_default").comboBoxSetValue(i.defaultValue))
				},
				allowSearch: !0
			});
			n("#control_dataItem").comboBoxTreeSetValue(i.dataItemCodeId)
		}
		c ? r(c) : t.getDataForm({
			type: "get",
			url: "../../SystemManage/DataItem/GetTreeJson",
			async: !0,
			success: function (n) {
				c = n;
				r(n)
			}
		})
	}
	function p(i) {
		function r(r) {
			n("#control_db").unbind();
			n("#control_db").bind("change",
            function () {
            	var r = n(this).attr("data-value");
            	i.dbId = r;
            	h[r] ? u(h[r]) : t.getDataForm({
            		type: "get",
            		url: "../../SystemManage/DataBaseTable/GetTableListJson",
            		param: {
            			dataBaseLinkId: r
            		},
            		success: function (n) {
            			h[r] = n;
            			u(n)
            		}
            	})
            });
			n("#control_db").comboBox({
				data: r,
				id: "DatabaseLinkId",
				text: "DBAlias",
				selectOne: !i.dbId ? !0 : !1,
				maxHeight: "140px",
				description: ""
			});
			n("#control_db").comboBoxSetValue(i.dbId)
		}
		function u(r) {
			n("#control_dbTable").unbind();
			n("#control_dbTable").bind("change",
            function () {
            	var r = n(this).attr("data-value");
            	i.dbTable = r;
            	t.getDataForm({
            		type: "get",
            		url: "../../SystemManage/DataBaseTable/GetTableFiledListJson",
            		param: {
            			dataBaseLinkId: i.dbId,
            			tableName: r
            		},
            		async: !0,
            		success: function (n) {
            			f(n)
            		}
            	})
            });
			n("#control_dbTable").comboBox({
				data: r,
				id: "name",
				text: "name",
				selectOne: !i.dbTable ? !0 : !1,
				maxHeight: "120px",
				description: "",
				allowSearch: !0
			});
			n("#control_dbTable").comboBoxSetValue(i.dbTable)
		}
		function f(t) {
			n("#control_dbFiledText").unbind();
			n("#control_dbFiledValue").unbind();
			n("#control_dbFiledText").bind("change",
            function () {
            	i.dbFiledText = n(this).attr("data-value"); !n("#control_dbFiledValue").attr("data-value") || (i.dbFiledValue = n("#control_dbFiledValue").attr("data-value"), i.dataSource == "dataTable" && (n("#control_default").comboBox({
            		url: "../../SystemManage/DataSource/GetTableData?dbLinkId=" + i.dbId + "&tableName=" + i.dbTable,
            		id: i.dbFiledValue.toLowerCase(),
            		text: i.dbFiledText.toLowerCase(),
            		maxHeight: "200px",
            		description: "无则不填"
            	}), n("#control_default").comboBoxSetValue(i.defaultValue)))
            });
			n("#control_dbFiledValue").bind("change",
            function () {
            	i.dbFiledValue = n(this).attr("data-value"); !n("#control_dbFiledText").attr("data-value") || (i.dbFiledText = n("#control_dbFiledText").attr("data-value"), i.dataSource == "dataTable" && (n("#control_default").comboBox({
            		url: "../../SystemManage/DataSource/GetTableData?dbLinkId=" + i.dbId + "&tableName=" + i.dbTable,
            		id: i.dbFiledValue.toLowerCase(),
            		text: i.dbFiledText.toLowerCase(),
            		maxHeight: "200px",
            		description: "无则不填"
            	}), n("#control_default").comboBoxSetValue(i.defaultValue)))
            });
			n("#control_dbFiledText").comboBox({
				data: t,
				id: "column",
				text: "column",
				selectOne: !i.dbFiledText ? !0 : !1,
				maxHeight: "100px",
				description: ""
			});
			n("#control_dbFiledValue").comboBox({
				data: t,
				id: "column",
				text: "column",
				selectOne: !i.dbFiledValue ? !0 : !1,
				maxHeight: "100px",
				description: ""
			});
			n("#control_dbFiledText").comboBoxSetValue(i.dbFiledText);
			n("#control_dbFiledValue").comboBoxSetValue(i.dbFiledValue)
		}
		s ? r(s) : t.getDataForm({
			type: "get",
			url: "../../SystemManage/DataBaseLink/GetListJson",
			async: !0,
			success: function (n) {
				s = n;
				r(n)
			}
		})
	}
	function w(t, i) {
		n("#control_relation").comboBox({
			data: i,
			id: "id",
			text: "label",
			maxHeight: "200px",
			description: "无则不填"
		}).change(function () {
			var i = n(this).attr("data-value");
			t.relation = i
		});
		n("#control_relation").comboBoxSetValue(t.relation)
	}
	function f(n) {
		var t = "";
		switch (n) {
			case "NotNull":
			case "Num":
			case "Email":
			case "EnglishStr":
			case "Phone":
			case "Fax":
			case "Mobile":
			case "MobileOrPhone":
			case "Uri":
				t = '<font face="宋体">*<\/font>'
		}
		return t
	}
	function o(n) {
		var t = "";
		return n != "" && (t = 'isvalid="yes" checkexpession="' + n + '"'),
        t
	}
	var s, h = {},
    c, u;
	t.components = {
		text: {
			init: function () {
				return n('<div class="item_row" data-type="text" ><i  class="fa fa-italic"><\/i>文本框<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "文本框",
					type: "text",
					field: "",
					defaultValue: "",
					verify: ""
				},
                t[0].itemdata);
				t[0].itemdata = r;
				t.html(i({
					name: r.label,
					text: "文本框"
				}))
			},
			property: function (t, i) {
				var e = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>无样式的单行文本框<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"/><\/div>';
				u += '<div class="field_title">字段验证<\/div>';
				u += '<div class="field_control"><div id="control_verify" type="select" class="ui-select"><\/div>';
				u += '<div class="field_title">默认值<i title="仅在添加数据时默认填入" class="help fa fa-question-circle"><\/i><\/div>';
				u += '<div class="field_control"><input id="control_default" type="text" class="form-control" placeholder="无则不填"/><\/div>';
				e.html(u);
				f = i[0].itemdata;
				r(t, f);
				v(f);
				e.find("#control_label").val(f.label);
				e.find("#control_default").val(f.defaultValue);
				e.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				});
				e.find("#control_default").keyup(function () {
					var t = n(this).val();
					f.defaultValue = t
				})
			},
			renderTable: function (t) {
				var r = '<th class="formTitle">' + t.label + f(t.verify) + "<\/th>",
                i;
				return r += '<td class="formValue custmerTd" data-type="' + t.type + '"  data-value="' + t.field + '"   ><input id="' + t.id + '" type="text" class="form-control" ' + o(t.verify) + " /><\/td>",
                i = n(r),
                i.find("input").val(t.defaultValue),
                i
			},
			validTable: function (n) {
				return n.Validform()
			},
			getValue: function (n) {
				var t = n.find("input");
				return {
					type: "text",
					value: t.val(),
					field: n.attr("data-value"),
					realValue: t.val()
				}
			},
			setValue: function (n, t) {
				var i = n.find("input");
				i.val(t.value)
			}
		},
		textarea: {
			init: function () {
				return n('<div class="item_row" data-type="textarea" ><i class="fa fa-align-justify"><\/i>文本区<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "文本区",
					type: "textarea",
					field: "",
					defaultValue: "",
					height: "100px",
					verify: ""
				},
                t[0].itemdata);
				t[0].itemdata = r;
				t.html(i({
					name: r.label,
					text: "文本区"
				}))
			},
			property: function (t, i) {
				var e = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>无样式的多行文本框<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">字段高度<\/div>';
				u += '<div class="field_control"><input id="control_height" type="text" class="form-control" value="100px"><\/div>';
				u += '<div class="field_title">字段验证<\/div>';
				u += '<div class="field_control"><div id="control_verify" type="select" class="ui-select"><\/div>';
				u += '<div class="field_title">默认值<i title="仅在添加数据时默认填入" class="help fa fa-question-circle"><\/i><\/div>';
				u += '<div class="field_control"><input id="control_default" type="text" class="form-control" placeholder="无则不填"><\/div>';
				e.html(u);
				f = i[0].itemdata;
				r(t, f);
				v(f);
				e.find("#control_label").val(f.label);
				e.find("#control_height").val(f.height);
				e.find("#control_default").val(f.defaultValue);
				e.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				});
				e.find("#control_height").change(function () {
					var t = n(this).val();
					f.height = t
				});
				e.find("#control_default").keyup(function () {
					var t = n(this).val();
					f.defaultValue = t
				})
			},
			renderTable: function (t) {
				var r = '<th class="formTitle">' + t.label + f(t.verify) + "<\/th>",
                i;
				return r += '<td class="formValue custmerTd" data-type="' + t.type + '"  data-value="' + t.field + '"  ><textarea id="' + t.id + '"  class="form-control" style="height: ' + t.height + ';"' + o(t.verify) + " /><\/td>",
                i = n(r),
                i.find("textarea").val(t.defaultValue),
                i
			},
			validTable: function (n) {
				return n.Validform()
			},
			getValue: function (n) {
				var t = n.find("textarea");
				return {
					type: "textarea",
					value: t.val(),
					field: n.attr("data-value"),
					realValue: t.val()
				}
			},
			setValue: function (n, t) {
				var i = n.find("textarea");
				i.val(t.value)
			}
		},
		texteditor: {
			init: function () {
				return n('<div class="item_row" data-type="texteditor" ><i class="fa fa-edit"><\/i>编辑器<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "编辑器",
					type: "texteditor",
					field: "",
					defaultValue: "",
					height: "200px",
					verify: ""
				},
                t[0].itemdata);
				t[0].itemdata = r;
				t.html(i({
					name: r.label,
					text: "编辑器"
				}))
			},
			property: function (t, i) {
				var o = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>设置丰富文字样式的多行文本编辑区<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">字段高度<\/div>';
				u += '<div class="field_control"><input id="control_height" type="text" class="form-control" value="200px"><\/div>';
				u += '<div class="field_title">字段验证<\/div>';
				u += '<div class="field_control"><div id="control_verify" type="select" class="ui-select"><\/div>';
				u += '<div class="field_title">默认值<i title="仅在添加数据时默认填入" class="help fa fa-question-circle"><\/i><\/div>';
				u += '<div class="field_control"><input id="control_default" type="text" class="form-control" placeholder="无则不填"><\/div>';
				o.html(u);
				f = i[0].itemdata;
				r(t, f);
				e(f);
				o.find("#control_label").val(f.label);
				o.find("#control_height").val(f.height);
				o.find("#control_default").val(f.defaultValue);
				o.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				});
				o.find("#control_height").change(function () {
					var t = n(this).val();
					f.height = t
				});
				o.find("#control_default").keyup(function () {
					var t = n(this).val();
					f.defaultValue = t
				})
			},
			renderTable: function (t) {
				var u = '<th class="formTitle">' + t.label + f(t.verify) + "<\/th>",
                i,
                r;
				return u += '<td class="formValue custmerTd"  data-name="' + t.label + '"  data-type="' + t.type + '"  data-value="' + t.field + '"  data-verify="' + t.verify + '"  ><textarea id="' + t.id + '"  class="form-control"  /><\/td>',
                i = n(u),
                r = new Simditor({
                	textarea: i.find("textarea"),
                	placeholder: "这里输入内容...",
                	toolbar: ["title", "bold", "italic", "underline", "strikethrough", "color", "|", "ol", "ul", "blockquote", "table", "|", "link", "image", "hr", "|", "indent", "outdent"]
                }),
                i.find(".simditor .simditor-body").height(parseInt(t.height.replace(/px/g, ""))).css({
                	overflow: "auto",
                	"min-height": "0"
                }),
                i[1].simditor = r,
                r.setValue(t.defaultValue),
                i
			},
			validTable: function (n) {
				var u = n.attr("data-verify"),
                i,
                r;
				return u == "NotNull" ? (i = n[0].simditor.getValue(), i != "" && i != undefined && i != null) ? !0 : (r = n.attr("data-name"), t.dialogTop({
					msg: r + "不能为空！",
					type: "error"
				}), !1) : !0
			},
			getValue: function (n) {
				var t = n[0].simditor.getValue();
				return {
					type: "texteditor",
					value: t,
					field: n.attr("data-value"),
					realValue: t
				}
			},
			setValue: function (n, t) {
				n[0].simditor.setValue(t.value)
			}
		},
		radio: {
			init: function () {
				return n('<div class="item_row" data-type="radio" ><i class="fa fa-circle-thin"><\/i>单选框<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "单选项",
					type: "radio",
					field: "",
					defaultValue: "",
					dataSource: "dataItem",
					dataItemCode: "",
					dbId: "",
					dbTable: "",
					dbFiledText: "",
					dbFiledValue: ""
				},
                t[0].itemdata);
				t[0].itemdata = r;
				t.html(i({
					name: r.label,
					text: "单选项"
				}))
			},
			property: function (t, i) {
				var e = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>显示设置数据，从中只可选择一项<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">默认值<i title="仅在添加数据时默认填入" class="help fa fa-question-circle"><\/i><\/div>';
				u += '<div class="field_control"><div id="control_default" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">数据来源<\/div>';
				u += '<div class="field_control"><div id="control_dataSource" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataItem">数据字典<\/div>';
				u += '<div class="field_control dataItem"><div id="control_dataItem" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">数据库<\/div>';
				u += '<div class="field_control dataDb"><div id="control_db" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">数据表<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbTable" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">显示字段<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbFiledText" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">保存字段<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbFiledValue" type="select" class="ui-select"><\/div><\/div>';
				e.html(u);
				f = i[0].itemdata;
				r(t, f);
				l(f);
				y(f);
				a(f);
				e.find("#control_label").val(f.label);
				e.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				})
			},
			renderTable: function (i) {
				var u = '<th class="formTitle">' + i.label + "<\/th>",
                r;
				return u += '<td class="formValue custmerTd" data-type="' + i.type + '"  data-value="' + i.field + '"   ><div class="radio" id="' + i.id + '" style="color: #95A0AA;"><\/div><\/td>',
                r = n(u),
                i.dataSource == "dataItem" ? t.getDataForm({
                	type: "get",
                	url: "../../SystemManage/DataItemDetail/GetDataItemTreeJson?EnCode=" + i.dataItemCode,
                	async: !1,
                	success: function (t) {
                		n.each(t,
                        function (t, u) {
                        	var f = n('<label><input name="' + i.id + '" value="' + u.value + '"" type="radio">' + u.text + "<\/label>");
                        	r.find("div").append(f)
                        });
                		r.find('input[value="' + i.defaultValue + '"]').attr("checked", "checked")
                	}
                }) : t.getDataForm({
                	type: "get",
                	url: "../../SystemManage/DataSource/GetTableData?dbLinkId=" + i.dbId + "&tableName=" + i.dbTable,
                	async: !1,
                	success: function (t) {
                		n.each(t,
                        function (t, u) {
                        	var f = n('<label><input name="' + i.id + '" value="' + u[i.dbFiledValue.toLowerCase()] + '"" type="radio">' + u[i.dbFiledText.toLowerCase()] + "<\/label>");
                        	r.find("div").append(f)
                        });
                		r.find('input[value="' + i.defaultValue + '"]').attr("checked", "checked")
                	}
                }),
                r
			},
			validTable: function () {
				return !0
			},
			getValue: function (n) {
				return {
					type: "radio",
					value: n.find("input:checked").val(),
					field: n.attr("data-value"),
					realValue: n.find("input:checked").parent().text()
				}
			},
			setValue: function (n, t) {
				n.find('input[value="' + t.value + '"]').attr("checked", "checked")
			}
		},
		checkbox: {
			init: function () {
				return n('<div class="item_row" data-type="checkbox" ><i class="fa fa-square-o"><\/i>多选框<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "多选项",
					type: "checkbox",
					field: "",
					dataSource: "dataItem",
					dataItemCode: "",
					dbId: "",
					dbTable: "",
					dbFiledText: "",
					dbFiledValue: ""
				},
                t[0].itemdata);
				t[0].itemdata = r;
				t.html(i({
					name: r.label,
					text: "多选项"
				}))
			},
			property: function (t, i) {
				var e = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>显示设置数据，从中可以选择多项<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">数据来源<\/div>';
				u += '<div class="field_control"><div id="control_dataSource" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataItem">数据字典<\/div>';
				u += '<div class="field_control dataItem"><div id="control_dataItem" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">数据库<\/div>';
				u += '<div class="field_control dataDb"><div id="control_db" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">数据表<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbTable" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">显示字段<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbFiledText" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">保存字段<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbFiledValue" type="select" class="ui-select"><\/div><\/div>';
				e.html(u);
				f = i[0].itemdata;
				r(t, f);
				l(f);
				a(f);
				e.find("#control_label").val(f.label);
				e.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				})
			},
			renderTable: function (i) {
				var u = '<th class="formTitle">' + i.label + "<\/th>",
                r;
				return u += '<td class="formValue custmerTd" data-type="' + i.type + '" data-value="' + i.field + '" ><div class="checkbox" id="' + i.id + '" style="color: #95A0AA;"><\/div><\/td>',
                r = n(u),
                i.dataSource == "dataItem" ? t.getDataForm({
                	type: "get",
                	url: "../../SystemManage/DataItemDetail/GetDataItemTreeJson?EnCode=" + i.dataItemCode,
                	async: !1,
                	success: function (t) {
                		n.each(t,
                        function (t, u) {
                        	var f = n('<label><input name="' + i.id + '" value="' + u.value + '"" type="checkbox">' + u.text + "<\/label>");
                        	r.find("div").append(f)
                        })
                	}
                }) : t.getDataForm({
                	type: "get",
                	url: "../../SystemManage/DataSource/GetTableData?dbLinkId=" + i.dbId + "&tableName=" + i.dbTable,
                	async: !1,
                	success: function (t) {
                		n.each(t,
                        function (t, u) {
                        	var f = n('<label><input name="' + i.id + '" value="' + u[i.dbFiledValue.toLowerCase()] + '"" type="checkbox">' + u[i.dbFiledText.toLowerCase()] + "<\/label>");
                        	r.find("div").append(f)
                        })
                	}
                }),
                r
			},
			validTable: function () {
				return !0
			},
			getValue: function (t) {
				var i = "",
                r = "";
				return t.find("input:checked").each(function () {
					i != "" && (i += ",", r += ",");
					i += n(this).val();
					r += n(this).parent().text()
				}),
                {
                	type: "checkbox",
                	value: i,
                	field: t.attr("data-value"),
                	realValue: r
                }
			},
			setValue: function (t, i) {
				var r = i.value.split(",");
				n.each(r,
                function (n, i) {
                	t.find('input[value="' + i + '"]').attr("checked", "checked")
                })
			}
		},
		select: {
			init: function () {
				return n('<div class="item_row" data-type="select" ><i class="fa fa-caret-square-o-right"><\/i>下拉框<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "下拉框",
					type: "select",
					field: "",
					verify: "",
					height: "100px",
					dataSource: "dataItem",
					dataItemCode: "",
					dbId: "",
					dbTable: "",
					dbFiledText: "",
					dbFiledValue: ""
				},
                t[0].itemdata);
				t[0].itemdata = r;
				t.html(i({
					name: r.label,
					text: "下拉框"
				}))
			},
			property: function (t, i) {
				var o = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">字段验证<\/div>';
				u += '<div class="field_control"><div id="control_verify" type="select" class="ui-select"><\/div>';
				u += '<div class="field_title">数据来源<\/div>';
				u += '<div class="field_control"><div id="control_dataSource" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataItem">数据字典<\/div>';
				u += '<div class="field_control dataItem"><div id="control_dataItem" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">数据库<\/div>';
				u += '<div class="field_control dataDb"><div id="control_db" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">数据表<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbTable" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">显示字段<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbFiledText" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title dataDb">保存字段<\/div>';
				u += '<div class="field_control dataDb"><div id="control_dbFiledValue" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">下拉框高度<\/div>';
				u += '<div class="field_control"><input id="control_height" type="text" class="form-control" value="100px"><\/div>';
				o.html(u);
				f = i[0].itemdata;
				r(t, f);
				e(f);
				l(f);
				a(f);
				o.find("#control_label").val(f.label);
				o.find("#control_height").val(f.height);
				o.find("#control_height").change(function () {
					var t = n(this).val();
					f.height = t
				});
				o.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				})
			},
			renderTable: function (i) {
				var u = '<th class="formTitle">' + i.label + f(i.verify) + "<\/th>",
                r;
				return u += '<td class="formValue custmerTd" data-type="' + i.type + '"  data-value="' + i.field + '" ><div id="' + i.id + '" type="select" class="ui-select"  ' + o(i.verify) + " ><\/div><\/td>",
                r = n(u),
                i.dataSource == "dataItem" ? t.getDataForm({
                	type: "get",
                	url: "../../SystemManage/DataItemDetail/GetDataItemTreeJson?EnCode=" + i.dataItemCode,
                	async: !0,
                	success: function (n) {
                		r.find("#" + i.id).comboBox({
                			data: n,
                			id: "value",
                			text: "text",
                			maxHeight: i.height,
                			allowSearch: !0
                		})
                	}
                }) : t.getDataForm({
                	type: "get",
                	url: "../../SystemManage/DataSource/GetTableData?dbLinkId=" + i.dbId + "&tableName=" + i.dbTable,
                	async: !0,
                	success: function (n) {
                		r.find("#" + i.id).comboBox({
                			data: n,
                			id: i.dbFiledValue.toLowerCase(),
                			text: i.dbFiledText.toLowerCase(),
                			maxHeight: i.height,
                			allowSearch: !0
                		})
                	}
                }),
                r
			},
			validTable: function (n) {
				return n.Validform()
			},
			getValue: function (n) {
				var t = n.find(".ui-select").attr("data-value"),
                i = n.find(".ui-select-text").text();
				return {
					type: "select",
					value: t,
					field: n.attr("data-value"),
					realValue: i
				}
			},
			setValue: function (n, t) {
				n.find(".ui-select").comboBoxSetValue(t.value)
			}
		},
		datetime: {
			init: function () {
				return n('<div class="item_row" data-type="datetime" ><i class="fa fa-calendar"><\/i>日期框<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "日期框",
					type: "datetime",
					field: "",
					defaultValue: "",
					verify: "",
					dateformat: "date"
				},
                t[0].itemdata),
                u;
				t[0].itemdata = r;
				u = r.dateformat == "date" ? "年-月-日" : "年-月-日 时:分";
				t.html(i({
					name: r.label,
					text: u
				}))
			},
			property: function (t, i) {
				var o = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>选择日期、时间控件<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">日期格式<\/div>';
				u += '<div class="field_control"><select id="control_dateformat" class="form-control"><option value="date">仅日期<\/option><option value="datetime">日期和时间<\/option><\/select><\/div>';
				u += '<div class="field_title">字段验证<\/div>';
				u += '<div class="field_control"><div id="control_verify" type="select" class="ui-select"><\/div>';
				u += '<div class="field_title">默认值<i title="仅在添加数据时默认填入" class="help fa fa-question-circle"><\/i><\/div>';
				u += '<div class="field_control"><select id="control_default" class="form-control"><option value="">无则不填<\/option><option value="Yesterday">昨天<\/option><option value="Today">今天<\/option><option value="Tomorrow">明天<\/option><\/select><\/div>';
				o.html(u);
				f = i[0].itemdata;
				r(t, f);
				e(f);
				o.find("#control_label").val(f.label);
				o.find("#control_dateformat").val(f.dateformat);
				o.find("#control_default").val(f.defaultValue);
				o.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				});
				o.find("#control_dateformat").change(function () {
					var t = n(this).val();
					t == "date" ? i.find(".item_field_value").html("年-月-日") : t == "datetime" && i.find(".item_field_value").html("年-月-日 时:分");
					f.dateformat = t
				});
				o.find("#control_default").change(function () {
					var t = n(this).val();
					f.defaultValue = t
				})
			},
			renderTable: function (t) {
				var e = t.dateformat == "date" ? "yyyy-MM-dd" : "yyyy-MM-dd HH:mm",
                i = "",
                r = new Date,
                u;
				switch (t.defaultValue) {
					case "Yesterday":
						i = r.DateAdd("d", -1);
						break;
					case "Today":
						i = r.DateAdd("d", 0);
						break;
					case "Tomorrow":
						i = r.DateAdd("d", 1)
				}
				return i = formatDate(i, e.replace(/H/g, "h")),
                u = '<th class="formTitle">' + t.label + f(t.verify) + "<\/th>",
                u += '<td class="formValue custmerTd" data-type="' + t.type + '" data-value="' + t.field + '"   ><input value="' + i + '" id="' + t.id + '"  readonly  " onClick="WdatePicker({dateFmt:\'' + e + '\',qsEnabled:false,isShowClear:false,isShowOK:false,isShowToday:false})"  type="text" class="form-control input-datepicker"  ' + o(t.verify) + " /><\/td>",
                n(u)
			},
			validTable: function (n) {
				return n.Validform()
			},
			getValue: function (n) {
				var t = n.find("input");
				return {
					type: "datetime",
					value: t.val(),
					field: n.attr("data-value"),
					realValue: t.val()
				}
			},
			setValue: function (n, t) {
				var i = n.find("input");
				i.val(t.value)
			}
		},
		image: {
			init: function () {
				return n('<div class="item_row" data-type="image" ><i class="fa fa-photo"><\/i>图片<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "上传图片",
					type: "image",
					field: "",
					verify: "",
					fileformat: "jpg,gif,png,bmp"
				},
                t[0].itemdata);
				t[0].itemdata = r;
				t.html(i({
					name: r.label,
					text: "上传图片/" + r.fileformat
				}))
			},
			property: function (t, i) {
				var o = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>上传图片数据<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">字段验证<\/div>';
				u += '<div class="field_control"><div id="control_verify" type="select" class="ui-select"><\/div>';
				u += '<div class="field_title">图片格式<i title=".jpg .gif .png .bmp" class="help fa fa-question-circle"><\/i><\/div>';
				u += '<div class="field_control"><input id="control_fileformat" type="text" class="form-control" placeholder="如：jpg,gif,png,bmp"><\/div>';
				o.html(u);
				f = i[0].itemdata;
				r(t, f);
				e(f);
				o.find("#control_label").val(f.label);
				o.find("#control_fileformat").val(f.fileformat);
				o.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				});
				o.find("#control_fileformat").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_value").html("上传图片/" + t);
					f.fileformat = t
				})
			},
			renderTable: function (t) {
				var r = '<th class="formTitle">' + t.label + f(t.verify) + "<\/th>",
                i;
				return r += '<td class="formValue custmerTd" data-name="' + t.label + '"  data-type="' + t.type + '"  data-value="' + t.field + '"  data-verify="' + t.verify + '"  ><div id="' + t.id + '"><\/div><\/td>',
                i = n(r),
                i[0].callback = {
                	data: t,
                	fn: function () {
                		i.find("#" + t.id).uploadifyEx({
                			url: "/Utility/UploadifyFile?DataItemEncode=SaveFilePath&DataItemName=FormFilePath",
                			btnName: "添加图片",
                			type: "uploadify",
                			fileTypeExts: t.fileformat
                		})
                	}
                },
                i
			},
			makeCallback: function (t) {
				n.each(t,
                function (n, t) {
                	t.fn()
                })
			},
			validTable: function (n) {
				var u = n.attr("data-verify"),
                i,
                r;
				return u == "NotNull" ? (i = n.find(".uploadify").attr("data-value"), i != "" && i != undefined && i != null) ? !0 : (r = n.attr("data-name"), t.dialogTop({
					msg: r + "不能为空！",
					type: "error"
				}), !1) : !0
			},
			getValue: function (n) {
				var t = n.find(".uploadify").attr("data-value");
				return {
					type: "image",
					value: t,
					field: n.attr("data-value"),
					realValue: t
				}
			},
			setValue: function (n, t) {
				n.find(".uploadify").uploadifyExSet(t.value)
			}
		},
		upload: {
			init: function () {
				return n('<div class="item_row" data-type="upload" ><i class="fa fa-paperclip"><\/i>附件<\/div>')
			},
			render: function (t) {
				var r = n.extend({
					label: "上传文件",
					type: "upload",
					field: "",
					verify: "",
					fileformat: "doc,xls,ppt,pdf"
				},
                t[0].itemdata);
				t[0].itemdata = r;
				t.html(i({
					name: r.label,
					text: "上传文件/" + r.fileformat
				}))
			},
			property: function (t, i) {
				var o = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>上传文件数据<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">字段验证<\/div>';
				u += '<div class="field_control"><div id="control_verify" type="select" class="ui-select"><\/div>';
				u += '<div class="field_title">文件格式<i title=".doc .xls .ppt .pdf " class="help fa fa-question-circle"><\/i><\/div>';
				u += '<div class="field_control"><input id="control_fileformat" type="text" class="form-control" placeholder="如：doc,xls,ppt,pdf"><\/div>';
				o.html(u);
				f = i[0].itemdata;
				r(t, f);
				e(f);
				o.find("#control_label").val(f.label);
				o.find("#control_fileformat").val(f.fileformat);
				o.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				});
				o.find("#control_fileformat").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_value").html("上传文件/" + t);
					f.fileformat = t
				})
			},
			renderTable: function (t) {
				var r = '<th class="formTitle">' + t.label + f(t.verify) + "<\/th>",
                i;
				return r += '<td class="formValue custmerTd" data-name="' + t.label + '"  data-type="' + t.type + '"  data-value="' + t.field + '"  data-verify="' + t.verify + '"   ><div id="' + t.id + '"><\/div><\/td>',
                i = n(r),
                i[0].callback = {
                	data: t,
                	fn: function () {
                		i.find("#" + t.id).uploadifyEx({
                			url: "/Utility/UploadifyFile?DataItemEncode=SaveFilePath&DataItemName=FormFilePath",
                			btnName: "添加附件",
                			type: "uploadify",
                			fileTypeExts: t.fileformat
                		})
                	}
                },
                i
			},
			makeCallback: function (t) {
				n.each(t,
                function (n, t) {
                	t.fn()
                })
			},
			validTable: function (n) {
				var u = n.attr("data-verify"),
                i,
                r;
				return u == "NotNull" ? (i = n.find(".uploadify").attr("data-value"), i != "" && i != undefined && i != null) ? !0 : (r = n.attr("data-name"), t.dialogTop({
					msg: r + "不能为空！",
					type: "error"
				}), !1) : !0
			},
			getValue: function (n) {
				var t = n.find(".uploadify").attr("data-value");
				return {
					type: "upload",
					value: t,
					field: n.attr("data-value"),
					realValue: t
				}
			},
			setValue: function (n, t) {
				n.find(".uploadify").uploadifyExSet(t.value)
			}
		},
		baseSelect: {
			init: function () {
				return n('<div class="item_row" data-type="baseSelect" ><i  class="fa fa-coffee"><\/i>单位组织<\/div>')
			},
			render: function (t) {
				var u = n.extend({
					label: "单位组织",
					type: "baseSelect",
					field: "",
					verify: "",
					baseType: "user",
					relation: "",
					height: "100px"
				},
                t[0].itemdata),
                r;
				t[0].itemdata = u;
				r = "";
				switch (u.baseType) {
					case "user":
						r = "人员选择";
						break;
					case "department":
						r = "部门选择";
						break;
					case "organize":
						r = "公司选择";
						break;
					case "post":
						r = "岗位选择";
						break;
					case "job":
						r = "职位选择";
						break;
					case "role":
						r = "角色选择"
				}
				t.html(i({
					name: u.label,
					text: "单位组织/" + r
				}))
			},
			property: function (t, i) {
				var o = n(".field_option"),
                f = "",
                u,
                s;
				f += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>单位组织下拉选择框,支持级联<\/span><\/div>';
				f += '<div class="field_title">字段标识<\/div>';
				f += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				f += '<div class="field_title">字段说明<\/div>';
				f += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				f += '<div class="field_title">字段验证<\/div>';
				f += '<div class="field_control"><div id="control_verify" type="select" class="ui-select"><\/div>';
				f += '<div class="field_title">下拉高度<\/div>';
				f += '<div class="field_control"><input id="control_height" type="text" class="form-control" value="100px"><\/div>';
				f += '<div class="field_title">类型选择<\/div>';
				f += '<div class="field_control"><select id="control_baseType" class="form-control"><option value="user">人员选择<\/option><option value="department">部门选择<\/option><option value="organize">公司选择<\/option><option value="post">岗位选择<\/option><option value="job">职位选择<\/option><option value="role">角色选择<\/option><\/select><\/div>';
				f += '<div class="field_title">单位组织控件级联-上一级<\/div>';
				f += '<div class="field_control"><div id="control_relation" type="select" class="ui-select"><\/div><\/div>';
				o.html(f);
				u = i[0].itemdata;
				s = [];
				n.each(i[0].parentNode.childNodes,
                function (t, i) {
                	var r = n(i)[0].itemdata;
                	r.id != u.id && u.type == "baseSelect" && (u.baseType == "user" || u.baseType == "job" ? r.baseType == "department" && s.push(r) : u.baseType != "organize" && r.baseType == "organize" && s.push(r))
                });
				r(t, u);
				e(u);
				w(u, s);
				o.find("#control_label").val(u.label);
				o.find("#control_baseType").val(u.baseType);
				o.find("#control_height").val(u.height);
				o.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					u.label = t
				});
				o.find("#control_baseType").change(function () {
					var t = n(this).val(),
                    r = n(this).find('[value="' + t + '"]').text();
					u.baseType = t;
					i.find(".item_field_value").html("单位组织/" + r)
				});
				o.find("#control_height").change(function () {
					var t = n(this).val();
					u.height = t
				})
			},
			renderTable: function (t) {
				var u = '<th class="formTitle">' + t.label + f(t.verify) + "<\/th>",
                r,
                i;
				u += '<td class="formValue custmerTd" data-type="' + t.type + '"  data-value="' + t.field + '"  ><div id="' + t.id + '" type="select" class="ui-select"  ' + o(t.verify) + " ><\/div><\/td>";
				r = n(u);
				i = r.find("#" + t.id);
				r[0].callback = {
					data: t,
					fn: null
				};
				switch (t.baseType) {
					case "user":
						r[0].callback.fn = function (n) {
							var r = "";
							return !n || (r = "?departmentId=" + n),
							!t.relation || r != "" ? i.comboBox({
								url: "../../BaseManage/User/GetListJson" + r,
								id: "UserId",
								text: "RealName",
								title: "Account",
								maxHeight: t.height,
								allowSearch: !0
							}) : i.comboBox({
								allowSearch: !0
							})
						};
						break;
					case "department":
						r[0].callback.fn = function (n) {
							var r = "";
							return !n || (r = "?organizeId=" + n),
							!t.relation || r != "" ? i.comboBoxTree({
								url: "../../BaseManage/Department/GetTreeJson" + r,
								maxHeight: t.height,
								allowSearch: !0
							}) : i.comboBoxTree({
								allowSearch: !0
							})
						};
						break;
					case "organize":
						r[0].callback.fn = function () {
							return i.comboBoxTree({
								url: "../../BaseManage/Organize/GetTreeJson",
								maxHeight: t.height,
								allowSearch: !0
							})
						};
						break;
					case "post":
						r[0].callback.fn = function (n) {
							var r = "";
							return !n || (r = "?organizeId=" + n),
							!t.relation || r != "" ? i.comboBox({
								url: "../../BaseManage/Post/GetListJson" + r,
								id: "RoleId",
								text: "FullName",
								maxHeight: t.height,
								allowSearch: !0
							}) : i.comboBox({
								allowSearch: !0
							})
						};
						break;
					case "job":
						r[0].callback.fn = function (n) {
							var r = "";
							return !n || (r = "?organizeId=" + n),
							!t.relation || r != "" ? i.comboBox({
								url: "../../BaseManage/Job/GetListJson" + r,
								id: "RoleId",
								text: "FullName",
								maxHeight: t.height,
								allowSearch: !0
							}) : i.comboBox({
								allowSearch: !0
							})
						};
						break;
					case "role":
						r[0].callback.fn = function (n) {
							var r = "";
							return !n || (r = "?organizeId=" + n),
							!t.relation || r != "" ? i.comboBox({
								url: "../../BaseManage/Role/GetListJson" + r,
								id: "RoleId",
								text: "FullName",
								maxHeight: t.height,
								allowSearch: !0
							}) : i.comboBox({
								allowSearch: !0
							})
						}
				}
				return r
			},
			makeCallback: function (t) {
				var i = {};
				n.each(t,
                function (n, t) {
                	i[n] || (i[n] = {
                		obj: "",
                		fnlist: []
                	}); !t.data.relation || (i[t.data.relation] || (i[t.data.relation] = {
                		obj: "",
                		fnlist: []
                	}), i[t.data.relation].fnlist.push(t.fn));
                	i[n].obj = t.fn()
                });
				n.each(i,
                function (t, i) {
                	i.fnlist.length > 0 && i.obj.bind("change",
                    function () {
                    	var t = n(this).attr("data-value");
                    	n.each(i.fnlist,
                        function (n, i) {
                        	i(t)
                        })
                    })
                })
			},
			validTable: function (n) {
				return n.Validform()
			},
			getValue: function (n) {
				var t = n.find(".ui-select").attr("data-value"),
                i = n.find(".ui-select-text").text();
				return {
					type: "baseSelect",
					value: t,
					field: n.attr("data-value"),
					realValue: i
				}
			},
			setValue: function (n, t) {
				n.find(".ui-select").comboBoxSetValue(t.value)
			}
		},
		currentInfo: {
			init: function () {
				return n('<div class="item_row" data-type="currentInfo" ><i  class="fa fa-book"><\/i>当前信息<\/div>')
			},
			render: function (t) {
				var u = n.extend({
					label: "当前信息",
					type: "currentInfo",
					field: "",
					infoType: "user"
				},
                t[0].itemdata),
                r;
				t[0].itemdata = u;
				r = "";
				switch (u.infoType) {
					case "user":
						r = "当前用户";
						break;
					case "department":
						r = "当前部门";
						break;
					case "organize":
						r = "当前公司";
						break;
					case "date":
						r = "当前时间"
				}
				t.html(i({
					name: u.label,
					text: "当前信息/" + r
				}))
			},
			property: function (t, i) {
				var e = n(".field_option"),
                u = "",
                f;
				u += '<div class="field_tips"><i class="fa fa-info-circle"><\/i><span>显示当前操作信息<\/span><\/div>';
				u += '<div class="field_title">字段标识<\/div>';
				u += '<div class="field_control"><div id="control_field" type="select" class="ui-select"><\/div><\/div>';
				u += '<div class="field_title">字段说明<\/div>';
				u += '<div class="field_control"><input id="control_label" type="text" class="form-control" placeholder="必填项"><\/div>';
				u += '<div class="field_title">类型选择<\/div>';
				u += '<div class="field_control"><select id="control_infoType" class="form-control"><option value="user">当前用户<\/option><option value="department">当前部门<\/option><option value="organize">当前公司<\/option><option value="date">当前时间<\/option><\/select><\/div>';
				e.html(u);
				f = i[0].itemdata;
				r(t, f);
				e.find("#control_label").val(f.label);
				e.find("#control_infoType").val(f.infoType);
				e.find("#control_label").keyup(function () {
					var t = n(this).val();
					i.find(".item_field_label").find("span").html(t);
					f.label = t
				});
				e.find("#control_infoType").change(function () {
					var t = n(this).val(),
                    r = n(this).find('[value="' + t + '"]').text();
					f.infoType = t;
					i.find(".item_field_value").html("当前信息/" + r)
				})
			},
			renderTable: function (i) {
				var e = '<th class="formTitle">' + i.label + "<\/th>",
                f, r;
				if (e += '<td class="formValue custmerTd" data-type="' + i.type + '"  data-value="' + i.field + '"  data-infoType="' + i.infoType + '"  ><input id="' + i.id + '"  readonly type="text" class="form-control"  /><\/td>', f = n(e), r = f.find("input"), !u) t.getDataForm({
					type: "get",
					url: "../../Utility/getCurrentInfo",
					success: function (n) {
						u = n;
						switch (i.infoType) {
							case "user":
								r.val(n.userName);
								r.attr("data-value", n.userId);
								break;
							case "department":
							    r.val(top.clientdepartmentData[n.departmentId].FullName);
								r.attr("data-value", n.departmentId);
								break;
							case "organize":
							    r.val(top.clientorganizeData[n.companyId].FullName);
								r.attr("data-value", n.companyId);
								break;
							case "date":
								r.val(n.time)
						}
					}
				});
				else switch (i.infoType) {
					case "user":
						r.val(u.userName);
						r.attr("data-value", u.userId);
						break;
					case "department":
					    r.val(top.clientdepartmentData[u.departmentId].FullName);
						r.attr("data-value", u.departmentId);
						break;
					case "organize":
					    r.val(top.clientorganizeData[u.companyId].FullName);
						r.attr("data-value", u.companyId);
						break;
					case "date":
						r.val(u.time)
				}
				return f
			},
			validTable: function () {
				return !0
			},
			getValue: function (n) {
				var t = n.find("input"),
                i = {
                	type: "currentInfo",
                	value: t.attr("data-value"),
                	field: n.attr("data-value"),
                	infoType: n.attr("data-infoType"),
                	realValue: t.val()
                };
				return i.infoType == "date" && (i.value = t.val()),
                i
			},
			setValue: function (n, t) {
				var i = n.find("input");
				switch (t.infoType) {
					case "user":
					    i.val(top.clientuserData[t.value].RealName);
						i.attr("data-value", t.value);
						break;
					case "department":
					    i.val(top.clientdepartmentData[t.departmentId].FullName);
						i.attr("data-value", t.value);
						break;
					case "organize":
					    i.val(top.clientorganizeData[t.companyId].FullName);
						i.attr("data-value", t.value);
						break;
					case "date":
						i.val(t.value)
				}
			}
		}
	}
})(window.jQuery, window.learun)