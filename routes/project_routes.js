// Import dependencies
var passport = require('passport');
var express = require('express');
var config = require('../config/app_config');
var jwt = require('jsonwebtoken');
var mongoose = require('mongoose');
var objectId = mongoose.Types.ObjectId;
var Producer = require('../models/producer_model');
var Project = require('../models/project_model');
var ProducerUpload = require('../models/upload_model').ProducerUpload;
var ProjectUpload = require('../models/upload_model').ProjectUpload;
var fs = require('fs-extra');
var mkdirp = require('mkdirp-promise')
var multer = require('multer');
var storage = multer.diskStorage({
  destination: function (req, file, cb) {
    var userDir = req.user ? req.user._id.toString() : 'uploads';
    var path = config.userImagesDir + userDir;
    mkdirp(path).then(function(responseData) {
    	console.log("Project upload directory created: " + path);
    	req.filepath = path;
    	cb(null, path);
    }).catch(function(error) {
    	console.log("Error creating project upload directory: " + JSON.stringify(error));
    	cb(null, path);
    })
  },
  filename: function (req, file, cb) {
 		var currentDate = new Date().getTime();
 		var fileName = currentDate + "_" + file.originalname;
 		file.path = req.filepath;
    cb(null, fileName);
  }
});

var uploadFile = multer({
  storage: storage,
  fileFilter: function (req, file, cb) {
    console.log(file);
    if (file.mimetype.indexOf('image/') > -1) {
      return cb(null, true);
    } else {
      return cb("File not an image.", false);
    }
  }
});

var uploadSingle = uploadFile.single('file');
var uploadMultiple = uploadFile.array('files');

const requireAuth = passport.authenticate('jwt', { session: false });

module.exports = function(app) {
  
  var BuildProjectRequest = function(requestBody) {
    var out = {};
    out.name = (requestBody.name)?(requestBody.name):('');
    out.desc = (requestBody.desc)?(requestBody.desc):('');
    out.status = (requestBody.status)?(requestBody.status):('');
    if (requestBody.availability) {
    	if (typeof requestBody.availability != 'object') {
    		out.availability = requestBody.availability.split(',');
    	} else {
    		out.availability = requestBody.availability;
    	}
    }
    out.projectType = (requestBody.projectType)?(requestBody.projectType):('');
    out.energyMix =  (requestBody.energyMix)?(requestBody.energyMix):('');
    out.address1 =  (requestBody.address1)?(requestBody.address1):('');
    out.address2 = (requestBody.address2)?(requestBody.address2):('');
    out.city = (requestBody.city)?(requestBody.city):('');
    out.state = (requestBody.state)?(requestBody.state):('');
    out.zip = (requestBody.zip)?(requestBody.zip):('');
    out.programCategory = (requestBody.programCategory)?(requestBody.programCategory):('');
    out.capacity = (requestBody.capacity)?(requestBody.capacity):('');
    if (requestBody.utilityDistricts) {
    	if (typeof requestBody.utilityDistricts != 'object') {
    		out.utilityDistricts = requestBody.utilityDistricts.split(',');
    	} else {
    		out.utilityDistricts = requestBody.utilityDistricts;
    	}
    }
    return out;
  }
  
	var router = express.Router();
	//Rewrite
	router.get('/project', function(req, res) {
		//Get all projects, narrow by search, how to implement paging?
		//TODO implement paging, we don;t want users requesting thousands of profiles
    //Get all profiles, or if req.email then search for one user by email
    var queryString = req.query;
    if (Object.keys(queryString).length > 0) {
    // if (req.query) {
      var result = Project.find();
      for (var i = 0; i < Object.keys(req.query).length; i++) {
        var term = Object.keys(req.query)[i];
        var value = req.query[term];
        if (term && value) {
        	switch (term.toLowerCase()) {
        		case 'availability':
        			result.find({ "availability": value });
        			break;
      			default:
      				result.where(term).equals(value);
      				break;
        	}
        }
      };
      result.populate('uploads').populate('projectOwner').limit(9).exec(function(err, projects) {
        if (err) {
          res.status(500).json({ error: err });
        }
        if (Object.keys(projects).length == 0) {
          res.status(200).json({ message: "No project profiles found matching search criteria." });
        } else {
          res.json(projects);
        }
      })
    } else {
      Project.find().populate('uploads').populate('projectOwner').limit(9).exec(function(err, projects){
        if (err) {
          console.log(err);
          res.json({ error: err });
        }
        res.status(200).json(projects);
      })
    }	
  })
  
  router.get('/project/:id', function(req, res) {
    Project.findById(req.params.id).populate('uploads').exec(function(err, project) {
      res.json(project);
    })
  })
  
  //TEST ROUTE - Disable before deployment
  router.post('/project', function(req, res) {
    var p = new Project(BuildProjectRequest(req.body));
    p.save(function(err, proj) {
      if (err) res.status(500).json('Could not find producer.');
      res.status(200).json({message:'Project saved.', project:proj});
    });
  });
  
  //TEST ROUTE - Disable before deployment
  router.put('/project/:id', function(req, res) {
    Project.findOneAndUpdate({ _id: req.params.id }, req.body, { new: true }, function(err,project) {
      if (err) res.status(500).json('Could not find producer.');
      res.status(200).json({message:'Project saved.', project:project});
    });
  })
  
  router.delete('/project/:projectId', requireAuth, function(req, res) {
    Project.remove({ _id: req.params.projectId }, function(err) {
      if (err) res.status(500).json('Could not delete producer.');
      res.status(200).json({message:'Project removed.'});
    })
  })
  
  router.post('/project/:projectId/images', [requireAuth, uploadMultiple], function(req, res) {
    try {
      if (objectId.isValid(req.params.projectId)) {
        var promisesArray = [];
        var saveArray = [];
        var upld = new ProjectUpload();
        upld.fileName = req.files[0].path;
        upld.fileType = req.files[0].mimetype;
        //upld.description = req.body.imageDescriptions[i];
        upld.fileSize = req.files[0].size;
        try {
          upld.save(function(err, upld) {
            Project.findById(req.params.projectId).populate('uploads').exec(function(err, proj) {
              proj.uploads.push(upld._id);
              proj.save(function(e, p) {
                if (e) res.status(500).json('Error saving uploads to user.');
                res.status(200).json(upld);
              })
            })
          });
        } catch (e) {
          res.status(500).json('Error saving user upload.');
        }
      } else {
        res.status(500).json({ message: "Unauthorized" }); 
      }
    } catch(e) { res.status(500).json('Error uploading image.'); }
  })
  
  app.use('/data', router);
}
