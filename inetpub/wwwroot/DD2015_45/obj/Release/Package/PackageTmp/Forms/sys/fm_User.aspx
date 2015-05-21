<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_User.aspx.cs" Inherits="DD2015_45.Forms.sys.fm_User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
  <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
  <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
  <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
  <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
  <table>
    <tr>
      <td>
        <asp:LinkButton ID="bt_DEL" runat="server"
          CausesValidation="False" CommandName="DELETE" AccessKey="X"
          CssClass="LinkButton80" Text="X刪除" Width="80px" OnClick="bt_DEL_Click"></asp:LinkButton>
      </td>
      <td>
        <asp:LinkButton ID="bt_QUT" runat="server"
          CausesValidation="False" CommandName="QUIT" AccessKey="Q"
          CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_QUT_Click"></asp:LinkButton>
      </td>
    </tr>
  </table>
  <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
  <asp:GridView ID="gr_GridView_sys_user" SkinID="gridviewSkinAlter" runat="server" AutoGenerateColumns="False" DataKeyNames="sys_user_gkey" EnableModelValidation="True" AllowPaging="True" OnSelectedIndexChanged="gr_GridView_sys_user_SelectedIndexChanged" OnRowDataBound="gr_GridView_sys_user_RowDataBound" OnPageIndexChanged="gr_GridView_sys_user_PageIndexChanged" OnPageIndexChanging="gr_GridView_sys_user_PageIndexChanging">
    <Columns>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>選</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"sys_user_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>帳號</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:TextBox ID="tx_sys_user_login_id02" runat="server" Width="64px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_user_login_id").ToString() %>'></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>員工姓名</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:TextBox ID="tx_sys_user_cname02" runat="server" Width="64px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_user_cname").ToString() %>'></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>英文姓名</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:TextBox ID="tx_sys_user_ename02" runat="server"   Width="120px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_user_ename").ToString() %>'></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>login_time</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:TextBox ID="tx_sys_user_login_time02" runat="server"  Width="140px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_user_login_time").ToString() %>'></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>logout_time</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:TextBox ID="tx_sys_user_logout_time02" runat="server"  Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_user_logout_time").ToString() %>'></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>ip</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:TextBox ID="tx_sys_user_client_ip02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_user_client_ip").ToString() %>'></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>狀態</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:TextBox ID="tx_sys_user_login_status02" runat="server"  Width="60px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_user_login_status").ToString() %>'></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField>
        <HeaderTemplate>
          <b>login_gkey</b>
        </HeaderTemplate>
        <ItemTemplate>
          <asp:TextBox ID="tx_sys_user_login_gkey02" runat="server"   Width="270px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_user_login_gkey").ToString() %>'></asp:TextBox>
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>
  <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
</asp:Content>
