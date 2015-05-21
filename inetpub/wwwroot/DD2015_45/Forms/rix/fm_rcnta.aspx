<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_rcnta.aspx.cs" Inherits="DD2015_45.Forms.rix.fm_rcnta" %>

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

  <script type="text/javascript" id="igClientScript">
<!--


  // -->
  </script>

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
      <input id="hh_rcntr_OldNum_Field" type="hidden" name="hh_rcntr_OldNum_Field" runat="server" />
      <input id="btnAction" type="hidden" name="btnAction" runat="server" />
      <input id="btnUpdateCancel" type="hidden" name="btnUpdateCancel" value="" enableviewstate="false" runat="server" />
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
                                            <asp:Label ID="lb_rcnta_BDDTS_s1" runat="server" Text="開始日期"></asp:Label>
                                          </td>
                                          <td>
                                            <ig:WebDatePicker ID="tx_rcnta_BDDTS_s1" Width="100px" CssClass="Office2010Blue" runat="server"></ig:WebDatePicker>
                                          </td>
                                          <td>
                                            <asp:Label ID="lb_rcnta_BDDTS_s2" runat="server" Text="～"></asp:Label>
                                          </td>
                                          <td>
                                            <ig:WebDatePicker ID="tx_rcnta_BDDTS_s2" Width="100px" CssClass="Office2010Blue" runat="server"></ig:WebDatePicker>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <asp:Label ID="lb_rcnta_BDPTN" runat="server" Text="商品編號"></asp:Label>
                                      <asp:TextBox ID="tx_rcnta_BDPTN_s" Width="80px" runat="server" MaxLength="30"></asp:TextBox>
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
                <ig:WebDataGrid ID="WebDataGrid_rcnta" runat="server"
                  EnableAjax="False" EnableDataViewState="True"
                  Width="1100px" AutoGenerateColumns="False" DataKeyFields="rcnta_gkey">
                  <Columns>
                    <ig:TemplateDataField Key="rcnta_hidden" Hidden="true">
                      <ItemTemplate>
                        <input id="tx_rcnta_gkey02" type="hidden" name="tx_rcnta_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rcnta_gkey").ToString() %>' runat="server" />
                        <input id="tx_rcnta_mkey02" type="hidden" name="tx_rcnta_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rcnta_mkey").ToString() %>' runat="server" />
                        <input id="tx_rcnta_BDCCN02" type="hidden" name="tx_rcnta_BDCCN02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rcnta_BDCCN").ToString() %>' runat="server" />
                      </ItemTemplate>
                      <Header Text="rcnta_hidden" />
                    </ig:TemplateDataField>
                    <ig:BoundDataField DataFieldName="rcnta_BDCCN" Key="rcnta_BDCCN" Width="100px">
                      <Header Text="rcnta_BDCCN">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="rcnta_BDDTS" Key="rcnta_BDDTS" Width="100px">
                      <Header Text="rcnta_BDDTS">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="rcnta_BDPTN" Key="rcnta_BDPTN" Width="120px">
                      <Header Text="rcnta_BDPTN">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bpud_BPTNA" Key="bpud_BPTNA" Width="300px">
                      <Header Text="bpud_BPTNA">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="rcnta_BDNUM" Key="rcnta_BDNUM" Width="100px">
                      <Header Text="rcnta_BDNUM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDNAM" Key="bdlr_BDNAM" Width="200px">
                      <Header Text="bdlr_BDNAM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="rcnta_gkey" Key="rcnta_gkey" Hidden="true" HtmlEncode="true">
                      <Header Text="rcnta_gkey">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="rcnta_mkey" Key="rcnta_mkey" Hidden="true" HtmlEncode="true">
                      <Header Text="rcnta_mkey">
                      </Header>
                    </ig:BoundDataField>
                  </Columns>
                  <ClientEvents DoubleClick="WebDataGrid_rcnta_Grid_DoubleClick" />
                  <Behaviors>
                    <ig:Selection CellClickAction="Row" CellSelectType="None" RowSelectType="Single">
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
                      <ig:WebDataGrid ID="WebDataGrid_rcnta_ba" runat="server"
                        EnableAjax="False" EnableDataViewState="True"
                        Width="240px" AutoGenerateColumns="False" DataKeyFields="rcnta_gkey" OnRowSelectionChanged="WebDataGrid_rcnta_ba_RowSelectionChanged">
                        <Columns>
                          <ig:TemplateDataField Key="rcnta_hidden" Hidden="true">
                            <ItemTemplate>
                              <input id="tx_rcnta_gkey02" type="hidden" name="tx_rcnta_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rcnta_gkey").ToString() %>' runat="server" />
                              <input id="tx_rcnta_mkey02" type="hidden" name="tx_rcnta_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rcnta_mkey").ToString() %>' runat="server" />
                              <input id="tx_rcnta_BDCCN02" type="hidden" name="tx_rcnta_BDCCN02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "rcnta_BDCCN").ToString() %>' runat="server" />
                            </ItemTemplate>
                            <Header Text="rcnta_hidden" />
                          </ig:TemplateDataField>
                          <ig:BoundDataField DataFieldName="rcnta_BDCCN" Key="rcnta_BDCCN" Width="100px">
                            <Header Text="rcnta_BDCCN">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="rcnta_BDDTS" Key="rcnta_BDDTS" Width="100px">
                            <Header Text="rcnta_BDDTS">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="rcnta_gkey" Key="rcnta_gkey" Hidden="true" HtmlEncode="true">
                            <Header Text="rcnta_gkey">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="rcnta_mkey" Key="rcnta_mkey" Hidden="true" HtmlEncode="true">
                            <Header Text="rcnta_mkey">
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
                  <td class="tdtop">
                    <asp:Panel ID="PanEdtRightTop" runat="server" Width="800px" BorderStyle="Inset">
                      <table>
                        <tr>
                          <td>&nbsp;
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_rcnta_BDCCN" runat="server" Text="合約編號"></asp:Label>
                            <asp:TextBox ID="tx_rcnta_BDCCN" Width="120px" runat="server" MaxLength="30"></asp:TextBox>
                          </td>
                          <td>
                            <table>
                              <tr>
                                <td>
                                  <asp:Label ID="lb_rcnta_BDDTS" runat="server" Text="開始日期"></asp:Label>
                                </td>
                                <td>
                                  <ig:WebDatePicker ID="tx_rcnta_BDDTS" Width="120px" runat="server" DisplayModeFormat="d" Font-Size="Medium"></ig:WebDatePicker>
                                </td>
                              </tr>
                            </table>
                          </td>
                          <td>
                            <table>
                              <tr>
                                <td>
                                  <asp:Label ID="lb_rcnta_BDDTE" runat="server" Text="結束日期"></asp:Label>
                                </td>
                                <td>
                                  <ig:WebDatePicker ID="tx_rcnta_BDDTE" Width="120px" runat="server" DisplayModeFormat="d" Font-Size="Medium"></ig:WebDatePicker>
                                </td>
                              </tr>
                            </table>

                          </td>
                        </tr>
                        <tr>
                          <td colspan="3">
                            <asp:Label ID="lb_rcnta_BDNUM" runat="server" Text="出版社號"></asp:Label>
                            <asp:TextBox ID="tx_rcnta_BDNUM" Width="120px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_bdlr_BDNAM" Width="360px" runat="server" MaxLength="40" Enabled="false" TabIndex="-1"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="3">
                            <asp:Label ID="Label1" runat="server" Text="商品編號"></asp:Label>
                            <asp:TextBox ID="tx_rcnta_BDPTN" Width="120px" runat="server" MaxLength="30"></asp:TextBox>
                            <asp:TextBox ID="tx_bpud_BPTNA" Width="360px" runat="server" MaxLength="30" Enabled="false" TabIndex="-1"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_rcnta_BDQTY" runat="server" Text="預付本數"></asp:Label>
                            <ig:WebNumericEditor ID="tx_rcnta_BDQTY" Width="120px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          </td>
                          <td>
                            <asp:Label ID="lb_rcnta_BDDE1" runat="server" Text="定價金額"></asp:Label>
                            <ig:WebNumericEditor ID="tx_rcnta_BDDE1" Width="120px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          </td>
                          <td>
                            <asp:Label ID="lb_rcnta_BDRAT" runat="server" Text="預付％數"></asp:Label>
                            <ig:WebNumericEditor ID="tx_rcnta_BDRAT" Width="120px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_rcnta_BDTXP" runat="server" Text="計稅方式"></asp:Label>
                            <asp:DropDownList ID="dr_rcnta_BDTXP" Width="120px" runat="server" />
                            <asp:TextBox ID="tx_rcnta_BDTXP" Width="0px" Visible="false" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td colspan="4">
                            <asp:Label ID="lb_rcnta_BDRMK" runat="server" Text="備　　註"></asp:Label>
                            <asp:TextBox ID="tx_rcnta_BDRMK" Width="500px" runat="server" MaxLength="100"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td>&nbsp;
                          </td>
                        </tr>
                      </table>
                    </asp:Panel>
                    <asp:Panel ID="PanEdtRightDown" runat="server" Width="910px" BorderStyle="Inset">
                      <table>
                        <tr>
                          <td>
                            <igtxt:WebImageButton ID="bt_New_B" Text="Z新增" AccessKey="Z" ImageDirectory="../../images/" AutoSubmit="False" runat="server">
                              <Appearance>
                                <Image Url="add_down.gif"></Image>
                              </Appearance>
                            </igtxt:WebImageButton>
                          </td>
                          <td>
                            <igtxt:WebImageButton ID="bt_SAVE_B" Text="S存檔" AccessKey="S" AutoSubmit="False" ImageDirectory="../../images/" runat="server" OnClick="bt_SAVE_B_Click">
                              <ClientSideEvents Click="bt_SAVE_B_Click" />
                              <Appearance>
                                <Image Url="save_down.gif"></Image>
                              </Appearance>
                            </igtxt:WebImageButton>
                          </td>
                          <td>
                            <igtxt:WebImageButton ID="bt_Cancel_B" Text="O取消" AccessKey="O" AutoSubmit="False" ImageDirectory="../../images/" runat="server">
                              <ClientSideEvents Click="bt_Cancel_B_Click" />
                              <Appearance>
                                <Image Url="undo_down.gif"></Image>
                              </Appearance>
                            </igtxt:WebImageButton>
                          </td>
                          <td>
                            <igtxt:WebImageButton ID="bt_MOD_B" Text="W更正" AccessKey="W" ImageDirectory="../../images/" runat="server" OnClick="bt_MOD_B_Click">
                              <Appearance>
                                <Image Url="edit_down.gif"></Image>
                              </Appearance>
                            </igtxt:WebImageButton>
                          </td>
                        </tr>
                      </table>
                      <ig:WebTab ID="WebTabGrid" runat="server" Height="300px" Width="900px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px" TabIndex="1">
                        <Tabs>
                          <ig:ContentTabItem runat="server" Text="作著資料" VisibleIndex="0">
                            <Template>
                              <ig:WebDataGrid ID="WebDataGrid_rcntr" runat="server"
                                Width="890px" AutoGenerateColumns="False" DataKeyFields="rcntr_gkey" OnRowSelectionChanged="WebDataGrid_rcntr_RowSelectionChanged" OnRowAdded="WebDataGrid_rcntr_RowAdded" OnRowAdding="WebDataGrid_rcntr_RowAdding" OnRowUpdated="WebDataGrid_rcntr_RowUpdated" OnRowUpdating="WebDataGrid_rcntr_RowUpdating" Height="250px">
                                <Columns>
                                  <ig:TemplateDataField Key="EDIT" Width="22px">
                                    <Header Text="Del" />
                                    <ItemTemplate>
                                      <asp:ImageButton ID="Delete" OnClientClick="deleteRow('WebDataGrid_rcntr'); return false" ImageUrl="~/images/delete_down.gif"
                                        ToolTip="Delete" runat="server"></asp:ImageButton>
                                    </ItemTemplate>
                                  </ig:TemplateDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_BDCCN" Key="rcntr_BDCCN" Width="100px">
                                    <Header Text="rcntr_BDCCN">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_BDAUR" Key="rcntr_BDAUR" Width="60px">
                                    <Header Text="rcntr_BDAUR">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="baur_BCNAM" Key="baur_BCNAM" Width="120px">
                                    <Header Text="baur_BCNAM">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_BDAMT" Key="rcntr_BDAMT" Width="80px" DataType="System.Decimal" CssClass="txRightWdg" DataFormatString="{0:N2}">
                                    <Header Text="rcntr_BDAMT" CssClass="txRightWdg">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_BDEDT" Key="rcntr_BDEDT" Width="80px">
                                    <Header Text="rcntr_BDEDT">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_BDINV" Key="rcntr_BDINV" Width="70px">
                                    <Header Text="rcntr_BDINV">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_BDINT" Key="rcntr_BDINT" Width="120px">
                                    <Header Text="rcntr_BDINT">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_BDRMK" Key="rcntr_BDRMK" Width="80px">
                                    <Header Text="rcntr_BDRMK">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_gkey" Key="rcntr_gkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="rcntr_gkey">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntr_mkey" Key="rcntr_mkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="rcntr_mkey">
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
                                      <ig:RowDeleting>
                                      </ig:RowDeleting>
                                      <ig:CellEditing>
                                        <ColumnSettings>
                                          <ig:EditingColumnSetting ColumnKey="rcntr_BDCCN" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rcntr_BDAUR" />
                                          <ig:EditingColumnSetting ColumnKey="baur_BCNAM" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rcntr_BDAMT" EditorID="WebDataGrid_rcntr_RDAMT" />
                                          <ig:EditingColumnSetting ColumnKey="rcntr_BDEDT" EditorID="WebDataGrid_rcntr_BDEDT" />
                                          <ig:EditingColumnSetting ColumnKey="rcntr_BDINV" />
                                          <ig:EditingColumnSetting ColumnKey="rcntr_BDINT" />
                                          <ig:EditingColumnSetting ColumnKey="rcntr_BDRMK" />
                                          <ig:EditingColumnSetting ColumnKey="rcntr_gkey" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rcntr_mkey" ReadOnly="True" />
                                        </ColumnSettings>
                                        <CellEditingClientEvents ExitedEditMode="rcntr_CellEdit_ExitedEditMode" EnteredEditMode="rcntr_CellEdit_EnteredEditMode" />
                                        <EditModeActions EnableOnActive="True" EnableOnKeyPress="True" />
                                      </ig:CellEditing>
                                    </Behaviors>
                                  </ig:EditingCore>
                                  <ig:Activation>
                                    <ActivationClientEvents ActiveCellChanging="rcntr_Activation_ActiveCellChanging" />
                                  </ig:Activation>
                                  <ig:Paging>
                                  </ig:Paging>
                                </Behaviors>
                                <EditorProviders>
                                  <ig:DateTimeEditorProvider ID="WebDataGrid_rcntr_BDEDT">
                                    <EditorControl ClientIDMode="Predictable">
                                    </EditorControl>
                                  </ig:DateTimeEditorProvider>
                                  <ig:NumericEditorProvider ID="WebDataGrid_rcntr_RDAMT">
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
                              <ig:WebDataGrid ID="WebDataGrid_rcntd" runat="server"
                                Width="890px" AutoGenerateColumns="False" DataKeyFields="rcntd_gkey" OnRowSelectionChanged="WebDataGrid_rcntd_RowSelectionChanged" OnRowAdded="WebDataGrid_rcntd_RowAdded" OnRowAdding="WebDataGrid_rcntd_RowAdding" OnRowUpdated="WebDataGrid_rcntd_RowUpdated" OnRowUpdating="WebDataGrid_rcntd_RowUpdating" Height="250px">
                                <Columns>
                                  <ig:TemplateDataField Key="EDIT" Width="22px">
                                    <Header Text="Del" />
                                    <ItemTemplate>
                                      <asp:ImageButton ID="Delete" OnClientClick="deleteRow('WebDataGrid_rcntd'); return false" ImageUrl="~/images/delete_down.gif"
                                        ToolTip="Delete" runat="server"></asp:ImageButton>
                                    </ItemTemplate>
                                  </ig:TemplateDataField>
                                  <ig:BoundDataField DataFieldName="rcntd_BDCCN" Key="rcntd_BDCCN" Width="100px">
                                    <Header Text="rcntd_BDCCN">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntd_BDDCX" Key="rcntd_BDDCX" Width="80px" DataType="System.Decimal" CssClass="txRightWdg" DataFormatString="{0:N2}" Hidden="true">
                                    <Header Text="rcntd_BDDCX" CssClass="txRightWdg">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntd_BDQTY" Key="rcntd_BDQTY" Width="80px" DataType="System.Decimal" CssClass="txRightWdg" DataFormatString="{0:N2}">
                                    <Header Text="rcntd_BDQTY" CssClass="txRightWdg">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntd_BDRAT" Key="rcntd_BDRAT" Width="80px" DataType="System.Decimal" CssClass="txRightWdg" DataFormatString="{0:N2}">
                                    <Header Text="rcntd_BDRAT" CssClass="txRightWdg">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntd_BDRMK" Key="rcntd_BDRMK" Width="180px">
                                    <Header Text="rcntd_BDRMK">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntd_gkey" Key="rcntd_gkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="rcntd_gkey">
                                    </Header>
                                  </ig:BoundDataField>
                                  <ig:BoundDataField DataFieldName="rcntd_mkey" Key="rcntd_mkey" Hidden="true" HtmlEncode="true">
                                    <Header Text="rcntd_mkey">
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
                                      <ig:RowDeleting  >
                                      </ig:RowDeleting>
                                      <ig:CellEditing>
                                        <ColumnSettings>
                                          <ig:EditingColumnSetting ColumnKey="rcntd_BDCCN" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rcntd_BDQTY" />
                                          <ig:EditingColumnSetting ColumnKey="rcntd_BDRAT" />
                                          <ig:EditingColumnSetting ColumnKey="rcntd_BDRMK" />
                                          <ig:EditingColumnSetting ColumnKey="rcntd_gkey" ReadOnly="True" />
                                          <ig:EditingColumnSetting ColumnKey="rcntd_mkey" ReadOnly="True" />
                                        </ColumnSettings>
                                        <CellEditingClientEvents ExitedEditMode="rcntd_CellEdit_ExitedEditMode" EnteredEditMode="rcntd_CellEdit_EnteredEditMode" />
                                        <EditModeActions EnableOnActive="True" EnableOnKeyPress="True" />
                                      </ig:CellEditing>
                                    </Behaviors>
                                  </ig:EditingCore>
                                  <ig:Activation>
                                    <ActivationClientEvents ActiveCellChanging="rcntd_Activation_ActiveCellChanging" />
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
      <asp:ObjectDataSource ID="Obj_rcnta" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_rcnta" TypeName="DD2015_45.DAC_rcnta" OnSelecting="Obj_rcnta_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_rcnta_ba" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_rcnta_ba" TypeName="DD2015_45.DAC_rcnta" OnSelecting="Obj_rcnta_ba_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_rcntd" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_rcntd" TypeName="DD2015_45.DAC_rcntd" OnSelecting="Obj_rcntd_Selecting" UpdateMethod="UpdateTable_rcntd" InsertMethod="InsertTable_rcntd" OnInserting="Obj_rcntd_Inserting" DeleteMethod="DeleteTable_rcntd" OnDeleting="Obj_rcntd_Deleting" OnUpdating="Obj_rcntd_Updating" OnDeleted="Obj_rcntd_Deleted" OnInserted="Obj_rcntd_Inserted" OnUpdated="Obj_rcntd_Updated">
        <DeleteParameters>
          <asp:Parameter Name="original_rcntd_gkey" Type="String" />
          <asp:Parameter Name="rcntd_gkey" Type="String" />
          <asp:Parameter Name="rcntd_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </DeleteParameters>
        <InsertParameters>
          <asp:Parameter Name="rcntd_BDCCN" Type="String" />
          <asp:Parameter Name="rcntd_BDDCX" Type="Decimal" />
          <asp:Parameter Name="rcntd_BDQTY" Type="Decimal" />
          <asp:Parameter Name="rcntd_BDRAT" Type="Decimal" />
          <asp:Parameter Name="rcntd_BDRMK" Type="String" />
          <asp:Parameter Name="rcntd_actkey" Type="String" />
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
          <asp:Parameter Name="original_rcntd_gkey" Type="String" />
          <asp:Parameter Name="rcntd_gkey" Type="String" />
          <asp:Parameter Name="rcntd_mkey" Type="String" />
          <asp:Parameter Name="rcntd_BDCCN" Type="String" />
          <asp:Parameter Name="rcntd_BDDCX" Type="Decimal" />
          <asp:Parameter Name="rcntd_BDQTY" Type="Decimal" />
          <asp:Parameter Name="rcntd_BDRAT" Type="Decimal" />
          <asp:Parameter Name="rcntd_BDRMK" Type="String" />
          <asp:Parameter Name="rcntd_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </UpdateParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_rcntr" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_rcntr" TypeName="DD2015_45.DAC_rcntr" OnSelecting="Obj_rcntr_Selecting" UpdateMethod="UpdateTable_rcntr" InsertMethod="InsertTable_rcntr" OnInserting="Obj_rcntr_Inserting" DeleteMethod="DeleteTable_rcntr" OnDeleting="Obj_rcntr_Deleting" OnUpdating="Obj_rcntr_Updating" OnUpdated="Obj_rcntr_Updated" OnDeleted="Obj_rcntr_Deleted" OnInserted="Obj_rcntr_Inserted">
        <DeleteParameters>
          <asp:Parameter Name="original_rcntr_gkey" Type="String" />
          <asp:Parameter Name="rcntr_gkey" Type="String" />
          <asp:Parameter Name="rcntr_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </DeleteParameters>
        <InsertParameters>
          <asp:Parameter Name="rcntr_BDCCN" Type="String" />
          <asp:Parameter Name="rcntr_BDAUR" Type="String" />
          <asp:Parameter Name="rcntr_BDAMT" Type="String" />
          <asp:Parameter Name="rcntr_BDEDT" Type="String" />
          <asp:Parameter Name="rcntr_BDINV" Type="String" />
          <asp:Parameter Name="rcntr_BDINT" Type="String" />
          <asp:Parameter Name="rcntr_BDRMK" Type="String" />
          <asp:Parameter Name="rcntr_actkey" Type="String" />
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
          <asp:Parameter Name="original_rcntr_gkey" Type="String" />
          <asp:Parameter Name="rcntr_gkey" Type="String" />
          <asp:Parameter Name="rcntr_mkey" Type="String" />
          <asp:Parameter Name="rcntr_BDCCN" Type="String" />
          <asp:Parameter Name="rcntr_BDAUR" Type="String" />
          <asp:Parameter Name="rcntr_BDAMT" Type="String" />
          <asp:Parameter Name="rcntr_BDEDT" Type="String" />
          <asp:Parameter Name="rcntr_BDINV" Type="String" />
          <asp:Parameter Name="rcntr_BDINT" Type="String" />
          <asp:Parameter Name="rcntr_BDRMK" Type="String" />
          <asp:Parameter Name="rcntr_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </UpdateParameters>
      </asp:ObjectDataSource>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
      <script type="text/javascript">
        var hh_company = document.all['ctl00$hh_Company'].value;
        var hh_rcntr_OldNum_Field = "<%= this.hh_rcntr_OldNum_Field.ClientID %>";
        var WebDataGrid_rcntd_ID = "<%= this.WebDataGrid_rcntd.ClientID %>";
        var WebDataGrid_rcntr_ID = "<%= this.WebDataGrid_rcntr.ClientID %>";
        var inputCount = 0;

        function di_Window_initialize(sender, e) {
          document.all[hh_rcntr_OldNum_Field].value = empty_field;
          sender.hide();
        }

        function di_Window_windowStateChanged(diWin, evntArgs) {
          var diWin = $find('<%= di_Window.ClientID %>');
          var state = diWin.get_windowState(); //1=Minimized 2=Maximized 3=Closed other= Restored
          var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
          var diWin_cmd = document.all[diWin_cmdField].value;
          if (state == 3) {
            if (diWin_cmd == "add_rcntr") {
              try {
                document.all[diWin_cmdField].value = "*"; //reset command
                webDataGrid_SetFieldValue("<%= this.WebDataGrid_rcntr.ClientID %>", 'rcntr_BDAUR', "");
                webDataGrid_RowAddFocus("<%= this.WebDataGrid_rcntr.ClientID %>", 'rcntr_BDAUR');
              }
              catch (e) {
                document.all[diWin_cmdField].value = "*"; //reset command
              }
            }
            else if (diWin_cmd == "mod_rcntr") {
              try {
                document.all[diWin_cmdField].value = "*"; //reset command
                var rcntr_OldNum = document.all[hh_rcntr_OldNum_Field].value;
                if (rcntr_OldNum != "" && rcntr_OldNum != empty_field) {
                  webDataGrid_SetFieldValue("<%= this.WebDataGrid_rcntr.ClientID %>", 'rcntr_BDAUR', rcntr_OldNum);
                }
                webDataGrid_ModRowfocus("<%= this.WebDataGrid_rcntr.ClientID %>", 'rcntr_BDAUR');
              }
              catch (e) {
                document.all[diWin_cmdField].value = "*"; //reset command
              }
            }

        }
      }

      function deleteRow(webDataGrid_ID) {
        var webDataGrid;
        if (webDataGrid_ID == 'WebDataGrid_rcntd') {
          webDataGrid = $find("<%= this.WebDataGrid_rcntd.ClientID %>");
            webDataGrid.get_rows().remove(webDataGrid.get_behaviors().get_activation().get_activeCell().get_row());
          }
          else if (webDataGrid_ID == 'WebDataGrid_rcntr') {
            webDataGrid = $find("<%= this.WebDataGrid_rcntr.ClientID %>");
              webDataGrid.get_rows().remove(webDataGrid.get_behaviors().get_activation().get_activeCell().get_row());
            }
        }

        function rcntd_CellEdit_EnteredEditMode(sender, e) {

        }

        function rcntd_CellEdit_ExitedEditMode(sender, e) {
          var cell = e.getCell();
          var value = cell.get_value();
          var row = cell.get_row();
          var rowIndex = row.get_index();
          var column = cell.get_column();
          var columnKey = column.get_key();
          //
          if (columnKey == "rcntd_BDRMK") {

          }
          //
        }

        function rcntd_Activation_ActiveCellChanging(webDataGrid, e) {
          var column = e.getNewActiveCell().get_column();
          if (column != null) {
            var columnKey = column.get_key();
            if (columnKey == "rcntd_BDCCN") {
              e.set_cancel(true);
            }
          }
          ///
        }

        function rcntr_CellEdit_EnteredEditMode(sender, e) {
          var cell = e.getCell();
          var value = cell.get_value();
          var row = cell.get_row();
          var rowIndex = row.get_index();
          var column = cell.get_column();
          var columnKey = column.get_key();
          var rcntr_BDAUR = row.get_cellByColumnKey("rcntr_BDAUR").get_value();
          if (columnKey == "rcntr_BDAUR") {
            //save old rbptn
            document.all[hh_rcntr_OldNum_Field].value = rcntr_BDAUR;
          }
          else {
            document.all[hh_rcntr_OldNum_Field].value = empty_field;
          }
        }

        function rcntr_CellEdit_ExitedEditMode(sender, e) {
          var cell = e.getCell();
          var value = cell.get_value();
          var row = cell.get_row();
          var rowIndex = row.get_index();
          var column = cell.get_column();
          var columnKey = column.get_key();
          //
          if (columnKey == "rcntr_BDAUR") {
            var rcntr_OldNum = document.all[hh_rcntr_OldNum_Field].value;
            var rcntr_BDAUR = row.get_cellByColumnKey("rcntr_BDAUR").get_value();
            rcntr_BDAUR = strtrim(rcntr_BDAUR);
            if (rcntr_BDAUR != rcntr_OldNum) {
              var iContEdit = '<%=st_ContentPlaceHolderEdit%>';
            var di_Window = '<%=di_Window.ClientID%>';
            var DataGrid_id = '<%=WebDataGrid_rcntr.ClientID%>';
            var di_OpenUrl = "../Dialog/Dialog_bdlr.aspx?iFunc=" + "rcntr" + "&oNewMod=mod" + "&oWindow_Id=" + di_Window + "&oDataGrid_id=" + DataGrid_id + "&iField=rcntr_BDAUR" + "&oField=baur_BCNAM"
            var di_Caption = cell.get_column().get_headerText();
            var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
            document.all[diWin_cmdField].value = "mod_rcntr";
            get_bdlr_grname('rcntr', iContEdit, "mod", "rcntr_BDAUR", "baur_BCNAM", "rcntr_BDINV", DataGrid_id, row, di_Window, di_OpenUrl, di_Caption, rcntr_BDAUR, rcntr_OldNum);
          }
        }
      }

      function rcntr_Activation_ActiveCellChanging(webDataGrid, e) {
        var column = e.getNewActiveCell().get_column();
        if (column != null) {
          var columnKey = column.get_key();
          if (columnKey == "baur_BCNAM") {

          }
        }
      }
      // 
      function WebDataGridView_AJAXResponse(grid, e) {
        if (e.get_gridResponseObject().Message)
          alert(e.get_gridResponseObject().Message);
        // 
      }
      //
      function webDataGrid_AddRowB() {
        var webtab = $find('<%=WebTabGrid.ClientID%>');
        var webindex = webtab.get_selectedIndex();
        //
        inputCount = inputCount + 1;
        if (webindex == 0) {
          var ar_row = new Array("","", "", null, "0", null, null, null, null, inputCount.toString(), inputCount.toString());
          webDataGrid_AddRow('<%=WebDataGrid_rcntr.ClientID%>', ar_row, "rcntr_BDAUR", 22)
          }
          else {
            var ar_row = new Array("","", 0, 0, 0, "", inputCount.toString(), inputCount.toString());
            webDataGrid_AddRow('<%=WebDataGrid_rcntd.ClientID%>', ar_row, "rcntd_BDQTY", 22)
          }
        }
        //
        function WebDataGrid_rcnta_Grid_DoubleClick(sender, eventArgs) {
          row = eventArgs.get_item().get_row();
          var mkey_a = row.get_cellByColumnKey("rcnta_mkey").get_value();
          //
          grid_ba = $find("<%= this.WebDataGrid_rcnta_ba.ClientID %>") //get the grid
          var ba_mkey = "";
          var grid_ba_sel_rows = grid_ba.get_behaviors().get_selection().get_selectedRows();
          var grid_ba_sel_row;
          var grid_ba_rows = grid_ba.get_rows();
          var grid_ba_row;
          for (var i = 0; i < grid_ba_rows.get_length() ; i++) {
            grid_ba_row = grid_ba_rows.get_row(i);
            ba_mkey = grid_ba_row.get_cellByColumnKey("rcnta_mkey").get_value();
            if (ba_mkey == mkey_a) {
              grid_ba_sel_rows.add(grid_ba_row);
              document.all["<%=st_ContentPlaceHolder%>" + "hh_fun_mkey"].value = ba_mkey;
            }
            else {
              grid_ba_sel_rows.remove(grid_ba_row);
            }
          }
          __doPostBack('<%=hh_fun_name.ClientID %>', 'showa' + '&' + mkey_a);
        }
        //
        function bt_Cancel_B_Click(oButton, oEvent) {
          inputCount = 0;
          document.all["<%=st_ContentPlaceHolder%>" + "btnUpdateCancel"].value = "1";
          oEvent.needPostBack = true;
        }
        //
        function bt_SAVE_B_Click(oButton, oEvent) {
          inputCount = 0;
          document.all["<%=st_ContentPlaceHolder%>" + "btnUpdateCancel"].value = "0";
          oEvent.needPostBack = true;
          return true;
        }
      </script>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
