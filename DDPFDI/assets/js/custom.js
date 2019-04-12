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

    //Slide Sidebar Nav

    $(".sidebar-holder .parent-nav").click(function(){
        $(this).find('.parent-nav-child').slideToggle();
        $(this).siblings().find('.parent-nav-child').slideUp();

        $(this).find('.fa-angle-down').toggleClass('rotate-icon');
    });

// Add Current Page in Sidebar
    var CURRENT_URL=window.location.pathname;
    var pathName = CURRENT_URL.slice(1);

    var $Sidebar = $(".left-sidebar");
    $Sidebar.find('a[href="'+pathName+'"]').parents(".parent-nav-child").show();
    console.log(pathName);


});