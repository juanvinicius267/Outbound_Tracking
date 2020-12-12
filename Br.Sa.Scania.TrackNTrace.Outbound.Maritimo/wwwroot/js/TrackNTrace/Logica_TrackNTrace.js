; (function () {
    var listTruckOnBoardData = [];
    var listData = [];
    var markerVectorLayer;
    var Mmsi = [];
    var Lat = [];
    var Lon = [];
    var listDataVessel = null;
    var listDataBatch = null;
    var firstCycle = 0;
    var firstCycle2 = 0;
    var totalBar = 0;
    var greenBar = 0;
    var redBar = 0;
    var orangeBar = 0;
    var coordOld = [];
    var urlBase = "http://10.251.16.112/TrackNTrace";
    var urlBaseImage = "http://10.251.16.112/mes/Images/";// "http://tracker-tracer.s3-eu-west-1.amazonaws.com/";//"http://10.251.16.112/mes/Images/";

    var checkListArray = ['OUTCHIL', 'OUTARG', 'OUTBOLI', 'OUTPERU'];



    function formatDate(dateTime) {
        if (dateTime == "1900-01-01T00:00:00" || dateTime == null) {
            return null;
        } else {
            var date = new Date(dateTime);

            var hours = date.getHours();
            var minutes = date.getMinutes();
            var ampm = hours >= 12 ? 'pm' : 'am';
            hours = hours % 12;
            hours = hours ? hours : 12; // the hour '0' should be '12'
            minutes = minutes < 10 ? '0' + minutes : minutes;
            var strTime = hours + ':' + minutes + ' ' + ampm;
            var rightMonth = date.getMonth() + 1;
            return strTime + " " + date.getDate() + "/" + rightMonth + "/" + date.getFullYear();
        }
    }
    function mapMoreInfo2(stringMmsi) {

        $.ajax({
            method: "POST",
            url: urlBase + '/api/TrackNTrace/PositionHistory',
            contentType: "application/json",
            data: JSON.stringify(stringMmsi)

        }).done(function (data) {
            if (coordOld.lenght > 0) {
                removeIcons(coordOld);
            }

            setTimeout(function () {
                for (var i = 0; i < data.length; i++) {
                    var image;
                    var coord;
                    var _mmsi;
                    image = 'https://icon-library.net/images/vessel-icon/vessel-icon-6.jpg';
                    coord = [data[i].lon, data[i].lat];
                    coordOld[i] = [data[i].lon, data[i].lat];

                    addHistory(coord);


                }


                $("#main-content").fadeIn();
                window.scrollTo(0, 1000);
            }, 1000);


        })








    }

    var mapMoreInfo = new ol.Map({
        //overlays: [overlay]
    });
    mapMoreInfo.setTarget('mapMoreInfo');
    var view = new ol.View({
        projection: 'EPSG:4326',
        center: [-46.656861, -23.587397],
        zoom: 0,
        maxResolution: 0.703125
    })
    mapMoreInfo.setView(view);
    var tile_layer = new ol.layer.Tile({
        source: new ol.source.OSM({ layer: 'osm' })
    })
    // Inicia o Mapa em uma posição setada
    mapMoreInfo.addLayer(tile_layer);
    mapMoreInfo.getView().setZoom(2.0);







    // Inicia o Mapa
    var map = new ol.Map({
        //overlays: [overlay]
    });
    map.setTarget('map');
    var view = new ol.View({
        projection: 'EPSG:4326',
        center: [-46.656861, -23.587397],
        zoom: 0,
        maxResolution: 0.703125
    })
    map.setView(view);
    var tile_layer = new ol.layer.Tile({
        source: new ol.source.OSM({ layer: 'osm' })
    })
    // Inicia o Mapa em uma posição setada
    map.addLayer(tile_layer);
    map.getView().setZoom(2.0);







    //Remove the information of the last loop
    $("#tableDelayed").hide();
    $("#main-content").hide();

    $("#chargerBar").show();



    $("#document").ready(function () {
        $("html").removeClass("loading");
        window.setInterval(function () {
            // IN FIRST SCAN CALL TO  GetPortsLocation()
            if (firstCycle == 0) {
                GetPortsLocation();
                firstCycle++;
            }
            //CLOSE MORE INFORMATION TABLE
            $("#closeTable").on('click', function () {
                window.scrollTo(0, 0);
                $("#main-content").fadeOut();
                $("#listaInfoNavio").empty();
                $("#tableVessel").empty();
                $("#listaInfoNavio").empty();
                firstCycle2 = 0;

            });
            // SEARCH ENGINE
            $(document).keypress('click', function (event) {
                if (firstCycle2 == 0) {
                    var keycode = (event.keyCode ? event.keyCode : event.which);
                    if (keycode == '13') {
                        window.scrollTo(0, 0);
                        $("#main-content").fadeOut();
                        $("#listaInfoNavio").empty();
                        $("#tableVessel").empty();
                        $("#listaInfoNavio").empty();

                        var value;
                        value = $("#searchEngineId").val().trim();
                        //value = String(value);
                        console.log(value);
                        var strLength = value.length;
                        console.log(strLength);
                        if (strLength == 7) {
                            for (var i = 0; i < listData.ListProcessModel.length; i++) {
                                if (listData.ListProcessModel[i].License_Plate == value) {
                                    firstCycle2 = 0;
                                    $("#searchEngineId").text("");
                                    SetTruckDataOnTable(listData.ListProcessModel[i]);
                                    break;
                                }

                            }
                            for (var i = 0; i < listTruckOnBoardData.length; i++) {
                                if (value == listTruckOnBoardData[i].licensePlate) {
                                    SetTruckOnBoardInTable(listTruckOnBoardData[i]);
                                    break;
                                }
                            }

                        } else if (strLength > 7 && strLength <= 10) {
                            firstCycle2 = 0;
                            $("#searchEngineId").text("");
                            SetVesselDataOnTable(value);

                        }
                        else {
                            alert("Este numero não existe: " + value);
                        }
                        $("#searchEngineId").text("");




                        firstCycle2++;
                    }



                }
            });

            if (listDataBatch != null || listData != null) {
                //SET A VALUE INTO A LATERAL NUMBER
                $("#countTotal").text("Total: " + totalBar);
                $("#countOk").text("Ok: " + greenBar);
                $("#countMissInformation").text("Possible Delayed: " + orangeBar);
                $("#countDelay").text("Delayed: " + redBar);
            } else {
                $("#countingBar").hide();
                $("#countingBar2").hide();
                $("#countingBar3").hide();
                $("#countingBar4").hide();
            }






            //SET VALUE IN DELAY TABLE
            if ($("#redTruckCheck").is(':checked')) {

                $("#tableDelayed").fadeIn();
            } else {
                $("#tableDelayed").fadeOut();
            }

        }, 500);

    }
    )


    function addHistory(coord) {
        //Vessel icon more info icon
        var image = 'https://i.imgur.com/mRCNbTr.png';


        var iconScaniaGeometry = new ol.geom.Point(ol.proj.fromLonLat(coord, 'EPSG:4326', 'EPSG:3857'));
        var iconScaniaFeature = new ol.Feature({
            geometry: iconScaniaGeometry,

        });


        var iconScaniakStyle = new ol.style.Style({
            image: new ol.style.Icon(({
                //anchor:[0.5, 1],
                scale: 0.045,
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

    function addMarkerOnMap(_truckCoordinate, _id, _image, _scale) {


        var iconScaniaGeometry = new ol.geom.Point(ol.proj.fromLonLat(_truckCoordinate, 'EPSG:4326', 'EPSG:3857'));
        var iconScaniaFeature = new ol.Feature({
            geometry: iconScaniaGeometry,
            data: _id,
            name: _id
        });
        var iconScaniakStyle = new ol.style.Style({
            image: new ol.style.Icon(({
                //anchor:[0.5, 1],
                scale: _scale,
                opacity: 0.9,
                src: _image
            }))
        });


        iconScaniaFeature.setStyle(iconScaniakStyle);

        var vectorSource = new ol.source.Vector({
            features: [iconScaniaFeature]
        });




        markerVectorLayer = new ol.layer.Vector({
            source: vectorSource,
        });

        map.addLayer(markerVectorLayer);

    }

    function moreInfoAboutVessel(evt) {
        var iCount = 0;
        var feature = map.forEachFeatureAtPixel(evt.pixel, function (feature, layer) {


            return feature;
        });
        console.log(feature.values_.data);
        var clickedItem = feature.values_.data;
        //Filtra a localização
        for (var i = 0; i < listData.ListProcessModel.length; i++) {
            if (clickedItem == listData.ListProcessModel[i].License_Plate) {
                alert("Buscando por License Plate: " + listData.ListProcessModel[i].License_Plate);
                SetTruckDataOnTable(listData.ListProcessModel[i]);
            }
        }
        for (var i = 0; i < listDataVessel.length; i++) {
            if (clickedItem == listDataVessel[i].mmsi) {

                alert("Buscando por Mmsi: " + listDataVessel[i].mmsi);
                iCount++;
                SetVesselDataOnTable(listDataVessel[i].mmsi);
                break;


            }

        }
        for (var i = 0; i < listTruckOnBoardData.length; i++) {
            if (clickedItem == listTruckOnBoardData[i].licensePlate) {
                SetTruckOnBoardInTable(listTruckOnBoardData[i]);
            }
        }
    }

    function SetTruckDataOnTable(_licensePlate) {
        var referenceNumber = _licensePlate.ListPruLoad[0].Reference_Number;
        var edaAvarage = _licensePlate.ListPruLoad[0].EdaAverageFromDistanceDotToDot;
        var initEda = _licensePlate.ListPruLoad[0].INIT_EDA;
        var EDA = _licensePlate.ListPruLoad[0].EDA;


        referenceNumber = referenceNumber.substr(0, 10);
        edaAvarage = edaAvarage.substr(0, 10);
        initEda = initEda.substr(0, 10);
        EDA = EDA.substr(0, 10);

        $("#listaInfoNavio").append(
            "<ul class='nav nav-pills style-scope ' >" +


            "<li class='style - scope'>" + "<a>" + "POP-ID - " + _licensePlate.License_Plate + "</a>" + "</li>" +
            "<li class='style - scope'>" + "<a>" + "MARKET - " + _licensePlate.Carrier_Code + "</a>" + "</li>" +
            " </ul> "
        );
        $("#tableVessel").append(
            "<table class='table table-striped table-clickable table-panel copyable style-scope ' ><thead class='style-scope '><tr class='style-scope '>" +

            "<th>REFERENCE</th>" +
            "<th>FORECAST</th>" +
            "<th> EDA </th>" +
            " <th>INIT EDA	</th>" +
            " </tr ></thead > "

            + "<tbody class='style-scope style='overflow:auto; width:300px' '><tr class='style-scope '>" +
            "<td>"
            + referenceNumber + "</td><td>"
            + edaAvarage + "</td><td>"
            + initEda + "</td><td>"
            + EDA + "</td>"
            + "</tr></tbody></table>");

        firstCycle2 = 0;

        $("#main-content").fadeIn();
        window.scrollTo(0, 1000);

    }


    function SetVesselDataOnTable(stringMmsi) {
        var number = 0;
        var vesselName;
        var vesselMmsi;
        for (var i = 0; i < listDataVessel.length; i++) {
            try {
                if (stringMmsi.includes(listDataVessel[i].mmsi) == true && number == 0) {


                    $("#listaInfoNavio").append(
                        "<ul class='nav nav-pills style-scope ' >" +


                        "<li class='style - scope'>" + "<a>" + "NAME - " + listDataVessel[i].name + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "IMO - " + listDataVessel[i].imo + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "MMSI - " + listDataVessel[i].mmsi + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "INDICATIVE - " + listDataVessel[i].indicative + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "FLAG - " + listDataVessel[i].flag + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "TYPE - " + listDataVessel[i].aisVesselType + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "CAPACITY - " + listDataVessel[i].capacity + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "SIZE - " + listDataVessel[i].vesselSize + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "YEAR - " + listDataVessel[i].year + "</a>" + "</li>" +
                        "<li class='style - scope'>" + "<a>" + "STATE - " + listDataVessel[i].state + "</a>" + "</li>" +
                        " </ul> "
                    );

                    firstCycle2 = 0;
                    vesselMmsi = listDataVessel[i].mmsi;
                    vesselName = listDataVessel[i].name;
                    number++;
                    SetBatchDataOnTable(vesselName, vesselMmsi);
                }
            } catch (e) {


            }




        }

        ;
    }

    function SetBatchDataOnTable(stringName, stringMmsi) {
        for (var i = 0; i < listDataBatch.length; i++) {
            var compara = listDataBatch[i].vessel.toUpperCase();
            firstCycle2 = 0;
            console.log(compara);
            if (compara.includes(stringName) == true) {

                $("#tableVessel").append(
                    "<table class='table table-striped table-clickable table-panel copyable style-scope ' ><thead class='style-scope '><tr class='style-scope '>" +

                    "<th>BATCH ID</th>" +
                    "<th>POP ID </th>" +
                    "<th> CHASSI </th>" +
                    " <th>CUSTUMER ORDER</th>" +
                    "<th>PART PERIOD</th>" +
                    " <th>TYPE</th>" +
                    " <th>MARKET</th>" +
                    " <th>MODEL</th>" +
                    " <th>PDD</th>" +
                    " <th>PLAN PACKING</th>" +
                    " <th>PLAN DELIVERY</th>" +
                    " <th>LINER</th>" +
                    " <th>PORT DESTINATION</th>" +
                    " <th>INTTRA NUMBER</th>" +
                    " <th>BOOKING</th>" +
                    " <th>TERMINAL</th>" +
                    " <th>LAST DATE OUT SLA</th>" +
                    " <th>ETD SANTOS</th>" +
                    " <th>ETD 2 SANTOS </th>" +
                    " <th>ATD SANTOS</th>" +
                    " <th>ETA DESTINATION</th>" +
                    " <th>ETA 2 DESTINATION</th>" +
                    " <th>ATA DESTINATION</th>" +


                    " </tr ></thead > "

                    + "<tbody class='style-scope style='overflow:auto; width:200px' '><tr class='style-scope '>" +
                    "<td>"
                    + listDataBatch[i].batchId + "</td><td>"
                    + listDataBatch[i].popId + "</td><td>"
                    + listDataBatch[i].chassis + "</td><td>"
                    + listDataBatch[i].customerOrder + "</td><td>"
                    + listDataBatch[i].partPeriod + "</td><td>"
                    + listDataBatch[i].type + "</td><td>"
                    + listDataBatch[i].market + "</td><td>"
                    + listDataBatch[i].model + "</td><td>"
                    + listDataBatch[i].pdd + "</td><td>"
                    + listDataBatch[i].planPacking + "</td><td>"
                    + listDataBatch[i].planDelivery + "</td><td>"
                    + listDataBatch[i].liner + "</td><td>"
                    + listDataBatch[i].portDestination + "</td><td>"
                    + listDataBatch[i].inttraNumber + "</td><td>"
                    + listDataBatch[i].booking + "</td><td>"
                    + listDataBatch[i].terminal + "</td><td>"
                    + listDataBatch[i].lastDateOutSLA + "</td><td>"
                    + listDataBatch[i].etdSantos + "</td><td>"
                    + listDataBatch[i].etd2Santos + "</td><td>"
                    + listDataBatch[i].atdSantos + "</td><td>"
                    + listDataBatch[i].etaDestination + "</td><td>"
                    + listDataBatch[i].etA2Destination + "</td><td>"
                    + listDataBatch[i].ataDestination + "</td>"
                    + "</tr></tbody></table><br/>");




            } else if (true) {

            }

        }
        firstCycle2 = 0;
        mapMoreInfo2(stringMmsi);
    }

    map.on('singleclick', function (evt) {
        window.scrollTo(0, 0);
        $("#main-content").fadeOut();
        $("#listaInfoNavio").empty();
        $("#tableVessel").empty();
        $("#listaInfoNavio").empty();
        firstCycle2 = 0;
        moreInfoAboutVessel(evt);
    });


    function percentFromDecimal(decimal) {
        var number = decimal * 1000;
        var stringToReturn = number.toString() + "px";
        return stringToReturn;
    }

    function GetPortsLocation() {
        $.ajax({
            type: 'GET',
            url: urlBase + "/api/CadastroDePontos/ReturnPortinDB",
            success: function (data) {
                console.log(data);
            },
            error: function () {
            }
        }).done(function (data) {
            for (var i = 0; i < data.length; i++) {
                var location = [data[i].longitude, data[i].latitude];
                var id = data[i].portName;
                var image = "https://cdn2.iconfinder.com/data/icons/travel-places-1/50/2693-anchor-512.png";
                var scale = 0.05;
                addMarkerOnMap(location, id, image, scale);
            }
            GetCredencial();
        });
    }

    function GetCredencial() {
        var userInputVal = "ssbjvq";
        var passInputVal = "ssbjvq";

        var parameter = { "username": userInputVal, "password": passInputVal };
        console.log(parameter);
        if (userInputVal != "") {
            if (passInputVal != "") {
                $.ajax({
                    type: "POST",
                    url: "https://tracking-api.lomi.devtest.aws.scania.com/api/Login?api-version=1.0",
                    data: JSON.stringify(parameter),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (data) {
                        console.log(data);
                    },
                    error: function () {
                    }
                }).done(function (data) {
                    GetAllOutboundTruck(data);
                });
            } else {

            }
        } else {
            ;
        }






    }

    function returnJustOutbound(item) {

        var arrayToReturn = [];
        $(item.ListProcessModel).each(function (index) {
            if (checkListArray.includes(item.ListProcessModel[index].Carrier_Code)) {
                arrayToReturn.push(item.ListProcessModel[index])
                    ;
            }
        });
        //console.log(arrayToReturn);
        return { CheckListModel: null, ListProcessModel: arrayToReturn };
    }

    function GetAllOutboundTruck(data) {
        // console.log(JSON.parse(window.localStorage.getItem("__amplify__trackingAndTraceKey")).data);
        var token = data.accessToken;
        console.log(token);
        $.ajax({
            type: 'GET',
            url: "https://tracking-api.lomi.devtest.aws.scania.com/api" + "/Process/GetAll?api-version=1.0&flowId=4", //"/Process/GetAll?api-version=1.0&flowId=4",
            headers: { "Authorization": token },
            success: function (dataGetAll) {
                 console.log(dataGetAll);
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
            }
        }).done(function (dataGetAll) {
            listData = returnJustOutbound(dataGetAll.Content);
            console.log(listData);
            for (var i = 0; i < listData.ListProcessModel.length; i++) {
                var localizacao = [listData.ListProcessModel[i].Longitude, listData.ListProcessModel[i].Latitude];
                var id = listData.ListProcessModel[i].License_Plate;
                var colorTruck1 = "";
                var EDA = new Date(listData.ListProcessModel[i].ListPruLoad[0].EDA);
                var forecast = new Date(listData.ListProcessModel[i].ListPruLoad[0].EdaAverageFromDistanceDotToDot);
                var nowDate = new Date();

                if (listData.ListProcessModel[i].ListPruLoad[0].PRU_GATE_ARRIVAL_DATE == null && listData.ListProcessModel[i].ListPruLoad[0].PRU_GATE_ENTRY_DATE == null) {

                    colorTruck1 = "Pictures/blank.png";
                    if (nowDate > EDA) {
                        colorTruck1 = "Pictures/redTruckReverse.png";


                    }
                    else if (forecast > EDA) {
                        colorTruck1 = "Pictures/orangeTruck.png";


                    }
                    else { colorTruck1 = "Pictures/greenTruckReverse.png"; }

                    if (listData.ListProcessModel[i].Longitude == 0 || listData.ListProcessModel[i].Latitude == 0 || listData.ListProcessModel[i].Longitude == null || listData.ListProcessModel[i].Latitude == null) {
                        $("#tableFailedTrucks").append(
                            "<tr class='style-scope '>" +
                            "<td>"
                            + "<p>" + listData.ListProcessModel[i].Trailer_Id + "</p>" +
                            "</td>"
                            + "<td>"
                            + "<p>" + listData.ListProcessModel[i].Carrier_Code + "</p>" +
                            "</td>"
                            + "</tr>"

                        );
                    } else if ((colorTruck1 === "Pictures/redTruck.png" || colorTruck1 === "Pictures/redTruckReverse.png") && (listData.ListProcessModel[i].Longitude != 0 || listData.ListProcessModel[i].Latitude != 0 || listData.ListProcessModel[i].Longitude != null || listData.ListProcessModel[i].Latitude != null)) {

                        redBar++;
                        totalBar++;
                        $("#tableTrucksDelayed").append(
                            "<tr class='style-scope '>" +
                            "<td>"
                            + "<p>" + listData.ListProcessModel[i].Trailer_Id + "</p>" +
                            "</td>"
                            + "<td>"
                            + "<p>" + listData.ListProcessModel[i].Carrier_Code + "</p>" +
                            "</td>"
                            + "</tr>"

                        );
                    } else if ((colorTruck1 === "Pictures/greenTruck.png" || colorTruck1 === "Pictures/greenTruckReverse.png") && (listData.ListProcessModel[i].Longitude != 0 || listData.ListProcessModel[i].Latitude != 0 || listData.ListProcessModel[i].Longitude != null || listData.ListProcessModel[i].Latitude != null)) {

                        greenBar++;
                        totalBar++;
                        $("#tableTrucksGreen").append(
                            "<tr class='style-scope '>" +
                            "<td>"
                            + "<p>" + listData.ListProcessModel[i].Trailer_Id + "</p>" +
                            "</td>"
                            + "<td>"
                            + "<p>" + listData.ListProcessModel[i].Carrier_Code + "</p>" +
                            "</td>"
                            + "</tr>"

                        );
                    } else if ((colorTruck1 === "Pictures/orangeTruck.png" || colorTruck1 === "Pictures/orangeTruckReverse.png") && (listData.ListProcessModel[i].Longitude != 0 || listData.ListProcessModel[i].Latitude != 0 || listData.ListProcessModel[i].Longitude != null || listData.ListProcessModel[i].Latitude != null)) {
                        orangeBar++;
                        totalBar++;
                        $("#tableTrucksOrange").append(
                            "<tr class='style-scope '>" +
                            "<td>"
                            + "<p>" + listData.ListProcessModel[i].Trailer_Id + "</p>" +
                            "</td>"
                            + "<td>"
                            + "<p>" + listData.ListProcessModel[i].Carrier_Code + "</p>" +
                            "</td>"
                            + "</tr>"

                        );
                    }
                }
                var image = "https://tracking.lomi.devtest.aws.scania.com/" + colorTruck1;
                if (listData.ListProcessModel[i].Longitude != 0 && listData.ListProcessModel[i].Latitude != 0 && listData.ListProcessModel[i].Longitude != null && listData.ListProcessModel[i].Latitude != null) {
                    var scale = 0.8;
                    addMarkerOnMap(localizacao, id, image, scale);

                    var localizacao = [listData.ListProcessModel[i].ListPruLoad[0].Destiny.DestinyLongitude, listData.ListProcessModel[i].ListPruLoad[0].Destiny.DestinyLatitude];
                    var id = listData.ListProcessModel[i].ListPruLoad[0].Destiny.DestinyName;

                    var image = "https://tracking.lomi.devtest.aws.scania.com/"  + "Pictures/scaniaPin.png";
                    var scale = 0.1;
                    addMarkerOnMap(localizacao, id, image, scale);
                }

            }


            GetAllOutboundVessel();
        });

    }

    function GetAllOutboundVessel() {

        $.getJSON(urlBase + '/api/TrackNTrace/ReturnAllJustOnTransport', function (data) { //  ReturnJustOnTransport GetLocationMostRecent


        }).done(function (data) {
            var count = 0;
            var today = new Date();
            console.log(data);
            var listVesselDaleyed = [];
            listDataBatch = data.item3;
            listDataVessel = data.item2;
            for (var i = 0; i < listDataBatch.length; i++) {
                try {
                    if (listDataBatch[i].etA2Destination.length > 1) {


                        $("#tableItemsDelayed").append(
                            "<tr class='style-scope '>" +
                            "<td>"
                            + "<p>" + data.item3[i].batchId + "</p>" +
                            "</td>"
                            + "<td>"
                            + "<p>" + data.item3[i].chassis + "</p>" +
                            "<td>"
                            + "<p>" + data.item3[i].vessel + "</p>" +
                            "</td>"
                            + "</tr>"

                        );
                        if (count == 0) {
                            listVesselDaleyed[count] = listDataBatch[i].vessel;
                            count++;
                        }
                        else if (listVesselDaleyed.indexOf(listDataBatch[i].vessel) === -1) {
                            listVesselDaleyed[count] = listDataBatch[i].vessel;
                            count++;
                        }

                    }

                } catch (e) {

                }
            }














            for (var i = 0; i <= data.item1.length; i++) {

                try {

                    Mmsi[i] = data.item1[i].mmsi;

                    Lon[i] = data.item1[i].lon;

                    Lat[i] = data.item1[i].lat;

                    var localizacao = [Lon[i], Lat[i]];
                    var image = urlBaseImage + "Pictures/vessel-icon.jpg";
                    var scale = 0.05;
                    totalBar++;
                    addMarkerOnMap(localizacao, Mmsi[i], image, scale);

                } catch (e) {

                }
            }





            $("#countingBar").show();
            $("#countingBar2").show();
            $("#countingBar3").show();
            $("#countingBar4").show();

            $("#chargerBar").remove();
            $("#chargerBar2").remove();
            $("#chargerBar3").remove();
            $("#chargerBar4").remove();
            GetTruckOnBoard();


        })
    };

    function GetTruckOnBoard(_licensePlate) {
        $.ajax({
            type: 'GET',
            url: urlBase + "/api/TruckOnBoard/GetAllTable", //"/Process/GetAll?api-version=1.0&flowId=4",urlBase
            
            success: function (data) {
                // console.log(dataGetAll.Content);
            },
            error: function (data, status, erro) {
                if (erro == "Internal Server Error") {
                    console.log("Problema ocorrido: " + status + "\nDescição: " + erro);
                    window.location.href = urlBase + "/Error/Index";
                }
                if (erro == "Not Found") {
                    console.log("Problema ocorrido: " + status + "\nDescição: " + erro);
                    window.location.href = urlBase + "/Error/Index";
                }
            }
        }).done(function (data) {
            listTruckOnBoardData = data;
            for (var i = 0; i < data.length; i++) {
                var coord = [listTruckOnBoardData[i].longitude, listTruckOnBoardData[i].latitude];
                var id = listTruckOnBoardData[i].licensePlate;
                var image = "https://tracker-tracer.s3-eu-west-1.amazonaws.com/Pictures/truck.png";
                var scale = 0.6;
                $("#tableItemsOnBoard").append(
                    "<tr class='style-scope '>" +
                    "<td>"
                    + "<p>" + listTruckOnBoardData[i].licensePlate + "</p>" +
                    "</td>"
                    + "<td>"
                    + "<p>" + listTruckOnBoardData[i].trackNumber + "</p>" +
                    "</td>"
                    + "</tr>"

                );
                totalBar++;
                addMarkerOnMap(coord,id,image,scale);
            }

            console.log(listTruckOnBoardData);
            $("#countingBar").show();
            $("#countingBar2").show();
            $("#countingBar3").show();
            $("#countingBar4").show();



        });
    }

    function SetTruckOnBoardInTable(_licensePlate) {
      

        $("#listaInfoNavio").append(
            "<ul class='nav nav-pills style-scope ' >" +


            "<li class='style - scope'>" + "<a>" + "POP-ID - " + _licensePlate.licensePlate + "</a>" + "</li>" +
            "<li class='style - scope'>" + "<a>" + "ON BOARD - ABC CARGA" + "</a>" + "</li>" +
            " </ul> "
        );
        $("#tableVessel").append(
            "<table class='table table-striped table-clickable table-panel copyable style-scope ' ><thead class='style-scope '><tr class='style-scope '>" +

            "<th>Track Number</th>" +
            "<th>Data de Localização</th>" +
            "<th> Data de Gravação </th>" +
            " </tr ></thead > "

            + "<tbody class='style-scope style='overflow:auto; width:300px' '><tr class='style-scope '>" +
            "<td>"
            + _licensePlate.trackNumber + "</td><td>"
            + _licensePlate.dataDaLocalizacao + "</td><td>"
            + _licensePlate.dataDeGravacao + "</td>"
            + "</tr></tbody></table>");

        firstCycle2 = 0;

        $("#main-content").fadeIn();
        window.scrollTo(0, 1000);

    

    }

})()