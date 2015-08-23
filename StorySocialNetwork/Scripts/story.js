$(document).on("click", ".close_popup", function () {
    $('#popup_container').empty();
});

$(function () {
    $("#popup_container").draggable({ containment: "parent" });
    $("#stories_list").sortable();
    $("#stories_list").disableSelection();
});

function showLoader() {
    $('#loading').show();
}

function hideLoader() {
    $('#loading').hide();
}

function Search(serachText) {

    var search = $(serachText).val();
    if (search != "") {
        $('.story').hide();
    }
    else {
        $('.story').show();
    }

    $(".story_title").each(function (index) {
        var el = $(this);
        var n = el.text().search(search);
        if (n > -1) {
            el.parent().show();
        }
    });
    $(".stroy_Description").each(function (index) {
        var el = $(this);
        var n = el.text().search(search);
        if (n > -1)
        { el.parent().show(); }
    });

}