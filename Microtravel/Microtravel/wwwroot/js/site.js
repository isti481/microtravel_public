// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let timer;

document.getElementById("searchBox").addEventListener("keyup", function () {


clearTimeout(timer);

let value = this.value.trim();
    console.log(value);
    if (value.length == 0) {
        render(value);
        return;
    }
    if (value.length < 3) { return }
    

    render(value);

});


function render(value) {
    timer = setTimeout(() => {

        fetch(`/Travels/Search?searchString=${value}`)
            .then(res => res.text())
            .then(html => {
                document.getElementById("travelContainer").innerHTML = html;
            });

    }, 400);
}