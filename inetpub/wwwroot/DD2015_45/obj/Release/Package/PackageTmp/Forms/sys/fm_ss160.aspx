<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_ss160.aspx.cs" Inherits="DD2015_45.Forms.sys.fm_ss160" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
  <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
  <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
  <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
  <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
  <input id="hh_qkey" type="hidden" name="hh_qkey" runat="server" />
  <table border="0">
    <tr>
      <td colspan="2"></td>
      <td colspan="10">
      <asp:LinkButton ID="bt_CAN" runat="server" CausesValidation="False" CommandName="CANCEL" AccessKey="C" CssClass="LinkButton80" Text="C上一步" Width="80px" OnClick="bt_CAN_Click"></asp:LinkButton>
      <asp:LinkButton ID="bt_SAV" runat="server" CausesValidation="False" CommandName="SAVE" AccessKey="S" CssClass="LinkButton80" Text="S完成" Width="80px" OnClick="bt_SAV_Click"></asp:LinkButton>
      <asp:LinkButton ID="bt_04" runat="server" CausesValidation="False" CommandName="MODIFY" AccessKey="M" CssClass="LinkButton80" Text="M修改" Width="80px" OnClick="bt_04_Click" ></asp:LinkButton>
      <asp:LinkButton ID="bt_QUT" runat="server" CausesValidation="False" CommandName="QUIT" AccessKey="Q" CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_QUT_Click"></asp:LinkButton>
      </td>
      <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
    </tr>
    <tr>
      <td colspan="2" rowspan="10">
        <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows">
          <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
          <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
          <ParentNodeStyle Font-Bold="False" />
          <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
      </td>
      <td colspan="10">
        <asp:GridView ID="gr_GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="sys_ss160_gkey" EnableModelValidation="True" AllowPaging="false" OnRowDataBound="gr_GridView1_RowDataBound">
          <Columns>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>使用</b>
              </HeaderTemplate>
              <ItemTemplate>
                <input id="tx_sys_ss160_gkey02" type="hidden" name="tx_sys_ss160_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_gkey").ToString() %>' runat="server" />
                <input id="tx_sys_ss160_mkey02" type="hidden" name="tx_sys_ss160_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_mkey").ToString() %>' runat="server" />
                <asp:CheckBox Width="30px" Checked='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_checked").ToString()=="1" ? true :false  %>' runat="server" ID="ck_sys_ss160_checked" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>編號</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="40px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_buttonno").ToString() %>' runat="server" ID="tx_sys_ss160_buttonno02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>按鈕名稱</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="70px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_button").ToString() %>' runat="server" ID="tx_sys_ss160_button02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>英文</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_button_e").ToString() %>' runat="server" ID="tx_sys_ss160_button_e02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>繁體</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="64px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_button_t").ToString() %>' runat="server" ID="tx_sys_ss160_button_t02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>簡體</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="64px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_button_c").ToString() %>' runat="server" ID="tx_sys_ss160_button_c02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>越南</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="90px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_button_v").ToString() %>' runat="server" ID="tx_sys_ss160_button_v02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>英文Tip</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_tip_e").ToString() %>' runat="server" ID="tx_sys_ss160_tip_e02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>繁體Tip</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="64px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_tip_t").ToString() %>' runat="server" ID="tx_sys_ss160_tip_t02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>簡體Tip</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="64px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_tip_c").ToString() %>' runat="server" ID="tx_sys_ss160_tip_c02" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <HeaderTemplate>
                <b>越南Tip</b>
              </HeaderTemplate>
              <ItemTemplate>
                <asp:TextBox Width="90px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_tip_v").ToString() %>' runat="server" ID="tx_sys_ss160_tip_v02" />
              </ItemTemplate>
            </asp:TemplateField>
          </Columns>
        </asp:GridView>
      </td>
    </tr>
  </table>
  <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
</asp:Content>
