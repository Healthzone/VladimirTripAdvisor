$(".toggle").click(function () {
    $(this).removeClass("d-inline-flex").addClass("d-none");
    $(this).siblings("button").removeClass("d-none").addClass("d-inline-flex");
    $(this).parent("").children("p").toggleClass("truncate");
});