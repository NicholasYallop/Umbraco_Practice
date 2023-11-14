(function() {
    angular.module('umbraco.services').factory('contentService', ['entityResource', 'contentResource', function(entityResource, contentResource) {
        return {
            getByIds: function(listOfUdis, contentType = "document") {
                return entityResource.getByIds(listOfUdis, contentType)
                    .then(entities => {
                        let ids = [];
                        entities.forEach(c => ids.push(c.id));
                        return ids;
                    })
                    .then(function(ids){
                        return contentResource.getByIds(ids).then(function(pages) { 
                            return pages;
                        });
                    });
            },
        };
    }]);
})();
