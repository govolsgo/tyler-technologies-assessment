// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function() {
    BindButtons();
    BindDropdown();
    BindSelectableList();
});

function BindButtons() {
    $("#add-button").click(function () {
        $(location).attr("href", "./add-employee");
    });

    $("#save-button").click(function () {
        var url = "/add-employee?handler=AddNewEmployee"

        var manager = $("#manager-dropdown").val();
        var employeeID = $("#employee-id").val();
        var firstName = $("#first-name").val();
        var lastName = $("#last-name").val();
        var roles = $(".role .selected").map(function () {
            return $(this).text();
        }).get();

        var data = {
            Manager: manager,
            EmployeeID: employeeID,
            FirstName: firstName,
            LastName: lastName,
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
        var postbackUrl = "/subordinate-viewer?handler=GetSubordinates"
        var url = window.location.href;

        if (url.indexOf("subordinate-viewer") > -1) {
            var manager = $("#manager-dropdown").val();
            var data = { Manager: manager };

            Postback(postbackUrl, data, PopulateEmployeeTable, FailueAlert);
        }
    });
}

function BindSelectableList() {
    $("#selectable-list .role").click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");
        }
        else {
            $(this).addClass("selected");
        }
    });
}

function PopulateEmployeeTable(data) {
    var employees = JSON.parse(data.data);

    $("#employee-table tr").remove();

    employees.forEach(function (employee) {
        $("#employee-table tbody").append("<tr><td>" + employee.EmployeeID + "</td><td>" + employee.LastName + "</td><td>" + employee.FirstName + "</td></tr>")
    });
}

function Postback(url, data, success, failure) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        //contentType: "application/json",
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("RequestVerificationToken", $("input:hidden[name='__RequestVerificationToken']").val())
        },
        success: success,
        failure: failure,
        error: failure
    });
}

function FailueAlert() {
    alert("Operation Failed!");
}