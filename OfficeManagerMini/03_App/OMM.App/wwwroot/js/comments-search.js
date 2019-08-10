$('#comments-search-bar').on("change paste keyup", function () {
    $('.comment').show();

    let searchValue = $('#comments-search-bar').val();

    for (let elem of $('.comment')) {
        let commentHeader = $($(elem).children()[0]).text();
        let commentDescription = $($(elem).children()[1]).text();

        if (!commentHeader.toLowerCase().includes(searchValue.toLowerCase()) && !commentDescription.toLowerCase().includes(searchValue.toLowerCase())) {
            $(elem).hide();
        }
    }
});