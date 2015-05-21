<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_bdsr.aspx.cs" Inherits="DD2015_45.Forms.bas.fm_bdsr" %>

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
              <igtxt:webimagebutton id="bt_CAN" AccessKey="C" runat="server" usebrowserdefaults="False"
                height="90%" text="C取消" imagedirectory="../../images/" onclick="bt_CAN_Click">
                <Appearance>
                  <Image Url="form_cancel.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_SAV"  AccessKey="S"  runat="server" usebrowserdefaults="False"
                height="90%" text="S存檔" imagedirectory="../../images/" onclick="bt_SAV_Click">
                <Appearance>
                  <Image Url="form_save.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_02" AccessKey="N"  runat="server" usebrowserdefaults="False"
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
              <igtxt:webimagebutton id="bt_08" runat="server" usebrowserdefaults="False"
                height="90%" text="F查詢" AccessKey="F" imagedirectory="../../images/" onclick="bt_08_Click">
                <Appearance>
                  <Image Url="form_serch.png"></Image>
                  <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                </Appearance>
              </igtxt:webimagebutton>
            </td>
            <td>
              <igtxt:webimagebutton id="bt_09" runat="server" usebrowserdefaults="False"
                height="90%" text="T轉單" AccessKey ="T" imagedirectory="../../images/">
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
              <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
            </td>
          </tr>
        </table>
      </asp:Panel>
      <ig:WebTab ID="WebTab_form" runat="server" Height="800px" Width="1200px" StyleSetName="Claymation" StyleSetPath="~/ig_res" TabItemSize="100px" SelectedIndex="0">
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
                <ig:WebDataGrid ID="WebDataGrid_bdsr" runat="server"
                  EnableAjax="False" EnableDataViewState="True"
                  Width="1100px" AutoGenerateColumns="False" DataKeyFields="bdlr_gkey" OnRowSelectionChanged="WebDataGrid_bdsr_RowSelectionChanged">
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
                      <ig:WebDataGrid ID="WebDataGrid_bdsrba" runat="server"
                        EnableAjax="false" EnableViewState="True" EnableDataViewState="True"
                        Width="240px" AutoGenerateColumns="False" DataKeyFields="bdlr_gkey" OnRowSelectionChanged="WebDataGrid_bdsrba_RowSelectionChanged">
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
                    <ig:WebTab ID="WebTabEdtRightTop" runat="server" Width="920px" StyleSetName="Appletini" StyleSetPath="~/ig_res" TabItemSize="100px" SelectedIndex="0">
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
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDCJ5" runat="server" Text="名稱首碼"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDCJ5" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:CheckBox ID="ck_bdlr_BDISS" Width="100px" runat="server" Text="客戶資料" />
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDNAM" runat="server" Text="名　　稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNAM" Width="470px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDSHT" runat="server" Text="簡　　稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDSHT" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDNME" runat="server" Text="英文名稱"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNME" Width="470px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="1">
                                    <asp:Label ID="lb_bdlr_BDTEL" runat="server" Text="連絡電話"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDTEL" Width="200px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                  <td colspan="1">
                                    <asp:Label ID="lb_bdlr_BDFAX" runat="server" Text="傳真號碼"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDFAX" Width="200px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="1">
                                    <asp:Label ID="lb_bdlr_BDMN2" runat="server" Text="連絡人名"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMN2" Width="200px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                  <td colspan="1">
                                    <asp:Label ID="lb_bdlr_BDMT2" runat="server" Text="連絡行動"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMT2" Width="200px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDEML" runat="server" Text="電子郵件"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDEML" Width="200px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDWWW" runat="server" Text="公司網址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDWWW" Width="200px" runat="server" MaxLength="100"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDNUP" runat="server" Text="付款廠商"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNUP" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_bdlr_BDNUPN" Width="100px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDSAL" runat="server" Text="負責業務"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDSAL" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:TextBox ID="tx_es010_BDSAL" Width="100px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDMCU" runat="server" Text="貨運公司"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDMCU" Width="80px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDMCU" Width="0px" Visible="false" runat="server" />
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
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDINA" runat="server" Text="發票抬頭"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDINA" Width="300px" runat="server" MaxLength="60"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDINV" runat="server" Text="統一編號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDINV" Width="300px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDMN1" runat="server" Text="負責人名"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMN1" Width="300px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDMT1" runat="server" Text="行動電話"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDMT1" Width="300px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:Label ID="lb_bdlr_BDDPT" runat="server" Text="部門設定"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDDPT" Width="300px" runat="server" MaxLength="10"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:CheckBox ID="ck_bdlr_BDACC" Width="300px" runat="server" Text="轉換傳票" />
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
                                    <asp:Label ID="lb_bdlr_BDCLA" runat="server" Text="廠商等級"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDCLA" Width="120px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDCLA" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDDEX" runat="server" Text="價格等級"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDDEX" Width="120px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDDEX" Width="0px" Visible="false" runat="server" />
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDPAY" runat="server" Text="付款方式"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDPAY" Width="120px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDPAY" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDSPY" runat="server" Text="文易方式"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDSPY" Width="120px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDSPY" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDDRS" runat="server" Text="使用幣別"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDDRS" Width="120px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDDRS" Width="0px" Visible="false" runat="server" />
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDSMA" runat="server" Text="營業類別"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDSMA" Width="120px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDSMA" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDIVY" runat="server" Text="結帳發票"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDIVY" Width="120px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDIVY" Width="0px" Visible="false" runat="server" />
                                  </td>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDCTY" runat="server" Text="國別設定"></asp:Label>
                                    <asp:DropDownList ID="dr_bdlr_BDCTY" Width="120px" runat="server" />
                                    <asp:TextBox ID="tx_bdlr_BDCTY" Width="0px" Visible="false" runat="server" />
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDBCN" runat="server" Text="連接編號"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDBCN" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="4">
                                    <asp:Label ID="lb_bdlr_BDRMK" runat="server" Text="備　　註"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDRMK" Width="500px" runat="server" MaxLength="500"></asp:TextBox>
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
                        <ig:ContentTabItem runat="server" Text="國外">
                          <Template>
                            <asp:Panel ID="WebTabEdtRightTop04" runat="server" Width="900px">
                              <table>
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDED1" runat="server" Text="國外地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDED1" Width="500px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDED2" runat="server" Text="國外地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDED2" Width="500px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDED3" runat="server" Text="國外地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDED3" Width="500px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDED4" runat="server" Text="國外地址"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDED4" Width="500px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDEM1" runat="server" Text="國外聯絡"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDEM1" Width="500px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDET1" runat="server" Text="國外電話"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDET1" Width="500px" runat="server" MaxLength="120"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDEF1" runat="server" Text="國外傳真"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDEF1" Width="500px" runat="server" MaxLength="120"></asp:TextBox>
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
                        <ig:ContentTabItem runat="server" Text="說明">
                          <Template>
                            <asp:Panel ID="WebTabEdtRightTop05" runat="server" Width="900px">
                              <table>
                                <tr>
                                  <td>
                                    <asp:Label ID="lb_bdlr_BDNOT" runat="server" Height="150px" Text="備註說明"></asp:Label>
                                    <asp:TextBox ID="tx_bdlr_BDNOT" Width="700px" Height="150px" TextMode="MultiLine"   runat="server" MaxLength="2000"></asp:TextBox>
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
      <asp:ObjectDataSource ID="Obj_bdsr" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_bdsr" TypeName="DD2015_45.DAC_bdsr" OnSelecting="Obj_bdsr_Selecting">
        <SelectParameters>
          <asp:Parameter Name="WhereQuery" Type="Object" />
          <asp:Parameter DefaultValue="" Name="st_addSelect" Type="String" />
          <asp:Parameter DefaultValue="false" Name="bl_lock" Type="Boolean" />
          <asp:Parameter DefaultValue="" Name="st_addJoin" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_addUnion" Type="String" />
          <asp:Parameter DefaultValue="" Name="st_orderKey" Type="String" />
        </SelectParameters>
      </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="Obj_bdsrba" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTable_bdsrba" TypeName="DD2015_45.DAC_bdsr" OnSelecting="Obj_bdsrba_Selecting">
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
