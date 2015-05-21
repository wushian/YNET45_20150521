using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using YNetLib_45;

namespace DD2015_45.Forms.bas
{
  public partial class fm_bpud : FormBase
  {

    string st_object_func = "UNbpud";
    string st_ContentPlaceHolder = "ctl00$ContentPlaceHolder1$";
    string st_ContentPlaceHolderEdit1 = "ctl00$ContentPlaceHolder1$WebTab1$tmpl0$";
    protected void Page_Load(object sender, EventArgs e)
    {
      //檢查權限&狀態
      li_Msg.Text = "";
      li_AccMsg.Text = "";
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 1, UserLoginGkey, ref li_AccMsg))
      {
        if (!IsPostBack)
        {
          dr_bpud_BPLAB = sFN.DropDownListFromTable(ref dr_bpud_BPLAB, "PDBPLAB", "bknum", "bknam", "", "bknum");
          dr_bpud_BPDP1 = sFN.DropDownListFromTable(ref dr_bpud_BPDP1, "PDBPDP1", "bknum", "bknam", "", "bknum");
          dr_bpud_BPDP2 = sFN.DropDownListFromTable(ref dr_bpud_BPDP2, "PDBPDP2", "bknum", "bknam", "", "bknum");
          dr_bpud_BPDP3 = sFN.DropDownListFromTable_(ref dr_bpud_BPDP3, "PDBPDP3", "bknum", "bknam", "", "bknum");
          dr_bpud_BPDP4 = sFN.DropDownListFromTable(ref dr_bpud_BPDP4, "PDBPDP4", "bknum", "bknam", "", "bknum");
          dr_bpud_BPDX1 = sFN.DropDownListFromTable_(ref dr_bpud_BPDX1, "PDBPDP1", "bknum", "bknam", "", "", "bknum", " ");
          dr_bpud_BPDX2 = sFN.DropDownListFromTable_(ref dr_bpud_BPDX2, "PDBPDP2", "bknum", "bknam", "", "", "bknum", " ");
          dr_bpud_BPDX3 = sFN.DropDownListFromTable_(ref dr_bpud_BPDX3, "PDBPDP3", "bknum", "bknam", "", "", "bknum", " ");
          dr_bpud_BPDX4 = sFN.DropDownListFromTable_(ref dr_bpud_BPDX4, "PDBPDP4", "bknum", "bknam", "", "", "bknum", " ");
          dr_bpud_BPMDC = sFN.DropDownListFromTable(ref dr_bpud_BPMDC, "PDBPMDC", "bknum", "bknam", "", "bknum");
          dr_bpud_BPUNI = sFN.DropDownListFromTable(ref dr_bpud_BPUNI, "PDBPUNI", "bknum", "bknam", "", "bknum");

          dr_bpud_BPMNY = sFN.DropDownListFromTable(ref dr_bpud_BPMNY, "PDGAMNY", "bknum", "bknam", "", "bknum");
          dr_bpud_BPUN1 = sFN.DropDownListFromTable(ref dr_bpud_BPUN1, "PDBPUNI", "bknum", "bknam", "", "bknum");
          dr_bpud_BPUN3 = sFN.DropDownListFromTable(ref dr_bpud_BPUN3, "PDBPUNI", "bknum", "bknam", "", "bknum");

          //
          WebTab1.SelectedIndex = 0;
          //
          //
          CmdQueryS.CommandText = " AND 1=1 ";
          Session["fmbpud_CmdQueryS"] = CmdQueryS;
          Set_Control();
          if (DAC.GetStringValue(Session["fmbpud_gr_GridView_bpud_GridGkey"]) != "")
          {
            gr_GridView_bpud.PageIndex = DAC.GetInt16Value(Session["fmbpud_gr_GridView_bpud_PageIndex"]);
            gr_GridView_bpud.SelectedIndex = DAC.GetInt16Value(Session["fmbpud_gr_GridView_bpud_SelectedIndex"]);
            hh_GridGkey.Value = DAC.GetStringValue(Session["fmbpud_gr_GridView_bpud_GridGkey"]);
          }
          //
          BindNew(true);
          SetSerMod();
          Session["fmbpud_gr_GridView_bpud_PageIndex"] = gr_GridView_bpud.PageIndex;
          Session["fmbpud_gr_GridView_bpud_SelectedIndex"] = gr_GridView_bpud.SelectedIndex;
        }
        else
        {
          if ((hh_GridCtrl.Value.ToString().ToLower() == "ins") || (hh_GridCtrl.Value.ToString().ToLower() == "mod"))
          {
            BindNew(false);
          }
          else
          {
            //BindNew(true);
          }
        }
      }
    }

    private void Set_Control()
    {
      FunctionName = sFN.SetFormTitle(st_object_func, PublicVariable.LangType);   //取Page Title
      this.Page.Title = FunctionName;
      gr_GridView_bpud.PageSize = int.Parse(sFN.Get_FormVariable(st_object_func, "pagesize"));  //GridView 取pagesize
      sFN.SetFormControlsText(this.Form, PublicVariable.LangType,ApVer, "UNbpud", "bpud");
      //
      tx_bpud_BPDE1.MinDecimalPlaces = cin_TDE1;
      tx_bpud_BPDE2.MinDecimalPlaces = cin_TDE1;
      //
      //
      WebTab1.Tabs[0].Hidden = false;   //一般
      WebTab1.Tabs[1].Hidden = false;    //成本
      WebTab1.Tabs[2].Hidden = false;   //繁體說明
      WebTab1.Tabs[3].Hidden = false;   //簡體說明
      WebTab1.Tabs[4].Hidden = false;   //英文說明
      //
      if (PublicVariable.CompanyName == "NAMI")
      {
        //WebTab1.Tabs[1].Hidden = true;   //成本
      }
      if (PublicVariable.CompanyName == "PDM")
      {
        WebTab1.Tabs[3].Hidden = true;   //簡體說明
        WebTab1.Tabs[4].Hidden = true;   //英文說明
      }
      //
    }

    private void ClearText()
    {
      //
      tx_bpud_BPNUM.Text = "";  //商品編號
      tx_bpud_BPNUB.Text = "";  //條碼編號
      dr_bpud_BPLAB.SelectedIndex = -1;  //品牌名稱
      tx_bpud_BPYES.Text = "";  //年度季節
      tx_bpud_BPTNA.Text = "";  //中文名稱
      tx_bpud_BPCNA.Text = "";  //簡體名稱
      tx_bpud_BPENA.Text = "";  //英文名稱
      tx_bpud_BPCLA.Text = "";  //規　　格
      dr_bpud_BPDP1.SelectedIndex = -1;  //分類一
      dr_bpud_BPDP2.SelectedIndex = -1;  //分類二
      dr_bpud_BPDP3.SelectedIndex = -1;  //分類三
      dr_bpud_BPDP4.SelectedIndex = -1;  //分類四

      dr_bpud_BPDX1.CurrentValue = "";  //分類四
      dr_bpud_BPDX2.CurrentValue = "";  //分類四
      dr_bpud_BPDX3.CurrentValue = "";  //分類四
      dr_bpud_BPDX4.CurrentValue = "";  //分類四


      dr_bpud_BPMDC.SelectedIndex = -1;  //產　　地
      dr_bpud_BPUNI.SelectedIndex = -1;  //單位
      tx_bpud_BPNCR.Text = "";  //款式編號
      tx_bpud_BPCLR.Text = "";  //顏色代號
      tx_bpud_BPCLN.Text = "";  //顏色名稱
      tx_bpud_BPSIZ.Text = "";  //尺寸名稱
      tx_bpud_BPDE1.Text = "0";  //售價含稅
      tx_bpud_BPDE2.Text = "0";  //會員含稅
      tx_bpud_BPDT1.Text = "";  //上架日期
      tx_bpud_BPETQ.Text = "";  //下架日期
      tx_bpud_BPOD1.Text = "";  //採購日一
      tx_bpud_BPOD2.Text = "";  //採購日二
      tx_bpud_BPCUS.Text = "";  //供應商一
      tx_bdlr_BPCUS.Text = "";  //供應商一
      tx_bpud_BPCU2.Text = "";  //供應商二
      tx_bdlr_BPCU2.Text = "";  //供應商二
      //
      tx_bpud_BPNPC.Text = "0";  //標準進價
      tx_bpud_BPSPC.Text = "0";  //最近進價
      tx_bpud_BPVPC.Text = "0";  //平均進價
      tx_bpud_BPGPC.Text = "0";  //參考進價
      tx_bpud_BPDE3.Text = "0";  //批發Ｃ價
      tx_bpud_BPDE4.Text = "0";  //批發Ｄ價
      tx_bpud_BPDE5.Text = "0";  //批發Ｅ價
      tx_bpud_BPDT3.Text = "";  //進貨日期
      tx_bpud_BPDT4.Text = "";  //銷貨日期
      tx_bpud_BPDT5.Text = "";  //盤點日期
      tx_bpud_BPMND.Text = "0";  //外幣進價
      tx_bpud_BPMNR.Text = "0";  //外幣倍數
      dr_bpud_BPMNY.SelectedIndex = -1;  //外幣幣別
      tx_bpud_BPCUQ.Text = "0";  //經濟採量
      dr_bpud_BPUN1.SelectedIndex = -1;  //進貨單位
      dr_bpud_BPUN3.SelectedIndex = -1;  //銷貨單位
      tx_bpud_BPQTM.Text = "0";  //調撥數量
      tx_bpud_BPQTH.Text = "0";  //最高量
      tx_bpud_BPQTL.Text = "0";  //最低量
      tx_bpud_BPQTS.Text = "0";  //安全量
      tx_bpud_BPDT2.Text = "";  //修改日期
      tx_bpud_BPET1.Text = "";  //停進貨日
      tx_bpud_BPETM.Text = "";  //停調貨日
      tx_bpud_BPET3.Text = "";  //停銷貨日
      //
      tx_bpud_BPDSN.Text = "";  //設計人員
      tx_bpud_BPSSN.Text = "";  //企劃人員
      ht_bpud_BPDSC.Text = "";  //繁體成份說明
      ht_bpud_BPDSCC.Text = "";  //簡體成份說明
      ht_bpud_BPDSCE.Text = "";  //英文成份說明
      ht_bpud_BPWSH.Text = "";  //繁體使用說明
      ht_bpud_BPWSHC.Text = "";  //簡體使用說明
      ht_bpud_BPWSHE.Text = "";  //英文使用說明
      tx_bpud_BPFLG.Text = "";  //功能旗標
      tx_bpud_BPRMK.Text = "";  //備註說明
      //dr_bpud_BPCL2.SelectedIndex = -1;  //進貨屬性

      //
      hh_mkey.Value = "";
    }

    private void SetSerMod()
    {
      //
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPNUM, ref tx_bpud_BPNUM, false);  //商品編號
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPTNA, ref tx_bpud_BPTNA, false);  //中文名稱
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCNA, ref tx_bpud_BPCNA, false);  //簡體名稱
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPENA, ref tx_bpud_BPENA, false);  //英文名稱
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCLA, ref tx_bpud_BPCLA, false);  //規　　格
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPDE1, ref tx_bpud_BPDE1, false);  //售價含稅
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPDE2, ref tx_bpud_BPDE2, false);  //會員含稅
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPDT1, ref tx_bpud_BPDT1, false);  //上架日期
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPETQ, ref tx_bpud_BPETQ, false);  //下架日期
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPOD1, ref tx_bpud_BPOD1, false);  //採購日一
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPOD2, ref tx_bpud_BPOD2, false);  //採購日二
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCUS, ref tx_bpud_BPCUS, false);  //供應商一
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCUS, ref tx_bdlr_BPCUS, false);  //供應商一
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCU2, ref tx_bpud_BPCU2, false);  //供應商二
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCU2, ref tx_bdlr_BPCU2, false);  //供應商二
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPNPC, ref tx_bpud_BPNPC, false);   //標準進價

      //
      clsGV.TextBox_Set(ref tx_bpud_BPNUM, false);   //商品編號
      clsGV.TextBox_Set(ref tx_bpud_BPNUB, false);   //條碼編號
      clsGV.Drpdown_Set(ref dr_bpud_BPLAB, ref tx_bpud_BPLAB, false);   //品牌名稱
      clsGV.TextBox_Set(ref tx_bpud_BPYES, false);   //年度季節
      clsGV.TextBox_Set(ref tx_bpud_BPTNA, false);   //中文名稱
      clsGV.TextBox_Set(ref tx_bpud_BPCNA, false);   //簡體名稱
      clsGV.TextBox_Set(ref tx_bpud_BPENA, false);   //英文名稱
      clsGV.TextBox_Set(ref tx_bpud_BPCLA, false);   //規　　格
      clsGV.Drpdown_Set(ref dr_bpud_BPDP1, ref tx_bpud_BPDP1, false);   //分類一
      clsGV.Drpdown_Set(ref dr_bpud_BPDP2, ref tx_bpud_BPDP2, false);   //分類二
      clsGV.Drpdown_Set(ref dr_bpud_BPDP3, ref tx_bpud_BPDP3, false);   //分類三
      clsGV.Drpdown_Set(ref dr_bpud_BPDP4, ref tx_bpud_BPDP4, false);   //分類四
      clsGV.Drpdown_Set(ref dr_bpud_BPDX1, ref tx_bpud_BPDX1, false);   //分類一
      clsGV.Drpdown_Set(ref dr_bpud_BPDX2, ref tx_bpud_BPDX2, false);   //分類二
      clsGV.Drpdown_Set(ref dr_bpud_BPDX3, ref tx_bpud_BPDX3, false);   //分類三
      clsGV.Drpdown_Set(ref dr_bpud_BPDX4, ref tx_bpud_BPDX4, false);   //分類四
      //igdr_bpud_BPDP4.Enabled=false;  
      clsGV.Drpdown_Set(ref dr_bpud_BPMDC, ref tx_bpud_BPMDC, false);   //產　　地
      clsGV.Drpdown_Set(ref dr_bpud_BPUNI, ref tx_bpud_BPUNI, false);   //單位
      clsGV.TextBox_Set(ref tx_bpud_BPNCR, false);   //款式編號
      clsGV.TextBox_Set(ref tx_bpud_BPCLR, false);   //顏色代號
      clsGV.TextBox_Set(ref tx_bpud_BPCLN, false);   //顏色名稱
      clsGV.TextBox_Set(ref tx_bpud_BPSIZ, false);   //尺寸名稱
      clsGV.TextBox_Set(ref tx_bpud_BPDE1, false);   //售價含稅
      clsGV.TextBox_Set(ref tx_bpud_BPDE2, false);   //會員含稅
      clsGV.TextBox_Set(ref tx_bpud_BPDT1, false);   //上架日期
      clsGV.TextBox_Set(ref tx_bpud_BPETQ, false);   //下架日期
      clsGV.TextBox_Set(ref tx_bpud_BPOD1, false);   //採購日一
      clsGV.TextBox_Set(ref tx_bpud_BPOD2, false);   //採購日二
      clsGV.TextBox_Set(ref tx_bpud_BPCUS, false);   //供應商一
      clsGV.TextBox_Set(ref tx_bdlr_BPCUS, false);   //供應商一
      clsGV.TextBox_Set(ref tx_bpud_BPCU2, false);   //供應商二
      clsGV.TextBox_Set(ref tx_bdlr_BPCU2, false);   //供應商二
      clsGV.TextBox_Set(ref tx_bpud_BPDSN, false);   //設計人員
      clsGV.TextBox_Set(ref tx_bpud_BPSSN, false);   //企劃人員
      clsGV.TextBox_Set(ref tx_bpud_BPFLG, false);   //功能旗標
      clsGV.TextBox_Set(ref tx_bpud_BPRMK, false);   //備註說明
      //
      clsGV.TextBox_Set(ref tx_bpud_BPNPC, false);   //標準進價
      clsGV.TextBox_Set(ref tx_bpud_BPSPC, false);   //最近進價
      clsGV.TextBox_Set(ref tx_bpud_BPVPC, false);   //平均進價
      clsGV.TextBox_Set(ref tx_bpud_BPGPC, false);   //參考進價
      clsGV.TextBox_Set(ref tx_bpud_BPDE3, false);   //批發Ｃ價
      clsGV.TextBox_Set(ref tx_bpud_BPDE4, false);   //批發Ｄ價
      clsGV.TextBox_Set(ref tx_bpud_BPDE5, false);   //批發Ｅ價
      clsGV.TextBox_Set(ref tx_bpud_BPDT3, false);   //進貨日期
      clsGV.TextBox_Set(ref tx_bpud_BPDT4, false);   //銷貨日期
      clsGV.TextBox_Set(ref tx_bpud_BPDT5, false);   //盤點日期
      clsGV.TextBox_Set(ref tx_bpud_BPMND, false);   //外幣進價
      clsGV.TextBox_Set(ref tx_bpud_BPMNR, false);   //外幣倍數
      clsGV.Drpdown_Set(ref dr_bpud_BPMNY, ref tx_bpud_BPMNY, false);   //外幣幣別
      clsGV.TextBox_Set(ref tx_bpud_BPCUQ, false);   //經濟採量
      clsGV.Drpdown_Set(ref dr_bpud_BPUN1, ref tx_bpud_BPUN1, false);   //進貨單位
      clsGV.Drpdown_Set(ref dr_bpud_BPUN3, ref tx_bpud_BPUN3, false);   //銷貨單位
      clsGV.TextBox_Set(ref tx_bpud_BPQTM, false);   //調撥數量
      clsGV.TextBox_Set(ref tx_bpud_BPQTH, false);   //最高量
      clsGV.TextBox_Set(ref tx_bpud_BPQTL, false);   //最低量
      clsGV.TextBox_Set(ref tx_bpud_BPQTS, false);   //安全量
      clsGV.TextBox_Set(ref tx_bpud_BPDT2, false);   //修改日期
      clsGV.TextBox_Set(ref tx_bpud_BPET1, false);   //停進貨日
      clsGV.TextBox_Set(ref tx_bpud_BPETM, false);   //停調貨日
      clsGV.TextBox_Set(ref tx_bpud_BPET3, false);   //停銷貨日
      //clsGV.Drpdown_Set(ref dr_bpud_BPCL2, ref tx_bpud_BPCL2, false);   //進貨屬性
      ht_bpud_BPDSC.ReadOnly = true;     //成份說明
      ht_bpud_BPDSCC.ReadOnly = true;     //成份說明
      ht_bpud_BPDSCE.ReadOnly = true;     //成份說明
      ht_bpud_BPWSH.ReadOnly = true;   //洗滌說明
      ht_bpud_BPWSHC.ReadOnly = true;   //洗滌說明
      ht_bpud_BPWSHE.ReadOnly = true;   //洗滌說明
      //
      clsGV.SetControlShowAlert(ref lb_bpud_BPNUM, ref tx_bpud_BPNUM, true);  // 商品編號
      clsGV.SetControlShowAlert(ref lb_bpud_BPTNA, ref tx_bpud_BPTNA, true);  // 中文名稱
      clsGV.SetControlShowAlert(ref lb_bpud_BPCNA, ref tx_bpud_BPCNA, true);  // 簡體名稱
      clsGV.SetControlShowAlert(ref lb_bpud_BPENA, ref tx_bpud_BPENA, true);  // 英文名稱
      clsGV.SetControlShowAlert(ref lb_bpud_BPCLA, ref tx_bpud_BPCLA, true);  // 規　　格
      clsGV.SetControlShowAlert(ref lb_bpud_BPDE1, ref tx_bpud_BPDE1, true);  // 售價含稅
      clsGV.SetControlShowAlert(ref lb_bpud_BPDE2, ref tx_bpud_BPDE2, true);  // 會員含稅
      clsGV.SetControlShowAlert(ref lb_bpud_BPDT1, ref tx_bpud_BPDT1, true);  // 上架日期
      clsGV.SetControlShowAlert(ref lb_bpud_BPETQ, ref tx_bpud_BPETQ, true);  // 下架日期
      clsGV.SetControlShowAlert(ref lb_bpud_BPOD1, ref tx_bpud_BPOD1, true);  // 採購日一
      clsGV.SetControlShowAlert(ref lb_bpud_BPOD2, ref tx_bpud_BPOD2, true);  // 採購日二
      clsGV.SetControlShowAlert(ref lb_bpud_BPCUS, ref tx_bpud_BPCUS, true);  // 供應商一
      clsGV.SetControlShowAlert(ref lb_bpud_BPCUS, ref tx_bdlr_BPCUS, true);  // 供應商一
      clsGV.SetControlShowAlert(ref lb_bpud_BPCU2, ref tx_bpud_BPCU2, true);  // 供應商二
      clsGV.SetControlShowAlert(ref lb_bpud_BPCU2, ref tx_bdlr_BPCU2, true);  // 供應商二
      clsGV.SetControlShowAlert(ref lb_bpud_BPNPC, ref tx_bpud_BPNPC, true);   //標準進價
      //
      tx_bpud_BPCUS.Attributes.Remove("onblur");
      tx_bpud_BPCU2.Attributes.Remove("onblur");
      //
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "ser");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, false);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, false);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, true);
      //

      //
      //bt_NEW.Visible = true;
      //bt_MOD.Visible = true;
      //bt_MODALL.Visible = true;
      //bt_SER.Visible = true;
      //bt_DEL.Visible = true;
      //bt_PRN.Visible = true;
      //bt_SAV.Visible = false;
      //bt_CAN.Visible = false;
      //bt_QUT.Visible = true;
      //
      gr_GridView_bpud.Enabled = true;
      //gr_GridView_bpud.Columns[0].Visible=true;
    }

    private void SetEditMod()
    {
      // 
      clsGV.TextBox_Set(ref tx_bpud_BPNUM, true);  //商品編號
      clsGV.TextBox_Set(ref tx_bpud_BPNUB, true);  //條碼編號
      clsGV.Drpdown_Set(ref dr_bpud_BPLAB, ref tx_bpud_BPLAB, true);   //品牌名稱
      clsGV.TextBox_Set(ref tx_bpud_BPYES, true);  //年度季節
      clsGV.TextBox_Set(ref tx_bpud_BPTNA, true);  //中文名稱
      clsGV.TextBox_Set(ref tx_bpud_BPCNA, true);  //簡體名稱
      clsGV.TextBox_Set(ref tx_bpud_BPENA, true);  //英文名稱
      clsGV.TextBox_Set(ref tx_bpud_BPCLA, true);  //規　　格
      clsGV.Drpdown_Set(ref dr_bpud_BPDP1, ref tx_bpud_BPDP1, true);   //分類一
      clsGV.Drpdown_Set(ref dr_bpud_BPDP2, ref tx_bpud_BPDP2, true);   //分類二
      clsGV.Drpdown_Set(ref dr_bpud_BPDP3, ref tx_bpud_BPDP3, true);   //分類三
      clsGV.Drpdown_Set(ref dr_bpud_BPDP4, ref tx_bpud_BPDP4, true);   //分類四
      clsGV.Drpdown_Set(ref dr_bpud_BPDX1, ref tx_bpud_BPDX1, true);   //分類一
      clsGV.Drpdown_Set(ref dr_bpud_BPDX2, ref tx_bpud_BPDX2, true);   //分類二
      clsGV.Drpdown_Set(ref dr_bpud_BPDX3, ref tx_bpud_BPDX3, true);   //分類三
      clsGV.Drpdown_Set(ref dr_bpud_BPDX4, ref tx_bpud_BPDX4, true);   //分類四
      //igdr_bpud_BPDP4.Enabled=true; 
      clsGV.Drpdown_Set(ref dr_bpud_BPMDC, ref tx_bpud_BPMDC, true);   //產　　地
      clsGV.Drpdown_Set(ref dr_bpud_BPUNI, ref tx_bpud_BPUNI, true);   //單位
      clsGV.TextBox_Set(ref tx_bpud_BPNCR, true);  //款式編號
      clsGV.TextBox_Set(ref tx_bpud_BPCLR, true);  //顏色代號
      clsGV.TextBox_Set(ref tx_bpud_BPCLN, true);  //顏色名稱
      clsGV.TextBox_Set(ref tx_bpud_BPSIZ, true);  //尺寸名稱
      clsGV.TextBox_Set(ref tx_bpud_BPDE1, true);  //售價含稅
      clsGV.TextBox_Set(ref tx_bpud_BPDE2, true);  //會員含稅
      clsGV.TextBox_Set(ref tx_bpud_BPDT1, true);  //上架日期
      clsGV.TextBox_Set(ref tx_bpud_BPETQ, true);  //下架日期
      clsGV.TextBox_Set(ref tx_bpud_BPOD1, true);  //採購日一
      clsGV.TextBox_Set(ref tx_bpud_BPOD2, true);  //採購日二
      clsGV.TextBox_Set(ref tx_bpud_BPCUS, true);  //供應商一
      clsGV.TextBox_Set(ref tx_bdlr_BPCUS, true);  //供應商一
      clsGV.TextBox_Set(ref tx_bpud_BPCU2, true);  //供應商二
      clsGV.TextBox_Set(ref tx_bdlr_BPCU2, true);  //供應商二
      clsGV.TextBox_Set(ref tx_bpud_BPDSN, true);  //設計人員
      clsGV.TextBox_Set(ref tx_bpud_BPSSN, true);  //企劃人員
      clsGV.TextBox_Set(ref tx_bpud_BPFLG, true);  //功能旗標
      clsGV.TextBox_Set(ref tx_bpud_BPRMK, true);  //備註說明
      //
      clsGV.TextBox_Set(ref tx_bpud_BPNPC, true);  //標準進價
      clsGV.TextBox_Set(ref tx_bpud_BPSPC, true);  //最近進價
      clsGV.TextBox_Set(ref tx_bpud_BPVPC, true);  //平均進價
      clsGV.TextBox_Set(ref tx_bpud_BPGPC, true);  //參考進價
      clsGV.TextBox_Set(ref tx_bpud_BPDE3, true);  //批發Ｃ價
      clsGV.TextBox_Set(ref tx_bpud_BPDE4, true);  //批發Ｄ價
      clsGV.TextBox_Set(ref tx_bpud_BPDE5, true);  //批發Ｅ價
      clsGV.TextBox_Set(ref tx_bpud_BPDT3, true);  //進貨日期
      clsGV.TextBox_Set(ref tx_bpud_BPDT4, true);  //銷貨日期
      clsGV.TextBox_Set(ref tx_bpud_BPDT5, true);  //盤點日期
      clsGV.TextBox_Set(ref tx_bpud_BPMND, true);  //外幣進價
      clsGV.TextBox_Set(ref tx_bpud_BPMNR, true);  //外幣倍數
      clsGV.Drpdown_Set(ref dr_bpud_BPMNY, ref tx_bpud_BPMNY, true);   //外幣幣別
      clsGV.TextBox_Set(ref tx_bpud_BPCUQ, true);  //經濟採量
      clsGV.Drpdown_Set(ref dr_bpud_BPUN1, ref tx_bpud_BPUN1, true);   //進貨單位
      clsGV.Drpdown_Set(ref dr_bpud_BPUN3, ref tx_bpud_BPUN3, true);   //銷貨單位
      clsGV.TextBox_Set(ref tx_bpud_BPQTM, true);  //調撥數量
      clsGV.TextBox_Set(ref tx_bpud_BPQTH, true);  //最高量
      clsGV.TextBox_Set(ref tx_bpud_BPQTL, true);  //最低量
      clsGV.TextBox_Set(ref tx_bpud_BPQTS, true);  //安全量
      clsGV.TextBox_Set(ref tx_bpud_BPDT2, true);  //修改日期
      clsGV.TextBox_Set(ref tx_bpud_BPET1, true);  //停進貨日
      clsGV.TextBox_Set(ref tx_bpud_BPETM, true);  //停調貨日
      clsGV.TextBox_Set(ref tx_bpud_BPET3, true);  //停銷貨日
      //
      //clsGV.Drpdown_Set(ref dr_bpud_BPCL2, ref tx_bpud_BPCL2, true);   //進貨屬性
      ht_bpud_BPDSC.ReadOnly = false;     //成份說明
      ht_bpud_BPDSCC.ReadOnly = false;     //成份說明
      ht_bpud_BPDSCE.ReadOnly = false;     //成份說明
      ht_bpud_BPWSH.ReadOnly = false;   //洗滌說明
      ht_bpud_BPWSHC.ReadOnly = false;   //洗滌說明
      ht_bpud_BPWSHE.ReadOnly = false;   //洗滌說明
      // 
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPNUM, ref tx_bpud_BPNUM, true);  // 商品編號
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPTNA, ref tx_bpud_BPTNA, true);  // 中文名稱
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCNA, ref tx_bpud_BPCNA, true);  // 簡體名稱
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPENA, ref tx_bpud_BPENA, true);  // 英文名稱
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCLA, ref tx_bpud_BPCLA, true);  // 規　　格
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPDE1, ref tx_bpud_BPDE1, true);  // 售價含稅
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPDE2, ref tx_bpud_BPDE2, true);  // 會員含稅
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPDT1, ref tx_bpud_BPDT1, true);  // 上架日期
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPETQ, ref tx_bpud_BPETQ, true);  // 下架日期
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPOD1, ref tx_bpud_BPOD1, true);  // 採購日一
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPOD2, ref tx_bpud_BPOD2, true);  // 採購日二
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCUS, ref tx_bpud_BPCUS, true);  // 供應商一
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCUS, ref tx_bdlr_BPCUS, true);  // 供應商一
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCU2, ref tx_bpud_BPCU2, true);  // 供應商二
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPCU2, ref tx_bdlr_BPCU2, true);  // 供應商二
      //
      clsGV.SetTextBoxEditAlert(ref lb_bpud_BPNPC, ref tx_bpud_BPNPC, true);   //標準進價
      //
      tx_bpud_BPCUS.Attributes.Add("onblur", "return  get_bdlr_cname('tx','" + st_ContentPlaceHolderEdit1 + "','" + st_ContentPlaceHolderEdit1 + "tx_bpud_BPCUS','" + st_ContentPlaceHolderEdit1 + "tx_bdlr_BPCUS'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bdlr.aspx" + "','" + "廠商資料" + "')");
      tx_bpud_BPCU2.Attributes.Add("onblur", "return  get_bdlr_cname('tx','" + st_ContentPlaceHolderEdit1 + "','" + st_ContentPlaceHolderEdit1 + "tx_bpud_BPCU2','" + st_ContentPlaceHolderEdit1 + "tx_bdlr_BPCU2'" + ",'" + di_Window.ClientID + "','" + "../Dialog/Dialog_bdlr.aspx" + "','" + "廠商資料" + "')");
      sFN.SetWebImageButtons(this, PublicVariable.LangType, st_object_func, UserGkey, st_ContentPlaceHolder, "mod");
      sFN.SetWebImageButton(this, "bt_SAV", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_save, true);
      sFN.SetWebImageButton(this, "bt_CAN", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_cancel, true);
      sFN.SetWebImageButton(this, "bt_QUT", PublicVariable.LangType, st_ContentPlaceHolder, PublicVariable.st_quit, false);
      //
      //
      //bt_DEL.OnClientClick = " return false;";
      //bt_MOD.OnClientClick = " return false;";
      gr_GridView_bpud.Enabled = false;
    }

    private void BindText(DataRow CurRow)
    {
      //
      //
      tx_bpud_BPNUM.Text = DAC.GetStringValue(CurRow["bpud_BPNUM"]);  //商品編號
      tx_bpud_BPNUB.Text = DAC.GetStringValue(CurRow["bpud_BPNUB"]);  //條碼編號
      dr_bpud_BPLAB = sFN.SetDropDownList(ref dr_bpud_BPLAB, DAC.GetStringValue(CurRow["bpud_BPLAB"]));  //品牌名稱
      tx_bpud_BPYES.Text = DAC.GetStringValue(CurRow["bpud_BPYES"]);  //年度季節
      tx_bpud_BPTNA.Text = DAC.GetStringValue(CurRow["bpud_BPTNA"]);  //中文名稱
      tx_bpud_BPCNA.Text = DAC.GetStringValue(CurRow["bpud_BPCNA"]);  //簡體名稱
      tx_bpud_BPENA.Text = DAC.GetStringValue(CurRow["bpud_BPENA"]);  //英文名稱
      tx_bpud_BPCLA.Text = DAC.GetStringValue(CurRow["bpud_BPCLA"]);  //規　　格
      dr_bpud_BPDP1 = sFN.SetDropDownList(ref dr_bpud_BPDP1, DAC.GetStringValue(CurRow["bpud_BPDP1"]));  //分類一
      dr_bpud_BPDP2 = sFN.SetDropDownList(ref dr_bpud_BPDP2, DAC.GetStringValue(CurRow["bpud_BPDP2"]));  //分類二
      dr_bpud_BPDP3 = sFN.SetDropDownList(ref dr_bpud_BPDP3, DAC.GetStringValue(CurRow["bpud_BPDP3"]));  //分類三
      dr_bpud_BPDP4 = sFN.SetDropDownList(ref dr_bpud_BPDP4, DAC.GetStringValue(CurRow["bpud_BPDP4"]));  //分類四
      //
      dr_bpud_BPDX1 = sFN.setWebDropDownListSelectedFromString(ref dr_bpud_BPDX1, DAC.GetStringValue(CurRow["bpud_BPDX1"])); //分類一
      dr_bpud_BPDX2 = sFN.setWebDropDownListSelectedFromString(ref dr_bpud_BPDX2, DAC.GetStringValue(CurRow["bpud_BPDX2"])); //分類二
      dr_bpud_BPDX3 = sFN.setWebDropDownListSelectedFromString(ref dr_bpud_BPDX3, DAC.GetStringValue(CurRow["bpud_BPDX3"])); //分類三
      dr_bpud_BPDX4 = sFN.setWebDropDownListSelectedFromString(ref dr_bpud_BPDX4, DAC.GetStringValue(CurRow["bpud_BPDX4"])); //分類四
      //
      dr_bpud_BPMDC = sFN.SetDropDownList(ref dr_bpud_BPMDC, DAC.GetStringValue(CurRow["bpud_BPMDC"]));  //產　　地
      dr_bpud_BPUNI = sFN.SetDropDownList(ref dr_bpud_BPUNI, DAC.GetStringValue(CurRow["bpud_BPUNI"]));  //單位
      tx_bpud_BPNCR.Text = DAC.GetStringValue(CurRow["bpud_BPNCR"]);  //款式編號
      tx_bpud_BPCLR.Text = DAC.GetStringValue(CurRow["bpud_BPCLR"]);  //顏色代號
      tx_bpud_BPCLN.Text = DAC.GetStringValue(CurRow["bpud_BPCLN"]);  //顏色名稱
      tx_bpud_BPSIZ.Text = DAC.GetStringValue(CurRow["bpud_BPSIZ"]);  //尺寸名稱
      tx_bpud_BPDE1.Text = DAC.GetStringValue(CurRow["bpud_BPDE1"]);  //售價含稅
      tx_bpud_BPDE2.Text = DAC.GetStringValue(CurRow["bpud_BPDE2"]);  //會員含稅
      if (CurRow["bpud_BPDT1"] == DBNull.Value) { tx_bpud_BPDT1.Text = ""; } else { tx_bpud_BPDT1.Text = DAC.GetDateTimeValue(CurRow["bpud_BPDT1"]).ToString(sys_DateFormat); }  //上架日期
      if (CurRow["bpud_BPETQ"] == DBNull.Value) { tx_bpud_BPETQ.Text = ""; } else { tx_bpud_BPETQ.Text = DAC.GetDateTimeValue(CurRow["bpud_BPETQ"]).ToString(sys_DateFormat); }  //下架日期
      if (CurRow["bpud_BPOD1"] == DBNull.Value) { tx_bpud_BPOD1.Text = ""; } else { tx_bpud_BPOD1.Text = DAC.GetDateTimeValue(CurRow["bpud_BPOD1"]).ToString(sys_DateFormat); }  //採購日一
      if (CurRow["bpud_BPOD2"] == DBNull.Value) { tx_bpud_BPOD2.Text = ""; } else { tx_bpud_BPOD2.Text = DAC.GetDateTimeValue(CurRow["bpud_BPOD2"]).ToString(sys_DateFormat); }  //採購日二
      tx_bpud_BPCUS.Text = DAC.GetStringValue(CurRow["bpud_BPCUS"]);  //供應商一
      tx_bdlr_BPCUS.Text = DAC.GetStringValue(CurRow["bdlr_BPCUS"]);  //供應商一
      tx_bpud_BPCU2.Text = DAC.GetStringValue(CurRow["bpud_BPCU2"]);  //供應商二
      tx_bdlr_BPCU2.Text = DAC.GetStringValue(CurRow["bdlr_BPCU2"]);  //供應商二
      tx_bpud_BPDSN.Text = DAC.GetStringValue(CurRow["bpud_BPDSN"]);  //設計人員
      tx_bpud_BPSSN.Text = DAC.GetStringValue(CurRow["bpud_BPSSN"]);  //企劃人員
      tx_bpud_BPFLG.Text = DAC.GetStringValue(CurRow["bpud_BPFLG"]);  //功能旗標
      tx_bpud_BPRMK.Text = DAC.GetStringValue(CurRow["bpud_BPRMK"]);  //備註說明
      //
      tx_bpud_BPNPC.Text = DAC.GetStringValue(CurRow["bpud_BPNPC"]);  //標準進價
      tx_bpud_BPSPC.Text = DAC.GetStringValue(CurRow["bpud_BPSPC"]);  //最近進價
      tx_bpud_BPVPC.Text = DAC.GetStringValue(CurRow["bpud_BPVPC"]);  //平均進價
      tx_bpud_BPGPC.Text = DAC.GetStringValue(CurRow["bpud_BPGPC"]);  //參考進價
      tx_bpud_BPDE3.Text = DAC.GetStringValue(CurRow["bpud_BPDE3"]);  //批發Ｃ價
      tx_bpud_BPDE4.Text = DAC.GetStringValue(CurRow["bpud_BPDE4"]);  //批發Ｄ價
      tx_bpud_BPDE5.Text = DAC.GetStringValue(CurRow["bpud_BPDE5"]);  //批發Ｅ價
      tx_bpud_BPDT3.Text = DAC.GetStringValue(CurRow["bpud_BPDT3"]);  //進貨日期
      tx_bpud_BPDT4.Text = DAC.GetStringValue(CurRow["bpud_BPDT4"]);  //銷貨日期
      tx_bpud_BPDT5.Text = DAC.GetStringValue(CurRow["bpud_BPDT5"]);  //盤點日期
      tx_bpud_BPMND.Text = DAC.GetStringValue(CurRow["bpud_BPMND"]);  //外幣進價
      tx_bpud_BPMNR.Text = DAC.GetStringValue(CurRow["bpud_BPMNR"]);  //外幣倍數
      dr_bpud_BPMNY = sFN.SetDropDownList(ref dr_bpud_BPMNY, DAC.GetStringValue(CurRow["bpud_BPMNY"]));  //外幣幣別
      tx_bpud_BPCUQ.Text = DAC.GetStringValue(CurRow["bpud_BPCUQ"]);  //經濟採量
      dr_bpud_BPUN1 = sFN.SetDropDownList(ref dr_bpud_BPUN1, DAC.GetStringValue(CurRow["bpud_BPUN1"]));  //進貨單位
      dr_bpud_BPUN3 = sFN.SetDropDownList(ref dr_bpud_BPUN3, DAC.GetStringValue(CurRow["bpud_BPUN3"]));  //銷貨單位
      tx_bpud_BPQTM.Text = DAC.GetStringValue(CurRow["bpud_BPQTM"]);  //調撥數量
      tx_bpud_BPQTH.Text = DAC.GetStringValue(CurRow["bpud_BPQTH"]);  //最高量
      tx_bpud_BPQTL.Text = DAC.GetStringValue(CurRow["bpud_BPQTL"]);  //最低量
      tx_bpud_BPQTS.Text = DAC.GetStringValue(CurRow["bpud_BPQTS"]);  //安全量
      if (CurRow["bpud_BPDT2"] == DBNull.Value) { tx_bpud_BPDT2.Text = ""; } else { tx_bpud_BPDT2.Text = DAC.GetDateTimeValue(CurRow["bpud_BPDT2"]).ToString(sys_DateFormat); }  //修改日期
      if (CurRow["bpud_BPET1"] == DBNull.Value) { tx_bpud_BPET1.Text = ""; } else { tx_bpud_BPET1.Text = DAC.GetDateTimeValue(CurRow["bpud_BPET1"]).ToString(sys_DateFormat); }  //停進貨日
      if (CurRow["bpud_BPETM"] == DBNull.Value) { tx_bpud_BPETM.Text = ""; } else { tx_bpud_BPETM.Text = DAC.GetDateTimeValue(CurRow["bpud_BPETM"]).ToString(sys_DateFormat); }  //停調貨日
      if (CurRow["bpud_BPET3"] == DBNull.Value) { tx_bpud_BPET3.Text = ""; } else { tx_bpud_BPET3.Text = DAC.GetDateTimeValue(CurRow["bpud_BPET3"]).ToString(sys_DateFormat); }  //停銷貨日
      //
      //dr_bpud_BPCL2 = sFN.SetDropDownList(ref dr_bpud_BPCL2, DAC.GetStringValue(CurRow["bpud_BPCL2"]));  //進貨屬性
      //
      //ht_bpud_BPDSC.Text = Server.HtmlDecode(sFN.ReadTextFile(tx_bpud_BPNUM.Text.Trim() + "_BPDSC", sys_DocFilePath + st_object_func + @"\"));   //成份說明
      //ht_bpud_BPWSH.Text = Server.HtmlDecode(sFN.ReadTextFile(tx_bpud_BPNUM.Text.Trim() + "_BPWSH", sys_DocFilePath + st_object_func + @"\"));   //洗滌說明
      ht_bpud_BPDSC.Text = DAC.GetStringValue(CurRow["bpud_BPDSC"]); //成份說明
      ht_bpud_BPDSCC.Text = DAC.GetStringValue(CurRow["bpud_BPDSCC"]); //成份說明
      ht_bpud_BPDSCE.Text = DAC.GetStringValue(CurRow["bpud_BPDSCE"]); //成份說明
      ht_bpud_BPWSH.Text = DAC.GetStringValue(CurRow["bpud_BPWSH"]); //洗滌說明
      ht_bpud_BPWSHC.Text = DAC.GetStringValue(CurRow["bpud_BPWSHC"]); //洗滌說明
      ht_bpud_BPWSHE.Text = DAC.GetStringValue(CurRow["bpud_BPWSHE"]); //洗滌說明
      //
      //圖一
      string vPICNAME = "";
      int voWinW = 0, voWinH = 0;
      vPICNAME = sFN.GetPicName(tx_bpud_BPNUM.Text, sys_DocFilePath + st_object_func + @"\", true);
      if (vPICNAME != "")
      {
        ig_dcnews_DCPC1.ImageUrl = sys_HttpFilePath + st_object_func + @"/" + vPICNAME;
        sFN.ImageFitX(ref ig_dcnews_DCPC1, sys_DocFilePath + st_object_func + @"\" + vPICNAME, 200, 200);
        sFN.ImageFitWH(sys_DocFilePath + st_object_func + @"\" + vPICNAME, ref voWinW, ref voWinH);
        voWinW += 50;
        voWinH += 50;
        if (voWinW < 200) { voWinW = 200; }
        if (voWinH < 200) { voWinH = 200; }

        string url = "'" + sys_HttpAppRootPath + "/Forms/Dialog/WinPicDialog.aspx?Pic=" + ig_dcnews_DCPC1.ImageUrl + "'";
        ig_dcnews_DCPC1.Attributes.Add("onclick", "javascript:MsgDialog(" + url + ",'" + voWinW.ToString() + "','" + voWinH.ToString() + "')");
      }
      else
      {
        ig_dcnews_DCPC1.ImageUrl = sys_HttpFilePath + @"/";
        ig_dcnews_DCPC1.Attributes.Remove("onclick");
      }

      //
      hh_mkey.Value = DAC.GetStringValue(CurRow["bpud_mkey"]);
      //
    }

    private void BindNew(bool bl_showdata)
    {
      string SelDataKey = "";
      DataRow[] SelDataRow;
      DataRow CurRow;
      //
      try
      {
        CmdQueryS = (OleDbCommand)Session["fmbpud_CmdQueryS"];
      }
      catch
      {
        CmdQueryS.CommandText = "";
      }
      //
      DataTable tb_bpud = new DataTable();
      DAC_bpud bpudDao = new DAC_bpud(conn);
      OleDbDataAdapter ad_DataDataAdapter;
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      ad_DataDataAdapter = bpudDao.GetDataAdapter(ApVer, "UNbpud", "bpud", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, " bpnum ");
      ad_DataDataAdapter.Fill(tb_bpud);
      //
      if (tb_bpud.Rows.Count > 0)
      {
        //bt_05.OnClientClick = "return btnDEL_c()";
        //bt_04.OnClientClick = "return btnMOD_c()";
      }
      else
      {
        //bt_05.OnClientClick = "return btnDEL0_c()";
        //bt_04.OnClientClick = "return btnMOD0_c()";
      }
      gr_GridView_bpud.DataSource = tb_bpud;
      //fmsn101_GV1_SelectedIndex
      //fmsn101_GV1_PageIndex
      gr_GridView_bpud = clsGV.BindGridView(gr_GridView_bpud, tb_bpud, hh_GridCtrl, ref hh_GridGkey, "fmbpud_gr_GridView_bpud");
      gr_GridView_bpud.DataBind();
      SelDataKey = "bpud_gkey='" + hh_GridGkey.Value + "'";
      SelDataRow = tb_bpud.Select(SelDataKey);
      //
      if (bl_showdata)
      {
        if (SelDataRow.Length == 1)
        {
          CurRow = SelDataRow[0];
          Session["fmbpud_gr_GridView_bpud_GridGkey"] = hh_GridGkey.Value;
          BindText(CurRow);
        }
        else
        {
          hh_GridCtrl.Value = "init";
          ClearText();
        }
      }
      tb_bpud.Dispose();
      bpudDao.Dispose();
    }

    protected void gr_GridView_bpud_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gr_GridView_bpud.PageIndex = e.NewPageIndex;
      Session["fmbpud_gr_GridView_bpud_PageIndex"] = gr_GridView_bpud.PageIndex;
      hh_GridGkey.Value = gr_GridView_bpud.DataKeys[gr_GridView_bpud.SelectedIndex].Value.ToString();
    }

    protected void gr_GridView_bpud_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindNew(true);
      Session["fmbpud_gr_GridView_bpud_PageIndex"] = gr_GridView_bpud.PageIndex;
      Session["fmbpud_gr_GridView_bpud_SelectedIndex"] = gr_GridView_bpud.SelectedIndex;
      hh_GridGkey.Value = gr_GridView_bpud.DataKeys[gr_GridView_bpud.SelectedIndex].Value.ToString();
      SetSerMod();
    }

    protected void gr_GridView_bpud_PageIndexChanged(object sender, EventArgs e)
    {
      if (gr_GridView_bpud.Enabled)
      {
        hh_GridCtrl.Value = "ser";
        BindNew(true);
        SetSerMod();
        Session["fmbpud_gr_GridView_bpud_PageIndex"] = gr_GridView_bpud.PageIndex;
        Session["fmbpud_gr_GridView_bpud_SelectedIndex"] = gr_GridView_bpud.SelectedIndex;
      }
      else
      {
        li_Msg.Text = "<script> alert('" + StringTable.GetString("請先處理資料輸入") + "'); </script>";
      }
    }

    protected void bt_02_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actNEW();
    }

    protected void actNEW()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 2, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "ins";
        Set_Control();
        ClearText();
        //定義guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(false);
        SetEditMod();
        WebTab1.SelectedIndex = 0;
        tx_bpud_BPDT1.Value = sFN.DateTimeToDateString(DateTime.Today, sys_DateFormat);
        li_Msg.Text = "<script> document.all('" + st_ContentPlaceHolder + "WebTab1$tmpl0$" + "tx_bpud_BPNUM" + "').focus(); </script>";
      }
    }

    protected void bt_04_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actMOD();
    }

    protected void actMOD()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 4, UserLoginGkey, ref li_AccMsg))
      {
        hh_GridCtrl.Value = "mod";
        Set_Control();
        //取Act guidkey
        hh_ActKey.Value = DAC.get_guidkey();
        BindNew(true);
        SetEditMod();
        WebTab1.SelectedIndex = 0;
        tx_bpud_BPNUM.Enabled = false;
        li_Msg.Text = "<script> document.all('" + st_ContentPlaceHolder + "WebTab1$tmpl0$" + "tx_bpud_BPNUB" + "').focus(); </script>";
      }
    }

    protected void bt_QUT_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      Response.Redirect("~/Master/" + Page.Theme + "/MainForm.aspx");
    }

    protected void bt_CAN_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actCAN();
    }

    protected void actCAN()
    {
      hh_GridCtrl.Value = "ser";
      Set_Control();
      ClearText();
      BindNew(true);
      SetSerMod();
    }

    protected void bt_05_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actDEL();
    }

    protected void actDEL()
    {
      if (sFN.checkAccessFunc(UserGkey, st_object_func, 5, UserLoginGkey, ref li_AccMsg))
      {
        bool bl_delok = false;
        Set_Control();
        hh_ActKey.Value = DAC.get_guidkey();
        //
        DAC_bpud bpudDao = new DAC_bpud(conn);
        string st_addselect = "";
        string st_addjoin = "";
        string st_addunion = "";
        string st_SelDataKey = "bpud_gkey='" + hh_GridGkey.Value + "' and bpud_mkey='" + hh_mkey.Value + "' ";
        DataTable tb_bpud = new DataTable();
        DbDataAdapter da_ADP = bpudDao.GetDataAdapter(ApVer, "UNbpud", "bpud", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
        da_ADP.Fill(tb_bpud);
        DataRow[] DelRow = tb_bpud.Select(st_SelDataKey);
        if (DelRow.Length == 1)
        {
          conn.Open();
          OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
          da_ADP.DeleteCommand.Transaction = thistran;
          try
          {
            bpudDao.Insertbalog(conn, thistran, "bpud", hh_ActKey.Value, hh_GridGkey.Value);
            bpudDao.Insertbtlog(conn, thistran, "bpud", DAC.GetStringValue(DelRow[0]["bpud_gkey"]), "D", UserName, DAC.GetStringValue(DelRow[0]["bpud_gkey"]));
            DelRow[0].Delete();
            da_ADP.Update(tb_bpud);
            thistran.Commit();
            bl_delok = true;
          }
          catch (Exception e)
          {
            thistran.Rollback();
            bl_delok = false;
            lb_ErrorMessage.Visible = true;
            lb_ErrorMessage.Text = e.Message;
          }
          finally
          {
            thistran.Dispose();
            bpudDao.Dispose();
            tb_bpud.Dispose();
            da_ADP.Dispose();
            conn.Close();
          }
        }
        else
        {
          bl_delok = false;
          lb_ErrorMessage.Visible = true;
          lb_ErrorMessage.Text = StringTable.GetString("資料已變更,請重新選取!");
        }
        tb_bpud.Clear();
        //
        if (bl_delok)
        {
          gr_GridView_bpud = clsGV.SetGridCursor("del", gr_GridView_bpud, -2);
          BindNew(true);
          SetSerMod();
        }
        //bl_delok
      }
    }

    protected void bt_SAV_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
      actSAV();
    }

    protected void actSAV()
    {
      string st_ckerrmsg = "";
      string st_dberrmsg = "";
      string st_tempgkey = "";
      bool bl_insok = false, bl_updateok = false;
      //
      Set_Control();
      if (ServerEditCheck(ref st_ckerrmsg))
      {
        DAC_bpud bpudDao = new DAC_bpud(conn);
        if (hh_GridCtrl.Value.ToLower() == "modall")
        {
          if (UpdateDataAll(hh_ActKey.Value, ref st_dberrmsg))
          {
            hh_GridCtrl.Value = "rekey";
            BindNew(true);
            SetSerMod();
            hh_GridCtrl.Value = "ser";
          }
          else
          {
            lb_ErrorMessage.Visible = true;
            lb_ErrorMessage.Text = st_dberrmsg;
          }
        }  //
        else
        {
          string st_addselect = "";
          string st_addjoin = "";
          string st_addunion = "";
          string st_SelDataKey = "bpud_gkey='" + hh_GridGkey.Value + "'";
          if (hh_GridCtrl.Value.ToLower() == "ins")
          {
            //檢查重複
            if (bpudDao.IsExists("bpud", "bpnum", tx_bpud_BPNUM.Text, ""))
            {
              bl_insok = false;
              st_dberrmsg = StringTable.GetString(tx_bpud_BPNUM.Text + ",已存在.");
            }
            else
            {
              DataTable tb_bpud_ins = new DataTable();
              DbDataAdapter da_ADP_ins = bpudDao.GetDataAdapter(ApVer, "UNbpud", "bpud", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_ins.Fill(tb_bpud_ins);
              conn.Open();
              OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
              da_ADP_ins.InsertCommand.Transaction = thistran;
              try
              {
                DataRow ins_row = tb_bpud_ins.NewRow();
                st_tempgkey = DAC.get_guidkey();
                ins_row["bpud_gkey"] = st_tempgkey;       // 
                ins_row["bpud_mkey"] = DAC.get_guidkey(); //
                ins_row["bpud_BPNUM"] = tx_bpud_BPNUM.Text.Trim();       // 商品編號
                ins_row["bpud_BPNUB"] = tx_bpud_BPNUB.Text.Trim();       // 條碼編號
                ins_row["bpud_BPLAB"] = dr_bpud_BPLAB.SelectedValue;       // 品牌名稱
                ins_row["bpud_BPYES"] = tx_bpud_BPYES.Text.Trim();       // 年度季節
                ins_row["bpud_BPTNA"] = tx_bpud_BPTNA.Text.Trim();       // 中文名稱
                ins_row["bpud_BPCNA"] = tx_bpud_BPCNA.Text.Trim();       // 簡體名稱
                ins_row["bpud_BPENA"] = tx_bpud_BPENA.Text.Trim();       // 英文名稱
                ins_row["bpud_BPCLA"] = tx_bpud_BPCLA.Text.Trim();       // 規　　格
                ins_row["bpud_BPDP1"] = dr_bpud_BPDP1.SelectedValue;       // 分類一
                ins_row["bpud_BPDP2"] = dr_bpud_BPDP2.SelectedValue;       // 分類二
                ins_row["bpud_BPDP3"] = dr_bpud_BPDP3.SelectedValue;       // 分類三
                ins_row["bpud_BPDP4"] = dr_bpud_BPDP4.SelectedValue;       // 分類四
                ins_row["bpud_BPDX1"] = sFN.getWebDropDownListSelectedString(dr_bpud_BPDX1);       // 分類一
                ins_row["bpud_BPDX2"] = sFN.getWebDropDownListSelectedString(dr_bpud_BPDX2);       // 分類二
                ins_row["bpud_BPDX3"] = sFN.getWebDropDownListSelectedString(dr_bpud_BPDX3);       // 分類三
                ins_row["bpud_BPDX4"] = sFN.getWebDropDownListSelectedString(dr_bpud_BPDX4);       // 分類四
                ins_row["bpud_BPMDC"] = dr_bpud_BPMDC.SelectedValue;       // 產　　地
                ins_row["bpud_BPUNI"] = dr_bpud_BPUNI.SelectedValue;       // 單位
                ins_row["bpud_BPNCR"] = tx_bpud_BPNCR.Text.Trim();       // 款式編號
                ins_row["bpud_BPCLR"] = tx_bpud_BPCLR.Text.Trim();       // 顏色代號
                ins_row["bpud_BPCLN"] = tx_bpud_BPCLN.Text.Trim();       // 顏色名稱
                ins_row["bpud_BPSIZ"] = tx_bpud_BPSIZ.Text.Trim();       // 尺寸名稱
                ins_row["bpud_BPDE1"] = tx_bpud_BPDE1.Text.Trim();       // 售價含稅
                ins_row["bpud_BPDE2"] = tx_bpud_BPDE2.Text.Trim();       // 會員含稅
                if (tx_bpud_BPDT1.Text.Trim() == "") { ins_row["bpud_BPDT1"] = DBNull.Value; } else { ins_row["bpud_BPDT1"] = tx_bpud_BPDT1.Text.Trim(); }       //上架日期
                if (tx_bpud_BPETQ.Text.Trim() == "") { ins_row["bpud_BPETQ"] = DBNull.Value; } else { ins_row["bpud_BPETQ"] = tx_bpud_BPETQ.Text.Trim(); }       //下架日期
                if (tx_bpud_BPOD1.Text.Trim() == "") { ins_row["bpud_BPOD1"] = DBNull.Value; } else { ins_row["bpud_BPOD1"] = tx_bpud_BPOD1.Text.Trim(); }       //採購日一
                if (tx_bpud_BPOD2.Text.Trim() == "") { ins_row["bpud_BPOD2"] = DBNull.Value; } else { ins_row["bpud_BPOD2"] = tx_bpud_BPOD2.Text.Trim(); }       //採購日二
                ins_row["bpud_BPCUS"] = tx_bpud_BPCUS.Text.Trim();       // 供應商一
                ins_row["bpud_BPCU2"] = tx_bpud_BPCU2.Text.Trim();       // 供應商二
                ins_row["bpud_BPDSN"] = tx_bpud_BPDSN.Text.Trim();       // 設計人員
                ins_row["bpud_BPSSN"] = tx_bpud_BPSSN.Text.Trim();       // 企劃人員
                ins_row["bpud_BPFLG"] = tx_bpud_BPFLG.Text.Trim();       // 功能旗標
                ins_row["bpud_BPRMK"] = tx_bpud_BPRMK.Text.Trim();       // 備註說明
                //
                ins_row["bpud_BPNPC"] = tx_bpud_BPNPC.Text.Trim();       // 標準進價
                ins_row["bpud_BPSPC"] = tx_bpud_BPSPC.Text.Trim();       // 最近進價
                ins_row["bpud_BPVPC"] = tx_bpud_BPVPC.Text.Trim();       // 平均進價
                ins_row["bpud_BPGPC"] = tx_bpud_BPGPC.Text.Trim();       // 參考進價
                ins_row["bpud_BPDE3"] = tx_bpud_BPDE3.Text.Trim();       // 批發Ｃ價
                ins_row["bpud_BPDE4"] = tx_bpud_BPDE4.Text.Trim();       // 批發Ｄ價
                ins_row["bpud_BPDE5"] = tx_bpud_BPDE5.Text.Trim();       // 批發Ｅ價
                ins_row["bpud_BPMND"] = tx_bpud_BPMND.Text.Trim();       // 外幣進價
                ins_row["bpud_BPMNR"] = tx_bpud_BPMNR.Text.Trim();       // 外幣倍數
                ins_row["bpud_BPMNY"] = dr_bpud_BPMNY.SelectedValue;       // 外幣幣別
                ins_row["bpud_BPCUQ"] = tx_bpud_BPCUQ.Text.Trim();       // 經濟採量
                ins_row["bpud_BPUN1"] = dr_bpud_BPUN1.SelectedValue;       // 進貨單位
                ins_row["bpud_BPUN3"] = dr_bpud_BPUN3.SelectedValue;       // 銷貨單位
                ins_row["bpud_BPQTM"] = tx_bpud_BPQTM.Text.Trim();       // 調撥數量
                ins_row["bpud_BPQTH"] = tx_bpud_BPQTH.Text.Trim();       // 最高量
                ins_row["bpud_BPQTL"] = tx_bpud_BPQTL.Text.Trim();       // 最低量
                ins_row["bpud_BPQTS"] = tx_bpud_BPQTS.Text.Trim();       // 安全量
                if (tx_bpud_BPET1.Text.Trim() == "") { ins_row["bpud_BPET1"] = DBNull.Value; } else { ins_row["bpud_BPET1"] = sFN.DateStringToDateTime(tx_bpud_BPET1.Text); }       //停進貨日
                if (tx_bpud_BPETM.Text.Trim() == "") { ins_row["bpud_BPETM"] = DBNull.Value; } else { ins_row["bpud_BPETM"] = sFN.DateStringToDateTime(tx_bpud_BPETM.Text); }       //停調貨日
                if (tx_bpud_BPET3.Text.Trim() == "") { ins_row["bpud_BPET3"] = DBNull.Value; } else { ins_row["bpud_BPET3"] = sFN.DateStringToDateTime(tx_bpud_BPET3.Text); }       //停銷貨日
                //
                ins_row["bpud_BPCL2"] = "01";       // 進貨屬性
                ins_row["bpud_BPDSC"] = ht_bpud_BPDSC.Text;       //成份說明
                ins_row["bpud_BPDSCC"] = ht_bpud_BPDSCC.Text;       //成份說明
                ins_row["bpud_BPDSCE"] = ht_bpud_BPDSCE.Text;       //成份說明
                ins_row["bpud_BPWSH"] = ht_bpud_BPWSH.Text;       //使用說明
                ins_row["bpud_BPWSHC"] = ht_bpud_BPWSHC.Text;       //使用說明
                ins_row["bpud_BPWSHE"] = ht_bpud_BPWSHE.Text;       //使用說明
                //
                ins_row["bpud_trusr"] = UserGkey;  //
                tb_bpud_ins.Rows.Add(ins_row);
                //
                //
                da_ADP_ins.Update(tb_bpud_ins);
                bpudDao.Insertbalog(conn, thistran, "bpud", hh_ActKey.Value, hh_GridGkey.Value);
                bpudDao.Insertbtlog(conn, thistran, "bpud", DAC.GetStringValue(ins_row["bpud_gkey"]), "I", UserName, DAC.GetStringValue(ins_row["bpud_gkey"]));
                //sFN.WriteTextFile(tx_bpud_BPNUM.Text.Trim() + "_BPDSC", sys_DocFilePath + st_object_func + @"\", Server.HtmlEncode(ht_bpud_BPDSC.Text));  // 成份說明
                //sFN.WriteTextFile(tx_bpud_BPNUM.Text.Trim() + "_BPWSH", sys_DocFilePath + st_object_func + @"\", Server.HtmlEncode(ht_bpud_BPWSH.Text));  // 洗滌說明
                thistran.Commit();
                bl_insok = true;
              }
              catch (Exception e)
              {
                thistran.Rollback();
                bl_insok = false;
                st_dberrmsg = e.Message;
              }
              finally
              {
                thistran.Dispose();
                bpudDao.Dispose();
                tb_bpud_ins.Dispose();
                da_ADP_ins.Dispose();
                conn.Close();
              }
            }
            if (bl_insok)
            {
              hh_GridGkey.Value = st_tempgkey;
              hh_GridCtrl.Value = "rekey";
              BindNew(true);
              hh_GridCtrl.Value = "ser";
              SetSerMod();
            }
            else
            {
              lb_ErrorMessage.Text = st_dberrmsg;
              lb_ErrorMessage.Visible = true;
            } //bl_insok
          }  //ins
          else if (hh_GridCtrl.Value.ToLower() == "mod")
          {
            if (bpudDao.IsExists("bpud", "bpnum", tx_bpud_BPNUM.Text, "gkey<>'" + hh_GridGkey.Value + "'"))
            {
              bl_updateok = false;
              st_dberrmsg = StringTable.GetString(tx_bpud_BPNUM.Text + ",已存在.");
            }
            else
            {
              DataTable tb_bpud_mod = new DataTable();
              DbDataAdapter da_ADP_mod = bpudDao.GetDataAdapter(ApVer, "UNbpud", "bpud", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
              da_ADP_mod.Fill(tb_bpud_mod);
              st_SelDataKey = "bpud_gkey='" + hh_GridGkey.Value + "' and bpud_mkey='" + hh_mkey.Value + "' ";
              DataRow[] mod_rows = tb_bpud_mod.Select(st_SelDataKey);
              DataRow mod_row;
              if (mod_rows.Length != 1)
              {
                bl_updateok = false;
                st_dberrmsg = StringTable.GetString("資料已變更,請重新選取!");
              }
              else
              {
                conn.Open();
                OleDbTransaction thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                da_ADP_mod.UpdateCommand.Transaction = thistran;
                try
                {
                  mod_row = mod_rows[0];
                  mod_row.BeginEdit();

                  mod_row["bpud_BPNUB"] = tx_bpud_BPNUB.Text.Trim();       // 條碼編號
                  mod_row["bpud_BPLAB"] = dr_bpud_BPLAB.SelectedValue;       // 品牌名稱
                  mod_row["bpud_BPYES"] = tx_bpud_BPYES.Text.Trim();       // 年度季節
                  mod_row["bpud_BPTNA"] = tx_bpud_BPTNA.Text.Trim();       // 中文名稱
                  mod_row["bpud_BPCNA"] = tx_bpud_BPCNA.Text.Trim();       // 簡體名稱
                  mod_row["bpud_BPENA"] = tx_bpud_BPENA.Text.Trim();       // 英文名稱
                  mod_row["bpud_BPCLA"] = tx_bpud_BPCLA.Text.Trim();       // 規　　格
                  mod_row["bpud_BPDP1"] = dr_bpud_BPDP1.SelectedValue;       // 分類一
                  mod_row["bpud_BPDP2"] = dr_bpud_BPDP2.SelectedValue;       // 分類二
                  mod_row["bpud_BPDP3"] = dr_bpud_BPDP3.SelectedValue;       // 分類三
                  mod_row["bpud_BPDP4"] = dr_bpud_BPDP4.SelectedValue;       // 分類四
                  mod_row["bpud_BPDX1"] = sFN.getWebDropDownListSelectedString(dr_bpud_BPDX1);       // 分類四
                  mod_row["bpud_BPDX2"] = sFN.getWebDropDownListSelectedString(dr_bpud_BPDX2);       // 分類四
                  mod_row["bpud_BPDX3"] = sFN.getWebDropDownListSelectedString(dr_bpud_BPDX3);       // 分類四
                  mod_row["bpud_BPDX4"] = sFN.getWebDropDownListSelectedString(dr_bpud_BPDX4);       // 分類四
                  mod_row["bpud_BPMDC"] = dr_bpud_BPMDC.SelectedValue;       // 產　　地
                  mod_row["bpud_BPUNI"] = dr_bpud_BPUNI.SelectedValue;       // 單位
                  mod_row["bpud_BPNCR"] = tx_bpud_BPNCR.Text.Trim();       // 款式編號
                  mod_row["bpud_BPCLR"] = tx_bpud_BPCLR.Text.Trim();       // 顏色代號
                  mod_row["bpud_BPCLN"] = tx_bpud_BPCLN.Text.Trim();       // 顏色名稱
                  mod_row["bpud_BPSIZ"] = tx_bpud_BPSIZ.Text.Trim();       // 尺寸名稱
                  mod_row["bpud_BPDE1"] = tx_bpud_BPDE1.Text.Trim();       // 售價含稅
                  mod_row["bpud_BPDE2"] = tx_bpud_BPDE2.Text.Trim();       // 會員含稅

                  if (tx_bpud_BPDT1.Text.Trim() == "") { mod_row["bpud_BPDT1"] = DBNull.Value; } else { mod_row["bpud_BPDT1"] = tx_bpud_BPDT1.Text.Trim(); }       //上架日期
                  if (tx_bpud_BPETQ.Text.Trim() == "") { mod_row["bpud_BPETQ"] = DBNull.Value; } else { mod_row["bpud_BPETQ"] = tx_bpud_BPETQ.Text.Trim(); }       //下架日期
                  mod_row["bpud_BPCUS"] = tx_bpud_BPCUS.Text.Trim();       // 供應商一
                  if (tx_bpud_BPOD1.Text.Trim() == "") { mod_row["bpud_BPOD1"] = DBNull.Value; } else { mod_row["bpud_BPOD1"] = sFN.DateStringToDateTime(tx_bpud_BPOD1.Text); }       //採購日一
                  mod_row["bpud_BPCU2"] = tx_bpud_BPCU2.Text.Trim();       // 供應商二
                  if (tx_bpud_BPOD2.Text.Trim() == "") { mod_row["bpud_BPOD2"] = DBNull.Value; } else { mod_row["bpud_BPOD2"] = sFN.DateStringToDateTime(tx_bpud_BPOD2.Text); }       //採購日二
                  mod_row["bpud_BPDSN"] = tx_bpud_BPDSN.Text.Trim();       // 設計人員
                  mod_row["bpud_BPSSN"] = tx_bpud_BPSSN.Text.Trim();       // 企劃人員
                  mod_row["bpud_BPFLG"] = tx_bpud_BPFLG.Text.Trim();       // 功能旗標
                  mod_row["bpud_BPRMK"] = tx_bpud_BPRMK.Text.Trim();       // 備註說明
                  //
                  mod_row["bpud_BPNPC"] = tx_bpud_BPNPC.Text.Trim();       // 標準進價
                  mod_row["bpud_BPSPC"] = tx_bpud_BPSPC.Text.Trim();       // 最近進價
                  mod_row["bpud_BPVPC"] = tx_bpud_BPVPC.Text.Trim();       // 平均進價
                  mod_row["bpud_BPGPC"] = tx_bpud_BPGPC.Text.Trim();       // 參考進價
                  mod_row["bpud_BPDE3"] = tx_bpud_BPDE3.Text.Trim();       // 批發Ｃ價
                  mod_row["bpud_BPDE4"] = tx_bpud_BPDE4.Text.Trim();       // 批發Ｄ價
                  mod_row["bpud_BPDE5"] = tx_bpud_BPDE5.Text.Trim();       // 批發Ｅ價
                  mod_row["bpud_BPMND"] = tx_bpud_BPMND.Text.Trim();       // 外幣進價
                  mod_row["bpud_BPMNR"] = tx_bpud_BPMNR.Text.Trim();       // 外幣倍數
                  mod_row["bpud_BPMNY"] = dr_bpud_BPMNY.SelectedValue;       // 外幣幣別
                  mod_row["bpud_BPCUQ"] = tx_bpud_BPCUQ.Text.Trim();       // 經濟採量
                  mod_row["bpud_BPUN1"] = dr_bpud_BPUN1.SelectedValue;       // 進貨單位
                  mod_row["bpud_BPUN3"] = dr_bpud_BPUN3.SelectedValue;       // 銷貨單位
                  mod_row["bpud_BPQTM"] = tx_bpud_BPQTM.Text.Trim();       // 調撥數量
                  mod_row["bpud_BPQTH"] = tx_bpud_BPQTH.Text.Trim();       // 最高量
                  mod_row["bpud_BPQTL"] = tx_bpud_BPQTL.Text.Trim();       // 最低量
                  mod_row["bpud_BPQTS"] = tx_bpud_BPQTS.Text.Trim();       // 安全量
                  if (tx_bpud_BPDT2.Text.Trim() == "") { mod_row["bpud_BPDT2"] = DBNull.Value; } else { mod_row["bpud_BPDT2"] = sFN.DateStringToDateTime(tx_bpud_BPDT2.Text); }       //修改日期
                  if (tx_bpud_BPET1.Text.Trim() == "") { mod_row["bpud_BPET1"] = DBNull.Value; } else { mod_row["bpud_BPET1"] = sFN.DateStringToDateTime(tx_bpud_BPET1.Text); }       //停進貨日
                  if (tx_bpud_BPETM.Text.Trim() == "") { mod_row["bpud_BPETM"] = DBNull.Value; } else { mod_row["bpud_BPETM"] = sFN.DateStringToDateTime(tx_bpud_BPETM.Text); }       //停調貨日
                  if (tx_bpud_BPET3.Text.Trim() == "") { mod_row["bpud_BPET3"] = DBNull.Value; } else { mod_row["bpud_BPET3"] = sFN.DateStringToDateTime(tx_bpud_BPET3.Text); }       //停銷貨日
                  //mod_row["bpud_BPCL2"] = dr_bpud_BPCL2.SelectedValue;       // 進貨屬性
                  mod_row["bpud_BPDSC"] = ht_bpud_BPDSC.Text;       //成份說明
                  mod_row["bpud_BPDSCC"] = ht_bpud_BPDSCC.Text;       //成份說明
                  mod_row["bpud_BPDSCE"] = ht_bpud_BPDSCE.Text;       //成份說明
                  mod_row["bpud_BPWSH"] = ht_bpud_BPWSH.Text;       //使用說明
                  mod_row["bpud_BPWSHC"] = ht_bpud_BPWSHC.Text;       //使用說明
                  mod_row["bpud_BPWSHE"] = ht_bpud_BPWSHE.Text;       //使用說明
                  //
                  mod_row["bpud_mkey"] = DAC.get_guidkey();        //
                  mod_row["bpud_trusr"] = UserGkey;  //
                  mod_row.EndEdit();
                  da_ADP_mod.Update(tb_bpud_mod);
                  bpudDao.Insertbalog(conn, thistran, "bpud", hh_ActKey.Value, hh_GridGkey.Value);
                  bpudDao.Insertbtlog(conn, thistran, "bpud", DAC.GetStringValue(mod_row["bpud_gkey"]), "M", UserName, DAC.GetStringValue(mod_row["bpud_gkey"]));
                  //sFN.WriteTextFile(tx_bpud_BPNUM.Text.Trim() + "_BPDSC", sys_DocFilePath + st_object_func + @"\", Server.HtmlEncode(ht_bpud_BPDSC.Text));  // 成份說明
                  //sFN.WriteTextFile(tx_bpud_BPNUM.Text.Trim() + "_BPWSH", sys_DocFilePath + st_object_func + @"\", Server.HtmlEncode(ht_bpud_BPWSH.Text));  // 洗滌說明
                  thistran.Commit();
                  bl_updateok = true;
                }
                catch (Exception e)
                {
                  thistran.Rollback();
                  bl_updateok = false;
                  st_dberrmsg = e.Message;
                }
                finally
                {
                  thistran.Dispose();
                  bpudDao.Dispose();
                  tb_bpud_mod.Dispose();
                  da_ADP_mod.Dispose();
                  conn.Close();
                }
              } //mod_rows.Length=1
            } //IsExists
            if (bl_updateok)
            {
              //hh_GridCtrl.Value = "rekey";
              BindNew(true);
              hh_GridCtrl.Value = "ser";
              SetSerMod();
            }
            else
            {
              lb_ErrorMessage.Text = st_dberrmsg;
              lb_ErrorMessage.Visible = true;
            } //bl_updateok
          }   //mod
        }  //ins & mod
        bpudDao.Dispose();
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_ckerrmsg;
      }
    }

    private bool ServerEditCheck(ref string sMsg)
    {
      bool ret;
      ret = true;
      sMsg = "";
      clsDataCheck DataCheck = new clsDataCheck();

      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPNUM.Text, lb_bpud_BPNUM.Text, ref sMsg, PublicVariable.LangType, sFN);  //商品編號
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPTNA.Text, lb_bpud_BPTNA.Text, ref sMsg, PublicVariable.LangType, sFN);  //中文名稱
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPDE1.Text, lb_bpud_BPDE1.Text, ref sMsg, PublicVariable.LangType, sFN);  //售價含稅
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPDE2.Text, lb_bpud_BPDE2.Text, ref sMsg, PublicVariable.LangType, sFN);  //會員含稅
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPDT1.Text, lb_bpud_BPDT1.Text, ref sMsg, PublicVariable.LangType, sFN);  //上架日期
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_bpud_BPDT1.Text, lb_bpud_BPDT1.Text, ref sMsg, PublicVariable.LangType, sFN); //上架日期
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_bpud_BPETQ.Text, lb_bpud_BPETQ.Text, ref sMsg, PublicVariable.LangType, sFN); //下架日期
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_bpud_BPOD1.Text, lb_bpud_BPOD1.Text, ref sMsg, PublicVariable.LangType, sFN); //採購日一
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_bpud_BPOD2.Text, lb_bpud_BPOD2.Text, ref sMsg, PublicVariable.LangType, sFN); //採購日二
      //
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPNPC.Text, lb_bpud_BPNPC.Text, ref sMsg, PublicVariable.LangType, sFN); //標準進價
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPSPC.Text, lb_bpud_BPSPC.Text, ref sMsg, PublicVariable.LangType, sFN); //最近進價
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPVPC.Text, lb_bpud_BPVPC.Text, ref sMsg, PublicVariable.LangType, sFN); //平均進價
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPGPC.Text, lb_bpud_BPGPC.Text, ref sMsg, PublicVariable.LangType, sFN); //參考進價
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPDE3.Text, lb_bpud_BPDE3.Text, ref sMsg, PublicVariable.LangType, sFN); //批發Ｃ價
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPDE4.Text, lb_bpud_BPDE4.Text, ref sMsg, PublicVariable.LangType, sFN); //批發Ｄ價
      ret = DataCheck.cIsStrEmptyChk(ret, tx_bpud_BPDE5.Text, lb_bpud_BPDE5.Text, ref sMsg, PublicVariable.LangType, sFN); //批發Ｅ價

      ret = DataCheck.cIsStrDecimalChk(ret, tx_bpud_BPMND.Text, lb_bpud_BPMND.Text, ref sMsg, PublicVariable.LangType, sFN); //外幣進價
      ret = DataCheck.cIsStrDecimalChk(ret, tx_bpud_BPMNR.Text, lb_bpud_BPMNR.Text, ref sMsg, PublicVariable.LangType, sFN); //外幣倍數
      ret = DataCheck.cIsStrDecimalChk(ret, tx_bpud_BPCUQ.Text, lb_bpud_BPCUQ.Text, ref sMsg, PublicVariable.LangType, sFN); //經濟採量
      ret = DataCheck.cIsStrDecimalChk(ret, tx_bpud_BPQTM.Text, lb_bpud_BPQTM.Text, ref sMsg, PublicVariable.LangType, sFN); //調撥數量
      ret = DataCheck.cIsStrDecimalChk(ret, tx_bpud_BPQTH.Text, lb_bpud_BPQTH.Text, ref sMsg, PublicVariable.LangType, sFN); //最高量
      ret = DataCheck.cIsStrDecimalChk(ret, tx_bpud_BPQTL.Text, lb_bpud_BPQTL.Text, ref sMsg, PublicVariable.LangType, sFN); //最低量
      ret = DataCheck.cIsStrDecimalChk(ret, tx_bpud_BPQTS.Text, lb_bpud_BPQTS.Text, ref sMsg, PublicVariable.LangType, sFN); //安全量
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_bpud_BPDT2.Text, lb_bpud_BPDT2.Text, ref sMsg, PublicVariable.LangType, sFN); //修改日期
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_bpud_BPET1.Text, lb_bpud_BPET1.Text, ref sMsg, PublicVariable.LangType, sFN); //停進貨日
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_bpud_BPETM.Text, lb_bpud_BPETM.Text, ref sMsg, PublicVariable.LangType, sFN); //停調貨日
      ret = DataCheck.cIsStrDatetimeChk(ret, tx_bpud_BPET3.Text, lb_bpud_BPET3.Text, ref sMsg, PublicVariable.LangType, sFN); //停銷貨日

      DataCheck.Dispose();
      return ret;
    }

    protected void gr_GridView_bpud_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      string st_datavalue = "";
      if (e.Row.RowIndex >= 0)
      {
        DataRowView rowView = (DataRowView)e.Row.DataItem;
        //上架日期
        if (e.Row.FindControl("tx_bpud_BPDT102") != null)
        {
          Label tx_bpud_BPDT102 = e.Row.FindControl("tx_bpud_BPDT102") as Label;
          st_datavalue = sFN.datetostr(DAC.GetDateTimeValue(rowView["bpud_BPDT1"]));
          tx_bpud_BPDT102.Text = st_datavalue;
        }
        //下架日期
        if (e.Row.FindControl("tx_bpud_BPETQ02") != null)
        {
          Label tx_bpud_BPETQ02 = e.Row.FindControl("tx_bpud_BPETQ02") as Label;
          if (rowView["bpud_BPETQ"] == DBNull.Value)
          {
            st_datavalue = "";
          }
          else
          {
            st_datavalue = sFN.datetostr(DAC.GetDateTimeValue(rowView["bpud_BPETQ"]));
          }
          tx_bpud_BPETQ02.Text = st_datavalue;
        }
        //售價含稅
        if (e.Row.FindControl("tx_bpud_BPDE102") != null)
        {
          Infragistics.Web.UI.EditorControls.WebNumericEditor tx_bpud_BPDE102 = e.Row.FindControl("tx_bpud_BPDE102") as Infragistics.Web.UI.EditorControls.WebNumericEditor;
          st_datavalue = DAC.GetStringValue(rowView["bpud_BPDE1"]).Trim();
          tx_bpud_BPDE102.MinDecimalPlaces = cin_TDE1;
          tx_bpud_BPDE102.Value = DAC.GetDecimalValue(st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_bpud_BPDE102, true); } else { clsGV.TextBox_Set(ref tx_bpud_BPDE102, false); }
        }
        //會員含稅
        if (e.Row.FindControl("tx_bpud_BPDE202") != null)
        {
          Infragistics.Web.UI.EditorControls.WebNumericEditor tx_bpud_BPDE202 = e.Row.FindControl("tx_bpud_BPDE202") as Infragistics.Web.UI.EditorControls.WebNumericEditor;
          st_datavalue = DAC.GetStringValue(rowView["bpud_BPDE2"]).Trim();
          tx_bpud_BPDE202.MinDecimalPlaces = cin_TDE1;
          tx_bpud_BPDE202.Value = DAC.GetDecimalValue(st_datavalue);
          if (hh_GridCtrl.Value == "modall") { clsGV.TextBox_Set(ref tx_bpud_BPDE202, true); } else { clsGV.TextBox_Set(ref tx_bpud_BPDE202, false); }
        }




      }
    }


    protected bool UpdateDataAll(string st_ActKey, ref string st_errmsg)
    {
      bool bl_updateok = false;
      bool bl_Mod = false;
      //
      string st_ctrl = "", st_ctrlname = "";
      string st_bpud_gkey = "", st_bpud_mkey = "", st_bpud_BPNUM = "", st_bpud_BPTNA = "", st_bpud_BPCNA = "", st_bpud_BPENA = "", st_bpud_BPCLA = "", st_bpud_BPDE1 = "", st_bpud_BPDE2 = "", st_bpud_BPDT1 = "", st_bpud_BPETQ = "";
      DataRow mod_row;
      DataRow[] sel_rows;
      //
      st_ctrl = "ctl00$ContentPlaceHolder1$gr_GridView_bpud$ctl";
      CmdQueryS.CommandText = " and 1=1 ";
      DataTable tb_bpud = new DataTable();
      DAC_bpud bpudDao = new DAC_bpud(conn);
      string st_addselect = "";
      string st_addjoin = "";
      string st_addunion = "";
      DbDataAdapter da_ADP = bpudDao.GetDataAdapter(ApVer, "UNbpud", "bpud", st_addselect, false, st_addjoin, CmdQueryS, st_addunion, "");
      da_ADP.Fill(tb_bpud);
      //
      OleDbTransaction thistran;
      conn.Open();
      thistran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
      da_ADP.UpdateCommand.Transaction = thistran;
      da_ADP.DeleteCommand.Transaction = thistran;
      da_ADP.InsertCommand.Transaction = thistran;
      try
      {
        for (int in_g = 0; in_g <= gr_GridView_bpud.Rows.Count + 4; in_g++)
        {
          //gkey
          st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_gkey02";
          if (FindControl(st_ctrlname) != null)
          {
            //gkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_gkey02";
            st_bpud_gkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //mkey
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_mkey02";
            st_bpud_mkey = ((HtmlInputHidden)FindControl(st_ctrlname)).Value.Trim();
            //商品編號
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPNUM02";
            st_bpud_BPNUM = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //中文名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPTNA02";
            st_bpud_BPTNA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //簡體名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPCNA02";
            st_bpud_BPCNA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //英文名稱
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPENA02";
            st_bpud_BPENA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //規　　格
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPCLA02";
            st_bpud_BPCLA = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //售價含稅
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPDE102";
            st_bpud_BPDE1 = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //會員含稅
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPDE202";
            st_bpud_BPDE2 = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //上架日期
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPDT102";
            st_bpud_BPDT1 = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            //下架日期
            st_ctrlname = st_ctrl + DAC.GetGridViewRowId(in_g) + "$tx_bpud_BPETQ02";
            st_bpud_BPETQ = ((TextBox)FindControl(st_ctrlname)).Text.Trim();
            bl_Mod = true;
          }
          else
          {
            bl_Mod = false;
          }
          //
          if (bl_Mod)
          {
            sel_rows = tb_bpud.Select("bpud_gkey='" + st_bpud_gkey + "'");
            if (sel_rows.Length == 1)
            {
              mod_row = sel_rows[0];
              if (
                   (DAC.GetStringValue(mod_row["bpud_BPNUM"]) != st_bpud_BPNUM)
                || (DAC.GetStringValue(mod_row["bpud_BPTNA"]) != st_bpud_BPTNA)
                || (DAC.GetStringValue(mod_row["bpud_BPCNA"]) != st_bpud_BPCNA)
                || (DAC.GetStringValue(mod_row["bpud_BPENA"]) != st_bpud_BPENA)
                || (DAC.GetStringValue(mod_row["bpud_BPCLA"]) != st_bpud_BPCLA)
                || (DAC.GetStringValue(mod_row["bpud_BPDE1"]) != st_bpud_BPDE1)
                || (DAC.GetStringValue(mod_row["bpud_BPDE2"]) != st_bpud_BPDE2)
                || (DAC.GetStringValue(mod_row["bpud_BPDT1"]) != st_bpud_BPDT1)
                || (DAC.GetStringValue(mod_row["bpud_BPETQ"]) != st_bpud_BPETQ)
              )
              {
                bpudDao.Insertbalog(conn, thistran, "bpud", st_ActKey, st_bpud_gkey);
                bpudDao.Insertbtlog(conn, thistran, "bpud", DAC.GetStringValue(mod_row["bpud_bpnum"]), "M", UserGkey, DAC.GetStringValue(mod_row["bpud_bpnum"]) + " " + DAC.GetStringValue(mod_row["bpud_bptna"]) + " " + DAC.GetStringValue(mod_row["bpud_bpde1"]));
                mod_row.BeginEdit();
                mod_row["bpud_BPNUM"] = st_bpud_BPNUM;      //商品編號
                mod_row["bpud_BPTNA"] = st_bpud_BPTNA;      //中文名稱
                mod_row["bpud_BPCNA"] = st_bpud_BPCNA;      //簡體名稱
                mod_row["bpud_BPENA"] = st_bpud_BPENA;      //英文名稱
                mod_row["bpud_BPCLA"] = st_bpud_BPCLA;      //規　　格
                mod_row["bpud_BPDE1"] = st_bpud_BPDE1;      //售價含稅
                mod_row["bpud_BPDE2"] = st_bpud_BPDE2;      //會員含稅
                mod_row["bpud_BPDT1"] = st_bpud_BPDT1;      //上架日期
                if (st_bpud_BPETQ == "") { mod_row["bpud_BPETQ"] = DBNull.Value; } else { mod_row["bpud_BPETQ"] = st_bpud_BPETQ; }  //下架日期
                mod_row.EndEdit();
                st_ActKey = DAC.get_guidkey();  //
              }
            }  //sel_rows.Length == 1
          }  //bl_Mod
        }  //for
        da_ADP.Update(tb_bpud);
        thistran.Commit();
        bl_updateok = true;
      }  //try
      catch (Exception e)
      {
        thistran.Rollback();
        bl_updateok = false;
        st_errmsg += e.Message;
      }
      finally
      {
        thistran.Dispose();
        bpudDao.Dispose();
        tb_bpud.Dispose();
        da_ADP.Dispose();
      }
      return bl_updateok;
    }

    protected void bt_dcnews_DCPC1_DEL_Click(object sender, EventArgs e)
    {
      sFN.DocDEL_Pic(sys_DocFilePath, st_object_func, tx_bpud_BPNUM.Text, "");
      BindNew(true);
    }

    protected void bt_dcnews_DCPC1_MOD_Click(object sender, EventArgs e)
    {
      string st_errmsg = "";
      //if (sFN.DocMOD_PIC(attimg01, sys_PicFilePath, sys_PicExtension, st_dd_apx, tx_dcnews_DCREN.Text, "01", ref st_errmsg) == true)
      if (sFN.DocMOD_PIC(attimg01, sys_DocFilePath, sys_PicExtension, st_object_func, tx_bpud_BPNUM.Text, "", ref st_errmsg) == true)
      {
        BindNew(true);
      }
      else
      {
        lb_ErrorMessage.Visible = true;
        lb_ErrorMessage.Text = st_errmsg;
      }
    }

    protected void bt_08_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

    }
    
  }
}