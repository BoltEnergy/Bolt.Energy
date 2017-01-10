// Import dependencies
var passport = require('passport');
var express = require('express');
var config = require('../config/app_config');
var jwt = require('jsonwebtoken');
var mongoose = require('mongoose');
var Producer = require('../models/producer_model');
var Project = require('../models/project_model');

const requireAuth = passport.authenticate('jwt', { session: false });

module.exports = function(app) {
	var router = express.Router();

	router.get('/producerSearch/:searchTerm', function(req, res) {
    var term = req.params.searchTerm;
    var queryArray = [];
    queryArray.push({ "name": { $regex: term, $options: "i" } });
    queryArray.push({ "desc": { $regex: term, $options: "i" } });
    queryArray.push({ "status": { $regex: term, $options: "i" } });
    queryArray.push({ "energyType": { $regex: term, $options: "i" } });
    queryArray.push({ "type": { $regex: term, $options: "i" } });
    queryArray.push({ "availability": { $regex: term, $options: "i" } });
    var query = {
      $or: queryArray
    };
    Producer.find(query, function(err, producers){
      if (err) {
        console.log(err);
        res.json({ error: err });
      }
      res.status(200).json(producers);
    });
  })

  router.get('/projectSearch/:searchTerm', function(req, res) {
    var term = req.params.searchTerm;
    var queryArray = [];
    queryArray.push({ "name": { $regex: term, $options: "i" } });
    queryArray.push({ "desc": { $regex: term, $options: "i" } });
    queryArray.push({ "status": { $regex: term, $options: "i" } });
    queryArray.push({ "energyMix": { $regex: term, $options: "i" } });
    queryArray.push({ "projectType": { $regex: term, $options: "i" } });
    queryArray.push({ "availability": { $regex: term, $options: "i" } });
    var query = {
      $or: queryArray
    };
    Project.find(query, function(err, projects) {
      if (err) { res.status(500).json(err); }
      res.status(200).json(projects);
    })
  })
  // Set url for API group routes
  app.use('/data', router);
}