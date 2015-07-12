$.fn.extend({
    UploadFile: function (callback, datajson, ElementId, url) {
        if ($(this).val() == "") { alert("请选择文件！"); return; }
        $.blockUI();
        $.ajaxFileUpload(
	    {
	        url: url,
	        secureuri: false,
	        fileElementId: ElementId,
	        dataType: 'json',
	        type: 'post',
	        data: datajson,
	        success: function (data, status) {
	            $.unblockUI();
	            if (data.status == 'success') {
	                recover();
	                if (dataTempObject.Type == "File") {
	                    if (dataTempObject.IsMultiple) {
	                        callback(data.Attachment);
	                    }
	                    else {
	                        callback(data.Attachment[0]);
	                    }
	                }
	                else if (dataTempObject.Type == "Excel") {
	                    callback(data.ExcelValue);
	                    if (typeof (data.msg) != "undefined" && data.msg != null && data.msg != "") {
	                        alert(data.msg);
	                    }
	                }
	            }
	            else {
	                alert(data.msg);
	            }
	        },
	        error: function (data, status, e) {
	            $.unblockUI();
	            alert(data.msg);
	        }
	    });
    }
});
//保存或解析文件
function SaveExcel() {
    if (dataTempObject.Type == "File") {
        var action = "save";
        if (!dataTempObject.IsSaveFile) {
            action = "nosave";
        }
        var url = '/Control/upload_ajax.ashx?Action=' + action + '&random=' + Math.random();
        if (dataTempObject.IsMultiple) {
            var arrayID = new Array();
            $.each($(".fileUpload"), function (i, item) {
                var id = "fileUpload" + i;
                arrayID.push(id);
                $(item).attr("id", id);
            });
            var attach = $("#FileUpload0").UploadFile(dataTempObject.CallBack, dataTempObject.Data, arrayID, url);
        }
        else {

            var filePath = $("#FileUpload1").val();
            if (dataTempObject.AllowExtention != undefined && dataTempObject.AllowExtention != "") {
                if (filePath != "") {
                    var extention = filePath.substring(filePath.lastIndexOf(".") + 1).toLowerCase();
                    if (dataTempObject.AllowExtention.indexOf(extention) <= -1) {
                        alert("文件格式不正确，请选择文件格式" + dataTempObject.AllowExtention + "！"); return;
                    }
                    else {
                        var attach = $("#FileUpload1").UploadFile(dataTempObject.CallBack, dataTempObject.Data, $("#FileUpload1").attr("id"), url);
                    }
                }
                else {
                    alert("请选择文件！"); return;
                }
            }
            else {
                var attach = $("#FileUpload1").UploadFile(dataTempObject.CallBack, dataTempObject.Data, $("#FileUpload1").attr("id"), url);
            }

        }
    }
    else if (dataTempObject.Type == "Excel") {
        var url = dataTempObject.Url + '?action=' + dataTempObject.Action + '&random=' + Math.random();
        var allowExtention = ".xls,.xlsx"; //允许上传文件的后缀名
        var filePath = $("#FileUpload1").val();
        if (filePath != "") {
            var extention = filePath.substring(filePath.lastIndexOf(".") + 1).toLowerCase();
            if (dataTempObject.AllowExtention.indexOf(extention) <= -1) {
                alert("文件格式不正确，请选择Excel文件！"); return;
            }
            else {
                var attach = $("#FileUpload1").UploadFile(dataTempObject.CallBack, dataTempObject.Data, $("#FileUpload1").attr("id"), url);
            }
        }
    }
    return false;
}

var dataTempObject = {
    IsMultiple: false,
    IsSaveFile: false,
    AllowExtention: '',
    Action: "",
    Type: "File",
    Data: {
        BusinessAttachmentID: "",
        BusinessType: "",
        BusinessID: "",
        AlwaysDelete: false,
    },
    CallBack: undefined,
    Url: ''
}

function ImportExcelFile(url, action, callback, data) {
    dataTempObject.Type = "Excel";
    if (data == undefined) {
        dataTempObject.Data = {};
    }
    else {
        dataTempObject.Data = data;
    }
    if (action == undefined) {
        dataTempObject.Action = "";
    }
    else {
        dataTempObject.Action = action;
    }
    dataTempObject.CallBack = callback;
    dataTempObject.Url = url;
    showwin(false);
}

