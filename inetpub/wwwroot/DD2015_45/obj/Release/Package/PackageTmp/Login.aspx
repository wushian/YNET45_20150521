<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DD2015_45.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <table style="border-collapse: collapse; border-spacing: 0;" border="0" cellpadding="0">
    <tr>
      <td>
        <asp:Panel ID="Panel1" Width="300px" runat="server">
          <asp:Image ID="Image2" runat="server" ImageUrl="~/Picture/memberlogin_01.jpg" />
          <div style="width: 305px; height: 145px; border: 0; margin: 0;"
            id="div1" runat="server">
            <table style="width: 100%; height: 100%; border-collapse: collapse; border-spacing: 0;" border="0" cellpadding="3">
              <tr style="height: 0px">
                <td style="width: 130px"></td>
                <td style="width: 60px"></td>
                <td></td>
              </tr>
              <tr>
                <td class="labelCell">
                  <asp:Label ID="Label3" runat="server" Text="使用者代號：" ForeColor="White"></asp:Label>
                </td>
                <td colspan="2">
                  <asp:TextBox ID="txtId" runat="server" Columns="15" MaxLength="20"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="labelCell">
                  <asp:Label ID="Label4" runat="server" Text="登入密碼：" ForeColor="White"></asp:Label>
                </td>
                <td colspan="2">
                  <asp:TextBox ID="txtPassword" runat="server" Columns="15" MaxLength="20"
                    TextMode="Password"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="labelCell">
                  <asp:Label ID="Label5" runat="server" Text="驗證碼：" ForeColor="White"></asp:Label>
                </td>
                <td>
                  <asp:TextBox ID="txtCC" runat="server" Columns="4" MaxLength="4"></asp:TextBox>
                </td>
                <td>
                  <asp:Image ID="img1" runat="server" />
                </td>
              </tr>
              <tr>
                <td colspan="3" style="text-align: center;">
                  <asp:Button ID="btnLogin"  runat="server" Text="登入系統" OnClick="btnLogin_Click" />
                </td>
              </tr>
            </table>
          </div>
          <asp:Label ID="lbErrorMessage" runat="server" Text="" CssClass="ErrorMessage"
            EnableViewState="false" Visible="false"></asp:Label>
        </asp:Panel>
      </td>

    </tr>
  </table>
</asp:Content>
