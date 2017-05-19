/**
 * Initialize new channel. It should be single for page.
 */
function Channel(endpoint) {

    var self = this;

    self.id = guid();

    self.apiEndpoint = endpoint;

    self.eventSource = null;

    /**
     * 
     */
    self.subscribe = function (client) {

        self.eventSource.addEventListener(client.id, function (e) {
            var obj = JSON.parse(e.data);
            client.callback(obj);
        }, false);

        var url = this.apiEndpoint + 'subscribe?clientId=' + client.id + '&channelId=' + this.id;

        httpGetAsync(url, function () {
            console.info('Client[' + client.id + '] subscribed to events on channel ' + self.id);
        });
    };


    self.init = function () {

        self.eventSource = new EventSource(self.apiEndpoint + '?channelId=' + self.id);

        self.eventSource.onmessage = function (obj) {
            console.log('onmessage [' + obj + ']');
        };

        self.eventSource.onopen = function (obj) {
            console.log('onopen [' + obj + ']');
        };

        self.eventSource.onerror = function (obj) {
            console.log('onerror [' + obj + ']');

            if (self.eventSource.readyState == 2) {
                console.log('try reconnect...');
                setTimeout(self.init, 5000);
            }
        };
    }
}