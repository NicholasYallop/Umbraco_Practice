function getFilteredContent(id) {
  var xmlhttp = new XMLHttpRequest();
  xmlhttp.reponseType = "json";
  xmlhttp.body = JSON.stringify({ PageId: parseInt(id), SearchText: SearchText.value });

  xmlhttp.onreadystatechange = function () {
    ProcessResponse(xmlhttp.response);
  }

  xmlhttp.open("GET",
    "/umbraco/surface/Children/GetChildrenAsJson?"
    + "id=" + parseInt(id)
    + "&crit=" + encodeURIComponent(SearchText.value)
    , true);
  xmlhttp.send();
}

function ProcessResponse(response) {
  console.log("response recieved");
  console.log("response: " + response);

  var result = JSON.parse(response);

  var ul = document.getElementById("ResultsDisplay");
  while (ul.firstChild) {
    ul.removeChild(ul.firstChild)
  }

  for (var i = result.length-1; i >= 0 ; i--) {
    let child = result[i];

    let div = document.createElement("div");
    div.classList.add('resultelement');

    let img = new Image();
    img.src = child.imgUrl;
    div.appendChild(img);

    let textdiv = document.createElement("div");

    let h = document.createElement("h5");
    h.innerText = child.title;
    textdiv.appendChild(h);
    div.appendChild(textdiv);

    div.addEventListener('click', function () {
      location.href = child.url;
    });

    ul.appendChild(div);
  }
}