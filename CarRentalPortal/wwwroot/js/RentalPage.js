function fetchCars(transmision, varient) {
    $('#carSelector')
        .find('option')
        .remove()
        .end()
        .append('<option selected value="0">---Select---</option>')
        .val('whatever')
        ;
    var newSelect = document.getElementById("carSelector");
        fetch("https://localhost:44305/api/Car/getlist/" + transmision + "/" + varient).then(response => response.json())
            .then((items) => {
                $.each(items, function (i, item) {
                    var opt = document.createElement("option");
                    opt.value = item.carId;
                    opt.innerHTML = item.carName; // whatever property it has
                    // then append it to the select element
                    newSelect.appendChild(opt);
                });
            });
}
$(document).ready(function () {

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    $(".next").click(function () {
        var from = new Date( $('#fromDatePicker').val());
        var to = new Date ($('#toDatePicker').val());
        if (from <= to) {

            current_fs = $(this).parent();
            next_fs = $(this).parent().next();

            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

            //show the next fieldset
            next_fs.show();
            //hide the current fieldset with style
            current_fs.animate({ opacity: 0 }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                    });
                    next_fs.css({ 'opacity': opacity });
                },
                duration: 600
            });
        }
    });
    $("#varientNext").click(function () {
        var transmision = $('#transmissionSelector').find(":selected").val();
        var varient = $('#varientSelector').find(":selected").val();
        console.log(transmision + " " + varient);
        fetchCars(transmision,varient);
            current_fs = $(this).parent();
            next_fs = $(this).parent().next();

            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

            //show the next fieldset
            next_fs.show();
            //hide the current fieldset with style
            current_fs.animate({ opacity: 0 }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                    });
                    next_fs.css({ 'opacity': opacity });
                },
                duration: 600
            });
        
    })

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });


});
