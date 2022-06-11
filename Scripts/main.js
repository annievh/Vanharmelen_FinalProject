let Sortable = {
    baseUrl: '',
    sortBy: 0,
    searchTerm: '',
    Search() {
        let searchKey = $('#txtSearch').val();
        window.location.href = Sortable.baseUrl + "/" + searchKey;
    },
    Sort(sortBy, baseLink) {
        window.location.href = Sortable.baseUrl + "?sortBy=" + sortBy;
    }
};


var apiHandler  {
    DELETE(url) {
        if (confirm("Are you sure you want to delete?")) {
            $.ajax({
                url: url,
                type: 'GET',
                success: function (res) {
                    debuggerl
                }
            });
        } else {
            alert("Luckily we asked!");
        }
    }
};
