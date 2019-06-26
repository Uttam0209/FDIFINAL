<%@ Page Language="C#" AutoEventWireup="true" CodeFile="faq.aspx.cs" MasterPageFile="~/Admin/MasterPage.master" Inherits="Admin_faq" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                    <div id="ContentPlaceHolder1_divHeadPage">
                        <ul class="breadcrumb">
                            <li class=""><span>FAQs</span></li>

                        </ul>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="clearfix"></div>
                    <div style="margin-top: 5px;">
                        <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="clearfix" style="margin-bottom: 10px;"></div>
            <div class="row">
                <div class="col-md-12">
                    <div class="faq-secion">


                        <div class="accordion" id="accordion">
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" id="headingOne" data-parent="#accordion" data-target="#faq1" aria-expanded="true" aria-controls="faq1">Preface 
                                            <i class="fa fa-minus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq1" class="collapse in" aria-labelledby="headingOne">
                                    <div class="card-body">
                                        <p>Policy for Indigenization of components and spares used in defence Platforms for DPSUs /OFB have been issued vide Department of Defence Production notification no. 01(18)/02/.  The Indigenization Portal is mentioned in the clause 3.6 of the policy. The portal as per the policy provides following services:- </p>
                                        <ol>
                                            <li>List of Items to be indigenized – The portal vide Indigenization tab allows DPSUs/OFB to add Items through add product functionality. </li>
                                            <li>Details of Items – The portal display all the relevant information added by the   DPSUs/OFB in the view product functionality.</li>
                                            <li>Registration of Vendors - The portal vide Industry tab allows Admin to add vendors through add industry functionality. </li>
                                            <li>Enabling Defence PSUs and Ordnance Factories to search if  similar component have been indigenized earlier – The description column of Add Product category contain provision for addition of above information </li>
                                            <li>Providing details of Facilitation Centres – to be Done </li>
                                            <li>Identifying & Listing Test Centres – to be Done </li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq2" aria-expanded="false" aria-controls="faq2">Table of Contents
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq2" class="collapse">
                                    <div class="card-body">
                                        <ol>
                                            <li>Company Master 
                                                <ol style="list-style: lower-alpha">
                                                    <li>ADD  DIVISION /PLANT</li>
                                                    <li>ADD  UNIT </li>
                                                    <li>ADD DESIGNATION </li>
                                                    <li>EDIT COMPANY 
                                                        <ol style="list-style: lower-roman">
                                                            <li>EDIT COMPANY </li>
                                                            <li>EDIT DIVISIONS/PLANTS </li>
                                                            <li>EDIT UNIT </li>
                                                        </ol>
                                                    </li>
                                                    <li>EDIT DESIGNATION </li>
                                                    <li>EDIT EMPLOYEE </li>
                                                </ol>

                                            </li>
                                            <li>Category Master </li>
                                            <li>Indigenisation</li>

                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq3" aria-expanded="false" aria-controls="faq3">1.Company Master
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq3" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            The Indenisation portal will have the functionality where a company Nodal Officer is able to add division and units along with designation  and employee.
                                            The steps for adding division and units along with designation  and employee are given below :- 
                                        </p>
                                        <div class="faq-img">
                                            <img src="../assets/images/faq-images/Company-master.jpg" alt="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq4" aria-expanded="false" aria-controls="faq4">1.1 Add  Division/Plant 
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq4" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Division /Plant Name:- 
                                        </p>
                                        <ol>
                                            <li>Click on Company Master Tab at the left hand corner of the portal after login.</li>
                                            <li>Select Add tab</li>
                                            <li>Select Division /Plant tab </li>
                                            <li>Enter name of Division /Plant  on Space given below Division/ Plant </li>
                                            <li>Press save for the Division /Plant Name to add in the portal. 
                                                    <div class="faq-img">
                                                        <img src="../assets/images/faq-images/Add-plant.jpg" alt="" />
                                                    </div>

                                            </li>
                                            <li>The Name will be Displayed in the Matrix after save button is clicked. 
                                                    <div class="faq-img">
                                                        <img src="../assets/images/faq-images/display-metrix.jpg" alt="" />
                                                    </div>
                                            </li>

                                        </ol>


                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq5" aria-expanded="false" aria-controls="faq5">1.2 Add Unit
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq5" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Unit Name:- 
                                        </p>
                                        <ol>
                                            <li>Click on Company Master Tab at the left hand corner of the portal after login. 
                                            </li>
                                            <li>Select Add tab </li>
                                            <li>Select Unit  tab</li>
                                            <li>Enter name of Division /Plant  on Scroll Down bar given below Division/ Plant </li>
                                            <li>Enter name of Unit on Space given below Unit, </li>
                                            <li>Press save for the Division /Plant Name to add in the portal. 
                                               <div class="faq-img">
                                                   <img src="../assets/images/faq-images/add-unit.jpg" alt="" />
                                               </div>

                                            </li>
                                            <li>The Name will be Displayed in the Matrix after save button is clicked.
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/save-edited-unit.jpg" alt="" />
                                                </div>
                                            </li>

                                        </ol>

                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq6" aria-expanded="false" aria-controls="faq6">1.3 Add Designation
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq6" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Designation:- 
                                        </p>
                                        <ol>
                                            <li>Click on Company Master Tab at the left hand corner of the portal after login. 
                                            </li>
                                            <li>Select Add tab </li>
                                            <li>Select Designation tab </li>
                                            <li>Enter name of Designation on Space given below Designation,</li>
                                            <li>Press save for the Designation Name to add in the portal.
                                                  <div class="faq-img">
                                                      <img src="../assets/images/faq-images/add-disignation.jpg" alt="" />
                                                  </div>
                                            </li>
                                            <li>The Name will be Displayed in the Matrix after save button is clicked. 
                                                  <div class="faq-img">
                                                      <img src="../assets/images/faq-images/designation_display.jpg" alt="" />
                                                  </div>
                                            </li>
                                        </ol>

                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq7" aria-expanded="false" aria-controls="faq7">1.4 Add Employee
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq7" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Employee:- 
                                        </p>
                                        <ol>
                                            <li>Click on Company Master Tab at the left hand corner of the portal after login.  
                                            </li>
                                            <li>Select Add tab  </li>
                                            <li>Select Employee tab </li>
                                            <li>To Add Employee in Company , fill  details Name ,Designation ,Employee Code , Email Id, Mobile, Telephone, and Fax and choose whether the employee is Nodal officer or User. </li>
                                            <li>To Add Employee in Division/Plant select Division/plant from drop down toolbar below “Select Division/plant” ,  then fill  details Name ,Designation ,Employee Code ,Email Id ,Mobile ,Telephone, Fax and choose whether the employee is Nodal officer or User. 
                                            </li>
                                            <li>To Add Employee in Unit first  select Division/plant from drop down toolbar below “Select Division/plant” ,  next select Unit from drop down toolbar below “Select Unit” , then fill  details Name ,Designation ,Employee Code ,Email Id ,Mobile ,Telephone, Fax and choose whether the employee is Nodal officer or User 
