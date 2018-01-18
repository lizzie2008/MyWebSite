/* global angular */

;(function () {
  'use strict'

  angular.module('localize', ['ngSanitize'])

    // SECURITY CONTEXT:
    // This simple filter only properly sanitizes values
    // that are rendered between HTML tags, e.g.
    // <div>ESCAPED_CONTENT</div>
    // It will fall short when used in any other context,
    // e.g. within attributes not enclosed by double quotes
    // or as value for event handlers or href attributes:
    // https://www.owasp.org/index.php/Cross-site_Scripting_(XSS)
    .filter('escapeHTML', function () {
      var config = {
        encReg: /[<>&"]/g,
        encMap: {
          '<': '&lt;',
          '>': '&gt;',
          '&': '&amp;',
          '"': '&quot;'
        },
        encFunc: function (c) {
          return config.encMap[c]
        }
      }
      return function (str) {
        return String(str).replace(
          config.encReg,
          config.encFunc
        )
      }
    })

    .factory('localizeConfig', ['$window', function ($window) {
      return {
        i18n: $window.i18n,
        // Only observe non-directive data-attributes:
        observableAttrs: /^data-(?!ng-|localize)/
      }
    }])

    .factory('localize', [
      '$filter', 'localizeConfig',
      function ($filter, localizeConfig) {
        var escapeHTML = $filter('escapeHTML')
        return function (key, data, escape) {
          var func = localizeConfig.i18n[key]
          var escapedData
          if (func) {
            if (!func.call) {
              // Translation is not a function, assume a static string
              return func
            }
            if (escape) {
              escapedData = {}
              angular.forEach(data, function (value, key) {
                escapedData[key] = escapeHTML(value)
              })
            }
            return func(escapedData || data || {})
          }
          return key
        }
      }
    ])

    .filter('localize', ['localize', function (localize) {
      return localize
    }])

    .directive('localize', [
      '$sanitize', '$filter', 'localizeConfig',
      function ($sanitize, $filter, localizeConfig) {
        var escapeHTML = $filter('escapeHTML')
        return function (scope, elm, attrs) {
          // Take the translation key from the element content
          // if the localize attribute is empty:
          var key = attrs.localize || elm.html()
          var func = localizeConfig.i18n[key]
          var isInput = /input|textarea/i.test(elm.prop('nodeName'))
          var staticString
          var data
          var update
          var hasObservers
          if (func) {
            if (!func.call) {
              // Translation is not a function, assume a static string
              staticString = func
              func = function () {
                return staticString
              }
            }
            if (isInput) {
              update = function () {
                attrs.$set('placeholder', func(attrs))
              }
            } else if (attrs.localize) {
              // Localization is text only
              update = function () {
                elm.text(func(attrs))
              }
            } else {
              // Localization can contain HTML
              data = {}
              update = function (key, value) {
                if (key) {
                  data[key] = escapeHTML(value)
                }
                elm.html($sanitize(func(data)))
              }
            }
            if (!staticString) {
              angular.forEach(attrs.$attr, function (attr, normAttr) {
                if (localizeConfig.observableAttrs.test(attr)) {
                  attrs.$observe(
                    normAttr,
                    isInput || attrs.localize
                      ? update
                      : function (value) { update(normAttr, value) }
                  )
                  hasObservers = true
                }
              })
            }
            if (!hasObservers) {
              update()
            }
          } else if (attrs.localize) {
            // If there is no translation function,
            // the key itself is the translation value:
            if (isInput) {
              attrs.$set('placeholder', key)
            } else {
              elm.text(key)
            }
          }
        }
      }
    ])

    .factory('localizeFactory', [
      'localizeConfig',
      function (localizeConfig) {
        return function () {
          var directiveObj = {
            link: function (scope, elm, attrs) {
              var name = directiveObj.name
              var target = name.charAt(8).toLowerCase() + name.slice(9)
              var key = attrs[name]
              var func = localizeConfig.i18n[key]
              var update
              var hasObservers
              if (func) {
                if (!func.call) {
                  // Translation is not a function, assume a static string
                  update = function () {
                    attrs.$set(target, func)
                  }
                } else {
                  update = function () {
                    attrs.$set(target, func(attrs))
                  }
                  angular.forEach(attrs.$attr, function (attr, normAttr) {
                    if (localizeConfig.observableAttrs.test(attr)) {
                      attrs.$observe(normAttr, update)
                      hasObservers = true
                    }
                  })
                }
                if (!hasObservers) {
                  update()
                }
              } else {
                attrs.$set(target, key)
              }
            }
          }
          return directiveObj
        }
      }
    ])
}())
