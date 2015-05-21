<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_ss180.aspx.cs" Inherits="DD2015_45.Forms.sys.fm_ss180" %>

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
      <input id="hh_treekey" type="hidden" name="hh_treekey" runat="server" />
      <input id="hh_es101gkey" type="hidden" name="hh_es101gkey" runat="server" />
      <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
      <table>
        <tr>
          <td rowspan="1">
            <asp:Panel ID="pn_left_top" runat="server" BorderStyle="Inset"  ScrollBars="Both" Height="400px" Width="500px">
              <table>
                <tr>
                  <td colspan="3">
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
                          <igtxt:WebImageButton ID="bt_NEW" runat="server" UseBrowserDefaults="False"
                            Height="90%" Text="N新增" AccessKey="N" ImageDirectory="../../images/" OnClick="bt_NEW_Click">
                            <Appearance>
                              <Image Url="form_new.png"></Image>
                              <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                            </Appearance>
                          </igtxt:WebImageButton>
                        </td>
                        <td>
                          <igtxt:WebImageButton ID="bt_MOD" AccessKey="M" runat="server" UseBrowserDefaults="False"
                            Height="90%" Text="M更正" ImageDirectory="../../images/" OnClick="bt_MOD_Click">
                            <Appearance>
                              <Image Url="form_edit.png"></Image>
                              <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                            </Appearance>
                          </igtxt:WebImageButton>
                        </td>
                        <td>
                          <igtxt:WebImageButton ID="bt_DEL" runat="server" AccessKey="X" AutoSubmit="false" UseBrowserDefaults="False"
                            Height="90%" Text="X刪除" ImageDirectory="../../images/" OnClick="bt_DEL_Click">
                            <Appearance>
                              <Image Url="form_delete.png"></Image>
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
                  </td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
                <tr>
                  <td colspan="2">
                    <asp:Label ID="lb_es101_no" runat="server" Text="員工編號"></asp:Label>
                    <asp:DropDownList ID="dr_es101_no" Width="230px" runat="server" />
                    <asp:TextBox ID="tx_es101_no" runat="server" Width="0px" Visible="false"></asp:TextBox>
                  </td>
                  <td>
                    <asp:Label ID="lb_es101_ename" runat="server" Text="英  文  名"></asp:Label>
                    <asp:TextBox ID="tx_es101_ename" Width="80px" runat="server" MaxLength="20"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td>
                    <asp:Label ID="lb_ss101_usercode" runat="server" Text="帳　　號"></asp:Label>
                    <asp:TextBox ID="tx_ss101_usercode" Width="80px" runat="server" MaxLength="50"></asp:TextBox>
                  </td>
                  <td>
                    <asp:Label ID="lb_ss101_password" runat="server" Text="密　　碼"></asp:Label>
                    <asp:TextBox ID="tx_ss101_password" Width="80px" runat="server" MaxLength="50"></asp:TextBox>
                  </td>
                  <td>
                    <asp:Label ID="lb_ss101_usertype" runat="server" Text="帳號類別"></asp:Label>
                    <asp:DropDownList ID="dr_ss101_usertype" Width="80px" runat="server" />
                    <asp:TextBox ID="tx_ss101_usertype" Width="0px" Visible="false" runat="server" />
                  </td>
                </tr>
              </table>
              <asp:GridView ID="gr_GridView_ss101" SkinID="gridviewSkinNoAlter" runat="server" AutoGenerateColumns="False" DataKeyNames="ss101_gkey" EnableModelValidation="True" AllowPaging="True" OnRowDataBound="gr_GridView1_RowDataBound" OnSelectedIndexChanged="gr_GridView_ss101_SelectedIndexChanged" OnPageIndexChanged="gr_GridView_ss101_PageIndexChanged" OnPageIndexChanging="gr_GridView_ss101_PageIndexChanging" PageSize="5">
                <Columns>
                  <asp:TemplateField>
                    <HeaderTemplate>
                      <b>選</b>
                    </HeaderTemplate>
                    <ItemTemplate>
                      <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"ss101_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <HeaderTemplate>
                      <b>員工編號</b>
                    </HeaderTemplate>
                    <ItemTemplate>
                      <input id="tx_ss101_gkey02" type="hidden" name="tx_ss101_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"ss101_gkey").ToString() %>' runat="server" />
                      <input id="tx_ss101_es101gkey02" type="hidden" name="tx_ss101_es101gkey02" value='<%# DataBinder.Eval(Container.DataItem,"ss101_es101gkey").ToString() %>' runat="server" />
                      <asp:Label Width="150px" runat="server" ID="tx_es101_no02" Text='<%# DataBinder.Eval(Container.DataItem,"es101_no").ToString() %>' />
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <HeaderTemplate>
                      <b>帳號</b>
                    </HeaderTemplate>
                    <ItemTemplate>
                      <asp:Label ID="tx_ss101_usercode02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"ss101_usercode").ToString() %>'></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <HeaderTemplate>
                      <b>帳號類別</b>
                    </HeaderTemplate>
                    <ItemTemplate>
                      <asp:TextBox Visible="false" runat="server" ID="tx_ss101_usertype02" />
                      <asp:DropDownList Width="100px" runat="server" ID="dr_ss101_usertype02" />
                    </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
                <PagerSettings Position="TopAndBottom" />
              </asp:GridView>
            </asp:Panel>
          </td>
          <td rowspan="2">
            <asp:Panel ID="pn_right_top" runat="server" BorderStyle="Inset" ScrollBars="Both" Height="800px" Width="500px">
              <table>
                <tr>
                  <td colspan="3"></td>
                  <td>
                    <igtxt:WebImageButton ID="bn_ACC" AccessKey="V" runat="server" UseBrowserDefaults="False"
                      Height="90%" Text="V保存" ImageDirectory="../../images/" OnClick="bn_ACC_Click">
                      <Appearance>
                        <Image Url="form_save.png"></Image>
                        <ButtonStyle Font-Names="verdana" Font-Size="12pt" />
                      </Appearance>
                    </igtxt:WebImageButton>
                  </td>
                </tr>
                <tr>
                  <td colspan="4">
                    <asp:GridView ID="gr_GridView_sys_ss180" runat="server" AutoGenerateColumns="False" DataKeyNames="sys_ss180_gkey" EnableModelValidation="True" AllowPaging="false" OnRowDataBound="gr_GridView_sys_ss180_RowDataBound">
                      <Columns>
                        <asp:TemplateField>
                          <HeaderTemplate>
                            <b>作業編號</b>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <input id="tx_sys_ss180_gkey02" type="hidden" name="tx_sys_ss180_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss180_gkey").ToString() %>' runat="server" />
                            <input id="tx_sys_ss180_es101gkey02" type="hidden" name="tx_sys_ss180_es101gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss180_es101gkey").ToString() %>' runat="server" />
                            <input id="tx_sys_ss180_ss160gkey02" type="hidden" name="tx_sys_ss180_ss160gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss180_ss160gkey").ToString() %>' runat="server" />
                            <asp:Label ID="tx_sys_menu_obj_name_02" runat="server" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_menu_obj_name_").ToString() %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                          <HeaderTemplate>
                            <b>作業名稱</b>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <asp:Label ID="tx_sys_menu_chinesesimp_02" runat="server" Width="90px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_menu_chinesesimp_").ToString() %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                          <HeaderTemplate>
                            <b>編號</b>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <asp:Label ID="tx_sys_ss160_buttonno02" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_buttonno").ToString() %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                          <HeaderTemplate>
                            <b>按鈕名稱</b>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <asp:Label ID="tx_sys_ss160_button02" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_button").ToString() %>'></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                          <HeaderTemplate>
                            <b>權限</b>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <p align="center">
                              <asp:CheckBox Width="40px" runat="server" ID="ck_sys_ss180_mark02" />
                            </p>
                          </ItemTemplate>
                        </asp:TemplateField>
                      </Columns>
                    </asp:GridView>

                  </td>
                </tr>
              </table>
            </asp:Panel>
          </td>
        </tr>
        <tr>
          <td rowspan="1">
            <asp:Panel ID="pn_left_mid" runat="server" BorderStyle="Inset"   ScrollBars="Both" Height="400px" Width="500px">
              <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
              </asp:TreeView>
            </asp:Panel>
          </td>
        </tr>
        <tr>
          <td>down left</td>
          <td>down right</td>
        </tr>
      </table>
      <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
