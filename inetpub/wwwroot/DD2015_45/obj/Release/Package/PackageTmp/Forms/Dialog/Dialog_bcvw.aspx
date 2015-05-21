<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialog_bcvw.aspx.cs" Inherits="DD2015_45.Forms.Dialog.Dialog_bcvw" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.WebUI.WebDataInput.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics45.WebUI.WebHtmlEditor.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebHtmlEditor" tagprefix="ighedit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
  <script type="text/javascript">
    function btnSUR_tx(st_no, st_cname) {
      var iFunc = document.all["iFunc"].value;
      var iField = document.all["iField"].value;
      var oField = document.all["oField"].value;
      var oWindow_Id = document.all["oWindow_Id"].value;
      //
      var win = window.parent;
      var dialog = win.$find(oWindow_Id);
      if (iFunc != "gr") {
        win.document.all[iField].value = st_no;
        win.document.all[oField].value = st_cname;
        dialog.hide();
        win.document.all[iField].focus();
      }
      return "waiting...";
    }

  </script>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
      <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
      <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
      <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
      <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
      <input id="iFunc" type="hidden" name="iFunc" runat="server" />
      <input id="iField" type="hidden" name="iField" runat="server" />
      <input id="oField" type="hidden" name="oField" runat="server" />
      <input id="oWindow_Id" type="hidden" name="oWindow_Id" runat="server" />
      <input id="iIndex" type="hidden" name="iIndex" runat="server" />
      <input id="iInput" type="hidden" name="iInput" runat="server" />
      <table>
        <tr>
          <td>
            <asp:Label ID="lb_bcvw_BCNUM" runat="server" Text="編　　號"></asp:Label>
            <asp:TextBox ID="tx_bcvw_BCNUM" Width="80px" runat="server" MaxLength="10"></asp:TextBox>

          </td>
          <td>
            <asp:Label ID="lb_bcvw_BCNAM" runat="server" Text="名　　稱"></asp:Label>
            <asp:TextBox ID="tx_bcvw_BCNAM" Width="80px" runat="server" MaxLength="100"></asp:TextBox>

          </td>
          <td>
            <asp:Label ID="lb_bcvw_BCTEL" runat="server" Text="連絡電話"></asp:Label>
            <asp:TextBox ID="tx_bcvw_BCTEL" Width="80px" runat="server" MaxLength="120"></asp:TextBox>
          </td>
          <td>
            <asp:LinkButton ID="bt_08" runat="server" CausesValidation="False" CommandName="SERCH" AccessKey="F" CssClass="LinkButton80" Text="F查詢" Width="80px" OnClick="bt_08_Click"></asp:LinkButton>
          </td>
        </tr>
      </table>
      <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
      <asp:GridView ID="gr_GridView_bcvw" runat="server" SkinID="gridviewSkinNoAlter" AutoGenerateColumns="False" DataKeyNames="bcvw_gkey" EnableModelValidation="True" AllowPaging="false" OnRowDataBound="gr_GridView_bcvw_RowDataBound" OnPageIndexChanged="gr_GridView_bcvw_PageIndexChanged" OnPageIndexChanging="gr_GridView_bcvw_PageIndexChanging" OnSelectedIndexChanged="gr_GridView_bcvw_SelectedIndexChanged">
        <Columns>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%# DD2015_45.PublicVariable.st_choose %></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"bcvw_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
              <input id="tx_bcvw_gkey02" type="hidden" name="tx_bcvw_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"bcvw_gkey").ToString() %>' runat="server" />
              <input id="tx_bcvw_mkey02" type="hidden" name="tx_bcvw_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"bcvw_mkey").ToString() %>' runat="server" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_bcvw_BCNUM.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:HyperLink Width="150px" Text='<%# DataBinder.Eval(Container.DataItem,"bcvw_BCNUM") %>' runat="server" ID="hy_bcvw_BCNUM"
                href=<%# "javascript:btnSUR_tx('"+DataBinder.Eval(Container.DataItem,"bcvw_BCNUM")+"','"+DataBinder.Eval(Container.DataItem,"bcvw_BCNAM")+"')" %> />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_bcvw_BCNAM.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:HyperLink Width="150px" Text='<%# DataBinder.Eval(Container.DataItem,"bcvw_BCNAM") %>' runat="server" ID="hy_bcvw_BCNAM"
                href=<%# "javascript:btnSUR_tx('"+DataBinder.Eval(Container.DataItem,"bcvw_BCNUM")+"','"+DataBinder.Eval(Container.DataItem,"bcvw_BCNAM")+"')" %> />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
    </div>
  </form>
</body>
</html>
