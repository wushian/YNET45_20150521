<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_bpud.aspx.cs" Inherits="DD2015_45.Forms.bas.fm_bpud" %>

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
            <igtxt:WebImageButton ID="bt_SAV" runat="server"  AccessKey="S" UseBrowserDefaults="False"
              Height="90%" Text="S存檔" ImageDirectory="../../images/" OnClick="bt_SAV_Click">
              <Appearance>
                <Image Url="form_save.png"></Image>
                <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
              </Appearance>
            </igtxt:WebImageButton>
          </td>
          <td>
            <igtxt:WebImageButton ID="bt_02" runat="server" AccessKey="N" UseBrowserDefaults="False"
              Height="90%" Text="N新增" ImageDirectory="../../images/" OnClick="bt_02_Click">
              <Appearance>
                <Image Url="form_new.png"></Image>
                <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
              </Appearance>
            </igtxt:WebImageButton>
          </td>
          <td>
            <igtxt:WebImageButton ID="bt_03" runat="server" AccessKey="I" UseBrowserDefaults="False"
              Height="90%" Text="I插入" ImageDirectory="../../images/">
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
            <igtxt:WebImageButton ID="bt_05" runat="server" AccessKey="X" AutoSubmit="false" UseBrowserDefaults="False"
              Height="90%" Text="X刪除" ImageDirectory="../../images/" OnClick="bt_05_Click">
              <Appearance>
                <Image Url="form_delete.png"></Image>
                <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
              </Appearance>
            </igtxt:WebImageButton>
          </td>
          <td>
            <igtxt:WebImageButton ID="bt_06" runat="server" AccessKey="O" UseBrowserDefaults="False"
              Height="90%" Text="O複製" ImageDirectory="../../images/">
              <Appearance>
                <Image Url="form_copy.png"></Image>
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
            <igtxt:WebImageButton ID="bt_08" runat="server" AccessKey="F" UseBrowserDefaults="False"
              Height="90%" Text="F查詢" ImageDirectory="../../images/" OnClick="bt_08_Click">
              <Appearance>
                <Image Url="form_serch.png"></Image>
                <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
              </Appearance>
            </igtxt:WebImageButton>
          </td>
          <td>
            <igtxt:WebImageButton ID="bt_09" runat="server" AccessKey="T" UseBrowserDefaults="False"
              Height="90%" Text="T轉單" ImageDirectory="../../images/">
              <Appearance>
                <Image Url="form_copy.png"></Image>
                <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
              </Appearance>
            </igtxt:WebImageButton>
          </td>
          <td>
            <igtxt:WebImageButton ID="bt_10" runat="server" AccessKey="E" UseBrowserDefaults="False"
              Height="90%" Text="Excel" ImageDirectory="../../images/">
              <Appearance>
                <Image Url="form_excel.png"></Image>
                <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
              </Appearance>
            </igtxt:WebImageButton>
          </td>
          <td>
            <igtxt:WebImageButton ID="bt_11" runat="server" UseBrowserDefaults="False"
              Height="90%" Text="功能" ImageDirectory="../../images/">
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
      <br />
      <ig:WebTab ID="WebTab1" runat="server" Height="360px" Width="1200px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px" SelectedIndex="1">
        <Tabs>
          <ig:ContentTabItem runat="server" Text="一般" Key="GEN" VisibleIndex="0">
            <Template>
              <asp:Panel ID="PaneComm" runat="server" Width="1100px" Height="330px">
                <table>
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="lb_bpud_BPNUM" runat="server" Text="商品編號"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPNUM" Width="280px" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                    <td colspan="2">
                      <asp:Label ID="lb_bpud_BPNUB" runat="server" Text="條碼編號"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPNUB" Width="280px" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                    <td colspan="4" rowspan="10">
                      <input id="attimg01" type="file" name="attimg01" runat="server" />
                      <asp:Button ID="bt_dcnews_DCPC1_MOD" Text="更新" runat="server" Width="64px" OnClick="bt_dcnews_DCPC1_MOD_Click" />
                      <br />
                      <asp:Image ID="ig_dcnews_DCPC1" Height="200px" runat="server" Width="200px"></asp:Image>
                      <br />
                      <asp:Button ID="bt_dcnews_DCPC1_DEL" Text="刪圖" runat="server" Width="64px" OnClick="bt_dcnews_DCPC1_DEL_Click" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="lb_bpud_BPTNA" runat="server" Text="繁體名稱"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPTNA" Width="280px" runat="server" MaxLength="100"></asp:TextBox>
                    </td>
                    <td colspan="2">
                      <asp:Label ID="lb_bpud_BPCLA" runat="server" Text="規　　格"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPCLA" Width="280px" runat="server" MaxLength="100"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="lb_bpud_BPCNA" runat="server" Text="簡體名稱"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPCNA" Width="280px" runat="server" MaxLength="100"></asp:TextBox>
                    </td>
                    <td colspan="2">
                      <asp:Label ID="lb_bpud_BPENA" runat="server" Text="英文名稱"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPENA" Width="280px" runat="server" MaxLength="100"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPDP1" runat="server" Text="分類　一"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPDP1" Width="120px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPDP1" Width="0px" Visible="false" runat="server" />
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDP2" runat="server" Text="分類　二"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPDP2" Width="90px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPDP2" Width="0px" Visible="false" runat="server" />
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDP3" runat="server" Text="分類　三"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPDP3" Width="120px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPDP3" Width="0px" Visible="false" runat="server" />
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDP4" runat="server" Text="分類　四"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPDP4" Width="90px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPDP4" Width="0px" Visible="false" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <table>
                        <tr>
                          <td colspan="1">
                            <asp:Label ID="lb_bpud_BPDX1" runat="server" Text="分類　一"></asp:Label><br />
                          </td>
                          <td colspan="3">
                            <ig:WebDropDown ID="dr_bpud_BPDX1" runat="server" DisplayMode="DropDownList" EnableMultipleSelection="True" StyleSetName="Appletini" StyleSetPath="~\ig_res" Width="280px">
                            </ig:WebDropDown>
                            <asp:TextBox ID="tx_bpud_BPDX1" Width="0px" Visible="false" runat="server" />
                          </td>
                        </tr>
                      </table>
                    </td>
                    <td colspan="2">
                      <table>
                        <tr>
                          <td colspan="1">
                            <asp:Label ID="lb_bpud_BPDX2" runat="server" Text="分類　二"></asp:Label><br />
                          </td>
                          <td colspan="3">
                            <ig:WebDropDown ID="dr_bpud_BPDX2" runat="server" DisplayMode="DropDownList" EnableMultipleSelection="True" StyleSetName="Appletini" StyleSetPath="~\ig_res" Width="280px">
                            </ig:WebDropDown>
                            <asp:TextBox ID="tx_bpud_BPDX2" Width="0px" Visible="false" runat="server" />
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <table>
                        <tr>
                          <td colspan="1">
                            <asp:Label ID="lb_bpud_BPDX3" runat="server" Text="分類　三"></asp:Label><br />
                          </td>
                          <td colspan="3">
                            <ig:WebDropDown ID="dr_bpud_BPDX3" runat="server" DisplayMode="DropDownList" EnableMultipleSelection="True" StyleSetName="Appletini" StyleSetPath="~\ig_res" Width="280px">
                            </ig:WebDropDown>
                            <asp:TextBox ID="tx_bpud_BPDX3" Width="0px" Visible="false" runat="server" />
                          </td>
                        </tr>
                      </table>
                    </td>
                    <td colspan="2">
                      <table>
                        <tr>
                          <td colspan="1">
                            <asp:Label ID="lb_bpud_BPDX4" runat="server" Text="分類　四"></asp:Label><br />
                          </td>
                          <td colspan="3">
                            <ig:WebDropDown ID="dr_bpud_BPDX4" runat="server" DisplayMode="DropDownList" EnableMultipleSelection="True" StyleSetName="Appletini" StyleSetPath="~\ig_res" Width="280px">
                            </ig:WebDropDown>
                            <asp:TextBox ID="tx_bpud_BPDX4" Width="0px" Visible="false" runat="server" />
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPUNI" runat="server" Text="單　　位"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPUNI" Width="120px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPUNI" Width="0px" Visible="false" runat="server" />
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPLAB" runat="server" Text="品牌名稱"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPLAB" Width="90px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPLAB" Width="0px" Visible="false" runat="server" />
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPMDC" runat="server" Text="產　　地"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPMDC" Width="120px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPMDC" Width="0px" Visible="false" runat="server" />
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPYES" runat="server" Text="年度季節"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPYES" Width="90px" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPNCR" runat="server" Text="款式編號"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPNCR" Width="120px" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPCLR" runat="server" Text="顏色代號"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPCLR" Width="90px" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPCLN" runat="server" Text="顏色名稱"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPCLN" Width="120px" runat="server" MaxLength="40"></asp:TextBox>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPSIZ" runat="server" Text="尺寸名稱"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPSIZ" Width="90px" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPCUS" runat="server" Text="供應商一"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPCUS" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                      <asp:TextBox ID="tx_bdlr_BPCUS" Width="60px" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPOD1" runat="server" Text="採購日一"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPOD1" Width="90px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>

                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPCU2" runat="server" Text="供應商二"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPCU2" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                      <asp:TextBox ID="tx_bdlr_BPCU2" Width="60px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPOD2" runat="server" Text="採購日二"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPOD2" Width="90px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="lb_bpud_BPDSN" runat="server" Text="設計人員"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPDSN" Width="280px" runat="server" MaxLength="240"></asp:TextBox>
                    </td>
                    <td colspan="2">
                      <asp:Label ID="lb_bpud_BPSSN" runat="server" Text="企劃人員"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPSSN" Width="280px" runat="server" MaxLength="240"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPDE1" runat="server" Text="售價含稅"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPDE1" Width="120px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDE2" runat="server" Text="會員含稅"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPDE2" Width="90px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDT1" runat="server" Text="上架日期"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPDT1" Width="120px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPETQ" runat="server" Text="下架日期"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPETQ" Width="90px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="3">
                      <asp:Label ID="lb_bpud_BPRMK" runat="server" Text="備註說明"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPRMK" Width="500px" runat="server" MaxLength="80"></asp:TextBox>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPFLG" runat="server" Text="功能旗標"></asp:Label>
                      <asp:TextBox ID="tx_bpud_BPFLG" Width="90px" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                  </tr>
                </table>
              </asp:Panel>
            </Template>
          </ig:ContentTabItem>
          <ig:ContentTabItem runat="server" Text="資料" Key="DATA" VisibleIndex="1">
            <Template>
              <asp:Panel ID="PaneData" runat="server" Width="1100px" Height="330px">
                <table>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPNPC" runat="server" Text="標準進價"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPNPC" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPSPC" runat="server" Text="最近進價"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPSPC" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPVPC" runat="server" Text="平均進價"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPVPC" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPGPC" runat="server" Text="參考進價"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPGPC" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPDE3" runat="server" Text="批發Ｃ價"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPDE3" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDE4" runat="server" Text="批發Ｄ價"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPDE4" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDE5" runat="server" Text="批發Ｅ價"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPDE5" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPDT3" runat="server" Text="進貨日期"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPDT3" Width="80px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDT4" runat="server" Text="銷貨日期"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPDT4" Width="80px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDT5" runat="server" Text="盤點日期"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPDT5" Width="80px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPMND" runat="server" Text="外幣進價"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPMND" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPMNR" runat="server" Text="外幣倍數"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPMNR" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPMNY" runat="server" Text="外幣幣別"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPMNY" Width="80px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPMNY" Width="0px" Visible="false" runat="server" />
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPCUQ" runat="server" Text="經濟採量"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPCUQ" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPUN1" runat="server" Text="進貨單位"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPUN1" Width="80px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPUN1" Width="0px" Visible="false" runat="server" />
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPUN3" runat="server" Text="銷貨單位"></asp:Label>
                      <asp:DropDownList ID="dr_bpud_BPUN3" Width="80px" runat="server" />
                      <asp:TextBox ID="tx_bpud_BPUN3" Width="0px" Visible="false" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPQTM" runat="server" Text="調撥數量"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPQTM" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPQTH" runat="server" Text="最高量"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPQTH" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPQTL" runat="server" Text="最低量"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPQTL" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPQTS" runat="server" Text="安全量"></asp:Label>
                      <ig:WebNumericEditor ID="tx_bpud_BPQTS" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPDT2" runat="server" Text="修改日期"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPDT2" Width="80px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:Label ID="lb_bpud_BPET1" runat="server" Text="停進貨日"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPET1" Width="80px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPETM" runat="server" Text="停調貨日"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPETM" Width="80px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                    <td>
                      <asp:Label ID="lb_bpud_BPET3" runat="server" Text="停銷貨日"></asp:Label>
                      <ig:WebDateTimeEditor ID="tx_bpud_BPET3" Width="80px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                    </td>
                  </tr>
                </table>
              </asp:Panel>
            </Template>
          </ig:ContentTabItem>
          <ig:ContentTabItem runat="server" Text="繁體說明" Key="TDOC" VisibleIndex="2">
            <Template>
              <asp:Panel ID="PaneDoc" runat="server" Width="1000px" Height="330px">
                <table>
                  <tr>
                    <td>
                      <br />
                      <asp:Label ID="lb_bpud_BPWSH" runat="server" Text="使用說明"></asp:Label>
                      <br />
                      <ighedit:WebHtmlEditor ID="ht_bpud_BPWSH" runat="server" StyleSetName="Appletini" StyleSetPath="~/ig_res" ImageDirectory="~/ig_common/Images/htmleditor/" Width="560px" Height="300px" UploadedFilesDirectory="/webpic/Upload">
                      </ighedit:WebHtmlEditor>
                    </td>
                    <td>
                      <br />
                      <asp:Label ID="lb_bpud_BPDSC" runat="server" Text="成份說明"></asp:Label>
                      <br />
                      <ighedit:WebHtmlEditor ID="ht_bpud_BPDSC" runat="server" StyleSetName="Appletini" StyleSetPath="~/ig_res" ImageDirectory="~/ig_common/Images/htmleditor/" Width="560px" Height="300px" UploadedFilesDirectory="/webpic/Upload">
                      </ighedit:WebHtmlEditor>
                    </td>
                  </tr>
                </table>
              </asp:Panel>
            </Template>
          </ig:ContentTabItem>
          <ig:ContentTabItem runat="server" Text="簡體說明" Key="CDOC" VisibleIndex="3">
            <Template>
              <asp:Panel ID="PaneDocC" runat="server" Width="1000px" Height="330px">
                <table>
                  <tr>
                    <td>
                      <br />
                      <asp:Label ID="lb_bpud_BPWSHC" runat="server" Text="簡體使用說明"></asp:Label>
                      <br />
                      <ighedit:WebHtmlEditor ID="ht_bpud_BPWSHC" runat="server" StyleSetName="Appletini" StyleSetPath="~/ig_res" ImageDirectory="~/ig_common/Images/htmleditor/" Width="560px" Height="300px" UploadedFilesDirectory="/webpic/Upload">
                      </ighedit:WebHtmlEditor>
                    </td>
                    <td>
                      <br />
                      <asp:Label ID="lb_bpud_BPDSCC" runat="server" Text="簡體成份說明"></asp:Label>
                      <br />
                      <ighedit:WebHtmlEditor ID="ht_bpud_BPDSCC" runat="server" StyleSetName="Appletini" StyleSetPath="~/ig_res" ImageDirectory="~/ig_common/Images/htmleditor/" Width="560px" Height="300px" UploadedFilesDirectory="/webpic/Upload">
                      </ighedit:WebHtmlEditor>
                    </td>
                  </tr>
                </table>
              </asp:Panel>
            </Template>
          </ig:ContentTabItem>
          <ig:ContentTabItem runat="server" Text="英文說明" Key="EDOC" VisibleIndex="4">
            <Template>
              <asp:Panel ID="PaneDocE" runat="server" Width="1000px" Height="330px">
                <table>
                  <tr>
                    <td>
                      <br />
                      <asp:Label ID="lb_bpud_BPWSHCE" runat="server" Text="英文使用說明"></asp:Label>
                      <br />
                      <ighedit:WebHtmlEditor ID="ht_bpud_BPWSHE" runat="server" StyleSetName="Appletini" StyleSetPath="~/ig_res" ImageDirectory="~/ig_common/Images/htmleditor/" Width="560px" Height="300px" UploadedFilesDirectory="/webpic/Upload">
                      </ighedit:WebHtmlEditor>
                    </td>
                    <td>
                      <br />
                      <asp:Label ID="lb_bpud_BPDSCCE" runat="server" Text="英文成份說明"></asp:Label>
                      <br />
                      <ighedit:WebHtmlEditor ID="ht_bpud_BPDSCE" runat="server" StyleSetName="Appletini" StyleSetPath="~/ig_res" ImageDirectory="~/ig_common/Images/htmleditor/" Width="560px" Height="300px" UploadedFilesDirectory="/webpic/Upload">
                      </ighedit:WebHtmlEditor>
                    </td>
                  </tr>
                </table>
              </asp:Panel>
            </Template>
          </ig:ContentTabItem>
        </Tabs>
      </ig:WebTab>
      <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
      <asp:GridView ID="gr_GridView_bpud" runat="server" SkinID="gridviewSkinAlter" AutoGenerateColumns="False" DataKeyNames="bpud_gkey" EnableModelValidation="True" AllowPaging="True" OnRowDataBound="gr_GridView_bpud_RowDataBound" OnPageIndexChanged="gr_GridView_bpud_SelectedIndexChanged" OnPageIndexChanging="gr_GridView_bpud_PageIndexChanging" OnSelectedIndexChanged="gr_GridView_bpud_SelectedIndexChanged">
        <Columns>
          <asp:TemplateField>
            <HeaderTemplate>
              <b>選</b>
            </HeaderTemplate>
            <ItemTemplate>
              <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"bpud_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
              <input id="tx_bpud_gkey02" type="hidden" name="tx_bpud_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"bpud_gkey").ToString() %>' runat="server" />
              <input id="tx_bpud_mkey02" type="hidden" name="tx_bpud_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"bpud_mkey").ToString() %>' runat="server" />
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="bpud_BPNUM" >
            <ItemTemplate>
              <asp:Label ID="tx_bpud_BPNUM02" runat="server" Width="150px" Text='<%# DataBinder.Eval(Container.DataItem,"bpud_BPNUM").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="bpud_BPTNA" >
            <ItemTemplate>
              <asp:Label ID="tx_bpud_BPTNA02" runat="server" Width="300px" Text='<%# DataBinder.Eval(Container.DataItem,"bpud_BPTNA").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField  HeaderText="bpud_BPCLA" >
            <ItemTemplate>
              <asp:Label ID="tx_bpud_BPCLA02" runat="server" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem,"bpud_BPCLA").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="bpud_BPDE1" >
            <ItemTemplate>
              <ig:WebNumericEditor ID="tx_bpud_BPDE102" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="bpud_BPDE2" >
            <ItemTemplate>
              <ig:WebNumericEditor ID="tx_bpud_BPDE202" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="bpud_BPDT1" >
            <ItemTemplate>
              <asp:Label ID="tx_bpud_BPDT102" runat="server" Width="90px" Text='<%# DataBinder.Eval(Container.DataItem,"bpud_BPDT1").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="bpud_BPETQ">
            <ItemTemplate>
              <asp:Label ID="tx_bpud_BPETQ02" runat="server" Width="90px" Text='<%# DataBinder.Eval(Container.DataItem,"bpud_BPETQ").ToString() %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
      <ig:WebDialogWindow ID="di_Window" runat="server" Width="500px"
        Height="500px" InitialLocation="Centered" Modal="True">
        <ContentPane BackColor="#FAFAFA" ContentUrl="#">
        </ContentPane>
        <Header CaptionText="my dialog" BorderColor="#cccccc">
        </Header>
        <ClientEvents Initialize="di_Window_initialize" />
        <Resizer Enabled="True" />
      </ig:WebDialogWindow>
      <script type="text/javascript">
        function di_Window_initialize(sender, e) {
          sender.hide();
        }
      </script>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
