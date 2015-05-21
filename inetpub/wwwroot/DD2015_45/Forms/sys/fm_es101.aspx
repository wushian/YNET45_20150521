<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_es101.aspx.cs" Inherits="DD2015_45.Forms.sys.fm_es101" %>

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
      <asp:Panel ID="pn_detail" runat="server"  Width="1000px">
        <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
        <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
        <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
        <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
        <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
        <table>
          <tr>
            <td>
              <igtxt:webimagebutton id="bt_CAN" AccessKey="C" runat="server" usebrowserdefaults="False"
                height="90%" text="C取消" imagedirectory="../../images/" onclick="bt_CAN_Click">
                <Appearance>
                  <Image Url="form_cancel.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_SAV" AccessKey="S" runat="server" usebrowserdefaults="False"
                height="90%" text="S存檔" imagedirectory="../../images/" onclick="bt_SAV_Click">
                <Appearance>
                  <Image Url="form_save.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_02" AccessKey="N" runat="server" usebrowserdefaults="False"
                height="90%" text="N新增" imagedirectory="../../images/" onclick="bt_02_Click">
                <Appearance>
                  <Image Url="form_new.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_03" AccessKey="I" runat="server" usebrowserdefaults="False"
                height="90%" text="I插入" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_new.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_04" AccessKey="M" runat="server" usebrowserdefaults="False"
                height="90%" text="M更正" imagedirectory="../../images/" onclick="bt_04_Click">
                <Appearance>
                  <Image Url="form_edit.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_05" AccessKey="X" runat="server" autosubmit="false" usebrowserdefaults="False"
                height="90%" text="X刪除" imagedirectory="../../images/" onclick="bt_05_Click">
                <Appearance>
                  <Image Url="form_delete.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_06" AccessKey="O" runat="server" usebrowserdefaults="False"
                height="90%" text="O複製" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_copy.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_07" AccessKey="P" runat="server" usebrowserdefaults="False"
                height="90%" text="P列印" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_print.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_08" AccessKey="F" runat="server" usebrowserdefaults="False"
                height="90%" text="F查詢" imagedirectory="../../images/" onclick="bt_08_Click">
                <Appearance>
                  <Image Url="form_serch.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_09" AccessKey="T" runat="server" usebrowserdefaults="False"
                height="90%" text="T轉單" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_copy.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_10" AccessKey="E" runat="server" usebrowserdefaults="False"
                height="90%" text="Excel" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_excel.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_11" runat="server" usebrowserdefaults="False"
                height="90%" text="功能" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_edit.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_QUT" AccessKey="Q" runat="server" usebrowserdefaults="False"
                height="90%" text="Q離開" imagedirectory="../../images/" onclick="bt_QUT_Click">
                <Appearance>
                  <Image Url="form_quit.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
          </tr>
        </table>
        <table>
          <tr>
            <td>
              <asp:Label ID="lb_es101_no" runat="server" Text="員工編號"></asp:Label>
              <asp:TextBox ID="tx_es101_no" Width="100px" runat="server" MaxLength="10"></asp:TextBox>
            </td>
            <td>
              <asp:Label ID="lb_es101_cname" runat="server" Text="中文姓名"></asp:Label>
              <asp:TextBox ID="tx_es101_cname" Width="100px" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td>
              <asp:Label ID="lb_es101_ename" runat="server" Text="英文姓名"></asp:Label>
              <asp:TextBox ID="tx_es101_ename" Width="100px" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td>
              <asp:Label ID="lb_es101_sex" runat="server" Text="性別"></asp:Label>
              <asp:DropDownList ID="dr_es101_sex" Width="50px" runat="server" />
              <asp:TextBox ID="tx_es101_sex" Width="0px" Visible="false" runat="server" />
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <asp:Label ID="lb_es101_tel2" runat="server" Text="行動電話"></asp:Label>
              <asp:TextBox ID="tx_es101_tel2" Width="270px" runat="server" MaxLength="20"></asp:TextBox>
            </td>
            <td colspan="2">
              <asp:Label ID="lb_es101_email1" runat="server" Text="e-mail 　"></asp:Label>
              <asp:TextBox ID="tx_es101_email1" Width="290px" runat="server" MaxLength="100"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td>
              <asp:Label ID="lb_es101_blood" runat="server" Text="血　　型"></asp:Label>
              <asp:TextBox ID="tx_es101_blood" Width="100px" runat="server" MaxLength="10"></asp:TextBox>
            </td>
            <td>
              <asp:Label ID="lb_es101_indate" runat="server" Text="到職日期"></asp:Label>
              <ig:WebDateTimeEditor ID="tx_es101_indate" Width="100px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
            </td>
          </tr>
          <tr>
            <td>
              <asp:Label ID="lb_es101_es001gkey" runat="server" Text="部　　門"></asp:Label>
              <asp:TextBox ID="tx_es101_es001gkey" Width="100px" runat="server" MaxLength="40"></asp:TextBox>
            </td>
            <td>
              <asp:Label ID="lb_es101_es004gkey" runat="server" Text="職　　稱"></asp:Label>
              <asp:TextBox ID="tx_es101_es004gkey" Width="100px" runat="server" MaxLength="40"></asp:TextBox>
            </td>
            <td>
              <asp:Label ID="lb_es101_redate" runat="server" Text="調 職 日"></asp:Label>
              <ig:WebDateTimeEditor ID="tx_es101_redate" Width="100px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
            </td>
            <td>
              <asp:Label ID="lb_es101_nopay" runat="server" Text="離職日"></asp:Label>
              <ig:WebDateTimeEditor ID="tx_es101_nopay" Width="100px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
            </td>
          </tr>
          <tr>
            <td colspan="4">
              <asp:Label ID="lb_es101_addr2" runat="server" Text="通訊地址"></asp:Label>
              <asp:TextBox ID="tx_es101_addr2" Width="600px" runat="server" MaxLength="200"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td colspan="4">
              <asp:Label ID="lb_es101_remark" runat="server" Text="備　　註"></asp:Label>
              <asp:TextBox ID="tx_es101_remark" Width="600px" runat="server" MaxLength="200"></asp:TextBox>
            </td>
          </tr>
        </table>
        <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
        <asp:GridView ID="gr_GridView_es101" runat="server" SkinID="gridviewSkinAlter" AutoGenerateColumns="False" DataKeyNames="es101_gkey" EnableModelValidation="True" AllowPaging="True" OnRowDataBound="gr_GridView_es101_RowDataBound" OnPageIndexChanged="gr_GridView_es101_PageIndexChanged" OnPageIndexChanging="gr_GridView_es101_PageIndexChanging" OnSelectedIndexChanged="gr_GridView_es101_SelectedIndexChanged" PageSize="5">
          <Columns>
            <asp:TemplateField HeaderText="choose" >
              <ItemTemplate>
                <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"es101_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
                <input id="tx_es101_gkey02" type="hidden" name="tx_es101_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"es101_gkey").ToString() %>' runat="server" />
                <input id="tx_es101_mkey02" type="hidden" name="tx_es101_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"es101_mkey").ToString() %>' runat="server" />
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="es101_no" >
              <ItemTemplate>
                <asp:Label ID="tx_es101_no02" runat="server" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem,"es101_no").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="es101_cname">
              <ItemTemplate>
                <asp:Label ID="tx_es101_cname02" runat="server" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem,"es101_cname").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="es101_ename">
              <ItemTemplate>
                <asp:Label ID="tx_es101_ename02" runat="server" Width="142px" Text='<%# DataBinder.Eval(Container.DataItem,"es101_ename").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="es101_tel2">
              <ItemTemplate>
                <asp:Label ID="tx_es101_tel202" runat="server" Width="240px" Text='<%# DataBinder.Eval(Container.DataItem,"es101_tel2").ToString() %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
          </Columns>
        </asp:GridView>
        <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
