<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_ss165.aspx.cs" Inherits="DD2015_45.Forms.sys.fm_ss165" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
  <input id="hh_GridGkey_ss165" type="hidden" name="hh_GridGkey_ss165" runat="server" />
  <input id="hh_GridCtrl_ss165" type="hidden" name="hh_GridCtrl_ss165" runat="server" />
  <input id="hh_ActKey_ss165" type="hidden" name="hh_ActGuidKey_ss165" runat="server" />
  <input id="hh_mkey_ss165" type="hidden" name="hh_mkey_ss165" runat="server" />
  <input id="hh_GridGkey_ss175" type="hidden" name="hh_GridGkey_ss175" runat="server" />
  <input id="hh_GridCtrl_ss175" type="hidden" name="hh_GridCtrl_ss175" runat="server" />
  <input id="hh_ActKey_ss175" type="hidden" name="hh_ActGuidKey_ss175" runat="server" />
  <input id="hh_mkey_ss175" type="hidden" name="hh_mkey_ss175" runat="server" />
  <input id="hh_treekey" type="hidden" name="hh_treekey" runat="server" />
  <input id="hh_tree_act_type" type="hidden" name="hh_tree_act_type" runat="server" />
  <input id="hh_GridGkey_ss170" type="hidden" name="hh_GridGkey_ss170" runat="server" />
  <input id="hh_GridCtrl_ss170" type="hidden" name="hh_GridCtrl_ss170" runat="server" />
  <input id="hh_ActGuidKey_ss170" type="hidden" name="hh_ActGuidKey_ss170" runat="server" />
  <input id="hh_mkey_ss170" type="hidden" name="hh_mkey_ss170" runat="server" />
  <table>
    <tr>
      <td rowspan="1">
        <asp:Panel ID="pn_lftop" runat="server" BorderStyle="Dashed" ScrollBars="Both" Height="280px" Width="500px">
          <table>
            <tr>
              <td>
                <asp:LinkButton ID="bt_CAN_ss165" runat="server" AccessKey="C"
                  CausesValidation="False" CommandName="CANCEL"
                  CssClass="LinkButton80" Text="C上一步" Width="80px" OnClick="bt_CAN_ss165_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_NEW_ss165" runat="server"
                  CausesValidation="False" CommandName="NEW"
                  CssClass="LinkButton80" Text="A新增" Width="80px" OnClick="bt_NEW_ss165_Click" AccessKey="A"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_MOD_ss165" runat="server"
                  CausesValidation="False" CommandName="MODIFY" AccessKey="M"
                  CssClass="LinkButton80" Text="M更正" Width="80px" OnClick="bt_MOD_ss165_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_SAV_ss165" runat="server"
                  CausesValidation="False" CommandName="SAVE"
                  CssClass="LinkButton80" Text="S完成" Width="80px" OnClick="bt_SAV_ss165_Click" AccessKey="S"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_DEL_ss165" runat="server"
                  CausesValidation="False" CommandName="DELETE" AccessKey="X"
                  CssClass="LinkButton80" Text="X刪除" Width="80px" OnClick="bt_DEL_ss165_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_DTL_ss165" runat="server"
                  CausesValidation="False" CommandName="DETAIL" AccessKey="E"
                  CssClass="LinkButton80" Text="E員工" Width="80px" OnClick="bt_DTL_ss165_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_QUT_ss165" runat="server"
                  CausesValidation="False" CommandName="QUIT" AccessKey="Q"
                  CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_QUT_ss165_Click"></asp:LinkButton>
              </td>
            </tr>
          </table>
          <table>
            <tr>
              <td>&nbsp;
              </td>
            </tr>
            <tr>
              <td>
                <asp:Label ID="lb_sys_ss165_groupno" runat="server" Text="群組編號"></asp:Label>
                <asp:TextBox ID="tx_sys_ss165_groupno" Width="80px" runat="server" MaxLength="10"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_sys_ss165_groupname" runat="server" Text="群組名稱"></asp:Label>
                <asp:TextBox ID="tx_sys_ss165_groupname" Width="80px" runat="server" MaxLength="100"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_sys_ss165_usertype" runat="server" Text="帳號類別"></asp:Label>
                <asp:DropDownList ID="dr_sys_ss165_usertype" Width="80px" runat="server" />
                <asp:TextBox ID="tx_sys_ss165_usertype" Width="0px" Visible="false" runat="server" />
              </td>
            </tr>
          </table>
          <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
          <asp:GridView ID="gr_GridView_sys_ss165" SkinID="gridviewSkinAlter" runat="server" AutoGenerateColumns="False" DataKeyNames="sys_ss165_gkey" EnableModelValidation="True" AllowPaging="True" PageSize="5" OnRowDataBound="gr_GridView_sys_ss165_RowDataBound" OnPageIndexChanged="gr_GridView_sys_ss165_PageIndexChanged" OnSelectedIndexChanged="gr_GridView_sys_ss165_SelectedIndexChanged" OnPageIndexChanging="gr_GridView_sys_ss165_PageIndexChanging">
            <Columns>
              <asp:TemplateField>
                <HeaderTemplate>
                  <b><%# DD2015_45.PublicVariable.st_choose %></b>
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey_ss165.Value==DataBinder.Eval(Container.DataItem,"sys_ss165_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate>
                  <b><%#lb_sys_ss165_groupno.Text%></b>
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:Label ID="tx_sys_ss165_groupno02" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss165_groupno").ToString() %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate>
                  <b><%#lb_sys_ss165_groupname.Text%></b>
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:Label ID="tx_sys_ss165_groupname02" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss165_groupname").ToString() %>'></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate>
                  <b><%#lb_sys_ss165_usertype.Text%></b>
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:TextBox Visible="false" runat="server" ID="tx_sys_ss165_usertype02" />
                  <asp:DropDownList Width="80px" runat="server" ID="dr_sys_ss165_usertype02" />
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>
        </asp:Panel>
      </td>
      <td rowspan="1">
        <asp:Panel ID="pn_ritop" runat="server" BorderStyle="Dashed" ScrollBars="Both" Height="280px" Width="500px">
          <table>
            <tr>
              <td>
                <asp:LinkButton ID="bt_CAN_ss175" runat="server" AccessKey="C"
                  CausesValidation="False" CommandName="CANCEL"
                  CssClass="LinkButton80" Text="C上一步" Width="80px" OnClick="bt_CAN_ss175_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_NEW_ss175" runat="server"
                  CausesValidation="False" CommandName="NEW"
                  CssClass="LinkButton80" Text="新增員工" Width="80px" OnClick="bt_NEW_ss175_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_MOD_ss175" runat="server"
                  CausesValidation="False" CommandName="MODIFY"
                  CssClass="LinkButton80" Text="更正員工" Width="80px" OnClick="bt_MOD_ss175_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_SAV_ss175" runat="server"
                  CausesValidation="False" CommandName="SAVE"
                  CssClass="LinkButton80" Text="完成" Width="80px" OnClick="bt_SAV_ss175_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_DEL_ss175" runat="server"
                  CausesValidation="False" CommandName="DELETE"
                  CssClass="LinkButton80" Text="刪除員工" Width="80px" OnClick="bt_DEL_ss175_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_QUT_ss175" runat="server"
                  CausesValidation="False" CommandName="QUIT"
                  CssClass="LinkButton80" Text="離開" Width="80px" OnClick="bt_QUT_ss175_Click"></asp:LinkButton>
              </td>
            </tr>
          </table>
          <table>
            <tr>
            <td>&nbsp;
            </td>
            </tr>
            <tr>
              <td>
                <asp:Label ID="lb_es101_no" runat="server" Text="員工編號"></asp:Label>
                <asp:DropDownList ID="dr_es101_no" Width="160px" runat="server" />
                <asp:TextBox ID="tx_es101_no" runat="server" Width="0px" Visible="false"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_es101_ename" runat="server" Text="英文名"></asp:Label>
                <asp:TextBox ID="tx_es101_ename" Width="100px" runat="server" MaxLength="20"></asp:TextBox>
              </td>
            </tr>
          </table>
          <asp:Label ID="lb_ErrorMessage02" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
          <asp:GridView ID="gr_GridView_sys_ss175" SkinID="gridviewSkinAlter" runat="server" AutoGenerateColumns="False" DataKeyNames="sys_ss175_gkey" EnableModelValidation="True" AllowPaging="false" OnRowDataBound="gr_GridView_sys_ss175_RowDataBound" OnSelectedIndexChanged="gr_GridView_sys_ss175_SelectedIndexChanged" OnPageIndexChanged="gr_GridView_sys_ss175_PageIndexChanged" OnPageIndexChanging="gr_GridView_sys_ss175_PageIndexChanging">
            <Columns>
              <asp:TemplateField>
                <HeaderTemplate>
                  <b>選</b>
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey_ss175.Value==DataBinder.Eval(Container.DataItem,"sys_ss175_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate>
                  <b>員工編號</b>
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:TextBox Visible="false" runat="server" ID="tx_es101_no02" />
                  <asp:DropDownList Width="200px" runat="server" ID="dr_es101_no02" />
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate>
                  <b>英文姓名</b>
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:TextBox ID="tx_es101_ename02" runat="server" MaxLength="10" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem,"es101_ename").ToString() %>'></asp:TextBox>
                  <input id="tx_sys_ss175_es101gkey02" type="hidden" name="tx_sys_ss175_es101gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss175_es101gkey").ToString() %>' runat="server" />
                  <input id="tx_sys_ss175_ss165gkey02" type="hidden" name="tx_sys_ss175_ss165gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss175_ss165gkey").ToString() %>' runat="server" />
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>
        </asp:Panel>
      </td>
    </tr>
    <tr>
      <td rowspan="2">
        <asp:Panel ID="pn_lfdown" runat="server" BorderStyle="Dashed" ScrollBars="Both" Height="600px" Width="500px">
          <asp:TreeView ID="tr_funcView" runat="server" ImageSet="Arrows" OnSelectedNodeChanged="tr_funcView_SelectedNodeChanged">
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
          </asp:TreeView>

        </asp:Panel>
      </td>
      <td rowspan="2">
        <asp:Panel ID="pn_ridown" runat="server" BorderStyle="Dashed" ScrollBars="Both" Height="600px" Width="500px">
          <table>
            <tr>
              <td colspan="3"></td>
              <td colspan="1">
                <asp:LinkButton ID="bn_ACC" runat="server"
                  CausesValidation="False" CommandName="SAVE"
                  CssClass="LinkButton80" Text="T保存" Width="80px" AccessKey="T" OnClick="bn_ACC_Click"></asp:LinkButton>
              </td>
            </tr>
            <tr>
              <td colspan="4">
                <asp:GridView ID="gr_GridView_sys_ss170" runat="server" AutoGenerateColumns="False" DataKeyNames="sys_ss170_gkey" EnableModelValidation="True" AllowPaging="false" OnRowDataBound="gr_GridView_sys_ss170_RowDataBound">
                  <Columns>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>作業編號</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <input id="tx_sys_ss170_gkey02" type="hidden" name="tx_sys_ss170_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss170_gkey").ToString() %>' runat="server" />
                        <input id="tx_sys_ss170_ss165gkey02" type="hidden" name="tx_sys_ss170_ss165gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss170_ss165gkey").ToString() %>' runat="server" />
                        <input id="tx_sys_ss170_ss160gkey02" type="hidden" name="tx_sys_ss170_ss160gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_ss170_ss160gkey").ToString() %>' runat="server" />
                        <asp:TextBox ID="tx_sys_menu_obj_name_02" runat="server" MaxLength="10" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_menu_obj_name_").ToString() %>'></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>作業名稱</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox ID="tx_sys_menu_chinesesimp_02" runat="server" MaxLength="10" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_menu_chinesesimp_").ToString() %>'></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>編號</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox ID="tx_sys_ss160_buttonno02" runat="server" MaxLength="10" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_buttonno").ToString() %>'></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>按鈕名稱</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox ID="tx_sys_ss160_button02" runat="server" MaxLength="10" Width="60px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_ss160_button").ToString() %>'></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>權限</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <p align="center">
                          <asp:CheckBox Width="40px" runat="server" ID="ck_sys_ss170_mark02" />
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
  </table>
  <asp:Literal ID="li_Msg" runat="server"></asp:Literal>
</asp:Content>
