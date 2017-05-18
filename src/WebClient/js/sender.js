
document.getElementById("send-to-all-clients").addEventListener("click", function () {
    var message = document.getElementById("message").value;

    for (var index = 1; index <= clientCount; index++) {
        var url = 'http://localhost:5000/api/messages/send?clientId=' + index + '&message=' + message;
        httpGetAsync(url);
    }
});

document.getElementById("send-to-client").addEventListener("click", function () {
    var message = document.getElementById("message").value;
    var clientId = document.getElementById("client-id").value;
    var url = 'http://localhost:5000/api/messages/send?clientId=' + clientId + '&message=' + message;
    httpGetAsync(url);

});

function httpGetAsync(theUrl, callback) {
    var xmlHttp = new XMLHttpRequest();

    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200 && !!callback) {
            callback(xmlHttp.responseText);
        }
    }

    xmlHttp.open("GET", theUrl, true); // true for asynchronous 
    xmlHttp.send(null);
}
