var companyData;

$(document).ready(function () {
    companyView();
});

function companyView() {
    companyData =
        $('#ApplicationUser_Role').change(function () {
            var selection = $('#ApplicationUser_Role Option:Selected').text();
            if (selection == 'Company') {
                $('#ApplicationUser_CompanyId').show();
            }
            else {
                $('#ApplicationUser_CompanyId').hide();
            }
        });


}





