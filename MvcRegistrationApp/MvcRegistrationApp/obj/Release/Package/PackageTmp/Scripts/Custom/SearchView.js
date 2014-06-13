


    
    

    function changeStyle(obj) {

        if (obj.checked) {
            $("." + obj.id).css("display", "table-cell")
        }
        else {
            $("." + obj.id).css("display", "none")
        }


    }
    


    

    function DisplayAll() {
        //alert("dis");
        $(".SS").css("display", "table-cell");
        $(".JS").css("display", "table-cell");
        $(".SOWDate").css("display", "table-cell");
        $(".SR").css("display", "table-cell");
        $(".tech").css("display", "table-cell");
        $(".Mtech").css("display", "table-cell");
        $(".FT").css("display", "table-cell");
        $(".RM").css("display", "table-cell");
        $(".ASD").css("display", "table-cell");
        $(".TED").css("display", "table-cell");
        $(".RM").css("display", "table-cell");
        $(".RT").css("display", "table-cell");
        $(".HR").css("display", "table-cell");
        $(".WL").css("display", "table-cell");
        $(".D").css("display", "table-cell");
        $(".BU").css("display", "table-cell");
        $(".RT").css("display", "table-cell");
        $(".RN").css("display", "table-cell");
        $(".GAR").css("display", "table-cell");

        $(".dataTables_scrollBody").find('thead').hide();
    }
    function HideAll() {
        //alert("hide");
        $(".SS").css("display", "none");
        $(".JS").css("display", "none");
        $(".SOWDate").css("display", "none");
        $(".SR").css("display", "none");
        $(".tech").css("display", "none");
        $(".Mtech").css("display", "none");
        $(".FT").css("display", "none");
        $(".RM").css("display", "none");
        $(".ASD").css("display", "none");
        $(".TED").css("display", "none");
        $(".RM").css("display", "none");
        $(".RT").css("display", "none");
        $(".HR").css("display", "none");
        $(".WL").css("display", "none");
        $(".D").css("display", "none");
        $(".BU").css("display", "none");
        $(".RT").css("display", "none");
        $(".RN").css("display", "none");
        $(".GAR").css("display", "none");

        $(".dataTables_scrollBody").find('thead').hide();
    }