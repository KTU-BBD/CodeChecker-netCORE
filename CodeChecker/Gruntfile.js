﻿module.exports = function(grunt) {
    grunt.loadNpmTasks("grunt-contrib-uglify");
    grunt.loadNpmTasks("grunt-contrib-sass");
    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks("grunt-contrib-watch");
    grunt.loadNpmTasks("grunt-contrib-concat");

    grunt.initConfig({
        config: {
            APP_PATH: "src/assets/app",
            JS_DEPLOY_PATH: "wwwroot/static/js",
            CSS_DEPLOY_PATH: "wwwroot/static/css",
            FONT_DEPLOY_PATH: "wwwroot/static/fonts",
            VENDOR_PATH: "bower_components",
            NPM_VENDOR_PATH: "node_modules",
            SCSS_ASSET_PATH: "Sass"
        },
        concat: {
            jsDepepndenciesFront: {
                files: {
                    "<%= config.JS_DEPLOY_PATH  %>/plugins.front.js": [
                        "<%= config.VENDOR_PATH  %>/angular/angular.js",
                        "<%= config.VENDOR_PATH  %>/angular-route/angular-route.js"
                    ]
                }
            },
            cssDependenciesFront: {
                files: {
                    "<%= config.CSS_DEPLOY_PATH  %>/plugins.front.css": [
                        "<%= config.VENDOR_PATH  %>/bootstrap/dist/css/bootstrap.css",
                        "<%= config.VENDOR_PATH  %>/bootstrap/dist/css/bootstrap-theme.css",
                        "<%= config.VENDOR_PATH  %>/font-awesome/css/font-awesome.css"
                    ]
                }
            },
            cssDependenciesAdmin: {
                files: {
                    "<%= config.CSS_DEPLOY_PATH  %>/plugins.admin.css": [
                        "<%= config.NPM_VENDOR_PATH %>/ng-table/bundles/ng-table.css",
                        "<%= config.VENDOR_PATH  %>/simple-line-icons/css/simple-line-icons.css",
                        "<%= config.VENDOR_PATH  %>/font-awesome/css/font-awesome.css"
                    ]
                }
            },
            jsDepepndenciesAdmin: {
                files: {
                    "<%= config.JS_DEPLOY_PATH  %>/plugins.admin.js": [
                        "<%= config.VENDOR_PATH  %>/jquery/dist/jquery.js",
                        "<%= config.VENDOR_PATH  %>/angular/angular.js",
                        "<%= config.NPM_VENDOR_PATH %>/ng-table/bundles/ng-table.js",
                        "<%= config.VENDOR_PATH  %>/tether/dist/js/tether.js",
                        "<%= config.VENDOR_PATH  %>/ng-file-upload/ng-file-upload.js",
                        "<%= config.VENDOR_PATH  %>/bootstrap/dist/js/bootstrap.js",
                        "<%= config.VENDOR_PATH  %>/angular-resource/angular-resource.js",
                        "<%= config.VENDOR_PATH  %>/angular-ui-router/release/angular-ui-router.js",
                        "<%= config.VENDOR_PATH  %>/oclazyload/dist/ocLazyLoad.js",
                        "<%= config.VENDOR_PATH  %>/angular-breadcrumb/dist/angular-breadcrumb.js",
                        "<%= config.VENDOR_PATH  %>/angular-loading-bar/build/loading-bar.js"
                    ]
                }
            }
        },
        sass: {
                dist: {
                    options: {
                        style: "expanded"
                    },
                    files: {
                        "<%= config.CSS_DEPLOY_PATH %>/styles.front.css": "<%= config.SCSS_ASSET_PATH %>/Front/styles.scss",
                        "<%= config.CSS_DEPLOY_PATH %>/styles.back.css": "<%= config.SCSS_ASSET_PATH %>/Admin/style.scss"
                    }
                }
        },
        copy: {
            fa_fonts: {
                cwd: "<%= config.VENDOR_PATH  %>/font-awesome/fonts/",
                src: "**/*",
                dest: "<%= config.FONT_DEPLOY_PATH  %>",
                expand: true
            },
            bs_fonts: {
                cwd: "<%= config.VENDOR_PATH  %>/bootstrap/fonts/",
                src: "**/*",
                dest: "<%= config.FONT_DEPLOY_PATH  %>",
                expand: true
            },
            sl_fonts: {
                cwd: "<%= config.VENDOR_PATH  %>/simple-line-icons/fonts/",
                src: "**/*",
                dest: "<%= config.FONT_DEPLOY_PATH  %>",
                expand: true
            }
        },
        watch: {
            compileCss: { files: "<%= config.SCSS_ASSET_PATH %>/**/*.scss", tasks: ["sass"] }
        }
    });
    grunt.registerTask("init", ["concat", "copy", "sass"]);
    grunt.registerTask("default", ["watch:compileCss"]);
};