$(document).ready(function () {
    let file;
    $("#file").on("change", function () {
        //console.log($("#file").get(0).files[0])
        // $(".image").attr("src", $("#file").get(0).file[0]);
        const fileReader = new FileReader();
        file = $("#file").get(0).files[0];
        //
        fileReader.readAsDataURL(file);
        fileReader.addEventListener("load", function () {
            $("#image").attr("src", this.result)
        })
    })

})