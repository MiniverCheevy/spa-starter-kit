var isDevBuild = process.argv.indexOf('--env.prod') < 0;
var BrowserSyncPlugin = require('browser-sync-webpack-plugin');
var path = require('path');
var webpack = require('webpack');
const { AureliaPlugin } = require("aurelia-webpack-plugin");
console.log("isDevBuild=" + isDevBuild);
var bundleOutputDir = './wwwroot/dist';
module.exports = {
    devtool: isDevBuild ? "#cheap-module-source-map" : null,
    resolve: {
        extensions: ['.js', '.ts'],
        modules: ['ClientApp','node_modules']
    },
    entry: {
        'app': [
            'webpack-hot-middleware/client?reload=true',
            'aurelia-bootstrapper']
    }, // Note: The aurelia-webpack-plugin will add your app's modules to this bundle automatically
    output: {
        path: path.resolve(bundleOutputDir),
        publicPath: '/dist',
        filename: '[name].js'
    },
    module: {
        loaders: [
            { test: /\.ts$/, include: /ClientApp/, loader: 'ts-loader', query: { silent: true } },
            { test: /\.html$/, loader: 'html-loader' },
            { test: /\.css$/, loaders: ['style-loader', 'css-loader'] },
            { test: /\.(png|woff|woff2|eot|ttf|svg)$/, loader: 'url-loader?limit=100000' },
            { test: /\.json$/, loader: 'json-loader' }
        ]
    },
    plugins: [
        new AureliaPlugin({
            //https://github.com/jods4/aurelia-webpack-build/wiki/AureliaPlugin%20options
            root: path.resolve('./'),
            includeAll: 'ClientApp',
            baseUrl: '/',
            aureliaApp: "boot",
            features: {
                ie:  true,
                svg: false,
                unparser:  true              
            }
        }),
        new webpack.ProvidePlugin({
            Promise: 'bluebird',
            $: 'jquery',
            jQuery: 'jquery'
        }),
        new webpack.DefinePlugin({ IS_DEV_BUILD: JSON.stringify(isDevBuild) }),
        new webpack.DllReferencePlugin({
            context: __dirname,
            manifest: require('./wwwroot/dist/vendor-manifest.json')
        }),

    ].concat(isDevBuild ? [
        //new BrowserSyncPlugin({
        //    host: 'localhost',
        //    port: 5005,
        //    server: { baseDir: ['wwwroot'] }
        //})
        // Plugins that apply in development builds only
        //new webpack.SourceMapDevToolPlugin({
        //    filename: '[file].map', // Remove this line if you prefer inline source maps
        //    moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
        //})
        //new webpack.optimize.UglifyJsPlugin({ sourceMap:true })
    ] : [
            // Plugins that apply in production builds only
            new webpack.optimize.UglifyJsPlugin()
        ])
};
