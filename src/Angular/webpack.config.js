const { BaseHrefWebpackPlugin } = require('base-href-webpack-plugin');
const bundleOutputDir = './wwwroot/dist';
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const CopyWebpackPlugin = require('copy-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
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
            'main': ['./ClientApp/boot.ts',
                './ClientApp/theme/site.css']
        },
          resolve: { extensions: [ '.js', '.ts'] },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: virtualDirectory + 'dist/'
        },
        module: {
            rules: [
                { test: /\.ts$/, include: /ClientApp/, use: ['awesome-typescript-loader?silent=true', 'angular2-template-loader'] },
                { test: /\.html$/, use: 'html-loader?minimize=false' },
                {
                    test: /\.(css)$/,
                    exclude: [/node_modules/, /ClientApp\/app/], 
                    use: [
                        'to-string-loader',
                        'raw-loader',
                        'style-loader',
                        'css-loader',
                    ]
                },
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
