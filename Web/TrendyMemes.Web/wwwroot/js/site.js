﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function start() {
    function setupPostCreation() {

        const preview = document.getElementById('image-preview');
        const input = document.getElementById('image-upload');

        if (input && preview) {

            input.addEventListener('onchange', (event) => {
                preview.src = URL.createObjectURL(event.target.files[0]);
            });
        }
    }

    function setupVoting() {
        let container = document.getElementById("main");

        container.addEventListener('click', (event) => {

            if (event.target.classList
                && (event.target.classList.contains('upvote')
                    || event.target.classList.contains('downvote'))) {

                event.preventDefault();

                const parent = event.target.parentElement;
                const ratingDisplay = parent.getElementsByClassName("rating")[0];

                $.ajax({
                    url: event.target.href,
                    success: (newRating) => ratingDisplay.innerText = newRating,
                });
            }
        });
    }

    setupPostCreation();
    setupVoting();
}())
