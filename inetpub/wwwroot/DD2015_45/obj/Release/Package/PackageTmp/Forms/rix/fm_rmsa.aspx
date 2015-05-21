<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_rmsa.aspx.cs" Inherits="DD2015_45.Forms.rix.fm_rmsa" %>

<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.WebUI.WebDataInput.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics45.WebUI.WebHtmlEditor.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebHtmlEditor" tagprefix="ighedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
    .hide
    {
      visibility: hidden;
    }

    .tdtop
    {
      vertical-align: top;
    }

    .txRightWdg
    {
      text-align: right !important;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:UpdatePanel ID="UpdatePanelForm" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
      <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
      <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
      <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
      <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
      <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
      <input id="hh_fun_name" type="hidden" name="hh_fun_name" runat="server" />
      <input id="hh_fun_mkey" type="hidden" name="hh_fun_mkey" runat="server" />
      <input id="hh_fun_mkey_old" type="hidden" name="hh_fun_mkey_old" runat="server" />
      <input id="di_Window_Command" type="hidden" name="di_Window_Command" runat="server" />
      <input id="hh_rmsb_OldNum_Field" type="hidden" name="hh_rmsb_OldNum_Field" runat="server" />
      <input id="hh_rmsc_OldNum_Field" type="hidden" name="hh_rmsc_OldNum_Field" runat="server" />
      <asp:Button ID="btnAction" runat="server" UseSubmitBehavior="False" Visible="false" />
      <asp:Button ID="btnPost" runat="server" UseSubmitBehavior="False" Visible="false" />
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
              <igtxt:WebImageButton ID="bt_02" AccessKey="N" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="N新增" ImageDirectory="../../images/" OnClick="bt_02_Click">
                <Appearance>
                  <Image Url="form_new.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_03" AccessKey="I" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="I插入" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_new.png"></Image>
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
              <igtxt:WebImageButton ID="bt_05" AccessKey="X" runat="server" AutoSubmit="false" UseBrowserDefaults="False"
                Height="90%" Text="X刪除" ImageDirectory="../../images/" OnClick="bt_05_Click">
                <Appearance>
                  <Image Url="form_delete.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_06" AccessKey="O" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="O複製" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_copy.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_07" AccessKey="P" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="P列印" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_print.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_08" AccessKey="F" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="F查詢" ImageDirectory="../../images/" OnClick="bt_08_Click">
                <Appearance>
                  <Image Url="form_serch.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_09" AccessKey="T" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="T轉單" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_copy.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_10" AccessKey="E" runat="server" UseBrowserDefaults="False"
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
              <igtxt:WebImageButton ID="bt_QUT" AccessKey="Q" runat="server" UseBrowserDefaults="False"
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
      <ig:WebTab ID="WebTab_form" runat="server" Width="1200px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px" SelectedIndex="1">
        <Tabs>
          <ig:ContentTabItem runat="server" Key="QUERY" Text="查詢">
            <Template>
              <asp:Panel ID="PanSer" runat="server" Width="1000px">
                <table>
                  <tr>
                    <td>
                      <ig:WebTab ID="WebTab_SER" runat="server"  Width="1100px" StyleSetName="Pear" StyleSetPath="~/ig_res" TabItemSize="70px">
                        <Tabs>
                          <ig:ContentTabItem runat="server" Key="GEN" Text="一般">
                            <Template>
                              <asp:Panel ID="PanSerComm" runat="server" Width="1000px">
                                <table>
                                  <tr>
                                    <td>&nbsp;
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <table>
                                        <tr>
                                          <td>
                                            <asp:Label ID="lb_rmsa_RDDAT_s1" runat="server" Text="開始日期"></asp:Label>
                                          </td>
                                          <td>
                                            <ig:WebDatePicker ID="tx_rmsa_RDDAT_s1" Width="100px" CssClass="Office2010Blue" runat="server" Font-Size="Medium"></ig:WebDatePicker>
                                          </td>
                                          <td>
                                            <asp:Label ID="lb_rmsa_RDDAT_s2" runat="server" Text="～"></asp:Label>
                                          </td>
                                          <td>
                                            <ig:WebDatePicker ID="tx_rmsa_RDDAT_s2" Width="100px" CssClass="Office2010Blue" runat="server" Font-Size="Medium"></ig:WebDatePicker>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                              </asp:Panel>
                            </Template>
                          </ig:ContentTabItem>
                          <ig:ContentTabItem runat="server" Key="ADV" Text="進階">
                            <Template>
                              <asp:Panel ID="PanSerAdv" runat="server" Width="1000px">
                                <table>
                                  <tr>
                                    <td></td>
                                  </tr>
                                </table>
                              </asp:Panel>
                            </Template>
                          </ig:ContentTabItem>
                        </Tabs>
                      </ig:WebTab>
                  </tr>
                </table>
              </asp:Panel>
              <asp:Panel ID="PanGridA" runat="server" Width="1000px">
                <ig:WebDataGrid ID="WebDataGrid_rmsa" runat="server"
                  EnableAjax="false" EnableViewState="True" EnableDataViewState="True"
                  Width="1100px" AutoGenerateColumns="False" DataKeyFields="rmsa_gkey" OnRowSelectionChanged="WebDataGrid_rmsa_RowSelectionChanged">
                  <Columns>
                    <ig:TemplateDataField Key="rmsa_hidden" Hidden="true">
                      <ItemTemplate>
                        <input id="tx_rmsa_gkey02" type="hidden" name="tx_rmsa_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rmsa_gkey").ToString() %>' runat="server" />
                        <input id="tx_rmsa_mkey02" type="hidden" name="tx_rmsa_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rmsa_mkey").ToString() %>' runat="server" />
                      </ItemTemplate>
                      <Header Text="rmsa_hidden" />
                    </ig:TemplateDataField>
                    <ig:BoundDataField DataFieldName="rmsa_RDTIL" Key="rmsa_RDTIL" Width="100px">
                      <Header Text="rmsa_RDTIL">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="rmsa_gkey" Key="rmsa_gkey" Hidden="true" HtmlEncode="true">
                      <Header Text="rmsa_gkey">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="rmsa_mkey" Key="rmsa_mkey" Hidden="true" HtmlEncode="true">
                      <Header Text="rmsa_mkey">
                      </Header>
                    </ig:BoundDataField>
                  </Columns>
                  <Behaviors>
                    <ig:Selection CellClickAction="Row" CellSelectType="None" RowSelectType="Single">
                      <AutoPostBackFlags RowSelectionChanged="True" />
                    </ig:Selection>
                    <ig:RowSelectors>
                    </ig:RowSelectors>
                    <ig:Sorting SortingMode="Multi">
                    </ig:Sorting>
                    <ig:Paging PagerMode="NumericFirstLast">
                    </ig:Paging>
                  </Behaviors>
                </ig:WebDataGrid>
              </asp:Panel>
            </Template>
          </ig:ContentTabItem>
          <ig:ContentTabItem runat="server" Key="EDIT" Text="編輯">
            <Template>
              <table>
                <tr>
                  <td class="tdtop">
                    <asp:Panel ID="PanEdtLeft" runat="server" Width="250px" BorderStyle="Inset">
                      <ig:WebDataGrid ID="WebDataGrid_rmsa_ba" runat="server"
                        EnableAjax="False" EnableDataViewState="True"
                        Width="240px" AutoGenerateColumns="False" DataKeyFields="rmsa_gkey" OnRowSelectionChanged="WebDataGrid_rmsa_ba_RowSelectionChanged">
                        <Columns>
                          <ig:TemplateDataField Key="rmsa_hidden" Hidden="true">
                            <ItemTemplate>
                              <input id="tx_rmsa_gkey02" type="hidden" name="tx_rmsa_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rmsa_gkey").ToString() %>' runat="server" />
                              <input id="tx_rmsa_mkey02" type="hidden" name="tx_rmsa_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rmsa_mkey").ToString() %>' runat="server" />
                            </ItemTemplate>
                            <Header Text="rmsa_hidden" />
                          </ig:TemplateDataField>
                          <ig:BoundDataField DataFieldName="rmsa_RDDAT" Key="rmsa_RDDAT" Width="70px">
                            <Header Text="rmsa_RDDAT">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="rmsa_RDTIL" Key="rmsa_RDTIL" Width="100px">
                            <Header Text="rmsa_RDTIL">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="rmsa_gkey" Key="rmsa_gkey" Hidden="true" HtmlEncode="true">
                            <Header Text="rmsa_gkey">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="rmsa_mkey" Key="rmsa_mkey" Hidden="true" HtmlEncode="true">
                            <Header Text="rmsa_mkey">
                            </Header>
                          </ig:BoundDataField>
                        </Columns>
                        <Behaviors>
                          <ig:Selection CellClickAction="Row" CellSelectType="None" RowSelectType="Single">
                            <AutoPostBackFlags RowSelectionChanged="True" />
                          </ig:Selection>
                          <ig:RowSelectors>
                          </ig:RowSelectors>
                          <ig:Sorting>
                          </ig:Sorting>
                          <ig:Paging>
                          </ig:Paging>
                        </Behaviors>
                      </ig:WebDataGrid>
                    </asp:Panel>
                  </td>
                  <td>
                    <asp:Panel ID="PanEdtRightTop" runat="server" Width="800px" BorderStyle="Inset">
                      <table>
                        <tr>
                          <td>&nbsp;
                          </td>
                        </tr>
                        <tr>
                          <td colspan="1">
                            <asp:Label ID="lb_rmsa_RDREN" runat="server" Text="訊息編號"></asp:Label>
                            <asp:TextBox ID="tx_rmsa_RDREN" Width="180px" runat="server" MaxLength="20"></asp:TextBox>
                          </td>
                          <td>
                            <table>
                              <tr>
                                <td>
                                  <asp:Label ID="lb_rmsa_RDDAT" runat="server" Text="訊息日期"></asp:Label>
                                </td>
                                <td>
                                  <ig:WebDatePicker ID="tx_rmsa_RDDAT" Width="180px" CssClass="Office2010Blue" Font-Size="Medium" runat="server"></ig:WebDatePicker>
                                </td>
                              </tr>
                            </table>
                          </td>
                          <td>
                            <asp:CheckBox ID="ck_rmsa_RDALT" Width="100px" runat="server" Text="ALTER" />
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_rmsa_RDENO" runat="server" Text="員工編號"></asp:Label>
                            <asp:TextBox ID="tx_rmsa_RDENO" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_es101_RDENO" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                          </td>
                          <td>
                            <asp:Label ID="lb_rmsa_RDNUM" runat="server" Text="經銷編號"></asp:Label>
                            <asp:TextBox ID="tx_rmsa_RDNUM" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_bdlr_RDNUM" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                          </td>
                          <td>
                            <asp:Label ID="lb_rmsa_RDCUS" runat="server" Text="會員編號"></asp:Label>
                            <asp:TextBox ID="tx_rmsa_RDCUS" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_bcvw_RDCUS" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="4">
                            <asp:Label ID="lb_rmsa_RDTIL" runat="server" Text="訊息TITLE"></asp:Label>
                            <asp:TextBox ID="tx_rmsa_RDTIL" Width="640px" runat="server" MaxLength="1000"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_rmsa_RDKD1" runat="server" Text="訊息類１"></asp:Label>
                            <asp:DropDownList ID="dr_rmsa_RDKD1" Width="160px" runat="server" />
                            <asp:TextBox ID="tx_rmsa_RDKD1" Width="0px" Visible="false" runat="server" />
                          </td>
                          <td>
                            <asp:Label ID="lb_rmsa_RDKD2" runat="server" Text="訊息類２"></asp:Label>
                            <asp:DropDownList ID="dr_rmsa_RDKD2" Width="160px" runat="server" />
                            <asp:TextBox ID="tx_rmsa_RDKD2" Width="0px" Visible="false" runat="server" />
                          </td>
                          <td>
                            <asp:Label ID="lb_rmsa_RDKD3" runat="server" Text="訊息類３"></asp:Label>
                            <asp:DropDownList ID="dr_rmsa_RDKD3" Width="160px" runat="server" />
                            <asp:TextBox ID="tx_rmsa_RDKD3" Width="0px" Visible="false" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td colspan="2">
                            <asp:Label ID="lb_rmsa_RDPT1" runat="server" Text="商品編號"></asp:Label>
                            <asp:TextBox ID="tx_rmsa_RDPT1" Width="120px" runat="server" MaxLength="30"></asp:TextBox>
                            <asp:TextBox ID="tx_bpud_RDPT1" Width="280px" runat="server" MaxLength="30"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="4">
                            <asp:CheckBox ID="ck_rmsa_RDOKE" Width="60px" runat="server" Text="完成" />
                            <asp:Label ID="lb_rmsa_RDOKM" runat="server" Text="確認人員"></asp:Label>
                            <asp:TextBox ID="tx_rmsa_RDOKM" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_es101_RDOKM" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:Label ID="lb_rmsa_RDOKD" runat="server" Text="確認日期"></asp:Label>
                            <asp:TextBox ID="tx_rmsa_RDOKD" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                          </td>
                        </tr>
                      </table>
                    </asp:Panel>
                    <table>
                      <tr>
                        <td>
                          <igtxt:WebImageButton ID="bt_New_B" AccessKey="A" Text="A新增" ImageDirectory="../../images/" AutoSubmit="false" runat="server">
                            <FocusAppearance>
                              <Image Url="add_up.gif"></Image>
                            </FocusAppearance>
                            <PressedAppearance ContentShift="DownRight">
                              <Image Url="add_down.gif"></Image>
                            </PressedAppearance>
                            <HoverAppearance ContentShift="UpLeft">
                              <Image Url="add_hover.gif"></Image>
                            </HoverAppearance>
                            <DisabledAppearance>
                              <Image Url="add_disabled.gif"></Image>
                            </DisabledAppearance>
                            <Appearance>
                              <Image Url="add_down.gif"></Image>
                            </Appearance>
                          </igtxt:WebImageButton>
                        </td>
                        <td>
                          <igtxt:WebImageButton ID="bt_MOD_B" Text="更正" ImageDirectory="../../images/" runat="server" OnClick="bt_MOD_B_Click">
                            <FocusAppearance>
                              <Image Url="edit_up.gif"></Image>
                            </FocusAppearance>
                            <PressedAppearance ContentShift="DownRight">
                              <Image Url="edit_down.gif"></Image>
                            </PressedAppearance>
                            <HoverAppearance ContentShift="UpLeft">
                              <Image Url="edit_hover.gif"></Image>
                            </HoverAppearance>
                            <DisabledAppearance>
                              <Image Url="edit_disabled.gif"></Image>
                            </DisabledAppearance>
                            <Appearance>
                              <Image Url="edit_down.gif"></Image>
                            </Appearance>
                          </igtxt:WebImageButton>
                        </td>
                        <td>
                          <igtxt:WebImageButton ID="bt_SAVE_B" Text="存檔" ImageDirectory="../../images/" runat="server" OnClick="bt_SAVE_B_Click">
                            <FocusAppearance>
                              <Image Url="save_up.gif"></Image>
                            </FocusAppearance>
                            <PressedAppearance ContentShift="DownRight">
                              <Image Url="save_down.gif"></Image>
                            </PressedAppearance>
                            <HoverAppearance ContentShift="UpLeft">
                              <Image Url="save_hover.gif"></Image>
                            </HoverAppearance>
                            <DisabledAppearance>
                              <Image Url="save_disabled.gif"></Image>
                            </DisabledAppearance>
                            <Appearance>
                              <Image Url="save_down.gif"></Image>
                            </Appearance>
                          </igtxt:WebImageButton>
                        </td>
                      </tr>
                    </table>
                    <asp:Panel ID="PanEdtRightDown" runat="server" Width="910px" BorderStyle="Inset">
                      <ig:WebTab ID="WebTabGrid" runat="server"  Width="900px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px" SelectedIndex="0" OnSelectedIndexChanged="WebTabGrid_SelectedIndexChanged">
                        <AutoPostBackFlags SelectedIndexChanged="On" />
                        <Tabs>
                          <ig:ContentTabItem runat="server" Text="訊息明細" Key="UNrmsb"  VisibleIndex="0">
                            <Template>
                              <ig:WebDataGrid ID="WebDataGrid_rmsb" runat="server"
                                Width="890px" AutoGenerateColumns="False" DataKeyFields="rmsb_gkey" OnRowAdded="WebDataGrid_rmsb_RowAdded" OnRowAdding="WebDataGrid_rmsb_RowAdding" OnRowUpdated="WebDataGrid_rmsb_RowUpdated" OnRowUpdating="WebDataGrid_rmsb_RowUpdating" Height="360px" OnRowDeleted="WebDataGrid_rmsb_RowDeleted" OnRowsDeleting="WebDataGrid_rmsb_RowsDeleting" OnCellSelectionChanged="WebDataGrid_rmsb_CellSelectionChanged">
                                <Columns>
                                  <ig:TemplateDataField Key="rmsb_hidden" Hidden="true">
                                    <ItemTemplate>
                                      <input id="tx_rmsb_gkey02" type="hidden" name="tx_rmsb_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rmsb_gkey").ToString() %>' runat="server" />
                                      <input id="tx_rmsb_mkey02" type="hidden" name="tx_rmsb_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rmsb_mkey").ToString() %>' runat="server" />
                                    </ItemTemplate>
                                    <Header Text="rmsb_hidden" />
                                  </ig:TemplateDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_RDREN" Key="rmsb_RDREN" Hidden="true" HtmlEncode="true">
                                    <Header Text="rmsb_RDREN">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_RDITM" Key="rmsb_RDITM" Hidden="true" HtmlEncode="true">
                                    <Header Text="rmsb_RDITM">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_RDDAT" Key="rmsb_RDDAT" Width="70px">
                                    <Header Text="rmsb_RDDAT">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_RDTXT" Key="rmsb_RDTXT" HtmlEncode="true" Width="200px">
                                    <Header Text="rmsb_RDTXT">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_RDENO" Key="rmsb_RDENO" Width="60px">
                                    <Header Text="rmsb_RDENO">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="es101_RDENO" Key="es101_RDENO" Width="60px">
                                    <Header Text="es101_RDENO">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_RDCUS" Key="rmsb_RDCUS" Width="80px">
                                    <Header Text="rmsb_RDCUS">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="bcvw_RDCUS" Key="bcvw_RDCUS" Width="80px">
                                    <Header Text="bcvw_RDCUS">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_RDNUM" Key="rmsb_RDNUM" Width="80px">
                                    <Header Text="rmsb_RDNUM">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="bdlr_RDNUM" Key="bdlr_RDNUM" Width="80px">
                                    <Header Text="bdlr_RDNUM">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_gkey" Key="rmsb_gkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="rmsb_gkey">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsb_mkey" Key="rmsb_mkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="rmsb_mkey">
                                    </Header>
                                  </ig:BoundDataField>
                                </Columns>
                                <Behaviors>
                                  <ig:Selection RowSelectType="Single">
                                    <AutoPostBackFlags CellSelectionChanged="True" />
                                  </ig:Selection>
                                  <ig:RowSelectors>
                                  </ig:RowSelectors>
                                  <ig:Sorting SortingMode="Multi">
                                  </ig:Sorting>
                                  <ig:EditingCore BatchUpdating="True">
                                    <EditingClientEvents RowDeleted="rmsb_RowDeleted" RowAdded="rmsb_RowAdded" />
                                    <Behaviors>
                                      <ig:RowDeleting ShowDeleteButton="True">
                                      </ig:RowDeleting>
                                      <ig:RowAdding>
                                        <ColumnSettings>
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_RDREN" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_RDITM" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_RDDAT" EditorID="WebDataGrid_rmsb_RDDAT" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_RDTXT" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_RDENO" />
                                          <ig:RowAddingColumnSetting ColumnKey="es101_RDENO" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_RDCUS" />
                                          <ig:RowAddingColumnSetting ColumnKey="bcvw_RDCUS" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_RDNUM" />
                                          <ig:RowAddingColumnSetting ColumnKey="bdlr_RDNUM" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_gkey" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsb_mkey" ReadOnly="True" />
                                        </ColumnSettings>
                                        <AddNewRowClientEvents EnteringEditMode="rmsb_AddNewRow_EnteringEditMode" ExitedEditMode="rmsb_AddNewRow_ExitedEditMode" ExitingEditMode="rmsb_AddNewRow_ExitingEditMode" />
                                        <EditModeActions MouseClick="Single" EnableOnActive="True" EnableOnKeyPress="True" />
                                      </ig:RowAdding>
                                      <ig:CellEditing>
                                        <ColumnSettings>
                                          <ig:EditingColumnSetting ColumnKey="rmsb_RDREN" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsb_RDITM" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsb_RDDAT" EditorID="WebDataGrid_rmsb_RDDAT" />
                                          <ig:EditingColumnSetting ColumnKey="rmsb_RDTXT" />
                                          <ig:EditingColumnSetting ColumnKey="rmsb_RDENO" />
                                          <ig:EditingColumnSetting ColumnKey="es101_RDENO" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsb_RDCUS" />
                                          <ig:EditingColumnSetting ColumnKey="bcvw_RDCUS" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsb_RDNUM" />
                                          <ig:EditingColumnSetting ColumnKey="bdlr_RDNUM" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsb_gkey" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsb_mkey" ReadOnly="True" />
                                        </ColumnSettings>
                                        <CellEditingClientEvents ExitedEditMode="rmsb_CellEdit_ExitedEditMode" EnteredEditMode="rmsb_CellEdit_EnteredEditMode" />
                                        <EditModeActions EnableOnActive="True" EnableOnKeyPress="True" />
                                      </ig:CellEditing>
                                    </Behaviors>
                                  </ig:EditingCore>
                                  <ig:Activation>
                                    <ActivationClientEvents ActiveCellChanging="rmsb_Activation_ActiveCellChanging" />
                                  </ig:Activation>
                                  <ig:Paging PageSize="10" PagerAppearance="Both">
                                  </ig:Paging>
                                </Behaviors>
                                <EditorProviders>
                                  <ig:DateTimeEditorProvider ID="WebDataGrid_rmsb_RDDAT">
                                    <EditorControl ClientIDMode="Predictable">
                                    </EditorControl>
                                  </ig:DateTimeEditorProvider>
                                </EditorProviders>
                                <ClientEvents AJAXResponse="WebDataGridView_AJAXResponse" />
                              </ig:WebDataGrid>
                            </Template>
                          </ig:ContentTabItem>
                          <ig:ContentTabItem runat="server" Text="處理人員"  Key="UNrmsc" VisibleIndex="1">
                            <Template>
                              <ig:WebDataGrid ID="WebDataGrid_rmsc" runat="server"
                                Width="890px" AutoGenerateColumns="False" DataKeyFields="rmsc_gkey" OnRowAdded="WebDataGrid_rmsc_RowAdded" OnRowAdding="WebDataGrid_rmsc_RowAdding" OnRowUpdated="WebDataGrid_rmsc_RowUpdated" OnRowUpdating="WebDataGrid_rmsc_RowUpdating" Height="360px" OnRowDeleted="WebDataGrid_rmsc_RowDeleted" OnRowsDeleting="WebDataGrid_rmsc_RowsDeleting">
                                <Columns>
                                  <ig:TemplateDataField Key="rmsc_hidden" Hidden="true">
                                    <ItemTemplate>
                                      <input id="tx_rmsc_gkey02" type="hidden" name="tx_rmsc_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rmsc_gkey").ToString() %>' runat="server" />
                                      <input id="tx_rmsc_mkey02" type="hidden" name="tx_rmsc_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rmsc_mkey").ToString() %>' runat="server" />
                                    </ItemTemplate>
                                    <Header Text="rmsc_hidden" />
                                  </ig:TemplateDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_RDREN" Key="rmsc_RDREN" Hidden="true" HtmlEncode="true">
                                    <Header Text="rmsc_RDREN">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_RDITM" Key="rmsc_RDITM" Hidden="true" HtmlEncode="true">
                                    <Header Text="rmsc_RDITM">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_rmsb_gkey" Key="rmsc_rmsb_gkey" Hidden="true">
                                    <Header Text="rmsc_rmsb_gkey">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_RDDAT" Key="rmsc_RDDAT" Hidden="true">
                                    <Header Text="rmsc_RDDAT">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_RDENO" Key="rmsc_RDENO" Width="60px">
                                    <Header Text="rmsc_RDENO">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="es101_RDENO" Key="es101_RDENO" Width="60px">
                                    <Header Text="es101_RDENO">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_RDCUS" Key="rmsc_RDCUS" Width="80px">
                                    <Header Text="rmsc_RDCUS">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="bcvw_RDCUS" Key="bcvw_RDCUS" Width="80px">
                                    <Header Text="bcvw_RDCUS">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_RDNUM" Key="rmsc_RDNUM" Width="80px">
                                    <Header Text="rmsc_RDNUM">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="bdlr_RDNUM" Key="bdlr_RDNUM" Width="80px">
                                    <Header Text="bdlr_RDNUM">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_gkey" Key="rmsc_gkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="rmsc_gkey">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rmsc_mkey" Key="rmsc_mkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="rmsc_mkey">
                                    </Header>
                                  </ig:BoundDataField>
                                </Columns>
                                <Behaviors>
                                  <ig:Selection RowSelectType="Single">
                                  </ig:Selection>
                                  <ig:RowSelectors>
                                  </ig:RowSelectors>
                                  <ig:Sorting SortingMode="Multi">
                                  </ig:Sorting>
                                  <ig:EditingCore BatchUpdating="True">
                                    <EditingClientEvents RowDeleted="rmsc_RowDeleted" RowAdded="rmsc_RowAdded" />
                                    <Behaviors>
                                      <ig:RowDeleting ShowDeleteButton="True">
                                      </ig:RowDeleting>
                                      <ig:RowAdding>
                                        <ColumnSettings>
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_RDREN" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_RDITM" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_rmsb_gkey" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_RDDAT" ReadOnly="True" EditorID="WebDataGrid_rmsc_RDDAT" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_RDENO" />
                                          <ig:RowAddingColumnSetting ColumnKey="es101_RDENO" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_RDCUS" />
                                          <ig:RowAddingColumnSetting ColumnKey="bcvw_RDCUS" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_RDNUM" />
                                          <ig:RowAddingColumnSetting ColumnKey="bdlr_RDNUM" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_gkey" ReadOnly="True" />
                                          <ig:RowAddingColumnSetting ColumnKey="rmsc_mkey" ReadOnly="True" />
                                        </ColumnSettings>
                                        <AddNewRowClientEvents EnteringEditMode="rmsc_AddNewRow_EnteringEditMode" ExitedEditMode="rmsc_AddNewRow_ExitedEditMode" ExitingEditMode="rmsc_AddNewRow_ExitingEditMode" />
                                        <EditModeActions MouseClick="Single" EnableOnActive="True" EnableOnKeyPress="True" />
                                      </ig:RowAdding>
                                      <ig:CellEditing>
                                        <ColumnSettings>
                                          <ig:EditingColumnSetting ColumnKey="rmsc_RDREN" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsc_RDITM" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsc_rmsb_gkey" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsc_RDDAT" ReadOnly="True" EditorID="WebDataGrid_rmsc_RDDAT" />
                                          <ig:EditingColumnSetting ColumnKey="rmsc_RDENO" />
                                          <ig:EditingColumnSetting ColumnKey="es101_RDENO" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsc_RDCUS" />
                                          <ig:EditingColumnSetting ColumnKey="bcvw_RDCUS" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsc_RDNUM" />
                                          <ig:EditingColumnSetting ColumnKey="bdlr_RDNUM" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsc_gkey" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rmsc_mkey" ReadOnly="True" />
                                        </ColumnSettings>
                                        <CellEditingClientEvents ExitedEditMode="rmsc_CellEdit_ExitedEditMode" EnteredEditMode="rmsc_CellEdit_EnteredEditMode" />
                                        <EditModeActions EnableOnActive="True" EnableOnKeyPress="True" />
                                      </ig:CellEditing>
                                    </Behaviors>
                                  </ig:EditingCore>
                                  <ig:Activation>
                                    <ActivationClientEvents ActiveCellChanging="rmsc_Activation_ActiveCellChanging" />
                                  </ig:Activation>
                                  <ig:Paging PageSize="10" PagerAppearance="Both">
                                  </ig:Paging>
                                </Behaviors>
                                <EditorProviders>
                                  <ig:DateTimeEditorProvider ID="WebDataGrid_rmsc_RDDAT">
                                    <EditorControl ClientIDMode="Predictable">
                                    </EditorControl>
                                  </ig:DateTimeEditorProvider>
                                </EditorProviders>
                                <ClientEvents AJAXResponse="WebDataGridView_AJAXResponse" />
                              </ig:WebDataGrid>
                            </Template>
                          </ig:ContentTabItem>
                        </Tabs>
                      </ig:WebTab>
                    </asp:Panel>
                  </td>
                </tr>
              </table>
            </Template>
          </ig:ContentTabItem>
        </Tabs>
      </ig:WebTab>
      <ig:WebDialogWindow ID="di_Window" runat="server" Width="600px"
        Height="500px" InitialLocation="Centered" Modal="True" Moveable="False" Resizer-Enabled="False">
        <ContentPane BackColor="#FAFAFA" ContentUrl="#">
        </ContentPane>
        <Header CaptionText="my dialog" BorderColor="#cccccc">
        </Header>
        <ClientEvents Initialize="di_Window_initialize" WindowStateChanged="di_Window_windowStateChanged" />
      </ig:WebDialogWindow>
      <asp:ObjectDataSource ID="Obj_rmsa" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_rmsa" TypeName="DD2015_45.DAC_rmsa" OnSelecting="Obj_rmsa_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_rmsa_ba" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_rmsa_ba" TypeName="DD2015_45.DAC_rmsa" OnSelecting="Obj_rmsa_ba_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_rmsb" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_rmsb" TypeName="DD2015_45.DAC_rmsb" OnSelecting="Obj_rmsb_Selecting" UpdateMethod="UpdateTable_rmsb" InsertMethod="InsertTable_rmsb" OnInserting="Obj_rmsb_Inserting" DeleteMethod="DeleteTable_rmsb" OnDeleting="Obj_rmsb_Deleting" OnUpdating="Obj_rmsb_Updating" OnUpdated="Obj_rmsb_Updated" OnDeleted="Obj_rmsb_Deleted" OnInserted="Obj_rmsb_Inserted" OnSelected="Obj_rmsb_Selected">
        <DeleteParameters>
          <asp:Parameter Name="original_rmsb_gkey" Type="String" />
          <asp:Parameter Name="rmsb_gkey" Type="String" />
          <asp:Parameter Name="rmsb_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </DeleteParameters>
        <InsertParameters>
          <asp:Parameter Name="rmsb_RDREN" Type="String" />
          <asp:Parameter Name="rmsb_RDITM" Type="Int32" />
          <asp:Parameter Name="rmsb_RDDAT" Type="String" />
          <asp:Parameter Name="rmsb_RDENO" Type="String" />
          <asp:Parameter Name="rmsb_RDNUM" Type="String" />
          <asp:Parameter Name="rmsb_RDCUS" Type="String" />
          <asp:Parameter Name="rmsb_RDTXT" Type="String" />
          <asp:Parameter Name="rmsb_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </InsertParameters>
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter Name="st_addSelect" Type="String" />
          <asp:Parameter Name="bl_lock" Type="Boolean" />
          <asp:Parameter Name="st_addJoin" Type="String" />
          <asp:Parameter Name="st_addUnion" Type="String" />
          <asp:Parameter Name="st_orderKey" Type="String" />
        </SelectParameters>
        <UpdateParameters>
          <asp:Parameter Name="original_rmsb_gkey" Type="String" />
          <asp:Parameter Name="rmsb_gkey" Type="String" />
          <asp:Parameter Name="rmsb_mkey" Type="String" />
          <asp:Parameter Name="rmsb_RDREN" Type="String" />
          <asp:Parameter Name="rmsb_RDITM" Type="Int32" />
          <asp:Parameter Name="rmsb_RDDAT" Type="String" />
          <asp:Parameter Name="rmsb_RDENO" Type="String" />
          <asp:Parameter Name="rmsb_RDNUM" Type="String" />
          <asp:Parameter Name="rmsb_RDCUS" Type="String" />
          <asp:Parameter Name="rmsb_RDTXT" Type="String" />
          <asp:Parameter Name="rmsb_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </UpdateParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_rmsc" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_rmsc" TypeName="DD2015_45.DAC_rmsc" OnSelecting="Obj_rmsc_Selecting" UpdateMethod="UpdateTable_rmsc" InsertMethod="InsertTable_rmsc" OnInserting="Obj_rmsc_Inserting" DeleteMethod="DeleteTable_rmsc" OnDeleting="Obj_rmsc_Deleting" OnUpdating="Obj_rmsc_Updating" OnUpdated="Obj_rmsc_Updated" OnDeleted="Obj_rmsc_Deleted" OnInserted="Obj_rmsc_Inserted" OnSelected="Obj_rmsc_Selected">
        <DeleteParameters>
          <asp:Parameter Name="original_rmsc_gkey" Type="String" />
          <asp:Parameter Name="rmsc_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </DeleteParameters>
        <InsertParameters>
          <asp:Parameter Name="rmsc_RDREN" Type="String" />
          <asp:Parameter Name="rmsc_RDITM" Type="Int32" />
          <asp:Parameter Name="rmsc_rmsb_gkey" Type="String" />
          <asp:Parameter Name="rmsc_RDENO" Type="String" />
          <asp:Parameter Name="rmsc_RDNUM" Type="String" />
          <asp:Parameter Name="rmsc_RDCUS" Type="String" />
          <asp:Parameter Name="rmsc_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </InsertParameters>
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter Name="st_addSelect" Type="String" />
          <asp:Parameter Name="bl_lock" Type="Boolean" />
          <asp:Parameter Name="st_addJoin" Type="String" />
          <asp:Parameter Name="st_addUnion" Type="String" />
          <asp:Parameter Name="st_orderKey" Type="String" />
        </SelectParameters>
        <UpdateParameters>
          <asp:Parameter Name="original_rmsc_gkey" Type="String" />
          <asp:Parameter Name="rmsc_gkey" Type="String" />
          <asp:Parameter Name="rmsc_mkey" Type="String" />
          <asp:Parameter Name="rmsc_RDREN" Type="String" />
          <asp:Parameter Name="rmsc_RDITM" Type="Int32" />
          <asp:Parameter Name="rmsc_RDDAT" Type="String" />
          <asp:Parameter Name="rmsc_RDENO" Type="String" />
          <asp:Parameter Name="rmsc_RDNUM" Type="String" />
          <asp:Parameter Name="rmsc_RDCUS" Type="String" />
          <asp:Parameter Name="rmsc_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </UpdateParameters>
      </asp:ObjectDataSource>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
      <script type="text/javascript">
        var hh_company = document.all['ctl00$hh_Company'].value;
        var hh_rmsb_OldNum_Field = "<%= this.hh_rmsb_OldNum_Field.ClientID %>";

        var hh_rmsc_OldNum_Field = "<%= this.hh_rmsc_OldNum_Field.ClientID %>";

        function di_Window_initialize(sender, e) {
          document.all[hh_rmsb_OldNum_Field].value = empty_field;
          sender.hide();
        }

        function di_Window_windowStateChanged(diWin, evntArgs) {
          var diWin = $find('<%= di_Window.ClientID %>');
          var state = diWin.get_windowState(); //1=Minimized 2=Maximized 3=Closed other= Restored
          var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
          var diWin_cmd = document.all[diWin_cmdField].value;
          if (state == 3) {
          }
        }
        ///
        //rmsb_DeleteRow
        function rmsb_RowDeleted() {

        }

        //rmsb_AddRowfocus
        function rmsb_RowAddFocus(st_focus_field) {

          return false;
        }

        //
        //rmsb_AddRow
        function rmsb_RowAdded(webDataGrid, evntArgs) {
          //連續輸入
          var behaviors = webDataGrid.get_behaviors();
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          // 
          var focus_cell = newRow.get_cellByColumnKey("rmsb_RDTXT");
          webDataGrid.get_behaviors().get_activation().set_activeCell(focus_cell);
        }

        function rmsb_AddNewRow_Initialize() {
          //form load的時侯即會顯示,與rmsb edit無關
          //alert('rmsb_AddNewRow_Initialize');
        }

        function rmsb_AddNewRow_EnteringEditMode(webDataGrid, evntArgs) {
          //設定初值
          //var in_row = webDataGrid.get_behaviors().get_editingCore().get_behaviors().get_rowAdding().get_row();
          //alert('rmsb_AddNewRow_EnteringEditMode');
          var behaviors = webDataGrid.get_behaviors();
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          var activation = behaviors.get_activation();
          var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
          var columnKey = activeCell.get_column().get_key();
          //
          var new_rmsb_RDDAT = new Date().format('yyyy/MM/dd');
          var rmsb_RDTXT = newRow.get_cellByColumnKey("rmsb_RDTXT").get_value();
          if ((rmsb_RDTXT == null) || ((columnKey == "rmsb_RDTXT") && (rmsb_RDTXT == ""))) {
            newRow.get_cellByColumnKey("rmsb_RDDAT").set_value(new_rmsb_RDDAT);
            newRow.get_cellByColumnKey("rmsb_RDENO").set_value("");
            newRow.get_cellByColumnKey("es101_RDENO").set_value("");
            newRow.get_cellByColumnKey("rmsb_RDCUS").set_value("");
            newRow.get_cellByColumnKey("bcvw_RDCUS").set_value("");
            newRow.get_cellByColumnKey("rmsb_RDNUM").set_value("");
            newRow.get_cellByColumnKey("bdlr_RDNUM").set_value("");
          }
        }

        function rmsb_AddNewRow_ExitingEditMode(webDataGrid, evntArgs) {
          var behaviors = webDataGrid.get_behaviors();
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          var activation = behaviors.get_activation();
          var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
          var columnKey = activeCell.get_column().get_key();
          if (columnKey == "rmsb_RDNUM") {
            if (evntArgs.get_browserEvent().keyCode == 13) {
              evntArgs.set_cancel(true);
              alert("輸入完成後,請按Tab鍵.");
            }
          }
        }

        function rmsb_AddNewRow_ExitedEditMode(webDataGrid, evntArgs) {
          var behaviors = webDataGrid.get_behaviors();
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          var activation = behaviors.get_activation();
          var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
          var columnKey = activeCell.get_column().get_key();
          //
          var rmsb_RDNUM = newRow.get_cellByColumnKey("rmsb_RDNUM").get_value();
          if ((columnKey == "rmsb_RDNUM") && (rmsb_RDNUM != "") && (rmsb_RDNUM != null)) {
            //init
            newRow.get_cellByColumnKey("bdlr_RDNUM").set_value("");
            //
            var rmsb_OldNum = document.all[hh_rmsb_OldNum_Field].value;
            var rmsb_RDNUM = newRow.get_cellByColumnKey("rmsb_RDNUM").get_value();
            var iContEdit = '<%=st_ContentPlaceHolderEdit%>';
            var di_Window = '<%=di_Window.ClientID%>';
            var DataGrid_id = '<%=WebDataGrid_rmsb.ClientID%>';
            var di_OpenUrl = "../Dialog/Dialog_bdlr.aspx?iFunc=" + "rmsb" + "&oNewMod=add" + "&oWindow_Id=" + di_Window + "&oDataGrid_id=" + DataGrid_id + "&iField=rmsb_RDNUM" + "&oField=bdlr_RDNUM"
            var di_Caption = activeCell.get_column().get_headerText();
            //
            var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
            document.all[diWin_cmdField].value = "add_rmsb_RDNUM";
            get_bdlr_grname('rmsb', iContEdit, "add", "rmsb_RDNUM", "bdlr_RDNUM", "*", DataGrid_id, newRow, di_Window, di_OpenUrl, di_Caption, rmsb_RDNUM, rmsb_OldNum);

          }
        }


        function rmsb_CellEdit_EnteredEditMode(sender, e) {
          var cell = e.getCell();
          var value = cell.get_value();
          var row = cell.get_row();
          var rowIndex = row.get_index();
          var column = cell.get_column();
          var columnKey = column.get_key();
          var rmsb_RDNUM = row.get_cellByColumnKey("rmsb_RDNUM").get_value();
          if (columnKey == "rmsb_RDNUM") {
            //save old rbptn
            document.all[hh_rmsb_OldNum_Field].value = rmsb_RDNUM;
          }
          else {
            document.all[hh_rmsb_OldNum_Field].value = empty_field;
          }
        }

        function rmsb_CellEdit_ExitedEditMode(sender, e) {
          var cell = e.getCell();
          var value = cell.get_value();
          var row = cell.get_row();
          var rowIndex = row.get_index();
          var column = cell.get_column();
          var columnKey = column.get_key();
          //
          if (columnKey == "rmsb_RDNUM") {
            var rmsb_OldNum = document.all[hh_rmsb_OldNum_Field].value;
            var rmsb_RDNUM = row.get_cellByColumnKey("rmsb_RDNUM").get_value();
            if (rmsb_RDNUM != rmsb_OldNum) {
              var iContEdit = '<%=st_ContentPlaceHolderEdit%>';
              var di_Window = '<%=di_Window.ClientID%>';
              var DataGrid_id = '<%=WebDataGrid_rmsb.ClientID%>';
              var di_OpenUrl = "../Dialog/Dialog_bdlr.aspx?iFunc=" + "rmsb" + "&oNewMod=mod" + "&oWindow_Id=" + di_Window + "&oDataGrid_id=" + DataGrid_id + "&iField=rmsb_RDNUM" + "&oField=bdlr_RDNUM"
              var di_Caption = cell.get_column().get_headerText();
              var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
              document.all[diWin_cmdField].value = "mod_rmsb_RDNUM";
              get_bdlr_grname('rmsb', iContEdit, "mod", "rmsb_RDNUM", "bdlr_RDNUM", "*", DataGrid_id, row, di_Window, di_OpenUrl, di_Caption, rmsb_RDNUM, rmsb_OldNum);
            }
          }
          //
        }


        function rmsb_Activation_ActiveCellChanging(webDataGrid, e) {
          var column = e.getNewActiveCell().get_column();
          if (column != null) {
            var columnKey = column.get_key();
            if (columnKey == "rmsb_RDNUM") {
              //var index = e.getNewActiveCell().get_row().get_index();
              //e.set_cancel(true);
              //if (index == 1) {
              //  // edit cell
              //  var cell = webDataGrid.get_rows().get_row(index).get_cellByColumnKey("rmsb_BDRMK");
              //  webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
              //}
              //else {
              //  // add cell
              //  var row = e.getNewActiveCell().get_row();
              //  var cell = row.get_cellByColumnKey("rmsb_BDRMK");
              //  webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
              //}
            }
          }
        }
        ///
        //rmsc_DeleteRow
        function rmsc_RowDeleted() {

        }

        //rmsc_AddRowfocus
        function rmsc_RowAddFocus(st_focus_field) {

          return false;
        }

        //
        //rmsc_AddRow
        function rmsc_RowAdded(webDataGrid, evntArgs) {
          //連續輸入
          var behaviors = webDataGrid.get_behaviors();
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          // 
          var focus_cell = newRow.get_cellByColumnKey("rmsc_RDENO");
          webDataGrid.get_behaviors().get_activation().set_activeCell(focus_cell);
        }

        function rmsc_AddNewRow_Initialize() {
          //form load的時侯即會顯示,與rmsc edit無關
          //alert('rmsc_AddNewRow_Initialize');
        }

        function rmsc_AddNewRow_EnteringEditMode(webDataGrid, evntArgs) {
          //設定初值
          //var in_row = webDataGrid.get_behaviors().get_editingCore().get_behaviors().get_rowAdding().get_row();
          //alert('rmsc_AddNewRow_EnteringEditMode');
          var behaviors = webDataGrid.get_behaviors();
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          var activation = behaviors.get_activation();
          var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
          var columnKey = activeCell.get_column().get_key();
          //
          var new_rmsc_RDDAT = new Date().format('yyyy/MM/dd');
          var rmsc_RDDAT = newRow.get_cellByColumnKey("rmsc_RDDAT").get_value();
          if ((rmsc_RDDAT == null) || ((columnKey == "rmsc_RDDAT") && (rmsc_RDDAT == ""))) {
            newRow.get_cellByColumnKey("rmsc_RDDAT").set_value(new_rmsc_RDDAT);
            newRow.get_cellByColumnKey("rmsc_RDENO").set_value("");
            newRow.get_cellByColumnKey("es101_RDENO").set_value("");
            newRow.get_cellByColumnKey("rmsc_RDCUS").set_value("");
            newRow.get_cellByColumnKey("bcvw_RDCUS").set_value("");
            newRow.get_cellByColumnKey("rmsc_RDNUM").set_value("");
            newRow.get_cellByColumnKey("bdlr_RDNUM").set_value("");
          }
        }

        function rmsc_AddNewRow_ExitingEditMode(webDataGrid, evntArgs) {
          var behaviors = webDataGrid.get_behaviors();
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          var activation = behaviors.get_activation();
          var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
          var columnKey = activeCell.get_column().get_key();
          if (columnKey == "rmsc_RDENO") {
            if (evntArgs.get_browserEvent().keyCode == 13) {
              evntArgs.set_cancel(true);
              alert("輸入完成後,請按Tab鍵.");
            }
          }
        }

        function rmsc_AddNewRow_ExitedEditMode(webDataGrid, evntArgs) {
          var behaviors = webDataGrid.get_behaviors();
          var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
          var newRow = newRowBehavior.get_row();
          var activation = behaviors.get_activation();
          var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
          var columnKey = activeCell.get_column().get_key();
          //
          var rmsc_RDNUM = newRow.get_cellByColumnKey("rmsc_RDNUM").get_value();
          if ((columnKey == "rmsc_RDNUM") && (rmsc_RDNUM != "") && (rmsc_RDNUM != null)) {
            //init
            newRow.get_cellByColumnKey("bdlr_RDNUM").set_value("");
            //
            var rmsc_OldNum = document.all[hh_rmsc_OldNum_Field].value;
            var rmsc_RDNUM = newRow.get_cellByColumnKey("rmsc_RDNUM").get_value();
            var iContEdit = '<%=st_ContentPlaceHolderEdit%>';
            var di_Window = '<%=di_Window.ClientID%>';
            var DataGrid_id = '<%=WebDataGrid_rmsc.ClientID%>';
            var di_OpenUrl = "../Dialog/Dialog_bdlr.aspx?iFunc=" + "rmsc" + "&oNewMod=add" + "&oWindow_Id=" + di_Window + "&oDataGrid_id=" + DataGrid_id + "&iField=rmsc_RDNUM" + "&oField=bdlr_RDNUM"
            var di_Caption = activeCell.get_column().get_headerText();
            //
            var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
            document.all[diWin_cmdField].value = "add_rmsc_RDNUM";
            get_bdlr_grname('rmsc', iContEdit, "add", "rmsc_RDNUM", "bdlr_RDNUM", "*", DataGrid_id, newRow, di_Window, di_OpenUrl, di_Caption, rmsc_RDNUM, rmsc_OldNum);

          }
        }


        function rmsc_CellEdit_EnteredEditMode(sender, e) {
          var cell = e.getCell();
          var value = cell.get_value();
          var row = cell.get_row();
          var rowIndex = row.get_index();
          var column = cell.get_column();
          var columnKey = column.get_key();
          var rmsc_RDNUM = row.get_cellByColumnKey("rmsc_RDNUM").get_value();
          if (columnKey == "rmsc_RDNUM") {
            //save old rbptn
            document.all[hh_rmsc_OldNum_Field].value = rmsc_RDNUM;
          }
          else {
            document.all[hh_rmsc_OldNum_Field].value = empty_field;
          }
        }

        function rmsc_CellEdit_ExitedEditMode(sender, e) {
          var cell = e.getCell();
          var value = cell.get_value();
          var row = cell.get_row();
          var rowIndex = row.get_index();
          var column = cell.get_column();
          var columnKey = column.get_key();
          //
          if (columnKey == "rmsc_RDNUM") {
            var rmsc_OldNum = document.all[hh_rmsc_OldNum_Field].value;
            var rmsc_RDNUM = row.get_cellByColumnKey("rmsc_RDNUM").get_value();
            if (rmsc_RDNUM != rmsc_OldNum) {
              var iContEdit = '<%=st_ContentPlaceHolderEdit%>';
              var di_Window = '<%=di_Window.ClientID%>';
              var DataGrid_id = '<%=WebDataGrid_rmsc.ClientID%>';
              var di_OpenUrl = "../Dialog/Dialog_bdlr.aspx?iFunc=" + "rmsc" + "&oNewMod=mod" + "&oWindow_Id=" + di_Window + "&oDataGrid_id=" + DataGrid_id + "&iField=rmsc_RDNUM" + "&oField=bdlr_RDNUM"
              var di_Caption = cell.get_column().get_headerText();
              var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
              document.all[diWin_cmdField].value = "mod_rmsc_RDNUM";
              get_bdlr_grname('rmsc', iContEdit, "mod", "rmsc_RDNUM", "bdlr_RDNUM", "*", DataGrid_id, row, di_Window, di_OpenUrl, di_Caption, rmsc_RDNUM, rmsc_OldNum);
            }
          }
          //
        }


        function rmsc_Activation_ActiveCellChanging(webDataGrid, e) {
          var column = e.getNewActiveCell().get_column();
          if (column != null) {
            var columnKey = column.get_key();
            if (columnKey == "rmsc_RDNUM") {
              //var index = e.getNewActiveCell().get_row().get_index();
              //e.set_cancel(true);
              //if (index == 1) {
              //  // edit cell
              //  var cell = webDataGrid.get_rows().get_row(index).get_cellByColumnKey("rmsc_BDRMK");
              //  webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
              //}
              //else {
              //  // add cell
              //  var row = e.getNewActiveCell().get_row();
              //  var cell = row.get_cellByColumnKey("rmsc_BDRMK");
              //  webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
              //}
            }
          }
        }

        ///
        function WebDataGridView_AJAXResponse(grid, e) {
          if (e.get_gridResponseObject().Message)
            alert(e.get_gridResponseObject().Message);
          ///
        }
      </script>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
