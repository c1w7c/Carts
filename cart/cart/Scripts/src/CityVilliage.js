$(document).ready(function () {
    let value = '';
    let Villiage = {};
    $('#city').change(function () {
        value = $('#city').val();
        //console.log(value);
        Villiage.CityId = value;
        $.ajax({
            type: "post",
            url: "../Register/Villiage",
            data: Villiage,
            contentsType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log(response.villiage);
                var villiageData = response.villiage;
                $("#villiage").removeClass("d-none").append("<option value='' disabled>請選擇行政區</option>");
                for(var i = 0; i < villiageData.length; i++){
                    $("#villiage").append(`<option value='${villiageData[i]}'>${villiageData[i]}</option>`)
                 }
            }
        });
        
    });
   
});
