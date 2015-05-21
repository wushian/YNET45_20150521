<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="dx_dbset.aspx.cs" Inherits="DD2015_45.Forms.dax.dx_dbset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:UpdatePanel ID="UpdatePanelForm" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
      <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
      <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
      <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
      <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
      <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
      <input id="hh_qkey" type="hidden" name="hh_qkey" runat="server" />
      <table>
        <tr>
          <td>
            <asp:LinkButton ID="bt_CAN" runat="server" CausesValidation="False" CommandName="CANCEL" AccessKey="C" CssClass="LinkButton80" Text="C上一步" Width="80px" OnClick="bt_CAN_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_SAV" runat="server" CausesValidation="False" CommandName="SAVE" AccessKey="S" CssClass="LinkButton80" Text="S完成" Width="80px" OnClick="bt_SAV_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_02" runat="server" CausesValidation="False" CommandName="NEW" AccessKey="N" CssClass="LinkButton80" Text="N新增" Width="80px" OnClick="bt_02_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_03" runat="server" CausesValidation="False" CommandName="INSERT" AccessKey="I" CssClass="LinkButton80" Text="I插入" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_04" runat="server" CausesValidation="False" CommandName="MODIFY" AccessKey="M" CssClass="LinkButton80" Text="M修改" Width="80px" OnClick="bt_04_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_05" runat="server" CausesValidation="False" CommandName="DELETE" AccessKey="X" CssClass="LinkButton80" Text="X刪除" Width="80px" OnClick="bt_05_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_06" runat="server" CausesValidation="False" CommandName="COPY" AccessKey="O" CssClass="LinkButton80" Text="O複製" Width="80px" OnClick="bt_06_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_07" runat="server" CausesValidation="False" CommandName="PRINT" AccessKey="P" CssClass="LinkButton80" Text="P列印" Width="80px" OnClick="bt_07_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_08" runat="server" CausesValidation="False" CommandName="SERCH" AccessKey="F" CssClass="LinkButton80" Text="F查詢" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_09" runat="server" CausesValidation="False" CommandName="TRANS" AccessKey="T" CssClass="LinkButton80" Text="T轉單" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_10" runat="server" CausesValidation="False" CommandName="EXCEL" AccessKey="E" CssClass="LinkButton80" Text="Excel" Width="80px" OnClick="bt_10_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_11" runat="server" CausesValidation="False" CommandName="MODALL" AccessKey="B" CssClass="LinkButton80" Text="B整批修改" Width="80px" OnClick="bt_11_Click1"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_12" runat="server" CausesValidation="False" CommandName="SQL" CssClass="LinkButton80" Text="SQL" Width="80px" OnClick="bt_12_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_13" runat="server" CausesValidation="False" CommandName="HTML" CssClass="LinkButton80" Text="HTML" Width="80px" OnClick="bt_13_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_14" runat="server" CausesValidation="False" CommandName="SOURCE" CssClass="LinkButton80" Text="SOURCE" Width="80px" OnClick="bt_14_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_QUT" runat="server" CausesValidation="False" CommandName="QUIT" AccessKey="Q" CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_QUT_Click"></asp:LinkButton>
          </td>
        </tr>
      </table>
      <table>
        <tr>
          <td>
            <asp:Label ID="lb_DBSET_DBVER" runat="server" Text="版本編號"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBVER" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBNUM" runat="server" Text="客戶編號"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBNUM" Width="80px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBAPX" runat="server" Text="程式名稱"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBAPX" Width="140px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="lb_DBSET_DBITM" runat="server" Text="項　　次"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBITM" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBFLD" runat="server" Text="欄位名稱"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBFLD" Width="80px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBTNA" runat="server" Text="繁體名稱"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBTNA" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBTYP" runat="server" Text="資料型態"></asp:Label>
            <asp:DropDownList ID="dr_DBSET_DBTYP" Width="230px" runat="server" />
            <asp:TextBox ID="tx_DBSET_DBTYP" Width="0px" Visible="false" runat="server" />
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBLEN" runat="server" Text="資料長度"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBLEN" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="lb_DBSET_DBENA" runat="server" Text="英文名稱"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBENA" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBCNA" runat="server" Text="簡體名稱"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBCNA" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBJIA" runat="server" Text="JoinAlias"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBJIA" Width="40px" runat="server" MaxLength="1"></asp:TextBox>
            <asp:TextBox ID="tx_DBSET_DBJIN" Width="80px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td colspan="2">
            <asp:TextBox ID="tx_DBSET_DBJIF" Width="80px" runat="server" MaxLength="100"></asp:TextBox>
            <asp:TextBox ID="tx_DBSET_DBJIK" Width="360px" runat="server" MaxLength="250"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td colspan="1">
            <asp:Label ID="lb_DBSET_DBROW" runat="server" Text="ROW"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBROW" Width="40px" runat="server" MaxLength="10"></asp:TextBox>
            <asp:Label ID="lb_DBSET_DBCOL" runat="server" Text="COL"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBCOL" Width="40px" runat="server" MaxLength="10"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBUCO" runat="server" Text="使用元件"></asp:Label>
            <asp:DropDownList ID="dr_DBSET_DBUCO" Width="140px" runat="server" />
            <asp:TextBox ID="tx_DBSET_DBUCO" Width="0px" Visible="false" runat="server" />
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBWID" runat="server" Text="元件寬度"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBWID" Width="70px" runat="server" MaxLength="10"></asp:TextBox>
          </td>
          <td colspan="2">
            <asp:Label ID="lb_DBSET_DBUED" runat="server" Text="EDIT寬度"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBUED" Width="70px" runat="server" MaxLength="10"></asp:TextBox>
            <asp:Label ID="lb_DBSET_DBUTB" runat="server" Text="參考Table"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBUTB" Width="80px" runat="server" MaxLength="20"></asp:TextBox>
            <asp:Label ID="lb_DBSET_DBUHO" runat="server" Text="參考Class"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBUHO" Width="80px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="lb_DBSET_DBGRD" runat="server" Text="GridList"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBGRD" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DBSET_DBDEF" runat="server" Text="Default"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBDEF" Width="80px" runat="server" MaxLength="250"></asp:TextBox>
          </td>
          <td colspan="5">
            <asp:CheckBox ID="ck_DBSET_DBPRY" Width="100px" runat="server" Text="PrimaryKey" />
            <asp:CheckBox ID="ck_DBSET_DBINS" Width="100px" runat="server" Text="須新增" />
            <asp:CheckBox ID="ck_DBSET_DBMOD" Width="100px" runat="server" Text="可更正" />
            <asp:CheckBox ID="ck_DBSET_DBEMP" Width="100px" runat="server" Text="可空白" />
            <asp:CheckBox ID="ck_DBSET_DBSER" Width="100px" runat="server" Text="查詢鍵" />
            <asp:CheckBox ID="ck_DBSET_DBSOR" Width="100px" runat="server" Text="排序鍵" />
          </td>
        </tr>
        <tr>
          <td colspan="6">
            <asp:Label ID="lb_DBSET_DBRMK" runat="server" Text="備註資料"></asp:Label>
            <asp:TextBox ID="tx_DBSET_DBRMK" Width="800px" runat="server" MaxLength="210"></asp:TextBox>
          </td>
        </tr>
      </table>
      <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
      <asp:GridView ID="gr_GridView_DBSET" SkinID="gridviewSkinAlter" runat="server" AutoGenerateColumns="False" DataKeyNames="DBSET_gkey" EnableModelValidation="True" AllowPaging="True" OnRowDataBound="gr_GridView_DBSET_RowDataBound" OnPageIndexChanged="gr_GridView_DBSET_PageIndexChanged" OnPageIndexChanging="gr_GridView_DBSET_PageIndexChanging" OnSelectedIndexChanged="gr_GridView_DBSET_SelectedIndexChanged">
        <Columns>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>選</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"DBSET_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
              <input id="tx_DBSET_gkey02" type="hidden" name="tx_DBSET_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"DBSET_gkey").ToString() %>' runat="server" />
              <input id="tx_DBSET_mkey02" type="hidden" name="tx_DBSET_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"DBSET_mkey").ToString() %>' runat="server" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>Row-Col</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_DBSET_DBROW02" runat="server" MaxLength="10" Width="20px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBROW").ToString() %>'></asp:TextBox>
              <asp:Label ID="lb_01" runat="server" Text="~"></asp:Label>
              <asp:TextBox ID="tx_DBSET_DBCOL02" runat="server" MaxLength="10" Width="20px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBCOL").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DBSET_DBFLD.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_DBSET_DBFLD02" runat="server" MaxLength="20" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBFLD").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DBSET_DBTNA.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_DBSET_DBTNA02" runat="server" MaxLength="20" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBTNA").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DBSET_DBCNA.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_DBSET_DBCNA02" runat="server" MaxLength="20" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBCNA").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DBSET_DBENA.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_DBSET_DBENA02" runat="server" MaxLength="20" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBENA").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DBSET_DBTYP.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox Visible="false" runat="server" ID="tx_DBSET_DBTYP02" />
              <asp:DropDownList Width="110px" runat="server" ID="dr_DBSET_DBTYP02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>長度</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_DBSET_DBLEN02" runat="server" MaxLength="10" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBLEN").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DBSET_DBUCO.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox Visible="false" runat="server" ID="tx_DBSET_DBUCO02" />
              <asp:DropDownList Width="120px" runat="server" ID="dr_DBSET_DBUCO02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>主鍵</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:CheckBox Width="20px" runat="server" ID="ck_DBSET_DBPRY02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>新增</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:CheckBox Width="20px" runat="server" ID="ck_DBSET_DBINS02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>更正</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:CheckBox Width="20px" runat="server" ID="ck_DBSET_DBMOD02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>空白</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:CheckBox Width="20px" runat="server" ID="ck_DBSET_DBEMP02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>查詢</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:CheckBox Width="2px" runat="server" ID="ck_DBSET_DBSER02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>排序</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:CheckBox Width="20px" runat="server" ID="ck_DBSET_DBSOR02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DBSET_DBJIA.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_DBSET_DBJIA02" runat="server" MaxLength="1" Width="20px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBJIA").ToString() %>'></asp:TextBox>
              <asp:TextBox ID="tx_DBSET_DBJIN02" runat="server" MaxLength="20" Width="70px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBJIN").ToString() %>'></asp:TextBox>
              <asp:TextBox ID="tx_DBSET_DBJIF02" runat="server" MaxLength="100" Width="90px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBJIF").ToString() %>'></asp:TextBox>
              <br />
              <asp:TextBox ID="tx_DBSET_DBJIK02" runat="server" MaxLength="250" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"DBSET_DBJIK").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
