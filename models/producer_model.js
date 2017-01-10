var mongoose = require('mongoose');
var Schema = mongoose.Schema;
var User = require('./user_model');
var Upload = require('./upload_model').ProducerUpload;
var Project = require('./project_model');
var Comment = require('./comment_model');

var ProducerSchema = new Schema({
	name: String,
	desc: String,
	status: String,
	availability: [String],
	//availability: String,
	type: String,
	energyType: String,
	uploads: [{
		type: mongoose.Schema.Types.ObjectId,
		ref: 'ProducerUpload'
	}],
	owner: {
		type: mongoose.Schema.Types.ObjectId,
		ref: 'User'
	},
	address1: String,
	address2: String,
	city: String,
	state: String,
	zip: String,
	approvalNumber: String,
	certifications: [{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'Certification'
	}],
	projects: [{ 
		type: mongoose.Schema.Types.ObjectId,
		ref: 'Project'
	}],
	updates: [{
		type: mongoose.Schema.Types.ObjectId,
		ref: 'Update'
	}],
	comments: [{
		type: mongoose.Schema.Types.ObjectId,
		ref: 'Comment'
	}],
	created: { type: Date, default: Date.now },
	modified: Date
});

ProducerSchema.pre('save', function (next) {
    var producer = this;
    User.findById(producer.owner, function(err, u) {
    	if (err) { return next(err); }
    	u.producer = producer._id;
    	u.save(function(err, us) {
    		if (err) { return next(err); }
    		next();
    	});
    });
});

module.exports = mongoose.model('Producer', ProducerSchema);