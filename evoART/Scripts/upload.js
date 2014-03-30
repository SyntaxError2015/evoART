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
    uploadImage(files,"");
}

function uploadImage(files, fData) {

    if (fData != "") {
        var formData = fData;
        files = $("#ImageBrowse")[0].files;
    } else {
        var formData = new FormData();
        formData.append('file', files[0]);
    }
    
    formData.append('photoId', photos[photoNumber]);
    
    //Check if the file is an image
    var imageType = /image.*/;
    if (!files[0].type.match(imageType)) {
        showMessage("alert-warning", "You need to select an image file!");
        return;
    }
    
    //Create the new album
    $.get('/Photos/CreateNewPhoto', function (result) {
        $('#dropbox').append(result);
        doAJAX(formData);
    });

    
}

function doAJAX(formData) {
    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    //Do something with upload progress here
                    var percentVal = percentComplete * 100 + '%';
                    $("#progress-" + photos[photoNumber]).width(percentVal);
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

            alert(data);
        },
        error: function (response) {

            showMessage("alert-warning", "The upload failed!");
        }
    });
}

$(document).ready(function (e) {
    $('#uploadImageForm').on('submit', (function (e) {
        e.preventDefault();
        var formData = new FormData(this);

        uploadImage('', formData);
    }));
    

});

$(document).ready(function (e) {

    var photoNumber = 0;
    var photos = new Array();
});