function DeleteFile(attachmentid, callback) {
    if (attachmentid != null && attachmentid != undefined && attachmentid != "") {
        var url = '/Control/upload_ajax.ashx?Action=delete&random=' + Math.random();
        $.ajax({
            url: url,
            dataType: 'json',
            type: 'post',
            data: { BusinessAttachmentID: attachmentid },
            success: function (data, status) {
                if (data.status == 'success') {
                    callback(attachmentid);
                }
                else {
                    alert(data.msg);
                }
            },
            error: function (data, status, e) {
                alert(data.msg + "|" + status);
            }
        })
    }
}

function ImportFile(data, callback, IsMultiple, IsSaveFile, allowExtention) {
    if (data != null && data != undefined && data != "") {
        dataTempObject.Data = data;
        dataTempObject.IsSaveFile = false;
    }
    dataTempObject.CallBack = callback;
    if (IsMultiple != undefined) {
        dataTempObject.IsMultiple = false;
    }
    else {
        dataTempObject.IsMultiple = IsMultiple;
    }
    if (IsSaveFile == undefined) {
        dataTempObject.IsSaveFile = true;
    }
    else {
        dataTempObject.IsSaveFile = IsSaveFile;
    }
    dataTempObject.AllowExtention = allowExtention;
    showwin(dataTempObject.IsMultiple);
}

var hscrollTemp = 0;
var xscrollTemp = 0;
function showwin(IsMultiple) {
    var x = document.documentElement.scrollLeft;
    var hscroll = document.documentElement.scrollTop;
    hscrollTemp = hscroll;
    xscrollTemp = x;
    window.scrollTo(x, 0);

    if ($("#shadow").length <= 0) {
        var html = '<div id="bgrepeter"></div>';
        $("body").append(html);
    }

    if ($("#detailrepeter").length <= 0) {
        var html = '<div id="detailrepeter">' +
        '<table border="0" class="tbFiles" cellpadding="0" cellspacing="0" style="width: 100%;">' +
           '<tr class="file">' +
        '<td>' +
        '<input type="file" class="fileUpload"  style="width: 100%" name="fileUpload" id="FileUpload1" />' +
        '</td>' +
        '<td style="width: 50px"></td>' +
        '</tr>' +

        '</table>' +
         '<script type="text/html" id="templateTr">' +
         '<tr>' +
             '<td>' +
                 '<input type="file" class="fileUpload" style="width: 100%"  id="fileUpload" name="fileUpload" />' +
             '</td>' +
             '<td>' +
                 '&nbsp;&nbsp;<input type="button" class="buttondelete" value="删除" onclick="removeFile(this)" /></td>' +
         '</tr>' +
     '</script>' +

   ' <table style="width: 100%;">' +
        ' <tr>' +
            ' <td align="center">';
        //if (IsMultiple) {
        //    html = html + ' <input type="button" class="button" value="AddFile" onclick="addFile();" />'
        //}
        html = html + '  <input type="button" class="button" value="OK" onclick="return SaveExcel();" />' +
       '   <input type="button" class="button" value="Close" onclick="recover();" /><br />' +
   '   </td>' +
 '  </tr>' +
 '  </table>' +
 '</div>';
        $("body").append(html);
    }

    $(".fileUpload").attr("id", "FileUpload1");

    document.getElementsByTagName("html")[0].style.overflow = "hidden";
    document.getElementById("bgrepeter").style.display = "block";
    document.getElementById("detailrepeter").style.display = "block";
}
function removeFile(obj) {
    $(obj).parent().parent().remove();
}
function addFile() {
    $(".tbFiles").append($("#templateTr").html());
}
function recover() {
    document.getElementsByTagName("html")[0].style.overflow = "auto";
    document.getElementById("bgrepeter").style.display = "none";
    document.getElementById("detailrepeter").style.display = "none";
    window.scrollTo(xscrollTemp, hscrollTemp);
}
