var updateMs = 60000;

var rateCtx = document.getElementById("rate").getContext("2d");
var maxRateCtx = document.getElementById("maxrate").getContext("2d");
var attenuationUpCtx = document.getElementById("attenuationup").getContext("2d");
var attenuationDownCtx = document.getElementById("attenuationdown").getContext("2d");
var noiseMarginCtx = document.getElementById("noisemargin").getContext("2d");

var red = 'rgb(255, 99, 132)';
var orange = 'rgb(255, 159, 64)';
var yellow = 'rgb(255, 205, 86)';
var green = 'rgb(75, 192, 192)';
var blue = 'rgb(54, 162, 235)';
var purple = 'rgb(153, 102, 255)';
var grey = 'rgb(201, 203, 207)';
var transparent = 'rgba(0, 0, 0, 0)';

var timestamps = []

var rateData = {
    labels: timestamps,
    datasets: [{
        label: "Upload",
        data: [],
        borderColor: red,
        backgroundColor: transparent
    }, {
        label: "Download",
        data: [],
        borderColor: blue,
        backgroundColor: transparent
    }]
};

var maxRateData = {
    labels: timestamps,
    datasets: [{
        label: "Upload",
        data: [],
        borderColor: red,
        backgroundColor: transparent
    }, {
        label: "Download",
        data: [],
        borderColor: blue,
        backgroundColor: transparent
    }]
};

var attenuationUpData = {
    labels: timestamps,
    datasets: [{
        label: "DS1",
        data: [],
        borderColor: orange,
        backgroundColor: transparent
    }, {
        label: "DS2",
        data: [],
        borderColor: yellow,
        backgroundColor: transparent
    },
    {
        label: "DS3",
        data: [],
        borderColor: green,
        backgroundColor: transparent
    },
    {
        label: "DS4",
        data: [],
        borderColor: purple,
        backgroundColor: transparent
    }]
};

var attenuationDownData = {
    labels: timestamps,
    datasets: [{
        label: "DS1",
        data: [],
        borderColor: orange,
        backgroundColor: transparent
    }, {
        label: "DS2",
        data: [],
        borderColor: yellow,
        backgroundColor: transparent
    },
    {
        label: "DS3",
        data: [],
        borderColor: green,
        backgroundColor: transparent
    }]
};

var noiseMarginData = {
    labels: timestamps,
    datasets: [{
        label: "Upload",
        data: [],
        borderColor: red,
        backgroundColor: transparent
    }, {
        label: "Download",
        data: [],
        borderColor: blue,
        backgroundColor: transparent
    }]
};

function now() {
    var now = new Date();

    return [
        now.getFullYear() +
        "/" + (now.getMonth() + 1) +
        "/" + now.getDate(),
        now.getHours() +
        ":" + now.getMinutes() +
        ":" + now.getSeconds()
    ];
}

drawCharts();
setInterval(function () {

    var xmlhttp;
    xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var data = JSON.parse(xmlhttp.responseText);

            rateData.datasets[0].data.push(data.lineRateUp);
            rateData.datasets[1].data.push(data.lineRateDown);

            maxRateData.datasets[0].data.push(data.maxLineRateUp);
            maxRateData.datasets[1].data.push(data.maxLineRateDown);

            attenuationUpData.datasets[0].data.push(data.attenuationUp[0]);
            attenuationUpData.datasets[1].data.push(data.attenuationUp[1]);
            attenuationUpData.datasets[2].data.push(data.attenuationUp[2]);
            attenuationUpData.datasets[3].data.push(data.attenuationUp[3]);

            attenuationDownData.datasets[0].data.push(data.attenuationDown[0]);
            attenuationDownData.datasets[1].data.push(data.attenuationDown[1]);
            attenuationDownData.datasets[2].data.push(data.attenuationDown[2]);

            noiseMarginData.datasets[0].data.push(data.noiseMarginUp);
            noiseMarginData.datasets[1].data.push(data.noiseMarginDown);

            timestamps.push(now());

            //redraw
            drawCharts();
        }
    }
    xmlhttp.open("GET", "api/values", true);
    xmlhttp.send();
}, updateMs);

function drawCharts() {
    var rateChart = new Chart(rateCtx, { type: 'line', data: rateData });
    var maxrateChart = new Chart(maxRateCtx, { type: 'line', data: maxRateData });
    var attenuationUpChart = new Chart(attenuationUpCtx, { type: 'line', data: attenuationUpData });
    var attenuationDownChart = new Chart(attenuationDownCtx, { type: 'line', data: attenuationDownData });
    var noiseMarginChart = new Chart(noiseMarginCtx, { type: 'line', data: noiseMarginData });
}
