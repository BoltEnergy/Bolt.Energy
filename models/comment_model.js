var mongoose = require('mongoose');
var Schema = mongoose.Schema;
var User = require('./user_model');

var CommentSchema = new Schema({
	title: String,
	body: String,
	created: { type: Date, default: Date.now },
	modified: Date,
	postedBy: { type: mongoose.Schema.Types.ObjectId, ref: 'User' },
	replies: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Comment' }],
	visible: Boolean
});

module.exports = mongoose.model('Comment', CommentSchema);