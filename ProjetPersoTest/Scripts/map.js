/* Fonction qui ouvre une popup au clic sur le marqueur */
function onPopupClick(e) {
    this.bindPopup("<b>Solutec</b><br/>Coordinates : (" + e.latlng.lat.toFixed(4) + ", " + e.latlng.lng.toFixed(4) + ")");
}

/* Fonction qui ajoute une popup au clic sur la carte */
function onMapClick(e) {
    var popup = L.popup();
    popup.setLatLng(e.latlng).setContent("You clicked the map at " + e.latlng.toString()).openOn(map);
}

/* Fonction qui ajoute une popup au clic sur la géométrie de Gare Saint Lazare */
function onGSLClick(e) {
    var popup = L.popup();
    this.bindPopup("<b>Gare Saint Lazare</b><br/>Coordinates : (" + e.latlng.lat.toFixed(4) + ", " + e.latlng.lng.toFixed(4) + ")");
}

/* Définit la latitude et la longitude d'un point */
function setLatLong() {
    return new L.LatLng($("#lat").val(), $("#lng").val());
}

/* Positionne le centre de la carte grâce au point créé par setLatLong() */
function position() {
    map.panTo(setLatLong());
}

/* Mets à jour la valeur des spinners quand on se déplace sur la carte */
function setSpinnerCoords(e) {
    $("#lat").spinner('value', e.target.getCenter().lat);
    $("#lng").spinner('value', e.target.getCenter().lng);
}

/* Mets à jour la valeur du slider au zoom sur la carte */
function setSliderZoom() {
    $("#slider-zoom").slider('value', map.getZoom());
}

/* Initialise des spinners pour les champs de latitude et longitude */
$("#lat, #lng").spinner({
    step: .001,
    change: position,
    stop: position
});

/* On initialiser la carte */
var map = L.map('map', {
    center: setLatLong(),
    zoom: 13,
});

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

$("#slider-zoom").slider({
    min: 0,
    max: 18,
    value: 13,
    slide: function (event, ui) {
        map.setZoom(ui.value);
    }
});

map.addEventListener("click", onMapClick);
map.addEventListener("move", setSpinnerCoords)
map.addEventListener("zoomend", setSliderZoom)