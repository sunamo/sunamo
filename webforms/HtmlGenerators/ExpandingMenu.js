//function createNewNavigation(deep, i : number = 32767) {
//    var first = prompt("Zadejte název nové kategorie:").trim();
//    if (first == "") { //
//        ToStatus_StatusType(StatusType.varovani, "Nemůžete nechat název nové kategorie prázdný");
//    }
//    else {
//        var r = ajaxGet4("Shp_CreateNewCategory.ashx?newCategory=" + encodeURIComponent(first) + "&upCategory=" + i + "&deep=" + deep);
//    if (isStatus(r)) {
//        ToStatus2(r);
//    }
//    else {
//        submit();
//    }
//# sourceMappingURL=ExpandingMenu.js.map