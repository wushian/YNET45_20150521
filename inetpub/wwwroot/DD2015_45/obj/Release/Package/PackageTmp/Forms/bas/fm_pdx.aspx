<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_pdx.aspx.cs" Inherits="DD2015_45.Forms.bas.fm_pdx" %>

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
      <asp:Panel ID="pn_detail" runat="server" Width="1000px">
        <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
        <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
        <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
        <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
        <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
        <table>
          <tr>
            <td>
              <igtxt:WebImageButton ID="bt_CAN" runat="server" AccessKey="C" UseBrowserDefaults="False"
                Height="90%" Text="C取消" ImageDirectory="../../images/" OnClick="bt_CAN_Click">
                <Appearance>
                  <Image Url="form_cancel.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_SAV" runat="server" AccessKey="S" UseBrowserDefaults="False"
                Height="90%" Text="S存檔" ImageDirectory="../../images/" OnClick="bt_SAV_Click">
                <Appearance>
                  <Image Url="form_save.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_02" runat="server" AccessKey="N" UseBrowserDefaults="False"
                Height="90%" Text="N新增" ImageDirectory="../../images/" OnClick="bt_02_Click" >
                <Appearance>
                  <Image Url="form_new.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_04" runat="server" AccessKey="M" UseBrowserDefaults="False"
                Height="90%" Text="M更正" ImageDirectory="../../images/" OnClick="bt_04_Click">
                <Appearance>
                  <Image Url="form_edit.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_05" runat="server" AccessKey="X" AutoSubmit="false"   UseBrowserDefaults="False"
                Height="90%" Text="X刪除" ImageDirectory="../../images/" OnClick="bt_05_Click" >
                <Appearance>
                  <Image Url="form_delete.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_08" runat="server" AccessKey="F" UseBrowserDefaults="False"
                Height="90%" Text="F查詢" ImageDirectory="../../images/" >
                <Appearance>
                  <Image Url="form_serch.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_07" runat="server" AccessKey="P" UseBrowserDefaults="False"
                Height="90%" Text="P列印" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_print.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_QUT" runat="server" AccessKey="Q" UseBrowserDefaults="False"
                Height="90%" Text="Q離開" ImageDirectory="../../images/" OnClick="bt_QUT_Click" >
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
              <asp:Label ID="lb_pdpdx_BKNUM" runat="server" Text="類別編號"></asp:Label>
              <asp:TextBox ID="tx_pdpdx_BKNUM" Width="200px" runat="server" MaxLength="20"></asp:TextBox>
            </td>
            <td>
              <asp:Label ID="lb_pdpdx_BKACC" runat="server" Text="會計科目"></asp:Label>
              <asp:TextBox ID="tx_pdpdx_BKACC" Width="200px" runat="server" MaxLength="20"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td>
              <asp:Label ID="lb_pdpdx_BKNAM" runat="server" Text="繁體名稱"></asp:Label>
              <asp:TextBox ID="tx_pdpdx_BKNAM" Width="200px" runat="server" MaxLength="80"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td>
              <asp:Label ID="lb_pdpdx_BKNAC" runat="server" Text="簡體名稱"></asp:Label>
              <asp:TextBox ID="tx_pdpdx_BKNAC" Width="200px" runat="server" MaxLength="80"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td>
              <asp:Label ID="lb_pdpdx_BKNAE" runat="server" Text="英文名稱"></asp:Label>
              <asp:TextBox ID="tx_pdpdx_BKNAE" Width="200px" runat="server" MaxLength="80"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td>
              <asp:Label ID="lb_pdpdx_BKNAV" runat="server" Text="印文名稱"></asp:Label>
              <asp:TextBox ID="tx_pdpdx_BKNAV" Width="200px" runat="server" MaxLength="80"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <asp:Label ID="lb_pdpdx_BKDSC" runat="server" Text="類別說明"></asp:Label>
              <asp:TextBox ID="tx_pdpdx_BKDSC" Width="460px" runat="server" MaxLength="200"></asp:TextBox>
            </td>
          </tr>
        </table>
        <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
        <asp:GridView ID="gr_GridView_pdpdx" SkinID="gridviewSkinAlter" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" AllowPaging="True" OnRowDataBound="gr_GridView_pdpdx_RowDataBound" OnPageIndexChanged="gr_GridView_pdpdx_SelectedIndexChanged" OnPageIndexChanging="gr_GridView_pdpdx_PageIndexChanging" OnSelectedIndexChanged="gr_GridView_pdpdx_SelectedIndexChanged" PageSize="20">
          <Columns>
            <asp:TemplateField HeaderText="choose">
              <ItemTemplate>
                <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,st_dd_table+"_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
                <input id="tx_pdpdx_gkey02" type="hidden" name="tx_pdpdx_gkey02" value='<%# DataBinder.Eval(Container.DataItem,st_dd_table+"_gkey").ToString() %>' runat="server" />
                <input id="tx_pdpdx_mkey02" type="hidden" name="tx_pdpdx_mkey02" value='<%# DataBinder.Eval(Container.DataItem,st_dd_table+"_mkey").ToString() %>' runat="server" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="pdpdx_BKNUM">
              <ItemTemplate>
                <asp:Label ID="tx_pdpdx_BKNUM02" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,st_dd_table+"_BKNUM").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField   HeaderText="pdpdx_BKNAM">
              <ItemTemplate>
                <asp:Label ID="tx_pdpdx_BKNAM02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,st_dd_table+"_BKNAM").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="pdpdx_BKNAC">
              <ItemTemplate>
                <asp:Label ID="tx_pdpdx_BKNAC02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,st_dd_table+"_BKNAC").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="pdpdx_BKNAE">
              <ItemTemplate>
                <asp:Label ID="tx_pdpdx_BKNAE02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,st_dd_table+"_BKNAE").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="pdpdx_BKNAV">
              <ItemTemplate>
                <asp:Label ID="tx_pdpdx_BKNAV02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,st_dd_table+"_BKNAV").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="pdpdx_BKACC">
              <ItemTemplate>
                <asp:Label ID="tx_pdpdx_BKACC02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,st_dd_table+"_BKACC").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
          </Columns>
        </asp:GridView>
        <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
