
var empty_field = "$*$**";

//圖形ShowDialog
function MsgDialog(PicUrl, WinWidth, WinHeight) {
    var sFeatures;
    PicUrl = encodeURI(PicUrl);
    WinWidth = encodeURI(WinWidth);
    WinHeight = encodeURI(WinHeight);
    if (window.showModalDialog) {
        WinWidth += "px";
        WinHeight += "px";
        var WinOp = "status:no;center:yes;dialogWidth:" + WinWidth + ";dialogHeight:" + WinHeight + ";resizable:no;dialogHide:yes";
        var oPass = PicUrl;
        var retval = window.showModalDialog(oPass, window, WinOp);
    }
    return false;
}

//from商品資料輸入選擇,webservice取資料
function get_bpud_cname(iFunc, iContEdit, iField, oField, di_Window, di_OpenUrl, di_Caption) {
    var vNum = document.all[iField].value;
    document.all[oField].value = "";
    if (vNum != "") {
        vNum = encodeURI(vNum);
        DD2015_45.wwwService.Svrwww.service_get_bpud_cname('key1', 'key2', iFunc, iContEdit, vNum, iField, oField, bpud_cname_onSuccess, servicr_onError);
    }
    return false;
    ///
    //bpud_cname_onSuccess成功 
    function bpud_cname_onSuccess(receiveData, userContext, methodName) {
        var r_str = receiveData;
        var rtype = GetTagValue(r_str, "rFlag");
        var iFunc = GetTagValue(r_str, "iFunc");
        var iField = GetTagValue(r_str, "iField");

        if (rtype == '1') {
            Show_rTable(r_str);
        }
        else if (rtype == '0') {
            alert(vNum + ',no found.');
            document.all[iField].value = "";
            document.all[iField].focus();
        }
        else if (rtype == '2') {
            document.all[iField].value = "";
            //
            var iContEdit = GetTagValue(r_str, "iContEdit");
            var iIndex = GetTagValue(r_str, "rIndex");
            var iInput = GetTagValue(r_str, "iInput");
            var oField = GetTagValue(r_str, "oField");
            //
            var di_OpenUrl_Full = di_OpenUrl + "?iFunc=" + iFunc + "&oWindow_Id=" + di_Window + "&iField=" + iField + "&oField=" + oField + "&iIndex=" + iIndex + "&iInput=" + iInput;
            //
            //document.all[iField].focus();
            var dialog = $find(di_Window);
            dialog.show();
            dialog.get_header().setCaptionText(di_Caption);
            dialog.get_contentPane().set_contentUrl(di_OpenUrl_Full);
            dialog.show();
            document.all[iField].focus();
        }
        else {
            document.all[iField].focus();
        }
        return false;
    }
}

//from作著baur輸入選擇,webservice取資料
function get_baur_cname(iFunc, iContEdit, iField, oField, di_Window, di_OpenUrl, di_Caption) {
    var vNum = document.all[iField].value;
    document.all[oField].value = "";
    if (vNum != "") {
        vNum = encodeURI(vNum);
        DD2015_45.wwwService.Svrwww.service_get_baur('key1', 'key2', iFunc, iContEdit, vNum, iField, oField, baur_cname_onSuccess, servicr_onError);
    }
    return false;

    function baur_cname_onSuccess(receiveData, userContext, methodName) {
        var r_str = receiveData;
        var rtype = GetTagValue(r_str, "rFlag");
        if (rtype == '1') {
            Show_rTable(r_str);
        }
        else if (rtype == '0') {
            alert(vNum + ',no found.');
            document.all[iField].value = "";
            document.all[iField].focus();
        }
        else if (rtype == '2') {
            document.all[iField].value = "";
            //
            var iIndex = GetTagValue(r_str, "rIndex");
            var iInput = GetTagValue(r_str, "iInput");
            var di_OpenUrl_Full = di_OpenUrl + "?iFunc=" + iFunc + "&oWindow_Id=" + di_Window + "&oBCNUM_Field=" + iField + "&oBCNAM_Field=" + oField + "&iIndex=" + iIndex + "&iInput=" + iInput;
            var dialog = $find(di_Window);
            dialog.show();
            dialog.get_header().setCaptionText(di_Caption);
            dialog.get_contentPane().set_contentUrl(di_OpenUrl_Full);
            dialog.show();
            document.all[iField].focus();
        }
        else {
            document.all[userContext].value = receiveData;
        }
        return false;
    }
}


