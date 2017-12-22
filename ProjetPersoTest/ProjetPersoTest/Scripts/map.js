/* Fonction qui ouvre une popup au clic sur le marqueur */
function onPopupClick(e) {
    this.bindPopup("<b>Solutec</b><br/>Coordinates : " + e.latlng).openPopup()
}

/* On initialiser la carte */
var map = L.map('map', {
    center: [48.853, 2.35],
    zoom: 13
});

/* On ajoute les tuiles qui doivent être affichées sur la carte */
L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a>'
}).addTo(map);

/* On ajoute un marqueur sur la carte */
var marker = L.marker([48.87430545931439, 2.32435405254364]).addTo(map).on("click", onPopupClick);