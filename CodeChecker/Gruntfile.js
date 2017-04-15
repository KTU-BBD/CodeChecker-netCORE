module.exports = function(grunt) {
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
            SCSS_ASSET_PATH: "Sass",
        },
        concat: {
            jsDepepndencies: {
                files: {
                    "<%= config.JS_DEPLOY_PATH  %>/plugins.js": [
                        "<%= config.VENDOR_PATH  %>/angular/angular.js",
                        "<%= config.VENDOR_PATH  %>/angular-ui-router/release/angular-ui-router.js"
                    ]
                }
            },
            cssDependencies: {
                files: {
                    "<%= config.CSS_DEPLOY_PATH  %>/plugins.css": [
                        "<%= config.VENDOR_PATH  %>/bootstrap/dist/css/bootstrap.css",
                        "<%= config.VENDOR_PATH  %>/bootstrap/dist/css/bootstrap-theme.css",
                        "<%= config.VENDOR_PATH  %>/font-awesome/css/font-awesome.css"
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
                    "<%= config.CSS_DEPLOY_PATH %>/styles.css": "<%= config.SCSS_ASSET_PATH %>/styles.scss"
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
            }
        },
        watch: {
            compileCss: { files: "<%= config.SCSS_ASSET_PATH %>/**/*.scss", tasks: ["sass"] }
        }
    });
    grunt.registerTask("init", ["concat", "copy", "sass"]);
    grunt.registerTask("default", ["watch:compileCss"]);
};