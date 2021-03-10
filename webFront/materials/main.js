var stations;

function retrieveAllContracts() {
    var targetUrl = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + document.getElementById("apiKey").value;
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    // The header set below limits the elements we are OK to retrieve from the server.
    caller.setRequestHeader("Accept", "application/json");
    // onload shall contain the function that will be called when the call is finished.
    caller.onload = contractsRetrieved;

    caller.send();
}

function contractsRetrieved() {
    // Let's parse the response:
    var response = JSON.parse(this.responseText);
    console.log(response);
    createList(response);
}

function createList(response) {

    // INPUT
    var inputdatalist = document.createElement("input");
    inputdatalist.setAttribute("list", "contractsList");
    inputdatalist.setAttribute("id", "inputContractList");

    // LABEL
    var labelInput = document.createElement("label");
    labelInput.setAttribute("for", "inputContractList");
    labelInput.textContent = "Contract : ";

    // DATALIST
    var datalist = document.createElement("datalist");
    datalist.setAttribute("id", "contractsList");
    console.log(response.length);

    // OPTIONS
    var i;
    for (i = 0; i < response.length; i++) {
        var optionContract = document.createElement("option");
        optionContract.setAttribute("value", response[i].name);
        console.log(response[i].name);
        datalist.appendChild(optionContract);
    }

    // BUTTON
    var validButton = document.createElement("button");
    validButton.setAttribute("type", "button");
    validButton.setAttribute("onclick", "retrieveContractStations();");
    validButton.textContent = "Search Contract";


    // ADD NODES TO DOM
    var el = document.getElementById('showContract');

    document.body.insertBefore(labelInput, el);
    document.body.insertBefore(inputdatalist, el);
    document.body.insertBefore(datalist, el);
    document.body.insertBefore(validButton, el);

}

function retrieveContractStations() {
    var key = document.getElementById("apiKey").value;
    var name = document.getElementById("inputContractList").value;
    var targetUrl = "https://api.jcdecaux.com/vls/v3/stations?contract=" + name + "&apiKey=" + key;
    var requestType = "GET";

    var caller = new XMLHttpRequest();
    caller.open(requestType, targetUrl, true);
    // The header set below limits the elements we are OK to retrieve from the server.
    caller.setRequestHeader("Accept", "application/json");
    // onload shall contain the function that will be called when the call is finished.
    caller.onload = stationsRetrieved;

    caller.send();
}

function stationsRetrieved() {
    // Let's parse the response:
    stations = JSON.parse(this.responseText);
    console.log(stations);

    // SHOW NEXT STEP
    if (stations.length > 0) {
        var divStep2 = document.getElementById('step2');
        divStep2.style.display = 'block';
    } else {
        console.log("no station found");
    }
}


function findClosestStation() {

    var lat = document.getElementById("latitude").value;
    var long = document.getElementById("longitude").value;

    var minDistanceStation;
    var distenceToMinStation = Infinity;

    var i;
    for (i = 0; i < stations.length; i++) {
        if (i == 0) {
            minDistanceStation = stations[0];
        }

        var stationLat = stations[i].position.lat;
        var stationLng = stations[i].position.lng;
        var distanceToStation = getDistanceFrom2GpsCoordinates(lat, long, stationLat, stationLng);

        if (distanceToStation < distenceToMinStation) {
            distenceToMinStation = distanceToStation;
            minDistanceStation = stations[i];
        }
    }

    console.log(minDistanceStation);
}


function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    // Radius of the earth in km
    var earthRadius = 6371;
    var dLat = deg2rad(lat2 - lat1);
    var dLon = deg2rad(lon2 - lon1);
    var a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2)
        ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = earthRadius * c; // Distance in km
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI / 180)
}