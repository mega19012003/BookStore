document.addEventListener('DOMContentLoaded', function () {
    const stars = document.querySelectorAll('.rating-stars i');
    stars.forEach(star => {
        star.addEventListener('mouseover', function () {
            const index = parseInt(this.getAttribute('data-index'));
            highlightStars(index);
        });

        star.addEventListener('mouseout', function () {
            resetStars();
        });

        star.addEventListener('click', function () {
            const index = parseInt(this.getAttribute('data-index'));
            saveRating(index);
        });
    });

    function highlightStars(index) {
        stars.forEach(star => {
            const starIndex = parseInt(star.getAttribute('data-index'));
            star.classList.remove('text-muted');
            star.classList.toggle('text-warning', starIndex <= index);
            star.classList.toggle('text-muted', starIndex > index);
        });
    }

    function resetStars() {
        stars.forEach(star => {
            star.classList.remove('text-warning');
            star.classList.add('text-muted');
        });
    }

    function saveRating(index) {
        console.log("Bạn đã đánh giá: " + index + " sao");
        // TODO: Xử lý lưu đánh giá tại đây nếu muốn
    }
});
