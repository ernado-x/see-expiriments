var _channel = new Channel(API_ENDPOINT + 'sse/');
_channel.init();

var eventCount = 0;

document.getElementById("clean").addEventListener("click", function () {
    var out = document.getElementById("out");
    out.innerHTML = '';
});

document.getElementById("create-clients").addEventListener("click", function () {

    for (var i = 1; i <= clientCount; i++) {

        var client = new Client(i, function (data) {
            eventCount++;
            console.log('[' + getNowDate() + ']Received data: ' + data);
            document.getElementById("event-count").innerText = eventCount;
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
