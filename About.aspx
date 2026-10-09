
<%@ Page Title="About Us"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="About.aspx.cs"
    Inherits="WebForm.About" %>

<asp:Content ID="AboutContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <section class="hero">
        <h1>About Our Student Portal</h1>

        <p>
            Welcome to our Student Portal!
        </p>
    </section>

    <div class="card">
        <h2>Our Purpose</h2>

        <p>
            Our goal is to provide a simple platform for
            student registration and student record management.
        </p>
    </div>

    <div class="card">
        <h2>Our Features</h2>

        <ul>
            <li>Student registration</li>
            <li>Input validation</li>
            <li>Student record management</li>
            <li>Role-based access for Admin and Student users</li>
        </ul>
    </div>

    <div class="card">
        <h2>Explore More</h2>

        <p>
            Ready to explore the application?
        </p>

        <asp:HyperLink ID="lnkHome"
            runat="server"
            NavigateUrl="~/Default.aspx"
            CssClass="button"
            Text="Go to Home">
        </asp:HyperLink>
    </div>

</asp:Content>
