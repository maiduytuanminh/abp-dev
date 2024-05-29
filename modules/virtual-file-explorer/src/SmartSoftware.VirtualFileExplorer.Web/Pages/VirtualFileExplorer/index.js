var _fileContentModal = new ss.ModalManager(
    ss.appPath + 'VirtualFileExplorer/FileContentModal'
);

function showContent(filePath) {
    _fileContentModal.open({
        filePath: filePath,
    });
}
