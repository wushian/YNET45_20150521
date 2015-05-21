<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_temp_mdd.aspx.cs" Inherits="DD2015_45.Forms.rix.fm_temp_mdd" %>

<%@ Register Assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.WebUI.WebDataInput.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics45.WebUI.WebHtmlEditor.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebHtmlEditor" TagPrefix="ighedit" %>

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
      <input id="hh_dtable1_OldNum_Field" type="hidden" name="hh_dtable1_OldNum_Field" runat="server" />
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
          <ig:ContentTabItem runat="server" Text="查詢">
            <Template>
              <asp:Panel ID="PanSer" runat="server" Width="1000px">
                <table>
                  <tr>
                    <td>
                      <ig:WebTab ID="WebTab_SER" runat="server" Width="1100px" StyleSetName="Pear" StyleSetPath="~/ig_res" TabItemSize="70px">
                        <Tabs>
                          <ig:ContentTabItem runat="server" Text="一般">
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
                                            <asp:Label ID="lb_mtable_BDDTS_s1" runat="server" Text="開始日期"></asp:Label>
                                          </td>
                                          <td>
                                            <ig:WebDatePicker ID="tx_mtable_BDDTS_s1" Width="100px" CssClass="Office2010Blue" runat="server"></ig:WebDatePicker>
                                          </td>
                                          <td>
                                            <asp:Label ID="lb_mtable_BDDTS_s2" runat="server" Text="～"></asp:Label>
                                          </td>
                                          <td>
                                            <ig:WebDatePicker ID="tx_mtable_BDDTS_s2" Width="100px" CssClass="Office2010Blue" runat="server"></ig:WebDatePicker>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <asp:Label ID="lb_mtable_BDPTN" runat="server" Text="商品編號"></asp:Label>
                                      <asp:TextBox ID="tx_mtable_BDPTN_s" Width="80px" runat="server" MaxLength="30"></asp:TextBox>
                                      <asp:TextBox ID="tx_bpud_BPTNA_s" Width="80px" runat="server" MaxLength="30" ReadOnly="true"></asp:TextBox>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                </table>
                              </asp:Panel>
                            </Template>
                          </ig:ContentTabItem>
                          <ig:ContentTabItem runat="server" Text="進階">
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
                <ig:WebDataGrid ID="WebDataGrid_mtable" runat="server"
                  EnableAjax="false" EnableViewState="True" EnableDataViewState="True"
                  Width="1100px" AutoGenerateColumns="False" DataKeyFields="mtable_gkey" OnRowSelectionChanged="WebDataGrid_mtable_RowSelectionChanged">
                  <Columns>
                    <ig:TemplateDataField Key="mtable_hidden" Hidden="true">
                      <ItemTemplate>
                        <input id="tx_mtable_gkey02" type="hidden" name="tx_mtable_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "mtable_gkey").ToString() %>' runat="server" />
                        <input id="tx_mtable_mkey02" type="hidden" name="tx_mtable_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "mtable_mkey").ToString() %>' runat="server" />
                        <input id="tx_mtable_BDCCN02" type="hidden" name="tx_mtable_BDCCN02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "mtable_BDCCN").ToString() %>' runat="server" />
                      </ItemTemplate>
                      <Header Text="mtable_hidden" />
                    </ig:TemplateDataField>
                    <ig:BoundDataField DataFieldName="mtable_BDCCN" Key="mtable_BDCCN" Width="100px">
                      <Header Text="mtable_BDCCN">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="mtable_BDDTS" Key="mtable_BDDTS" Width="100px">
                      <Header Text="mtable_BDDTS">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="mtable_BDPTN" Key="mtable_BDPTN" Width="120px">
                      <Header Text="mtable_BDPTN">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bpud_BPTNA" Key="bpud_BPTNA" Width="300px">
                      <Header Text="bpud_BPTNA">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="mtable_BDNUM" Key="mtable_BDNUM" Width="100px">
                      <Header Text="mtable_BDNUM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDNAM" Key="bdlr_BDNAM" Width="200px">
                      <Header Text="bdlr_BDNAM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="mtable_gkey" Key="mtable_gkey" Hidden="true" HtmlEncode="true">
                      <Header Text="mtable_gkey">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="mtable_mkey" Key="mtable_mkey" Hidden="true" HtmlEncode="true">
                      <Header Text="mtable_mkey">
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
          <ig:ContentTabItem runat="server" Text="編輯">
            <Template>
              <table>
                <tr>
                  <td class="tdtop">
                    <asp:Panel ID="PanEdtLeft" runat="server" Width="250px" BorderStyle="Inset">
                      <ig:WebDataGrid ID="WebDataGrid_mtable_ba" runat="server"
                        EnableAjax="False" EnableDataViewState="True"
                        Width="240px" AutoGenerateColumns="False" DataKeyFields="mtable_gkey" OnRowSelectionChanged="WebDataGrid_mtable_ba_RowSelectionChanged">
                        <Columns>
                          <ig:TemplateDataField Key="mtable_hidden" Hidden="true">
                            <ItemTemplate>
                              <input id="tx_mtable_gkey02" type="hidden" name="tx_mtable_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "mtable_gkey").ToString() %>' runat="server" />
                              <input id="tx_mtable_mkey02" type="hidden" name="tx_mtable_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "mtable_mkey").ToString() %>' runat="server" />
                              <input id="tx_mtable_BDCCN02" type="hidden" name="tx_mtable_BDCCN02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "mtable_BDCCN").ToString() %>' runat="server" />
                            </ItemTemplate>
                            <Header Text="mtable_hidden" />
                          </ig:TemplateDataField>
                          <ig:BoundDataField DataFieldName="mtable_BDCCN" Key="mtable_BDCCN" Width="100px">
                            <Header Text="mtable_BDCCN">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="mtable_BDDTS" Key="mtable_BDDTS" Width="100px">
                            <Header Text="mtable_BDDTS">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="mtable_gkey" Key="mtable_gkey" Hidden="true" HtmlEncode="true">
                            <Header Text="mtable_gkey">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="mtable_mkey" Key="mtable_mkey" Hidden="true" HtmlEncode="true">
                            <Header Text="mtable_mkey">
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
                          <td>
                            <asp:Label ID="lb_mtable_BDCCN" runat="server" Text="合約編號"></asp:Label>
                            <asp:TextBox ID="tx_mtable_BDCCN" Width="120px" runat="server" MaxLength="30"></asp:TextBox>
                          </td>
                          <td>
                            <table>
                              <tr>
                                <td>
                                  <asp:Label ID="lb_mtable_BDDTS" runat="server" Text="開始日期"></asp:Label>
                                </td>
                                <td>
                                  <ig:WebDatePicker ID="tx_mtable_BDDTS" Width="120px" runat="server" DisplayModeFormat="d" Font-Size="Medium"></ig:WebDatePicker>
                                </td>
                              </tr>
                            </table>
                          </td>
                          <td>
                            <table>
                              <tr>
                                <td>
                                  <asp:Label ID="lb_mtable_BDDTE" runat="server" Text="結束日期"></asp:Label>
                                </td>
                                <td>
                                  <ig:WebDatePicker ID="tx_mtable_BDDTE" Width="120px" runat="server" DisplayModeFormat="d" Font-Size="Medium"></ig:WebDatePicker>
                                </td>
                              </tr>
                            </table>

                          </td>
                        </tr>
                        <tr>
                          <td colspan="3">
                            <asp:Label ID="lb_mtable_BDNUM" runat="server" Text="出版社號"></asp:Label>
                            <asp:TextBox ID="tx_mtable_BDNUM" Width="120px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_bdlr_BDNAM" Width="360px" runat="server" MaxLength="40" Enabled="false" TabIndex="-1"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="3">
                            <asp:Label ID="Label1" runat="server" Text="商品編號"></asp:Label>
                            <asp:TextBox ID="tx_mtable_BDPTN" Width="120px" runat="server" MaxLength="30"></asp:TextBox>
                            <asp:TextBox ID="tx_bpud_BPTNA" Width="360px" runat="server" MaxLength="30" Enabled="false" TabIndex="-1"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_mtable_BDQTY" runat="server" Text="預付本數"></asp:Label>
                            <ig:WebNumericEditor ID="tx_mtable_BDQTY" Width="120px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          </td>
                          <td>
                            <asp:Label ID="lb_mtable_BDDE1" runat="server" Text="定價金額"></asp:Label>
                            <ig:WebNumericEditor ID="tx_mtable_BDDE1" Width="120px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          </td>
                          <td>
                            <asp:Label ID="lb_mtable_BDRAT" runat="server" Text="預付％數"></asp:Label>
                            <ig:WebNumericEditor ID="tx_mtable_BDRAT" Width="120px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_mtable_BDTXP" runat="server" Text="計稅方式"></asp:Label>
                            <asp:DropDownList ID="dr_mtable_BDTXP" Width="120px" runat="server" />
                            <asp:TextBox ID="tx_mtable_BDTXP" Width="0px" Visible="false" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td colspan="4">
                            <asp:Label ID="lb_mtable_BDRMK" runat="server" Text="備　　註"></asp:Label>
                            <asp:TextBox ID="tx_mtable_BDRMK" Width="500px" runat="server" MaxLength="100"></asp:TextBox>
                          </td>
                        </tr>
                      </table>
                    </asp:Panel>
                    <br />
                    <table>
                      <tr>
                        <td>
                          <igtxt:WebImageButton ID="bt_New_B" Text="新增" ImageDirectory="../../images/" AutoSubmit="False" runat="server">
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
                      <ig:WebTab ID="WebTabGrid" runat="server" Height="300px" Width="900px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px" TabIndex="1" SelectedIndex="1">
                        <Tabs>
                          <ig:ContentTabItem runat="server" Text="作著資料" VisibleIndex="0">
                            <Template>
                              <ig:WebDataGrid ID="WebDataGrid_dtable1" runat="server"
                                Width="890px" AutoGenerateColumns="False" DataKeyFields="dtable1_gkey" OnRowSelectionChanged="WebDataGrid_dtable1_RowSelectionChanged" OnRowAdded="WebDataGrid_dtable1_RowAdded" OnRowAdding="WebDataGrid_dtable1_RowAdding" OnRowUpdated="WebDataGrid_dtable1_RowUpdated" OnRowUpdating="WebDataGrid_dtable1_RowUpdating" Height="250px">
                                <Columns>
                                  <ig:BoundDataField DataFieldName="dtable1_BDCCN" Key="dtable1_BDCCN" Width="100px">
                                    <Header Text="dtable1_BDCCN">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable1_BDAUR" Key="dtable1_BDAUR" Width="60px">
                                    <Header Text="dtable1_BDAUR">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="baur_BCNAM" Key="baur_BCNAM" Width="120px">
                                    <Header Text="baur_BCNAM">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable1_BDAMT" Key="dtable1_BDAMT" Width="80px" DataType="System.Decimal" CssClass="txRightWdg" DataFormatString="{0:N2}">
                                    <Header Text="dtable1_BDAMT" CssClass="txRightWdg">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable1_BDEDT" Key="dtable1_BDEDT" Width="80px">
                                    <Header Text="dtable1_BDEDT">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable1_BDINV" Key="dtable1_BDINV" Width="70px">
                                    <Header Text="dtable1_BDINV">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable1_BDINT" Key="dtable1_BDINT" Width="120px">
                                    <Header Text="dtable1_BDINT">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable1_BDRMK" Key="dtable1_BDRMK" Width="80px">
                                    <Header Text="dtable1_BDRMK">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable1_gkey" Key="dtable1_gkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="dtable1_gkey">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable1_mkey" Key="dtable1_mkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="dtable1_mkey">
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
                                    <EditingClientEvents  />
                                    <Behaviors>
                                      <ig:RowDeleting ShowDeleteButton="True">
                                      </ig:RowDeleting>
                                      <ig:CellEditing>
                                        <ColumnSettings>
                                          <ig:EditingColumnSetting ColumnKey="dtable1_BDCCN" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="dtable1_BDAUR" />
                                          <ig:EditingColumnSetting ColumnKey="baur_BCNAM" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="dtable1_BDAMT" EditorID="WebDataGrid_dtable1_RDAMT" />
                                          <ig:EditingColumnSetting ColumnKey="dtable1_BDEDT" EditorID="WebDataGrid_dtable1_BDEDT" />
                                          <ig:EditingColumnSetting ColumnKey="dtable1_BDINV" />
                                          <ig:EditingColumnSetting ColumnKey="dtable1_BDINT" />
                                          <ig:EditingColumnSetting ColumnKey="dtable1_BDRMK" />
                                          <ig:EditingColumnSetting ColumnKey="dtable1_gkey" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="dtable1_mkey" ReadOnly="True" />
                                        </ColumnSettings>
                                        <CellEditingClientEvents ExitedEditMode="dtable1_CellEdit_ExitedEditMode" EnteredEditMode="dtable1_CellEdit_EnteredEditMode" />
                                        <EditModeActions EnableOnActive="True" EnableOnKeyPress="True" />
                                      </ig:CellEditing>
                                    </Behaviors>
                                  </ig:EditingCore>
                                  <ig:Activation>
                                    <ActivationClientEvents ActiveCellChanging="dtable1_Activation_ActiveCellChanging" />
                                  </ig:Activation>
                                  <ig:Paging>
                                  </ig:Paging>
                                </Behaviors>
                                <EditorProviders>
                                  <ig:DateTimeEditorProvider ID="WebDataGrid_dtable1_BDEDT">
                                    <EditorControl ClientIDMode="Predictable">
                                    </EditorControl>
                                  </ig:DateTimeEditorProvider>
                                  <ig:NumericEditorProvider ID="WebDataGrid_dtable1_RDAMT">
                                    <EditorControl ClientIDMode="Predictable">
                                    </EditorControl>
                                  </ig:NumericEditorProvider>
                                </EditorProviders>
                                <ClientEvents AJAXResponse="WebDataGridView_AJAXResponse" />
                              </ig:WebDataGrid>
                            </Template>
                          </ig:ContentTabItem>
                          <ig:ContentTabItem runat="server" Text="版稅率" VisibleIndex="1">
                            <Template>
                              <ig:WebDataGrid ID="WebDataGrid_dtable2" runat="server"
                                Width="890px" AutoGenerateColumns="False" DataKeyFields="dtable2_gkey" OnRowSelectionChanged="WebDataGrid_dtable2_RowSelectionChanged" OnRowAdded="WebDataGrid_dtable2_RowAdded" OnRowAdding="WebDataGrid_dtable2_RowAdding" OnRowUpdated="WebDataGrid_dtable2_RowUpdated" OnRowUpdating="WebDataGrid_dtable2_RowUpdating" Height="250px">
                                <Columns>
                                  <ig:BoundDataField DataFieldName="dtable2_BDCCN" Key="dtable2_BDCCN" Width="100px">
                                    <Header Text="dtable2_BDCCN">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable2_BDDCX" Key="dtable2_BDDCX" Width="80px" DataType="System.Decimal" CssClass="txRightWdg" DataFormatString="{0:N2}" Hidden="true">
                                    <Header Text="dtable2_BDDCX" CssClass="txRightWdg">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable2_BDQTY" Key="dtable2_BDQTY" Width="80px" DataType="System.Decimal" CssClass="txRightWdg" DataFormatString="{0:N2}">
                                    <Header Text="dtable2_BDQTY" CssClass="txRightWdg">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable2_BDRAT" Key="dtable2_BDRAT" Width="80px" DataType="System.Decimal" CssClass="txRightWdg" DataFormatString="{0:N2}">
                                    <Header Text="dtable2_BDRAT" CssClass="txRightWdg">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable2_BDRMK" Key="dtable2_BDRMK" Width="180px">
                                    <Header Text="dtable2_BDRMK">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable2_gkey" Key="dtable2_gkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="dtable2_gkey">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="dtable2_mkey" Key="dtable2_mkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="dtable2_mkey">
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
                                    <EditingClientEvents />
                                    <Behaviors>
                                      <ig:RowDeleting ShowDeleteButton="True">
                                      </ig:RowDeleting>
                                      <ig:CellEditing>
                                        <ColumnSettings>
                                          <ig:EditingColumnSetting ColumnKey="dtable2_BDCCN" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="dtable2_BDQTY" />
                                          <ig:EditingColumnSetting ColumnKey="dtable2_BDRAT" />
                                          <ig:EditingColumnSetting ColumnKey="dtable2_BDRMK" />
                                          <ig:EditingColumnSetting ColumnKey="dtable2_gkey" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="dtable2_mkey" ReadOnly="True" />
                                        </ColumnSettings>
                                        <CellEditingClientEvents ExitedEditMode="dtable2_CellEdit_ExitedEditMode" EnteredEditMode="dtable2_CellEdit_EnteredEditMode" />
                                        <EditModeActions EnableOnActive="True" EnableOnKeyPress="True" />
                                      </ig:CellEditing>
                                    </Behaviors>
                                  </ig:EditingCore>
                                  <ig:Activation>
                                    <ActivationClientEvents ActiveCellChanging="dtable2_Activation_ActiveCellChanging" />
                                  </ig:Activation>
                                  <ig:Paging>
                                  </ig:Paging>
                                </Behaviors>
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
      <asp:ObjectDataSource ID="Obj_mtable" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_mtable" TypeName="DD2015_45.DAC_mtable" OnSelecting="Obj_mtable_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_mtable_ba" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_mtable_ba" TypeName="DD2015_45.DAC_mtable" OnSelecting="Obj_mtable_ba_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_dtable2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_dtable2" TypeName="DD2015_45.DAC_dtable2" OnSelecting="Obj_dtable2_Selecting" UpdateMethod="UpdateTable_dtable2" InsertMethod="InsertTable_dtable2" OnInserting="Obj_dtable2_Inserting" DeleteMethod="DeleteTable_dtable2" OnDeleting="Obj_dtable2_Deleting" OnUpdating="Obj_dtable2_Updating" OnDeleted="Obj_dtable2_Deleted" OnInserted="Obj_dtable2_Inserted" OnUpdated="Obj_dtable2_Updated">
        <DeleteParameters>
          <asp:Parameter Name="original_dtable2_gkey" Type="String" />
          <asp:Parameter Name="dtable2_gkey" Type="String" />
          <asp:Parameter Name="dtable2_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </DeleteParameters>
        <InsertParameters>
          <asp:Parameter Name="dtable2_BDCCN" Type="String" />
          <asp:Parameter Name="dtable2_BDDCX" Type="Decimal" />
          <asp:Parameter Name="dtable2_BDQTY" Type="Decimal" />
          <asp:Parameter Name="dtable2_BDRAT" Type="Decimal" />
          <asp:Parameter Name="dtable2_BDRMK" Type="String" />
          <asp:Parameter Name="dtable2_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </InsertParameters>
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
        <UpdateParameters>
          <asp:Parameter Name="original_dtable2_gkey" Type="String" />
          <asp:Parameter Name="dtable2_gkey" Type="String" />
          <asp:Parameter Name="dtable2_mkey" Type="String" />
          <asp:Parameter Name="dtable2_BDCCN" Type="String" />
          <asp:Parameter Name="dtable2_BDDCX" Type="Decimal" />
          <asp:Parameter Name="dtable2_BDQTY" Type="Decimal" />
          <asp:Parameter Name="dtable2_BDRAT" Type="Decimal" />
          <asp:Parameter Name="dtable2_BDRMK" Type="String" />
          <asp:Parameter Name="dtable2_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </UpdateParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_dtable1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_dtable1" TypeName="DD2015_45.DAC_dtable1" OnSelecting="Obj_dtable1_Selecting" UpdateMethod="UpdateTable_dtable1" InsertMethod="InsertTable_dtable1" OnInserting="Obj_dtable1_Inserting" DeleteMethod="DeleteTable_dtable1" OnDeleting="Obj_dtable1_Deleting" OnUpdating="Obj_dtable1_Updating" OnUpdated="Obj_dtable1_Updated" OnDeleted="Obj_dtable1_Deleted" OnInserted="Obj_dtable1_Inserted">
        <DeleteParameters>
          <asp:Parameter Name="original_dtable1_gkey" Type="String" />
          <asp:Parameter Name="dtable1_gkey" Type="String" />
          <asp:Parameter Name="dtable1_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </DeleteParameters>
        <InsertParameters>
          <asp:Parameter Name="dtable1_BDCCN" Type="String" />
          <asp:Parameter Name="dtable1_BDAUR" Type="String" />
          <asp:Parameter Name="dtable1_BDAMT" Type="String" />
          <asp:Parameter Name="dtable1_BDEDT" Type="String" />
          <asp:Parameter Name="dtable1_BDINV" Type="String" />
          <asp:Parameter Name="dtable1_BDINT" Type="String" />
          <asp:Parameter Name="dtable1_BDRMK" Type="String" />
          <asp:Parameter Name="dtable1_actkey" Type="String" />
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
          <asp:Parameter Name="original_dtable1_gkey" Type="String" />
          <asp:Parameter Name="dtable1_gkey" Type="String" />
          <asp:Parameter Name="dtable1_mkey" Type="String" />
          <asp:Parameter Name="dtable1_BDCCN" Type="String" />
          <asp:Parameter Name="dtable1_BDAUR" Type="String" />
          <asp:Parameter Name="dtable1_BDAMT" Type="String" />
          <asp:Parameter Name="dtable1_BDEDT" Type="String" />
          <asp:Parameter Name="dtable1_BDINV" Type="String" />
          <asp:Parameter Name="dtable1_BDINT" Type="String" />
          <asp:Parameter Name="dtable1_BDRMK" Type="String" />
          <asp:Parameter Name="dtable1_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </UpdateParameters>
      </asp:ObjectDataSource>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
      <script type="text/javascript">
        var hh_company = document.all['ctl00$hh_Company'].value;
        var hh_dtable1_OldNum_Field = "<%= this.hh_dtable1_OldNum_Field.ClientID %>";
        var inputkey = 0;

        function di_Window_initialize(sender, e) {
          document.all[hh_dtable1_OldNum_Field].value = empty_field;
          sender.hide();
        }

        function di_Window_windowStateChanged(diWin, evntArgs) {
          var diWin = $find('<%= di_Window.ClientID %>');
          var state = diWin.get_windowState(); //1=Minimized 2=Maximized 3=Closed other= Restored
          var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
          var diWin_cmd = document.all[diWin_cmdField].value;
          if (state == 3) {
            if (diWin_cmd == "add_dtable1") {
              try {
                document.all[diWin_cmdField].value = "*"; //reset command
                webDataGrid_SetFieldValue("<%= this.WebDataGrid_dtable1.ClientID %>", 'dtable1_BDAUR', "");
                webDataGrid_RowAddFocus("<%= this.WebDataGrid_dtable1.ClientID %>", 'dtable1_BDAUR');
              }
              catch (e) {
                document.all[diWin_cmdField].value = "*"; //reset command
              }
            }
            else if (diWin_cmd == "mod_dtable1") {
              try {
                document.all[diWin_cmdField].value = "*"; //reset command
                var dtable1_OldNum = document.all[hh_dtable1_OldNum_Field].value;
                if (dtable1_OldNum != "" && dtable1_OldNum != empty_field) {
                  webDataGrid_SetFieldValue("<%= this.WebDataGrid_dtable1.ClientID %>", 'dtable1_BDAUR', dtable1_OldNum);
                }
                webDataGrid_ModRowfocus("<%= this.WebDataGrid_dtable1.ClientID %>", 'dtable1_BDAUR');
              }
              catch (e) {
                document.all[diWin_cmdField].value = "*"; //reset command
              }
            }

        }
      }



      function dtable2_CellEdit_EnteredEditMode(sender, e) {

      }

      function dtable2_CellEdit_ExitedEditMode(sender, e) {
        var cell = e.getCell();
        var value = cell.get_value();
        var row = cell.get_row();
        var rowIndex = row.get_index();
        var column = cell.get_column();
        var columnKey = column.get_key();
        //
        //
      }

      function dtable2_Activation_ActiveCellChanging(webDataGrid, e) {
        var column = e.getNewActiveCell().get_column();
        if (column != null) {
          var columnKey = column.get_key();
          if (columnKey == "dtable2_BDRMK") {

          }
        }
        ///
      }

      function dtable1_CellEdit_EnteredEditMode(sender, e) {
        var cell = e.getCell();
        var value = cell.get_value();
        var row = cell.get_row();
        var rowIndex = row.get_index();
        var column = cell.get_column();
        var columnKey = column.get_key();
        var dtable1_BDAUR = row.get_cellByColumnKey("dtable1_BDAUR").get_value();
        if (columnKey == "dtable1_BDAUR") {
          //save old rbptn
          document.all[hh_dtable1_OldNum_Field].value = dtable1_BDAUR;
        }
        else {
          document.all[hh_dtable1_OldNum_Field].value = empty_field;
        }
      }

      function dtable1_CellEdit_ExitedEditMode(sender, e) {
        var cell = e.getCell();
        var value = cell.get_value();
        var row = cell.get_row();
        var rowIndex = row.get_index();
        var column = cell.get_column();
        var columnKey = column.get_key();
        //
        if (columnKey == "dtable1_BDAUR") {
          var dtable1_OldNum = document.all[hh_dtable1_OldNum_Field].value;
          var dtable1_BDAUR = row.get_cellByColumnKey("dtable1_BDAUR").get_value();
          dtable1_BDAUR = strtrim(dtable1_BDAUR);
          if (dtable1_BDAUR != dtable1_OldNum) {
            var iContEdit = '<%=st_ContentPlaceHolderEdit%>';
            var di_Window = '<%=di_Window.ClientID%>';
            var DataGrid_id = '<%=WebDataGrid_dtable1.ClientID%>';
            var di_OpenUrl = "../Dialog/Dialog_bdlr.aspx?iFunc=" + "dtable1" + "&oNewMod=mod" + "&oWindow_Id=" + di_Window + "&oDataGrid_id=" + DataGrid_id + "&iField=dtable1_BDAUR" + "&oField=baur_BCNAM"
            var di_Caption = cell.get_column().get_headerText();
            var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
              document.all[diWin_cmdField].value = "mod_dtable1";
              get_bdlr_grname('dtable1', iContEdit, "mod", "dtable1_BDAUR", "baur_BCNAM", "dtable1_BDINV", DataGrid_id, row, di_Window, di_OpenUrl, di_Caption, dtable1_BDAUR, dtable1_OldNum);
            }
          }
        //
        }

        function dtable1_Activation_ActiveCellChanging(webDataGrid, e) {
          var column = e.getNewActiveCell().get_column();
          if (column != null) {
            var columnKey = column.get_key();
            if (columnKey == "baur_BCNAM") {

            }
          }
        }
        ///
        function WebDataGridView_AJAXResponse(grid, e) {
          if (e.get_gridResponseObject().Message)
            alert(e.get_gridResponseObject().Message);
          ///
        }
        //
        function webDataGrid_dtable1_AddRow(webDataGrid_ClientID, st_focus_field) {
          inputkey = inputkey + 1;
          var ar_row = new Array("", "", null, "0", null, null, null, null, inputkey.toString(), inputkey.toString());
          webDataGrid_AddRow(webDataGrid_ClientID, ar_row, st_focus_field, 22)
        }
        //
        function webDataGrid_dtable2_AddRow(webDataGrid_ClientID, st_focus_field) {
          inputkey = inputkey + 1;
          var ar_row = new Array("", 0, 0, 0, "", inputkey.toString(), inputkey.toString());
          webDataGrid_AddRow(webDataGrid_ClientID, ar_row, st_focus_field, 22)
        }
      </script>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
