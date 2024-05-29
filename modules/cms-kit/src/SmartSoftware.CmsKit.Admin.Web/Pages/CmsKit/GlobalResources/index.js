$(function (){
    var l = ss.localization.getResource("CmsKit");
    
    var service = smartsoftware.cmsKit.admin.globalResources.globalResourceAdmin;

    var scriptEditor = CodeMirror.fromTextArea(document.getElementById("ScriptContent"),{
        mode:"javascript",
        lineNumbers:true
    });

    var styleEditor = CodeMirror.fromTextArea(document.getElementById("StyleContent"),{
        mode:"css",
        lineNumbers:true
    });

    $('.nav-tabs a').on('shown.bs.tab', function() {
        scriptEditor.refresh();
        styleEditor.refresh();
    });

    $('#SaveResourcesButton').on('click','',function(){
        service.setGlobalResources(
            {
                style: styleEditor.getValue(),
                script: scriptEditor.getValue()
            }
        ).then(function () {
            ss.message.success(l("SavedSuccessfully"));
        });
    });
});