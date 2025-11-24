function handleBtnLogin(role) {
  chrome.webview.postMessage(role);
}
