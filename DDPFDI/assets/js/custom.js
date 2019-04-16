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

    $(".sidebar-holder .parent-nav > a").click(function(){
        $(this).next().slideToggle();
        $(this).parents('.parent-nav').siblings().find('.parent-nav-child').slideUp();

        $(this).find('.fa-angle-down').toggleClass('rotate-icon');
    });

// Add Current Page in Sidebar
    var CURRENT_URL= window.location.pathname;
    var $Sidebar = $(".left-sidebar");
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').parents(".parent-nav-child").slideDown();
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').parents("li").addClass('active').siblings("li").removeClass("active");
    if(CURRENT_URL ===! '/DDPFDI/Dashboard') {
        $('#main-admin').next('.parent-nav-child').slideDown();
    }

    console.log(CURRENT_URL);

    //Hide Show Chart

    $(".GraphType").on('change', function () {
        var $Chartvalue = $(this).val();
        console.log($Chartvalue);
        if ($Chartvalue == 'Pie Chart') {
            $("#divPieChart").show();
            $("#divLineChart").hide();
        }

        else if ($Chartvalue == 'Line Chart') {
            $("#divLineChart").show();
            $("#divPieChart").hide();
        }

    });


});