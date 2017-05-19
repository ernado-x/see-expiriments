var _channel = new Channel(API_ENDPOINT + 'sse/');
_channel.init();

var _eventCount = 0;

document.getElementById("clean").addEventListener("click", function () {
    _eventCount = 0;

    document.getElementById("event-count").innerText = _eventCount;
    document.getElementById("out").innerHTML = '';
});

document.getElementById("create-clients").addEventListener("click", function () {

    for (var i = 1; i <= clientCount; i++) {

        var client = new Client(i, function (data) {
            _eventCount++;
            console.log('[' + getNowDate() + ']Received data: ' + data);
            document.getElementById("event-count").innerText = _eventCount;
        });

        _channel.subscribe(client);
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
