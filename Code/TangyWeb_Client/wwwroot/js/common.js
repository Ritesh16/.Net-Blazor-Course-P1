window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 5000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 5000 });
    }
}

function ShowDeleteConfirmation() {
    console.log(2);
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmation() {
    $('#deleteConfirmationModal').modal('hide');
}