//WebDataGrid作著資料輸入選擇,webservice取資料
function get_baur_grname(iFunc, iContEdit, iNewMod, iField, oField, DataGrid_id, row, di_Window, di_OpenUrl, di_Caption, vptn) {
    if (vptn != "") {
        vptn = encodeURI(vptn);
        DD2015_45.wwwService.Svrwww.service_get_baur_grname('key1', 'key2', iFunc, iContEdit, iField, oField, vptn, baur_grname_onSuccess, servicr_onError);
    }

    function baur_grname_onSuccess(receiveData, userContext, methodName) {
        var r_str = receiveData;
        var rtype = GetTagValue(r_str, "rFlag");

        if (rtype == '1') {
            Show_rTable_grow(row, r_str);
        }
        else if (rtype == '0') {
            alert(vptn + ',no found');
            //Show_rTable_grow_focus(iNewMod, DataGrid_id, row, r_str);
        }
        else if (rtype == '2') {
            var iFunc = GetTagValue(r_str, "iFunc");
            var iContEdit = GetTagValue(r_str, "iContEdit");
            var iIndex = GetTagValue(r_str, "rIndex");
            var iInput = GetTagValue(r_str, "iInput");
            var iField = GetTagValue(r_str, "iField");
            var oField = GetTagValue(r_str, "oField");
            //
            var dialog = $find(di_Window);
            dialog.show();
            dialog.get_header().setCaptionText(di_Caption);
            dialog.get_contentPane().set_contentUrl(di_OpenUrl);
            dialog.show();
            //open_bcvw(iFunc, iContEdit, iIndex, iInput, iField, oField);
        }
        else {
            //Show_rTable_grow_focus(iNewMod, DataGrid_id, row, r_str);
        }
        return false;
    }
}


//WebDataGrid商品資料輸入選擇,webservice取資料
function get_bpud_grname(iFunc, iContEdit, iNewMod, iField, oField, DataGrid_id, row, di_Window, di_OpenUrl, di_Caption, vptn, old_PTN, oCus, oDate) {
    if (vptn != "") {
        vptn = encodeURI(vptn);
        DD2015_45.wwwService.Svrwww.service_get_bpud_grname('key1', 'key2', iFunc, iContEdit, iField, oField, vptn, old_PTN, oCus, oDate, bpud_grname_onSuccess, servicr_onError);
    }

    function bpud_grname_onSuccess(receiveData, userContext, methodName) {
        var r_str = receiveData;
        var rtype = GetTagValue(r_str, "rFlag");
        var iField = GetTagValue(r_str, "iField");  //fail    focus
        var oField = GetTagValue(r_str, "oField");  //success focus

        if (rtype == '1') {
            Show_rTable_grow(row, r_str);
            Show_rTable_gfocusField(iNewMod, DataGrid_id, row, oField);
        }
        else if (rtype == '0') {
            alert(vptn + ',no found');
            if ((old_PTN != "") && (old_PTN != empty_field) && (iNewMod == "mod")) {
                webDataGrid_SetFieldValue(DataGrid_id, iField, old_PTN);
                old_PTN = empty_field;
                Show_rTable_gfocusField(iNewMod, DataGrid_id, row, iField);
            }
            else {
                webDataGrid_SetFieldValue(DataGrid_id, iField, "");
                old_PTN = empty_field;
                Show_rTable_gfocusField(iNewMod, DataGrid_id, row, iField);
            }
        }
        else if (rtype == '2') {
            var iFunc = GetTagValue(r_str, "iFunc");
            var iContEdit = GetTagValue(r_str, "iContEdit");
            var iIndex = GetTagValue(r_str, "rIndex");
            var iInput = GetTagValue(r_str, "iInput");
            //
            var di_OpenUrl_str = di_OpenUrl + "&iIndex=" + iIndex + "&iInput=" + iInput
            var dialog = $find(di_Window);
            dialog.show();
            dialog.get_header().setCaptionText(di_Caption);
            dialog.get_contentPane().set_contentUrl(di_OpenUrl_str);
            dialog.show();
        }
        else {
            //Show_rTable_grow_focus(iNewMod, DataGrid_id, row, r_str);
        }
        return false;

    }
}


