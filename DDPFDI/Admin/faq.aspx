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
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="faq-secion">


                        <div class="accordion" id="accordion">
                            <div class="card">
                                <div class="card-header">
                                        <h2 data-toggle="collapse" id="headingOne" data-parent="#accordion" data-target="#faq1" aria-expanded="true"  aria-controls="faq1">
                                           Preface 
                                            <i class ="fa fa-minus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq1" class="collapse in"  aria-labelledby="headingOne">
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
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq2"  aria-expanded="false" aria-controls="faq2">
                                            Table of Contents
                                            <i class ="fa fa-plus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq2" class="collapse" >
                                    <div class="card-body">
                                        <ol>
                                            <li> Company Master 
                                                <ol style="list-style:lower-alpha">
                                                    <li>ADD  DIVISION /PLANT</li>
                                                    <li> ADD  UNIT </li>
                                                    <li>ADD DESIGNATION </li>
                                                    <li>EDIT COMPANY 
                                                        <ol style="list-style:lower-roman">
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
                                            <li> Indigenisation</li>

                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq3"  aria-expanded="false" aria-controls="faq3">
                                           1.Company Master
                                            <i class ="fa fa-plus pull-right"></i>
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
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq4"  aria-expanded="false" aria-controls="faq4">
                                          1.1 Add  Division/Plant 
                                            <i class ="fa fa-plus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq4" class="collapse">
                                    <div class="card-body">
                                        <p>
                                           Steps to Add Division /Plant Name:-  </p>
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
                                                <li>
                                                    The Name will be Displayed in the Matrix after save button is clicked. 
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
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq5"  aria-expanded="false" aria-controls="faq5">
                                           1.2 Add Unit
                                            <i class ="fa fa-plus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq5" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Unit Name:- 
                                        </p>
                                        <ol>
                                            <li>
                                                Click on Company Master Tab at the left hand corner of the portal after login. 
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
                                            <li>
                                                The Name will be Displayed in the Matrix after save button is clicked.
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
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq6"  aria-expanded="false" aria-controls="faq6">
                                           1.3 Add Designation
                                            <i class ="fa fa-plus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq6" class="collapse">
                                    <div class="card-body">
                                        <p>
                                            Steps to Add Designation:- 
                                        </p>
                                        <ol>
                                            <li>
                                                Click on Company Master Tab at the left hand corner of the portal after login. 
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
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq7"  aria-expanded="false" aria-controls="faq7">
                                           1.4 Add Employee
                                            <i class ="fa fa-plus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq7" class="collapse">
                                    <div class="card-body">
                                        <p>
                                           Steps to Add Employee:- 
                                        </p>
                                        <ol>
                                            <li>
                                               Click on Company Master Tab at the left hand corner of the portal after login.  
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
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq8"  aria-expanded="false" aria-controls="faq8">
                                            1.5 Edit Company
                                            <i class ="fa fa-plus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq8" class="collapse">
                                    <div class="card-body">
                                        <p>
                                          This functionality provides for editing Company, Division/Plant and Unit details. </p>
                                        <h3>1.5. A. Steps to Edit Company Details:- </h3>
                                        <ol>
                                            <li>
                                               Click on Company Master Tab at the left hand corner of the portal after login.   
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
                                             <li><h3>1.5. B.Steps to Edit Division Details:- </h3>
                                                 <ol>
                                                     <li>
                                                         Click on Company Master Tab at the left hand corner of the portal after login. 
                                                     </li>
                                                     <li>
                                                         Select Edit tab.  
                                                     </li>
                                                     <li>
                                                         Select Company tab. 
                                                     </li>
                                                     <li>
                                                         Select + given in front of company to get all the division under company. 
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
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq9"  aria-expanded="false" aria-controls="faq9">
                                            1.6 Edit Designation
                                            <i class ="fa fa-plus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq9" class="collapse">
                                    <div class="card-body">
                                        <p>
                                           This functionality provides for editing Designation name. 
                                        </p>
                                        <p>Steps to Edit Designation Details:- </p>
                                        <ol>
                                            <li>
                                               Click on Company Master Tab at the left hand corner of the portal after login.  
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
                                        <h2  data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq10"  aria-expanded="false" aria-controls="faq10">
                                            1.7 Edit Employee
                                            <i class ="fa fa-plus pull-right"></i>
                                        </h2>
                                  </div>

                                <div id="faq10" class="collapse">
                                    <div class="card-body">
                                        <p>
                                           This functionality provides for editing Designation name.  
                                        </p>
                                        <p>Steps to Edit Employee Details :- </p>
                                        <ol>
                                            <li>
                                               Click on Company Master tab at the left hand corner of the portal after login.   
                                            </li>
                                            <li> Select Edit tab</li>
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
                                            <li> By Edit option , Employee detail can be Edited
                                                <div class="faq-img">
                                                         <img src="../assets/images/faq-images/edit-employee-details.jpg" alt="" />
                                                  </div>
                                            </li>
                                            <li>By Send Password Option, Password mail will be sent to employee registered Id.</li>
                                            
                                        </ol>
                                      
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


