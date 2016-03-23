/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    'use strict';

    var bowerPath = 'bower_components/';

    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-bowercopy');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        bowercopy: {
            options: {
                runBower: true,
                destPrefix: 'scripts/Vendors'
            },
            libs: {
                files: {
                    'angular': 'angular',
                    'jquery': 'jquery/dist',
                    'bootstrap': 'bootstrap/dist/css'
                }
            }
        },
        copy: {
            main: {
                files: [
                    {
                        expand: true, flatten: true,
                        src: [bowerPath + 'bootstrap/fonts/*'],
                        dest: 'Contents/fonts/', filter: 'isFile'
                    }
                ]
            }
        },
        uglify: {
            my_target: {
                files: {
                    'Scripts/app.js': ['Scripts/App/**/*.js'],
                    'Scripts/vendor.js': [bowerPath + 'jquery/dist/jquery.js',
                                          bowerPath + 'bootstrap/dist/js/bootstrap.js',
                                          bowerPath + 'angular/angular.js',
                                          bowerPath + 'ng-dialog/js/ngDialog.js']
                }
            }
        },
        cssmin: {
            concatenate: {
                files: {
                    'Content/vendors.min.css': ['scripts/Vendors/**/*.css',
                                                        bowerPath + 'ng-dialog/css/ngDialog.css',
                                                        bowerPath + 'ng-dialog/css/ngDialog-theme-default.css'],
                    'Content/styles.min.css': ['Content/Styles/**/*.css', '!content/Styles/**/*min.css']
                }
            }
        },
        watch: {
            js: {
                options: {
                    interrupt: true
                },
                files: ['Scripts/App/*/**.js'],
                tasks: ['uglify']
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
    grunt.registerTask('bowerDependencies', ['bowercopy:libs']);
    grunt.registerTask('default', ['bowerDependencies', 'minification']);

    grunt.loadNpmTasks('grunt-bowercopy');
    grunt.loadNpmTasks('grunt-karma');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-clean');
};