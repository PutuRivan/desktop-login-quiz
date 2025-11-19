let pdfOpened = false; // Default PDF tertutup (module content ditampilkan)

// Default tampilan ketika halaman pertama kali dibuka
window.onload = () => {
    document.getElementById("pdfViewer").style.display = "none";
    document.getElementById("moduleContent").style.display = "block";

    document.getElementById("togglePdfBtn").textContent = "Buka PDF";

    // Siapkan PDF (dari C#) tapi jangan tampilkan
    chrome.webview.postMessage("open-pdf");
};

// Terima path PDF dari C#
chrome.webview.addEventListener("message", (event) => {
  const pdfPath = event.data;
  document.getElementById("pdfViewer").src = pdfPath;
});

// Tombol buka/tutup PDF
function togglePDF() {
    const viewer = document.getElementById("pdfViewer");
    const content = document.getElementById("moduleContent");
    const toggleBtn = document.getElementById("togglePdfBtn");

    if (pdfOpened) {
        // Tutup PDF → tampilkan module content
        viewer.style.display = "none";
        content.style.display = "block";
        toggleBtn.textContent = "Buka PDF";
    } else {
        // Buka PDF → sembunyikan module content
        content.style.display = "none";
        viewer.style.display = "block";
        toggleBtn.textContent = "Tutup PDF";
    }

    pdfOpened = !pdfOpened;
}

// Tombol kembali ke quiz
function finishModule() {
    chrome.webview.postMessage("finish");
}
