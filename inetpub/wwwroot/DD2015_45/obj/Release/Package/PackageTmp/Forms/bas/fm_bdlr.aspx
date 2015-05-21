<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_bdlr.aspx.cs" Inherits="DD2015_45.Forms.bas.fm_bdlr" %>

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

    .auto-style1
    {
      height: 30px;
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
              <igtxt:webimagebutton id="bt_02" runat="server" usebrowserdefaults="False"
                height="90%" text="N新增" AccessKey="N" imagedirectory="../../images/" onclick="bt_02_Click">
                <Appearance>
                  <Image Url="form_new.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_03" runat="server" usebrowserdefaults="False"
                height="90%" text="I插入" AccessKey="I" imagedirectory="../../images/">
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
              <igtxt:webimagebutton id="bt_05" runat="server" AccessKey="X"  autosubmit="false" usebrowserdefaults="False"
                height="90%" text="X刪除" imagedirectory="../../images/" onclick="bt_05_Click">
                <Appearance>
                  <Image Url="form_delete.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_06" runat="server" AccessKey="O"  usebrowserdefaults="False"
                height="90%" text="O複製" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_copy.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_07" runat="server" AccessKey="P" usebrowserdefaults="False"
                height="90%" text="P列印" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_print.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_08" runat="server" AccessKey="F" usebrowserdefaults="False"
                height="90%" text="F查詢" imagedirectory="../../images/" onclick="bt_08_Click">
                <Appearance>
                  <Image Url="form_serch.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_09" runat="server" AccessKey="T" usebrowserdefaults="False"
                height="90%" text="T轉單" imagedirectory="../../images/">
                <Appearance>
                  <Image Url="form_copy.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_10" runat="server" AccessKey="E" usebrowserdefaults="False"
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
              <igtxt:webimagebutton id="bt_QUT" runat="server" AccessKey="Q" usebrowserdefaults="False"
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
              <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
            </td>
          </tr>
        </table>
      </asp:Panel>
      <ig:WebTab ID="WebTab_form" runat="server" Height="800px" Width="1200px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px">
        <Tabs>
          <ig:ContentTabItem runat="server" Key="QUE" Text="查詢">
            <Template>
              <asp:Panel ID="PanSer" runat="server" Width="1000px">
                <table>
                  <tr>
                    <td>
                      <ig:WebTab ID="WebTab_SER" runat="server" Width="1100px" StyleSetName="Pear" StyleSetPath="~/ig_res" TabItemSize="70px">
                        <Tabs>
                          <ig:ContentTabItem runat="server"  Key="GEN" Text="一般">
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
                          <ig:ContentTabItem runat="server"  Key="ADV" Text="進階">
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
                <ig:WebDataGrid ID="WebDataGrid_bdlr" runat="server"
                  EnableAjax="false" EnableViewState="True" EnableDataViewState="True"
                  Width="1100px" AutoGenerateColumns="False" DataKeyFields="bdlr_gkey" OnRowSelectionChanged="WebDataGrid_bdlr_RowSelectionChanged">
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
          <ig:ContentTabItem runat="server"  Key="EDI" Text="編輯">
            <Template>
              <table>
                <tr>
                  <td class="tdtop">
                    <asp:Panel ID="PanEdtLeft" runat="server" Width="250px" BorderStyle="Inset">
                      <ig:WebDataGrid ID="WebDataGrid_bdlrba" runat="server"
                        EnableAjax="false" EnableViewState="True" EnableDataViewState="True"
                        Width="240px" AutoGenerateColumns="False" DataKeyFields="bdlr_gkey" OnRowSelectionChanged="WebDataGrid_bdlrba_RowSelectionChanged">
                        <Columns>
                          <ig:TemplateDataField Key="bdlr_hidden" Hidden="true">
                            <ItemTemplate>
                              <input id="tx_bdlr_gkey02" type="hidden" name="tx_bdlr_gkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_gkey").ToString() %>' runat="server" />
                              <input id="tx_bdlr_mkey02" type="hidden" name="tx_bdlr_mkey02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_mkey").ToString() %>' runat="server" />
                              <input id="tx_bdlr_BDNUM02" type="hidden" name="tx_bdlr_BDNUM02" value='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "bdlr_BDNUM").ToString() %>' runat="server" />
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
                    <ig:WebTab ID="WebTabEdtRightTop" runat="server" Width="920px" StyleSetName="Appletini" StyleSetPath="~/ig_res" TabItemSize="100px" SelectedIndex="2">
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
                                    <asp:TextBox ID="tx_bdlr_BDNUM" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDCJ5" runat="server" Text="拼音代碼"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDCJ5" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:CheckBox ID="ck_bdlr_BDISP" Width="100px" runat="server" Text="分倉資料" />
                                    <asp:CheckBox ID="ck_bdlr_BDISF" Width="100px" runat="server" Text="廠商資料" />
                                    <asp:CheckBox ID="ck_bdlr_BDISG" Width="100px" runat="server" Text="寄賣經銷" />
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDNAM" runat="server" Text="名　　稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNAM" Width="580px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDSHT" runat="server" Text="簡　　稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDSHT" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDNME" runat="server" Text="英文名稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNME" Width="580px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDC1R" runat="server" Text="聯絡地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDC11" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDC12" Width="140px" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDC13" Width="360px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDA1R" runat="server" Text="送貨地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDA11" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDA12" Width="140px" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDA13" Width="360px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDTEL" runat="server" Text="連絡電話"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDTEL" Width="360px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDFAX" runat="server" Text="傳真號碼"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDFAX" Width="340px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDMN2" runat="server" Text="連絡人　"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMN2" Width="360px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDMT2" runat="server" Text="連絡人行動"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMT2" Width="340px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDEML" runat="server" Text="EMAIL "></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDEML" Width="360px" runat="server" MaxLength="60"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDWWW" runat="server" Text="公司網址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDWWW" Width="340px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDNUP" runat="server" Text="請款客戶"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNUP" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDNUPN" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDSAL" runat="server" Text="負責業務"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDSAL" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_es101_BDSAL" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDMCU" runat="server" Text="貨運公司"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDMCU" Width="140px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDMCU" Width="0px" Visible="false" runat="server" />
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
                        <ig:ContentTabItem runat="server" Text="公司">
                          <Template>
                            <asp:Panel ID="WebTabEdtRightTop02" runat="server" Width="900px">
                              <table>
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDINA" runat="server" Text="發票抬頭"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDINA" Width="460px" runat="server" MaxLength="60"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDINV" runat="server" Text="統一編號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDINV" Width="120px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDB1R" runat="server" Text="發票地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDB11" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDB12" Width="120px" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDB13" Width="250px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="1" class="auto-style1">
                                    <asp:Label ID="lb_bdlr_BDMN1" runat="server" Text="負責人　"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMN1" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                  <td colspan="2" class="auto-style1">
                                    <asp:Label ID="lb_bdlr_BDMT1" runat="server" Text="行動電話"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMT1" Width="240px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="3">
                                    <asp:Label ID="lb_bdlr_BDD1R" runat="server" Text="帳單地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDD11" Width="60px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDD12" Width="120px" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDD13" Width="250px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDMNA" runat="server" Text="連絡會計"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMNA" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDTEA" runat="server" Text="會計電話"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDTEA" Width="240px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:CheckBox ID="ck_bdlr_BDPR1" Width="100px" runat="server" Text="列印名條" />
                                    <asp:CheckBox ID="ck_bdlr_BDACC" Width="100px" runat="server" Text="轉換傳票" />
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDBK1" runat="server" Text="付款銀行"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBK1" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDBN1" runat="server" Text="銀行名稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBN1" Width="240px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDBO1" runat="server" Text="銀行帳號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBO1" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDBK2" runat="server" Text="付款銀行"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBK2" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDBN2" runat="server" Text="銀行名稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBN2" Width="240px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDBO2" runat="server" Text="銀行帳號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBO2" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDBK3" runat="server" Text="付款銀行"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBK3" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDBN3" runat="server" Text="銀行名稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBN3" Width="240px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDBO3" runat="server" Text="銀行帳號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBO3" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDEND" runat="server" Text="停止日期"></asp:Label>
                                    <ig:WebDateTimeEditor ID="tx_bdlr_BDEND" Width="120px" StyleSetName="Appletini" StyleSetPath="../../../ig_res" runat="server"></ig:WebDateTimeEditor>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDDPT" runat="server" Text="部門設定"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDDPT" Width="120px" runat="server" MaxLength="10"></asp:TextBox>
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
                        <ig:ContentTabItem runat="server" Text="設定">
                          <Template>
                            <asp:Panel ID="WebTabEdtRightTop03" runat="server" Width="900px">
                              <table>
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDCLA" runat="server" Text="客戶等級"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDCLA" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDCLA" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDDEX" runat="server" Text="價格等級"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDDEX" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDDEX" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDCRD" runat="server" Text="信用額度"></asp:Label>
                                    <ig:WebNumericEditor ID="tx_bdlr_BDCRD" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDMLB" runat="server" Text="管理品牌"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDMLB" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDMLB" Width="0px" Visible="false" runat="server" />
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDPAY" runat="server" Text="付款方式"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDPAY" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDPAY" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDDTL" runat="server" Text="資料安全"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDDTL" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDDTL" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDBCN" runat="server" Text="連接編號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBCN" Width="80px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDCHN" runat="server" Text="通路類別"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDCHN" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDCHN" Width="0px" Visible="false" runat="server" />
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDSMA" runat="server" Text="營業類別"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDSMA" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDSMA" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDIVY" runat="server" Text="發票方式"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDIVY" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDIVY" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDTXP" runat="server" Text="計稅方式"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDTXP" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDTXP" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDNXM" runat="server" Text="下月帳日"></asp:Label>
                                    <ig:WebNumericEditor ID="tx_bdlr_BDNXM" Width="80px" runat="server" MinDecimalPlaces="0" StyleSetName="Appletini" StyleSetPath="../../../ig_res"></ig:WebNumericEditor>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="4">
                                    <asp:Label ID="lb_bdlr_BDRMK" runat="server" Text="備　　註"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDRMK" Width="580px" runat="server" MaxLength="100"></asp:TextBox>
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
                        <ig:ContentTabItem runat="server" Text="貨運">
                          <Template>
                            <asp:Panel ID="WebTabEdtRightTop04" runat="server" Width="900px">
                              <table>
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDNOT" runat="server" Text="備註說明"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNOT" Width="80px" runat="server" MaxLength="2000"></asp:TextBox>
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
      <asp:ObjectDataSource ID="Obj_bdlr" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_bdlr" TypeName="DD2015_45.DAC_bdlr" OnSelecting="Obj_bdlr_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_bdlrba" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_bdlrba" TypeName="DD2015_45.DAC_bdlr" OnSelecting="Obj_bdlrba_Selecting">
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
