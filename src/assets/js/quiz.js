// ===============================
// BANK SOAL — TIK (Teknologi Informasi dan Komunikasi)
// ===============================
const questionBank = [
  {
    question: "Apa kepanjangan dari TIK?",
    options: ["Teknologi Informasi dan Komunikasi", "Teknologi Internet dan Komputer", "Teknologi Informasi dan Komputer", "Teknologi Internet dan Komunikasi"],
    answer: "a"
  },
  {
    question: "Komponen utama komputer yang berfungsi sebagai otak adalah?",
    options: ["RAM", "CPU", "Hard Disk", "Motherboard"],
    answer: "b"
  },
  {
    question: "Apa fungsi dari RAM dalam komputer?",
    options: ["Menyimpan data secara permanen", "Memproses data sementara", "Menghubungkan perangkat", "Menyediakan daya listrik"],
    answer: "b"
  },
  {
    question: "Protokol jaringan yang paling umum digunakan di internet adalah?",
    options: ["FTP", "HTTP", "TCP/IP", "SMTP"],
    answer: "c"
  },
  {
    question: "Apa yang dimaksud dengan bandwidth?",
    options: ["Kecepatan transfer data", "Kapasitas penyimpanan", "Jumlah pengguna jaringan", "Ukuran file"],
    answer: "a"
  },
  {
    question: "Software yang digunakan untuk mengakses halaman web disebut?",
    options: ["Search Engine", "Web Browser", "Email Client", "FTP Client"],
    answer: "b"
  },
  {
    question: "Apa kepanjangan dari URL?",
    options: ["Uniform Resource Locator", "Universal Resource Link", "Uniform Resource Link", "Universal Resource Locator"],
    answer: "a"
  },
  {
    question: "Database yang menggunakan model relasional disebut?",
    options: ["NoSQL Database", "Relational Database", "Document Database", "Graph Database"],
    answer: "b"
  },
  {
    question: "Apa fungsi dari firewall dalam keamanan jaringan?",
    options: ["Mempercepat koneksi internet", "Mengontrol lalu lintas jaringan", "Menyimpan data backup", "Mengenkripsi data"],
    answer: "b"
  },
  {
    question: "Bahasa pemrograman yang digunakan untuk membuat halaman web interaktif adalah?",
    options: ["HTML", "CSS", "JavaScript", "SQL"],
    answer: "c"
  },
  {
    question: "Apa yang dimaksud dengan cloud computing?",
    options: ["Komputasi menggunakan awan", "Layanan komputasi melalui internet", "Komputasi offline", "Komputasi menggunakan server lokal"],
    answer: "b"
  },
  {
    question: "Format file gambar yang mendukung transparansi adalah?",
    options: ["JPG", "PNG", "BMP", "GIF"],
    answer: "b"
  },
  {
    question: "Apa kepanjangan dari HTML?",
    options: ["HyperText Markup Language", "HighText Markup Language", "HyperText Markdown Language", "HighText Markdown Language"],
    answer: "a"
  },
  {
    question: "Teknologi yang memungkinkan komunikasi tanpa kabel disebut?",
    options: ["Ethernet", "WiFi", "Bluetooth", "Semua benar"],
    answer: "d"
  },
  {
    question: "Apa fungsi dari antivirus?",
    options: ["Mempercepat komputer", "Melindungi dari malware", "Mengoptimalkan RAM", "Membersihkan hard disk"],
    answer: "b"
  },
  {
    question: "Sistem operasi open source yang populer adalah?",
    options: ["Windows", "macOS", "Linux", "iOS"],
    answer: "c"
  },
  {
    question: "Apa yang dimaksud dengan phishing?",
    options: ["Teknik memancing ikan", "Serangan cyber untuk mencuri informasi", "Metode enkripsi data", "Protokol jaringan"],
    answer: "b"
  },
  {
    question: "Unit terkecil dari data dalam komputer adalah?",
    options: ["Byte", "Bit", "Kilobyte", "Megabyte"],
    answer: "b"
  },
  {
    question: "Apa fungsi dari router dalam jaringan?",
    options: ["Menyimpan data", "Mengarahkan lalu lintas jaringan", "Mengenkripsi data", "Mempercepat koneksi"],
    answer: "b"
  },
  {
    question: "Format file yang digunakan untuk dokumen portabel adalah?",
    options: ["DOC", "TXT", "PDF", "RTF"],
    answer: "c"
  }
];

// Jumlah soal yang akan dipakai
const TOTAL_QUESTIONS = 5;

let quizData = [];
let currentIndex = 0;
let correctCount = 0;

// Shuffle helper
function shuffle(array) {
  for (let i = array.length - 1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i + 1));
    [array[i], array[j]] = [array[j], array[i]];
  }
  return array;
}

// Generate quiz random
function generateQuiz() {
  const shuffled = shuffle([...questionBank]);
  const selected = shuffled.slice(0, TOTAL_QUESTIONS);

  quizData = selected.map(item => {
    return {
      question: item.question,
      options: [
        "A. " + item.options[0],
        "B. " + item.options[1],
        "C. " + item.options[2],
        "D. " + item.options[3],
      ],
      answer: item.answer
    };
  });
}

generateQuiz();

// Tampilkan soal
function loadQuestion() {
  const q = quizData[currentIndex];
  const box = document.getElementById("question-box");

  box.innerHTML = `
    <p style="font-size:18px; margin-bottom:20px;">${currentIndex + 1}. ${q.question}</p>
    ${q.options
      .map((opt, idx) => {
        const letter = ["a", "b", "c", "d"][idx];
        return `
          <label class="option">
            <input type="radio" name="answer" value="${letter}">
            ${opt}
          </label>
        `;
      })
      .join("")}
  `;
}

loadQuestion();

// Next
function nextQuestion() {
  const selected = document.querySelector("input[name='answer']:checked");

  if (!selected) {
    alert("Pilih salah satu jawaban!");
    return;
  }

  if (selected.value === quizData[currentIndex].answer) {
    correctCount++;
  }

  currentIndex++;

  if (currentIndex < quizData.length) {
    loadQuestion();
  } else {
    finishQuiz();
  }
}

// Finish
function finishQuiz() {
  if (correctCount === quizData.length) {
    chrome.webview.postMessage("open_desktop");
  } else {
    chrome.webview.postMessage("wrong");
  }

  document.getElementById("question-box").innerHTML =
    `<h2>Quiz Selesai!</h2>
     <p>Nilai: ${correctCount} / ${quizData.length}</p>`;

  document.getElementById("next-btn").style.display = "none";
}
