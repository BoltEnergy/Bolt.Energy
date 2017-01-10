var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var UpdateSchema = new Schema({
	title: String,
	body: String,
	created: { type: Date, default: Date.now },
	modified: Date,
	posted: Date,
	visible: Boolean
});
module.exports = mongoose.model('Update', UpdateSchema);