(function () {
    angular.module('umbraco.services').factory('imageService', ['mediaResource', 'imageUrlGeneratorResource', function (mediaResource, imageUrlGeneratorResource) {
        return {
            getImage: function (images, width = 380, height = 380) {
                if (!images) {
                    return "";
                }
                return mediaResource.getById(images[0].mediaKey)
                    .then(mediaItem => mediaItem.mediaLink).then(mediaLink => {
                        return imageUrlGeneratorResource.getCropUrl(mediaLink, width, height).then(cropUrl => {
                            return cropUrl;
                        });
                    });
            },
        };
    }]);
})();
