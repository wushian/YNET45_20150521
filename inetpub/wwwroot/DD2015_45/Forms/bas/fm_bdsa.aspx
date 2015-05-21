<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_bdsa.aspx.cs" Inherits="DD2015_45.Forms.bas.fm_bdsa" %>

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
      <asp:Button ID="btnAction" runat="server" Visible="false" />
      <asp:Button ID="btnPost" runat="server" UseSubmitBehavior="False" Visible="false" />
      <asp:Panel ID="PanBtns" runat="server" Width="1000px">
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
        <table>
          <tr>
            <td>
              <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
            </td>
          </tr>
        </table>
      </asp:Panel>
      <ig:WebTab ID="WebTab_form" runat="server" Height="800px" Width="1200px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px" SelectedIndex="1">
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
                                      <asp:Label ID="lb_bdlr_BDNUM_s1" runat="server" Text="編　　號"></asp:Label>
                                      <asp:TextBox ID="tx_bdlr_BDNUM_s1" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <asp:Label ID="lb_bdlr_BDNAM_s1" runat="server" Text="名　　稱"></asp:Label>
                                      <asp:TextBox ID="tx_bdlr_BDNAM_s1" Width="80px" runat="server" MaxLength="100"></asp:TextBox>
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
                                    <td>&nbsp;</td>
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
                <ig:WebDataGrid ID="WebDataGrid_bdsa" runat="server"
                  EnableAjax="false" EnableViewState="True" EnableDataViewState="True"
                  Width="1100px" AutoGenerateColumns="False" DataKeyFields="bdlr_gkey" OnRowSelectionChanged="WebDataGrid_bdsa_RowSelectionChanged">
                  <Columns>
                    <ig:TemplateDataField Key="bdlr_hidden" Hidden="true">
                      <ItemTemplate>
                        <input id="tx_bdlr_gkey02" type="hidden" name="tx_bdlr_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_gkey").ToString() %>' runat="server" />
                        <input id="tx_bdlr_mkey02" type="hidden" name="tx_bdlr_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_mkey").ToString() %>' runat="server" />
                        <input id="tx_bdlr_BDNUM02" type="hidden" name="tx_bdlr_BDNUM02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_BDNUM").ToString() %>' runat="server" />
                      </ItemTemplate>
                      <Header Text="bdlr_hidden" />
                    </ig:TemplateDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDNUM" Key="bdlr_BDNUM" Width="80px">
                      <Header Text="bdlr_BDNUM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDNAM" Key="bdlr_BDNAM" Width="200px">
                      <Header Text="bdlr_BDNAM">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDMN2" Key="bdlr_BDMN2" Width="100px">
                      <Header Text="bdlr_BDMN2">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDTEL" Key="bdlr_BDTEL" Width="100px">
                      <Header Text="bdlr_BDTEL">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDA12" Key="bdlr_BDA12" Width="100px">
                      <Header Text="bdlr_BDA12">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDA13" Key="bdlr_BDA13" Width="200px">
                      <Header Text="bdlr_BDA13">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_BDA12" Key="bdlr_BDA11" Width="100px">
                      <Header Text="bdlr_BDA12">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_gkey" Key="bdlr_gkey" Hidden="true" HtmlEncode="true">
                      <Header Text="bdlr_gkey">
                      </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="bdlr_mkey" Key="bdlr_mkey" Hidden="true" HtmlEncode="true">
                      <Header Text="bdlr_mkey">
                      </Header>
                    </ig:BoundDataField>
                  </Columns>
                  <Behaviors>
                    <ig:Selection CellClickAction="Row" CellSelectType="None" RowSelectType="Single">
                      <AutoPostBackFlags RowSelectionChanged="true" />
                    </ig:Selection>
                    <ig:RowSelectors>
                    </ig:RowSelectors>
                    <ig:Sorting SortingMode="Multi">
                    </ig:Sorting>
                    <ig:Paging PageSize="12" PagerMode="NumericFirstLast">
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
                      <ig:WebDataGrid ID="WebDataGrid_bdsaba" runat="server"
                        EnableAjax="false" EnableViewState="True" EnableDataViewState="True"
                        Width="240px" AutoGenerateColumns="False" DataKeyFields="bdlr_gkey" OnRowSelectionChanged="WebDataGrid_bdsaba_RowSelectionChanged">
                        <Columns>
                          <ig:TemplateDataField Key="bdlr_hidden" Hidden="true">
                            <ItemTemplate>
                              <input id="Hidden1" type="hidden" name="tx_bdlr_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_gkey").ToString() %>' runat="server" />
                              <input id="Hidden2" type="hidden" name="tx_bdlr_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_mkey").ToString() %>' runat="server" />
                              <input id="Hidden3" type="hidden" name="tx_bdlr_BDNUM02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_BDNUM").ToString() %>' runat="server" />
                            </ItemTemplate>
                            <Header Text="bdlr_hidden" />
                          </ig:TemplateDataField>
                          <ig:BoundDataField DataFieldName="bdlr_BDNUM" Key="bdlr_BDNUM" Width="60px">
                            <Header Text="bdlr_BDNUM">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="bdlr_BDNAM" Key="bdlr_BDNAM" Width="200px">
                            <Header Text="bdlr_BDNAM">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="bdlr_gkey" Key="bdlr_gkey" Hidden="true" HtmlEncode="true">
                            <Header Text="bdlr_gkey">
                            </Header>
                          </ig:BoundDataField>
                          <ig:BoundDataField DataFieldName="bdlr_mkey" Key="bdlr_mkey" Hidden="true" HtmlEncode="true">
                            <Header Text="bdlr_mkey">
                            </Header>
                          </ig:BoundDataField>
                        </Columns>
                        <Behaviors>
                          <ig:Selection CellClickAction="Row" CellSelectType="None" RowSelectType="Single">
                            <AutoPostBackFlags RowSelectionChanged="true" />
                          </ig:Selection>
                          <ig:RowSelectors>
                          </ig:RowSelectors>
                          <ig:Sorting SortingMode="Multi">
                          </ig:Sorting>
                          <ig:Paging PageSize="12" PagerMode="NumericFirstLast">
                          </ig:Paging>
                        </Behaviors>
                        <ClientEvents AJAXResponse="WebDataGridView_AJAXResponse" />
                      </ig:WebDataGrid>
                    </asp:Panel>
                  </td>
                  <td class="tdtop">
                    <ig:WebTab ID="WebTabEdtRightTop" runat="server" Width="920px" StyleSetName="Appletini" StyleSetPath="~/ig_res" TabItemSize="100px">
                      <Tabs>
                        <ig:ContentTabItem runat="server" Text="基本">
                          <Template>
                            <asp:Panel ID="WebTabEdtRightTop01" runat="server" Width="900px">
                              <table>
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDNUM" runat="server" Text="編　　號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNUM" Width="100px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDNAM" runat="server" Text="名　　稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNAM" Width="190px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDCJ5" runat="server" Text="拼音代碼"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDCJ5" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="lb_bdlr_BDCNX" runat="server" Text="分倉類別"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDCNX" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDCNX" Width="0px" Visible="false" runat="server" />
                                    <asp:CheckBox ID="ck_bdlr_BDISP" Width="100px" runat="server" Text="分倉資料" />
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDMN2" runat="server" Text="連絡人　"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMN2" Width="100px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                  <td colspan="4">
                                    <asp:Label ID="lb_bdlr_BDA1R" runat="server" Text="連絡地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDA11" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDA12" Width="120px" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDA13" Width="300px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDTEL" runat="server" Text="連絡電話"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDTEL" Width="360px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDMT2" runat="server" Text="行動電話"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMT2" Width="120px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDFAX" runat="server" Text="傳真號碼"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDFAX" Width="120px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <td>
                                  <asp:Label ID="lb_bdlr_BDBIR" runat="server" Text="生　　日"></asp:Label>
                                  <ig:WebDateTimeEditor ID="tx_bdlr_BDBIR" Width="100px" runat="server"></ig:WebDateTimeEditor>
                                </td>
                                <td colspan="2">
                                  <asp:Label ID="lb_bdlr_BDEML" runat="server" Text="電子郵件"></asp:Label>
                                  <asp:TextBox ID="tx_bdlr_BDEML" Width="190px" runat="server" MaxLength="60"></asp:TextBox>
                                </td>
                                <td>
                                  <asp:Label ID="lb_bdlr_BDNAT" runat="server" Text="國　　籍"></asp:Label>
                                  <asp:DropDownList ID="dr_bdlr_BDNAT" Width="120px" runat="server" />
                                  <asp:TextBox ID="tx_bdlr_BDNAT" Width="0px" Visible="false" runat="server" />
                                </td>
                                <td>
                                  <asp:Label ID="lb_bdlr_BDSEX" runat="server" Text="性　　別"></asp:Label>
                                  <asp:DropDownList ID="dr_bdlr_BDSEX" Width="120px" runat="server" />
                                  <asp:TextBox ID="tx_bdlr_BDSEX" Width="0px" Visible="false" runat="server" />
                                </td>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDTXN" runat="server" Text="身份證號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDTXN" Width="100px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDINV" runat="server" Text="統一編號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDINV" Width="190px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDINA" runat="server" Text="發票抬頭"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDINA" Width="320px" runat="server" MaxLength="60"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDEND" runat="server" Text="停止日期"></asp:Label>
                                    <ig:WebDateTimeEditor ID="tx_bdlr_BDEND" Width="100px" runat="server" MaxLength="10"></ig:WebDateTimeEditor>
                                  </td>
                                  <td colspan="4">
                                    <asp:Label ID="lb_bdlr_BDB1R" runat="server" Text="發票地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDB11" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDB12" Width="120px" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDB13" Width="300px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="5">
                                    <asp:Label ID="lb_bdlr_BDRMK" runat="server" Text="備　　註"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDRMK" Width="750px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </asp:Panel>
                          </Template>
                        </ig:ContentTabItem>
                      </Tabs>
                    </ig:WebTab>
                  </td>
                </tr>
              </table>
            </Template>
          </ig:ContentTabItem>
        </Tabs>
      </ig:WebTab>
      <asp:ObjectDataSource ID="Obj_bdsa" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_bdsa" TypeName="DD2015_45.DAC_bdsa" OnSelecting="Obj_bdsa_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_bdsaba" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_bdsaba" TypeName="DD2015_45.DAC_bdsa" OnSelecting="Obj_bdsaba_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
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
        function WebDataGridView_AJAXResponse(grid, e) {
          if (e.get_gridResponseObject().Message)
            alert(e.get_gridResponseObject().Message);
        }
      </script>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
