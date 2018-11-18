var homeImg = document.getElementById("icon_home");
var logoutImg = document.getElementById("icon_logout");
var aboutImg = document.getElementById("icon_about");
var mapImg = document.getElementById("icon_map");

//Set listeners to home image
homeImg.addEventListener("mouseover", function () {
    this.src = "../../img/home_pressed.png";
    this.title = "Accueil";
});
homeImg.addEventListener("mouseout", function () {
    this.src = "../../img/home.png";
});

//Set listeners to logout image
logoutImg.addEventListener("mouseover", function () {
    this.src = "../../img/logout_pressed.png";
    this.title = "Déconnexion";
});
logoutImg.addEventListener("mouseout", function () {
    this.src = "../../img/logout.png";
});

//Set listeners to about image
aboutImg.addEventListener("mouseover", function () {
    this.src = "../../img/about_pressed.png";
    this.title = "A propos";
});
aboutImg.addEventListener("mouseout", function () {
    this.src = "../../img/about.png";
});

//Set listeners to map image
mapImg.addEventListener("mouseover", function () {
    this.src = "../../img/map_pressed.png";
    this.title = "Carte";
});
mapImg.addEventListener("mouseout", function () {
    this.src = "../../img/map.png";
});
