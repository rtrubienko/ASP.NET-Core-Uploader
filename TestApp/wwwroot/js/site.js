function updateUploadButton() {
    var fileInput = document.getElementById('fileInput');
    var uploadButton = document.getElementById('uploadButton');

    if (fileInput.files.length > 0) {
        uploadButton.removeAttribute('disabled');
    } else {
        uploadButton.setAttribute('disabled', 'disabled');
    }
}
(function () {
    document.addEventListener("DOMContentLoaded", function () {
        var fileDropContainer = document.getElementById("fileDropContainer");
        var fileInput = document.getElementById("fileInput");
        var uploadButton = document.getElementById("uploadButton");
        var fileListContainer = document.getElementById("fileList");
        var textDanger = document.getElementById("textDanger");

        if (!fileDropContainer) return

        fileDropContainer.addEventListener("dragenter", function (e) {
            e.preventDefault();
            fileDropContainer.classList.add("drag-over");
        });

        fileDropContainer.addEventListener("dragover", function (e) {
            e.preventDefault();
            fileDropContainer.classList.add("drag-over");
        });

        fileDropContainer.addEventListener("dragleave", function () {
            fileDropContainer.classList.remove("drag-over");
        });

        fileDropContainer.addEventListener("drop", function (e) {
            e.preventDefault();
            fileDropContainer.classList.remove("drag-over");

            var files = e.dataTransfer.files;
            if (files.length > 0) {
                fileInput.files = files;
                if (!!textDanger) {
                    textDanger.innerHTML = "";
                }
                updateUploadButton();
                displayFileList();
            }
        });

        fileInput.addEventListener("change", function () {
            if (!!textDanger) {
                textDanger.innerHTML = "";
            }
            updateUploadButton();
            displayFileList();
        });

        function updateUploadButton() {
            uploadButton.disabled = fileInput.files.length === 0;
        }

        function displayFileList() {
            fileListContainer.innerHTML = "";
            for (var i = 0; i < fileInput.files.length; i++) {
                var fileName = fileInput.files[i].name;
                var listItem = document.createElement("div");
                listItem.textContent = fileName;
                fileListContainer.appendChild(listItem);
            }
        }
    });
})();

