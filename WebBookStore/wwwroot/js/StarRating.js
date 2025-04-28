document.addEventListener('DOMContentLoaded', function () {
    const stars = document.querySelectorAll('.rating-stars .fa-star');
    const ratingInput = document.getElementById('Rating');
    const ratingText = document.getElementById('rating-value');

    stars.forEach(star => {
        star.addEventListener('click', function () {
            const rating = this.getAttribute('data-index');
            setRating(rating);
            showRating(rating);
        });

        star.addEventListener('mouseover', function () {
            const index = this.getAttribute('data-index');
            highlightStars(index);
        });

        star.addEventListener('mouseout', function () {
            resetStars();
        });
    });

    function setRating(rating) {
        ratingInput.value = rating;
        highlightStars(rating);
    }

    function showRating(rating) {
        ratingText.innerText = `Bạn đã chọn ${rating} sao.`;
    }

    function highlightStars(index) {
        stars.forEach(star => {
            if (parseInt(star.getAttribute('data-index')) <= parseInt(index)) {
                star.classList.remove('text-muted');
                star.classList.add('text-warning');
            } else {
                star.classList.remove('text-warning');
                star.classList.add('text-muted');
            }
        });
    }

    function resetStars() {
        const rating = ratingInput.value;
        highlightStars(rating);
    }
});
