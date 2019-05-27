<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterdAs.aspx.cs" Inherits="Admin_RegisterdAs" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" ChildrenAsTriggers="True">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="col-mod-12">
                        <asp:HiddenField runat="server" ID="hfCatID" />
                        <asp:HiddenField runat="server" ID="hfSubCatID" />
                        <asp:HiddenField runat="server" ID="hftype" />
                        <div class="row">
                            <div class="col-md-12 padding_0">
                                <div id="divHeadPage" runat="server"></div>
                            </div>
                        </div>
                        <div class="UserInnerpage">
                            <div class="resitered">
                                <div class="section-pannel">
                                    <div class="row">
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsave">
                                            <div class="col-md-4" runat="server" id="divcategory1textbox" visible="False">
                                                <div class="form-group">
                                                    <label>Add Dropdown Label <span class="mandatory">*</span></label>
                                                    <asp:TextBox class="form-control" required="" TabIndex="1" runat="server" ID="txtmastercategory"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="divflag" visible="False">
                                                <div class="form-group">
                                                    <label style="display: block">&nbsp;</label>
                                                    <asp:RadioButtonList ID="rbflag" runat="server" TabIndex="2" RepeatLayout="Flow" RepeatDirection="Horizontal" RepeatColumns="3">
                                                        <asp:ListItem Value="1" Selected="True">Level 1 </asp:ListItem>
                                                        <asp:ListItem Value="2">Level 2 </asp:ListItem>
                                                        <asp:ListItem Value="3">Level 3 </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="divActive" visible="False">
                                                <div class="form-group">
                                                    <label style="display: block">&nbsp;</label>
                                                    <asp:RadioButtonList ID="rbactive" runat="server" TabIndex="3" RepeatLayout="Flow" RepeatDirection="Horizontal" RepeatColumns="2">
                                                        <asp:ListItem Value="Y" Selected="True">Visible</asp:ListItem>
                                                        <asp:ListItem Value="N">Hidden</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="divcategory1dropdown" visible="False">
                                                <div class="form-group">
                                                    <label>Dropdown Label <span class="mandatory">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddlmastercategory" TabIndex="4" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="divcategory2textbox" visible="False">
                                                <div class="form-group">
                                                    <label>Add Level 1 <span class="mandatory">*</span></label>
                                                    <asp:TextBox class="form-control" required="" TabIndex="5" runat="server" ID="txtsubcategory"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="divcategory2ddl" visible="False">
                                                <div class="form-group">
                                                    <label>Select Level 1 <span class="mandatory">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddlcategroy2" TabIndex="6" AutoPostBack="False" class="form-control" OnSelectedIndexChanged="ddlcategroy2_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="divcategory3textbox" visible="False">
                                                <div class="form-group">
                                                    <label>Add Level 2 <span class="mandatory">*</span></label>
                                                    <asp:TextBox class="form-control" runat="server" TabIndex="7" ID="txtcategory3"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="divlabel2drop" visible="False">
                                                <div class="form-group">
                                                    <label>Select Label 2 <span class="mandatory">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddllabel2" class="form-control" TabIndex="8" AutoPostBack="True" OnSelectedIndexChanged="ddllabel2_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="divlevel3" visible="False">
                                                <div class="form-group">
                                                    <label>Add Level 3 <span class="mandatory">*</span></label>
                                                    <asp:TextBox class="form-control" required="" TabIndex="9" runat="server" ID="txtlevel3"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:LinkButton ID="btncancle" runat="server" class="btn btn-danger pull-right" Text="Cancel" Style="margin-right: 0 !important;" OnClick="btncancle_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnsave" runat="server" Text="Save Label" class="btn btn-primary pull-right" OnClick="btnsave_Click" OnClientClick="return confirm('Are you sure you want to save this dropdown?');"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="table-wraper" runat="server" id="divmastercategory">
                                    <asp:GridView ID="gvCategory" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                        PageSize="25" AllowSorting="true">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="MCategoryName" HeaderText="Dropdown Label" />
                                            <asp:TemplateField HeaderText="Hierarchy">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Flag") %>' NullDisplayText="#"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("IsActive") %>' NullDisplayText="#"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="CreatedBy" HeaderText="Create By" NullDisplayText="#" />
                                            <%--     <asp:BoundField ItemStyle-Width="150px" DataField="SCategoryName" HeaderText="FLevel 2" />--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="table-wrapper" id="divinnercat" runat="server">
                                    <asp:GridView ID="gvlevel3" runat="server" AutoGenerateColumns="false" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" OnRowDataBound="gvlevel3_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField runat="server" DataField="SCategoryName" NullDisplayText="#" HeaderText="Level 1" />
                                            <%--   <asp:BoundField runat="server" DataField="SCategoryName" NullDisplayText="#" HeaderText="Level 2" />
                                            <asp:BoundField runat="server" DataField="SCategoryName" NullDisplayText="#" HeaderText="Level 3" />--%>
                                            <asp:BoundField runat="server" DataField="CreatedBy" HeaderText="Created By" NullDisplayText="#" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
        <ProgressTemplate>
            <!---Progress Bar ---->
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p>Processing</p>
                </div>
            </div>
            <!---Progress Bar ---->
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
