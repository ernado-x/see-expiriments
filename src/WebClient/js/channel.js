/**
 * Initialize new channel. It should be single for page.
 */
function Channel() {

    var self = this;

    self.id = null;

    self.apiEndpoint = 'http://localhost:5000/api/sse/';

    self.subscribes = [];

    self.eventSource = null;

    self.subscribe = function (id, callback) {
        var client = new Client(id, callback);

        self.subscribes.push(client);

        var url = this.apiEndpoint + 'subscribe?clientId=' + client.id + '&channelId=' + this.id;
        httpGetAsync(url);
    };


    self.init = function () {
        self.id = guid();
        self.eventSource = new EventSource(self.apiEndpoint + '?channelId=' + self.id);

        self.eventSource.onmessage = function (obj) {

            var event = JSON.parse(obj.data);

            var clients = self.subscribes.filter(function (c) { return c.id == event.clientId; });

            clients.forEach(function (client) {
                client.callback(event.data);
            });
        };

        self.eventSource.onopen = function (obj) {
            console.log('onopen [' + obj + ']');
        };

        self.eventSource.onerror = function (obj) {
            console.log('onerror [' + obj + ']');
        };
    }
}