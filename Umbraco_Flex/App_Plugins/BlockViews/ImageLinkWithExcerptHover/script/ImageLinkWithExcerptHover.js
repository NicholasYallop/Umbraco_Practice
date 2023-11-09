angular.module("umbraco").controller("imageLinkWithExcerptHover", function ($scope, imageService, stringService) {

    $scope.vm = {
        title: "",
        centreTitle: false,
        twoColumnLayout: false,
        blocks: [],
        getColumnCss: function (key) {

            if ($scope.vm.twoColumnLayout === '1') {
                return "col-12 col-md-6";
            }

            var start = key + 1;
            var mod = start % 5;
            if (mod == 0 || mod == 4) {
                return "col-12 col-md-6";
            }
            else {
                return "col-12 col-md-4";
            }
        }
    };

    $scope.$watch("block.data.centreTitle", function (newValue) {
        if (!newValue) {
            return;
        }
        $scope.vm.centreTitle = newValue;
    });

    $scope.$watch("block.data.twoColumnLayout", function (newValue) {
        if (!newValue) {
            return;
        }
        $scope.vm.twoColumnLayout = newValue;
    });

    $scope.$watch("block.data.images.contentData", function (newValue, oldValue) {
        $scope.vm.blocks = [];

        if (typeof newValue === 'undefined') {
            return;
        }
        if (newValue.length === 0) {
            return;
        }

        // Populate blocks with data
        let blockList = [];
        newValue.forEach(function (content) {
            let block = {
                title: content.title,
                subtitle: content.subtitle,
                image: ""
            };
            imageService.getImage(content.image).then(image => block.image = image);
            blockList.push(block);
        });

        $scope.vm.blocks = blockList;
    }, true);

    $scope.$watch("block.data.title", function (newValue, oldValue) {
        if (newValue === null) {
            return;
        }
        $scope.vm.title = stringService.removeParagraphTags($scope.block.data.title);
    }, true);
});
