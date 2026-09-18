window.renderPdfAsFlipbook = async function(pdfUrl, canvasContainerId) {
    const container = document.getElementById(canvasContainerId);
    if (!container) return;

    // Estructura interna con la barra de herramientas integrada
    container.innerHTML = `
        <div class="flipbook-toolbar">
            <button id="btn-fullscreen_${canvasContainerId}" class="btn-tool" title="Pantalla completa">
                🖵 Pantalla Completa
            </button>
            <a href="${pdfUrl}" download class="btn-tool primary" title="Descargar PDF">
                📥 Descargar PDF
            </a>
        </div>
        <div id="book-wrapper_${canvasContainerId}" style="display: flex; justify-content: center; align-items: center; width: 100%;">
            <div id="book_${canvasContainerId}"></div>
        </div>
    `;

    const wrapperElement = document.getElementById(`book-wrapper_${canvasContainerId}`);
    const bookElement = document.getElementById(`book_${canvasContainerId}`);
    const btnFullscreen = document.getElementById(`btn-fullscreen_${canvasContainerId}`);

    // Manejo de Pantalla Completa
    btnFullscreen.addEventListener('click', () => {
        if (!document.fullscreenElement) {
            wrapperElement.requestFullscreen().catch(err => {
                console.error(`Error al intentar modo pantalla completa: ${err.message}`);
            });
        } else {
            document.exitFullscreen();
        }
    });

    // Configurar el worker de PDF.js
    pdfjsLib.GlobalWorkerOptions.workerSrc = 'https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.16.105/pdf.worker.min.js';

    try {
        const loadingTask = pdfjsLib.getDocument(pdfUrl);
        const pdf = await loadingTask.promise;

        for (let pageNum = 1; pageNum <= pdf.numPages; pageNum++) {
            const page = await pdf.getPage(pageNum);
            const viewport = page.getViewport({ scale: 1.5 });

            const canvas = document.createElement('canvas');
            const context = canvas.getContext('2d');
            canvas.height = viewport.height;
            canvas.width = viewport.width;

            await page.render({ canvasContext: context, viewport: viewport }).promise;

            const pageDiv = document.createElement('div');
            pageDiv.className = 'page';
            pageDiv.appendChild(canvas);
            bookElement.appendChild(pageDiv);
        }

        // Inicializar StPageFlip
        const pageFlip = new St.PageFlip(bookElement, {
            width: 550,
            height: 733,
            showCover: true,
            maxShadowOpacity: 0.5,
            mobileScrollSupport: true
        });

        pageFlip.loadFromHTML(bookElement.querySelectorAll('.page'));
    } catch (error) {
        console.error("Error al cargar el PDF en el flipbook:", error);
        container.innerHTML = '<p style="color: var(--red); font-size: 0.875rem; text-align: center; padding: 2rem;">No se pudo cargar el documento interactivo.</p>';
    }
};