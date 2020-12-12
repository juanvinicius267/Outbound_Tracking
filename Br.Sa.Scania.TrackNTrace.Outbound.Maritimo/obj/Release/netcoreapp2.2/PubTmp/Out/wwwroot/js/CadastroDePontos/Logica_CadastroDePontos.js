;(function(){
 var placeCoordinates = [];
        var model;
        var modelString;
        var firstTimeRequested = 0;
        var urlBase = "http://10.251.16.112/TrackNTrace";



        var mapMoreInfo = new ol.Map({
            //overlays: [overlay]
        });
        mapMoreInfo.setTarget('mapMoreInfo');
        var view = new ol.View({
            projection: 'EPSG:4326',
            center: [0, 0],
            zoom: 0,
            maxResolution: 0.703125
        })
        mapMoreInfo.setView(view);
        var tile_layer = new ol.layer.Tile({
            source: new ol.source.OSM({ layer: 'osm' })
        })
        // Inicia o Mapa em uma posição setada
        mapMoreInfo.addLayer(tile_layer);
        mapMoreInfo.getView().setZoom(1.0);

        function chamadaAjax(value) {
            $.ajax({
                type: 'POST',
                url: urlBase + "/api/CadastroDePontos/SetPortDestination",
                contentType: "application/json",
                data: modelString,
                success: function (result, status, request) {
                    $("html").removeClass("loading");
                },
                error: function (request, status, erro) {
                    if (erro == "Internal Server Error") {
                        console.log("Problema ocorrido: " + status + "\nDescição: " + erro);
                        window.location.href = urlBase + "/Error/Index";
                    }
                    if (erro == "Not Found") {
                        console.log("Problema ocorrido: " + status + "\nDescição: " + erro);
                        window.location.href = urlBase + "/Error/Index";
                    }
                },

            }).done(function () {
                document.location.reload(true);
            }
            );

        }

        function chamadaAjax2() {
            $.ajax({
                type: 'GET',
                url: urlBase +"/api/CadastroDePontos/ReturnPortinDB",
                success: function (result, status, request) {
                    $("html").removeClass("loading");
                },
                error: function (request, status, erro) {
                    if (erro == "Internal Server Error") {
                        console.log("Problema ocorrido: " + status + "\nDescição: " + erro);
                        window.location.href = urlBase + "/Error/Index";
                    }
                    if (erro == "Not Found") {
                        console.log("Problema ocorrido: " + status + "\nDescição: " + erro);
                        window.location.href = urlBase + "/Error/Index";
                    }
                },

            }).done(function (data) {
                console.log(data);
                for (var i = 0; i < data.length; i++) {

                    var photo = data[i].country;//.slice(0, 2);
                    photo = photo.toLowerCase();
                    if (photo.search(" ")) {
                        console.log("aqui");
                        photo = photo.replace(" ", "-");
                    }

                    placeCoordinates[i] = [data[i].longitude, data[i].latitude];
                    addMarkerPlace(placeCoordinates[i], data[i].portName);
                    $("#tableItems").append(
                        "<tr class='style-scope' >" +
                        "<td>"
                        + data[i].portName +
                        "</td>"
                        + "<td>"
                        + data[i].longitude +
                        "</td>"
                        +
                        "<td>"
                        + data[i].latitude +
                        "</td>"
                        +
                        "<td>"
                        + "<img src='https://cdn.countryflags.com/thumbs/" + photo + "/flag-800.png' style='width:30px; height:20px' />"   +" - " + data[i].country +
                        "</td>" +
                        
                        + "</tr>"

                    );
                    console.log(placeCoordinates[i]);

                }

            }
            );

        }

    $("#document").ready(function () {
            chamadaAjax2();
           
            window.setInterval(function () {
                if (firstTimeRequested === 0) {
                    $("#buttonSend").on('click', function () {
                        if ($('#portName').val() == "" || $('#portName').val() == " ") {
                            $('#portName').css("border-color", "red")
                            $('#portName').css("background-color", "rgba(255, 0, 0, 0.2)")                           
                        } else {
                            $('#portName').css("border-color", "gray")
                            $('#portName').css("background-color", "rgba(0, 0, 0, 0)")
                        }
                        if ($('#longitude').val() == "" || $('#longitude').val() == " ") {
                            $('#longitude').css("border-color", "red")
                            $('#longitude').css("background-color", "rgba(255, 0, 0, 0.2)")    
                        } else {
                            $('#longitude').css("border-color", "gray")
                            $('#longitude').css("background-color", "rgba(0, 0, 0, 0)")
                        }
                        if ($('#latitutde').val() == "" || $('#latitutde').val() == " ") {
                            $('#latitutde').css("border-color", "red")
                            $('#latitutde').css("background-color", "rgba(255, 0, 0, 0.2)")    
                        } else {
                            $('#latitutde').css("border-color", "gray")
                            $('#latitutde').css("background-color", "rgba(0, 0, 0, 0)")
                        }
                        if ($('#country').val() == "" ) {
                            $('#country').css("border-color", "red")
                            $('#country').css("background-color", "rgba(255, 0, 0, 0.2)")    
                        } else {
                            $('#country').css("border-color", "gray")
                            $('#country').css("background-color", "rgba(0, 0, 0, 0)")
                        }               
                        if (($('#portName').val() != "" && $('#portName').val() != " ") && ($('#longitude').val() != "" && $('#longitude').val() != " ") && ($('#latitutde').val() != "" && $('#latitutde').val() != " ") && ($('#country').val() != "" && $('#country').val() != " ")) {

                            model = {
                                portName: $('#portName').val(),
                                longitude: $('#longitude').val(),
                                latitutde: $('#latitutde').val(),
                                country: $('#country').val()
                            }
                            modelString = JSON.stringify(model);

                            chamadaAjax(modelString);
                        }
                       

                    })
                    firstTimeRequested++;
                }
                
            }, 3000)
        })



        function addMarkerPlace(placeCoordinates, nameport) {

            var image;
            var scale;
            var iconScaniaGeometry = new ol.geom.Point(ol.proj.fromLonLat(placeCoordinates, 'EPSG:4326', 'EPSG:3857'));
            var iconScaniaFeature = new ol.Feature({
                geometry: iconScaniaGeometry,

            });
            if (nameport == 'SCANIA BRASIL') {
                image = 'https://i.imgur.com/uBbqFbH.png';
                scale = 0.15;
            }
            else {
                image = 'https://i.imgur.com/tGBJdEV.png';
                scale = 0.1;
            }

            var iconScaniakStyle = new ol.style.Style({
                image: new ol.style.Icon(({
                    //anchor:[0.5, 1],
                    scale: scale,
                    opacity: 0.9,
                    src: image
                }))
            });


            iconScaniaFeature.setStyle(iconScaniakStyle);

            var vectorSource = new ol.source.Vector({
                features: [iconScaniaFeature]
            });




            markerVectorLayer = new ol.layer.Vector({
                source: vectorSource,
            });

            mapMoreInfo.addLayer(markerVectorLayer);

        }

})()