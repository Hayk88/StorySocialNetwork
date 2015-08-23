
isStoriesCount = false;
isUserCount = false;

$(document).on("click", ".close_popup", function () {
    $('#popup_container').empty();
    hideBack();
});
$(document).on("click", ".stories_count_sort", function () {
    var usersCounts = [];
    var groups = $(".groups_row_container .group");
    $(".groups_row_container .group").each(function (index) {
        usersCounts[usersCounts.length] = parseFloat($(this).find('.stories_count').attr('count'));
    });
    var sortGroupe = usersCounts.sort();
    $('.groups_row_container').empty();

    if (isStoriesCount) {
        sortGroupe.reverse();
        isStoriesCount = false;
    }
    else {
        isStoriesCount = true;
    }
    for (var i = 0; i < sortGroupe.length; i++) {
        var el = groups.find(".stories_count[count='" + sortGroupe[i] + "']");
        $('.groups_row_container').append(el.parent());
    }

});

$(document).on("click", ".users_count_sort", function () {
    var usersCounts = [];
    var groups = $(".groups_row_container .group");
    $(".groups_row_container .group").each(function (index) {
        usersCounts[usersCounts.length] = parseFloat($(this).find('.users_count').attr('count'));
    });
    var sortGroupe = usersCounts.sort();
    $('.groups_row_container').empty();
    

    if (isUserCount) {
        sortGroupe.reverse();
        isUserCount = false;
    }
    else {
        isUserCount = true;
    }
    for (var i = 0; i < sortGroupe.length; i++) {
        var el = groups.find(".users_count[count='" + sortGroupe[i] + "']");
        $('.groups_row_container').append(el.parent());
    }

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

function showBack() {
    $('#back').show();
}

function hideBack() {
    $('#back').hide();
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
