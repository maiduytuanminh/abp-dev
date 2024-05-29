(function () {
    var l = ss.localization.getResource("CmsKit");

    ss.widgets.CmsRating = function ($widget) {
        var widgetManager = $widget.data("ss-widget-manager");
        var $ratingArea = $widget.find(".cms-rating-area");

        function getFilters() {
            return {
                entityType: $ratingArea.attr("data-entity-type"),
                entityId: $ratingArea.attr("data-entity-id")
            };
        }

        function registerCreateOfNewRating() {
            $widget.find(".my-rating").each(function () {
                    var authenticated = $(this).attr("data-authenticated");
                    var readonly = $(this).attr("data-readonly");

                    $(this).starRating({
                        initialRating: 0,
                        starSize: 16,
                        emptyColor: '#eee',
                        hoverColor: '#ffc107',
                        activeColor: '#ffc107',
                        useGradient: false,
                        strokeWidth: 0,
                        disableAfterRate: true,
                        useFullStars: true,
                        readOnly: authenticated === "True" || readonly === "True",
                        onHover: function (currentIndex, currentRating, $el) {
                            $widget.find(".live-rating").text(currentIndex);
                        },
                        onLeave: function (currentIndex, currentRating, $el) {
                            $widget.find(".live-rating").text(currentRating);
                        },
                        callback: function (currentRating, $el) {
                            smartsoftware.cmsKit.public.ratings.ratingPublic.create(
                                $ratingArea.attr("data-entity-type"),
                                $ratingArea.attr("data-entity-id"),
                                {
                                    starCount: parseInt(currentRating)
                                }
                            ).then(function () {
                                widgetManager.refresh($widget);
                            })
                        }
                    });
                }
            );
        }

        function registerUndoLink() {
            $widget.find(".rating-undo-link").each(function () {
                $(this).on('click', '', function (e) {
                    e.preventDefault();

                    ss.message.confirm(l("RatingUndoMessage"), function (ok) {
                        if (ok) {
                            smartsoftware.cmsKit.public.ratings.ratingPublic.delete(
                                $ratingArea.attr("data-entity-type"),
                                $ratingArea.attr("data-entity-id")
                            ).then(function () {
                                widgetManager.refresh($widget);
                            });
                        }
                    })
                });
            });
        }

        function init() {
            registerCreateOfNewRating();
            registerUndoLink();
        }

        return {
            init: init,
            getFilters: getFilters
        }
    };
})
(jQuery);
