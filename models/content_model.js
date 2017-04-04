var mongoose = require('mongoose');

var ContentSchema = new mongoose.Schema({
    pageTitle: String,
    pageVanityUrl: { type: String, unique: true, required: true },
    pageContent: String,
    created: { type: Date, default: Date.now },
    modified: Date
});

module.exports = mongoose.model('Content', ContentSchema);