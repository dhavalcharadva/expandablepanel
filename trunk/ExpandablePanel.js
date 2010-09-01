function fnTogglePanel(objname){
    
    if(document.getElementById(objname).style.display == "none"){
        document.getElementById(objname + "_UpDownButton").innerHTML = "Hide";
        document.getElementById(objname + "_UpDownButton").title = "Click to hide this panel";
        document.getElementById(objname).style.display = "";
    }
    else{
       document.getElementById(objname + "_UpDownButton").innerHTML = "Show";
       document.getElementById(objname + "_UpDownButton").title = "Click to show this panel";
       document.getElementById(objname).style.display = "none";
    }
    document.getElementById(objname + "_hidden").value = document.getElementById(objname).style.display;
}