const bundleOutputDir = './wwwroot/dist';

const path = require('path');
const webpack = require('webpack');
const extractTextPlugin = require('extract-text-webpack-plugin');
const checkerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const copyWebpackPlugin = require('copy-webpack-plugin');
const { BaseHrefWebpackPlugin } = require('base-href-webpack-plugin');
const htmlWebpackPlugin = require('html-webpack-plugin');
const friendlyErrorsPlugin = require('friendly-errors-webpack-plugin');
const merge = require('webpack-merge');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    const virtualDirectory = isDevBuild ? '/' : '/';
    console.log('IsDevBuild=' + isDevBuild.toString());
    return [{
        stats: { modules: false },
        entry: {
            'main': ['babel-polyfill', './ClientApp/boot.tsx',
                './ClientApp/theme/site.css']
        },
        resolve: { extensions: ['.js', '.jsx', '.ts', '.tsx'] },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: virtualDirectory + 'dist/'
        },
        module: {
            rules: [
                { test: /\.ts(x?)$/, include: /ClientApp/, use: 'babel-loader' },
                { test: /\.tsx?$/, include: /ClientApp/, use: 'awesome-typescript-loader?silent=true' },
                { test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : extractTextPlugin.extract({ use: 'css-loader?minimize' }) },
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' }
            ]
        },
        plugins: [
            new friendlyErrorsPlugin(),
            new htmlWebpackPlugin({
                filename: '../index.html',
                template: './ClientApp/index.html',
                inject: true
            }),

            new checkerPlugin(),

            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            }),
            new copyWebpackPlugin([
                { from: 'ClientApp/favicon.ico', to: '../' },
                { from: 'ClientApp/theme/site.css', to: '.' }
            ])
        ].concat(isDevBuild ? [
            new extractTextPlugin('site.css'),
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map',
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]')
            })
        ] : [
            new extractTextPlugin('site.css'),
                new BaseHrefWebpackPlugin({
                    baseHref: virtualDirectory
                })
            ])
    }];
};
