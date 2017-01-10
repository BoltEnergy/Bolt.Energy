// Import dependencies
var passport = require('passport');
var express = require('express');
var config = require('../config/app_config');
var jwt = require('jsonwebtoken');
var mongoose = require('mongoose');
var mkdirp = require('mkdirp-promise')
var objectId = mongoose.Types.ObjectId;
var fs = require('fs-extra');
var multer = require('multer');
var storage = multer.diskStorage({
  destination: function (req, file, cb) {
    var userDir = req.user ? req.user._id.toString() : 'uploads';
    var path = config.userImagesDir + userDir;
    mkdirp(path).then(function(responseData) {
    	console.log("Producer upload directory created: " + path);
    	req.filepath = path;
    	cb(null, path);
    }).catch(function(error) {
    	console.log("Error creating producer upload directory: " + JSON.stringify(error));
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

// Set up middleware
const requireAuth = passport.authenticate('jwt', { session: false });

// Load models
const User = require('../models/user_model');
const UserUpload = require('../models/upload_model').UserUpload;

// Export the routes for our app to use
module.exports = function(app) {
  // Create API group routes
  const apiRoutes = express.Router();
  // Register new users
  apiRoutes.post('/register', function(req, res, next) {
    if (req.body.email && req.body.password) {
      var newUser = new User(req.body);
      newUser.save(function(err, user) {
        if (err) {
          if (err.message.indexOf('duplicate') != -1) {
            res.status(403).json({ message: 'Email already in use.' });
          } else {
            res.status(403).json({ error: err });
          }
        } else {
            const token = jwt.sign({ id: user._id, firstName: user.firstName, lastName: user.lastName, email: user.email, accountType: user.accountType }, config.secret, {
              expiresIn: 10080 // in seconds
            });
          res.status(201).json({ success: true, message: 'Successfully created new user.', token: 'JWT ' + token, user: user });
        }
      });
    } else {
      res.json({ message: "Email and password are required to register." });
    }
  });

  //This is a general search, for granning individual profiles for administration see below
  apiRoutes.get('/users', requireAuth, function(req, res, next){
    //Get all profiles, or if req.email then search for one user by email
    var queryString = req.query;
    if (Object.keys(queryString).length > 0) {
    // if (req.query) {
      var result = User.find();
      for (var i = 0; i < Object.keys(req.query).length; i++) {
        var term = Object.keys(req.query)[i];
        var value = req.query[term];
        result.where(term).equals(value);
      }
      result.exec(function(err, user) {
        if (err) {
          res.status(500).json({ error: err });
        } else {
          if (Object.keys(user).length == 0) {
            res.status(200).json({ message: "No users found matching search criteria." });
          } else {
            res.json({ users: user });
          }
        }
      });
    } else {
      User.find(function(err, users){
        if (err) {
          console.log(err);
          res.json({ error: err });
        } else {
          console.log(users);
          res.json({ users: users });
        }
      });
    }
  });

  // Authenticate the user and get a JSON Web Token to include in the header of future requests.
  apiRoutes.post('/authenticate', function(req, res) {
    User.findOne({
      email: req.body.email
    }, function(err, user) {
      if (err) throw err;

      if (!user) {
        res.status(401).json({ success: false, message: 'Authentication failed. User not found.' });
      } else {
        // Check if password matches
        user.comparePassword(req.body.password, function(err, isMatch) {
          if (isMatch && !err) {
            // Create token if the password matched and no error was thrown
            const token = jwt.sign({ id: user._id, firstName: user.firstName, lastName: user.lastName, email: user.email, accountType: user.accountType }, config.secret, {
              expiresIn: 10080 // in seconds
            });
            console.log(Date.now());
            res.status(200).json({ success: true, token: 'JWT ' + token, user: user });
          } else {
            res.status(401).json({ success: false, message: 'Authentication failed. Passwords did not match.' });
          }
        });
      }
    });
  });

  //TODO: Add an authentication check to make sure user can edit this profile
  apiRoutes.put('/users/:id', requireAuth, function(req, res) {
    try {
      if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
        User.findById(req.params.id, function(err, user) {
          if (err) {
            res.status(500).json({ message: "Error finding user.", error: err });
          } else {
            user.firstName = (req.body.firstName)?(req.body.firstName):(user.firstName);
            user.lastName = (req.body.lastName)?(req.body.lastName):(user.lastName);
            user.accountType = (req.body.accountType)?(req.body.accountType):(user.accountType);
            user.modified = new Date().getTime();
            user.save(function(err, user) {
              if (err) {
                res.status(500).json({ message: "Error saving user.", error: err });
              } else {
                res.status(200).json({ message: "User updated successfully.", user: user });
              }
            })
          }

        });
      } else {
        res.status(500).json({ message: "Invalid Object Id." });
      }
    } catch (e) {
      res.status(500).json({ error: e, message: "Error occurred while saving." });
    }
  });
  
  apiRoutes.delete('/users/:id', requireAuth, function(req, res) {
    try {
      if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
        User.remove({ _id: req.params.id }, function(err) {
          if (err) {
            res.status(500).json({ message: "Error finding user.", error: err });
          } else {
            res.status(200).json({ message: "User removed successfully." });
            //TODO: remove orphaned projects, producer profiles, uploads, etc.
          }
        })
      } else {
        res.status(403).json({ message: 'Unauthorized' });
      }
    } catch (e) {
      res.status(500).json({ message: "Error removing user.", error: e });
    }
  });

  apiRoutes.get('/users/:id', requireAuth, function(req, res) {
    try {
      if (objectId.isValid(req.params.id)) {
        User.findById(req.params.id).populate('uploads').exec(function(err, user) {
          if (err) {
            res.status(500).json({ error: err, message: "Error finding user." });
          } else {
            res.status(200).json({ user: user, message: "User information found." });
          }
        });
      } else {
        res.status(500).json({ message: "Invalid User Id." });
      }
    } catch (e) {
      res.status(500).json({ error: e, message: "Error occurred while retrieving data." });
    }
  });
 
  apiRoutes.post('/users/:id/images', [requireAuth, uploadMultiple], function(req, res) {
    //Upload multiple files, save them, add the ID's to the user uploads
    try {
      if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
        var promisesArray = [];
        var saveArray = [];
        var upld = new UserUpload();
        upld.fileName = req.files[0].path;
        upld.fileType = req.files[0].mimetype;
        //upld.description = req.body.imageDescriptions[i];
        upld.fileSize = req.files[0].size;
        try {
          upld.save(function(err, upld) {
            User.findById(req.user.id).populate('uploads').exec(function(err, user) {
              user.uploads.push(upld._id);
              user.save(function(e, usr) {
                if (e) res.status(500).json('Error saving uploads to user.');
                res.status(200).json(usr);
              })
            })
          });
        } catch (e) {
          res.status(500).json('Error saving user upload.');
        }
      } else {
        res.status(500).json({ message: "Unauthorized" }); 
      }
    } catch(e) { res.status(500).json({ message: 'Error getting user images.', error: e }); }
  });
  
  apiRoutes.put('/users/:id/images/:imageId', requireAuth, function(req, res) {
    try {
      if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
        UserUpload.findById(req.params.imageId, function(err, userUpload) {
          if (err) {
            res.status(500).json({ message: "Error finding image.", error: err });
          } else {
            userUpload.description = (req.body.description)?(req.body.description):(userUpload.description);
            userUpload.save(function(err, userUpload) {
              if (err) {
                res.status(500).json({ message: "Error saving user upload.", error: err });
              } else {
                res.status(200).json({ message: "User updated successfully.", userUpload: userUpload });
              }
            });
          }
        });
      } else {
        res.status(500).json({ message: "Invalid Object Id." });
      }
    } catch (e) {
      res.status(500).json({ error: e, message: "Error occurred while saving." });
    }
  });
  
  apiRoutes.delete('/users/:id/images/:imageId', requireAuth, function(req, res) {
    try {
      if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
        UserUpload.remove({ _id: req.params.imageId }, function(err) {
          if (err) {
            res.status(500).json({ message: "Error finding user upload.", error: err });
          } else {
            User.findById(req.params.id).populate('uploads').exec(function(err, user) {
              if (err) throw err;
              var index = 0;
              var found = false;
              var fileName = '';
              while (index < user.uploads.length && !found) {
                if (req.params.imageId == user.uploads[index]._id) {
                  fileName = user.uploads[index].fileName;
                  user.uploads.splice(index,1);
                  found = true;
                }
              }
              user.save(function(err, user) {
                if (err) throw err;
                res.status(200).json({ message: "User upload removed successfully.", user: user });
              });
              fs.remove(fileName);
            });
          }
        });
      } else {
        res.status(403).json({ message: 'Unauthorized' });
      }
    } catch (e) {
      res.status(500).json({ message: "Error removing user.", error: e });
    }
  });

  // Set url for API group routes
  app.use('/data', apiRoutes);
};
