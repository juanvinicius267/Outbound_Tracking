; (function () {
    var nomeDoNavio = [];
    var imo = [];
    var mmsi = [];
    var Indicative = [];
    var Flag = [];
    var AisVesselType = [];
    var Capacity = [];
    var VesselSize = [];
    var Year = [];
    var State = [];
    var modelString = [];
    var jooj = 0;
    var urlBase = "http://10.251.16.112/TrackNTrace";///"http://localhost:51720";
    $("#document").load(function () {

    })
    $("#document").ready(function () {

        if (jooj == 0) {
            ajaxcall();
        }

        $("#buttonSend").on('click', function () {

            if (($('#Name').val() == "") || ($('#Name').val() == " ")) {
                $('#Name').css("border-color", "red")
                $('#Name').css("background-color", "rgba(255, 0, 0, 0.2)")  
            } else {
                $('#Name').css("border-color", "gray")
                $('#Name').css("background-color", "rgba(0, 0, 0, 0)")
            }

            if (($('#Imo').val() == "") || ($('#Imo').val() == " ")) {
                $('#Imo').css("border-color", "red")
                $('#Imo').css("background-color", "rgba(255, 0, 0, 0.2)") 
            } else {
                $('#Imo').css("border-color", "gray")
                $('#Imo').css("background-color", "rgba(0, 0, 0, 0)")
            }

            if (($('#Mmsi').val() == "") || ($('#Mmsi').val() == " ")) {
                $('#Mmsi').css("border-color", "red")
                $('#Mmsi').css("background-color", "rgba(255, 0, 0, 0.2)")
            } else {
                $('#Mmsi').css("border-color", "gray")
                $('#Mmsi').css("background-color", "rgba(0, 0, 0, 0)")
            }

            if (($('#Indicative').val() == "") || ($('#Indicative').val() == " ")) {
                $('#Indicative').css("border-color", "red")
                $('#Indicative').css("background-color", "rgba(255, 0, 0, 0.2)")
            } else {
                $('#Indicative').css("border-color", "gray")
                $('#Indicative').css("background-color", "rgba(0, 0, 0, 0)")
            }

            if (($('#Flag').val() == "") || ($('#Flag').val() == " ")) {
                $('#Flag').css("border-color", "red")
                $('#Flag').css("background-color", "rgba(255, 0, 0, 0.2)")
            } else {
                $('#Flag').css("border-color", "gray")
                $('#Flag').css("background-color", "rgba(0, 0, 0, 0)")
            }

            if (($('#AisVesselType').val() == "") || ($('#AisVesselType').val() == " ")) {
                $('#AisVesselType').css("border-color", "red")
                $('#AisVesselType').css("background-color", "rgba(255, 0, 0, 0.2)")
            } else {
                $('#AisVesselType').css("border-color", "gray")
                $('#AisVesselType').css("background-color", "rgba(0, 0, 0, 0)")
            }

            if (($('#Capacity').val() == "") || ($('#Capacity').val() == " ")) {
                $('#Capacity').css("border-color", "red")
                $('#Capacity').css("background-color", "rgba(255, 0, 0, 0.2)")
            } else {
                $('#Capacity').css("border-color", "gray")
                $('#Capacity').css("background-color", "rgba(0, 0, 0, 0)")
            }
            if (($('#VesselSize').val() == "") || ($('#VesselSize').val() == " ")) {
                $('#VesselSize').css("border-color", "red")
                $('#VesselSize').css("background-color", "rgba(255, 0, 0, 0.2)")
            } else {
                $('#VesselSize').css("border-color", "gray")
                $('#VesselSize').css("background-color", "rgba(0, 0, 0, 0)")
            }

            if (($('#Year').val() == "") || ($('#Year').val() == " ")) {
                $('#Year').css("border-color", "red")
                $('#Year').css("background-color", "rgba(255, 0, 0, 0.2)")
            } else {
                $('#Year').css("border-color", "gray")
                $('#Year').css("background-color", "rgba(0, 0, 0, 0)")
            }

            if (($('#State').val() == "") || ($('#State').val() == " ")) {
                $('#State').css("border-color", "red")
                $('#State').css("background-color", "rgba(255, 0, 0, 0.2)")
            } else {
                $('#State').css("border-color", "gray")
                $('#State').css("background-color", "rgba(0, 0, 0, 0)")
            }
            if (($('#State').val() != "") && ($('#State').val() != " ") && 
                (($('#Year').val() != "") && ($('#Year').val() != " ")) &&
                ($('#VesselSize').val() != "") && ($('#VesselSize').val() != " ") &&
                ($('#Capacity').val() != "") && ($('#Capacity').val() != " ") &&
                ($('#AisVesselType').val() != "") && ($('#AisVesselType').val() != " ") &&
                ($('#Flag').val() != "") && ($('#Flag').val() != " ") &&
                ($('#Indicative').val() != "") && ($('#Indicative').val() != " ") &&
                ($('#Mmsi').val() != "") && ($('#Mmsi').val() != " ") &&
                ($('#Imo').val() != "") && ($('#Imo').val() != " ") &&
                ($('#Name').val() != "") && ($('#Name').val() != " ")) {
                    var model = {
                        Name: $('#Name').val(),
                        Imo: $('#Imo').val(),
                        Mmsi: $('#Mmsi').val(),
                        Indicative: $('#Indicative').val(),
                        Flag: $('#Flag').val(),
                        AisVesselType: $('#AisVesselType').val(),
                        Capacity: $('#Capacity').val(),
                        VesselSize: $('#VesselSize').val(),
                        Year: $('#Year').val(),
                        State: $('#State').val()

                    };
                    modelString = JSON.stringify(model);
                    console.log(modelString);

                    ajaxcal2();

            }









            

        })
    });






    function preencheTabela(numeroNavios) {
        if (numeroNavios !== 0) {

            for (var i = 1; i <= numeroNavios; i++) {

                var table = document.getElementById("myTable");

                var row = table.insertRow(i);

                var cell1 = row.insertCell(0);
                var cell2 = row.insertCell(1);
                var cell3 = row.insertCell(2);
                var cell4 = row.insertCell(3);
                var cell5 = row.insertCell(4);
                var cell6 = row.insertCell(5);
                var cell7 = row.insertCell(6);
                var cell8 = row.insertCell(7);
                var cell9 = row.insertCell(8);
                var cell10 = row.insertCell(9);
                try {
                    cell1.innerHTML = nomeDoNavio[i];
                } catch (e) {
                    cell1.innerHTML = "null";
                }
                try {
                    cell2.innerHTML = imo[i];
                } catch (e) {
                    cell2.innerHTML = "null";
                }
                try {
                    cell3.innerHTML = mmsi[i];
                } catch (e) {
                    cell3.innerHTML = "null";
                }
                try {
                    cell4.innerHTML = Indicative[i];
                } catch (e) {
                    cell4.innerHTML = "null";
                }
                try {

                    cell5.innerHTML = Flag[i];
                } catch (e) {
                    cell5.innerHTML = "null";
                }
                try {
                    cell6.innerHTML = AisVesselType[i];
                } catch (e) {
                    cell6.innerHTML = "null";
                }
                try {
                    cell7.innerHTML = Capacity[i];
                } catch (e) {
                    cell7.innerHTML = "null";
                }
                try {
                    cell8.innerHTML = VesselSize[i];
                } catch (e) {
                    cell8.innerHTML = "null";
                }
                try {

                    cell9.innerHTML = Year[i];
                } catch (e) {
                    cell9.innerHTML = "null";
                }
                try {

                    cell10.innerHTML = State[i];
                } catch (e) {
                    cell10.innerHTML = "null";
                }
            }

        }
    }

    function ajaxcall() {
        $.getJSON(urlBase +"/api/CadastroDeNavios/GetVessel", function (data) {
            var num = 0;
            var str = JSON.stringify(data);
            var v = str.split(",");
            for (var i = 0; i < v.length; i++) {
                var vove = v[i].split(":");

                var strr = ["Name", "Imo", "Mmsi", "Indicative", "Flag", "AisVesselType", "Capacity", "VesselSize", "Year", "State"];
                if (vove[0].includes(strr[0])) {
                    nomeDoNavio[num] = vove[1];
                }
                if (vove[0].includes(strr[1])) {
                    imo[num] = vove[1];
                }
                if (vove[0].includes(strr[2])) {
                    mmsi[num] = vove[1];
                }
                if (vove[0].includes(strr[3])) {
                    Indicative[num] = vove[1];
                }
                if (vove[0].includes(strr[4])) {
                    Flag[num] = vove[1];
                }
                if (vove[0].includes(strr[5])) {
                    AisVesselType[num] = vove[1];
                }
                if (vove[0].includes(strr[6])) {
                    Capacity[num] = vove[1];
                }
                if (vove[0].includes(strr[7])) {
                    VesselSize[num] = vove[1];
                }
                if (vove[0].includes(strr[8])) {
                    Year[num] = vove[1];
                }
                if (vove[0].includes(strr[9])) {
                    breakState = vove[1].split("}");
                    State[num] = breakState[0];
                    num = num + 1;
                }
            }
            preencheTabela(data.length);
        });

    }


    function ajaxcal2(string) {
        $.ajax({
            type: 'POST',
            url: urlBase +"/api/CadastroDeNavios/SetVessel",
            contentType: "application/json",
            data: modelString,
            success: function (result, status, request) {
                $("html").removeClass("loading");
            },
            error: function (request, status, erro) {

                if (erro == "Not Found") {
                    console.log("Problema ocorrido: " + status + "\nDescição: " + erro);
                    window.location.href = urlBase+"/Error/Index";
                }
            },

        }).done(function () {
            alert("Cadastrado!");
            document.location.reload(true);
        }
        );

    }
})()