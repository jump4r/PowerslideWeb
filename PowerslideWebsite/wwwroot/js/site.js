// Write your Javascript code.

let sections = null;

document.addEventListener('DOMContentLoaded', _ => {

    modal = document.querySelector('#submission-modal-wrapper');
    window.addEventListener("resize", VerifyHeaderDisplay);
    sections = document.querySelectorAll('.header-section'); 

    if (modal != null) {
        document.querySelector('#submission-modal-open').addEventListener('click', OpenSubmissionModal);
        document.querySelector('#submission-modal-close').addEventListener('click', CloseSubmissionModal);
       
        $('#featured-beatmaps-carousel').slick({
            infinite: true,
            slidesToShow: 1,
            slidesToScroll: 1
        });
    }

    document.querySelector('#headerArrow').addEventListener('click', HeaderDisplayToggle);
    
});

function CloseSubmissionModal() {
    modal.style.display = 'none';
}

function OpenSubmissionModal() {
    modal.style.display = 'flex';
}

/* Header Slide Functionality */
function HeaderDisplayToggle() {
    let e = document.querySelector('#headerArrow');

    console.log(sections);

    // Get the Hex code of the current character to determine toggle direction
    let code = e.innerHTML.charCodeAt(0);
    let codeHex = code.toString(16).toUpperCase();
    while (codeHex.length < 4) {
        codeHex = "0" + codeHex;
    }


    // If we're at the down arrow, go up
    if (codeHex == "25BD") {
        sections.forEach(
            function (obj) {
                obj.style.display = "block";
            }
        );
        e.innerHTML = "△"; // △▽
    }

    // If we're on the up arrow, then go down.
    else {

        sections.forEach(
            function (obj) {
                obj.style.display = "none";
            }
        );
        e.innerHTML = "▽";
    }
    
}

// Verify Header Display Hack (Make sure that when the screen is big, header is always displayed despite a resize of the browser);
// A hack but it works, I'll do the CSS later.
function VerifyHeaderDisplay() {
    if ($(window).width() >= 831) {
        sections.forEach(
            function (obj) {
                obj.style.display = "flex";
            }
        );

    }

    else {
        sections.forEach(
            function (obj) {
                obj.style.display = "none";
            }
        );

        let e = document.querySelector('#headerArrow');
        e.innerHTML = "▽";
    }
}
