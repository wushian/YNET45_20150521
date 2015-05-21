<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="dx_daset.aspx.cs" Inherits="DD2015_45.Forms.dax.dx_daset" %>

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
            <asp:LinkButton ID="bt_06" runat="server" CausesValidation="False" CommandName="COPY" AccessKey="O" CssClass="LinkButton80" Text="O複製" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_07" runat="server" CausesValidation="False" CommandName="PRINT" AccessKey="P" CssClass="LinkButton80" Text="P列印" Width="80px" OnClick="bt_07_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_08" runat="server" CausesValidation="False" CommandName="SERCH" AccessKey="F" CssClass="LinkButton80" Text="F查詢" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_09" runat="server" CausesValidation="False" CommandName="TRANS" AccessKey="T" CssClass="LinkButton80" Text="T轉單" Width="80px" OnClick="bt_09_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_10" runat="server" CausesValidation="False" CommandName="EXCEL" AccessKey="E" CssClass="LinkButton80" Text="Excel" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_11" runat="server" CausesValidation="False" CommandName="DETAIL" AccessKey="B" CssClass="LinkButton80" Text="E欄位明細" Width="80px" OnClick="bt_11_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_QUT" runat="server" CausesValidation="False" CommandName="QUIT" AccessKey="Q" CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_QUT_Click"></asp:LinkButton>
          </td>
        </tr>
      </table>
      <table>
        <tr>
          <td>
            <asp:Label ID="lb_DASET_DAREN" runat="server" Text="序　　號"></asp:Label>
            <asp:TextBox ID="tx_DASET_DAREN" Width="160px" runat="server" MaxLength="10"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DASET_DAVER" runat="server" Text="版本編號"></asp:Label>
            <asp:TextBox ID="tx_DASET_DAVER" Width="160px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DASET_DANUM" runat="server" Text="客戶編號"></asp:Label>
            <asp:TextBox ID="tx_DASET_DANUM" Width="160px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="lb_DASET_DAAPX" runat="server" Text="程式名稱"></asp:Label>
            <asp:TextBox ID="tx_DASET_DAAPX" Width="160px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DASET_DANAM" runat="server" Text="繁體名稱"></asp:Label>
            <asp:TextBox ID="tx_DASET_DANAM" Width="160px" runat="server" MaxLength="80"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DASET_DANAC" runat="server" Text="簡體名稱"></asp:Label>
            <asp:TextBox ID="tx_DASET_DANAC" Width="160px" runat="server" MaxLength="80"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DASET_DANAE" runat="server" Text="英文名稱"></asp:Label>
            <asp:TextBox ID="tx_DASET_DANAE" Width="160px" runat="server" MaxLength="80"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="lb_DASET_DATBL" runat="server" Text="TABLE 名稱"></asp:Label>
            <asp:TextBox ID="tx_DASET_DATBL" Width="140px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_DASET_DATYP" runat="server" Text="檔案形態"></asp:Label>
            <asp:TextBox ID="tx_DASET_DATYP" Width="160px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
          <td colspan="2">
            <asp:Label ID="lb_DASET_DARMK" runat="server" Text="備　　註"></asp:Label>
            <asp:TextBox ID="tx_DASET_DARMK" Width="390px" runat="server" MaxLength="20"></asp:TextBox>
          </td>
        </tr>
      </table>
      <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
      <asp:GridView ID="gr_GridView_DASET" SkinID="gridviewSkinAlter" runat="server" AutoGenerateColumns="False" DataKeyNames="DASET_gkey" EnableModelValidation="True" AllowPaging="true" OnRowDataBound="gr_GridView_DASET_RowDataBound" OnPageIndexChanged="gr_GridView_DASET_PageIndexChanged" OnPageIndexChanging="gr_GridView_DASET_PageIndexChanging" OnSelectedIndexChanged="gr_GridView_DASET_SelectedIndexChanged">
        <Columns>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>選</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"DASET_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
              <input id="tx_DASET_gkey02" type="hidden" name="tx_DASET_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"DASET_gkey").ToString() %>' runat="server" />
              <input id="tx_DASET_mkey02" type="hidden" name="tx_DASET_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"DASET_mkey").ToString() %>' runat="server" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DASET_DAREN.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:Label ID="lb_DASET_DAREN02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"DASET_DAREN").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DASET_DAVER.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:Label ID="lb_DASET_DAVER02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"DASET_DAVER").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DASET_DAAPX.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:Label ID="lb_DASET_DAAPX02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"DASET_DAAPX").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DASET_DANAM.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:Label ID="lb_DASET_DANAM02" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"DASET_DANAM").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DASET_DANAC.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:Label ID="lb_DASET_DANAC02" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"DASET_DANAC").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b><%#lb_DASET_DANAE.Text%></b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:Label ID="lb_DASET_DANAE02" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"DASET_DANAE").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
