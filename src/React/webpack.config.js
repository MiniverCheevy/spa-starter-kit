var CopyWebpackPlugin = require('copy-webpack-plugin');
const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const bundleOutputDir = './wwwroot/dist';
const { BaseHrefWebpackPlugin } = require('base-href-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = (env) => {
    const extractCSS = new ExtractTextPlugin('app.css');
    const isDevBuild = !(env && env.prod);
    const virtualDirectory = isDevBuild ? '/' : '/audobon.inventory/';
    console.log('IsDevBuild=' + isDevBuild.toString());
    return [{
        stats: { modules: false },
        entry: {
            'main': ['babel-polyfill', './ClientApp/boot.tsx',
                './ClientApp/theme/site.css']
        },
        resolve: { extensions: [ '.js', '.jsx', '.ts', '.tsx' ] },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: virtualDirectory+'dist/'
        },
        module: {
            rules: [
                { test: /\.ts(x?)$/, include: /ClientApp/, use: 'babel-loader' },
                { test: /\.tsx?$/, include: /ClientApp/, use: 'awesome-typescript-loader?silent=true' },
                { test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader' }) },
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' }
            ]
        },
        plugins: [
            extractCSS,
            new CheckerPlugin(),

            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            }),
        new CopyWebpackPlugin([
            { from: 'ClientApp/index.html', to: '../' },
            { from: 'ClientApp/favicon.ico', to: '../' },
        ])
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
        ] : [
            // Plugins that apply in production builds only
            //new webpack.optimize.UglifyJsPlugin(),
                new BaseHrefWebpackPlugin({
                    baseHref: virtualDirectory
                })
        ])
    }];
};
