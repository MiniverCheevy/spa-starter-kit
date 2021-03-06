const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { BaseHrefWebpackPlugin } = require('base-href-webpack-plugin');
module.exports = (env) => {
    const extractCSS = new ExtractTextPlugin('vendor.css');
    const isDevBuild = !(env && env.prod);
    const virtualDirectory = isDevBuild ? '/' : '/';
    console.log('IsDevBuild=' + isDevBuild.toString());
    console.log('virtual directory =' + virtualDirectory);
    return [{
        stats: { modules: false },
        resolve: {
            extensions: ['.js']
        },
        module: {
            rules: [
                { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' },
                { test: /\.css(\?|$)/, use: extractCSS.extract(['css-loader']) }
            ]
        },
        entry: {
            //'jquery','bootstrap',
            vendor: [ 'babel-polyfill','event-source-polyfill', 'isomorphic-fetch',
                'react', 'react-dom', 'react-router',
                'mobx', 'mobx-react', 'mobx-react-devtools',                
                //'material-components-web/dist/material-components-web.js',
                'material-components-web/dist/material-components-web.css',
                'mdi/css/materialdesignicons.css',
                'sugar'
            ],
        },
        output: {
            path: path.join(__dirname, 'wwwroot', 'dist'),
            publicPath: virtualDirectory + 'dist/',
            filename: '[name].js',
            library: '[name]_[hash]',
        },
        plugins: [
            //new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
            // $: 'jquery', jQuery: 'jquery',
            extractCSS,
            new webpack.ProvidePlugin({ Promise: 'es6-promise-promise' }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)

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
