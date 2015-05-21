<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMM.Master" AutoEventWireup="true" CodeBehind="fm_rpt.aspx.cs" Inherits="DD2015_45.Forms.sys.fm_rpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Literal ID="li_AccMsg" runat="server"></asp:Literal>
  <input id="hh_GridGkey" type="hidden" name="hh_GridGkey" runat="server" />
  <input id="hh_GridCtrl" type="hidden" name="hh_GridCtrl" runat="server" />
  <input id="hh_ActKey" type="hidden" name="hh_ActGuidKey" runat="server" />
  <input id="hh_mkey" type="hidden" name="hh_mkey" runat="server" />
  <input id="hh_qkey" type="hidden" name="hh_qkey" runat="server" />
  <table>
    <tr>
      <td>
        <asp:Panel ID="pn_left_top" runat="server" BorderStyle="Dashed" ScrollBars="Both" Height="950px" Width="180px">
          <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows">
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
          </asp:TreeView>
        </asp:Panel>
      </td>
      <td>
        <asp:Panel ID="pan_right_top" runat="server" BorderStyle="None" ScrollBars="None" Height="40px" Width="880px">
          <table>
            <tr>
              <td>
                <asp:LinkButton ID="bt_CAN" runat="server" CausesValidation="False" CommandName="CANCEL" AccessKey="C" CssClass="LinkButton80" Text="C上一步" Width="80px" OnClick="bt_CAN_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_SAV" runat="server" CausesValidation="False" CommandName="SAVE" AccessKey="S" CssClass="LinkButton80" Text="S完成" Width="80px" OnClick="bt_SAV_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_02" runat="server" CausesValidation="False" CommandName="NEW" AccessKey="N" CssClass="LinkButton80" Text="N新增" Width="80px" OnClick="bt_02_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_03" runat="server" CausesValidation="False" CommandName="INSERT" AccessKey="I" CssClass="LinkButton80" Text="I插入" Width="80px"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_04" runat="server" CausesValidation="False" CommandName="MODIFY" AccessKey="M" CssClass="LinkButton80" Text="M修改" Width="80px" OnClick="bt_04_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_05" runat="server" CausesValidation="False" CommandName="DELETE" AccessKey="X" CssClass="LinkButton80" Text="X刪除" Width="80px" OnClick="bt_05_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_06" runat="server" CausesValidation="False" CommandName="COPY" AccessKey="O" CssClass="LinkButton80" Text="O複製" Width="80px"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_07" runat="server" CausesValidation="False" CommandName="PRINT" AccessKey="P" CssClass="LinkButton80" Text="P列印" Width="80px"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_08" runat="server" CausesValidation="False" CommandName="SERCH" AccessKey="F" CssClass="LinkButton80" Text="F查詢" Width="80px"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_09" runat="server" CausesValidation="False" CommandName="TRANS" AccessKey="T" CssClass="LinkButton80" Text="T轉單" Width="80px"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_10" runat="server" CausesValidation="False" CommandName="EXCEL" AccessKey="E" CssClass="LinkButton80" Text="Excel" Width="80px"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_11" runat="server" CausesValidation="False" CommandName="MODALL" AccessKey="B" CssClass="LinkButton80" Text="L整批修改" Width="80px" OnClick="bt_11_Click"></asp:LinkButton>
              </td>
              <td>
                <asp:LinkButton ID="bt_QUT" runat="server" CausesValidation="False" CommandName="QUIT" AccessKey="Q" CssClass="LinkButton80" Text="Q離開" Width="80px" OnClick="bt_QUT_Click"></asp:LinkButton>
              </td>
            </tr>
          </table>
        </asp:Panel>
        <asp:Panel ID="pan_right" runat="server" BorderStyle="Dashed" ScrollBars="Both" Height="900px" Width="880px">
          <asp:Label ID="lb_ErrorMessage" runat="server" Text="" EnableViewState="false" Visible="false" CssClass="ErrorMessage"></asp:Label>
          <table>
            <tr>
              <td>
                　
              </td>
            </tr>
            <tr>
              <td>
                <asp:Label ID="lb_sys_rpt_obj_name" runat="server" Text="功能物件"></asp:Label>
                <asp:TextBox ID="tx_sys_rpt_obj_name" Width="120px" runat="server" MaxLength="40"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_source" runat="server" Text="資料來源"></asp:Label>
                <asp:TextBox ID="tx_sys_rpt_rpt_source" Width="120px" runat="server" MaxLength="2"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_Page" runat="server" Text="使用頁面"></asp:Label>
                <asp:DropDownList ID="dr_sys_rpt_rpt_Page" Width="140px" runat="server" />
                <asp:TextBox ID="tx_sys_rpt_rpt_Page" Width="0px" Visible="false" runat="server" />
              </td>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_file" runat="server" Text="報表檔名"></asp:Label>
                <asp:TextBox ID="tx_sys_rpt_rpt_file" Width="120px" runat="server" MaxLength="20"></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td colspan="2">
                <asp:Label ID="lb_sys_rpt_rpt_name" runat="server" Text="報表名稱"></asp:Label>
                <asp:TextBox ID="tx_sys_rpt_rpt_name" Width="320px" runat="server" MaxLength="100"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_comp" runat="server" Text="公司類別"></asp:Label>
                <asp:DropDownList ID="dr_sys_rpt_rpt_comp" Width="140px" runat="server" />
                <asp:TextBox ID="tx_sys_rpt_rpt_comp" Width="0px" Visible="false" runat="server" />
              </td>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_title" runat="server" Text="表頭設定"></asp:Label>
                <asp:DropDownList ID="dr_sys_rpt_rpt_title" Width="120px" runat="server" />
                <asp:TextBox ID="tx_sys_rpt_rpt_title" Width="0px" Visible="false" runat="server" />
              </td>
            </tr>
            <tr>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_dates1" runat="server" Text="開始日01"></asp:Label>
                <asp:TextBox ID="tx_sys_rpt_rpt_dates1" Width="120px" runat="server" MaxLength="200"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_datee1" runat="server" Text="結束日01"></asp:Label>
                <asp:TextBox ID="tx_sys_rpt_rpt_datee1" Width="120px" runat="server" MaxLength="200"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_dates2" runat="server" Text="開始日02"></asp:Label>
                <asp:TextBox ID="tx_sys_rpt_rpt_dates2" Width="140px" runat="server" MaxLength="200"></asp:TextBox>
              </td>
              <td>
                <asp:Label ID="lb_sys_rpt_rpt_datee2" runat="server" Text="結束日02"></asp:Label>
                <asp:TextBox ID="tx_sys_rpt_rpt_datee2" Width="120px" runat="server" MaxLength="200"></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td colspan="4">
                <asp:GridView ID="gr_GridView_sys_rpt"  SkinID="gridviewSkinAlter" runat="server" AutoGenerateColumns="False" DataKeyNames="sys_rpt_gkey" EnableModelValidation="True" AllowPaging="True" OnRowDataBound="gr_GridView_sys_rpt_RowDataBound" OnSelectedIndexChanged="gr_GridView_sys_rpt_SelectedIndexChanged">
                  <Columns>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>選</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:ImageButton CommandName="Select" ImageUrl='<%# hh_GridGkey.Value==DataBinder.Eval(Container.DataItem,"sys_rpt_gkey").ToString() ? "~\\images\\GridCheck.gif":"~\\images\\GridUnCheck.gif" %>' runat="server" ID="Imagebutton1" />
                        <input id="tx_sys_rpt_gkey02" type="hidden" name="tx_sys_rpt_gkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_rpt_gkey").ToString() %>' runat="server" />
                        <input id="tx_sys_rpt_mkey02" type="hidden" name="tx_sys_rpt_mkey02" value='<%# DataBinder.Eval(Container.DataItem,"sys_rpt_mkey").ToString() %>' runat="server" />
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>來源</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox ID="tx_sys_rpt_rpt_source02" runat="server" MaxLength="2" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_rpt_rpt_source").ToString() %>'></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>頁面</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox Visible="false" runat="server" ID="tx_sys_rpt_rpt_Page02" />
                        <asp:DropDownList Width="130px" runat="server" ID="dr_sys_rpt_rpt_Page02" />
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>報表名稱</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox ID="tx_sys_rpt_rpt_name02" runat="server" MaxLength="100" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_rpt_rpt_name").ToString() %>'></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>公司類別</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox Visible="false" runat="server" ID="tx_sys_rpt_rpt_comp02" />
                        <asp:DropDownList Width="80px" runat="server" ID="dr_sys_rpt_rpt_comp02" />
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>表頭設定</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox Visible="false" runat="server" ID="tx_sys_rpt_rpt_title02" />
                        <asp:DropDownList Width="90px" runat="server" ID="dr_sys_rpt_rpt_title02" />
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>報表檔名</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox ID="tx_sys_rpt_rpt_file02" runat="server" MaxLength="20" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_rpt_rpt_file").ToString() %>'></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>開始日01</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox ID="tx_sys_rpt_rpt_dates102" runat="server" MaxLength="200" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_rpt_rpt_dates1").ToString() %>'></asp:TextBox>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <b>結束日01</b>
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:TextBox ID="tx_sys_rpt_rpt_datee102" runat="server" MaxLength="200" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"sys_rpt_rpt_datee1").ToString() %>'></asp:TextBox>
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
