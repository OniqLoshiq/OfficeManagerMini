tinymce.init({
    selector: '#mytextarea',
    /* plugin */
    plugins: [
        "advlist autolink link image lists charmap print preview hr anchor pagebreak",
        "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
        "save table contextmenu directionality emoticons template paste textcolor"
    ],

    /* toolbar */
    toolbar: "insertfile undo redo | styleselect fontselect fontsizeselect | bold italic underline| alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullscreen | forecolor backcolor hr emoticons",
})

//https://www.tiny.cloud/docs/advanced/editor-control-identifiers/#toolbarcontrols