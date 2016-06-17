 
 



 
    $('input[type=checkbox]').change(function () {
        if (!this.checked) {
            $(this).closest('td').parent('').find('td:last').find('input').prop('disabled', true);
            $(this).closest('td').parent('').find('td:last').find('input').val('');
        }
        else {
            $(this).closest('td').parent('').find('td:last').find('input').prop('disabled', false);
            $(this).closest('td').parent('').find('td:last').find('input').focus();
        }
    });
 //$('#txtsearch').change(){

//}
$(function () {
    $('#txtsearcha').on('change', function () {

        $('.linkaz').submit();

    });
});
function Submitit1() {
    //showcartLaundry();
    //showcart()
    $('.submitit').submit();
}
function seletedService() {
    $('#seletedService').show();
}

function remove1() {
    $('#rem').remove();
    //$(this).closest('td').parent('').find('td:last').find('input').val(null);
}
//$('button').click(function () {
//    $(this).closest('tr').hide();
//    $(this).closest('td').parent('').find('td:last').find('input').val(null);
//});

function CountRows() {
    $('#countrow').show;
    c = $("#svc tr").length;
    document.getElementById("row").innerHTML = c;
    //$("p:first").replaceWith("Hello world!");
    //$('#l').replaceWith(c);
    //alert($('#j').val());
}
function addtocart() {
    c = $("#tableSelected tr").length;
}

function Uncheckedall() {
    var all_checkboxes = jQuery(':checkbox');
    $(all_checkboxes).closest('td').parent('').find('td:last').find('input').prop('disabled', true);
    $(all_checkboxes).closest('td').parent('').find('td:last').find('input').val('');
    all_checkboxes.prop('checked', false)
}


 
    $('.linka').click(function () {
        var ddlRoomtypevalues = $("#txtsearch").val();
        this.href = this.href.replace("txsearch", ddlRoomtypevalues);
    });
 


$('#butt').click(function () {
    var sada = $("#tableSelected tr").length;
    this.href = this.href.replace("ccd", $("#tableSelected tr").length);
    $(".abc").submit();
});



//$(function () {
//    $('.btn btn-danger').click(function () {
//        $(this).closest('tr').remove();
//    });
//});
//$('button').click(function () {
//    alert('Hello');

//});
//function ples() {
//    $("#svc").find('tr').each(function () {

//        $(this).find('td').closest('tr').remove();
//    });

//}
//          //var i = r.parentNode.parentNode.rowIndex;
//          //document.getElementById("dataTables-cart").deleteRow(i); 
//}


function ples(index) {
    if ($('#svc tr').length > 0) {
        $(index).parents('tr').remove();

        var index = 0;
        $('#svc tr').each(function () {
            var this_row = $(this);
            this_row.find('input[name$=".Name"]').attr('name', '_service[' + index + '].Name');
            this_row.find('input[name$=".Description"]').attr('name', '_service[' + index + '].Description');
            this_row.find('input[name$=".Facility"]').attr('name', '_service[' + index + '].Facility');
            this_row.find('input[name$=".Charges"]').attr('name', '_service[' + index + '].Charges');
            this_row.find('input[name$=".Qty"]').attr('name', '_service[' + index + '].Qty');
            this_row.find('input[name$=".Id"]').attr('name', '_service[' + index + '].Id');
            index++;

        });


        var index1 = $('table #svc tr').length;
        $('#itq').html('Item in Cart:' + index1);

        //$(' #svc').find('tr').each(function () {
        //    $(this).attr(id,a[i]);
        //});

    }
}

//function showcart() {
    
//    $(document).ready(function () {
//        $("#seletedService").dialog({
//            autoOpen: false,
//            autoClose: true,
//            title: 'Order Detail',
//            width: 1100,
//            height: 600,
//            modal: true
//        });
//    });
//    function openPopup1() {
//        $("#seletedService").dialog("open");
//    }


//}
function showcartLaundry() {
    $('#divLaundryList').modal({ backdrop: 'static' })
    //c = $("#svcLaundry tr").length;divLaundryList
    //if (c >= 1) {
    //    $('#AddCartLaundry').hide();
    //    $('#itqLaundry').hide();
    //  //  $('#butNProceedLaundry').show();
    //    $('#butNContinueOrderLaundry').show();
    //    $('#seletedServiceLaundry').show();
    //    $('#hideiitLaundry').hide();
    //    $('#ddlTaxCategorName').hide();
    //   // $('#hideiit1').hide();
    //    // $('#hideiit2').hide(); 
    //    $('#dicAddToCardLaundry').show();
    //}
}
function CloseLaundry()
{
    $("#divLaundryList").modal('hide')
}
function showcart() {
  
    $('#divServicesDetailsDetailsList').modal({ backdrop: 'static' })
    //c = $("#svc tr").length;
    //if (c >= 1) {
    //    $('#AddCart').hide();
    //    $('#itq').hide();
    //  //  $('#butNProceed').show();
    //    $('#butNContinueOrder').show();
    //    $('#seletedService').show();
    //    $('#hideiit').hide();
    //    $('#hideiit1').hide();
    //    $('#hideiit2').hide();
    //    $('#dicAddToCard').show();
    //}
}
function Servicesclose()
{
   
    $("#divServicesDetailsDetailsList").modal('hide')
}
function continue1() {
    $('#hideiit').show();
    $('#seletedService').hide();
    $('#hideiit1').show();
    $('#hideiit2').show();
    $('#AddCart').show();
    $('#itq').show();
  //  $('#butNProceed').hide();
    $('#butNContinueOrder').hide();
    $('#dicAddToCard').hide();
}
function continueLaundry() {
    $('#hideiitLaundry').show();
    $('#seletedServiceLaundry').hide();
    $('#hideiit1Laundry').show();
    $('#hideiit2Laundry').show();
    $('#AddCartLaundry').show();
    $('#itqLaundry').show();
   // $('#butNProceedLaundry').hide();
    $('#butNContinueOrderLaundry').hide();
    $('#dicAddToCardLaundry').hide();
    $('#ddlTaxCategorName').show();
}

