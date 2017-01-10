var mongoose = require('mongoose');
var Schema = mongoose.Schema;
var Producer = require('../models/producer_model');

var ProjectSchema = new Schema({
	name: {
		type:String
	},
	desc: {
		type:String
	},
	status: String,
	availability: [{
		type:String
	}],
	projectType: {
		type: String
	},
	uploads: [{
		type: mongoose.Schema.Types.ObjectId,
		ref: 'ProjectUpload'
	}],
	energyMix: String,
	address1: String,
	address2: String,
	city: String,
	state: String,
	zip: String,
	programCategory: String,
	capacity: String,
	utilityDistricts: [{
		type: String,
		enum: ['Berlin','BGE','Choptank','DPL','Easton','Hagerstown','PE','Pepco','SMECO','Thurmont','Williamsport' ]
	}],
	projectOwner: {
		type: mongoose.Schema.Types.ObjectId,
		ref: 'Producer'
	},
	featured: { type: Boolean, default: false }
});

module.exports = mongoose.model('Project', ProjectSchema);