//from廠商客戶輸入選擇,webservice取資料
function get_bdlr_cname(iFunc, iContEdit, iField, oField, di_Window, di_OpenUrl, di_Caption) {
    var vNum = document.all[iField].value;
    document.all[oField].value = "";
    if (vNum != "") {
        vNum = encodeURI(vNum);
        DD2015_45.wwwService.Svrwww.service_get_bdlr_cname('key1', 'key2', iFunc, iContEdit, vNum, iField, oField, bdlr_cname_onSuccess, servicr_onError);
    }
    return false;
    ///
    function bdlr_cname_onSuccess(receiveData, userContext, methodName) {
        var r_str = receiveData;
        var rtype = GetTagValue(r_str, "rFlag");
        var iFunc = GetTagValue(r_str, "iFunc");
        var iField = GetTagValue(r_str, "iField");

        if (rtype == '1') {
            Show_rTable(r_str);
        }
        else if (rtype == '0') {
            alert(vNum + ',no found.');
            document.all[iField].value = "";
            document.all[iField].focus();
        }
        else if (rtype == '2') {
            document.all[iField].value = "";
            //
            var iContEdit = GetTagValue(r_str, "iContEdit");
            var iIndex = GetTagValue(r_str, "rIndex");
            var iInput = GetTagValue(r_str, "iInput");
            var oField = GetTagValue(r_str, "oField");
            var di_OpenUrl_Full = di_OpenUrl + "?iFunc=" + iFunc + "&oWindow_Id=" + di_Window + "&iField=" + iField + "&oField=" + oField + "&iIndex=" + iIndex + "&iInput=" + iInput;
            //
            var dialog = $find(di_Window);
            dialog.show();
            dialog.get_header().setCaptionText(di_Caption);
            dialog.get_contentPane().set_contentUrl(di_OpenUrl_Full);
            dialog.show();
            document.all[iField].focus();
        }
        else {
            document.all[iField].focus();
        }
        return false;
    }
}


//WebDataGrid廠商客戶輸入選擇,webservice取資料
function get_bdlr_grname(iFunc, iContEdit, iNewMod, iField, oField, focus_Field, DataGrid_id, row, di_Window, di_OpenUrl, di_Caption, vnum, old_NUM) {
    if (vnum != "") {
        vnum = encodeURI(vnum);
        DD2015_45.wwwService.Svrwww.service_get_bdlr_grname('key1', 'key2', iFunc, iContEdit, iField, oField, vnum, old_NUM, bdlr_grname_onSuccess, servicr_onError);
    }

    function bdlr_grname_onSuccess(receiveData, userContext, methodName) {
        var r_str = receiveData;
        var rtype = GetTagValue(r_str, "rFlag");
        //var iField = GetTagValue(r_str, "iField");  //input field
        //var oField = GetTagValue(r_str, "oField");  //name field

        if (rtype == '1') {
            Show_rTable_grow(row, r_str);
            Show_rTable_gfocusField(iNewMod, DataGrid_id, row, focus_Field);
        }
        else if (rtype == '0') {
            alert(vnum + ',no found');
            if ((old_NUM != "") && (old_NUM != empty_field) && (iNewMod == "mod")) {
                webDataGrid_SetFieldValue(DataGrid_id, iField, old_NUM);
                old_NUM = empty_field;
                Show_rTable_gfocusField(iNewMod, DataGrid_id, row, iField);
            }
            else {
                webDataGrid_SetFieldValue(DataGrid_id, iField, "");
                old_NUM = empty_field;
                Show_rTable_gfocusField(iNewMod, DataGrid_id, row, iField);
            }
        }
        else if (rtype == '2') {
            var iFunc = GetTagValue(r_str, "iFunc");
            var iContEdit = GetTagValue(r_str, "iContEdit");
            var iIndex = GetTagValue(r_str, "rIndex");
            var iInput = GetTagValue(r_str, "iInput");
            //
            var di_OpenUrl_str = di_OpenUrl + "&iIndex=" + iIndex + "&iInput=" + iInput
            var dialog = $find(di_Window);
            dialog.show();
            dialog.get_header().setCaptionText(di_Caption);
            dialog.get_contentPane().set_contentUrl(di_OpenUrl_str);
            dialog.show();
        }
        else {
            //Show_rTable_grow_focus(iNewMod, DataGrid_id, row, r_str);
        }
        return false;
    }
}