.  
                                                  <div class="faq-img">
                                                      <img src="../assets/images/faq-images/add-employee.jpg" alt="" />
                                                  </div>
                                            </li>
                                            <li>Press save for the Employee Name to add in the portal. 
.  
                                                  <div class="faq-img">
                                                      <img src="../assets/images/faq-images/save-employee.jpg" alt="" />
                                                  </div>
                                            </li>
                                        </ol>

                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq8" aria-expanded="false" aria-controls="faq8">1.5 Edit Company
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq8" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            This functionality provides for editing Company, Division/Plant and Unit details.
                                        </p>
                                        <h3>1.5. A. Steps to Edit Company Details:- </h3>
                                        <ol>
                                            <li>Click on Company Master Tab at the left hand corner of the portal after login.   
                                            </li>
                                            <li>Select Edit tab </li>
                                            <li>Select Company tab</li>
                                            <li>Click on Edit or update detail of company given under Action heading </li>
                                            <li>Edit desired headings via Address, CEO Name etc. 
                                            </li>
                                            <li>Press save for adding edited details in the company. 
.  
                                                  <div class="faq-img">
                                                      <img src="../assets/images/faq-images/edit-company.jpg" alt="" />
                                                  </div>
                                            </li>
                                            <li>
                                                <h3>1.5. B.Steps to Edit Division Details:- </h3>
                                                <ol>
                                                    <li>Click on Company Master Tab at the left hand corner of the portal after login. 
                                                    </li>
                                                    <li>Select Edit tab.  
                                                    </li>
                                                    <li>Select Company tab. 
                                                    </li>
                                                    <li>Select + given in front of company to get all the division under company. 
                                                         <div class="faq-img">
                                                             <img src="../assets/images/faq-images/edit-division.jpg" alt="" />
                                                         </div>
                                                    </li>
                                                    <li>Click on Edit or update detail of Division given under Action heading </li>
                                                    <li>Edit desired headings viz Address, Division Head Name etc. </li>
                                                    <li>Press save for adding edited details in the Division.
                                                          <div class="faq-img">
                                                              <img src="../assets/images/faq-images/save-edited-division.jpg" alt="" />
                                                          </div>

                                                    </li>
                                                </ol>
                                                .  
                                                  
                                            </li>
                                        </ol>

                                        <h3>1.5.C. Steps to Edit UNIT Details :- </h3>
                                        <ol>
                                            <li>Click on Company Master tab at the left hand corner of the portal after login. </li>
                                            <li>Select Edit tab . </li>
                                            <li>Select Company tab. </li>
                                            <li>Select + given in front of company to get all the division under company. Then Select + given in front of Division to get all the Units under Division. 
                                                 <div class="faq-img">
                                                     <img src="../assets/images/faq-images/edit-unit.jpg" alt="" />
                                                 </div>
                                            </li>
                                            <li>Click on Edit or update detail of Unit given under Action heading </li>
                                            <li>Edit desired headings viz Address, Unit Head Name etc. </li>
                                            <li>Press save for adding edited details in the Unit. 
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/save-edited-unit.jpg" alt="" />
                                                </div>

                                            </li>
                                        </ol>

                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq9" aria-expanded="false" aria-controls="faq9">1.6 Edit Designation
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq9" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            This functionality provides for editing Designation name. 
                                        </p>
                                        <p>Steps to Edit Designation Details:- </p>
                                        <ol>
                                            <li>Click on Company Master Tab at the left hand corner of the portal after login.  
                                            </li>
                                            <li>Select Edit tab</li>
                                            <li>Select Designation tab.
                                                
                                                  <div class="faq-img">
                                                      <img src="../assets/images/faq-images/edit-designation.jpg" alt="" />
                                                  </div>
                                            </li>
                                            <li>Click on Edit name of designation given under Action heading 
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/save-edited-designation.jpg" alt="" />
                                                </div>

                                            </li>

                                        </ol>

                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq10" aria-expanded="false" aria-controls="faq10">1.7 Edit Employee
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq10" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            This functionality provides for editing Designation name.  
                                        </p>
                                        <p>Steps to Edit Employee Details :- </p>
                                        <ol>
                                            <li>Click on Company Master tab at the left hand corner of the portal after login.   
                                            </li>
                                            <li>Select Edit tab</li>
                                            <li>Select Employee tab. 
                                                
                                                  <div class="faq-img">
                                                      <img src="../assets/images/faq-images/edit-employee.jpg" alt="" />
                                                  </div>
                                            </li>
                                            <li>By View option , employee details can be viewed . 
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/view-employee-details.jpg" alt="" />
                                                </div>

                                            </li>
                                            <li>By Edit option , Employee detail can be Edited
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/edit-employee-details.jpg" alt="" />
                                                </div>
                                            </li>
                                            <li>By Send Password Option, Password mail will be sent to employee registered Id.</li>

                                        </ol>

                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq11" aria-expanded="false" aria-controls="faq11">2.Category Master
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq11" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Indigenisation portal captures Product information through Product Master tab. 
                                            In Product master tab under indigenisation heading add option is given to add products in the portal. 
                                            The format for input data require various drop down menu to be selected for correct choice to be populated under the relevant space for e.g. in  END USER either ARMY . NAVY , AIRFORCE , Coast Guard to be selected for filling the details. The category master allows the details to be customized as per the requirement of DPSUs/OFB. Barring NATO group , NATO class and INC Code other drop downs information are recommended to be customised through Category Master .
                                        </p>
                                        <div class="faq-img">
                                            <img src="../assets/images/faq-images/category-master.jpg" alt="" />

                                            <p style="margin-top:20px; font-weight:bold;">Under Category Master following structure for capturing information are given as per their  :-</p>

                                        </div>
                                        <div class="faq-table">
                                            <table class="table manage-user">
                                                <tr>
                                                    <th>S.No</th>
                                                    <th>Drop Down Label </th>
                                                    <th>Level 1</th>
                                                    <th>Level 2</th>
                                                    <th>Level 3</th>
                                                </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>Certification</td>
                                                <td>Yes </td>
                                                <td>No</td>
                                                <td>No</td>
                                                
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Defence Platform</td>
                                                <td>Yes </td>
                                                <td>Yes(Name of Defence Platform)</td>
                                                <td>No</td>
                                                
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>Financial Support </td>
                                                <td>Yes </td>
                                                <td>No</td>
                                                <td>No</td>
                                                
                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td>HS Code</td>
                                                <td>Yes </td>
                                                <td>No</td>
                                                <td>No</td>
                                                
                                            </tr>
                                             <tr>
                                                <td>5</td>
                                                <td>NSN group</td>
                                                <td>Yes</td>
                                                <td>Yes (NSN Class) </td>
                                                <td>Yes  (INC Code) </td>
                                                
                                                
                                            </tr>
                                            
                                            <tr>
                                                <td>6</td>
                                                <td>Procurement Category</td>
                                                <td>Yes</td>
                                                <td>No</td>
                                                <td>No</td>
                                                
                                                
                                            </tr>
                                            <tr>
                                                <td>7</td>
                                                <td>Product (Industry domain )</td>
                                                <td>Yes</td>
                                                <td>Yes</td>
                                                <td>Yes</td>
                                                
                                                
                                            </tr>
                                            <tr>
                                                <td>8</td>
                                                <td>QA Agency</td>
                                                <td>Yes</td>
                                                <td>No</td>
                                                <td>No</td>
                                                
                                                
                                            </tr>
                                            <tr>
                                                <td>9</td>
                                                <td>Technical Support</td>
                                                <td>Yes</td>
                                                <td>No</td>
                                                <td>No</td>
                                                
                                                
                                            </tr>
                                            <tr>
                                                <td>10</td>
                                                <td>Testing</td>
                                                <td>Yes</td>
                                                <td>No</td>
                                                <td>No</td>
                                                
                                            </tr>
                                        </table>
                                            <p style="font-weight:bold;">The above table can be understand be example stated below :- </p>
                                            <ol style="list-style-type:lower-alpha">
                                                <li>Certification have only one level i.e. level 1 so any information needed on product add page( Product Master tab)  under Certification heading can be added here to be reflected in drop down menu.</li>
                                                <li>Defence Platform have two level i.e. level 1(Defence platform)  and level 2( Name of Defence Platform) so any information needed on product add page( Product Master tab)  under Defence Platform  and Name of Defence platform heading can be added here to be reflected in drop down menu. E.g. for adding new defence  platform say ’ X ‘ the same will be added under level 1 of category master tab and if the name  of defence platform ’ X ‘  say ‘Ý’ is to be added then the level 2 of category master tab is to be selected where at level 1 ’ X ‘  is to be selected then say ‘Ý’.</li>
                                            </ol>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq12" aria-expanded="false" aria-controls="faq12">2.1 ADD Level 1
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq12" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Dropdown master Level 1:-
                                        </p>
                                        <ol>
                                            <li>Click on Category Master Tab at the left hand corner of the portal after login.</li>
                                            <li>Click on Dropdown Master Tab</li>
                                            <li>Click on Level 1 and type level 1 value and click save button for adding level 1</li>
                                            <li>See the below image to add Dropdown level 1
                                                <div class="faq-img">
                                                     <img src="../assets/images/faq-images/add-lavel-1.jpg" alt="" />
                                                </div>

                                            </li>
                                            <li>
                                                Level 1 will be displayed after save button clicked.
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/view-level-1.jpg" alt="" />
                                                 </div>
                                            </li>
                                            
                                            
                                        </ol>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq13" aria-expanded="false" aria-controls="faq13">2.2.ADD Level 2
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq13" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Dropdown master Level 2:-
                                        </p>
                                        <ol>
                                            <li>Click on Category Master Tab at the left hand corner of the portal after login.</li>
                                            <li>Click on Dropdown Master Tab, Select drop down label e.g. defence platform and choose level 1 from available options </li>
                                            <li>Click on Level 2 and type value to be entered and click save to save level 2</li>
                                            <li>See the below image to add Dropdown level 2
                                                <div class="faq-img">
                                                     <img src="../assets/images/faq-images/add-level-2.jpg" alt="" />
                                                </div>

                                            </li>

                                           <li>
                                                Level 2 will be displayed after save button clicked.
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/view-level-2.jpg" alt="" />
                                                 </div>
                                            </li>
                                            
                                        </ol>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq14" aria-expanded="false" aria-controls="faq14">2.3.ADD Level 3
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq14" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Dropdown master Level 3:-
                                        </p>
                                        <ol>
                                            <li>Click on Category Master Tab at the left hand corner of the portal after login.</li>
                                            <li>Click on Dropdown Master Tab, and choose label e.g. NSN Group then Select level 1 and Level 2</li>
                                            <li>Click on Level 3 and type value that need to be saved at level 3, click save to save it.</li>
                                            <li>See the below image to add Dropdown level 3
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/add-level-3.jpg" alt="" />
                                                </div>
                                            </li>

                                            <li>
                                                Level 3 will be displayed after save button clicked.
                                                <div class="faq-img">
                                                    <img src="../assets/images/faq-images/view-level-3.jpg" alt="" />
                                                 </div>
                                            </li>
                                            
                                        </ol>
                                        
                                    </div>
                                </div>
                            </div>
                             <div class="card">
                                <div class="card-header">
                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq15" aria-expanded="false" aria-controls="faq15">2.4. ADD Dropdown
                                            <i class="fa fa-plus pull-right"></i>
                                    </h2>
                                </div>

                                <div id="faq15" class="collapse">
                                    <div class="card-body">
                                        <p>
                                           Drop down mention in the above matrix will contain each and every value that is being added into the portal by using Add level 1 , 
                                            level 2 or level 3 as per the case. However the same may also be customised as per the requirement of DPSUs/OFB by selecting drop down from 
                                            Category  master  then selecting required dropdown label. All the values entered for that label will appear as radio button. 
                                            Select those option which required  to be  reflected in the product  master add page.The above process is described in the image below:-
                                        </p>
                                        <ol>
                                            <li>Click on Category Master Tab at the left hand corner of the portal after login.</li>
                                            <li>Click on Dropdown Master Tab and Select dropdown lebel for e.g. certification</li>
                                            <li>On selecting required label all the values related with the label will appear in right hand side.</li>
                                            <li>Select the radio button for selecting those values that are required to be visible in the drop down menu at Product Master</li>
                                            
                                        </ol>
                                        <div class="faq-img">
                                            <img src="../assets/images/faq-images/category-dropdown.jpg" alt="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            

                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>


