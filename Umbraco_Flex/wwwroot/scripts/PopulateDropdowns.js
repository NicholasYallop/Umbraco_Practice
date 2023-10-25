function PopulateDropdowns(data) {
  let blogs = document.getElementById("blogs_dropdown");
  let news = document.getElementById("news_dropdown");
  let all = document.getElementById("all_dropdown");

  for (var i = 0; i < data.length; i++) {
    let a = document.createElement("a");
    a.href = data[i].url;
    a.innerHTML = data[i].contentName;
    console.log(a);

    all.appendChild(a);
    if (data[i].parent == "News") {
      news.appendChild(a.cloneNode(true));
    }
    else if (data[i].parent == "Blogs") {
      blogs.appendChild(a.cloneNode(true));
    }
  }
}

$(function () {
  $.ajax({
    type: "GET",
    url: '/umbraco/surface/Search/ContentMenuItemsJson',
    success: function (data) {
      PopulateDropdowns(data);
    }
  });
})
