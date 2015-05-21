<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_ss102.aspx.cs" Inherits="DD2015_45.Forms.sys.fm_ss102" %>
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
            <asp:LinkButton ID="bt_07" runat="server" CausesValidation="False" CommandName="PRINT" AccessKey="P" CssClass="LinkButton80" Text="P列印" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_08" runat="server" CausesValidation="False" CommandName="SERCH" AccessKey="F" CssClass="LinkButton80" Text="F查詢" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_09" runat="server" CausesValidation="False" CommandName="TRANS" AccessKey="T" CssClass="LinkButton80" Text="T轉單" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_10" runat="server" CausesValidation="False" CommandName="EXCEL" AccessKey="E" CssClass="LinkButton80" Text="Excel" Width="80px"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_11" runat="server" CausesValidation="False" CommandName="MODALL" AccessKey="B" CssClass="LinkButton80" Text="L整批修改" Width="80px" OnClick="bt_11_Click"></asp:LinkButton>
          </td>
          <td>
            <asp:LinkButton ID="bt_QUT" runat="server" CausesValidation="False" CommandName="QUIT" AccessKey="Q" CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_QUT_Click"></asp:LinkButton>
          </td>
        </tr>
      </table>
      <table>
        <tr>
          <td>
            <asp:Label ID="lb_ss102_kind" runat="server" Text="模塊"></asp:Label>
            <asp:DropDownList ID="dr_ss102_kind" Width="100px" runat="server" />
            <asp:TextBox Visible="false" runat="server" Width="0px" ID="tx_ss102_kind" />
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="lb_ss102_code" runat="server" Text="代號"></asp:Label>
            <asp:TextBox ID="tx_ss102_code" Width="100px" runat="server" MaxLength="10"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_ss102_name" runat="server" Text="名稱"></asp:Label>
            <asp:TextBox ID="tx_ss102_name" Width="305px" runat="server" MaxLength="100"></asp:TextBox>
          </td>
          <td>
            <asp:Label ID="lb_ss102_status" runat="server" Text="參數值"></asp:Label>
            <asp:TextBox ID="tx_ss102_status" Width="246px" runat="server" MaxLength="100"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td colspan="3">
            <asp:Label ID="lb_ss102_remark" runat="server" Text="備注"></asp:Label>
            <asp:TextBox ID="tx_ss102_remark" Width="756px" runat="server" MaxLength="100"></asp:TextBox>
          </td>
        </tr>
      </table>
      <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
      <asp:GridView SkinID="gridviewSkinAlter" ID="gr_GridView_ss102" runat="server" AutoGenerateColumns="False" DataKeyNames="ss102_gkey" EnableModelValidation="True" OnPageIndexChanged="gr_GridView_ss102_PageIndexChanged" OnPageIndexChanging="gr_GridView_ss102_PageIndexChanging" OnSelectedIndexChanged="gr_GridView_ss102_SelectedIndexChanged" OnRowDataBound="gr_GridView_ss102_RowDataBound" AllowPaging="True">
        <Columns>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>選</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"ss102_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
              <input id="tx_ss102_gkey02" type="hidden" name="tx_ss102_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"ss102_gkey").ToString() %>' runat="server" />
              <input id="tx_ss102_mkey02" type="hidden" name="tx_ss102_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"ss102_mkey").ToString() %>' runat="server" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>模塊</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox Visible="false" runat="server" ID="tx_ss102_kind02" />
              <asp:DropDownList Width="80px" runat="server" ID="dr_ss102_kind02" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>代號</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_ss102_code02" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container.DataItem,"ss102_code").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>名稱</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_ss102_name02" runat="server" Width="250px" Text='<%# DataBinder.Eval(Container.DataItem,"ss102_name").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>參數值</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_ss102_status02" runat="server" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem,"ss102_status").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>備注</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:TextBox ID="tx_ss102_remark02" runat="server" Width="500px" Text='<%# DataBinder.Eval(Container.DataItem,"ss102_remark").ToString() %>'></asp:TextBox>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
