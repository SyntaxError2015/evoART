var dropall;
dropall = document.getElementById("content");
dropall.addEventListener("dragenter", dragallenter, false);

function dragallenter(e) {
    e.stopPropagation();
    e.preventDefault();
    $("#dropbox").toggle();
}

var dropbox;

dropbox = document.getElementById("dropbox");
dropbox.addEventListener("dragenter", dragenter, false);
dropbox.addEventListener("dragleave", dragleave, false);
dropbox.addEventListener("dragover", dragover, false);
dropbox.addEventListener("drop", drop, false);

function dragenter(e) {
    e.stopPropagation();
    e.preventDefault();
    //merge
}

function dragleave(e) {
    e.stopPropagation();
    e.preventDefault();
    $("#dropbox").toggle();
}

function dragover(e) {
    e.stopPropagation();
    e.preventDefault();
}

function drop(e) {
    e.stopPropagation();
    e.preventDefault();

    var dt = e.dataTransfer;
    var files = dt.files;

    //alert(files[0].file);
    uploadImage(files, "");
}

var frmData;
function uploadImage(files, fData) {

    if (fData != "") {
        var formData = fData;
        files = $("#ImageBrowse")[0].files;
    } else {
        var formData = new FormData();
        formData.append('file', files[0]);
    }


    //Check if the file is an image
    var imageType = /image.*/;
    if (!files[0].type.match(imageType)) {
        showMessage("alert-warning", "You need to select an image file!");
        return;
    }

    frmData = formData;

    //Create the new album
    $.get('/Photos/CreateNewPhoto', function (result) {
        doAJAX(frmData, result);
    });


}

function doAJAX(formData, result) {

    $("#dropbox").append(result);

    formData.append('photoId', window.photos[window.photoNumber]);

    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    //Do something with upload progress here
                    var percentVal = percentComplete * 100 + '%';
                    $("#progress-" + window.photos[window.photoNumber]).width(percentVal);
                    //$("#status").append(percentVal + "<br>");
                }
            }, false);

            return xhr;
        },
        type: 'POST',
        url: "/Photos/Upload",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            $("#pic-" + data).attr('src', '/Content/Temp/' + data + '.jpg');
            //alert(data);
        },
        error: function (response) {

            showMessage("alert-warning", "The upload failed!");
        }
    });
}

function savePhoto(data, photoId) {
    if (data == "K") {
        $("#" + photoId).html("");
        $("#" + photoId).hide();
    } else showMessage("alert-danger", "The photo was not saved!");
}

function deleteNewPhoto(data, photoId) {
    if (data == "K") {
        $("#" + photoId).html("");
        $("#" + photoId).hide();
    } else showMessage("alert-danger", "The photo was not deleted!");
}

$(document).ready(function (e) {
    $('#uploadImageForm').on('submit', (function (e) {
        e.preventDefault();
        var formData = new FormData(this);

        uploadImage('', formData);
    }));


});


var photoNumber = 0;
var photos = new Array();
