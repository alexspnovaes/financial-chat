"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var chatroomId = document.getElementById("RoomId").value;

document.getElementById("sendButton").disabled = true;

connection.on(`message${chatroomId}`, function (message) {
    var newRow = document.createElement("tr");
    var newCell = document.createElement("td");
    message = JSON.parse(message);

    var user = message.from;
    var msg = message.message;
    var date = message.date;

    newCell.innerHTML = `${date} - ${user} says ${msg}`;

    newRow.append(newCell);
    document.getElementById("rows").appendChild(newRow);
    document.getElementById("messageInput").value = ''

    var table = document.getElementById("messagesTable");
    var tbodyCount = table.tBodies[0].rows.length;
    if (tbodyCount > 10) {
        table.deleteRow(1);
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message, chatroomId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});