// Import dependencies
var passport = require('passport');
var express = require('express');
var config = require('../config/app_config');
var jwt = require('jsonwebtoken');
var mongoose = require('mongoose');
var objectId = mongoose.Types.ObjectId;
var User = require('../models/user_model');
var Producer = require('../models/producer_model');
var Project = require('../models/project_model');
var ProducerUpload = require('../models/upload_model').ProducerUpload;
var ProjectUpload = require('../models/upload_model').ProjectUpload;
var Comment = require('../models/comment_model');
var Update = require('../models/update_model');
var fs = require('fs-extra');
var mkdirp = require('mkdirp-promise')
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
        // if (file.mimetype.indexOf('image/') > -1) {
        return cb(null, true);
        // } else {
        //   return cb("File not an image.", false);
        // }
    }
});

var uploadSingle = uploadFile.single('file');
var uploadMultiple = uploadFile.array('files');

const requireAuth = passport.authenticate('jwt', { session: false });

var BuildProducerRequest = function (requestBody) {
    var out = {};
    out.name = requestBody.name;
    out.desc = requestBody.desc;
    out.status = requestBody.status;
    if (requestBody.availability) {
        if (typeof requestBody.availability != 'object') {
            out.availability = requestBody.availability.split(',');
        } else {
            out.availability = requestBody.availability;
        }
    }
    out.type = requestBody.type;
    out.energyType = requestBody.energyType;
    out.address1 = requestBody.address1;
    out.address2 = requestBody.address2;
    out.city = requestBody.city;
    out.state = requestBody.state;
    out.zip = requestBody.zip;
    out.approvalNumber = requestBody.approvalNumber;
    return out;
}

var BuildProjectRequest = function (requestBody) {
    var out = {};
    out.name = (requestBody.name) ? (requestBody.name) : ('');
    out.desc = (requestBody.desc) ? (requestBody.desc) : ('');
    out.status = (requestBody.status) ? (requestBody.status) : ('');
    if (requestBody.availability) {
        if (typeof requestBody.availability != 'object') {
            out.availability = requestBody.availability.split(',');
        } else {
            out.availability = requestBody.availability;
        }
    }
    out.projectType = (requestBody.projectType) ? (requestBody.projectType) : ('');
    out.energyMix = (requestBody.energyMix) ? (requestBody.energyMix) : ('');
    out.address1 = (requestBody.address1) ? (requestBody.address1) : ('');
    out.address2 = (requestBody.address2) ? (requestBody.address2) : ('');
    out.city = (requestBody.city) ? (requestBody.city) : ('');
    out.state = (requestBody.state) ? (requestBody.state) : ('');
    out.zip = (requestBody.zip) ? (requestBody.zip) : ('');
    out.programCategory = (requestBody.programCategory) ? (requestBody.programCategory) : ('');
    out.capacity = (requestBody.capacity) ? (requestBody.capacity) : ('');
    if (requestBody.utilityDistricts) {
        if (typeof requestBody.utilityDistricts != 'object') {
            out.utilityDistricts = requestBody.utilityDistricts.split(',');
        } else {
            out.utilityDistricts = requestBody.utilityDistricts;
        }
    }
    return out;
}

