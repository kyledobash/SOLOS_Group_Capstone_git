"use strict"

$(document).ready(function () {
    $.get("https://localhost:44323/api/JobSearch", function (data) {
        data.map(function (el) {
            $("#JobsToUpdate").append(`<div>
                    <div>${JSON.stringify(el.title)}</div>
                    <div>${JSON.stringify(el.company)}</div>
                    <div>${JSON.stringify(el.type)}</div>
                    <div>${JSON.stringify(el.description)}</div>
                    </div>          
                    <br>`);
        })
    })
})
//// Get the modal
//var modal = document.getElementById("myModal");

//// Get the button that opens the modal
//var btn = document.getElementById("myBtn");

//// Get the <span> element that closes the modal
//var span = document.getElementsByClassName("close")[0];

//// When the user clicks on the button, open the modal
//btn.onclick = function () {
//    modal.style.display = "block";
//}

//// When the user clicks on <span> (x), close the modal
//span.onclick = function () {
//    modal.style.display = "none";
//}

//// When the user clicks anywhere outside of the modal, close it
//window.onclick = function (event) {
//    if (event.target == modal) {
//        modal.style.display = "none";
//    }
//} 