<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" />
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

    <link rel="shortcut icon" href="favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
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
        <script src="assetsTest/pages/scripts/table-datatables-editable.min.js"></script>
    </form>
</body>
</html>