//form員工(es101)輸入選擇,webservice取資料
function get_es101_cname(iFunc, iContEdit, iField, oField, di_Window, di_OpenUrl, di_Caption) {
    var vNum = document.all[iField].value;
    document.all[oField].value = "";
    if (vNum != "") {
        vNum = encodeURI(vNum);
        DD2015_45.wwwService.Svrwww.service_get_es101_cname('key1', 'key2', iFunc, iContEdit, vNum, iField, oField, es101_cname_onSuccess, servicr_onError);
    }
    return false;
    ///
    function es101_cname_onSuccess(receiveData, userContext, methodName) {
        var r_str = receiveData;
        var rtype = GetTagValue(r_str, "rFlag");
        var iFunc = GetTagValue(r_str, "iFunc");
        var iField = GetTagValue(r_str, "iField");

        if (rtype == '1') {
            Show_rTable(r_str);
        }
        else if (rtype == '0') {
            alert(vNum + ',no found.');
            document.all[iField].value = "";
            document.all[iField].focus();
        }
        else if (rtype == '2') {
            document.all[iField].value = "";
            //
            var iContEdit = GetTagValue(r_str, "iContEdit");
            var iIndex = GetTagValue(r_str, "rIndex");
            var iInput = GetTagValue(r_str, "iInput");
            var oField = GetTagValue(r_str, "oField");
            //
            var di_OpenUrl_Full = di_OpenUrl + "?iFunc=" + iFunc + "&oWindow_Id=" + di_Window + "&iField=" + iField + "&oField=" + oField + "&iIndex=" + iIndex + "&iInput=" + iInput;
            //
            //document.all[iField].focus();
            var dialog = $find(di_Window);
            dialog.show();
            dialog.get_header().setCaptionText(di_Caption);
            dialog.get_contentPane().set_contentUrl(di_OpenUrl_Full);
            dialog.show();
            document.all[iField].focus();
        }
        else {
            document.all[iField].focus();
        }
        return false;
    }
}


//form會員資料輸入選擇,webservice取資料 
function get_bcvw_cname_ri(iFunc, iContEdit, iField, oField, di_Window, di_OpenUrl, di_Caption) {
    vNum = document.all[iField].value;
    document.all[oField].value = "";
    if (vNum != "") {
        vNum = encodeURI(vNum);
        DD2015_45.wwwService.Svrwww.service_get_bcvw_cname_ri('key1', 'key2', iFunc, iContEdit, vNum, iField, oField, bcvw_cname_ri_onSuccess, servicr_onError);
    }
    return false;


    //servicr_成功 
    //rtype == '1' 表示正確抓到一筆
    //rtype == '0' 表示沒有抓到資料
    //rtype == '2' 表示有抓到1筆以上相似資料

    //receiveData=webservice回傳的data
    function bcvw_cname_ri_onSuccess(receiveData, userContext, methodName) {
        var r_str = receiveData;
        var rtype = GetTagValue(r_str, "rFlag");
        var iFunc = GetTagValue(r_str, "iFunc");
        var iField = GetTagValue(r_str, "iField");
        if (rtype == '1') {
            Show_rTable(r_str);
        }
        else if (rtype == '0') {
            alert('no found');
            document.all[iField].focus();
        }
        else if (rtype == '2') {
            document.all[iField].value = "";
            //
            var iContEdit = GetTagValue(r_str, "iContEdit");
            var iIndex = GetTagValue(r_str, "rIndex");
            var iInput = GetTagValue(r_str, "iInput");
            var oField = GetTagValue(r_str, "oField");
            //
            var di_OpenUrl_Full = di_OpenUrl + "?iFunc=" + iFunc + "&oWindow_Id=" + di_Window + "&iField=" + iField + "&oField=" + oField + "&iIndex=" + iIndex + "&iInput=" + iInput;
            //
            //document.all[iField].focus();
            var dialog = $find(di_Window);
            dialog.show();
            dialog.get_header().setCaptionText(di_Caption);
            dialog.get_contentPane().set_contentUrl(di_OpenUrl_Full);
            dialog.show();
            //
            document.all[iField].focus();
        }
        else {
            document.all[iField].focus();
        }
        return false;
    }
}


//servicr_onError失敗    
function servicr_onError(error, userContext, methodName) {
    if (error != null)
        alert(error.get_message());
}

//在WebDataGrid中,顯示多欄位查詢的回傳值,再Focus到iField
//
//iNewMod: mod=WebDataGrid edit,other=WebDataGrid Adding
//DataGrid_id=webDataGrid的ClientID
//row=WebDataGrid的row object
//iField=Focus欄位
function Show_rTable_gfocusField(iNewMod, DataGrid_id, row, iField) {
    webDataGrid = $find(DataGrid_id)
    if (iNewMod == "mod") {
        var cell = row.get_cellByColumnKey(iField);
        webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
    }
    else {
        var behaviors = webDataGrid.get_behaviors();
        var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
        var newRow = newRowBehavior.get_row();
        var activation = behaviors.get_activation();
        var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
        var columnKey = activeCell.get_column().get_key();
        //
        var focus_cell = newRow.get_cellByColumnKey(iField);
        webDataGrid.get_behaviors().get_activation().set_activeCell(focus_cell);
    }
}


