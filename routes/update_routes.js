// Import dependencies
var passport = require('passport');
var express = require('express');
var config = require('../config/app_config');
var jwt = require('jsonwebtoken');
var mongoose = require('mongoose');
var objectId = mongoose.Types.ObjectId;
var User = require('../models/user_model');
var Producer = require('../models/producer_model');
var Update = require('../models/update_model');

const requireAuth = passport.authenticate('jwt', { session: false });

module.exports = function(app) {
  var router = express.Router();
  router.get('/update/:id', function(req, res) {
		try {
			if (objectId.isValid(req.params.id)) {
				var search = Update.findById(req.params.id);
				search.exec(function(err, up) {
					if (err) {
						res.status(400).json({error: err});
					} else {
						res.json({ update: up });
					}
				});
			} else { res.status(401).json({ message: "Invalid object id." }); }
		} catch (e) {
			res.json({ err: e });
		}
	});
	router.post('/producer/:producerId/update', requireAuth, function(req, res) {
		try {
  		var p = new Update(req.body);
  		p.save(function(err,p) {
  			if (err) {
  				res.status(500).json({ error: JSON.stringify(err) });
  			} else {
  			  Producer.findById(req.params.producerId, function(err, pro) {
  			    if (err) res.status(500).json('Error saving update.');
  			    pro.updates.push(p._id);
  			    pro.save(function(err, producer) {
  			      if (err) res.status(500).json('Error saving update.');
  			      res.status(200).json({ update: p, message: "Save successful"});
  			    })
  			  })
  			}
  		})
		} catch(e) { res.status(500).json('Error saving update.'); }
	})
	router.put('/producer/:producerId/update/:updateId', requireAuth, function(req, res) {
	  try {
	    Update.findById(req.params.updateId, function(err, up) {
	      if (err) res.status(500).json('Error updating update.');
	      up.title = req.body.title;
	      up.body = req.body.body;
	      up.modified = new Date();
	      up.visible = req.body.visible;
	      up.save(function(e, upd) {
	        if (e) res.status(500).json('Error updating update.');
	        res.status(200).json({ update: upd });
	      })
	    })
	  } catch (e) { res.status(500).json('Error updating update.'); }
	})
	
  // Set url for API group routes
  app.use('/data', router);
}