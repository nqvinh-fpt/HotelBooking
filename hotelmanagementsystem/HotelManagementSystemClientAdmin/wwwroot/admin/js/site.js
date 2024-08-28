"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalRServer")
    .build();

connection.on("LoadServices", function () {
    location.reload.href = '/Service/Index'
    location.reload.href = '/Service'
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});