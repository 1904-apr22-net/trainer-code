const path = require('path');

module.exports = {
    entry: './out/main.js',
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: 'index.js'
    },
    mode: 'development',
    devtool: 'inline-source-map'
};
