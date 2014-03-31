function deleteAlbum(idAlbum) {
    if (confirm("Are you sure you want to delete this album?")) {
        $.ajax({
            url: "/Albums/Delete/" + idAlbum,
            success: function (response) {
                if (response == "K")
                    $("#" + idAlbum).toggle();
                else
                    showMessage("alert-danger", "Something went wrong! The album was not deleted!");
            }
        });
    }
}

function createAlbum(data) {
    if (data == "K") {
        showMessage("alert-success", "The new album was added successfully!");
        $("#addAlbum").toggle(200);
        setupPage(document.location.pathname);
    }

    if (data == "N") {
        showMessage("alert-danger", "Insert a name for the new album!");
    }

    if (data == "F") {
        showMessage("alert-danger", "Something went wrong! Reload the page and try again!");
        $("#addAlbum").toggle(200);
    }

}

