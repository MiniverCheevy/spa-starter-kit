const { BaseHrefWebpackPlugin } = require('base-href-webpack-plugin');
const bundleOutputDir = './wwwroot/dist';
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const CopyWebpackPlugin = require('copy-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const FriendlyErrorsPlugin = require('friendly-errors-webpack-plugin');
const merge = require('webpack-merge');
const path = require('path');
const webpack = require('webpack');


module.exports = (env) => {
    const extractCSS = new ExtractTextPlugin('app.css');
    const isDevBuild = !(env && env.prod);
    const virtualDirectory = isDevBuild ? '/' : '/audobon.inventory/';
    console.log('IsDevBuild=' + isDevBuild.toString());
    return [{
        stats: { modules: false },
        entry: {
            'main': [
                './ClientApp/boot.ts',
                './ClientApp/theme/site.css']
        },
        resolve: {
            extensions: ['.js', '.ts', '.vue', '.json'],
            alias: {
                'vue$': 'vue/dist/vue.esm.js'
            }
        },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: virtualDirectory + 'dist/'
        },
        module: {
            rules: [
                { test: /\.vue\.html$/, include: /ClientApp/, loader: 'vue-loader', options: { loaders: { js: 'awesome-typescript-loader?silent=true' } } },
                { test: /\.ts$/, include: /ClientApp/, use: 'awesome-typescript-loader?silent=true' },
                { test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader?minimize' }) },
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' }
            ]
        },
        resolve: { extensions: ['.js', '.ts', '.tsx'] },
        plugins: [
            new FriendlyErrorsPlugin(),
            new HtmlWebpackPlugin({
                filename: '../index.html',
                template: './ClientApp/index.html',
                inject: true
            }), new webpack.HotModuleReplacementPlugin(),
            new webpack.NoEmitOnErrorsPlugin(),
            extractCSS,
            new CheckerPlugin(),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            }),
            new CopyWebpackPlugin([
                //{ from: 'ClientApp/theme/fileIcons', to: '../fileIcons' },
                //{ from: 'ClientApp/index.html', to: '../' },
                { from: 'ClientApp/favicon.ico', to: '../' },
                //{ from: 'ClientApp/theme/site.css', to: '../' },
                //{ from: 'node_modules/filedrop/filedrop.js', to: './' }
            ])
        ].concat(isDevBuild ? [
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map',
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]')
            })
        ] : [
                new BaseHrefWebpackPlugin({
                    baseHref: virtualDirectory
                })
            ])
    }];
};
