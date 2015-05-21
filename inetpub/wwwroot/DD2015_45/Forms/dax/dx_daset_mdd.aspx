<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="dx_daset_mdd.aspx.cs" Inherits="DD2015_45.Forms.dax.dx_daset_mdd" %>

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
        <asp:Label ID="lb_mtable" runat="server">mtable</asp:Label>
        <asp:TextBox ID="tx_mtable" runat="server" Width="200px" ForeColor="Blue"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="lb_dtable1" runat="server">dtable1</asp:Label>
        <asp:TextBox ID="tx_dtable1" runat="server" Width="200px" ForeColor="Blue"></asp:TextBox>
      </td>
    </tr>
       <tr>
      <td>
        <asp:Label ID="lb_dtable2" runat="server">dtable1</asp:Label>
        <asp:TextBox ID="tx_dtable2" runat="server" Width="200px" ForeColor="Blue"></asp:TextBox>
      </td>
    </tr>
  
    <tr>
      <td></td>
      <td>
        <asp:LinkButton ID="bt_SUR" runat="server" CausesValidation="False" CommandName="SURE" AccessKey="S" CssClass="LinkButton80" Text="S確定產生" Width="80px" OnClick="bt_SUR_Click" ></asp:LinkButton>
      </td>
    </tr>
  </table>
  <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
  <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
</asp:Content>
