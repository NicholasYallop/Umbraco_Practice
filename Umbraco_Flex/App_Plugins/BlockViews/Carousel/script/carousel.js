angular.module("umbraco").controller("carousel",
    ['$scope', '$timeout', 'mediaResource', 'imageUrlGeneratorResource',
        function ($scope, $timeout, mediaResource, imageUrlGeneratorResource) {
            $scope.vm = {
                identifier: $scope.$id + (new Date().getTime()),
                loading: 0,
                error: 0,
                slides: [],
                init: function () {
                    $scope.$watch(
                        function (){
                            var key = '';
                            if ($scope.block.data.itemList.contentData !== undefined 
                                && $scope.block.data.itemList.contentData.length != 0) 
                            {
                                $scope.block.data.itemList.contentData.forEach(
                                    function (data, _) {
                                        var mediaKey = '';
                                        var action = '';
                                        try {
                                            mediaKey = data.image !== undefined
                                                && data.image.length != 0 
                                                ? data.image[0].mediaKey 
                                                : '';
                                            action = data.action !== undefined 
                                                && data.action.length != 0 
                                                ? data.action[0].url + data.action[0].name 
                                                : '';
                                        }
                                        catch (oh) {
                                        }
                                        key = key + mediaKey + data.title 
                                            + data.subtitle + action;
                                    });
                            }
                            return key;
                        },
                        $scope.vm.load
                    );
                },
                load: function () {
                    $scope.vm.error = 0;
                    try {
                        if ($scope.vm.loading == 0 
                            && $scope.block.data.itemList.contentData !== undefined 
                            && $scope.block.data.itemList.contentData.length != 0) 
                        {
                            $scope.vm.slides = [];
                            $scope.block.data.itemList.contentData.forEach(
                                function (data, index){
                                    $scope.vm.slides[index] = {
                                        image: '',
                                        title: data.title,
                                        subtitle: data.subtitle,
                                        action: data.action !== undefined 
                                        && data.action.length != 0 
                                        ?{
                                            url: data.action[0].url,
                                            text: data.action[0].name
                                        } : null
                                    } 
                                    var image = data.image[0].mediaKey || 0;
                                    if (image != 0) {
                                        $scope.vm.loading++;
                                        mediaResource.getById(image).then(
                                            function (media){
                                                if (media == null 
                                                    || media.mediaLink == null){
                                                    $scope.vm.error = 1;
                                                } else{
                                                    imageUrlGeneratorResource
                                                        .getCropUrl(media.mediaLink, 1920, 620)
                                                        .then(function (cropUrl) {
                                                            $scope.vm.slides[index].image = cropUrl;
                                                            $scope.vm.loading--;
                                                        });
                                                }
                                            });
                                    }
                                })
                        }
                    } catch (oh) {
                        $scope.vm.error = 1;
                        $scope.vm.loading = 0;
                    }
                }
            };

            $timeout($scope.vm.init, 1);
        }
    ]
);