//在WebDataGrid中,顯示多欄位查詢的回傳值,再Focus到iField
//
//iNewMod: mod=WebDataGrid edit,other=WebDataGrid Adding
//DataGrid_id=webDataGrid的ClientID
//row=WebDataGrid的row object
//r_str=類XML字串(webservice傳回的類XML字串)
function Show_rTable_grow_focus(iNewMod, DataGrid_id, row, r_str) {
    var iField = GetTagValue(r_str, "iField");
    webDataGrid = $find(DataGrid_id)
    if (iNewMod == "mod") {
        var cell = row.get_cellByColumnKey(iField);
        row.get_cellByColumnKey(iField).set_value("");
        //webDataGrid.get_behaviors().get_activation().set_activeCell(cell);
    }
    else {
        var behaviors = webDataGrid.get_behaviors();
        var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
        var newRow = newRowBehavior.get_row();
        var activation = behaviors.get_activation();
        var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
        var columnKey = activeCell.get_column().get_key();
        //
        var focus_cell = newRow.get_cellByColumnKey(iField);
        webDataGrid.get_behaviors().get_activation().set_activeCell(focus_cell);
        focus_cell.set_value("");
    }
}


//在WebDataGrid中,顯示多欄位查詢的回傳值
//在類XML字串中取tag=rTable中的資料
//將rValue的值 放入rField欄位中
//
//row=WebDataGrid的row object
//r_tbale=類XML字串(webservice傳回的類XML字串)
function Show_rTable_grow(row, r_tbale) {
    r_Datas = GetTagValue(r_tbale, "rTable");
    //
    var pfs = 0;
    var pfe = 0;
    var pvs = 0;
    var pee = 0;
    var ht_Field = "";
    var ht_Value = "";
    var ht_Text = "";
    //
    var pds = r_Datas.indexOf("<rData>", 0);
    var pde = r_Datas.indexOf("</rData>", 0);
    while (pds >= 0) {
        pds = pds + 7;
        pde = pde - 7;
        r_Fields = r_Datas.substr(pds, pde);
        ht_Field = GetTagValue(r_Fields, "rField");
        ht_Value = GetTagValue(r_Fields, "rValue");
        ht_Text = GetTagValue(r_Fields, "rText");
        if (ht_Field != "") {
            row.get_cellByColumnKey(ht_Field).set_value(ht_Value);
            if (ht_Text != "") {
                row.get_cellByColumnKey(ht_Field).set_text(ht_Text);
            }
        }
        try {
            r_Datas = r_Datas.substr(pde + 15);
            pds = r_Datas.indexOf("<rData>", 0);
            pde = r_Datas.indexOf("</rData>", 0);
        }
        catch (e) {
            r_Datas = "";
            pds = 0;
            pde = 0;
        }
    }
}


//在form中題示查詢資料(一段為textbox)
//在類XML字串中取tag=rTable中的資料
//將rValue的值 放入rField欄位中
//r_tbale=類XML字串(webservice傳回的類XML字串)
function Show_rTable(r_tbale) {
    r_Datas = GetTagValue(r_tbale, "rTable");
    //
    var pfs = 0;
    var pfe = 0;
    var pvs = 0;
    var pee = 0;
    var ht_Field = "";
    var ht_Value = "";
    //
    var pds = r_Datas.indexOf("<rData>", 0);
    var pde = r_Datas.indexOf("</rData>", 0);
    while (pds >= 0) {
        pds = pds + 7;
        pde = pde - 7;
        r_Fields = r_Datas.substr(pds, pde);
        ht_Field = GetTagValue(r_Fields, "rField");
        ht_Value = GetTagValue(r_Fields, "rValue");
        if (ht_Field != "") {
            document.all[ht_Field].value = ht_Value;
        }
        try {
            r_Datas = r_Datas.substr(pde + 15);
            pds = r_Datas.indexOf("<rData>", 0);
            pde = r_Datas.indexOf("</rData>", 0);
        }
        catch (e) {
            r_Datas = "";
            pds = 0;
            pde = 0;
        }
    }
}

