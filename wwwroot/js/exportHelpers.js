window.exportHelpers = {
    // 1. Exportar como PNG (Forzando vista desktop)
    downloadAsImage: async function (elementId, fileName) {
        const element = document.getElementById(elementId);
        if (!element) return;

        // Ocultar botones de exportación durante la captura
        const exportButtons = element.querySelector('.export-buttons-group');
        if (exportButtons) exportButtons.style.display = 'none';

        // Clonar el nodo para renderizarlo fuera de pantalla con resolución Desktop
        const clone = element.cloneNode(true);
        clone.style.width = '1200px';
        clone.style.position = 'absolute';
        clone.style.top = '-9999px';
        clone.style.left = '-9999px';
        document.body.appendChild(clone);

        try {
            const canvas = await html2canvas(clone, {
                scale: 2, // Alta resolución (Retina)
                windowWidth: 1200,
                useCORS: true
            });

            const link = document.createElement('a');
            link.download = `${fileName}.png`;
            link.href = canvas.toDataURL('image/png');
            link.click();
        } finally {
            // Limpiar el clon del DOM y restaurar botones
            document.body.removeChild(clone);
            if (exportButtons) exportButtons.style.display = '';
        }
    },

    // 2. Exportar como PDF (Forzando vista desktop en Horizontal)
    downloadAsPdf: async function (elementId, fileName) {
        const element = document.getElementById(elementId);
        if (!element) return;

        const exportButtons = element.querySelector('.export-buttons-group');
        if (exportButtons) exportButtons.style.display = 'none';

        const clone = element.cloneNode(true);
        clone.style.width = '1200px';
        clone.style.position = 'absolute';
        clone.style.top = '-9999px';
        clone.style.left = '-9999px';
        document.body.appendChild(clone);

        try {
            const canvas = await html2canvas(clone, {
                scale: 2,
                windowWidth: 1200,
                useCORS: true
            });

            const imgData = canvas.toDataURL('image/png');
            const { jsPDF } = window.jspdf;
            const pdf = new jsPDF('landscape', 'pt', 'a4');

            const pdfWidth = pdf.internal.pageSize.getWidth();
            const pdfHeight = (canvas.height * pdfWidth) / canvas.width;

            pdf.addImage(imgData, 'PNG', 0, 20, pdfWidth, pdfHeight);
            pdf.save(`${fileName}.pdf`);
        } finally {
            document.body.removeChild(clone);
            if (exportButtons) exportButtons.style.display = '';
        }
    },

    // 3. Exportar a Excel
    downloadAsExcel: function (jsonData, fileName) {
        const worksheet = XLSX.utils.json_to_sheet(jsonData);
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, "Horario");
        XLSX.writeFile(workbook, `${fileName}.xlsx`);
    }
};