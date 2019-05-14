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


    // Add Current Page in Sidebar
    //var CURRENT_URL= window.location.href;
    var CURRENT_URL= window.location.href.split('/').slice(3).join('/');
    var $Sidebar = $(".left-sidebar");
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').parents(".parent-nav-child").slideDown();
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').parents(".parent-nav-child").addClass('active').siblings(".parent-nav-child").removeClass("active");
    $Sidebar.find('a[href="'+CURRENT_URL+'"]').addClass('active');
    console.log(CURRENT_URL);

//Hide Show Chart

    //$(".GraphType").on('change', function () {
    //    var $Chartvalue = $(this).val();
    //    console.log($Chartvalue);
    //    if ($Chartvalue == 'Pie Chart') {
    //        $("#divPieChart").show();
    //        $("#divLineChart").hide();
    //    }

    //    else if ($Chartvalue == 'Line Chart') {
    //        $("#divLineChart").show();
    //        $("#divPieChart").hide();
    //    }

    //});


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
           $(".Turl_Tdate").show();

       }
       else {
           $(".Turl_Tdate").hide();
       }
    });

//Alert pop up box
 function ShowMessage(){
    $("body").css('overflow','hidden');
    $('.alert-overlay').show();
}
//Show Hide Contact Details
//Sys.Application.add_load(BindFunction);

    $('body').on('click', '.showMoreLink', function () {

        $(this).parents('.section-pannel').find('.contactFormRow').slideToggle();
        var $ToggleText = $(this).text();

        if ($ToggleText === "Show Details") {
            $($(this)).text('Hide Details');
        }
        else {
            $($(this)).text('Show Details');
        }
    });

     //Show Map

    $('body').on('click','.showMap',function(){
        $('.map-box').show();    
    });

//Password Show on Click

$(".toggle-password").on('click',function() {
 
  $(this).toggleClass("fa-eye fa-eye-slash");
  var input = $(".passField");
  if (input.attr("type") == "password") {
    input.attr("type", "text");
  } else {
    input.attr("type", "password");
  }
});


//image Uplodad Functionality
var count = 0;
        function handleFileSelect(evt) {
            var $fileUpload = $("input#files[type='file']");
            count = count + parseInt($fileUpload.get(0).files.length);

            if (parseInt($fileUpload.get(0).files.length) > 8 || count > 9) {
                alert("You can only upload a maximum of 8 files");
                count = count - parseInt($fileUpload.get(0).files.length);
                evt.preventDefault();
                evt.stopPropagation();
                return false;
            }
            var files = evt.target.files;
            for (var i = 0, f; f = files[i]; i++) {
                if (!f.type.match('image.*')) {
                    continue;
                }
                var reader = new FileReader();
                reader.onload = (function (theFile) {
                    return function (e) {
                        var span = document.createElement('span');
                        span.innerHTML = ['<img class="thumb" src="', e.target.result, '" title="', escape(theFile.name), '"/><span class="remove_img_preview"></span>'].join('');
                        document.getElementById('list').insertBefore(span, null);
                    };
                })(f);

                reader.readAsDataURL(f);
            }
        }
        $('#files').change(function (evt) {
            handleFileSelect(evt);
        });
        $('#list').on('click', '.remove_img_preview', function () {
            $(this).parent('span').remove();
            //           parseInt($fileUpload.get(0).files.length - 1;
        });
});
