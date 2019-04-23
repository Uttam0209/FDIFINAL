$(document).ready(function () {

    //Sidebar Toggle
    $('.btn-nav-toggle').click(function () {
        $('.site-holder').toggleClass('mini-sidebar');
        $('.hidden-minibar').toggleClass('hide');
        $('.toggle-left').toggleClass('rotate-toggle-btn');
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


$(window).on('load', function(){
    // Add Current Page in Sidebar
    var CURRENT_URL= window.location.href.split('/').slice(4).join('/');
    var $Sidebar = $(".left-sidebar");
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').parents(".parent-nav-child").slideDown();
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').parents(".parent-nav-child").addClass('active').siblings(".parent-nav-child").removeClass("active");
    console.log(CURRENT_URL);
    
});


   

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


    //Table Accordian
        $(".fa-plus").on("click", function () {
            $(this).closest("tr").after("<tr class='clone-row'><td></td><td colspan='9'>" + $(this).next().html() + "</td></tr>")
            $(this).addClass('fa-minus'); 
            $(this).removeClass('fa-plus'); 
           console.log('test');        
           

        });
 

});