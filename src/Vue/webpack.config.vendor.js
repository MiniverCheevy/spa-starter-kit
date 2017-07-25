const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { BaseHrefWebpackPlugin } = require('base-href-webpack-plugin');
module.exports = (env) => {
    const extractCSS = new ExtractTextPlugin('vendor.css');
    const isDevBuild = !(env && env.prod);
    const virtualDirectory = isDevBuild ? '/' : '/audobon.inventory/';
    console.log('IsDevBuild=' + isDevBuild.toString());
    return [{
        stats: { modules: false },
        resolve: {
            extensions: [ '.js' ]
        },
        module: {
            rules: [
                { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' },
                { test: /\.css(\?|$)/, use: extractCSS.extract(['css-loader']) }
            ]
        },
        entry: {
            //'jquery','bootstrap',
            vendor: ['vue',
                'vue-router',
                'material-components-web/dist/material-components-web.js',
                'material-components-web/dist/material-components-web.css',
                'mdi/css/materialdesignicons.css',
                'isomorphic-fetch',
                'event-source-polyfill'],
        },
        output: {
            path: path.join(__dirname, 'wwwroot', 'dist'),
            publicPath: virtualDirectory+'dist/',
            filename: '[name].js',
            library: '[name]_[hash]',
        },
        plugins: [
		//new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
            new webpack.IgnorePlugin(/^\.\/html$/),
            extractCSS,
            //new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery' }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
            new webpack.DllPlugin({
                path: path.join(__dirname, 'wwwroot', 'dist', '[name]-manifest.json'),
                name: '[name]_[hash]'
            }),
            new webpack.DefinePlugin({
                'process.env.NODE_ENV': isDevBuild ? '"development"' : '"production"'
            })
        ].concat(isDevBuild ? [] : [
            new webpack.optimize.UglifyJsPlugin(),
            new BaseHrefWebpackPlugin({
                baseHref: virtualDirectory
            })
        ])
    }];
};
