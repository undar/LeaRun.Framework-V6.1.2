/**
 * Created by cbb on 16/6/5.
 */
angular.module('starter.directive', [])
  .directive('lrFocusOn',['$timeout','$parse','$learunTopAlert', function($timeout, $parse,$learunTopAlert) {
    return {
      restrict: 'A',
      //scope: true,   // optionally create a child scope
      link:function(scope, element, attrs) {
        var model = $parse(attrs.lrFocusOn);
        scope.$watch(model, function(value) {
          if(value === true) {
            $timeout(function() {
              element[0].focus();
            });
          }
        });
        // to address @blesh's comment, set attribute value to 'false'
        // on blur event:
        /*element.bind('blur', function() {
         console.log('blur');
         scope.$apply(model.assign(scope, false));
         });*/
      }
    };
  }])
  .directive('resizeFootBar', ['$ionicScrollDelegate', function($ionicScrollDelegate){
    // Runs during compile
    return {
      replace: false,
      link: function(scope, iElm, iAttrs, controller) {
        scope.$on("taResize", function(e, ta) {
            if (!ta) {
                $ionicScrollDelegate.$getByHandle('messageDetailsScroll').scrollBottom();;
                return;
            }
          var scroll = document.body.querySelector("#message-detail-content");
          var scrollBar = $ionicScrollDelegate.$getByHandle('messageDetailsScroll');
          var taHeight = ta[0].offsetHeight;
          var newFooterHeight = taHeight + 10;
          newFooterHeight = (newFooterHeight > 44) ? newFooterHeight : 44;

          iElm[0].style.height = newFooterHeight + 'px';
          scroll.style.bottom = newFooterHeight + 'px';
          scrollBar.scrollBottom();
        });
      }
    };
  }])
  .directive('lrIonRadio', function() {
    return {
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      transclude: true,
      template:
      '<label class="item item-radio">' +
      '<input type="radio" name="radio-group">' +
      '<div class="radio-content">' +
      '<div class="item-content disable-pointer-events" ng-transclude></div>' +
      '<i class="radio-icon disable-pointer-events icon ion-ios-circle-outline"></i>' +
      '<i class="radio-icon disable-pointer-events icon ion-ios-checkmark"></i>' +
      '</div>' +
      '</label>',

      compile: function(element, attr) {
        if (attr.icon) {
          var iconElm = element.find('i');
          iconElm.removeClass('ion-ios-checkmark').addClass(attr.icon);
        }
        var input = element.find('input');
        angular.forEach({
          'name': attr.name,
          'value': attr.value,
          'disabled': attr.disabled,
          'ng-value': attr.ngValue,
          'ng-model': attr.ngModel,
          'ng-disabled': attr.ngDisabled,
          'ng-change': attr.ngChange,
          'ng-required': attr.ngRequired,
          'required': attr.required
        }, function(value, name) {
          if (angular.isDefined(value)) {
            input.attr(name, value);
          }
        });

        return function(scope, element, attr) {
          scope.getValue = function() {
            return scope.ngValue || attr.value;
          };
        };
      }
    };
  });
