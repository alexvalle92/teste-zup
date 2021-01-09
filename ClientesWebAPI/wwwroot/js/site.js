function ChamadaAjax(url, data, tipo, mycallback) {
    var xhr;
    if (!window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    else {
        xhr = new XMLHttpRequest();
    }
    //document.getElementById("carregando").style.display = "block";

    /*if (tipo === "GET") {
        url += "?buster=" + new Date().getTime();
    }*/

    xhr.open(tipo, url);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.setRequestHeader('Cache-Control', 'no-cache,no-store');
    xhr.onreadystatechange = function () {
        try {
            var ndata;
            if (xhr.status) {
                if (xhr.status == 200 && xhr.readyState == 4) {
                    //document.getElementById("carregando").style.display = "none";
                    try {
                        ndata = JSON.parse(xhr.responseText);
                    } catch (e) {
                        try {
                            if (xhr.responseText !== null)
                                document.writeln(xhr.responseText);
                        } catch (ex) {
                            //alert(ex);
                        }
                    }
                    mycallback(ndata);
                }
                else {
                    //alert('Erro');
                }
            }
        } catch (ex) {
        }
    };
    xhr.send(data);
}