const fse = require('fs-extra');
const path = require('path')


// packages
fse.removeSync(path.join(__dirname, 'publish-packages/smartsoftware'))

fse.removeSync(path.join(__dirname, '../packs/node_modules'))
fse.copySync(path.join(__dirname, '../packs'), path.join(__dirname, 'publish-packages/smartsoftware/npm/packs'), {overwrite: true, errorOnExist: false, })

// app react-native template
fse.removeSync(path.join(__dirname, 'expo-app/app'))

fse.removeSync(path.join(__dirname, '../../templates/app/react-native/node_modules'));
fse.copySync(path.join(__dirname, '../../templates/app/react-native'), path.join(__dirname, 'expo-app/app'), {overwrite: true, errorOnExist: false, })
