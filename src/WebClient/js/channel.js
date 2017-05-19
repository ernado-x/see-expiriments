/**
 * Initialize new channel. It should be single for page.
 */
function Channel(endpoint) {

    var self = this;

    self.id = null;

    self.apiEndpoint = endpoint;

    self.subscribes = [];

    self.eventSource = null;

    /**
     * 
     */
    self.subscribe = function (client) {
        self.subscribes.push(client);

        var url = this.apiEndpoint + 'subscribe?clientId=' + client.id + '&channelId=' + this.id;
        
        httpGetAsync(url, function () {
            console.info('Client[' + client.id + '] subscribed to events on channel ' + self.id);
        });
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