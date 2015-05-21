using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;
using YNetLib_45;

namespace DD2015_45
{
    public class StringReplacer
    {
        private static StringReplacer _instance = null;
        private Dictionary<string, ReplaceProcessor> processorList = new Dictionary<string, ReplaceProcessor>();

        private StringReplacer()
        {
            TextPropertyProcessor textProcessor1 = new TextPropertyProcessor();
            textProcessor1.UrlEncode = true;
            TextPropertyProcessor textProcessor2 = new TextPropertyProcessor();
            ValuePropertyProcessor valueProcessor = new ValuePropertyProcessor();
            ItemsPropertyProcessor itemsProcessor = new ItemsPropertyProcessor();
            ErrorMessagePropertyProcessor errorMessageProcessor = new ErrorMessagePropertyProcessor();

            // 將控制項類別名稱與處理器做關聯
            processorList.Add("System.Web.UI.WebControls.Label", textProcessor1);
            processorList.Add("System.Web.UI.WebControls.Button", textProcessor2);
            processorList.Add("System.Web.UI.WebControls.LinkButton", textProcessor2);
            processorList.Add("System.Web.UI.WebControls.HyperLink", textProcessor2);
            processorList.Add("System.Web.UI.WebControls.GridView", new GridViewControlProcessor());
            processorList.Add("System.Web.UI.WebControls.Menu", new MenuControlProcessor());
            processorList.Add("System.Web.UI.HtmlControls.HtmlInputButton", valueProcessor);
            processorList.Add("System.Web.UI.WebControls.ValidationSummary", new HeaderTextPropertyProcessor());
            processorList.Add("System.Web.UI.WebControls.ListBox", itemsProcessor);
            processorList.Add("System.Web.UI.WebControls.DropDownList", itemsProcessor);
            processorList.Add("System.Web.UI.WebControls.RequiredFieldValidator", errorMessageProcessor);
            processorList.Add("System.Web.UI.WebControls.CompareValidator", errorMessageProcessor);
            processorList.Add("System.Web.UI.WebControls.RangeValidator", errorMessageProcessor);
            processorList.Add("System.Web.UI.WebControls.RegularExpressionValidator", errorMessageProcessor);
            processorList.Add("System.Web.UI.WebControls.CustomValidator", errorMessageProcessor);
            processorList.Add("System.Web.UI.WebControls.GridViewRow", new GridViewRowControlProcessor());
            processorList.Add("System.Web.UI.WebControls.Image", new ImageProcessor());
            processorList.Add("WebLib.GridViewPager", new WebLib_GridViewPagerProcessor());
        }

        /// <summary>
        /// 遍歷控制項集合，處理所有子控制項的字串替換
        /// </summary>
        /// <param name="langId">語言編號</param>
        /// <param name="controls">控制項集合</param>
        public static void TravelControl(string langId, ControlCollection controls)
        {
            if (_instance == null)
                _instance = new StringReplacer();
            _instance.DoTravelControl(langId, controls);
        }

        /// <summary>
        /// 處理單一控制項的字串替換
        /// </summary>
        /// <param name="langId"></param>
        /// <param name="control"></param>
        public static void ReplaceControl(string langId, Control control)
        {
            if (_instance == null)
                _instance = new StringReplacer();

            if (_instance.processorList.ContainsKey(control.GetType().FullName))
            {
                _instance.processorList[control.GetType().FullName].Process(langId, control);
            }
        }

