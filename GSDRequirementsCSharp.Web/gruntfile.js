
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    'use strict';

    var bowerPath = 'bower_components/';

    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-typescript');

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        typescript: {
            base: {
                src: ['Scripts/App/**/*.ts'],
                dest: 'Scripts/App/Interpreted'
            }
        },
        copy: {
            main: {
                files: [
                    {
                        expand: true, flatten: true,
                        src: [bowerPath + 'bootstrap/fonts/*',
                              bowerPath + 'components-font-awesome/fonts/*'],
                        dest: 'fonts/', filter: 'isFile'
                    },
                    {
                        expand: true, flatten: true,
                        src: ['Content/images/*'],
                        dest: 'images/', filter: 'isFile'
                    }
                ]
            }
        },
        uglify: {
            my_target: {
                files: {
                    'Scripts/app.js': ['Scripts/App/**/*.js'],
                    'Scripts/vendor.js': [bowerPath + 'jquery/jquery.js',
                                          bowerPath + 'bootstrap/dist/js/bootstrap.js',
                                          bowerPath + 'angular/angular.js',
                                          bowerPath + 'toastr/toastr.js',
                                          bowerPath + 'angular-mask/dist/ngMask.js',
                                          bowerPath + 'lodash/lodash.js',
                                          bowerPath + 'backbone/backbone.js',
                                          bowerPath + 'jointjs/dist/joint.js',
                                          bowerPath + 'angular-resource/angular-resource.js',
                                          bowerPath + 'angular-bootstrap/ui-bootstrap-tpls.js',
                                          bowerPath + 'ng-dialog/js/ngDialog.js',
                                          bowerPath + 'angular-ui-select/dist/select.js',
                                          bowerPath + 'angular-sanitize/angular-sanitize.js']
                }
            }
        },
        cssmin: {
            concatenate: {
                files: {
                    'Content/vendors.min.css': ['scripts/Vendors/**/*.css',
                                                bowerPath + 'bootstrap/dist/css/bootstrap.css',
                                                bowerPath + 'bootstrap/dist/css/bootstrap-theme.css',
                                                bowerPath + 'toastr/toastr.css',
                                                bowerPath + 'jointjs/dist/joint.css',
                                                bowerPath + 'components-font-awesome/css/font-awesome.css',
                                                bowerPath + 'ng-dialog/css/ngDialog.css',
                                                bowerPath + 'ng-dialog/css/ngDialog-theme-default.css',
                                                bowerPath + 'angular-ui-select/dist/select.css'],
                    'Content/styles.min.css': ['Content/Styles/**/*.css', '!content/Styles/**/*min.css']
                }
            }
        },
        watch: {
            js: {
                options: {
                    interrupt: true
                },
                files: ['Scripts/App/*/**.js', 'Scripts/App/*/**.ts'],
                tasks: ['default']
            },
            css: {
                options: {
                    interrupt: true
                },
                files: ['content/Styles/*/**.css'],
                tasks: ['cssmin']
            }
        }
    });

    grunt.registerTask('minification', ['uglify', 'cssmin', 'copy']);
    grunt.registerTask('default', ['typescript', 'minification']);

    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-clean');
};