//在類XML字串中取tag中的所有字串
//st_xml=傳入的類XML字串
//st_tag=tag的名稱
function GetTagValue(st_xml, st_tag) {
    var pts = st_xml.indexOf("<" + st_tag + ">", 0);
    var pte = st_xml.indexOf("</" + st_tag + ">", 0);
    var tlen = st_tag.length + 2;
    var rValue = "";
    if ((pts >= 0) && (pte > 0)) {
        rValue = st_xml.substr(pts + tlen, pte - pts - tlen);
    }
    return rValue;
}

//將string去掉後方的空白(chr(32))
//idvalue=傳入的字串,null不處理
function strtrim(idvalue) {

    var sc = "";
    var sr = "";
    var vhed = true;
    var vmid = false;
    var vbum = false;
    var sl = "";
    //
    if (idvalue != null) {
        var vlen = idvalue.length;
        for (i = 0; i < vlen; i++) {
            sc = idvalue.substr(i, 1);
            if ((sc == " ") && (vhed)) {
                sc = " ";
            }
            else {
                sr = sr + sc;
                vhed = false;
            }
        }
        //
        vbum = true;

        vlen = sr.length;
        for (i = vlen - 1; i >= 0; i--) {
            sc = sr.substr(i, 1);
            if ((sc == " ") && (vbum)) {
                sc = " ";
            }
            else {
                sl = sc + sl;
                vbum = false;
            }
        }
    }
    return (sl);
}


//在 webDataGrid edit模式,新增一行row
//webDataGrid_ClientID=webDataGrid的ClientID
//ar_row=['',0,null] 假如共3個必須=webDataGrid的Column數,包括Hidden欄位
//st_focus_field=new row 的 focus欄位名稱
//預留 scroll row_pix,目前保留不用
function webDataGrid_AddRow(webDataGrid_ClientID, ar_row, st_focus_field, row_pix) {
    var webDataGrid = $find(webDataGrid_ClientID);
    var rows = webDataGrid.get_rows();
    var rowsLength = rows.get_length();
    //試者增加一個row
    try {
        rows.add(ar_row);
    }
    catch (e) {
        alert(e.message);
    }
    //
    if (st_focus_field != "") {
        var lastRow;
        var cell;
        try {
            lastRow = rows.get_row(rowsLength);
            cell = lastRow.get_cellByColumnKey(st_focus_field);
            webDataGrid.get_behaviors().get_editingCore().get_behaviors().get_cellEditing().enterEditMode(cell);
        }
        catch (e) {

        }
    }
    return false;
}


//設定 webDataGrid Rowadding 模式,active row 的 focus欄位
//webDataGrid_ClientID=webDataGrid的ClientID
//st_focus_field=active row 的 focus欄位名稱
function webDataGrid_RowAddFocus(webDataGrid_ClientID, st_focus_field) {
    var webDataGrid = $find(webDataGrid_ClientID);
    var behaviors = webDataGrid.get_behaviors();
    var newRowBehavior = behaviors.get_editingCore().get_behaviors().get_rowAdding();
    var newRow = newRowBehavior.get_row();
    var activation = behaviors.get_activation();
    var focus_cell = newRow.get_cellByColumnKey(st_focus_field);
    //
    webDataGrid.get_behaviors().get_activation().set_activeCell(focus_cell);
    return false;
}


//設定 webDataGrid edit 模式,active row 的 focus欄位
//webDataGrid_ClientID=webDataGrid的ClientID
//st_focus_field=active row 的 focus欄位名稱
function webDataGrid_ModRowfocus(webDataGrid_ClientID, st_focus_field) {
    var webDataGrid = $find(webDataGrid_ClientID);
    var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
    var row = activeCell.get_row();
    var focus_cell = row.get_cellByColumnKey(st_focus_field);
    webDataGrid.get_behaviors().get_activation().set_activeCell(focus_cell);
    return false;
}

//設定 webDataGrid activeCell 的值
//webDataGrid_ClientID=webDataGrid的ClientID
//st_field=cell key
//st_value=要設定的值
function webDataGrid_SetFieldValue(webDataGrid_ClientID, st_field, st_value) {
    var webDataGrid = $find(webDataGrid_ClientID);
    var activeCell = webDataGrid.get_behaviors().get_activation().get_activeCell();
    var row = activeCell.get_row();
    row.get_cellByColumnKey(st_field).set_value(st_value);;
    return false;
}


// delay時間 ms=千分之一秒
function delay(ms) {
    ms += new Date().getTime();
    while (new Date() < ms) { }
}
