<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="h" runat="server" ContentPlaceHolderID="head">
    <%-- <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" />
    <link href="assetsTest/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assetsTest/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="assetsTest/global/plugins/datatables/datatables.min.css" rel="stylesheet" />
    <link href="assetsTest/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="assetsTest/global/css/components.min.css" rel="stylesheet" id="style_components" />
    <link href="assetsTest/global/css/plugins.min.css" rel="stylesheet" />
    <!-- END THEME GLOBAL STYLES -->

    <link rel="shortcut icon" href="favicon.ico" />--%>


    <%--<link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet">--%>
    <%--<script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>--%>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <!------ Include the above in your HEAD tag ---------->

    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">--%>
    <%--<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

    <style>
        .custom-combobox {
            position: relative;
            display: inline-block;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
        }

        .custom-combobox-input {
            margin: 0;
            padding-top: 2px;
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
                <div class="col-md-12">
                    <div class="clearfix"></div>
                    <div style="margin-top: 5px;">
                        <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="container">

                <div class="row">
                    <div class="ui-widget">
                        <label>Procedure: </label>
                        <select id="combobox">
                            <option></option>
                            <option value="Ultrasound Knee Right">Ultrasound Knee Right</option>
                            <option value="Ultrasound Knee Left">Ultrasound Knee Left</option>
                            <option value="Ultrasound Forearm/Elbow Right">Ultrasound Forearm/  Elbow Right</option>
                            <option value="Ultrasound Forearm/Elbow Left">Ultrasound Forearm/Elbow Left</option>
                            <option value="MRI Knee Right">MRI Knee Right</option>
                            <option value="MRI Knee Left">MRI Knee Left</option>
                            <option value="MRI Forearm/Elbow Right">MRI Forearm/Elbow Right</option>
                            <option value="MRI Forearm/Elbow Left">MRI Forearm/Elbow Left</option>
                            <option value="CT Knee Right">CT Knee Right</option>
                            <option value="CT Knee Left">CT Knee Left</option>
                            <option value="CT Forearm/Elbow Right">CT Forearm/Elbow Right</option>
                            <option value="CT Forearm/Elbow Left">CT Forearm/Elbow Left</option>
                        </select>
                    </div>
                </div>
            </div>
            <%--<div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet light portlet-fit bordered">
                    <div class="portlet-title">
                    </div>
                    <div class="portlet-body">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group">
                                        <button id="sample_editable_1_new" class="btn green">
                                            Add New
											<i class="fa fa-plus"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                            <thead>
                                <tr>
                                    <th>Username </th>
                                    <th>Full Name </th>
                                    <th>Notes </th>
                                    <th>Edit </th>
                                    <th>Delete </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>alex </td>
                                    <td>Alex Nilson </td>
                                    
                                    <td class="center">power user </td>
                                    <td>
                                        <a class="edit" href="javascript:;">Edit </a>
                                    </td>
                                    <td>
                                        <a class="delete" href="javascript:;">Delete </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>lisa </td>
                                    <td>Lisa Wong </td>
                                   
                                    <td class="center">new user </td>
                                    <td>
                                        <a class="edit" href="javascript:;">Edit </a>
                                    </td>
                                    <td>
                                        <a class="delete" href="javascript:;">Delete </a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <!-- END EXAMPLE TABLE PORTLET-->
            </div>
        </div>
        <script src="assetsTest/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="assetsTest/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="assetsTest/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="assetsTest/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
        <script src="assetsTest/pages/scripts/table-datatables-editable.min.js"></script>--%>
            <script>
                $(function () {
                    $.widget("custom.combobox", {
                        _create: function () {
                            this.wrapper = $("<span>")
                              .addClass("custom-combobox")
                              .insertAfter(this.element);

                            this.element.hide();
                            this._createAutocomplete();
                            this._createShowAllButton();
                        },

                        _createAutocomplete: function () {
                            var selected = this.element.children(":selected"),
                              value = selected.val() ? selected.text() : "";

                            this.input = $("<input>")
                              .appendTo(this.wrapper)
                              .val(value)
                              .attr("title", "")
                              .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                              .autocomplete({
                                  delay: 0,
                                  minLength: 0,
                                  source: $.proxy(this, "_source")
                              })
                              .tooltip({
                                  classes: {
                                      "ui-tooltip": "ui-state-highlight"
                                  }
                              });

                            this._on(this.input, {
                                autocompleteselect: function (event, ui) {
                                    ui.item.option.selected = true;
                                    this._trigger("select", event, {
                                        item: ui.item.option
                                    });
                                },

                                autocompletechange: "_removeIfInvalid"
                            });
                        },

                        _createShowAllButton: function () {
                            var input = this.input,
                              wasOpen = false

                            $("<a>")
                              .attr("tabIndex", -1)
                              .attr("title", "Show All Items")
                              .attr("height", "")
                              .tooltip()
                              .appendTo(this.wrapper)
                              .button({
                                  icons: {
                                      primary: "ui-icon-triangle-1-s"
                                  },
                                  text: "false"
                              })
                              .removeClass("ui-corner-all")
                              .addClass("custom-combobox-toggle ui-corner-right")
                              .on("mousedown", function () {
                                  wasOpen = input.autocomplete("widget").is(":visible");
                              })
                              .on("click", function () {
                                  input.trigger("focus");

                                  // Close if already visible
                                  if (wasOpen) {
                                      return;
                                  }

                                  // Pass empty string as value to search for, displaying all results
                                  input.autocomplete("search", "");
                              });
                        },

                        _source: function (request, response) {
                            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                            response(this.element.children("option").map(function () {
                                var text = $(this).text();
                                if (this.value && (!request.term || matcher.test(text)))
                                    return {
                                        label: text,
                                        value: text,
                                        option: this
                                    };
                            }));
                        },

                        _removeIfInvalid: function (event, ui) {

                            // Selected an item, nothing to do
                            if (ui.item) {
                                return;
                            }

                            // Search for a match (case-insensitive)
                            var value = this.input.val(),
                              valueLowerCase = value.toLowerCase(),
                              valid = false;
                            this.element.children("option").each(function () {
                                if ($(this).text().toLowerCase() === valueLowerCase) {
                                    this.selected = valid = true;
                                    return false;
                                }
                            });

                            // Found a match, nothing to do
                            if (valid) {
                                return;
                            }

                            // Remove invalid value
                            this.input
                              .val("")
                              .attr("title", value + " didn't match any item")
                              .tooltip("open");
                            this.element.val("");
                            this._delay(function () {
                                this.input.tooltip("close").attr("title", "");
                            }, 2500);
                            this.input.autocomplete("instance").term = "";
                        },

                        _destroy: function () {
                            this.wrapper.remove();
                            this.element.show();
                        }
                    });

                    $("#combobox").combobox();
                    $("#toggle").on("click", function () {
                        $("#combobox").toggle();
                    });
                });
            </script>
        </div>
    </div>
</asp:Content>

