<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="dx_ddimport.aspx.cs" Inherits="DD2015_45.Forms.dax.dx_ddimport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
  <table>
    <tr>
      <td></td>
      <td></td>
    </tr>

    <tr>
      <td>
        <asp:LinkButton ID="bt_SERQ" runat="server" CausesValidation="False" CommandName="QUIT" AccessKey="Q" CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_SERQ_Click"></asp:LinkButton>
      </td>
      <td></td>
    </tr>
    <tr>
      <td></td>
      <td></td>
    </tr>

    <tr>
      <td>
        <asp:Label ID="Label1" runat="server" >Excel Sheet</asp:Label>
        <asp:TextBox ID="txtSHEET" runat="server" Width="200px" ForeColor="Blue">Sheet1</asp:TextBox>
      </td>
    </tr>
    <tr>
      <td colspan="2">
        <asp:Label ID="Label3" runat="server">轉入資料</asp:Label>
        <input id="attFbutton" style="WIDTH: 632px; HEIGHT: 22px" tabindex="1" type="file"
          name="attfile1" runat="server" />
      </td>
    </tr>
    <tr>
      <td></td>
      <td>
        <asp:LinkButton ID="bt_SUR" runat="server" CausesValidation="False" CommandName="SURE" AccessKey="S" CssClass="LinkButton80" Text="S確定轉入" Width="80px" OnClick="bt_SUR_Click"></asp:LinkButton>
      </td>
    </tr>
  </table>
  <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
  <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
</asp:Content>
