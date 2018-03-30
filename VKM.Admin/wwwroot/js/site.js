﻿// Write your JavaScript code.

$(function () {
    $('#BaseTree').jstree(
        {
            "types": {
                "team": {
                    "icon": "fas fa-users",
                    "valid_children": ["student"]
                },
                "student": {
                    "icon": "fas fa-user",
                    "valid_children": "none"
                }
            },
            "conditionalselect": function (node, event) {
                return node.type !== "team";
            },
            "plugins": ["conditionalselect", "sort", "wholerow", "unique", "types"]
        }
    );
});
$('#BaseTree')
    .on('changed.jstree', function (e, data) {
        $('#SelectedStudent').html(data.instance.get_node(data.selected[0]).text);

        function onGetStudent() {
            $('#CurrentStudentLastName').text(this.LastName)
        }

        var item = data.node.data.jstree.id;
        $.getJSON("/Home/Student", item, onGetStudent)
    });