$('#AddCart').click(function (event) { 
    $('#dataTables-example12v ').find("input[type='checkbox']").each(function () {

      
                if ($(this).is(':checked'))
                {
                   

                    var SvcId = $(this).parents('tr').find('#ItemsId').val();

                    var Svcname = $(this).parents('tr').find('#Itemsname').val();
                    var Svcdes = $(this).parents('tr').find('#ItemsDispName').val();
                    var Svcfacity = $(this).parents('tr').find('#Ablquantity').val();

                    var Svccge = $(this).parents('tr').find('td').find('#Rate').val();
                    var Svcqty = $(this).parents('tr').find('.qut').val();
                    var index = $('#dataTables-cart #svc').find('tr').length;

                    var Str = '<tr>';
                  
                    if (Svcqty != 0)
                        {
                        Str = Str + "<td>  <input type=button id='_service" + index + "__button' name=_service1[" + index + "].button  class=btn btn-danger id='dddll' onclick='ples(this);'  value='X'   > </td>";
                        Str = Str + "<td><input  type=hidden type=label id='_service" + index + "__Name' name=_service1[" + index + "].Name value=" + Svcname + "   >" + Svcname + "</td>";
                        Str = Str + "<td><input type=hidden    id='_service" + index + "__Description' name=_service1[" + index + "].Description value=" + Svcdes + "   >" + Svcdes + " </td>";
                        Str = Str + "<td><input type=hidden    id='_service" + index + "__Facility' name=_service1[" + index + "].Facility value=" + Svcfacity + "   >" + Svcfacity + " </td>";
                        Str = Str + "<td><input type=hidden    id='_service" + index + "__Charges' name=_service1[" + index + "].Charges value=" + Svccge + "  >" + Svccge + " </td>";
                        Str = Str + "<td id='qty'><input type=text    id='_service" + index + "__Qty' name=_service1[" + index + "].Qty  class=svcqty value=" + Svcqty + "   > </td>";
                        Str = Str + "<td><input type=hidden id='_service" + index + "__Id' name=_service1[" + index + "].Id value=" + SvcId + "  > </td>";
                    Str = Str + '</tr>';

                    $('table #svc').append(Str);
                    }
                }
         
        });
      
        var index = $('table #svc tr').length;
        $('#itq').html('Item in Cart:' + index);
        Uncheckedall();

    });

 
$('#AddCartLaundry').click(function (event) {
    $('#dataTables-example12vLaundry').find("input[type='checkbox']").each(function () {


        if ($(this).is(':checked')) {


            var SvcId = $(this).parents('tr').find('#ItemsId').val();

            var Svcname = $(this).parents('tr').find('#Itemsname').val();

            var Svccge = $(this).parents('tr').find('td').find('#Rate').val();
            //alert(Svccge)
            var Svcqty = $(this).parents('tr').find('.qut').val();
            var index = $('#dicAddToCardLaundry #svcLaundry').find('tr').length;

            var Str = '<tr>';

            if (Svcqty != 0) {
                Str = Str + "<td>  <input type=button id='laundryList" + index + "__button' name=laundryList[" + index + "].button  class=btn btn-danger id='dddll' onclick='ples(this);'  value='X'   > </td>";
                Str = Str + "<td><input  type=hidden  id='laundryList" + index + "__Name' name=laundryList[" + index + "].Name value=" + Svcname + "   >" + Svcname + "</td>";
             
               
                Str = Str + "<td><input type=hidden    id='laundryList" + index + "__Amount' name=laundryList[" + index + "].Amount value=" + Svccge + "  >" + Svccge + " </td>";
                Str = Str + "<td id='qty'><input type=text    id='laundryList" + index + "__qut' name=laundryList[" + index + "].qut  class=svcqty value=" + Svcqty + "   > </td>";
                Str = Str + "<td><input type=hidden id='laundryList" + index + "__Id' name=laundryList[" + index + "].Id value=" + SvcId + "  > </td>";
                Str = Str + '</tr>';

                $('table #svcLaundry').append(Str);
            }
        }

    });

    var index = $('table #svcLaundry tr').length;
    $('#itqLaundry').html('Item in Cart:' + index);
    Uncheckedall();

});
