function PopulateDropdowns(data) {
  let blogs = document.getElementById("blogs_dropdown");
  let news = document.getElementById("news_dropdown");
  let all = document.getElementById("all_dropdown");

  for (var i = 0; i < data.length; i++) {
    let li = document.createElement("li");
    let a = document.createElement("a");
    a.href = data[i].url;
    a.innerHTML = data[i].contentName;
    a.classList.add('dropdown-item');
    li.appendChild(a);

    all.appendChild(li);
    if (data[i].parent == "News") {
      news.appendChild(li.cloneNode(true));
    }
    else if (data[i].parent == "Blogs") {
      blogs.appendChild(li.cloneNode(true));
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