        /// <summary>
        /// 遍歷控制項集合，處理所有子控制項的字串替換
        /// </summary>
        /// <param name="langId"></param>
        /// <param name="controls"></param>
        private void DoTravelControl(string langId, ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (processorList.ContainsKey(control.GetType().FullName))
                {
                    processorList[control.GetType().FullName].Process(langId, control);
                }

                if (control.Controls.Count > 0)
                {
                    DoTravelControl(langId, control.Controls);
                }
            }
        }

    }

    /// <summary>
    /// 控制項串替換處理器之抽像類別，定義所有控制項串替換處理器之界面
    /// </summary>
    public abstract class ReplaceProcessor
    {
        protected bool _urlEncode = false;
        public abstract void Process(string langId, Control control);
        public bool UrlEncode
        {
            get
            {
                return _urlEncode;
            }
            set
            {
                _urlEncode = value;
            }
        }
    }

    /// <summary>
    /// Text 屬性替換處理器
    /// </summary>
    public class TextPropertyProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Type t = control.GetType();
            PropertyInfo p = t.GetProperty("Text", typeof(System.String));
            if (p != null)
            {
                string value = p.GetValue(control, null).ToString();
                value = StringTable.GetString(langId, value);
                if (_urlEncode)
                    value = value.Replace(" ", "&nbsp;").Replace("\r\n", "<br/>");
                p.SetValue(control, value, null);
            }
        }
    }

    /// <summary>
    /// HeaderText 屬性替換處理器
    /// </summary>
    public class HeaderTextPropertyProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Type t = control.GetType();
            PropertyInfo p = t.GetProperty("HeaderText", typeof(System.String));
            if (p != null)
            {
                p.SetValue(control, StringTable.GetString(langId, p.GetValue(control, null).ToString()), null);
            }
        }
    }

    /// <summary>
    /// Value 屬性替換處理器
    /// </summary>
    public class ValuePropertyProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Type t = control.GetType();
            PropertyInfo p = t.GetProperty("Value", typeof(System.String));
            if (p != null)
            {
                p.SetValue(control, StringTable.GetString(langId, p.GetValue(control, null).ToString()), null);
            }
        }
    }

    /// <summary>
    /// Label 控制項替換處理器
    /// </summary>
    public class LabelControlProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Label label = control as Label;
            if (label != null)
            {
                label.Text = StringTable.GetString(langId, label.Text);
            }
        }
    }

    /// <summary>
    /// Button 控制項替換處理器
    /// </summary>
    public class ButtonControlProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Button btn = control as Button;
            if (btn != null)
            {
                btn.Text = StringTable.GetString(langId, btn.Text);
            }
        }
    }

    /// <summary>
    /// GridView 控制項替換處理器
    /// </summary>
    public class GridViewControlProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            GridView gv = control as GridView;
            if (gv == null)
                return;

            foreach (DataControlField column in gv.Columns)
            {
                column.HeaderText = StringTable.GetString(langId, column.HeaderText);
            }

            if (gv.BottomPagerRow != null)
                foreach (TableCell cell in gv.BottomPagerRow.Cells)
                {
                    StringReplacer.TravelControl(langId, cell.Controls);
                }
        }
    }

    /// <summary>
    /// Menu 控制項替換處理器
    /// </summary>
    public class MenuControlProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Menu menu = control as Menu;
            if (menu == null)
                return;

            // 處理五層就好
            foreach (MenuItem item0 in menu.Items)
            {
                item0.Text = StringTable.GetString(langId, item0.Text);
                foreach (MenuItem item1 in item0.ChildItems)
                {
                    item1.Text = StringTable.GetString(langId, item1.Text);
                    foreach (MenuItem item2 in item1.ChildItems)
                    {
                        item2.Text = StringTable.GetString(langId, item2.Text);
                        foreach (MenuItem item3 in item2.ChildItems)
                        {
                            item3.Text = StringTable.GetString(langId, item3.Text);
                            foreach (MenuItem item4 in item3.ChildItems)
                            {
                                item4.Text = StringTable.GetString(langId, item4.Text);
                                foreach (MenuItem item5 in item4.ChildItems)
                                {
                                    item5.Text = StringTable.GetString(langId, item5.Text);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Items 屬性替換處理器
    /// </summary>
    public class ItemsPropertyProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Type t = control.GetType();
            PropertyInfo p = t.GetProperty("Items", typeof(System.Web.UI.WebControls.ListItemCollection));
            if (p != null)
            {
                foreach (ListItem item in (ListItemCollection)p.GetValue(control, null))
                {
                    item.Text = StringTable.GetString(langId, item.Text);
                    // p.SetValue(control, StringTable.GetString(langId, p.GetValue(control, null).ToString()), null);
                }
            }
        }
    }

    /// <summary>
    /// ErrorMessage 屬性替換處理器
    /// </summary>
    public class ErrorMessagePropertyProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Type t = control.GetType();
            PropertyInfo p = t.GetProperty("ErrorMessage", typeof(System.String));
            if (p != null)
            {
                p.SetValue(control, StringTable.GetString(langId, p.GetValue(control, null).ToString()), null);
            }
        }
    }

    /// <summary>
    /// GridViewRow 控制項替換處理器
    /// </summary>
    public class GridViewRowControlProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            
            GridViewRow row = control as GridViewRow;
            if (row == null)
              return;
            foreach (TableCell cell in row.Cells)
            {
              cell.Text = StringTable.GetString(langId, cell.Text);
            }
            
        }
    }

    /// <summary>
    /// Image ImageUrl 屬性替換處理器
    /// </summary>
    public class ImageProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            Image img = control as Image;

            if (img.ImageUrl.ToLower().StartsWith("http://"))
                return;
            if (!img.ImageUrl.ToLower().Contains("picture/"))
                return;

            string fileName = Path.GetFileName(img.ImageUrl);
            img.ImageUrl = "~/Picture/" + img.Page.Theme + "/" + fileName;
        }
    }

    public class WebLib_GridViewPagerProcessor : ReplaceProcessor
    {
        public override void Process(string langId, Control control)
        {
            GridViewPager pager = control as GridViewPager;

            pager.FirstButtonText = StringTable.GetString(langId, pager.FirstButtonText);
            pager.PrevButtonText = StringTable.GetString(langId, pager.PrevButtonText);
            pager.NextButtonText = StringTable.GetString(langId, pager.NextButtonText);
            pager.LastButtonText = StringTable.GetString(langId, pager.LastButtonText);
            pager.Text1 = StringTable.GetString(langId, pager.Text1);
            pager.Text2 = StringTable.GetString(langId, pager.Text2);
            pager.Text3 = StringTable.GetString(langId, pager.Text3);
        }
    }
}