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
    var reg = new RegExp(search, "i")
    $(".story_title").each(function (index) {
        fundElement(this, reg, search);
    });
    $(".stroy_Description").each(function (index) {
        fundElement(this, reg, search);
    });

}

function raplaceSearchResults(text, search)
{
    var reg = new RegExp(search, "i")
    text = text.replace(reg, "<span class='search_find'>" + search + "</span>");
    return text;
}

function fundElement(element, reg, search)
{
    var el = $(element);
    var eltext = el.text();
    var n = eltext.search(reg);
    if (n > -1) {
        el.parent().show();
        el.html(raplaceSearchResults(eltext, search));
    }
}