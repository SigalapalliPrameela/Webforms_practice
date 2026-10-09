
<%@ Page Title="Students"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Students.aspx.cs"
    Inherits="WebForm.Students" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style>
        .students-card {
            background: #fff;
            padding: 28px;
            border-radius: 12px;
            box-shadow: 0 4px 18px rgba(0,0,0,.08);
            margin-top: 20px;
        }

        .students-title {
            color: #243b53;
            margin-bottom: 8px;
        }

        .students-description {
            color: #627d98;
            margin-bottom: 24px;
        }

        .students-grid {
            width: 100%;
            border-collapse: collapse;
        }

        .students-grid th {
            background: #2457a7;
            color: white;
            padding: 12px;
            text-align: left;
        }

        .students-grid td {
            padding: 12px;
            border-bottom: 1px solid #e5e7eb;
        }

        .students-grid tr:nth-child(even) {
            background: #f5f8fc;
        }
    </style>

    <div class="students-card">
        <h2 class="students-title">Registered Students</h2>

        <p class="students-description">
            View all students registered in the system.
        </p>

        <asp:GridView ID="gvStudentsList"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="students-grid"
            GridLines="None"
            EmptyDataText="No registered students found.">

            <Columns>
                <asp:BoundField
                    DataField="Id"
                    HeaderText="Student ID" />

                <asp:BoundField
                    DataField="Name"
                    HeaderText="Student Name" />

                <asp:BoundField
                    DataField="Age"
                    HeaderText="Age" />

                <asp:BoundField
                    DataField="Course"
                    HeaderText="Course" />
            </Columns>

        </asp:GridView>
    </div>

</asp:Content>
