$(document).ready(function () {

    //Sidebar Toggle
    $('.btn-nav-toggle').click(function () {
        $('.site-holder').toggleClass('mini-sidebar');
        $('.hidden-minibar').toggleClass('hide');
        return false;
    });

    //Slide Sidebar in Responsive

    $(".btn-nav-toggle-responsive").click(function(){
        $(".left-sidebar").toggleClass("slide-leftbar")
    });
});