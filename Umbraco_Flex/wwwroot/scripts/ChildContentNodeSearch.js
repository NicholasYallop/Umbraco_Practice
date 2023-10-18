function UpdateList() {
  $.ajax({
    type: "GET",
    url: '/umbraco/surface/Search/FilteredChildrenContentNodes?search=' + $('#SearchText').val() + '&groupId=' + $('#GroupId').val(),
    success: function (data) {
      console.log('/umbraco/surface/Search/FilteredChildrenContentNodes?search=' + $('#SearchText').val() + '&groupId=' + $('#GroupId').val());
      console.log(data);
      $('#searchResults').html(data);
    }
  });
}