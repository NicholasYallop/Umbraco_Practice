function UpdateList() {
  $.ajax({
    type: "GET",
    url: '/umbraco/surface/Search/FilteredContentNodes?search=' + document.getElementById("SearchText").value,
    success: function (data) {
      console.log(data);
      $('#searchResults').html(data);
    }
  });
}