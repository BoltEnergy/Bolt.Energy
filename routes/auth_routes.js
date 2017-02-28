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
        mkdirp(path).then(function (responseData) {
            console.log("Producer upload directory created: " + path);
            req.filepath = path;
            cb(null, path);
        }).catch(function (error) {
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
const Producer = require('../models/producer_model');
const ProducerUpload = require('../models/upload_model').ProducerUpload;
const Project = require('../models/project_model');
const ProjectUpload = require('../models/upload_model').ProjectUpload;
const Update = require('../models/update_model');
const Comment = require('../models/comment_model');


// Export the routes for our app to use
module.exports = function (app) {
    // Create API group routes
    const apiRoutes = express.Router();
    // Register new users
    apiRoutes.post('/register', function (req, res, next) {
        if (req.body.email && req.body.password) {
            var newUser = new User(req.body);
            newUser.save(function (err, user) {
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
    apiRoutes.get('/users', requireAuth, function (req, res, next) {
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
            result.exec(function (err, user) {
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
            User.find(function (err, users) {
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
    apiRoutes.post('/authenticate', function (req, res) {
        User.findOne({
            email: req.body.email
        }, function (err, user) {
            if (err) throw err;

            if (!user) {
                res.status(401).json({ success: false, message: 'Authentication failed. User not found.' });
            } else {
                // Check if password matches
                user.comparePassword(req.body.password, function (err, isMatch) {
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
    apiRoutes.put('/users/:id', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
                User.findById(req.params.id, function (err, user) {
                    if (err) {
                        res.status(500).json({ message: "Error finding user.", error: err });
                    } else {
                        user.firstName = (req.body.firstName) ? (req.body.firstName) : (user.firstName);
                        user.lastName = (req.body.lastName) ? (req.body.lastName) : (user.lastName);
                        user.accountType = (req.body.accountType) ? (req.body.accountType) : (user.accountType);
                        user.password = (req.body.password) ? (req.body.password) : (user.password);
                        user.email = (req.body.email) ? (req.body.email) : (user.email);
                        user.modified = new Date().getTime();
                        user.save(function (err, user) {
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

    apiRoutes.delete('/users/:id', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
                var promisesArray = [];

                switch (req.user.accountType) {
                    case 'Producer':
                        //
                        var producerId = "";
                        Producer.findOne({
                            owner: req.user.id
                        }, function (err, producer) {
                            if (err) {
                                throw err;
                            }
                            if (!producer) {
                                //res.status(500).json({ message: "Producer Not Found", error: "" });
                            } else {
                                producerId = producer._id;
                            }
                            if (producerId != "") {
                                promisesArray.push(Producer.findById(producerId).populate('uploads').exec());
                                Promise.all(promisesArray).then(function (responseData) {
                                    User.remove({ _id: req.params.id }, function (err) {
                                        if (err) {
                                            res.status(500).json({ message: "Error finding user.", error: err });
                                        } else {
                                            if (responseData[0] != null) {
                                                var index = 0;

                                                // If User have Producer, remove Producer
                                                if (producerId != "") {
                                                    Producer.remove({ _id: producerId }, function (err) {
                                                        if (err) {
                                                            throw err;
                                                        }
                                                    });
                                                }

                                                // If Producer have Uploads, remove ProducerUploads
                                                if (responseData[0].uploads.length > 0) {
                                                    while (responseData[0].uploads.length > 0) {
                                                        ProducerUpload.remove({ _id: responseData[0].uploads[0] }, function (err) {
                                                            if (err) {
                                                                throw err;
                                                            }
                                                        });
                                                        responseData[0].uploads.splice(0, 1);
                                                    }
                                                }

                                                // If Producer have Updates, remove Updates
                                                if (responseData[0].updates.length > 0) {
                                                    while (responseData[0].updates.length > 0) {
                                                        Update.remove({ _id: responseData[0].updates[0] }, function (err) {
                                                            if (err) {
                                                                throw err;
                                                            }
                                                        });
                                                        responseData[0].updates.splice(0, 1);
                                                    }
                                                }

                                                // If Producer have Comments, remove Comments
                                                if (responseData[0].comments.length > 0) {
                                                    while (responseData[0].comments.length > 0) {
                                                        Comment.remove({ _id: responseData[0].comments[0] }, function (err) {
                                                            if (err) {
                                                                throw err;
                                                            }
                                                        });
                                                        responseData[0].comments.splice(0, 1);
                                                    }
                                                }

                                                // If Producer have Projects, remove Projects
                                                if (responseData[0].projects.length > 0) {
                                                    var promisesArray1 = [];
                                                    while (index < responseData[0].projects.length) {
                                                        promisesArray1.push(Project.findById(responseData[0].projects[index]).populate('uploads').exec());
                                                        index++;
                                                    }
                                                    Promise.all(promisesArray1).then(function (projectsData) {
                                                        Project.remove({ projectOwner: producerId }, function (err) {
                                                            if (err) {
                                                                throw err;
                                                            }
                                                            else {
                                                                var indx = 0;

                                                                while (indx < projectsData.length) {
                                                                    //If Projects have Uploads, find and remove ProjectUploads
                                                                    if (projectsData[indx].uploads.length > 0) {
                                                                        for (var i = 0; i < projectsData[indx].uploads.length; i++) {
                                                                            ProjectUpload.remove({ _id: projectsData[indx].uploads[i]._id }, function (err) {
                                                                                if (err) {
                                                                                    throw err;
                                                                                }
                                                                            })
                                                                        }
                                                                    }
                                                                    projectsData.splice(indx, 1);
                                                                }
                                                            }
                                                        })
                                                    });
                                                    index = 0;
                                                }

                                                fs.remove(config.userImagesDir + req.params.id);

                                            }
                                            res.status(200).json({ message: "User removed successfully." });
                                        }
                                    })
                                })
                            }
                        });

                        break;
                    case 'Consumer':
                        promisesArray.push(User.findById(req.params.id).populate('uploads').exec());
                        Promise.all(promisesArray).then(function (responseData) {

                            User.remove({ _id: req.params.id }, function (err) {
                                if (err) {
                                    res.status(500).json({ message: "Error finding user.", error: err });
                                } else {
                                    //TODO: 
                                    var index = 0;
                                    if (responseData[0].uploads.length > 0) {
                                        while (index < responseData[0].uploads.length) {
                                            fileName = responseData[0].uploads[index].fileName;
                                            UserUpload.remove({ _id: responseData[0].uploads[index]._id }, function (err) {
                                                if (err) {
                                                    throw err;
                                                }
                                            })
                                            index++;
                                        }
                                        fs.remove(config.userImagesDir + req.params.id);
                                    }
                                    res.status(200).json({ message: "User removed successfully." });
                                }
                            })
                        })
                        break;
                    default:
                        break;
                }

            } else {
                res.status(403).json({ message: 'Unauthorized' });
            }
        } catch (e) {
            res.status(500).json({ message: "Error removing user.", error: e });
        }
    });

    apiRoutes.get('/users/:id', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                User.findById(req.params.id).populate('uploads').exec(function (err, user) {
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

    apiRoutes.post('/users/:id/images', [requireAuth, uploadMultiple], function (req, res) {
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
                    upld.save(function (err, upld) {
                        User.findById(req.user.id).populate('uploads').exec(function (err, user) {
                            user.uploads.push(upld._id);
                            user.save(function (e, usr) {
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
        } catch (e) { res.status(500).json({ message: 'Error getting user images.', error: e }); }
    });

    apiRoutes.put('/users/:id/images/:imageId', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
                UserUpload.findById(req.params.imageId, function (err, userUpload) {
                    if (err) {
                        res.status(500).json({ message: "Error finding image.", error: err });
                    } else {
                        userUpload.description = (req.body.description) ? (req.body.description) : (userUpload.description);
                        userUpload.save(function (err, userUpload) {
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

    apiRoutes.delete('/users/:id/images/:imageId', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id) && req.user.id == req.params.id) {
                var promisesArray = [];
                promisesArray.push(UserUpload.findById(req.params.imageId).exec());
                Promise.all(promisesArray).then(function (responseData) {
                    User.findById(req.params.id).exec(function (err, user) {
                        UserUpload.remove({ _id: req.params.imageId }, function (err) {
                            if (err) {
                                res.status(500).json({ message: "Error finding user upload.", error: err });
                            } else {
                                if (responseData[0].fileName != null) {
                                    fs.remove(responseData[0].fileName);
                                    for (var i = 0; i < user.uploads.length; i++) {
                                        if (user.uploads[i] == req.params.imageId) {
                                            user.uploads.splice(i, 1);
                                            user.save(function (err, p) {
                                                if (err) res.status(500).json('Error removing upload.');
                                                res.status(200).json('Upload removed.');
                                            })
                                            i = user.uploads.length;
                                        }
                                    }
                                }
                            }
                        })
                    })
                })
            } else {
                res.status(403).json({ message: 'Unauthorized' });
            }
        } catch (e) {
            res.status(500).json({ message: "Error removing user image.", error: e });
        }
    });

    // Set url for API group routes
    app.use('/data', apiRoutes);
};
