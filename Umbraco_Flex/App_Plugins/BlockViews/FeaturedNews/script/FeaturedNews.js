angular.module("umbraco").controller("featuredNews", function ($scope, contentService, imageService) {
    $scope.vm = {
        title: "",
        topNewsItems: [],
        bottomNewsItems: [],
        getCssClass: function (length) {
            if (length === 1) {
                return "col-12 post_flex";
            } else {
                return "col-12 col-lg-9 post_flex";
            }
        }
    };

    $scope.$watch("block.data.newsItems", function (newValue, oldValue) {
        $scope.vm.topNewsItems = [];
        $scope.vm.bottomNewsItems = [];

        if (typeof newValue === 'undefined') {
            return;
        }
        if (newValue.length === 0) {
            return;
        }
        // tree node pickers set values as a string of comma seperated udis
        let newsItemIds = newValue.split(",");
        contentService.getByIds(newsItemIds)
            .then(pages => {
                let list = [];
                pages.forEach(newsPage => {
                    let firstVariation = newsPage.variants.pop();

                    if (Array.isArray(firstVariation.tabs)) {
                        // Get tabs
                        var affiliateTab = firstVariation.tabs.find(variant => variant.alias === "affiliate");
                        var contentTab = firstVariation.tabs.find(variant => variant.alias === "content");

                        // Get properties
                        let headline = contentTab.properties.find(prop => prop.alias === "headline");
                        let date = contentTab.properties.find(prop => prop.alias === "announced");
                        let category = affiliateTab.properties.find(prop => prop.alias === "category");
                        let abstract = affiliateTab.properties.find(prop => prop.alias === "abstract");
                        let thumbnail = affiliateTab.properties.find(prop => prop.alias === "thumbnail");

                        // Create vm
                        let news = {
                            headline: headline.value,
                            date: new Date(/*date.value*/),
                            category: category.value,
                            abstract: abstract.value,
                            thumbnail: ""
                        };

                        // Get image
                        imageService.getImage(thumbnail.value).then(image => news.thumbnail = image);
                        list.push(news);
                    }
                });
                
                // Populate correct props on vm
                for (let i = 0; i < list.length; i++) {
                    let element = list.at(i);
                    if (i < 2) {
                        $scope.vm.topNewsItems.push(element);
                    }
                    else {
                        $scope.vm.bottomNewsItems.push(element);
                    }
                }
            });
    });

    $scope.$watch("block.data.title", function (newValue, oldValue) {
        if (newValue === null) {
            return;
        }
        $scope.vm.title = $scope.block.data.title;
    }, true);
});
