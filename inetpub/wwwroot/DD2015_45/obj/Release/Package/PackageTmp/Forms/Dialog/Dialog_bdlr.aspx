<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialog_bdlr.aspx.cs" Inherits="DD2015_45.Forms.Dialog.Dialog_bdlr" %>

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
      document.all["oReturn"].value = "1";
      //
      var win = window.parent;
      var dialog = win.$find(oWindow_Id);
      if (iFunc == "tx") {
        win.document.all[iField].value = st_no;
        win.document.all[oField].value = st_cname;
        dialog.hide();
        win.document.all[iField].focus();
      }
      else {
        var oNewMod = document.all["oNewMod"].value;
        var oDataGrid_id = document.all["oDataGrid_id"].value;
        webDataGrid = win.$find(oDataGrid_id);
        var behaviors = webDataGrid.get_behaviors();
        if (oNewMod == "add") {
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          newRow.get_cellByColumnKey(iField).set_value(st_no);
          newRow.get_cellByColumnKey(oField).set_value(st_cname);
          dialog.hide();
          //
          var focus_cell = newRow.get_cellByColumnKey(iField);
          webDataGrid.get_behaviors().get_activation().set_activeCell(focus_cell);
        }
        else {
          var activation = behaviors.get_activation();
          var activeCell = activation.get_activeCell();
          var columnKey = activeCell.get_column().get_key();
          var edit_row = activeCell.get_row();
          dialog.hide();
          //
          var focus_cell = edit_row.get_cellByColumnKey(iField);
          edit_row.get_cellByColumnKey(oField).set_value(st_cname);
          edit_row.get_cellByColumnKey(iField).set_value(st_no);
          webDataGrid.get_behaviors().get_activation().set_activeCell(focus_cell);
        }

      }
      return "waiting...";
    }

  </script>
</head>
<body >
  <form id="form1" runat="server">
    <div>
      <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
      <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
      <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
      <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
      <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
      <input id="iFunc" type="hidden" name="iFunc" runat="server" />
      <input id="oNewMod" type="hidden" name="oNewMod" runat="server" />
      <input id="oDataGrid_id" type="hidden" name="oDataGrid_id" runat="server" />
      <input id="iField" type="hidden" name="iField" runat="server" />
      <input id="oField" type="hidden" name="oField" runat="server" />
      <input id="oWindow_Id" type="hidden" name="oWindow_Id" runat="server" />
      <input id="iIndex" type="hidden" name="iIndex" runat="server" />
      <input id="iInput" type="hidden" name="iInput" runat="server" />
      <input id="oReturn" type="hidden" name="oReturn" runat="server" />
      <table>
        <tr>
          <td>
            <asp:Label ID="lb_bdlr_BDNUM" runat="server" Text="編　　號"></asp:Label>
            <asp:TextBox ID="tx_bdlr_BDNUM" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
          </td>
          <td >
            <asp:Label ID="lb_bdlr_BDTEL" runat="server" Text="連絡電話"></asp:Label>
            <asp:TextBox ID="tx_bdlr_BDTEL" Width="90px" runat="server" MaxLength="120"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td colspan="2" >
            <asp:Label ID="lb_bdlr_BDNAM" runat="server" Text="名　　稱"></asp:Label>
            <asp:TextBox ID="tx_bdlr_BDNAM" Width="250px" runat="server" MaxLength="100"></asp:TextBox>
          </td>
         <td>
            <asp:LinkButton ID="bt_08" runat="server" CausesValidation="False" CommandName="SERCH" AccessKey="F" CssClass="LinkButton80" Text="F查詢" Width="80px" OnClick="bt_08_Click"></asp:LinkButton>
          </td>
        </tr>
      </table>
      <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
      <asp:GridView ID="gr_GridView_bdlr" runat="server" SkinID="gridviewSkinNoAlter" AutoGenerateColumns="False" DataKeyNames="bdlr_gkey" EnableModelValidation="True" AllowPaging="false" OnRowDataBound="gr_GridView_bdlr_RowDataBound" OnPageIndexChanged="gr_GridView_bdlr_PageIndexChanged" OnPageIndexChanging="gr_GridView_bdlr_PageIndexChanging" OnSelectedIndexChanged="gr_GridView_bdlr_SelectedIndexChanged">
        <Columns>
          <asp:TemplateField>
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_bdlr_BDNUM.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <input id="tx_bdlr_gkey02" type="hidden" name="tx_bdlr_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"bdlr_gkey").ToString() %>' runat="server" />
              <input id="tx_bdlr_mkey02" type="hidden" name="tx_bdlr_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"bdlr_mkey").ToString() %>' runat="server" />
              <asp:HyperLink Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"bdlr_BDNUM") %>' runat="server" ID="hy_bdlr_BDNUM"
                href=<%# "javascript:btnSUR_tx('"+DataBinder.Eval(Container.DataItem,"bdlr_BDNUM")+"','"+DataBinder.Eval(Container.DataItem,"bdlr_BDNAM")+"')" %> />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_bdlr_BDNAM.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:HyperLink Width="300px" Text='<%# DataBinder.Eval(Container.DataItem,"bdlr_BDNAM") %>' runat="server" ID="hy_bdlr_BDNAM"
                href=<%# "javascript:btnSUR_tx('"+DataBinder.Eval(Container.DataItem,"bdlr_BDNUM")+"','"+DataBinder.Eval(Container.DataItem,"bdlr_BDNAM")+"')" %> />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
    </div>
  </form>
</body>
</html>

