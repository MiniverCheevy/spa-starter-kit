const CopyWebpackPlugin = require('copy-webpack-plugin');
const path = require('path');
const webpack = require('webpack');
const { AureliaPlugin } = require('aurelia-webpack-plugin');
const bundleOutputDir = './wwwroot/dist';
const RuntimeAnalyzerPlugin = require('webpack-runtime-analyzer');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [
        {
        stats: { modules: false },
        entry: { 'app': 'aurelia-bootstrapper' },
        resolve: {
            extensions: ['.js', '.ts'],
            modules: ['ClientApp', 'node_modules']
        },
        entry: { 'app': 'aurelia-bootstrapper' },
        output: {
            path: path.resolve(bundleOutputDir),
            publicPath: '/dist/',
            filename: '[name].js'
        },
        module: {
            loaders: [
                { test: /\.ts$/, include: /ClientApp/, use: 'ts-loader?silent=true' },
                { test: /\.html$/, use: 'html-loader' },
                { test: /\.css$/, loader: 'raw-loader' },
                { test: /\.(png|woff|woff2|eot|ttf|svg|ico)$/, loader: 'url-loader?limit=100000' },
                { test: /\.json$/, loader: 'json-loader' }
            ]
        },
        plugins: [
            //new RuntimeAnalyzerPlugin({
            //    mode: 'standalone',
            //    port: 0,
            //    open: false,
            //    watchModeOnly: false
            //}),
            new webpack.DefinePlugin({ IS_DEV_BUILD: JSON.stringify(isDevBuild) }),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            }),
                //https://github.com/jods4/aurelia-webpack-build/wiki/AureliaPlugin%20options

            new AureliaPlugin({
                aureliaApp: 'boot',
                features: {
                    ie: true,
                    svg: false,
                    unparser: true
                }
            }),
            new webpack.ProvidePlugin({
                Promise: 'bluebird',
                $: 'jquery',
                jQuery: 'jquery'
            }),
            new webpack.DefinePlugin({ IS_DEV_BUILD: JSON.stringify(isDevBuild) }),

            new CopyWebpackPlugin([
                { from: 'ClientApp/index.html', to: '../' },
                { from: 'ClientApp/favicon.ico', to: '../' },
                { from: 'ClientApp/assets', to: '../assets/' },
            ])

        ].concat(isDevBuild ? [
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]')  // Point sourcemap entries to the original file locations on disk
            })
        ] : [
                new webpack.optimize.UglifyJsPlugin()
            ])
    }];
}
