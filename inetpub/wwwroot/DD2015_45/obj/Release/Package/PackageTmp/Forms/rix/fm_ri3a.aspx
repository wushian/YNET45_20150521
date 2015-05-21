<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_ri3a.aspx.cs" Inherits="DD2015_45.Forms.rix.fm_ri3a" %>

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
      <input id="hh_ri3b_OldPtn" type="hidden" name="hh_ri3b_OldPtn" runat="server" />
      <input id="di_Window_Command" type="hidden" name="di_Window_Command" runat="server" />
      <input id="btnAction" type="hidden" name="btnAction" runat="server" />
      <input id="btnUpdateCancel" type="hidden" name="btnUpdateCancel" value="" enableviewstate="false" runat="server" />
      <asp:Button ID="btnPost" runat="server" UseSubmitBehavior="False" Visible="false" />
      <asp:Panel ID="PanBtns" runat="server" Width="100%">
        <table>
          <tr>
            <td>
              <igtxt:WebImageButton ID="bt_CAN" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="C取消" ImageDirectory="../../images/" OnClick="bt_CAN_Click">
                <Appearance>
                  <Image Url="form_cancel.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_SAV" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="S存檔" ImageDirectory="../../images/" OnClick="bt_SAV_Click">
                <Appearance>
                  <Image Url="form_save.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_02" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="A新增" ImageDirectory="../../images/" OnClick="bt_02_Click">
                <Appearance>
                  <Image Url="form_new.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_03" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="I插入" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_new.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_04" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="M更正" ImageDirectory="../../images/" OnClick="bt_04_Click">
                <Appearance>
                  <Image Url="form_edit.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_05" runat="server" AutoSubmit="false" UseBrowserDefaults="False"
                Height="90%" Text="X刪除" ImageDirectory="../../images/" OnClick="bt_05_Click">
                <Appearance>
                  <Image Url="form_delete.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_06" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="O複製" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_copy.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_07" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="P列印" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_print.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_08" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="F查詢" ImageDirectory="../../images/" OnClick="bt_08_Click">
                <Appearance>
                  <Image Url="form_serch.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_09" runat="server" UseBrowserDefaults="False"
                Height="90%" Text="T轉單" ImageDirectory="../../images/">
                <Appearance>
                  <Image Url="form_copy.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:WebImageButton>
            </td>
            <td>
              <igtxt:WebImageButton ID="bt_10" runat="server" UseBrowserDefaults="False"
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
              <igtxt:WebImageButton ID="bt_QUT" runat="server" UseBrowserDefaults="False"
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
      <ig:WebTab ID="WebTab_form" runat="server" Height="600px" Width="1200px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px" SelectedIndex="1">
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
                              <asp:Panel ID="PanSerComm" runat="server" Width="1000px" Height="150px">
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
                                            <asp:Label ID="lb_ri3a_RIDAT_s1" runat="server" Text="銷貨日期"></asp:Label>
                                          </td>
                                          <td>
                                            <ig:WebDatePicker ID="tx_ri3a_RIDAT_s1" Width="100px" CssClass="Office2010Blue" runat="server"></ig:WebDatePicker>
                                          </td>
                                          <td>
                                            <asp:Label ID="lb_ri3a_RIDAT_s2" runat="server" Text="～"></asp:Label>
                                          </td>
                                          <td>
                                            <ig:WebDatePicker ID="tx_ri3a_RIDAT_s2" Width="100px" CssClass="Office2010Blue" runat="server"></ig:WebDatePicker>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <asp:Label ID="lb_ri3a_RINUM_s" runat="server" Text="經銷編號"></asp:Label>
                                      <asp:TextBox ID="tx_ri3a_RINUM_s" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                      <asp:TextBox ID="tx_bdlr_RINUM_s" Width="62px" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <asp:Label ID="lb_ri3a_RICNS_s" runat="server" Text="分倉編號"></asp:Label>
                                      <asp:DropDownList ID="dr_ri3a_RICNS_s" Width="134px" runat="server" />
                                      <asp:TextBox ID="tx_ri3a_RICNS_s" Width="0px" Visible="false" runat="server" />
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <asp:Label ID="lb_ri3a_RIREN_s" runat="server" Text="銷貨單號"></asp:Label>
                                      <asp:TextBox ID="tx_ri3a_RIREN_s" Width="130px" runat="server" MaxLength="30"></asp:TextBox>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td></td>
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
                <ig:WebDataGrid ID="WebDataGrid_ri3a" runat="server"
                  EnableAjax="False" EnableDataViewState="True"
                  Width="1100px" AutoGenerateColumns="False" DataKeyFields="ri3a_gkey">
                  <Columns>
                    <ig:BoundDataField DataFieldName="ri3a_RIREN" Key="ri3a_RIREN" Width="100px">
                      <Header Text="ri3a_RIREN">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ri3a_RIDAT" Key="ri3a_RIDAT" Width="80px">
                      <Header Text="ri3a_RIDAT">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_RINUM" Key="bdlr_RINUM" Width="80px">
                      <Header Text="bdlr_RINUM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ri3a_RINAM" Key="ri3a_RINAM" Width="100px">
                      <Header Text="ri3a_RINAM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ri3a_RIGSM" Key="ri3a_RIGSM" Width="100px">
                      <Header Text="ri3a_RIGSM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ri3a_RITEL" Key="ri3a_RITEL" Width="100px">
                      <Header Text="ri3a_RITEL">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ri3a_RIZIP" Key="ri3a_RIZIP" Width="60px">
                      <Header Text="ri3a_RIZIP">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ri3a_RIADR" Key="ri3a_RIADR" Width="300px">
                      <Header Text="ri3a_RIADR">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ri3a_gkey" Key="ri3a_gkey" Hidden="true" HtmlEncode="true">
                      <Header Text="ri3a_gkey">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ri3a_mkey" Key="ri3a_mkey" Hidden="true" HtmlEncode="true">
                      <Header Text="ri3a_mkey">
                      </Header>
                    </ig:BoundDataField>
                  </Columns>
                  <ClientEvents DoubleClick="WebDataGrid_ri3a_Grid_DoubleClick" />
                  <Behaviors>
                    <ig:Selection CellClickAction="Row" CellSelectType="None" RowSelectType="Single">
                    </ig:Selection>
                    <ig:RowSelectors>
                    </ig:RowSelectors>
                    <ig:Sorting SortingMode="Multi">
                    </ig:Sorting>
                    <ig:Paging>
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
                  <td rowspan="2" class="tdtop">
                    <asp:Panel ID="PanEdtLeft" runat="server" Width="250px" BorderStyle="Inset">
                      <ig:WebDataGrid ID="WebDataGrid_ri3ba" runat="server"
                        EnableAjax="False" EnableDataViewState="True"
                        Width="240px" AutoGenerateColumns="False" DataKeyFields="ri3a_gkey" OnRowSelectionChanged="WebDataGrid_ri3ba_RowSelectionChanged">
                        <Columns>
                          <ig:TemplateDataField Key="ri3a_hidden" Hidden="true">
                            <ItemTemplate>
                              <input id="tx_ri3a_gkey02" type="hidden" name="tx_ri3a_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "ri3a_gkey").ToString() %>' runat="server" />
                              <input id="tx_ri3a_mkey02" type="hidden" name="tx_ri3a_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "ri3a_mkey").ToString() %>' runat="server" />
                              <input id="tx_ri3a_RINUM02" type="hidden" name="tx_ri3a_RINUM02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "ri3a_RINUM").ToString() %>' runat="server" />
                            </ItemTemplate>
                            <Header Text="ri3a_hidden" />
                          </ig:TemplateDataField>
                          <ig:BoundDataField DataFieldName="ri3a_RIREN" Key="ri3a_RIREN" Width="80px">
                            <Header Text="ri3a_RIREN">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3a_RIDAT" Key="ri3a_RIDAT" Width="70px">
                            <Header Text="ri3a_RIDAT">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3a_gkey" Key="ri3a_gkey" Hidden="true" HtmlEncode="true">
                            <Header Text="ri3a_gkey">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3a_mkey" Key="ri3a_mkey" Hidden="true" HtmlEncode="true">
                            <Header Text="ri3a_mkey">
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
                    <asp:Panel ID="PanEdtRightTop" runat="server" Width="900px" BorderStyle="Inset">
                      <table>
                        <tr>
                          <td>&nbsp;
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_ri3a_RIREN" runat="server" Text="銷貨單號"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RIREN" Width="120px" runat="server" MaxLength="30"></asp:TextBox>
                          </td>
                          <td>
                            <asp:Label ID="lb_ri3a_RIMEN" runat="server" Text="開單人員"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RIMEN" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_es101_RIMEN" Width="60px" runat="server" MaxLength="10" ReadOnly="true" TabIndex="-1"></asp:TextBox>
                          </td>
                          <td>
                            <table>
                              <tr>
                                <td>
                                  <asp:Label ID="lb_ri3a_RIDAT" runat="server" Text="銷貨日期"></asp:Label>
                                </td>
                                <td>
                                  <ig:WebDatePicker ID="tx_ri3a_RIDAT" Width="120px" runat="server" DisplayModeFormat="d" Font-Size="Medium"></ig:WebDatePicker>
                                </td>
                              </tr>
                            </table>
                          </td>
                          <td>
                            <asp:Label ID="lb_ri3a_RINUM" runat="server" Text="經銷編號"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RINUM" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_bdlr_RINUM" Width="100px" runat="server" MaxLength="10" ReadOnly="true" TabIndex="-1"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_ri3a_RICNS" runat="server" Text="分倉編號"></asp:Label>
                            <asp:DropDownList ID="dr_ri3a_RICNS" Width="120px" runat="server" />
                            <asp:TextBox ID="tx_ri3a_RICNS" Width="0px" Visible="false" runat="server" />
                          </td>
                          <td>
                            <asp:Label ID="lb_ri3a_RIEDT" runat="server" Text="帳款日期"></asp:Label>
                            <asp:DropDownList ID="dr_ri3a_RIEDT" Width="120px" runat="server" />
                            <asp:TextBox ID="tx_ri3a_RIEDT" Width="0px" Visible="false" runat="server" />
                          </td>
                          <td>
                            <asp:Label ID="lb_ri3a_RISAL" runat="server" Text="業務人員"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RISAL" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="tx_es101_RISAL" Width="60px" runat="server" MaxLength="20" ReadOnly="true" TabIndex="-1"></asp:TextBox>
                          </td>
                          <td>
                            <asp:CheckBox ID="ck_ri3a_RICK1" Width="100px" runat="server" Text="計算版稅" />
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <asp:Label ID="lb_ri3a_RICUS" runat="server" Text="會員編號"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RICUS" Width="60px" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:TextBox ID="tx_bcvw_RICUS" Width="60px" runat="server" MaxLength="20" ReadOnly="true" TabIndex="-1"></asp:TextBox>
                          </td>
                          <td>
                            <asp:Label ID="lb_ri3a_RINAM" runat="server" Text="收貨人名"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RINAM" Width="120px" runat="server" MaxLength="100"></asp:TextBox>
                          </td>
                          <td>
                            <asp:Label ID="lb_ri3a_RIGSM" runat="server" Text="手機號碼"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RIGSM" Width="120px" runat="server" MaxLength="40"></asp:TextBox>
                          </td>
                          <td>
                            <asp:Label ID="lb_ri3a_RITEL" runat="server" Text="連絡電話"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RITEL" Width="180px" runat="server" MaxLength="40"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="4">
                            <asp:Label ID="lb_ri3a_RIZIP" runat="server" Text="郵遞區號"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RIZIP" Width="100px" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:Label ID="lb_ri3a_RIADR" runat="server" Text="收貨地址"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RIADR" Width="620px" runat="server" MaxLength="200"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="4">
                            <asp:Label ID="lb_ri3a_RIRTP" runat="server" Text="運送方式"></asp:Label>
                            <asp:DropDownList ID="dr_ri3a_RIRTP" Width="100px" runat="server" />
                            <asp:TextBox ID="tx_ri3a_RIRTP" Width="0px" Visible="false" runat="server" />
                            <asp:Label ID="lb_ri3a_RITME" runat="server" Text="收貨時段"></asp:Label>
                            <asp:DropDownList ID="dr_ri3a_RITME" Width="100px" runat="server" />
                            <asp:TextBox ID="tx_ri3a_RITME" Width="0px" Visible="false" runat="server" />
                            <asp:Label ID="lb_ri3a_RIIVT" runat="server" Text="發票方式"></asp:Label>
                            <asp:DropDownList ID="dr_ri3a_RIIVT" Width="100px" runat="server" />
                            <asp:TextBox ID="tx_ri3a_RIIVT" Width="0px" Visible="false" runat="server" />
                            <asp:Label ID="lb_ri3a_RIIVN" runat="server" Text="發票號碼"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RIIVN" Width="100px" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:Label ID="lb_ri3a_RIIVV" runat="server" Text="統一編號"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RIIVV" Width="100px" runat="server" MaxLength="20"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="4">
                            <asp:Label ID="lb_ri3a_RIRMK" runat="server" Text="備註說明"></asp:Label>
                            <asp:TextBox ID="tx_ri3a_RIRMK" Width="800px" runat="server" MaxLength="200"></asp:TextBox>
                          </td>
                        </tr>
                      </table>
                    </asp:Panel>
                  </td>
                </tr>
                <tr>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:Label ID="lb_ri3a_RITXP" runat="server" Text="計稅方式"></asp:Label>
                          <asp:DropDownList ID="dr_ri3a_RITXP" Width="100px" runat="server" />
                          <asp:TextBox ID="tx_ri3a_RITXP" Width="0px" Visible="false" runat="server" />
                          <asp:Label ID="lb_ri3a_RIRMN" runat="server" Text="運費金額"></asp:Label>
                          <ig:WebNumericEditor ID="tx_ri3a_RIRMN" Width="100px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          <asp:Label ID="lb_ri3a_RIDMN" runat="server" Text="貨品金額"></asp:Label>
                          <ig:WebNumericEditor ID="tx_ri3a_RIDMN" Width="100px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          <asp:Label ID="lb_ri3a_RITXN" runat="server" Text="稅　　額"></asp:Label>
                          <ig:WebNumericEditor ID="tx_ri3a_RITXN" Width="100px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                          <asp:Label ID="lb_ri3a_RITOL" runat="server" Text="合計金額"></asp:Label>
                          <ig:WebNumericEditor ID="tx_ri3a_RITOL" Width="100px" runat="server" MinDecimalPlaces="2" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:Label ID="lb_ErrorMessageB" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
                        </td>
                      </tr>
                    </table>
                    <asp:Panel ID="PanEdtRightDown" runat="server" Width="900px" BorderStyle="Inset">
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
                            <igtxt:WebImageButton ID="bt_Cancel_B" Text="O取消" AccessKey="O" AutoSubmit="False" ImageDirectory="../../images/" runat="server" OnClick="bt_Cancel_B_Click">
                              <ClientSideEvents Click="bt_Cancel_B_Click" />
                              <Appearance>
                                <Image Url="undo_down.gif"></Image>
                              </Appearance>
                            </igtxt:WebImageButton>
                          </td>
                          <td>
                            <igtxt:WebImageButton ID="bt_MOD_B" Text="W更正" AccessKey="W" ImageDirectory="../../images/" runat="server" OnClick="bt_MOD_B_Click">
                              <ClientSideEvents Click="bt_SAVE_B_Click" />
                              <Appearance>
                                <Image Url="edit_down.gif"></Image>
                              </Appearance>
                            </igtxt:WebImageButton>
                          </td>
                        </tr>
                      </table>
                      <ig:WebDataGrid ID="WebDataGrid_ri3b" runat="server"
                        Width="890px" AutoGenerateColumns="False" DataKeyFields="ri3b_gkey" OnRowSelectionChanged="WebDataGrid_ri3b_RowSelectionChanged" OnRowAdded="WebDataGrid_ri3b_RowAdded" OnRowAdding="WebDataGrid_ri3b_RowAdding" OnRowUpdated="WebDataGrid_ri3b_RowUpdated" OnRowUpdating="WebDataGrid_ri3b_RowUpdating" OnRowsDeleting="WebDataGrid_ri3b_RowsDeleting" Font-Size="Medium">
                        <Columns>
                          <ig:TemplateDataField Key="EDIT" Width="22px">
                            <Header Text="Del" />
                            <ItemTemplate>
                              <asp:ImageButton ID="Delete" OnClientClick="deleteRow('WebDataGrid_ri3b'); return false" ImageUrl="~/images/delete_down.gif"
                                ToolTip="Delete" runat="server"></asp:ImageButton>
                            </ItemTemplate>
                          </ig:TemplateDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBITM" Key="ri3b_RBITM" Width="20px" DataType="System.Int32">
                            <Header Text="ri3b_RBITM">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBPTN" Key="ri3b_RBPTN" HtmlEncode="true" Width="100px">
                            <Header Text="ri3b_RBPTN">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBNAM" Key="ri3b_RBNAM" Width="160px">
                            <Header Text="ri3b_RBNAM">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBCLA" Key="ri3b_RBCLA" Width="80px">
                            <Header Text="ri3b_RBCLA">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBUNI" Key="ri3b_RBUNI" Width="30px">
                            <Header Text="ri3b_RBUNI">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBUNIN" Key="ri3b_RBUNIN" Width="30px">
                            <Header Text="ri3b_RBUNI">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBQTY" Key="ri3b_RBQTY" Width="30px" DataType="System.Int32" DataFormatString="{0:N0}" CssClass="txRightWdg">
                            <Header Text="ri3b_RBQTY">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBDPC" Key="ri3b_RBDPC" Width="40px" CssClass="txRightWdg" DataFormatString="{0:N0}" DataType="System.Decimal">
                            <Header Text="ri3b_RBDPC">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBDCX" Key="ri3b_RBDCX" Width="40px" CssClass="txRightWdg" DataFormatString="{0:N2}" DataType="System.Decimal">
                            <Header Text="ri3b_RBDCX">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBUPC" Key="ri3b_RBUPC" Width="50px" CssClass="txRightWdg" DataFormatString="{0:N2}" DataType="System.Decimal">
                            <Header Text="ri3b_RBUPC">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBAMT" Key="ri3b_RBAMT" Width="50px" CssClass="txRightWdg" DataFormatString="{0:N2}" DataType="System.Decimal">
                            <Header Text="ri3b_RBAMT">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBRMK" Key="ri3b_RBRMK" Width="100px" EnableMultiline="True">
                            <Header Text="ri3b_RBRMK">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_RBREN" Key="ri3b_RBREN" Hidden="true" HtmlEncode="true">
                            <Header Text="ri3b_RBREN">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_gkey" Key="ri3b_gkey" Hidden="true" HtmlEncode="true">
                            <Header Text="ri3b_gkey">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="ri3b_mkey" Key="ri3b_mkey" Hidden="true" HtmlEncode="true">
                            <Header Text="ri3b_mkey">
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
                            <Behaviors>
                              <ig:RowDeleting>
                              </ig:RowDeleting>
                              <ig:CellEditing>
                                <ColumnSettings>
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBITM" EditorID="WebDataGrid_Provider_ri3b_RBITM" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBPTN" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBNAM" ReadOnly="True" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBCLA" ReadOnly="True" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBUNI" EditorID="WebDataGrid_Provider_ri3b_RBUNI" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBUNIN" ReadOnly="True" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBQTY" EditorID="WebDataGrid_Provider_ri3b_RBQTY" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBDPC" ReadOnly="True" EditorID="WebDataGrid_Provider_ri3b_RBDPC" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBDCX" EditorID="WebDataGrid_Provider_ri3b_RBDCX" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBUPC" EditorID="WebDataGrid_Provider_ri3b_RBUPC" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBAMT" EditorID="WebDataGrid_Provider_ri3b_RBAMT" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBRMK" EditorID="WebDataGrid_Provider_ri3b_RBRMK" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_RBREN" ReadOnly="True" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_gkey" ReadOnly="True" />
                                  <ig:EditingColumnSetting ColumnKey="ri3b_mkey" ReadOnly="True" />
                                </ColumnSettings>
                                <CellEditingClientEvents ExitedEditMode="ri3b_CellEdit_ExitedEditMode" EnteredEditMode="ri3b_CellEdit_EnteredEditMode" ExitingEditMode="ri3b_CellEdit_ExitingEditMode" />
                                <EditModeActions EnableOnActive="True" EnableOnKeyPress="True" />
                              </ig:CellEditing>
                            </Behaviors>
                          </ig:EditingCore>
                          <ig:Activation>
                            <ActivationClientEvents ActiveCellChanging="ri3b_Activation_ActiveCellChanging" />
                          </ig:Activation>
                          <ig:Paging>
                          </ig:Paging>
                        </Behaviors>
                        <EditorProviders>
                          <ig:DropDownProvider ID="WebDataGrid_Provider_ri3b_RBUNI">
                            <EditorControl ClientIDMode="Predictable" DataSourceID="Obj_ri3b_RBUNI" DropDownContainerMaxHeight="200px" EnableAnimations="False" EnableDropDownAsChild="False" TextField="pdbpuni_BKNAM" ValueField="pdbpuni_BKNUM" DisplayMode="DropDownList">
                              <DropDownItemBinding TextField="pdbpuni_BKNAM" ValueField="pdbpuni_BKNUM" />
                            </EditorControl>
                          </ig:DropDownProvider>
                          <ig:NumericEditorProvider ID="WebDataGrid_Provider_ri3b_RBITM">
                            <EditorControl ClientIDMode="Predictable">
                            </EditorControl>
                          </ig:NumericEditorProvider>
                          <ig:NumericEditorProvider ID="WebDataGrid_Provider_ri3b_RBDPC">
                            <EditorControl ClientIDMode="Predictable">
                            </EditorControl>
                          </ig:NumericEditorProvider>
                          <ig:NumericEditorProvider ID="WebDataGrid_Provider_ri3b_RBQTY">
                            <EditorControl ClientIDMode="Predictable">
                            </EditorControl>
                          </ig:NumericEditorProvider>
                          <ig:NumericEditorProvider ID="WebDataGrid_Provider_ri3b_RBDCX">
                            <EditorControl ClientIDMode="Predictable" MinDecimalPlaces="2">
                            </EditorControl>
                          </ig:NumericEditorProvider>
                          <ig:NumericEditorProvider ID="WebDataGrid_Provider_ri3b_RBUPC">
                            <EditorControl ClientIDMode="Predictable" MinDecimalPlaces="2">
                            </EditorControl>
                          </ig:NumericEditorProvider>
                          <ig:NumericEditorProvider ID="WebDataGrid_Provider_ri3b_RBAMT">
                            <EditorControl ClientIDMode="Predictable">
                            </EditorControl>
                          </ig:NumericEditorProvider>
                          <ig:TextBoxProvider ID="WebDataGrid_Provider_ri3b_RBRMK">
                            <EditorControl ClientIDMode="Predictable" Height="80px" TextMode="MultiLine" Width="80px"></EditorControl>
                          </ig:TextBoxProvider>
                        </EditorProviders>
                        <ClientEvents AJAXResponse="WebDataGridView_AJAXResponse" />
                      </ig:WebDataGrid>
                    </asp:Panel>
                    <asp:Literal ID="li_Focus" runat="server"></asp:Literal>
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
      <asp:ObjectDataSource ID="Obj_ri3a" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_ri3a" TypeName="DD2015_45.DAC_ri3a" OnSelecting="Obj_ri3a_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_ri3ba" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_ri3ba" TypeName="DD2015_45.DAC_ri3a" OnSelecting="Obj_ri3ba_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_ri3b" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_ri3b" TypeName="DD2015_45.DAC_ri3b" OnSelecting="Obj_ri3b_Selecting" UpdateMethod="UpdateTable_ri3b" InsertMethod="InsertTable_ri3b" OnInserting="Obj_ri3b_Inserting" DeleteMethod="DeleteTable_ri3b" OnDeleting="Obj_ri3b_Deleting" OnUpdating="Obj_ri3b_Updating" OnDeleted="Obj_ri3b_Deleted" OnInserted="Obj_ri3b_Inserted" OnUpdated="Obj_ri3b_Updated">
        <DeleteParameters>
          <asp:Parameter Name="original_ri3b_gkey" Type="String" />
          <asp:Parameter Name="ri3b_gkey" Type="String" />
          <asp:Parameter Name="ri3b_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </DeleteParameters>
        <InsertParameters>
          <asp:Parameter Name="ri3b_RBITM" Type="Int32" />
          <asp:Parameter Name="ri3b_RBREN" Type="String" />
          <asp:Parameter Name="ri3b_RBPTN" Type="String" />
          <asp:Parameter Name="ri3b_RBUNI" Type="String" />
          <asp:Parameter Name="ri3b_RBQTY" Type="Decimal" />
          <asp:Parameter Name="ri3b_RBUPC" Type="Decimal" />
          <asp:Parameter Name="ri3b_RBAMT" Type="Decimal" />
          <asp:Parameter Name="ri3b_RBRMK" Type="String" />
          <asp:Parameter Name="ri3b_actkey" Type="String" />
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
          <asp:Parameter Name="original_ri3b_gkey" Type="String" />
          <asp:Parameter Name="ri3b_gkey" Type="String" />
          <asp:Parameter Name="ri3b_mkey" Type="String" />
          <asp:Parameter Name="ri3b_RBITM" Type="Int32" />
          <asp:Parameter Name="ri3b_RBREN" Type="String" />
          <asp:Parameter Name="ri3b_RBPTN" Type="String" />
          <asp:Parameter Name="ri3b_RBUNI" Type="String" />
          <asp:Parameter Name="ri3b_RBQTY" Type="Decimal" />
          <asp:Parameter Name="ri3b_RBUPC" Type="Decimal" />
          <asp:Parameter Name="ri3b_RBAMT" Type="Decimal" />
          <asp:Parameter Name="ri3b_RBRMK" Type="String" />
          <asp:Parameter Name="ri3b_actkey" Type="String" />
          <asp:Parameter Name="UserGkey" Type="String" />
        </UpdateParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_ri3b_RBUNI" runat="server" OldValuesParameterFormatString="original_{0}" TypeName="DD2015_45.DAC_common" OnSelecting="Obj_ri3b_RBUNI_Selecting" SelectMethod="SelectDropsownFromTable_pdbpuni">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
      <script type="text/javascript">
        var hh_company = document.all['ctl00$hh_Company'].value;
        var hh_ri3b_OldPtn_Field = "<%= this.hh_ri3b_OldPtn.ClientID %>";
        var WebDataGrid_ri3b_ID = "<%= this.WebDataGrid_ri3b.ClientID %>";
        var inputCount = 0;
        var empty_field = "$*$**";

        function di_Window_initialize(sender, e) {
          //init
          document.all[hh_ri3b_OldPtn_Field].value = empty_field;
          sender.hide();
        }

        function di_Window_windowStateChanged(diWin, evntArgs) {
          var diWin = $find('<%= di_Window.ClientID %>');
          var state = diWin.get_windowState(); //1=Minimized 2=Maximized 3=Closed other= Restored
          var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
          var diWin_cmd = document.all[diWin_cmdField].value;
          if (state == 3) {
            alert("close");
            if (diWin_cmd == "add_ri3b") {
              try {
                document.all[diWin_cmdField].value = "*"; //reset command
                webDataGrid_RowAddFocus("<%= this.WebDataGrid_ri3b.ClientID %>", 'ri3b_RBPTN');
              }
              catch (e) {
                document.all[diWin_cmdField].value = "*"; //reset command
              }
            }
            else if (diWin_cmd == "mod_ri3b") {
              try {
                document.all[diWin_cmdField].value = "*"; //reset command
                var ri3b_OldPtn = document.all[hh_ri3b_OldPtn_Field].value;
                if (ri3b_OldPtn != "" && ri3b_OldPtn != empty_field) {
                  webDataGrid_SetFieldValue("<%= this.WebDataGrid_ri3b.ClientID %>", 'ri3b_RBPTN', ri3b_OldPtn);
                }
                webDataGrid_ModRowfocus("<%= this.WebDataGrid_ri3b.ClientID %>", 'ri3b_RBPTN');
              }
              catch (e) {
                document.all[diWin_cmdField].value = "*"; //reset command
              }
            }

        }
      }

      function ri3b_CellEdit_EnteredEditMode(sender, e) {
        var cell = e.getCell();
        var value = cell.get_value();
        var row = cell.get_row();
        var rowIndex = row.get_index();
        var column = cell.get_column();
        var columnKey = column.get_key();
        var ri3b_RBPTN = row.get_cellByColumnKey("ri3b_RBPTN").get_value();
        if (columnKey == "ri3b_RBPTN") {
          //save old rbptn
          if ((ri3b_RBPTN == null) || (ri3b_RBPTN == "")) {
            document.all[hh_ri3b_OldPtn_Field].value = empty_field;
          }
          else {
            document.all[hh_ri3b_OldPtn_Field].value = ri3b_RBPTN;
          }
        }
        else {
          document.all[hh_ri3b_OldPtn_Field].value = empty_field;
        }
      }
      //
      function ri3b_CellEdit_ExitingEditMode(sender, e) {
        var cell = e.getCell();
        var column = cell.get_column();
        var columnKey = column.get_key();
        //
        if (columnKey == "ri3b_RBRMK") {
          if (e.get_browserEvent().keyCode == 13) {
            e.set_cancel(true);
          }
        }

      }
      //
      function ri3b_CellEdit_ExitedEditMode(sender, e) {
        var cell = e.getCell();
        var value = cell.get_value();
        var row = cell.get_row();
        var rowIndex = row.get_index();
        var column = cell.get_column();
        var columnKey = column.get_key();
        //
        var ri3b_CUS_Field = '<%=tx_ri3a_RINUM.ClientID %>';
        var ri3b_CUS = document.all[ri3b_CUS_Field].value;
        var ri3b_DAT = '<%=tx_ri3a_RIDAT.Text%>';
        //
        var ri3b_RBPTN = row.get_cellByColumnKey("ri3b_RBPTN").get_value();
        var ri3b_RBDPC = row.get_cellByColumnKey("ri3b_RBDPC").get_value();
        var ri3b_RBQTY = row.get_cellByColumnKey("ri3b_RBQTY").get_value();
        var ri3b_RBDCX = row.get_cellByColumnKey("ri3b_RBDCX").get_value();
        var ri3b_RBUPC = row.get_cellByColumnKey("ri3b_RBUPC").get_value();
        var ri3b_RBAMT = row.get_cellByColumnKey("ri3b_RBAMT").get_value();
        var temp_OldPtn = document.all[hh_ri3b_OldPtn_Field].value;
        //
        if (columnKey == "ri3b_RBPTN") {
          if ((ri3b_RBPTN != null) && (ri3b_RBPTN != "")) {
            var ri3b_OldPtn = document.all[hh_ri3b_OldPtn_Field].value;
            //if (ri3b_RBPTN != ri3b_OldPtn) {
            var iContEdit = '<%=st_ContentPlaceHolderEdit%>';
            var di_Window = '<%=di_Window.ClientID%>';
            var DataGrid_id = '<%=WebDataGrid_ri3b.ClientID%>';
            var di_OpenUrl = "../Dialog/Dialog_bpud.aspx?iFunc=" + "ri3b" + "&oNewMod=mod" + "&oWindow_Id=" + di_Window + "&oDataGrid_id=" + DataGrid_id + "&iField=ri3b_RBPTN" + "&oField=ri3b_RBNAM"
            var di_Caption = cell.get_column().get_headerText();
            //
            var diWin_cmdField = '<%= di_Window_Command.ClientID %>';
            document.all[diWin_cmdField].value = "mod_ri3b";
            get_bpud_grname('ri3b', iContEdit, "mod", "ri3b_RBPTN", "ri3b_RBQTY", DataGrid_id, row, di_Window, di_OpenUrl, di_Caption, ri3b_RBPTN, ri3b_OldPtn, ri3b_CUS, ri3b_DAT);
            //}
          }
          else if (temp_OldPtn != empty_field) {
            row.get_cellByColumnKey("ri3b_RBPTN").set_value(temp_OldPtn);
          }
        }
        else if (columnKey == "ri3b_RBDCX") {
          //折數改單價,單價不更新折數,才不會loop
          if (ri3b_RBDPC == 0) {
            row.get_cellByColumnKey("ri3b_RBDCX").set_value("100");
          }
          else {
            ri3b_RBUPC = Math.round(ri3b_RBDPC * ri3b_RBDCX) / 100;
            ri3b_RBAMT = ri3b_RBQTY * ri3b_RBUPC;
            row.get_cellByColumnKey("ri3b_RBUPC").set_value(ri3b_RBUPC);
            row.get_cellByColumnKey("ri3b_RBAMT").set_value(ri3b_RBAMT);
          }
        }
        else if (columnKey == "ri3b_RBUPC") {
          //折數改單價,單價不更新折數,才不會loop
          if (ri3b_RBDPC == 0) {
            row.get_cellByColumnKey("ri3b_RBDCX").set_value("100");
          }
          else {
            ri3b_RBDCX = Math.round(ri3b_RBUPC / ri3b_RBDPC * 100 * 100) / 100;
            ri3b_RBAMT = ri3b_RBQTY * ri3b_RBUPC;
            row.get_cellByColumnKey("ri3b_RBAMT").set_value(ri3b_RBAMT);
          }
        }
        else if ((columnKey == "ri3b_RBQTY")) {
          ri3b_RBAMT = ri3b_RBQTY * ri3b_RBUPC;
          row.get_cellByColumnKey("ri3b_RBAMT").set_value(ri3b_RBAMT);
        }
      }
      //

      function ri3b_Activation_ActiveCellChanging(webDataGrid, e) {
        var column = e.getNewActiveCell().get_column();
        if (column != null) {
          var columnKey = column.get_key();
          var index = e.getNewActiveCell().get_row().get_index();
          if ((columnKey == "ri3b_RBCLA")) {
            e.set_cancel(true);
            if (index == 1) {
              // edit cell
              var cell = webDataGrid.get_rows().get_row(index).get_cellByColumnKey("ri3b_RBDCX");
              webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
            }
            else {
              // add cell
              var row = e.getNewActiveCell().get_row();
              var cell = row.get_cellByColumnKey("ri3b_RBDCX");
              webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
            }
          }
          else if (columnKey == "ri3b_RBDPC") {
            e.set_cancel(true);
            if (index == 1) {
              // edit cell
              if (hh_company == "PDM") {
                var cell = webDataGrid.get_rows().get_row(index).get_cellByColumnKey("ri3b_RBDCX");
              }
              else {
                var cell = webDataGrid.get_rows().get_row(index).get_cellByColumnKey("ri3b_RBUPC");
              }
              webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
            }
            else {
              // add cell
              var row = e.getNewActiveCell().get_row();
              if (hh_company == "PDM") {
                var cell = row.get_cellByColumnKey("ri3b_RBDCX");
              }
              else {
                var cell = row.get_cellByColumnKey("ri3b_RBUPC");
              }
              webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
            }
          }
        }
      }

      function WebDataGridView_AJAXResponse(grid, e) {
        if (e.get_gridResponseObject().Message)
          alert(e.get_gridResponseObject().Message);
      }
      //
      function WebDataGrid_ri3a_Grid_DoubleClick(sender, eventArgs) {
        var getrow = false;
        try {
          row = eventArgs.get_item().get_row();
          getrow = true;
        }
        catch (e) {
          getrow = false;
        }
        if (getrow == true) {
          var mkey_a = row.get_cellByColumnKey("ri3a_mkey").get_value();
          //
          grid_ba = $find("<%= this.WebDataGrid_ri3a.ClientID %>") //get the grid
          var ba_mkey = "";
          var grid_ba_sel_rows = grid_ba.get_behaviors().get_selection().get_selectedRows();
          var grid_ba_sel_row;
          var grid_ba_rows = grid_ba.get_rows();
          var grid_ba_row;
          for (var i = 0; i < grid_ba_rows.get_length() ; i++) {
            grid_ba_row = grid_ba_rows.get_row(i);
            ba_mkey = grid_ba_row.get_cellByColumnKey("ri3a_mkey").get_value();
            if (ba_mkey == mkey_a) {
              grid_ba_sel_rows.add(grid_ba_row);
              document.all["<%=st_ContentPlaceHolder%>" + "hh_fun_mkey"].value = ba_mkey;
            }
            else {
              grid_ba_sel_rows.remove(grid_ba_row);
            }
          }
          __doPostBack('<%=btnAction.ClientID %>', 'showa' + '&' + mkey_a);
        }
      }

      //
      function webDataGrid_AddRowB() {
        inputCount = inputCount + 1;
        var ar_row = new Array("", null, "", null, null, null, null, "1", "0", "100", "0", "0", "", "", inputCount.toString(), inputCount.toString());
        webDataGrid_AddRow('<%=WebDataGrid_ri3b.ClientID%>', ar_row, "ri3b_RBPTN", 22)
      }
      //
      function deleteRow(webDataGrid_ID) {
        var webDataGrid;
        if (webDataGrid_ID == 'WebDataGrid_ri3b') {
          webDataGrid = $find("<%= this.WebDataGrid_ri3b.ClientID %>");
          webDataGrid.get_rows().remove(webDataGrid.get_behaviors().get_activation().get_activeCell().get_row());
        }
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
