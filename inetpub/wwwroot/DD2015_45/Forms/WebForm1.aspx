<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DD2015_45.Forms.WebForm1" %>

<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.NavigationControls" tagprefix="ig" %>


<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.WebUI.WebDataInput.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics45.WebUI.WebHtmlEditor.v14.2, Version=14.2.20142.2146, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebHtmlEditor" tagprefix="ighedit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  <script type="text/javascript" id="igClientScript">
<!--

function WebTextEditor1_KeyDown(sender, eventArgs)
{
  var keycode = eventArgs.get_keyCode();
  var txt_field = 'TextBox1';
  document.all[txt_field].value = keycode.toString() + "," + document.all[txt_field].value;
  txt_field = 'WebTextEditor2';
  document.all[txt_field].value = keycode.toString() + "," + document.all[txt_field].value;
  //alert(keycode);
	///<summary>
	///
	///</summary>
	///<param name="sender" type="Infragistics.Web.UI.WebTextEditor"></param>
	///<param name="eventArgs" type="Infragistics.Web.UI.TextEditorKeyEventArgs"></param>

	//Add code to handle your event here.
}// -->
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
      <ig:WebDataMenu ID="WebDataMenu1" runat="server">
      </ig:WebDataMenu>
      <ig:WebDropDown ID="WebDropDown1" runat="server" Width="200px">
      </ig:WebDropDown>
      <ig:WebTab ID="WebTab1" runat="server" Height="200px" Width="300px">
        <tabs>
          <ig:ContentTabItem runat="server" Text="Tab 1">
            <Template>
              <asp:ScriptManager ID="ScriptManager1" runat="server">
              </asp:ScriptManager>
            </Template>
          </ig:ContentTabItem>
          <ig:ContentTabItem runat="server" Text="Tab 2">
          </ig:ContentTabItem>
        </tabs>
      </ig:WebTab>
      <ig:WebTextEditor ID="WebTextEditor1" Text="EDIT" runat="server">
        <ClientEvents KeyDown="WebTextEditor1_KeyDown" />
      </ig:WebTextEditor>
      <igtxt:WebImageButton ID="WebImageButton1" runat="server">
      </igtxt:WebImageButton>
      <ig:WebTextEditor ID="WebTextEditor2" Width="500px"  runat="server">
      </ig:WebTextEditor>
      <asp:TextBox ID="TextBox1" Width="500px" runat="server"></asp:TextBox>
      <ighedit:WebHtmlEditor ID="WebHtmlEditor1" runat="server">
      </ighedit:WebHtmlEditor>
      <ig:WebDataGrid ID="WebDataGrid1" runat="server" Height="350px" Width="400px" />
      <ig:WebDatePicker ID="WebDatePicker1" runat="server">
      </ig:WebDatePicker>
      <ig:WebDateTimeEditor ID="WebDateTimeEditor1" runat="server">
      </ig:WebDateTimeEditor>
      <ig:WebNumericEditor ID="WebNumericEditor1" runat="server">
      </ig:WebNumericEditor>
    </form>
</body>
</html>
