// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
    document.addEventListener("DOMContentLoaded", function () {
        const currentLocation = window.location.pathname;
    const menuItems = document.querySelectorAll('.nav-link');

        menuItems.forEach(item => {
            if (item.getAttribute('href') === currentLocation) {
        item.classList.add('active');
            }
        });
    });