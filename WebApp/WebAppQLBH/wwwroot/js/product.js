var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: "/admin/product/getall",
        },
        columns: [
            { "data": "title", "width": "20%"},
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%"},
            {
                "data": 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group" >
                        <div style="display:inline-block;vertical-align: middle;">
                            <a href = "/admin/product/upsert?id=${data}" class="btn btn-primary mx-2 rounded" style="width:100px"><i class="bi bi-pencil-square"></i> Edit</a >
                        </div>
                        <div style="display:inline-block;vertical-align: middle;">
                            <a onClick = Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2 rounded" style="width:100px"><i class="bi bi-trash-fill"></i> Delete</a>
                        </div>
                    </div >`
                },
                "width": "25%",
            }
        ],
    });
}
function Delete(url){
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}
