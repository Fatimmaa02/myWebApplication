<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="survey.aspx.cs" Inherits="CompanyProject8209web.employee.survey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        
  
        <asp:Label ID="Label1" runat="server" CssClass="msg"></asp:Label>


      <style>
    body { background-color:#fedbf9; }

    .page-wrap{
        max-width: 900px;
        margin: 25px auto;
        padding: 15px;
    }

    .card{
        background: rgba(255, 255, 255, 0.65);
        border-radius: 16px;
        padding: 20px;
        box-shadow: 0 8px 18px rgba(0,0,0,0.08);
        border: 1px solid rgba(0,0,0,0.06);
    }

    .title{
        font-size: 22px;
        font-weight: 700;
        margin-bottom: 6px;
    }

    .subtitle{
        margin-bottom: 16px;
        opacity: 0.85;
    }

    .q{
        font-weight: 700;
        margin-top: 14px;
        margin-bottom: 8px;
    }

    /* تنسيق قوائم الاختيار */
    .options{
        background: rgba(255,255,255,0.6);
        border: 1px solid rgba(0,0,0,0.08);
        border-radius: 12px;
        padding: 12px 14px;
    }

    /* نفس زر صفحة الموظفين (موف) */
    .btns{
        margin-top: 16px;
    }

    .btn-like{
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

    .btn-like:hover{
        background: linear-gradient(135deg, #b86aa0, #a94d8f) !important;
        transform: translateY(-1px);
        box-shadow: 0 8px 18px rgba(184,106,160,0.45);
    }

    .btn-like:active{
        transform: scale(0.98);
    }

   
    .msg{
        display:inline-block;
        padding:10px 14px;
        border-radius:12px;
        background:#f3e8ff;   /* بنفسجي فاتح */
        color:#b91c1c;        /* أحمر أنيق */
        font-weight:600;
        border: 1px solid #e9d5ff;
        margin-bottom: 10px;
    }
</style>

<div class="page-wrap">
    <div class="card">

        <div class="title">Survey</div>
        <div class="subtitle">Please answer the questions below:</div>

        
        <asp:Label ID="Label2" runat="server" CssClass="msg"></asp:Label>

        <div class="q">Q1. Which countries have you visited?</div>
        <div class="options">
            <asp:CheckBoxList ID="cblCountry" runat="server"></asp:CheckBoxList>
        </div>

        <div class="q">Q2. What is your gender?</div>
        <div class="options">
            <asp:RadioButtonList ID="rblGender" runat="server"></asp:RadioButtonList>
        </div>

        <div class="btns">
            <asp:Button ID="btnSubmit" runat="server"
                Text="Submit"
                OnClick="btnSubmit_Click"
                CssClass="btn-like" />
        </div>

    </div>
</div>

</asp:Content>

  
