angular.module("umbraco").controller("membershipPromo", function ($scope, mediaResource) {
    mediaResource.getById($scope.block.data.image[0].mediaKey).then(media => $scope.image = media.mediaLink);
});
