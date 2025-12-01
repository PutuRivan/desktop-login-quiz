// ===============================
// BANK SOAL — kota → negara
// ===============================
const questionBank = [
  { city: "Tokyo", country: "Jepang" },
  { city: "Seoul", country: "Korea Selatan" },
  { city: "Bangkok", country: "Thailand" },
  { city: "Beijing", country: "China" },
  { city: "New York", country: "Amerika Serikat" },
  { city: "Sydney", country: "Australia" },
  { city: "Paris", country: "Prancis" },
  { city: "Berlin", country: "Jerman" },
  { city: "Madrid", country: "Spanyol" },
  { city: "London", country: "Inggris" },
  { city: "Moscow", country: "Rusia" },
  { city: "Dubai", country: "Uni Emirat Arab" },
  { city: "Toronto", country: "Kanada" },
  { city: "New Delhi", country: "India" },
  { city: "Roma", country: "Italia" }
];

// Jumlah soal yang akan dipakai
const TOTAL_QUESTIONS = 10;

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
    const correct = item.country;

    let fakeOptions = questionBank
      .filter(x => x.country !== correct)
      .map(x => x.country);

    shuffle(fakeOptions);

    const options = [
      correct,
      fakeOptions[0],
      fakeOptions[1],
      fakeOptions[2]
    ];

    shuffle(options);

    const answerIndex = options.indexOf(correct);

    return {
      question: `Kota "${item.city}" berada di negara mana?`,
      options: [
        "A. " + options[0],
        "B. " + options[1],
        "C. " + options[2],
        "D. " + options[3],
      ],
      answer: ["a", "b", "c", "d"][answerIndex]
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
