var pointCenterSearch;

/* Définit la latitude et la longitude d'un point */
var setLatLong = function () {
    //return new L.LatLng($("#lat").val(), $("#lng").val());
    return new L.LatLng(48.853, 2.35);
};

/* On initialiser la carte */
var map = L.map("map", {
    center: setLatLong(),
    zoom: 13
});

/* Fonction qui ouvre une popup au clic sur le marqueur */
var onPopupClick = function (e) {
    this.bindPopup("<b>Solutec</b><br/>Coordinates : (" + e.latlng.lat.toFixed(4) + ", " + e.latlng.lng.toFixed(4) + ")");
};

/* Fonction qui ajoute une popup au clic sur la géométrie de Gare Saint Lazare */
var onGSLClick = function (e) {
    this.bindPopup("<b>Gare Saint Lazare</b><br/>Coordinates : (" + e.latlng.lat.toFixed(4) + ", " + e.latlng.lng.toFixed(4) + ")");
};

/* Fonction qui centre la carte sur la localisation sélectionnée parmi les résultats d'une recherche */
var centerOnResult = function (e) {
    if (map.hasLayer(pointCenterSearch)) {
        map.removeLayer(pointCenterSearch);
    }
    var options = {
        radius: 3,
        opacity: 0.8,
        fillOpacity: 0.8,
        fillColor: "#00ffff",
        color: "#00ffff"
    };
    pointCenterSearch = L.circleMarker(e.geocode.center, options).addTo(map);
    map.fitBounds(e.geocode.bbox);
};

/* On ajoute les tuiles qui doivent être affichées sur la carte */
L.tileLayer("http://{s}.tile.osm.org/{z}/{x}/{y}.png", {
    attribution: "Map data &copy; <a href=\"http://openstreetmap.org\">OpenStreetMap</a>"
}).addTo(map);

/* On ajoute un marqueur sur la carte */
var solutec = L.marker([48.8742713, 2.324352]).addTo(map);
solutec.addEventListener("click", onPopupClick);

/* On crée un objet Gare Saint-Lazare sur la carte */
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

/* Fonction qui ajoute une popup au clic sur la carte */
var onMapClick = function (e) {
    var popup = L.popup();
    popup.setLatLng(e.latlng).setContent("You clicked the map at (" + e.latlng.lat.toFixed(4) + ", " + e.latlng.lng.toFixed(4) + ")").openOn(map);
};

map.addEventListener("click", onMapClick);

var geocoder = L.Control.geocoder({
    defaultMarkGeocode: false
});
geocoder.on("markgeocode", centerOnResult).addTo(map);

var routingModule = L.Routing.control({
    reverseWaypoints: false,
    useZoomParameter: false,
    showAlternatives: true,
    fitSelectedRoutes: false,
    routeWhileDragging: true,
    geocoder: L.Control.Geocoder.nominatim()
});
routingModule.addTo(map);

/* Positionne le centre de la carte grâce au point créé par setLatLong() */
/*function position() {
    map.panTo(setLatLong());
}*/

/* Met à jour la valeur des spinners quand on se déplace sur la carte */
/*function setSpinnerCoords(e) {
    $("#lat").spinner('value', e.target.getCenter().lat);
    $("#lng").spinner('value', e.target.getCenter().lng);
}*/

/* Met à jour la valeur du slider au zoom sur la carte */
/*function setSliderZoom() {
    $("#slider-zoom").slider('value', map.getZoom());
    setStep(map.getZoom());
}*/

/* Centre la carte sur l'endroit de la recherche et y met un point */

/* On initialise des spinners pour les champs de latitude et longitude */
/*$("#lat").spinner({
    step: 0.015,
    spin: position,
    stop: position,
    max: 85.051,
    min: -85.051
});

$("#lng").spinner({
    step: 0.015,
    spin: position,
    stop: position,
    max: 170,
    min: -170
});*/

/* On crée le slider de zoom */
/*$("#slider-zoom").slider({
    min: 0,
    max: 18,
    value: 13,
    slide: function (event, ui) {
        map.setZoom(ui.value);
        setStep(ui.value);
    }
});*/

/* On définit la valeur du pas dans les spinners de latitude et longitude */
/*function setStep(val) {
    allSteps = [2, 1.429, 1.02, 0.729, 0.521, 0.372, 0.266, 0.19, 0.136, 0.097, 0.069, 0.049, 0.035, 0.025, 0.018, 0.013, 0.009, 0.007, 0.005];
    $("#lat, #lng").spinner("option", "step", allSteps[val]);
}*/

/* On désactive la modification au clavier des textes dans les spinners */
/*$("#lng, #lat").bind("keydown", function (event) {
    event.preventDefault();
});*/

/* On réalise une action au clic, au zoom et au déplacement sur la carte */

/*map.addEventListener("moveend", setSpinnerCoords)
map.addEventListener("zoomend", setSliderZoom)*/

/* On ajoute un module de recherche à la carte */
