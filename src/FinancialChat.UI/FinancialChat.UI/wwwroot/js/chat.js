"use strict";

var chatroomId = document.getElementById("RoomId").value;
var connection = new signalR.HubConnectionBuilder().withUrl(`/chatHub?chatroomId=${chatroomId}`).build();

document.getElementById("sendButton").disabled = true;

connection.on(`chatroom${chatroomId}`, function (user) {
    var radiobox = document.createElement('input');
    radiobox.type = 'radio';
    radiobox.id = user;
    radiobox.name = "usersonline";
    radiobox.value = user;

    var label = document.createElement('label')
    label.htmlFor = user;

    var description = document.createTextNode(user);
    label.appendChild(description);

    var newline = document.createElement('br');
    var users = document.getElementById("users");
    users.appendChild(radiobox);
    users.appendChild(label);
    users.appendChild(newline);

});

connection.on(`message${chatroomId}`, function (message) {
    var newRow = document.createElement("tr");
    var newCell = document.createElement("td");
    message = JSON.parse(message);

    var user = message.from;
    var msg = message.message;
    var date = message.date;
    var to = message.to;

    var milliseconds = date * 1000;
    var dateObject = new Date(milliseconds);
    var dateFormat = dateObject.toLocaleString();

    newCell.innerHTML = `${dateFormat} - <strong>${user}</strong> says to <strong>${to}</strong> -  ${msg}`;

    newRow.append(newCell);
    document.getElementById("rows").appendChild(newRow);
    document.getElementById("messageInput").value = ''

    var table = document.getElementById("messagesTable");
    var tbodyCount = table.tBodies[0].rows.length;
    if (tbodyCount > 50) {
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

    var ele = document.getElementsByName('usersonline');

    for (var i = 0; i < ele.length; i++) {
        if (ele[i].checked) {
            var userTo = ele[i].value;
            break;
        }
    }

    connection.invoke("SendMessage", message, chatroomId,userTo, null).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});