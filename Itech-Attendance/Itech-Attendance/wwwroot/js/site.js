﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var countDownDate = new Date(new Date().getTime() + 1 * 60000)

// Update the count down every 1 second
var x = setInterval(function () {

    // Get today's date and time
    var now = new Date().getTime();

    // Find the distance between now and the count down date
    var distance = countDownDate - now;

    // Time calculations for days, hours, minutes and seconds
    var days = Math.floor(distance / (1000 * 60 * 60 * 24));
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Output the result in an element with id="demo"
    document.getElementById("demo").innerHTML = days + "d " + hours + "h "
        + minutes + "m " + seconds + "s ";

    // If the count down is over, write some text 
    if (distance < 0) {
        clearInterval(x);
        document.getElementById("demo").innerHTML = "EXPIRED";

        var classNameValue = document.getElementById("classNameInput").value;
        console.log(classNameValue);
        var formdata = new FormData();

        formdata.append("className", classNameValue);

        fetch("/Home/GenerateNewQR",
            {
                method: "POST",
                body: formdata
            })
            .then(function (res) {
                if (res.redirected) {
                    // The response indicates a redirect
                    window.location.href = res.url; // Redirect to the new URL
                } else {
                    // Handle the response as needed
                    console.log(classNameValue);
                }
            })
    }
}, 1000);

$(document).ready(function () {
    $("#tablePartial").load("/Table/IT2X");

    document.getElementById('selectOption').addEventListener('change', function () {
        // Get the selected value from the select element
        const selectedOption = this.value;
        $("#tablePartial").load(`/Table/${selectedOption}`);
    });
});

