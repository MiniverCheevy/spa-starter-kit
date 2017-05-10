const CopyWebpackPlugin = require('copy-webpack-plugin');
const path = require('path');
const webpack = require('webpack');
const { AureliaPlugin } = require('aurelia-webpack-plugin');
const bundleOutputDir = './wwwroot/dist';
module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [{
        stats: { modules: false },
        entry: { 'app': 'aurelia-bootstrapper' },
        resolve: {
            extensions: ['.js', '.ts'],
            modules: ['ClientApp', 'node_modules']
        },
        entry: {
            'app': [
                'webpack-hot-middleware/client?reload=true',
                'aurelia-bootstrapper']
        }, // Note: The aurelia-webpack-plugin will add your app's modules to this bundle automatically
        output: {
            path: path.resolve(bundleOutputDir),
            publicPath: '/dist/',
            filename: '[name].js'
        },
        module: {
            loaders: [
                { test: /\.ts$/i, include: /ClientApp/, use: 'ts-loader?silent=true' },
                { test: /\.html$/i, use: 'html-loader' },
                { test: /\.css$/, loader: 'raw-loader' },
                { test: /\.(png|woff|woff2|eot|ttf|svg|ico)$/, loader: 'url-loader?limit=100000' },
                { test: /\.json$/, loader: 'json-loader' }
            ]
        },
        plugins: [
            new webpack.DefinePlugin({ IS_DEV_BUILD: JSON.stringify(isDevBuild) }),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            }),
            new AureliaPlugin({ aureliaApp: 'boot' }),
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
