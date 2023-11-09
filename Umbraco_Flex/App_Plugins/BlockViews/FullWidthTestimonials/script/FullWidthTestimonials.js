angular.module("umbraco").controller("fullWidthTestimonials",
    function ($scope, mediaResource) {
        $scope.vm = {
            headerTitle: '',
            headerImage: '',
            blocks: []
        };

        $scope.$watch("block.data.items.contentData", function (newValue, _) {
            $scope.vm.blocks = [];
            if (typeof newValue === 'undefined') {
                return;
            }
            if (newValue.length === 0) {
                return;
            }

            let blocks = [];
            newValue.forEach(function (content) {
                let block = {
                    image: "",
                    text: content.text,
                    author: content.author
                }
                if (content.image.length > 0) {
                    mediaResource.getById(content.image[0].mediaKey).then(media => block.image = media.mediaLink);
                }

                blocks.push(block);
            });
            // only take one to show in CMS
            $scope.vm.blocks = [blocks.pop()];
        });

        $scope.$watch("block.data.headerTitle", function (newValue, _) {
            if (!newValue) {
                return;
            }
            $scope.vm.headerTitle = newValue;
        });

        $scope.$watch("block.data.headerImage", function (newValue, _) {
            if (newValue.length === 0) {
                $scope.vm.headerImage = "/img/t.jpg";
                return;
            }
            mediaResource.getById(newValue[0].mediaKey).then(media => $scope.vm.headerImage = media.mediaLink);
        });
    }
);
