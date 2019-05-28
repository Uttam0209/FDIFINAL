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
                        <div class="accordion">
                            <div class="card">
                                <div class="card-header">
                                        <h2 data-toggle="collapse" data-target="#faq1" aria-expanded="true" aria-controls="faq1">
                                           Preface 
                                        </h2>
                                  </div>

                                <div id="faq1" class="collapse" aria-labelledby="headingOne">
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
                                        <h2  data-toggle="collapse" data-target="#faq2" aria-expanded="true" aria-controls="faq2">
                                            Table of Contents
                                        </h2>
                                  </div>

                                <div id="faq2" class="collapse">
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
                                            <li>Category Master </li>

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


