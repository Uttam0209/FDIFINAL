<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="User_FAQ" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/css/all.min.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" container">
        <div class="row">
            <div class="col text-center">
                <h2>FAQs</h2>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="accordian  my-md-2">
                    <div class="accordian-tab">
                        <input type="checkbox" id="toggel1" class="accordian-toggel" name="toggel">
                        <label for="toggel1">iDEX</label>
                        <div class="accordian-content">
                            <div style="margin-left: 30px;">
                                <p><b>Q1. What is iDEX?</b></p>
                                <p>
                                    <b>A1:-</b> Innovations for Defence Excellence (iDEX) is a framework promulgated by the Department of Defence Production, Ministry of Defence and launched by the Hon’ble PM in April 2018. iDEX aims to achieve self - reliance and foster innovation and technology development in Defence and Aerospace by engaging Industries including MSMEs, start-ups, individual innovators, R&D institutes and academia.
                                                    Information pertaining to the guidelines, frameworks, procedures and challenges can be accessed at <a href="https://idex.gov.in" target="_blank">idex.gov.in.</a>
                                </p>
                                <p><b>Q2.	What are the core objectives of iDEX?</b></p>
                                <p><b>A2:-</b> Objectives:</p>
                                <p style="margin-left: 30px;">
                                    <b>i.</b> To create 'corporate Venture Capital’ models for Indian Defence needs, which would identify emerging technologies, connect innovators to military units, facilitate co-creation of new and appropriate technologies, and create pathways for incorporation of cutting edge technologies into weapon systems used by Armed Services.
                                </p>
                                <p style="margin-left: 30px;"><b>ii.</b>	To transform defence innovation in India with the ultimate goal of delivering military-grade products that solve the critical needs of the Indian defence set-up by developing or applying advanced technologies.</p>
                                <p style="margin-left: 30px;"><b>iii.</b>	To create a culture of innovation in Indian defence & Aerospace by engaging startups and innovators through encouraging co-creation & co innovation.</p>

                                <p><b>Q3.	What is Defence Innovation Organization (DIO)?</b> </p>
                                <p><b>A3:-</b> Defence Innovation Organisation (DIO), is a not for profit Organisation, incorporated under Section 8 of the Companies Act by two founder members i.e., HAL & BEL. iDEX framework is being implemented by DIO.</p>

                                <p><b>Q4.	How does iDEX work?</b></p>
                                <p><b>A4:-</b> iDEX-DIO launches Defence India Startup Challenge (DISC) with problem statements from Armed Forces, DPSUs & OFB. After rigorous evaluation of the applications, winners are identified. Winner startups/ individuals/ MSMEs receive innovation grants in technological areas through the Prototype funding guidelines called “Support for Prototype and Research Kickstart” (SPARK), which entail provisioning of grants upto Rs 1.5 crore to the Startups on the basis of milestones through multiple tranches, for prototype development. </p>
                                <p><b>Q5.	What are the steps taken for maturity of challenge, in terms of the ultimate product,  by DIO?</b></p>
                                <p><b>A5:-</b> In order to curate the open innovation the following steps are taken up by the iDEX Program Management Unit. </p>
                                <p style="margin-left: 30px;">i. Challenge curation - Organizing various challenges / hackathons to shortlist potential technologies for defense and aerospace application. </p>
                                <p style="margin-left: 30px;">ii. Outreach -Outreach to the young innovators, students for sourcing of innovations all over the country. </p>
                                <p style="margin-left: 30px;">iii. HPSC Screening & Selection -Evaluate technologies and products in terms of their utility and impact on the Indian defense and aerospace setup. </p>
                                <p style="margin-left: 30px;">iv. Spark grants appraisal & contracts -Enable and fund pilots, using innovation funds dedicated to the purpose. </p>
                                <p style="margin-left: 30px;">v. Product co-development -Interface with the military (Army/Navy/Airforce) top brass about key innovative technologies & encourage their adoption with suitable assistance. </p>
                                <p style="margin-left: 30px;">vi. Product acceptance & certification: Facilitate scale-up indigenization and integration in manufacturing facilities for successful piloted technologies. </p>


                                <p><b>Q6.	What is the maximum limit of SPARK grant offered under iDEX?</b></p>
                                <p><b>A6:-</b> Upto max Rs 1.5 crore.</p>

                                <p><b>Q7.	What is the procedure for applying for grant under iDEX initiative?</b></p>
                                <p><b>A7:-</b> Applicants are required to apply for challenges launched under DISC at <a href="https://idex.gov.in">idex.gov.in</a> and fill in the application over the mentioned link. The major information sought in the application is:</p>
                                <p style="margin-left: 30px;">•	Full Details of a Single Point of Contact for the applying entity. We encourage that this person be a core member of the applicant entity (Startup/MSME).</p>
                                <p style="margin-left: 30px;">•	Entity Details – the details of the Startup/MSME applying. Individuals Innovators do not have to fill these.</p>
                                <p style="margin-left: 30px;">
                                    •	Proposal Details – the details of the proposal in response to the iDEX Challenge problem statements/focus areas. Applicants must select the problem statement they are addressing, what level of funding they want, give technical and financial details of their proposal. This includes the information of relevant patents and research papers by the applicant, if any, as well as a tentative business plan.
                                                <p><b>Q8. Who can apply?</b></p>

                                <p><b>A8:-</b></p>
                                <p style="margin-left: 30px;">i.	Start-UPS, as defined and recognized by DPIIT, Ministry of Commerce and Industry, Government of India.</p>
                                <p style="margin-left: 30px;">ii.	Any Indian company incorporated under the Companies Act 1956/2013, primarily a Micro, Small and Medium Enterprises (MSME) as defined in the MSME Act, 2006.</p>
                                <p style="margin-left: 30px;">iii.	Individual innovators are also encouraged to apply (research & academic institutions can use this category to apply).</p>
                                <p><b>9.	How is the evaluation of applications under DISC carried out?</b></p>
                                <p><b>A9:-</b> The applications are screened by a duly constituted High-Powered Selection Committee comprising defence and technology experts for the purpose of selection of iDEX Challenge winners.</p>
                                <p><b>Q10.	How grant mechanism works?</b></p>
                                <p>
                                    <b>A10:-</b> The DISC winners will be funded up to the maximum ceiling of Rs 1.5 crores (depending upon the costing of the project and matching contribution) in the
                                                    form of equity/other relevant structures. The funds will be disbursed in tranches based on the milestones decided by a High-Powered Selection Committee.
                                                    Apart from the fund, selected applicants may also be given entry to accelerator programs run by iDEX partners, where they will be
                                                    supported in technology and business development through mentorship under the innovation and entrepreneurship experts. The selected applicants
                                                    may also be supported in terms of access to defence testing facilities and experts for their product/ technology development.
                                </p>
                                <p><b>Q11.	What is the focus of iDEX?</b></p>
                                <p><b>A11:-</b> The program focuses on facilitation for creating prototypes and connecting the  products/technologies to the market (Defence or otherwise). Applicants will be encouraged to spend on:</p>
                                <p style="margin-left: 30px;">•	Research & Development</p>
                                <p style="margin-left: 30px;">•	Prototyping</p>
                                <p style="margin-left: 30px;">•	Pilot Implementation</p>
                                <p style="margin-left: 30px;">•	Market Assessment</p>
                                <p>
                                    <b>Q12.	 Is there any limitation on the number of winners selected for any challenge?</b>
                                </p>
                                <p><b>A12:-</b> No, there is no such limit. Any number of potential candidates can be selected under the iDEX challenges.</p>
                            </div>
                        </div>
                    </div>
                    <div class="accordian-tab">
                        <input type="checkbox" id="toggel2" class="accordian-toggel" name="toggel">
                        <label for="toggel2">Inter-Governmental Agreement (IGA)</label>
                        <div class="accordian-content">
                            <div style="margin-left: 30px;">

                                <p><b>Q1.	 What are IGAs? &nbsp;How industry can participate?</b></p>
                                <p>
                                    <b>A1:-</b> &nbsp;Intergovernmental Agreements (IGAs) are inter government agreements for procurements or transfer of technology from foreign countries.
                                                    Details are covered at Para 104 to 105 of Chapter II of DPP 2016.In present context, it refers to an agreement between India and Russia
                                                    on Mutual Cooperation in Joint Manufacturing of Spares, Components, Aggregates and other material related to Russian/Soviet Origin Arms
                                                    and Defence Equipment. This was signed during the India-Russia Summit in 2019.
                                </p>
                                <p>
                                    The objective of the IGA is to enhance the after sales
                                                    support and operational availability of Russian origin equipment currently in service in Indian Armed Forces by organizing production of
                                                    spares and components in the territory of India by Indian Industry by way of creation of Joint Ventures/Partnership with Russian Original
                                                    Equipment Manufacturers (OEMs) under the framework of the Make in India initiative.
                                </p>
                                <p>
                                    About 1000 types of spares are expected to be manufactured
                                                    in India under this agreement for which 30 Memorandum of Understanding (MoU) have already been signed so far between Indian companies
                                                    and Russian Original Equipment Manufacturers (OEMs) for manufacturing of these spares in India.
                                </p>
                                <p>
                                    The Indian Navy, Indian Air Force and
                                                    the Ordnance Factory Board have issued RFPs for about 400 spares under the IGA.Industry can visit the respective websites of HAL/OFB/SHQs
                                                    and participate in EoIs/RFPs.
                                </p>

                            </div>
                        </div>
                    </div>
                    <div class="accordian-tab">
                        <input type="checkbox" id="toggel3" class="accordian-toggel" name="toggel">
                        <label for="toggel3">In-House | Make-II | Other than Make-II</label>
                        <div class="accordian-content">
                            <div class="card-body card-custom" style="margin-left: 30px;">

                                <p><b>Q1.	What are the processes: &nbsp;In-House, Make-II and Other Than Make-II?</b></p>
                                <p><b>A1:-</b> 	OFB/DPSUs/SHQs adopt various processes of indigenization for development of defence equipment or their sub-systems/sub-assembly/assemblies/components/ materials, primarily for import substitution. These include In-house and Industry Process (Make-II and Other than Make-II).</p>
                                <p style="margin-left: 30px;"><b>i. In-house Process</b></p>
                                <p style="margin-left: 60px;">OFB/DPSUs/SHQs may use their In-house capability to indigenize their items.</p>
                                <p style="margin-left: 30px;"><b>ii. Industry Process</b></p>
                                <p style="margin-left: 60px;"><b>a. Make-II</b></p>
                                <p style="margin-left: 90px;">
                                    OFB/DPSUs Make-II procedure is based on the similar Make-II procedure promulgated by the Ministry of Defence under DPP-2016. Under this procedure,
                                                    no Government funding is envisaged for prototype development purposes but there is an assurance of orders on successful development and trials of the prototype.
                                                    Make-II development process starts with approval of a project by OFB/DPSUs, subsequent to which EoI is issued online. Based on the EoI responses,
                                                    Development Order is issued to selected Development Agency(ies). On successful development of prototype, procurement order is given to the lowest
                                                    bidding Development Agency (DA).The list of items taken up for indigenous development under Make-II is uploaded on respective websites of
                                                    OFB/DPSUs and is updated periodically. The same is also available at srijandefence.gov.in.
                                </p>
                                <p style="margin-left: 90px;">
                                    The detailed framework for implementation of Make-II Procedure at OFB & DPSUs can be downloaded here.
                                                    <a class="" target="_blank" href="Final_Make-II_Framework_for_OFB_and_DPSUs.pdf">Download Document</a>
                                </p>

                                <p style="margin-left: 60px;"><b>b. 	Other than Make-II</b></p>
                                <p style="margin-left: 90px;">DPSUs/OFB/SHQs may adopt their other extant processes of indigenization. The other extant processes of indigenization have been clubbed as Other than Make-II.</p>
                                <p><b>Q2. 	How can the industry participate in Make II developments?</b></p>
                                <p><b>A2:-</b> Industry can visit the respective website of OFB/DPSUs/SHQs and participate in their EOIs/RFPs.</p>
                                <p><b>Q3. 	Is there assurance of orders under Make II?</b></p>
                                <p><b>A3:-</b> 	Yes, the vendors are given assured orders subject to successful development and trial of the item.</p>
                                <p><b>Q4. 	Can the Start-ups participate in Make II developments?</b></p>
                                <p><b>A4:-</b> 	The Startups recognised by DPIT can participate in Make II developments.</p>
                                <p><b>Q5. 	What is the difference between Make I & Make II procedure?</b></p>
                                <p><b>A5:-</b> 	Development under Make I procedure are funded by Government of India, whereas developments under Make II are funded by Indian industry.</p>

                            </div>
                        </div>
                    </div>
                    <div class="accordian-tab">
                        <input type="checkbox" id="toggel4" class="accordian-toggel" name="toggel">
                        <label for="toggel4">FAQs related to srijandefence</label>
                        <div class="accordian-content">
                            <div class="card-body card-custom" style="margin-left: 30px;">
                                <p>
                                    <b>Q1. 	What is srijandefence portal?</b>
                                </p>
                                <p>
                                    <b>A1:-</b> Srijandefence.gov.in portal was launched by Hon’ble Raksha Mantri on 14 Aug 2020.It provides opportunity for Make in India in defence    for Indian Manufacturers.The main objective of the portal is to partner the private sector in indigenization efforts of
                                                    Defence Public Sector Undertakings(DPSUs), OFB and the Armed Forces. The portal will be a non-transactional online marketplace platform.
                                </p>
                                <p>
                                    DPSUs/OFB/SHQs will display their items on this portal, which they have imported or are going to import,
                                                    each item having sizable import value. They will also display those items which have been
                                                    planned/targeted in the coming years, for indigenization.The Indian industry will be able to show their interest
                                                    in those items for which they can design, develop and manufacture as per their capability or through joint venture with OEMs.
                                </p>
                                <p><b>Q2.	How many organizations are there on the srijandefence portal?</b></p>
                                <p>
                                    <b>A2:-</b>	 There are 13 organizations- 9 DPSUs (Defence Public Sector Undertakings), OFB and 3 Services Headquarters (Army, Navy, Airforce).
                                </p>
                                <p>
                                    <b>Q3. 	How do I use search option?</b>
                                </p>
                                <p>
                                    <b>A3:-</b>At the top banner of the website there is a search option provided in which we can type minimum three characters to find the related
                                                    products as per your search query.
                                </p>
                                <p>
                                    <b>Q4.	What are the available filters on srijandefence to use the filtering the available products?</b>
                                </p>
                                <p>
                                    <b>A4:-</b> On the left hand side of the portal, following filters are provided: -
                                </p>
                                <p style="margin-left: 30px;">
                                    a.	Year of import- 2018-19, 2019-20, 2020-21
                                </p>

                                <p style="margin-left: 30px;">
                                    b.	Annual Import Value(RS)- Below 0.5 Million, 0.5-5 Million, 5-10 Million, 10-50 Million, 50 Million and above.
                                </p>

                                <p style="margin-left: 30px;">
                                    c.	Make in India targets Year-2020-21, 2021-22, 2022-23, 2023-24, 2024-25.
                                </p>
                                <p style="margin-left: 30px;">
                                    d.	Company-HAL, BEL, BEML, BDL, MIDHANI, GRSE, GSL, HSL, MDL, OFB, SHQ (Air Force), SHQ (Army), SHQ (Navy).
                                </p>
                                <p style="margin-left: 30px;">
                                    e.	NATO Supply Group Class- In this portal, all items have been indicatively classified based on NATO supply group class. The user can identify/search their interest in items based on the NATO classification.
                                </p>
                                <p style="margin-left: 30px;">f.	Make in India category- iDEX/INNOVATION/R&D, IGA, IN HOUSE, MAKE-II, OTHER THAN MAKE-II </p>
                                <p style="margin-left: 30px;">[The appropriate filters will display the items, as a search result.]</p>
                                <p><b>Q5.	 How do I use Show Interest?</b></p>
                                <p>
                                    <b>A5:-</b>	As per the search result, the portal will display appropriate products. The products will be added on the Show Interest cart once the Add to show interest buttons are clicked. Thereafter, one can go to Show Interest cart and fill the requisite fields of name, email, company name, registered office address and mobile. Based on OTP authentication, the Show Interest is done.
                                </p>
                                <p><b>Q6	How do I use More details option?</b></p>
                                <p><b>A6:-</b> 	On the right hand side of website there is a More details option provided, by clicking on that we can easily get the report on items year wise 2018-19, 2019-20,2020-21.</p>
                                <p><b>Q7. 	What is make in India defence Portal?</b></p>
                                <p><b>A7:-</b> 	On the top of the website page there is an option provided Make in India Defence Portal.On Clicking the link, the portal will be redirected to https://www.makeinindiadefence.gov.in/ portal.</p>
                                <p><b>Q8.	 What is DPSU login Bar?</b></p>
                                <p><b>A8:-</b> 	The Portal is used by DPSUs/OFB/SHQs for updating the content available on the portal. The above stakeholders may login in to the portal by clicking on the DPSU login.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
</asp:Content>

