var clientCount = 1000;
var API_ENDPOINT = 'https://demo.dot-net.in.ua/api/';
    //API_ENDPOINT = 'http://localhost:5000/api/';

/**
 * Create new GUID
 */
function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}