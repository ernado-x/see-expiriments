var _channel = new Channel(API_ENDPOINT + 'sse/');
_channel.init();

var eventCount = 0;

document.getElementById("create-clients").addEventListener("click", function () {
    var out = document.getElementById("out");
    out.innerHTML = '';
});

document.getElementById("create-clients").addEventListener("click", function () {

    for (var i = 1; i <= clientCount; i++) {

        var client = {
            id: i,
            callback: function (data) {
                eventCount++;
                document.getElementById("event-count").innerText = eventCount;
                //var out = document.getElementById("out");
                //out.innerHTML = out.innerHTML + '<p> Client[' + this.id + '] received data: ' + data + '</p>';
            }
        };

        var onClientSubscribe = function () {
            console.log('Client[' + client.id + '] subscribed to events on channel ' + _channel.id);
        }
        _channel.subscribe(client.id, client.callback, onClientSubscribe);

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
