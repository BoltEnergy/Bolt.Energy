// Import dependencies
var passport = require('passport');
var express = require('express');
var config = require('../config/app_config');
var jwt = require('jsonwebtoken');
var mongoose = require('mongoose');
var objectId = mongoose.Types.ObjectId;
var User = require('../models/user_model');
var Producer = require('../models/producer_model');
var Comment = require('../models/comment_model');

const requireAuth = passport.authenticate('jwt', { session: false });

module.exports = function(app) {
  var router = express.Router();
  
  router.get('/comment/:id', function(req, res) {
    try {
			if (objectId.isValid(req.params.id)) {
				var search = Comment.findById(req.params.id);
				search.populate('replies').exec(function(err, up) {
					if (err) {
						res.status(400).json({error: err});
					} else {
						res.json({ comment: up });
					}
				});
			} else { res.status(401).json({ message: "Invalid object id." }); }
    } catch (e) { res.status(500).json('Error getting comments'); }
  })
  
  router.post('/producer/:producerId/comment', requireAuth, function(req, res) {
    try {
			if (objectId.isValid(req.params.producerId)) {
        var c = new Comment(req.body);
        c.postedBy = req.user.id;
        c.save(function(err, co) {
          if (err) res.status(500).json('Error saving comment.');
          Producer.findById(req.params.producerId, function(err, p) {
            if (err) res.status(500).json('Error saving update.');
            p.comments.push(co._id);
            p.save(function(err, pr) {
              if (err) res.status(500).json('Error saving update.');
              res.status(200).json({ message: 'Comment saved.', producer: p});  
            })
          })
        })
			} else { res.status(401).json({ message: "Invalid object id." }); }
    } catch (e) { res.status(500).json('Error getting comments'); }
  })
  
  router.post('/producer/:producerId/comment/:commentId/reply', requireAuth, function(req, res) {
    try {
			if (objectId.isValid(req.params.producerId)) {
        var c = new Comment(req.body);
        c.postedBy = req.user.id;
        c.save(function(err, co) {
          if (err) res.status(500).json('Error saving comment.');
          Comment.findById(req.params.commentId, function(err, com) {
            com.replies.push(co._id);
            com.save(function(err, comm) {
              if (err) res.status(500).json('Error saving update.');
              res.status(200).json({ message: 'Comment saved.', comment: comm});  
            })
          })
        })
			} else { res.status(401).json({ message: "Invalid object id." }); }
    } catch (e) { res.status(500).json('Error getting comments'); }
  })
  
  app.use('/data', router);
}