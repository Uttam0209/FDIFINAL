$(document).ready(function () {

    //Sidebar Toggle
    $('.btn-nav-toggle').click(function () {
        $('.left-sidebar').toggleClass('sidebar-collapse');
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
        //$(this).siblings('.parent-nav-child').slideUp();

        $(this).find('.fa-angle-down').toggleClass('rotate-icon');
    });


$(window).on('load', function(){
    // Add Current Page in Sidebar
    var CURRENT_URL= window.location.href.split('/').slice(4).join('/');
    var $Sidebar = $(".left-sidebar");
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').parents(".parent-nav-child").slideDown();
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').parents(".parent-nav-child").addClass('active').siblings(".parent-nav-child").removeClass("active");
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').addClass('active');
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
        $(document).on('click', '.toggle-table-plus', function () {
            $(this).closest("tr").after("<tr class='clone-row'><td colspan='7' style='padding:0;'>" + $(this).next().html() + "</td></tr>")
            $(this).hide();
            $(this).prev('.toggle-table-minus').show();
           
        });

        $(document).on('click','.toggle-table-minus', function() {
            $(this).parents('tr').next('.clone-row').remove();
            $(this).hide();
            $(this).next('.toggle-table-plus').show();
        });

// Hide Show when checkbox checked

    $(".live-status-box input[type='radio']").on('change', function(){
        var $inputChecked = $('.live-status-box .yes').is(":checked");
       if($inputChecked == true) {
           $(".live-status-box .inputbox").show();

       }
       else {
           $(".live-status-box .inputbox").hide();
       }
    });

    //Show Map

    $('.showMap').click(function(){
        $('.map-box').show();

    });

//Show Hide Contact Details
$(".showMore i").click(function(){
    console.log('clicked');
    $(this).parents('.section-pannel').find('.contactFormRow').slideToggle();
    $(this).toggleClass('fa-minus');
});

});