<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_httpx.aspx.cs" Inherits="DD2015_45.Forms.Download.fm_httpx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
  <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
  <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
  <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
  <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
  <input id="hh_qkey" type="hidden" name="hh_qkey" runat="server" />
  <table>
    <tr>
      <td>
        <asp:LinkButton ID="bt_QUT" runat="server" CausesValidation="False" CommandName="QUIT" AccessKey="Q" CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_QUT_Click"></asp:LinkButton>
      </td>
    </tr>
        <tr>
      <td>

      </td>
    </tr>
    <tr>
      <td>
        <asp:HyperLink id="HyFile1" runat="server" >HyperLink</asp:HyperLink>
      </td>
    </tr>
    <tr>
      <td>

      </td>
    </tr>
    <tr>
      <td>
         <asp:HyperLink id="HyFile2" runat="server" >HyperLink</asp:HyperLink>
      </td>
    </tr>
    <tr>
      <td>
         <asp:HyperLink id="HyFile3" runat="server" >HyperLink</asp:HyperLink>
      </td>
    </tr>
    <tr>
      <td>
         <asp:HyperLink id="HyFile4" runat="server" >HyperLink</asp:HyperLink>
      </td>
    </tr>
  </table>

</asp:Content>
