var mongoose = require('mongoose');
var bcrypt = require('bcrypt');
var Producer = require('./producer_model');

var UserSchema = new mongoose.Schema({
    firstName: String,
    lastName: String,
    email: { type: String, unique: true, required: true },
    password: { type: String, required: true },
    accountType: {
        type: String,
        default: 'Consumer'
    },
    isAdmin: { type: Boolean, default: false },
    created: { type: Date, default: Date.now },
    modified: Date,
    uploads: [{
		type: mongoose.Schema.Types.ObjectId,
		ref: 'UserUpload'
	}],
	producer: { type: mongoose.Schema.Types.ObjectId, ref: 'Producer' }
});

UserSchema.pre('save', function (next) {
    var user = this;
    //Validation needed: if consumer should not have any projects
    if (this.isModified('password') || this.isNew) {
        bcrypt.genSalt(10, function (err, salt) {
            if (err) {
                return next(err);
            }
            bcrypt.hash(user.password, salt, function (err, hash) {
                if (err) {
                    return next(err);
                }
                user.password = hash;
                next();
            });
        });
    } else {
        return next();
    }
});

UserSchema.methods.comparePassword = function (passw, cb) {
    bcrypt.compare(passw, this.password, function (err, isMatch) {
        if (err) {
            return cb(err);
        }
        cb(null, isMatch);
    });
};


UserSchema.methods.getProducerId = function(cb) {
    var user = this;
    if (user.producer == undefined || user.producer == null || !user.producer) {
        Producer.findOne({ 'owner' : user._id }, function(err, p) {
            if (err) { cb(err, null); }
            user.producer = p._id;
            user.save();
            cb(null, p._id);
        });
    } else {
        cb(null, this.producer);
    }
};

module.exports = mongoose.model('User', UserSchema);