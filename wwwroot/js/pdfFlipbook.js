window.renderPdfAsFlipbook = async function (pdfUrl, containerId) {

    var container = document.getElementById(containerId);

    if (!container) {
        console.error('Contenedor no encontrado: ' + containerId);
        return;
    }

    container.innerHTML = '';

    var pdfjsLib = window.pdfjsLib;

    if (!pdfjsLib) {
        container.innerHTML = '<p style="color:red;padding:1rem;">Error: pdf.js no esta cargado.</p>';
        return;
    }

    pdfjsLib.GlobalWorkerOptions.workerSrc =
        'https://cdnjs.cloudflare.com/ajax/libs/pdf.js/3.11.174/pdf.worker.min.js';

    pdfjsLib.getDocument(pdfUrl).promise.then(function (pdf) {

        var totalPages = pdf.numPages;
        var viewer = document.createElement('div');
        viewer.style.overflowY = 'auto';
        viewer.style.maxHeight = '75vh';
        viewer.style.width = '100%';
        viewer.style.display = 'flex';
        viewer.style.flexDirection = 'column';
        viewer.style.alignItems = 'center';
        viewer.style.gap = '8px';

        var pagePromises = [];
        for (var i = 1; i <= totalPages; i++) {
            pagePromises.push(pdf.getPage(i));
        }

        return Promise.all(pagePromises).then(function (pages) {
            pages.forEach(function (page) {
                var viewport = page.getViewport({ scale: 1.5 });

                var canvas = document.createElement('canvas');
                canvas.width = viewport.width;
                canvas.height = viewport.height;
                canvas.style.display = 'block';
                canvas.style.maxWidth = '100%';
                canvas.style.boxShadow = '0 2px 8px rgba(0,0,0,0.2)';

                var renderContext = {
                    canvasContext: canvas.getContext('2d'),
                    viewport: viewport
                };

                page.render(renderContext);
                viewer.appendChild(canvas);
            });

            container.appendChild(viewer);
        });

    }).catch(function (error) {
        console.error('Error al cargar el PDF:', error);
        container.innerHTML = '<p style="color:red;padding:1rem;">Error al cargar el PDF: ' + error.message + '</p>';
    });
};