var mongoose = require('mongoose');

var UploadModel = new mongoose.Schema({
	fileName: String,
	fileType: String,
	description: String,
	fileSize: Number
});

var userUpload = mongoose.model('UserUpload', UploadModel);
var producerUpload = mongoose.model('ProducerUpload', UploadModel);
var projectUpload = mongoose.model('ProjectUpload', UploadModel);

module.exports = {
	UserUpload: userUpload,
	ProducerUpload: producerUpload,
	ProjectUpload: projectUpload
};