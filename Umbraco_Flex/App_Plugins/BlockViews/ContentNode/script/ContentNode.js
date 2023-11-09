angular.module("umbraco").controller("contentNode",
  ['$scope', '$timeout', 'mediaResource', 'imageUrlGeneratorResource',
    function ($scope, $timeout, mediaResource, imageUrlGeneratorResource) {
      $scope.vm = {
        loading: 1,
        error: 0,
        imageUrl: null,
        message: "this value is from the viewmodel.",
        init: function () {
          $scope.$watch(
            "vm",
            $scope.vm.load
          )
        },
        load: function (newValue, oldValue) {
          $scope.vm.error = 0;
          try {
            $scope.vm.loading = 1;

            var imageUdi = $scope.block.data.image[0].mediaKey || 0;
            if (imageUdi != 0) {
              mediaResource.getById(imageUdi).then(function (media) {
                imageUrlGeneratorResource.getCropUrl(media.mediaLink, 200, 150).then(function (cropUrl) {
                  if ($scope.vm.imageUrl != cropUrl) {
                    $scope.vm.imageUrl = cropUrl;
                    $scope.vm.loading = 0;
                  }
                })
              });
            }
          }
          catch (oh) {
            $scope.vm.error = 1;
          }
        }
      };

      $scope.vm.init();
    }
  ]
);
