function handleAnswer(answer) {
  chrome.webview.postMessage(answer);
}

