var express = require('express');
var config = require('./config/app_config');
var mongoose = require('mongoose');
var compression = require('compression');
var passport = require('passport');
mongoose.connect(config.dbUrl);

var bodyParser = require('body-parser');
var app = express();
app.use(compression());
app.use(bodyParser.urlencoded({ limit: '10mb', extended: true }));
app.use(bodyParser.json({ limit: '10mb' }));

// Initialize passport for use
app.use(passport.initialize());
// Bring in defined Passport Strategy
require('./config/passport_config')(passport);

//Authentication/Registration routes
require('./routes/auth_routes')(app);
require('./routes/producer_routes')(app);
require('./routes/project_routes')(app);
require('./routes/search_routes')(app);
require('./routes/update_routes')(app);
require('./routes/comment_routes')(app);

app.use(express.static('public'));
//app.use(express.static('userImages'));
app.use('/userImages', express.static('userImages'));
var port = process.env.PORT || config.port || 3001;

app.listen(port);
console.log("Bolt Profile app running on " + port);