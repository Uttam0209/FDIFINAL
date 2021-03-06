﻿
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
    $(".btn-nav-toggle-responsive").click(function () {
        $(".left-sidebar").toggleClass("slide-leftbar")
    });
    //Slide Sidebar Nav
    $(".sidebar-holder .parent-nav > a").click(function () {
        $(this).next().slideToggle();
        $(this).parents('.parent-nav').siblings().find('.parent-nav-child').slideUp();
        //$(this).siblings('.parent-nav-child').slideUp();

        $(this).find('.fa-angle-down').toggleClass('rotate-icon');
    });
    // Add Current Page in Sidebar
    //var CURRENT_URL= window.location.href;
    var CURRENT_URL = window.location.href.split('/').slice(3).join('/');
    var $Sidebar = $(".left-sidebar");
    $Sidebar.find('a[href="' + CURRENT_URL + '"]').parents(".parent-nav-child").slideDown();
    $Sidebar.find('a[href="' + CURRENT_URL + '"]').parents(".parent-nav-child").addClass('active').siblings(".parent-nav-child").removeClass("active");
    $Sidebar.find('a[href="' + CURRENT_URL + '"]').addClass('active');
    console.log(CURRENT_URL);
    //Table Accordian
    $(document).on('click', '.toggle-table-plus', function () {
        $(this).closest("tr").after("<tr class='clone-row'><td colspan='7' style='padding:0;'>" + $(this).next().html() + "</td></tr>")
        $(this).hide();
        $(this).prev('.toggle-table-minus').show();

    });
    $(document).on('click', '.toggle-table-minus', function () {
        $(this).parents('tr').next('.clone-row').remove();
        $(this).hide();
        $(this).next('.toggle-table-plus').show();
    });
    //Alert pop up box   
    function ErrorMssgPopup(data) {
        $("body").addClass('CaptchaError');
        $("#alertPopup").show();
        $("#alertPopup .alertMsg").append(data);
        return false;
    }
    //Hide Alert Pop up
    $('.close_alert').on('click', function () {
        $("body").css('overflow', 'visible');
        $('.alert-overlay-error').hide();
    });
    function SuccessfullPop(data) {
        $("body").addClass('CaptchaError');
        $("#alertPopupS").show();
        $("#alertPopupS .alertMsg").append(data);
        return false;
    }
    //Hide Alert Pop up
    $('.close_alert').on('click', function () {
        $("body").css('overflow', 'visible');
        $('.alert-overlay-successful').hide();
    });
    //Show Hide Contact Details
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
    $('body').on('click', '.showMap', function () {
        $('.map-box').show();
    });
    //Password Show on Click
    $(".toggle-password").on('click', function () {

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
    //Tooltip
    $('[data-toggle="tooltip"]').tooltip();
    // bootstrap Accordion closing parent
    $("body").on('click', '.faq-secion .accordion .card-header h2', function () {
        console.log('testing');
        $(this).parents('.card').siblings().find('.collapse').removeClass('in');
    });
    //Select 2 Dropwdown
    $("#ContentPlaceHolder1_txtcountry").select2({});
    $("#ContentPlaceHolder1_ddlcountry").select2({});
    $("#ddlcountry").select2({});
    $(document).ready(function () {
        BindControls();
    });
    function BindControls() {
        $("#ContentPlaceHolder1_ddlcategroy2").select2({});
        $("#ContentPlaceHolder1_ddllabel2").select2({});
        $("#ContentPlaceHolder1_ddlmastercategory").select2({});
        $("#ContentPlaceHolder1_ddlsubcategory").select2({});
        $("#ContentPlaceHolder1_ddllevel3product").select2({});
        $("#ContentPlaceHolder1_ddlHSNCode").select2({});
        $("#ContentPlaceHolder1_ddlplatform").select2({});
        $("#ContentPlaceHolder1_ddlnomnclature").select2({});
        $("#ContentPlaceHolder1_ddltechnologycat").select2({});
        $("#ContentPlaceHolder1_ddlsubtech").select2({});
        $("#ContentPlaceHolder1_ddltechlevel3").select2({});
        $("#ContentPlaceHolder1_ddlyearofindiginization").select2({});
        //  $("#ContentPlaceHolder1_ddlenduser").select2({});
        $("#ContentPlaceHolder1_ddlmaster").select2({});
        $("#ContentPlaceHolder1_ddlfacotry").select2({});
        $("#ddlsearchkeywordsfilter").select2({});
        $("#ddlcomp").select2({});
        $("#ddldivision").select2({});
        $("#ddlunit").select2({});
        $("#ddlchkenduser").select2({});
        $("#ddldefplatform").select2({});
        $("#ddlnameofdefplat").select2({});
        $("#ddlprodindustrydomain").select2({});
        $("#ddlprodindussubdomain").select2({});
        $("#ContentPlaceHolder1_ddlPincode").select2({});
        $("#ContentPlaceHolder1_ddlstate").select2({});
        $("#ContentPlaceHolder1_ddlCity").select2({});

    }
    var req = Sys.WebForms.PageRequestManager.getInstance();
    req.add_endRequest(function () {
        BindControls();
    });
    //DataTable Jquery
    $(function () { BindGrid(); });
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest
            (function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null)
                { BindGrid(); }
                else
                { } // BindGrid();}
            });
    };
    function BindGrid() {
        $('#ContentPlaceHolder1_gvPrdoct').DataTable();
        $('#ContentPlaceHolder1_gvcompanydetailsave').DataTable();
        $('#ContentPlaceHolder1_gvViewNodalOfficerAdd').DataTable();
        $('#ContentPlaceHolder1_gvViewDesignationSave').DataTable();
        $('#ContentPlaceHolder1_gvCategory').DataTable();
        $('#ContentPlaceHolder1_gvlevel3').DataTable();
        $('#ContentPlaceHolder1_gvVendorDetails').DataTable();
        $('#ContentPlaceHolder1_gvViewNodalOfficertest').DataTable();
        $('#ContentPlaceHolder1_gvnewsadd').DataTable();
        $('#ContentPlaceHolder1_gvmastercategoryupdate').DataTable();
        $('#ContentPlaceHolder1_gvmastersubcategory').DataTable();
        $('#ContentPlaceHolder1_gvcount').DataTable();
        $('#ContentPlaceHolder1_gvproduct').DataTable();
        $('#ContentPlaceHolder1_gvproditemdetail').DataTable();
        $('#ContentPlaceHolder1_gvnsngroup').DataTable();
        $('#ContentPlaceHolder1_gvnsngroupclass').DataTable();
        $('#ContentPlaceHolder1_GridView1').DataTable();
        $('#ContentPlaceHolder1_gvcompanyfeedback').DataTable(); 
        $('#ContentPlaceHolder1_gvProgress').DataTable();
        $('#ContentPlaceHolder1_gvproductremark').DataTable();
    }
    //$('[data-fancybox="Prodgridviewgellry"]').fancybox({
    //    // Options will go here
    //});
    // document.multiselect('#ddlenduser');
    $(function () {
        $('[id*=ddlenduser]').multiselect({
            includeSelectAllOption: false
        });
    });
    //ProCurment Category Checkbox 
    var $checks = $("#ContentPlaceHolder1_gvprocurmentcategory td input[type='checkbox']").change(function () {
        var allChecked = $checks.filter(':checked').length;
        console.log(allChecked);

        if (allChecked > 0) {
            $("#ContentPlaceHolder1_divmake2status").show();
        }
        else {
            $("#ContentPlaceHolder1_divmake2status").hide();
        }

    });

    // Hide show  on basis of selectd tender

    $("#ContentPlaceHolder1_ddlteneoi").on('change', function () {
        var $ValueChecked = $(this).val();
        if ($ValueChecked == '2' || $ValueChecked == '3' || $ValueChecked == '7') {
            $("#ContentPlaceHolder1_divtimedateurl").show();
        }
        else {
            $("#ContentPlaceHolder1_divtimedateurl").hide();
        }

    });

   
    $("#ContentPlaceHolder1_ddlstatus").on('change', function () {
        var $ValueChecked = $(this).val();
        if ($ValueChecked == 'Live') {
            $("#ContentPlaceHolder1_extimedatevisible").show();
        }
        else {
            $("#ContentPlaceHolder1_extimedatevisible").hide();
        }
    });


    //Hide Next in Login Crousel 
    $("#carouselExampleControls .carousel-control-next ").on('click',function () {
        var lastSlideActive = $("#carouselExampleControls .item").last().hasClass("active");
        console.log(lastSlideActive);
        if (lastSlideActive == true) {
            $(this).hide();
        }
        else {
            $(this).show();
        }
    });


   

});



