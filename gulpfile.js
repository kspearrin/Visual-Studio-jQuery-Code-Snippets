var p = require('./package.json')
    gulp = require('gulp'),
    xmlpoke = require('gulp-xmlpoke')
    msbuild = require('gulp-msbuild')
    xunit = require('gulp-xunit-runner')
    flatten = require('gulp-flatten')
    nuget = require('nuget-runner')({
        apiKey: process.env.NUGET_API_KEY,
        nugetPath: '.nuget/nuget.exe'
    });

gulp.task('default', ['test']);

gulp.task('manifest', [], function () {
    return gulp
        .src('jQueryCodeSnippets/source.extension.vsixmanifest')
        .pipe(xmlpoke({
            replacements : [{
                xpath : "//PackageManifest:Identity/@Version",
                namespaces : { "PackageManifest" : "http://schemas.microsoft.com/developer/vsx-schema/2011" },
                value: p.version
            }]
        }))
        .pipe(gulp.dest('jQueryCodeSnippets'));
});

gulp.task('restore', ['manifest'], function () {
    return nuget
        .restore({
            packages: 'jQueryCodeSnippets.sln', 
            verbosity: 'normal'
        });
});

gulp.task('build', ['restore'], function() {
    return gulp
        .src('jQueryCodeSnippets.sln')
        .pipe(msbuild({
            toolsVersion: 12.0,
            targets: ['Clean', 'Build'],
            errorOnFail: true,
            configuration: 'Release'
        }));
});

gulp.task('copy', ['build'], function(){
    return gulp
        .src('packages/xunit.runner.console.*/tools/*')
        .pipe(flatten())
        .pipe(gulp.dest('.xunit'));
});

gulp.task('test', ['copy'], function() {
    return gulp
        .src(['Tests/bin/Release/Tests.dll'], {read: false})
        .pipe(xunit({
            executable: '.xunit'
        }));
});
