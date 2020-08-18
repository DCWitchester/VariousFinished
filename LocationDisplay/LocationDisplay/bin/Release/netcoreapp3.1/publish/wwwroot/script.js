
function initialize() {
    var latlng = new google.maps.LatLng(44.18989707, 28.62403504);
    var options = {
        zoom: 19, center: latlng,
        mapTypeId: google.maps.MapTypeId.SATTELLITE
    };
    var map = new google.maps.Map(document.getElementById("map"), options);
    new google.maps.Marker({
        position: latlng,
        map
    });
}
function loadMap(lat,lang){
    var latlng = new google.maps.LatLng(lat, lang);
    var options = {
        zoom: 19, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), options);
    new google.maps.Marker({
        position: latlng,
        map
    });
}

function setMarker(lat,lang)
{
    var latlng = new google.maps.LatLng(lat, lang);
    new google.maps.Marker({
        position: latlng,
        map
    });
}