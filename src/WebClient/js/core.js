var _channel = new Channel();
_channel.init();

document.getElementById("create-clients").addEventListener("click", function () {

    for (var i = 1; i <= clientCount; i++) {

        var client = {
            id: i,
            callback: function (data) {
                console.log('Client[' + this.id + '] received data: ' + data);
            }
        };

        _channel.subscribe(client.id, client.callback);
    }

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
