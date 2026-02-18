<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="employeeInfo.aspx.cs" Inherits="CompanyProject8209web.employee.employeeInfo"
   EnableEventValidation="false" ValidateRequest="false"
    %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <style>
    body {
        background-color: #fedbf9;
    }

    .page-wrap {
        max-width: 900px;
        margin: 25px auto;
        padding: 15px;
    }

    .card {
        background: rgba(255, 255, 255, 0.65);
        border-radius: 16px;
        padding: 20px;
        box-shadow: 0 8px 18px rgba(0,0,0,0.08);
        border: 1px solid rgba(0,0,0,0.06);
    }

    .title {
        font-size: 22px;
        font-weight: bold;
        margin-bottom: 5px;
    }

    .subtitle {
        margin-bottom: 15px;
    }

    .form-table {
        width: 100%;
        border-spacing: 0 10px;
    }

    .form-table td:first-child {
        width: 220px;
        text-align: right;
        font-weight: bold;
        padding-right: 10px;
    }

    .input-like {
    width: 260px;
    padding: 8px 10px;
    border-radius: 10px;
    border: 1px solid rgba(0,0,0,0.12);
    background-color: #fff7fb;   /* أوف وايت وردي ناعم */
    transition: 0.2s;
}
    .input-like:focus {
        border-color: #b86aa0;
        box-shadow: 0 0 6px rgba(184,106,160,0.35);
        outline: none;
        background-color: white;
    }

   
    .btns {
        margin-top: 10px;
        display: flex;
        gap: 10px;
        flex-wrap: wrap;
    }

   .btn-like {
    background: linear-gradient(135deg, #c38eb4, #b86aa0) !important;
    border: none;
    border-radius: 12px;
    padding: 10px 18px;
    color: white !important;
    font-weight: 600;
    cursor: pointer;
    box-shadow: 0 6px 14px rgba(184,106,160,0.35);
    transition: all 0.25s ease;
}

.btn-like:hover {
    background: linear-gradient(135deg, #b86aa0, #a94d8f) !important;
    transform: translateY(-1px);
    box-shadow: 0 8px 18px rgba(184,106,160,0.45);
}

.btn-like:active {
    transform: scale(0.98);
}

    .msg {
        display: inline-block;
        padding: 8px 12px;
        border-radius: 10px;
        background: rgba(0,0,0,0.12);
        color: #b91c1c;
        font-weight: bold;
        margin-bottom: 10px;
    }

    .grid {
        margin-top: 15px;
        background: rgba(255,255,255,0.65);
        border-radius: 12px;
        border: 1px solid rgba(0,0,0,0.06);
    }

    .grid th {
        background: rgba(0,0,0,0.05);
        padding: 10px;
    }

    .grid td {
        padding: 10px;
        border-top: 1px solid rgba(0,0,0,0.05);
    }
</style>

<div class="page-wrap">

    <div class="card">
        <div class="title">WELCOME TO ZID COMPANY PAGE</div>
        <div class="subtitle">Enter Your Information:</div>

        <asp:Label ID="lblOutput" runat="server" CssClass="msg"></asp:Label>

        <table class="form-table">

            <tr>
                <td>EmployeeId</td>
                <td><asp:TextBox ID="txtEmployeeId" runat="server" CssClass="input-like"></asp:TextBox></td>
            </tr>

            <tr>
                <td>FirstName</td>
                <td><asp:TextBox ID="txtfName" runat="server" CssClass="input-like"></asp:TextBox></td>
            </tr>

            <tr>
                <td>LastName</td>
                <td><asp:TextBox ID="txtlName" runat="server" CssClass="input-like"></asp:TextBox></td>
            </tr>

            <tr>
                <td>Salary</td>
                <td><asp:TextBox ID="txtSalary" runat="server" CssClass="input-like"></asp:TextBox></td>
            </tr>

            <tr>
                <td>PhoneNumber</td>
                <td><asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="input-like"></asp:TextBox></td>
            </tr>

            <tr>
                <td>Country</td>
                <td><asp:DropDownList ID="DdlCountry" runat="server" CssClass="input-like"></asp:DropDownList></td>
            </tr>

            <tr>
                <td>Gender</td>
                <td><asp:DropDownList ID="DdlGender" runat="server" CssClass="input-like"></asp:DropDownList></td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <div class="btns">

                        <asp:Button ID="btnInsert" runat="server"
                            CssClass="btn-like"
                            Text="Insert"
                            OnClick="btnInsert_Click" />

                        <asp:Button ID="btnUpdate" runat="server"
                            CssClass="btn-like"
                            Text="Update"
                            OnClick="btnUpdate_Click" />

                        <asp:Button ID="btnDelete" runat="server"
                            CssClass="btn-like"
                            Text="Delete"
                            OnClick="btnDelete_Click"
                            OnClientClick="return confirm('Are you Sure you want to Delete ?')" />

                        <asp:Button ID="btnSelect" runat="server"
                            CssClass="btn-like"
                            Text="Select"
                            OnClick="btnSelect_Click" />

                        <asp:Button ID="btnExportToE" runat="server"
                            CssClass="btn-like"
                            Text="Export To Excel"
                            OnClick="btnExportToE_Click" />

                    </div>
                </td>
            </tr>

        </table>
    </div>

    <asp:GridView ID="gvEmployee" runat="server"
        CssClass="grid"
        AutoGenerateColumns="False"
        DataKeyNames="employeeId">

        <Columns>

            <asp:TemplateField HeaderText="EmployeeId">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkupdate" runat="server"
                        CommandArgument='<%# Bind("employeeId") %>'
                        OnClick="populateForm_Click"
                        Text='<%# Eval("employeeId") %>'>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="fName" HeaderText="First Name" />
            <asp:BoundField DataField="lName" HeaderText="Last Name" />
            <asp:BoundField DataField="salary" HeaderText="Salary" />
            <asp:BoundField DataField="phoneNumber" HeaderText="Phone" />
            <asp:BoundField DataField="country" HeaderText="Country" />
            <asp:BoundField DataField="gender" HeaderText="Gender" />

        </Columns>

    </asp:GridView>

</div>

</asp:Content>