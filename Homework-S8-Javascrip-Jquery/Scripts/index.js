
function SubmitForm() {
    var temp = document.getElementById("demo").value;
    console.log(temp);
    alert(temp);
}


$(document).ready(function () {
    $("#btnSubmit").click(function () {
        
        alert($("demo").value);
    })
});
