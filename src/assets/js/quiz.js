function submitAnswer() {
  const ans = document.getElementById("answer").value.trim().toLowerCase();

  if (ans === "r") {
    chrome.webview.postMessage("correct");
  } else {
    chrome.webview.postMessage("wrong");
  }
}