module.exports = function (app) {
    var router = express.Router();
    //Rewrite
    router.get('/producer', function (req, res) {
        //Get all projects, narrow by search, how to implement paging?
        //TODO implement paging, we don;t want users requesting thousands of profiles
        //Get all profiles, or if req.email then search for one user by email
        var queryString = req.query;
        if (Object.keys(queryString).length > 0) {
            // if (req.query) {
            var result = Producer.find();
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
            result.populate('uploads').populate('projects').populate('owner').exec(function (err, producers) {
                if (err) {
                    res.status(500).json({ error: err });
                }
                if (Object.keys(producers).length == 0) {
                    res.status(200).json({ message: "No producer profiles found matching search criteria." });
                } else {
                    res.json({ producers: producers });
                }
            })
        } else {
            Producer.find(function (err, producer) {
                if (err) {
                    console.log(err);
                    res.json({ error: err });
                }
                res.status(200).json(producer);
            })
        }
    })

    router.post('/producer', requireAuth, function (req, res) {
        //New producer profile, check Owner, assign to req.user
        var body = BuildProducerRequest(req.body);
        var p = new Producer(body);
        p.owner = req.user.id;
        p.save(function (err, p) {
            if (err) {
                res.status(500).json({ error: JSON.stringify(err) });
            } else {
                res.status(200).json({ producer: p, message: "Save successful" });
            }
        })
    })

    router.get('/producer/:id', function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                var search = Producer.findById(req.params.id);
                search.populate('uploads').populate('updates').populate('comments').populate({ path: 'comments', populate: { path: 'postedBy' } }).populate({ path: 'projects', populate: { path: 'uploads' } }).populate('owner').exec(function (err, producer) {
                    if (err) {
                        res.status(400).json({ error: err });
                    } else {
                        res.json({ producer: producer });
                    }
                });
            } else { res.status(401).json({ message: "Invalid object id." }); }
        } catch (e) {
            res.json({ err: e });
        }
    })

    router.delete('/producerTest/:id', function (req, res) {
        Producer.remove({ _id: req.params.id }, function (e, rem) {
            if (e) { res.status(200).json('Removal error.'); }
            res.status(200).json('Removed: ' + rem);
        })
    })

    router.delete('/projectTest/:id', function (req, res) {
        Project.remove({ _id: req.params.id }, function (e, rem) {
            if (e) { res.status(200).json('Removal error.'); }
            res.status(200).json('Removed: ' + rem);
        })
    })

    router.put('/producer/:id', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                Producer.findById(req.params.id).exec(function (err, producer) {
                    if (err) throw err;
                    if (producer.owner.toString() == req.user.id) {
                        producer.name = req.body.name;
                        producer.desc = req.body.desc;
                        producer.status = req.body.status;
                        if (req.body.availability) {
                            if (typeof req.body.availability != 'object') {
                                producer.availability = req.body.availability.split(',');
                            } else {
                                producer.availability = req.body.availability;
                            }
                        }
                        producer.type = req.body.type;
                        producer.energyType = req.body.energyType;
                        producer.address1 = req.body.address1;
                        producer.address2 = req.body.address2;
                        producer.city = req.body.city;
                        producer.state = req.body.state;
                        producer.zip = req.body.zip;
                        producer.approvalNumber = req.body.approvalNumber;
                        producer.save(function (err, newProducer) {
                            if (err) {
                                res.status(500).json({ error: JSON.stringify(err) });
                            } else {
                                res.status(200).json({ producer: newProducer, message: "Update successful" });
                            }
                        })
                    } else {
                        console.log('User Id: ' + req.user.id);
                        console.log('Producer Owner Id: ' + producer.owner.id.toString());
                        res.status(403).json({ message: "Access denied" });
                    }
                })
            } else {
                res.status(500).json({ message: "Invalid object id" });
            }
        } catch (e) {
            res.status(500).json({ error: JSON.stringify(e) });
        }
    })

    router.delete('/producer/:id', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                Producer.findById(req.params.id).exec(function (err, producer) {
                    if (err) res.satus(500).json('Error findinng producer');
                    if (producer.owner && producer.owner.toString() == req.user.id) {
                        //This user owns the profile, enable update
                        Producer.remove({ _id: req.params.id }, function (error) {
                            if (err) {
                                res.status(500).json({ message: "Error finding producer.", error: err });
                            } else {
                                res.status(200).json({ message: "Producer removed successfully." });
                                //TODO: remove orphaned projects, producer profiles, uploads, etc.
                            }
                        })
                    } else {
                        res.status(403).json({ message: "Access denied" });
                    }
                })
            } else {
                res.status(500).json({ message: "Invalid object id" });
            }
        } catch (e) {
            res.status(500).json({ error: JSON.stringify(e) });
        }
    })

    router.get('producer/:id/comments', function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                Producer.findById(req.params.id).populate('comments').exec(function (err, producer) {
                    if (err) {
                        res.status(500).json({ message: "Error finding producer.", error: err });
                    } else {
                        res.status(200).json(producer);
                    }
                })
            } else {
                res.status(500).json('Invalid item Id.');
            }
        } catch (e) {
            res.status(500).json({ error: JSON.stringify(e) });
        }
    })
    router.post('producer/:id/comments', function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                Producer.findById(req.params.id).exec(function (err, producer) {
                    var c = new Comment(req.body);
                    c.save(function (err, cmt) {
                        if (err) {
                            res.status(500).json({ message: "Error saving comment.", error: err });
                        } else {
                            producer.comments.push(cmt._id);
                            producer.save(function (err, prod) {
                                if (err) res.status(500).json('Error saving producer comment.');
                                res.status(200).json('Comment saved.');
                            })
                        }
                    })
                })
            } else {
                res.status(500).json('Invalid item Id.');
            }
        } catch (e) {
            res.status(500).json({ error: JSON.stringify(e) });
        }
    })
    router.put('producer/:id/comments/:commentId', function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                Comment.findOneAndUpdate({ _id: req.params.commentId }, req.body, { new: true }, function (err, cmt) {
                    if (err) res.status(500).json('Error saving comment.');
                    res.status(200).json('Comment saved successfully.');
                })
            } else {
                res.status(500).json('Invalid item Id.');
            }
        } catch (e) {
            res.status(500).json({ error: JSON.stringify(e) });
        }
    })
    router.delete('producer/:id/comments/:commentId', function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                Producer.findById(req.params.id).exec(function (err, producer) {
                    Comment.remove({ _id: req.params.commentId }, function (err) {
                        if (err) res.status(500).json('Error removing comment.');
                        for (var i = 0; i < producer.comments.length; i++) {
                            if (producer.comments[i] == req.params.commentId) {
                                producer.comments.splice(i);
                                producer.save(function (err, p) {
                                    if (err) res.status(500).json('Error removing comment.');
                                    res.status(200).json('Comment removed.');
                                })
                            }
                        }
                    })
                })
            } else {
                res.status(500).json('Invalid item Id.');
            }
        } catch (e) {
            res.status(500).json({ error: JSON.stringify(e) });
        }
    })

    router.post('/producer/:id/images', [requireAuth, uploadMultiple], function (req, res) {
        try {
            //Create a new upload entry under the producer's uploads field
            //Check that producer profile is owner by this user
            Producer.findById(req.params.id).populate('uploads').populate({ path: 'projects', populate: { path: 'uploads' } }).populate('owner').exec(function (err, producer) {
                if (err) {
                    res.status(500).json({ error: "Unable to save user upload.", err: err });
                } else {
                    //Check to see that the current user owns this profile
                    if (producer.owner.id == req.user.id) {
                        try {
                            if (objectId.isValid(req.params.id)) {
                                var promisesArray = [];
                                var saveArray = [];
                                var upld = new ProducerUpload();
                                upld.fileName = req.files[0].path;
                                upld.fileType = req.files[0].mimetype;
                                //upld.description = req.body.imageDescriptions[i];
                                upld.fileSize = req.files[0].size;
                                try {
                                    upld.save(function (err, upld) {
                                        producer.uploads.push(upld._id);
                                        producer.save(function (e, usr) {
                                            if (e) res.status(500).json('Error saving uploads to user.');
                                            res.status(200).json(upld);
                                        })
                                    })
                                } catch (e) {
                                    res.status(500).json('Error saving user upload.');
                                }
                            } else {
                                res.status(500).json({ message: "Unauthorized" });
                            }
                        } catch (e) { res.status(500).json({ message: 'Error getting user images.', error: e }); }
                    } else {
                        //Current user does not own this profile, return an access denied message and delete the files
                        res.status(403).json({ message: "The current user does not have permission to update this producer profile." });
                    }
                }
            })
        } catch (e) {
            res.status(404).json({ message: "Error saving user uploads.", error: JSON.stringify(e) });
        }
    })

    router.put('/producers/:id/images/:imageId', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                ProducerUpload.findById(req.params.imageId).exec(function (err, producerUpload) {
                    if (err) {
                        res.status(500).json({ message: "Error finding image.", error: err });
                    } else {
                        producerUpload.description = (req.body.description) ? (req.body.description) : (producerUpload);
                        producerUpload.save(function (err, producerUpload) {
                            if (err) {
                                res.status(500).json({ message: "Error saving user upload.", error: err });
                            } else {
                                res.status(200).json({ message: "User updated successfully.", userUpload: producerUpload });
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

    router.delete('/producer/:id/images/:imageId', requireAuth, function (req, res) {
        try {
            if (objectId.isValid(req.params.id)) {
                var promisesArray = [];
                promisesArray.push(ProducerUpload.findById(req.params.imageId).exec());
                Promise.all(promisesArray).then(function (responseData) {
                    Producer.findById(req.params.id).exec(function (err, producer) {
                        ProducerUpload.remove({ _id: req.params.imageId }, function (err) {
                            if (err) {
                                res.status(500).json({ message: "Error finding producer upload.", error: err });
                            } else {
                                if (responseData[0].fileName != null) {
                                    fs.remove(responseData[0].fileName);
                                    for (var i = 0; i < producer.uploads.length; i++) {
                                        if (producer.uploads[i] == req.params.imageId) {
                                            producer.uploads.splice(i, 1);
                                            producer.save(function (err, p) {
                                                if (err) res.status(500).json('Error removing upload.');
                                                res.status(200).json('Upload removed.');
                                            })
                                            i = producer.uploads.length;
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
            res.status(500).json({ message: "Error removing producer uploads.", error: e });
        }
    });

    router.post('/producer/:id/projects', requireAuth, function (req, res) {
        try {
            //Create a new upload entry under the producer's uploads field
            //Check that producer profile is owner by this user
            Producer.findById(req.params.id).populate('uploads').populate('projects').populate('owner').exec(function (err, producer) {
                if (err) {
                    res.status(500).json({ error: "Unable to save project.", err: err });
                } else {
                    //Check to see that the current user owns this profile
                    if (producer.owner.id.toString() == req.user.id) {
                        try {
                            var proj = new Project(BuildProjectRequest(req.body));
                            proj.projectOwner = producer.id;
                            proj.save(function (err, project) {
                                if (err) {
                                    throw err;
                                } else {
                                    Producer.findById(req.params.id, function (err, producer) {
                                        if (err) throw err;
                                        producer.projects.push(project._id);
                                        producer.save(function (err, p) {
                                            res.status(200).json({ message: 'Project saved successfully.', project: project });
                                        })
                                    })
                                }
                            });
                        } catch (e) { console.log("Error uploading item.", JSON.stringify(e)); }
                    } else {
                        //Current user does not own this profile, return an access denied message and delete the files
                        res.status(403).json({ message: "The current user does not have permission to update this producer profile." });
                    }
                }
            })
        } catch (e) {
            res.status(404).json({ message: "Error saving user uploads.", error: JSON.stringify(e) });
        }
    })

    router.put('/producer/:id/projects/:projectId', requireAuth, function (req, res) {
        try {
            Producer.findById(req.params.id).populate('owner').exec(function (err, producer) {
                if (err) {
                    res.status(500).json({ error: "Unable to save project.", err: err });
                } else {
                    //Check to see that the current user owns this profile
                    if (producer.owner.id.toString() == req.user.id) {
                        try {
                            Project.findOneAndUpdate({ _id: req.params.projectId }, BuildProjectRequest(req.body), { new: true }, function (err, project) {
                                if (err) throw err;
                                Update.update({ projectid: req.params.projectId }, { $set: { 'projectname': req.body.name } }, function (err1, updates) {
                                    if (err1) { throw err1; }
                                    //
                                });
                                res.status(200).json({ message: 'Project saved successfully.', project: project });
                            });
                        } catch (e) { console.log("Error uploading item.", JSON.stringify(e)); }
                    } else {
                        //Current user does not own this profile, return an access denied message and delete the files
                        res.status(403).json({ message: "The current user does not have permission to update this producer profile." });
                    }
                }
            })
        } catch (e) {
            res.status(404).json({ message: "Error saving user uploads.", error: JSON.stringify(e) });
        }
    })

    router.delete('/producer/:id/projects/:projectId', requireAuth, function (req, res) {
        try {
            Producer.findById(req.params.id).populate('owner').exec(function (err, producer) {
                if (err) {
                    res.status(500).json({ error: "Unable to save project.", err: err });
                } else {
                    //Check to see that the current user owns this profile
                    if (producer.owner.id.toString() == req.user.id) {
                        try {
                            Project.findById(req.params.projectId).populate('projectOwner').exec(function (err, project) {
                                if (err) throw err;
                                if (req.params.id == project.owner.id) {
                                    project.remove(function (err) {
                                        if (err) throw err;
                                        res.status(200).json({ message: "Project removed." });
                                    })
                                } else {
                                    res.status(403).json({ message: "The current user does not have permission to update this producer profile." });
                                }
                            })
                        } catch (e) { console.log("Error uploading item.", JSON.stringify(e)); }
                    } else {
                        //Current user does not own this profile, return an access denied message and delete the files
                        res.status(403).json({ message: "The current user does not have permission to update this producer profile." });
                    }
                }
            })
        } catch (e) {
            res.status(404).json({ message: "Error saving user uploads.", error: JSON.stringify(e) });
        }
    })

    router.post('/producer/:id/projects/:projectId/images', [requireAuth, uploadMultiple], function (req, res) {
        try {
            Producer.findById(req.params.id).populate('owner').exec(function (err, producer) {
                if (err) {
                    res.status(500).json({ error: "Unable to save project.", err: err });
                } else {
                    //Check to see that the current user owns this profile
                    if (producer.owner.id.toString() == req.user.id) {
                        try {
                            Project.findById(req.params.projectId).populate('projectOwner').exec(function (err, project) {
                                if (err) throw err;
                                if (req.params.id == project.owner.id) {
                                    for (var i = 0; i < req.files.length; i++) {
                                        var upld = new ProjectUpload();
                                        upld.fileName = req.files[i].path + '/' + req.files[i].name;
                                        upld.fileType = req.files[i].mimetype;
                                        upld.description = req.body.imageDescriptions[i];
                                        upld.fileSize = req.files[i].size;
                                        try {
                                            upld.save(function (err, upload) {
                                                if (err) {
                                                    throw err;
                                                } else {
                                                    project.uploads.push(upload._id);
                                                }
                                            });
                                        } catch (e) { console.log("Error uploading item.", JSON.stringify(e)); }
                                    }
                                    project.save(function (err, project) {
                                        if (err) throw err;
                                        res.status(200).json({ message: 'Project uploads saved successfully.', project: project });
                                    })
                                } else {
                                    res.status(403).json({ message: "The current user does not have permission to update this producer profile." });
                                }
                            })
                        } catch (e) { console.log("Error uploading item.", JSON.stringify(e)); }
                    } else {
                        //Current user does not own this profile, return an access denied message and delete the files
                        res.status(403).json({ message: "The current user does not have permission to update this producer profile." });
                    }
                }
            })
        } catch (e) {
            res.status(404).json({ message: "Error saving user uploads.", error: JSON.stringify(e) });
        }
    })

    router.put('/producer/:id/projects/:projectId/images/:imageId', requireAuth, function (req, res) {
        try {
            var promisesArray = [];
            promisesArray.push(Producer.findById(req.params.id).populate('owner').exec());
            promisesArray.push(Project.findById(req.params.projectId).populate('projectOwner').exec());
            Promise.all(promisesArray).then(function (responseData) {
                var producer = responseData[0];
                var project = responseData[1];
                if (producer.owner.id.toString() === req.user.id && project.projectOwner.id.toString() == producer.id.toString()) {
                    ProjectUpload.findById(req.params.imageId, function (err, upload) {
                        if (err) throw err;
                        upload.description = (req.body.description) ? (req.body.description) : (upload.description);
                        upload.save(function (err, upld) {
                            if (err) throw err;
                            res.status(200).json({ message: 'Updated project upload.' });
                        })
                    })
                } else {
                    res.status(200).json({ message: 'Unauthorized' });
                }
            }, function (err) {
                res.status(500).json({ message: "Error updating project upload.", error: err });
            })
        } catch (e) { res.status(500).json({ message: 'Error updating project upload.', error: e }); }
    })

    router.delete('/producer/:id/projects/:projectId/images/:imageId', requireAuth, function (req, res) {
        try {
            var promisesArray = [];
            promisesArray.push(Producer.findById(req.params.id).populate('owner').exec());
            promisesArray.push(Project.findById(req.params.projectId).populate('projectOwner').exec());
            Promise.all(promisesArray).then(function (responseData) {
                var producer = responseData[0];
                var project = responseData[1];
                if (producer.owner.id.toString() === req.user.id && project.projectOwner.id.toString() == producer.id.toString()) {
                    ProjectUpload.remove({ _id: req.params.imageId }, function (err) {
                        if (err) throw err;
                        var index = 0;
                        var found = false;
                        var fileName = '';
                        while (index < project.uploads.length && !found) {
                            if (req.params.imageId == project.uploads[index]._id) {
                                fileName = project.uploads[index].fileName;
                                project.uploads.splice(index, 1);
                                found = true;
                            }
                        }
                        project.save(function (err, project) {
                            if (err) throw err;
                            res.status(200).json({ message: "User upload removed successfully." });
                        })
                        fs.remove(fileName);
                    })
                } else {
                    res.status(200).json({ message: 'Unauthorized' });
                }
            }, function (err) {
                res.status(500).json({ message: "Error updating project upload.", error: err });
            })
        } catch (e) { res.status(500).json({ message: 'Error removing project upload.', error: e }); }
    })


    // Set url for API group routes
    app.use('/data', router);
}
