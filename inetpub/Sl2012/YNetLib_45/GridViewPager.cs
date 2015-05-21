using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;


namespace YNetLib_45
{
  [ToolboxData("<{0}:GridViewPager runat=server></{0}:GridViewPager>")]
  [Description("GridView 跳頁元件")]
  [Category("Maolaoda Web Control")]

  public class GridViewPager : System.Web.UI.WebControls.CompositeControl
  {
    private Button btnFirst;
    private Button btnPrev;
    private Button btnNext;
    private Button btnLast;

    private Label lbLabel1;
    private DropDownList cbPages;
    private Label lbLabel2;
    private Label lbTotalPages;
    private Label lbLabel3;
    private GridView gridView = null;
    private string FButtonCssClass = "pagerButton";
    private bool _btnFirstEnabled = true;
    private bool _btnPrevEnabled = true;
    private bool _btnNextEnabled = true;
    private bool _btnLastEnabled = true;
    private bool _cbPagesEnabled = true;
    private string _textCssClass = "pagerText";
    private Label lbLabel4;
    private DropDownList cbPageSize;
    private Label lbLabel5;

    public GridViewPager()
    {
      PageSizeList = new List<string>();
      PageSizeList.Add("10");
      PageSizeList.Add("20");
      PageSizeList.Add("30");
      PageSizeList.Add("50");
      PageSizeList.Add("100");
      CanChangePageSize = true;
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("跳至第")]
    [Localizable(true)]
    public string Text1
    {
      get
      {
        EnsureChildControls();
        return lbLabel1.Text;
      }
      set
      {
        EnsureChildControls();
        lbLabel1.Text = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("頁 / 共")]
    [Localizable(true)]
    public string Text2
    {
      get
      {
        EnsureChildControls();
        return lbLabel2.Text;
      }
      set
      {
        EnsureChildControls();
        lbLabel2.Text = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("頁")]
    [Localizable(true)]
    public string Text3
    {
      get
      {
        EnsureChildControls();
        return lbLabel3.Text;
      }
      set
      {
        EnsureChildControls();
        lbLabel3.Text = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("第一頁")]
    [Localizable(true)]
    public string FirstButtonText
    {
      get
      {
        EnsureChildControls();
        return btnFirst.Text;
      }
      set
      {
        EnsureChildControls();
        btnFirst.Text = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("上一頁")]
    [Localizable(true)]
    public string PrevButtonText
    {
      get
      {
        EnsureChildControls();
        return btnPrev.Text;
      }
      set
      {
        EnsureChildControls();
        btnPrev.Text = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("下一頁")]
    [Localizable(true)]
    public string NextButtonText
    {
      get
      {
        EnsureChildControls();
        return btnNext.Text;
      }
      set
      {
        EnsureChildControls();
        btnNext.Text = value;
      }
    }

    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("最後頁")]
    [Localizable(true)]
    public string LastButtonText
    {
      get
      {
        EnsureChildControls();
        return btnLast.Text;
      }
      set
      {
        EnsureChildControls();
        btnLast.Text = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue("pageButton")]
    public string ButtonCssClass
    {
      get
      {
        EnsureChildControls();
        return FButtonCssClass;
      }
      set
      {
        EnsureChildControls();
        FButtonCssClass = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    public override bool Enabled
    {
      get
      {
        EnsureChildControls();
        return base.Enabled;
      }
      set
      {
        EnsureChildControls();
        base.Enabled = value;
        btnFirst.Enabled = value;
        btnPrev.Enabled = value;
        btnNext.Enabled = value;
        btnLast.Enabled = value;
        cbPages.Enabled = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    public bool FirstButtonEnabled
    {
      get
      {
        EnsureChildControls();
        return _btnFirstEnabled;
      }
      set
      {
        EnsureChildControls();
        _btnFirstEnabled = value;
        btnFirst.Enabled = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    public bool PrevButtonEnabled
    {
      get
      {
        EnsureChildControls();
        return _btnPrevEnabled;
      }
      set
      {
        EnsureChildControls();
        _btnPrevEnabled = value;
        btnPrev.Enabled = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    public bool NextButtonEnabled
    {
      get
      {
        EnsureChildControls();
        return _btnNextEnabled;
      }
      set
      {
        EnsureChildControls();
        _btnNextEnabled = value;
        btnNext.Enabled = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    public bool LastButtonEnabled
    {
      get
      {
        EnsureChildControls();
        return _btnLastEnabled;
      }
      set
      {
        EnsureChildControls();
        _btnLastEnabled = value;
        btnLast.Enabled = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    public bool PageJumpDropDownEnabled
    {
      get
      {
        EnsureChildControls();
        return _cbPagesEnabled;
      }
      set
      {
        EnsureChildControls();
        _cbPagesEnabled = value;
        cbPages.Enabled = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue("pageText")]
    public string TextCssClass
    {
      get
      {
        return _textCssClass;
      }
      set
      {
        _textCssClass = value;
      }
    }

    [Category("Appearance")]
    public List<string> PageSizeList
    {
      get;
      set;
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    public bool CanChangePageSize
    {
      get;
      set;
    }

    private void GetGridView()
    {
      Control parent = this.Parent;
      while (parent != null)
      {
        if (parent is GridView)
        {
          gridView = (GridView)parent;
          gridView.PreRender -= new EventHandler(GridView_PreRender);
          gridView.PreRender += new EventHandler(GridView_PreRender);
          break;
        }
        parent = parent.Parent;
      }
    }

    protected void cbPages_SelectedIndexChanged(object sender, EventArgs e)
    {
      GetGridView();
      if (gridView == null)
        return;

      gridView.PageIndex = cbPages.SelectedIndex;
      Type type = gridView.GetType();

      PropertyInfo propertyInfo = (type.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic));
      EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(gridView, null);
      FieldInfo fieldInfo = type.GetField("EventPageIndexChanged", BindingFlags.Static | BindingFlags.NonPublic);
      Delegate d = eventHandlerList[fieldInfo.GetValue(null)];

      if (d != null)
        d.Method.Invoke(d.Target, new object[] { gridView, e });
    }

    protected void cbPages_DataBinding(object sender, EventArgs e)
    {
      GetGridView();
      if (gridView == null)
        return;

      for (int i = 0; i < gridView.PageCount; i++)
      {
        cbPages.Items.Add((i + 1).ToString());
      }

      cbPages.SelectedIndex = gridView.PageIndex;
      btnFirst.Enabled = gridView.PageIndex != 0;
      btnPrev.Enabled = gridView.PageIndex != 0;
      btnNext.Enabled = gridView.PageIndex != gridView.PageCount - 1;
      btnLast.Enabled = gridView.PageIndex != gridView.PageCount - 1;
    }

    protected void cbPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GetGridView();
      if (gridView == null)
        return;

      int currentPageIndex = gridView.PageIndex;
      gridView.PageSize = Convert.ToInt32(cbPageSize.SelectedValue);
      gridView.DataBind();
      if (currentPageIndex >= gridView.PageCount)
      {
        gridView.PageIndex = gridView.PageCount - 1;
        gridView.DataBind();
      }
      cbPages.DataBind();
      lbTotalPages.DataBind();
    }

    protected void cbPageSize_DataBinding(object sender, EventArgs e)
    {
      GetGridView();
      if (gridView == null)
        return;

      ListItem item = cbPageSize.Items.FindByValue(Convert.ToString(gridView.PageSize));
      if (item != null)
      {
        cbPageSize.SelectedValue = Convert.ToString(gridView.PageSize);
      }
      else
      {
        cbPageSize.SelectedIndex = 0;
        cbPageSize_SelectedIndexChanged(sender, e);
      }
    }

    protected void lbTotalPages_DataBinding(object sender, EventArgs e)
    {
      GetGridView();
      if (gridView == null)
        return;

      lbTotalPages.Text = gridView.PageCount.ToString();
    }

    protected void GridView_PreRender(object sender, EventArgs e)
    {
      GetGridView();
      if (gridView == null)
        return;

      GridViewRow pagerRow = gridView.BottomPagerRow;
      if (gridView.AllowPaging && pagerRow != null && pagerRow.Visible == false)
        pagerRow.Visible = true;
    }

    protected override void RecreateChildControls()
    {
      //EnsureChildControls();
      CreateChildControls();
    }

    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      Controls.Clear();
      btnFirst = new Button();
      btnFirst.ID = "btnFirst";
      btnFirst.Text = "第一頁";
      btnFirst.Font.CopyFrom(this.Font);
      btnFirst.CommandArgument = "First";
      btnFirst.CommandName = "Page";
      btnFirst.CssClass = FButtonCssClass;
      btnFirst.Enabled = _btnFirstEnabled;
      btnFirst.UseSubmitBehavior = false;

      btnPrev = new Button();
      btnPrev.ID = "btnPrev";
      btnPrev.Text = "上一頁";
      btnPrev.Font.CopyFrom(this.Font);
      btnPrev.CommandArgument = "Prev";
      btnPrev.CommandName = "Page";
      btnPrev.CssClass = FButtonCssClass;
      btnPrev.Enabled = _btnPrevEnabled;
      btnPrev.UseSubmitBehavior = false;

      btnNext = new Button();
      btnNext.ID = "btnNext";
      btnNext.Text = "下一頁";
      btnNext.Font.CopyFrom(this.Font);
      btnNext.CommandArgument = "Next";
      btnNext.CommandName = "Page";
      btnNext.CssClass = FButtonCssClass;
      btnNext.Enabled = _btnNextEnabled;
      btnNext.UseSubmitBehavior = false;

      btnLast = new Button();
      btnLast.ID = "btnLast";
      btnLast.Text = "最後頁";
      btnLast.Font.CopyFrom(this.Font);
      btnLast.CommandArgument = "Last";
      btnLast.CommandName = "Page";
      btnLast.CssClass = FButtonCssClass;
      btnLast.Enabled = _btnLastEnabled;
      btnLast.UseSubmitBehavior = false;

      lbLabel1 = new Label();
      lbLabel1.Text = "跳至第";
      lbLabel1.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
      lbLabel1.Font.CopyFrom(this.Font);
      lbLabel1.CssClass = _textCssClass;

      cbPages = new DropDownList();
      cbPages.ID = "cbPages";
      cbPages.Font.CopyFrom(this.Font);
      cbPages.AutoPostBack = true;
      cbPages.SelectedIndexChanged += new EventHandler(cbPages_SelectedIndexChanged);
      cbPages.DataBinding += new EventHandler(cbPages_DataBinding);
      cbPages.Enabled = _cbPagesEnabled;

      lbLabel2 = new Label();
      lbLabel2.Text = "頁 / 共";
      lbLabel2.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
      lbLabel2.Font.CopyFrom(this.Font);
      lbLabel2.CssClass = _textCssClass;

      lbTotalPages = new Label();
      lbTotalPages.Text = "0";
      lbTotalPages.Font.CopyFrom(this.Font);
      lbTotalPages.DataBinding += new EventHandler(lbTotalPages_DataBinding);
      lbTotalPages.CssClass = _textCssClass;

      lbLabel3 = new Label();
      lbLabel3.Text = "頁";
      lbLabel3.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
      lbLabel3.Font.CopyFrom(this.Font);
      lbLabel3.CssClass = _textCssClass;

      if (CanChangePageSize)
      {
        lbLabel4 = new Label();
        lbLabel4.Text = "每頁";
        lbLabel4.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
        lbLabel4.Font.CopyFrom(this.Font);
        lbLabel4.CssClass = _textCssClass;

        lbLabel5 = new Label();
        lbLabel5.Text = "筆";
        lbLabel5.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
        lbLabel5.Font.CopyFrom(this.Font);
        lbLabel5.CssClass = _textCssClass;

        cbPageSize = new DropDownList();
        cbPageSize.ID = "cbPageSize";
        cbPageSize.Font.CopyFrom(this.Font);
        cbPageSize.AutoPostBack = true;
        cbPageSize.SelectedIndexChanged += new EventHandler(cbPageSize_SelectedIndexChanged);
        cbPageSize.DataBinding += new EventHandler(cbPageSize_DataBinding);
        foreach (string s in PageSizeList)
          cbPageSize.Items.Add(s);
        cbPageSize.Enabled = _cbPagesEnabled;
      }

      Controls.Add(btnFirst);
      Controls.Add(btnPrev);
      Controls.Add(btnNext);
      Controls.Add(btnLast);
      Controls.Add(lbLabel1);
      Controls.Add(cbPages);
      Controls.Add(lbLabel2);
      Controls.Add(lbTotalPages);
      Controls.Add(lbLabel3);

      if (CanChangePageSize)
      {
        Controls.Add(lbLabel4);
        Controls.Add(cbPageSize);
        Controls.Add(lbLabel5);
      }
    }

    private void RenderAControl(HtmlTextWriter output, Control control)
    {
      output.AddAttribute(HtmlTextWriterAttribute.Wrap, "nowrap", false);
      output.RenderBeginTag(HtmlTextWriterTag.Td);
      control.RenderControl(output);
      output.RenderEndTag();
    }

    protected override void Render(HtmlTextWriter output)
    {
      output.AddAttribute(HtmlTextWriterAttribute.Border, "0", false);
      output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "2", false);
      output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0", false);
      output.RenderBeginTag(HtmlTextWriterTag.Table);
      output.RenderBeginTag(HtmlTextWriterTag.Tr);
      RenderAControl(output, btnFirst);
      RenderAControl(output, btnPrev);
      RenderAControl(output, lbLabel1);
      RenderAControl(output, cbPages);
      RenderAControl(output, lbLabel2);
      RenderAControl(output, lbTotalPages);
      RenderAControl(output, lbLabel3);
      RenderAControl(output, btnNext);
      RenderAControl(output, btnLast);
      if (CanChangePageSize)
      {
        RenderAControl(output, lbLabel4);
        RenderAControl(output, cbPageSize);
        RenderAControl(output, lbLabel5);
      }
      output.RenderEndTag();
      output.RenderEndTag();
    }
  }
}
