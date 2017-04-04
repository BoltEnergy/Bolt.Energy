// Import dependencies
var passport = require('passport');
var express = require('express');
var config = require('../config/app_config');
var jwt = require('jsonwebtoken');
var mongoose = require('mongoose');
var Content = require('../models/content_model');
const requireAuth = passport.authenticate('jwt', { session: false });

module.exports = function (app) {
    var router = express.Router();

    router.get('/content/:vanityUrl', function (req, res) {
        try{
            if ((req.params.vanityUrl)) {
                Content.findOne({ 'pageVanityUrl': req.params.vanityUrl }, function (err, pageContent) {
                    if (err) {
                        res.status(400).json({ error: err });
                    } else {
                        res.json({ content: pageContent });
                    }
                });
            } else { res.status(401).json({ message: "Invalid page request." }); }
        }
        catch(e)
        {
            res.status(400).json({ err: e });
        }
    })

    // Set url for API group routes
    app.use('/data', router);
}