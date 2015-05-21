<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_dbset.aspx.cs" Inherits="DD2015_45.Forms.sys.fm_dbset" %>

<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.WebUI.WebDataInput.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics45.WebUI.WebHtmlEditor.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebHtmlEditor" tagprefix="ighedit" %>

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
      <asp:Panel ID="PanBtns" runat="server" Width="1000px">
        <table>
          <tr>
            <td>
              <igtxt:WebImageButton ID="bt_CAN" AccessKey="C" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="C取消" ImageDirectory="../../images/" OnClick="bt_CAN_Click">
                <Appearance>
                  <Image Url="form_cancel.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_SAV" AccessKey="S" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="S存檔" ImageDirectory="../../images/" OnClick="bt_SAV_Click">
                <Appearance>
                  <Image Url="form_save.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_04" AccessKey="M" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="M更正" ImageDirectory="../../images/" OnClick="bt_04_Click">
                <Appearance>
                  <Image Url="form_edit.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_QUT" runat="server" AccessKey="Q" UseBrowserDefaults="False"
                Height="90%" Text="Q離開" ImageDirectory="../../images/" OnClick="bt_QUT_Click">
                <Appearance>
                  <Image Url="form_quit.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
          </tr>
        </table>
        <table>
          <tr>
            <td>
              <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
            </td>
          </tr>
        </table>
      </asp:Panel>
      <table>
        <tr>
          <td>
            <asp:Panel  ID="pn_left" runat="server" BorderStyle="Inset" Height="700" ScrollBars="Vertical"    Width="200px">
              <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
              </asp:TreeView>
            </asp:Panel>
          </td>
          <td>
            <asp:Panel  ID="Panel1" runat="server" BorderStyle="Inset" Height="700" ScrollBars="Vertical"    Width="800px">
            <asp:GridView ID="gr_GridView_sys_dbset" SkinID="gridviewSkinNoAlter" runat="server" AutoGenerateColumns="False" DataKeyNames="sys_dbset_gkey" EnableModelValidation="True" AllowPaging="false" OnPageIndexChanged="gr_GridView_sys_dbset_PageIndexChanged" OnPageIndexChanging="gr_GridView_sys_dbset_PageIndexChanging" OnRowDataBound="gr_GridView_sys_dbset_RowDataBound" OnSelectedIndexChanged="gr_GridView_sys_dbset_SelectedIndexChanged">
              <Columns>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <b>欄位名稱</b>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <input id="tx_sys_dbset_gkey02" type="hidden" name="tx_sys_dbset_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_gkey").ToString() %>' runat="server" />
                    <input id="tx_sys_dbset_mkey02" type="hidden" name="tx_sys_dbset_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_mkey").ToString() %>' runat="server" />
                    <asp:TextBox ID="tx_sys_dbset_DBFLD02" runat="server" MaxLength="20" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_DBFLD").ToString() %>'></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <b>簡體中文</b>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:TextBox ID="tx_sys_dbset_DBCNA02" runat="server" MaxLength="20" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_DBCNA").ToString() %>'></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <b>繁體中文</b>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:TextBox ID="tx_sys_dbset_DBTNA02" runat="server" MaxLength="20" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_DBTNA").ToString() %>'></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <b>英文名稱</b>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:TextBox ID="tx_sys_dbset_DBENA02" runat="server" MaxLength="20" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_DBENA").ToString() %>'></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <b>越南名稱</b>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:TextBox ID="tx_sys_dbset_DBVNA02" runat="server" MaxLength="20" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_DBVNA").ToString() %>'></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <b>新增預設</b>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:TextBox ID="tx_sys_dbset_DBDEF02" runat="server" MaxLength="100" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_DBDEF").ToString() %>'></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <b>排序預設</b>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:TextBox ID="tx_sys_dbset_DBSOR02" runat="server" MaxLength="1" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_dbset_DBSOR").ToString() %>'></asp:TextBox>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
              </asp:Panel> 
          </td>
        </tr>
      </table>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
