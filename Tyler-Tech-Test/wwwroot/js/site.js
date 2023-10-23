// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function() {
    BindButtons();
    BindDropdown();
});

function BindButtons() {
    $("#add-button").click(function () {
        $(location).attr("href", "./add-employee");
    });

    $("#save-button").click(function () {
        var url = "./add-employee/add"

        var manager = $("#manager-dropdown").val();
        var employeeID = $("#employee-id").val();
        var firstName = $("#first-name").val();
        var lastName = $("#last-name").val();
        var roles = $(".role .selected").map(function () {
            return this.attr("dbid");
        }).get();

        var data = {
            Manager: manager,
            EmployeeID: employeeID,
            FirstName: firstName,
            LastName: LastName,
            Roles: roles
        };

        Postback(url,
            data,
            function () {
                $("#cancel-button").trigger("click");
            },
            FailueAlert);
    });

    $("#cancel-button").click(function () {
        $(location).attr("href", "./subordinate-viewer");
    });
}

function BindDropdown() {
    $("#manager-dropdown").change(function () {
        var postbackUrl = "./subordinate-viewer/get";
        var url = window.location.href;

        if (url.indexOf("subordinate-viewer") > -1) {
            var manager = $("#manager-dropdown").val();
            var data = { Manager: manager };

            Postback(postbackUrl, data, nothing, FailueAlert);
        }
    });
}

function Postback(url, data, success, failure) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json",
        dataType: "json",
        success: success,
        failure: failure
    });
}

function FailueAlert() {
    alert("Operation Failed!");
}