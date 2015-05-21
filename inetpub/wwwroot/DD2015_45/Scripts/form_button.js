function ClientCommand(oButton, oEvent) {
    if (oButton._clientID.indexOf("bt_05") >= 0) {
        oEvent.needPostBack = btnDEL_c();
    }
    else if (oButton._clientID.indexOf("bt_DEL") >= 0) {
        oEvent.needPostBack = btnDEL_c();
    }
    else {
        return true;
    }
}

function btnDEL_c() {
    return confirm('是否確定刪除此筆資料?');
}

function btnDEL0_c() {
    alert('沒有資料可以刪除!');
    return false;
}

function btnMOD_c() {
    return confirm('是否更正此筆資料?');
}

function btnMOD0_c() {
    alert('沒有資料可以更正,請先查詢!');
    return false
}
