cd ./src/Aurelia.Web
call node ./node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js
call node ./node_modules/webpack/bin/webpack.js
cd..
cd..