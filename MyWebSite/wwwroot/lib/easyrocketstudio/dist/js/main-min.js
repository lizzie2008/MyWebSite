$(document).ready(function() {
    fetchScroll();
    page();
    function page() {
        var windowWidth = $(window).width(), documentHeight = $(document).height(), windowHeight = $(window).height();
        if (windowWidth < 1280) {
            $(".page").width(1280);
        } else {
            $(".page").width(windowWidth);
        }
    }
    $(window).resize(function() {
        page();
    });
    $(".j-btn-menu").on("click", function(e) {
        e.preventDefault();
        $(".module").toggleClass("is-open");
        var menu = $(this).data("menu");
        $("#" + menu).slideToggle("fast");
    });
    $(".module").hover(function() {
        var $this = $(this);
        $this.find(".info-short").stop().css({
            opacity:0
        }, 500);
        $this.find(".info-full").stop().slideDown();
    }, function() {
        var $this = $(this);
        $this.find(".info-full").stop().slideUp("fast");
        $this.find(".info-short").stop().animate({
            opacity:1
        }, 500);
    });
});

$(window).scroll(function() {
    fetchScroll();
});

function fetchScroll() {
    playstop(M);
    //playstop(A);
    if (!isMobile) {
        moveAsteroids();
        moveSpaceships();
    }
}

function playstop(scene) {
    if (isScrolledIntoView($(scene.NAME))) scene.play(); else scene.stop();
}

function isScrolledIntoView(elem) {
    var docViewTop = $(window).scrollTop();
    var docViewBottom = docViewTop + $(window).height();
    var elemTop = $(elem).offset().top;
    var elemBottom = elemTop + $(elem).height();
    return docViewBottom >= elemTop && docViewTop <= elemBottom;
}

$(".to-second").on("click", function(e) {
    e.preventDefault();
    var offset = $(".screen-2").offset().top;
    $("html, body").animate({
        scrollTop:offset - 50
    }, 2e3);
});

$("#site-menu a").on("click", function(e) {
    e.preventDefault();
    var screenNumber = $(this).data("screen"), scrollTo = $(".screen-" + screenNumber), offset = scrollTo.offset().top;
    $("html, body").animate({
        scrollTop:offset - 50
    }, 2e3);
});