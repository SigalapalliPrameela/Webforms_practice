
<%@ Page Title="Student Registration"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="WebForm._Default" %>

<asp:Content ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style>
        .registration-card {
            background: white;
            padding: 30px;
            border-radius: 14px;
            box-shadow: 0 4px 18px #17255412;
            margin-bottom: 30px;
        }

        .registration-card h1,
        .registration-card h2 {
            color: #17365d;
        }

        .field {
            margin-bottom: 18px;
        }

        .field label {
            display: block;
            margin-bottom: 7px;
            font-weight: 600;
        }

        .field input[type="text"],
        .field input[type="email"],
        .field select,
        .field textarea {
            padding: 10px;
            border: 1px solid #cbd5e1;
            border-radius: 6px;
            max-width: 100%;
        }

        .validation-message {
            display: block;
            color: #dc2626;
            margin: 5px 0;
        }

        .register-button {
            background: #2563eb;
            color: white;
            padding: 11px 24px;
            border: none;
            border-radius: 7px;
            cursor: pointer;
        }

        .register-button:hover {
            background: #1d4ed8;
        }

        .student-grid {
            width: 100%;
            border-collapse: collapse;
            margin-top: 18px;
        }

        .student-grid th {
            background: #17365d;
            color: white;
            padding: 12px;
            text-align: left;
        }

        .student-grid td {
            padding: 12px;
            border-bottom: 1px solid #e2e8f0;
        }

        .student-grid tr:nth-child(even) {
            background: #f8fafc;
        }

        .form-message {
            display: block;
            margin-top: 15px;
            font-weight: bold;
        }
    </style>

    <section class="registration-card">

        <h1>Student Registration</h1>

        <p>
            Please fill in your details to register as a student.
        </p>

        <div class="field">
            <label for="txtName">Name:</label>

            <asp:TextBox ID="txtName"
                runat="server" />

            <asp:RequiredFieldValidator
                ID="rfvname"
                runat="server"
                ControlToValidate="txtName"
                ErrorMessage="Name is required."
                Text="Name is required."
                CssClass="validation-message"
                Display="Dynamic" />
        </div>

        <div class="field">
            <label for="txtemail">Email:</label>

            <asp:TextBox ID="txtemail"
                runat="server"
                TextMode="Email" />

            <asp:RegularExpressionValidator
                ID="revemail"
                runat="server"
                ControlToValidate="txtemail"
                ValidationExpression="^[\w\.\-]+@[\w\-]+(\.[\w\-]+)+$"
                ErrorMessage="Enter a valid email address."
                Text="Enter a valid email address."
                CssClass="validation-message"
                Display="Dynamic" />
        </div>

        <div class="field">
            <label for="txtage">Age:</label>

            <asp:TextBox ID="txtage"
                runat="server" />

            <asp:RequiredFieldValidator
                ID="rfvage"
                runat="server"
                ControlToValidate="txtage"
                ErrorMessage="Age is required."
                Text="Age is required."
                CssClass="validation-message"
                Display="Dynamic" />

            <asp:CompareValidator
                ID="cvage"
                runat="server"
                ControlToValidate="txtage"
                Operator="DataTypeCheck"
                Type="Integer"
                ErrorMessage="Age must be a number."
                Text="Age must be a number."
                CssClass="validation-message"
                Display="Dynamic" />

            <asp:RangeValidator
                ID="rvage"
                runat="server"
                ControlToValidate="txtage"
                Type="Integer"
                MinimumValue="18"
                MaximumValue="60"
                ErrorMessage="Age must be between 18 and 60."
                Text="Age must be between 18 and 60."
                CssClass="validation-message"
                Display="Dynamic" />

            <asp:CustomValidator
                ID="cvage1"
                runat="server"
                ControlToValidate="txtage"
                OnServerValidate="cvCustom_ServerValidate"
                ErrorMessage="Age must be at least 18."
                Text="Age must be at least 18."
                CssClass="validation-message"
                Display="Dynamic" />
        </div>

        <div class="field">
            <label for="ddlcourse">Course:</label>

            <asp:DropDownList ID="ddlcourse"
                runat="server"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlcourse_SelectedIndexChanged">

                <asp:ListItem Text="Select Course" Value="" />
                <asp:ListItem Text="CSharp" Value="C#" />
                <asp:ListItem Text="Data Analytics" Value="DA" />
                <asp:ListItem Text="Java Programming" Value="Java" />

            </asp:DropDownList>

            <asp:RequiredFieldValidator
                ID="rfvddl"
                runat="server"
                ControlToValidate="ddlcourse"
                InitialValue=""
                ErrorMessage="Please select a course."
                Text="Please select a course."
                CssClass="validation-message"
                Display="Dynamic" />
        </div>

        <div class="field">
            <label>Gender:</label>

            <asp:RadioButton ID="rbMale"
                Text="Male"
                runat="server"
                GroupName="Gender"  TextAlign="Right" />

            <asp:RadioButton ID="rbFemale"
                Text="Female"
                runat="server"
                GroupName="Gender"
                 TextAlign="Right"/>
        </div>

        <div class="field">
            <label>Choose your interests:</label>

            <asp:CheckBoxList ID="cbInterests"
                runat="server">

                <asp:ListItem Value="1"  TextAlign="Right">Coding</asp:ListItem>
                <asp:ListItem Value="2"  TextAlign="Right">Journaling</asp:ListItem>
                <asp:ListItem Value="3"  TextAlign="Right">Book Reading</asp:ListItem>

            </asp:CheckBoxList>
        </div>

        <div class="field">
            <asp:CheckBox ID="cbterms"
                Text="I agree to all Terms and Conditions"  TextAlign="Right"
                runat="server" />
        </div>

        <asp:Button ID="btnRegister"
            Text="Register"
            runat="server"
            CssClass="register-button"
            OnClick="btnRegister_Click" />

        <asp:Label ID="lblMessage"
            runat="server"
            CssClass="form-message" />

    </section>

    <section class="registration-card">

        <h2>Registered Students</h2>

        <p>View and manage registered student records below.</p>

        <asp:GridView
            ID="gvStudents"
            runat="server"
            CssClass="student-grid"
            AutoGenerateColumns="False"
            DataKeyNames="Id"
            OnRowEditing="gvStudents_RowEditing"
            OnRowCancelingEdit="gvStudents_RowCancelingEdit"
            OnRowUpdating="gvStudents_RowUpdating"
            OnRowDeleting="gvStudents_RowDeleting">

            <Columns>

                <asp:BoundField
                    DataField="Id"
                    HeaderText="ID"
                    ReadOnly="True" />

                <asp:BoundField
                    DataField="Name"
                    HeaderText="Name" />

                <asp:BoundField
                    DataField="Age"
                    HeaderText="Age" />

                <asp:BoundField
                    DataField="Course"
                    HeaderText="Course" />

                <asp:CommandField
                    ShowEditButton="True"
                    ShowDeleteButton="True" />

            </Columns>

        </asp:GridView>

    </section>

</asp:Content>
