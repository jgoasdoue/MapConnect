/* Fonction qui ouvre une popup au clic sur le marqueur */
function onPopupClick(e) {
    this.bindPopup("<b>Solutec</b><br/>Coordinates : (" + e.latlng.lat.toFixed(4) + ", " + e.latlng.lng.toFixed(4) + ")");
}

/* Fonction qui ajoute une popup au clic sur la carte */
function onMapClick(e) {
    var popup = L.popup();
    popup.setLatLng(e.latlng).setContent("You clicked the map at " + e.latlng.toString()).openOn(map);
}

function onGSLClick(e) {
    var popup = L.popup();
    this.bindPopup("<b>Gare Saint Lazare</b><br/>Coordinates : (" + e.latlng.lat.toFixed(4) + ", " + e.latlng.lng.toFixed(4) + ")");
}

/* On initialiser la carte */
var map = L.map('map', {
    center: [48.853, 2.35],
    zoom: 13
});

map.addEventListener("click", onMapClick);

/* On ajoute les tuiles qui doivent être affichées sur la carte */
L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a>'
}).addTo(map);

/* On ajoute un marqueur sur la carte */
var solutec = L.marker([48.87430545931439, 2.32435405254364]).addTo(map);
solutec.addEventListener("click", onPopupClick);

var gareSL = L.polygon([
    [48.87585, 2.32394],
    [48.87718, 2.32323],
    [48.87807, 2.32532],
    [48.87796, 2.32549],
    [48.87747, 2.32595],
    [48.87746, 2.32591],
    [48.87668, 2.32668],
    [48.877, 2.32669],
    [48.877, 2.32683],
    [48.8762, 2.32679]
]).addTo(map);

gareSL.addEventListener("click", onGSLClick);