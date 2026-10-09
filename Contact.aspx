
<%@ Page Title="Contact Us"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Contact.aspx.cs"
    Inherits="WebForm.Contact" %>

<asp:Content ID="ContactContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <section class="hero">
        <h1>Contact Us</h1>

        <p>
            Have questions about the Student Portal?
            We are happy to help.
        </p>
    </section>

    <div class="card">
        <h2>Contact Information</h2>

        <p><strong>Email:</strong> support@example.com</p>

        <p><strong>Working Hours:</strong>
            Monday to Friday
        </p>

        <p>
            For application-related queries,
            please contact the support team.
        </p>

        <asp:HyperLink ID="lnkHome"
            runat="server"
            NavigateUrl="~/Default.aspx"
            CssClass="button"
            Text="Back to Home">
        </asp:HyperLink>
    </div>

</asp:Content>
