// ===============================
// DATA SOAL
// ===============================
const quizData = [
  {
    question: "Apa huruf pertama dari kata 'Resmi'?",
    options: ["A. A", "B. R", "C. E", "D. I"],
    answer: "b"
  },
  {
    question: "Ibu kota Indonesia adalah?",
    options: ["A. Surabaya", "B. Bandung", "C. Jakarta", "D. Medan"],
    answer: "c"
  },
  {
    question: "2 + 5 × 2 = ?",
    options: ["A. 12", "B. 10", "C. 15", "D. 9"],
    answer: "a"
  }
];

let currentIndex = 0;
let correctCount = 0;


// ===============================
// TAMPILKAN SOAL
// ===============================
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


// ===============================
// NEXT / SUBMIT
// ===============================
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


// ===============================
// KIRIM HASIL KE WEBVIEW
// ===============================
function finishQuiz() {
  if (correctCount === quizData.length) {

    // Semua benar → kirim pesan ke aplikasi untuk membuka Desktop
    chrome.webview.postMessage("open_desktop");

  } else {

    // Jika ada yang salah → beri pesan salah
    chrome.webview.postMessage("wrong");
  }

  document.getElementById("question-box").innerHTML =
    `<h2>Quiz Selesai!</h2>
     <p>Nilai: ${correctCount} / ${quizData.length}</p>`;

  document.getElementById("next-btn").style.display = "none";
}
