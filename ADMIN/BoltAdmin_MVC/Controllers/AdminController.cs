using BoltAdmin_MVC.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoltAdmin_MVC.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult login()
        {
            try
            {
               
                MongoConnectionBE con = CreateConnection();
                if (!con._db.CollectionExists("admin"))
                {
                    string[] logindetails = ConfigurationManager.AppSettings["createAdminForFirstTime"].Split(',');
                    //if admin collection do not exists, read the value from appsettings ["createAdminForFirstTime"] for username,password and create admin collection with this username password.
                    if (logindetails.Length > 0)
                    {
                        List<AdminLoginBE> listUser = new List<AdminLoginBE>();
                        var documents = new BsonDocument[logindetails.Length];
                        foreach (var collection in logindetails)
                        {
                            AdminLoginBE UserBE = new AdminLoginBE();
                            UserBE._id = new ObjectId(ObjectId.GenerateNewId().ToString());
                            UserBE.firstName = "";
                            UserBE.lastName = "";
                            UserBE.email = collection.Trim();
                            UserBE.password = collection.Trim();
                            listUser.Add(UserBE);
                        }
                        var contents = con._db.GetCollection<AdminLoginBE>("admin");
                        contents.InsertBatch(listUser);
                    }
                }
              
               //string[] logdetails = ConfigurationManager.AppSettings["insert"].Split(',');
              

                return View();
            }
            catch(Exception ex)
            {
                Errorlog("Login" +":"+ ex);
                return View();
            }
        }

        public void  Errorlog(string Message)
        {
            MongoConnectionBE con = CreateConnection();
            MongoCollection<BsonDocument> logsdetail = con._db.GetCollection<BsonDocument>("insertLogs");
            List<Insertlogs> listlogs = new List<Insertlogs>();
            Insertlogs logs = new Insertlogs();
            logs.message = Message;
            logs.Date = DateTime.UtcNow;
            listlogs.Add(logs);
            var contents = con._db.GetCollection<Insertlogs>("insertLogs");
            contents.InsertBatch(listlogs);
        }
        public ActionResult logout()
        {
            try
            {
                Session["UserEmail"] = null;
                return RedirectToAction("login");
            }
            catch (Exception ex)
            {
                Errorlog("logout" + ":" + ex);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(AdminLoginBE obj)
        {
          
            try
            
            {
                if (ModelState.IsValid)
                {
                    MongoConnectionBE con = CreateConnection();
                    List<AdminLoginBE> userdetail = new List<AdminLoginBE>();
                    List<AdminLoginBE> userdetails = con._db.GetCollection<AdminLoginBE>("admin").FindAll().ToList();
                    var employees = userdetails.AsQueryable().Where(c => c.email == obj.email && c.password == obj.password);
                    if (employees.Count() != 0)
                    {
                        Session["UserEmail"] = obj.email.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login failed. Check your login details.");
                    }
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                Errorlog("Login" + ":" + ex);
                return View();
            }
        }


        [ValidateLogin]
        public ActionResult Index()
        {
            try
            {
                List<UserBE> usersBE = new List<UserBE>();
                MongoConnectionBE con = CreateConnection();
                List<UserBE> userBE = con._db.GetCollection<UserBE>("users").FindAll().ToList();
                return View(userBE);
            }
            catch (Exception ex)
            {
                Errorlog("Index" + ":" + ex);
                return View();
            }

        }
        [ValidateLogin]
        public ActionResult DisplayComments(string _id)
        {
            try
            {
                ViewBag.ID = _id.ToString();
                MongoConnectionBE con = CreateConnection();
                var query_id = Query.EQ("owner", ObjectId.Parse(_id));
                DisplayProducers Displaycomments = con._db.GetCollection<DisplayProducers>("producers").FindOne(query_id);
                //var employees = Comments.AsQueryable().Where(c => c.owner == ObjectId.Parse(_id));
                var query = Query.EQ("_id", ObjectId.Parse(_id));
                UserBE UserName = con._db.GetCollection<UserBE>("users").FindOne(query);
                ViewBag.UserName = UserName.firstName + "" + UserName.lastName;
                if (Displaycomments != null)
                {
                    // GetFilteredListQuery----------------------
                    List<BsonValue> ids = new List<BsonValue>();
                    List<IMongoQuery> queryList = new List<IMongoQuery>();
                    foreach (var items in Displaycomments.comments)
                    {
                        ids.Add(new ObjectId(items.ToString()));
                    }

                    queryList.Add(Query.In("_id", ids));

                    var finalQuery = Query.And(queryList.ToArray());

                    List<Comments> usercomments = con._db.GetCollection<Comments>("comments").Find(finalQuery).ToList();
                    return View(usercomments);
                }
                else
                {
                    return View();
                }
                ViewBag.ID = _id;
            }
            catch (Exception ex)
            {
                Errorlog("DisplayComments" + ":" + ex);
                return View();
            }
        }
        [ValidateLogin]
        public ActionResult DisplayProjects(string _id)
        {
           
            try
            {
                ViewBag.ID = _id.ToString();
                MongoConnectionBE con = CreateConnection();
                var query_id = Query.EQ("owner", ObjectId.Parse(_id));
                DisplayProducers Displaycomments = con._db.GetCollection<DisplayProducers>("producers").FindOne(query_id);
                var query = Query.EQ("_id", ObjectId.Parse(_id));
                UserBE UserName = con._db.GetCollection<UserBE>("users").FindOne(query);
                ViewBag.UserName = (UserName.firstName + "" + UserName.lastName).ToString();
                if (Displaycomments != null)
                {
                    // GetFilteredListQuery----------------------
                    List<BsonValue> ids = new List<BsonValue>();
                    List<IMongoQuery> queryList = new List<IMongoQuery>();
                    foreach (var items in Displaycomments.projects)
                    {
                        ids.Add(new ObjectId(items.ToString()));
                    }

                    queryList.Add(Query.In("_id", ids));

                    var finalQuery = Query.And(queryList.ToArray());

                    List<Projects> userprojects = con._db.GetCollection<Projects>("projects").Find(finalQuery).ToList();
                    return View(userprojects);
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                Errorlog("DisplayProjects" + ":" + ex);
                return View();
            }
        }
        [ValidateLogin]
        public ActionResult DisplayProfile(string _id)
        {
            try
            {
                ViewBag.ID = _id.ToString();
                MongoConnectionBE con = CreateConnection();
                var query_id = Query.EQ("owner", ObjectId.Parse(_id));
                var query = Query.EQ("_id", ObjectId.Parse(_id));
                UserBE userBE = con._db.GetCollection<UserBE>("users").FindOne(query);
                ViewBag.UserName = (userBE.firstName + "" + userBE.lastName).ToString();
                DisplayProducers Producers = con._db.GetCollection<DisplayProducers>("producers").FindOne(query_id);
                return View(Producers);
            }
            catch (Exception ex)
            {
                Errorlog("DisplayProfile" + ":" + ex);
                return View();
            }

        }

        [ValidateLogin]
        public ActionResult DisplayUpdates(string _id)
        {
           
            try
            {
                ViewBag.ID = _id.ToString();
                MongoConnectionBE con = CreateConnection();
                var query_id = Query.EQ("owner", ObjectId.Parse(_id));
                DisplayProducers Displaycomments = con._db.GetCollection<DisplayProducers>("producers").FindOne(query_id);

                var query = Query.EQ("_id", ObjectId.Parse(_id));
                UserBE UserName = con._db.GetCollection<UserBE>("users").FindOne(query);
                ViewBag.UserName = UserName.firstName + "" + UserName.lastName;
                //var employees = Comments.AsQueryable().Where(c => c.owner == ObjectId.Parse(_id));
                if (Displaycomments != null)
                {
                    // GetFilteredListQuery----------------------
                    List<BsonValue> ids = new List<BsonValue>();
                    List<IMongoQuery> queryList = new List<IMongoQuery>();
                    foreach (var items in Displaycomments.updates)
                    {
                        ids.Add(new ObjectId(items.ToString()));
                    }

                    queryList.Add(Query.In("_id", ids));

                    var finalQuery = Query.And(queryList.ToArray());

                    List<Updates> usercomments = con._db.GetCollection<Updates>("updates").Find(finalQuery).ToList();
                    return View(usercomments);
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                Errorlog("DisplayUpdates" + ":" + ex);
                return View();
            }
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Edit/5



        [ValidateLogin]
        public ActionResult Edit(string _id)
        {
          
            try
            {
                ViewBag.ID = _id.ToString();
                MongoConnectionBE con = CreateConnection();
                var query_id = Query.EQ("_id", ObjectId.Parse(_id));
                UserBE userBE = con._db.GetCollection<UserBE>("users").FindOne(query_id);
                userBE.password = "";
                if (userBE.uploads.Count() > 0)
                {
                    // GetFilteredListQuery----------------------
                    List<BsonValue> ids = new List<BsonValue>();
                    List<IMongoQuery> queryList = new List<IMongoQuery>();
                    foreach (var items in userBE.uploads)
                    {
                        ids.Add(new ObjectId(items.ToString()));
                    }

                    queryList.Add(Query.In("_id", ids));

                    var finalQuery = Query.And(queryList.ToArray());
                    // ----------------------
                    List<UserUploadsBE> useruploadsBE = con._db.GetCollection<UserUploadsBE>("useruploads").Find(finalQuery).ToList();
                    userBE.userUploads = (useruploadsBE.ToList());
                }
                return View(userBE);
            }
            catch (Exception ex)
            {
                Errorlog("Edit" + ":" + ex);
                return View();
            }
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection form, String _id)
        {
           
            try
            {
                // TODO: Add update logic here
                MongoConnectionBE con = CreateConnection();
                var query_id = Query.EQ("_id", ObjectId.Parse(_id));
                UserBE UserName = con._db.GetCollection<UserBE>("users").FindOne(query_id);
                string myPassword = form["NPWD"].ToString();
                string ConfrmPassword = form["CPWD"].ToString();
                string encryptpassword = Utility.Encryptpassword(myPassword);
                IMongoUpdate updateQuery = Update.Set("firstName",
                form["firstName"].ToString()).Set("lastName", form["lastName"].ToString()).Set("password", encryptpassword).Set("email", form["email"].ToString()).Set("accountType", form["accountType"].ToString());
                con._db.GetCollection("users").Update(query_id, updateQuery);
                //bool checkpwd = Utility.CheckPassword(myPassword, encryptpassword);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog("Edit" + ":" + ex);
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(string _id)
        {
            
            try
            {
                MongoConnectionBE con = CreateConnection();
                MongoCollection<UserBE> collection = con._db.GetCollection<UserBE>("users");
                MongoCollection<Uploads> UserUploads = con._db.GetCollection<Uploads>("useruploads");
                IMongoQuery query = Query.EQ("_id", ObjectId.Parse(_id));
                UserBE Userdetail = con._db.GetCollection<UserBE>("users").FindOne(query);
                List<BsonValue> UserUploadIds = new List<BsonValue>();
                List<IMongoQuery> queryListforUserUploads = new List<IMongoQuery>();
                foreach (var items in Userdetail.uploads)
                {
                    UserUploadIds.Add(new ObjectId(items.ToString()));
                }
                queryListforUserUploads.Add(Query.In("_id", UserUploadIds));
                var finalQueryforuser = Query.And(queryListforUserUploads.ToArray());
                UserUploads.Remove(finalQueryforuser);

                if (Userdetail.accountType == "Consumer")
                {
                    
                    collection.Remove(query);
                }
                else
                {
                    IMongoQuery query1 = Query.EQ("owner", ObjectId.Parse(_id));
                    DisplayProducers Displaycollection = con._db.GetCollection<DisplayProducers>("producers").FindOne(query1);
                    MongoCollection<DisplayProducers> DisplayProducers = con._db.GetCollection<DisplayProducers>("producers");
                    MongoCollection<Comments> DeleteComments = con._db.GetCollection<Comments>("comments");
                    MongoCollection<Updates> DeleteUpdates = con._db.GetCollection<Updates>("updates");
                    MongoCollection<Projects> DeleteProjects = con._db.GetCollection<Projects>("projects");
                    MongoCollection<Uploads> Uploaditems = con._db.GetCollection<Uploads>("projectuploads");
                    List<BsonValue> Commentids = new List<BsonValue>();
                    List<BsonValue> Updateids = new List<BsonValue>();
                    List<BsonValue> Projectids = new List<BsonValue>();
                    List<BsonValue> Projectuploadids = new List<BsonValue>();
                    List<IMongoQuery> queryListforComments = new List<IMongoQuery>();
                    List<IMongoQuery> queryListforUpdates = new List<IMongoQuery>();
                    List<IMongoQuery> queryListforProjects = new List<IMongoQuery>();
                    List<IMongoQuery> queryListforProjectsuploads = new List<IMongoQuery>();

                    if (Displaycollection != null)
                    {
                        foreach (var items in Displaycollection.comments)
                        {
                            Commentids.Add(new ObjectId(items.ToString()));
                        }
                        queryListforComments.Add(Query.In("_id", Commentids));
                        var finalQuery = Query.And(queryListforComments.ToArray());
                        DeleteComments.Remove(finalQuery);

                        foreach (var items in Displaycollection.updates)
                        {
                            Updateids.Add(new ObjectId(items.ToString()));
                        }

                        queryListforUpdates.Add(Query.In("_id", Updateids));
                        var finalQuery2 = Query.And(queryListforUpdates.ToArray());
                        DeleteUpdates.Remove(finalQuery2);

                        foreach (var items in Displaycollection.projects)
                        {
                            Projectids.Add(new ObjectId(items.ToString()));
                        }
                        queryListforProjects.Add(Query.In("_id", Projectids));
                        var finalQuery3 = Query.And(queryListforProjects.ToArray());
                        Projects Pro = con._db.GetCollection<Projects>("projects").FindOne(finalQuery3);
                        if (Pro != null)
                        {
                            foreach (var items in Pro.uploads)
                            {
                                Projectuploadids.Add(new ObjectId(items.ToString()));
                            }

                            queryListforProjectsuploads.Add(Query.In("_id", Projectuploadids));
                            var finalQuery4 = Query.And(queryListforProjectsuploads.ToArray());
                            Uploaditems.Remove(finalQuery4);
                        }
                        DeleteProjects.Remove(finalQuery3);
                        DisplayProducers.Remove(query1);
                    }
                    collection.Remove(query);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog("Delete" + ":" + ex);
                return View();
            }
        }

        public ActionResult DeleteComments(string _id)
        {
            
            try
            {
                MongoConnectionBE con = CreateConnection();
                MongoCollection<UserBE> collection = con._db.GetCollection<UserBE>("comments");
                MongoCollection<DisplayProducers> Commentscollection = con._db.GetCollection<DisplayProducers>("comments");
                IMongoQuery query = Query.EQ("comments", ObjectId.Parse(_id));
                IMongoQuery query1 = Query.EQ("_id", ObjectId.Parse(_id));
                DisplayProducers Displaycollection = con._db.GetCollection<DisplayProducers>("producers").FindOne(query);

                var array = new BsonArray();
                foreach (var items in Displaycollection.comments)
                {
                    if (items != ObjectId.Parse(_id))
                    {
                        array.Add(new ObjectId(items.ToString()));
                    }
                }
                var updateCommand = Update.Set("comments", array);
                var updResult = con._db.GetCollection("producers").Update(query, updateCommand);

                Commentscollection.Remove(query1);
                //IMongoUpdate update = Update.Set("comments", ObjectId.Parse(_id));
                //Commentscollection.Update(query, update);
                //collection.Remove(query);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog("DeleteComments" + ":" + ex);
                return View();
            }
        }

        public ActionResult DeleteUpdates(string _id)
        {
           
            try
            {
                MongoConnectionBE con = CreateConnection();
                MongoCollection<DisplayProducers> Commentscollection = con._db.GetCollection<DisplayProducers>("updates");
                IMongoQuery query = Query.EQ("updates", ObjectId.Parse(_id));
                IMongoQuery query1 = Query.EQ("_id", ObjectId.Parse(_id));
                DisplayProducers Displaycollection = con._db.GetCollection<DisplayProducers>("producers").FindOne(query);

                var array = new BsonArray();
                foreach (var items in Displaycollection.updates)
                {
                    if (items != ObjectId.Parse(_id))
                    {
                        array.Add(new ObjectId(items.ToString()));
                    }
                }
                var updateCommand = Update.Set("updates", array);
                var updResult = con._db.GetCollection("producers").Update(query, updateCommand);

                Commentscollection.Remove(query1);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog("DeleteUpdates" + ":" + ex);
                return View();
            }
        }

        [ValidateLogin]
        public ActionResult DeleteProjects(string _id)
        {
          
            try
            {
                MongoConnectionBE con = CreateConnection();
                //MongoCollection<UserBE> collection = _db.GetCollection<UserBE>("updates");
                MongoCollection<DisplayProducers> Commentscollection = con._db.GetCollection<DisplayProducers>("projects");
                MongoCollection<Uploads> Uploaditems = con._db.GetCollection<Uploads>("projectuploads");
                MongoCollection<Projects> Projects = con._db.GetCollection<Projects>("projects");
                IMongoQuery query = Query.EQ("projects", ObjectId.Parse(_id));
                IMongoQuery query1 = Query.EQ("_id", ObjectId.Parse(_id));
                DisplayProducers Displaycollection = con._db.GetCollection<DisplayProducers>("producers").FindOne(query);

                var array = new BsonArray();
                var array2 = new BsonArray();
                List<BsonValue> ids = new List<BsonValue>();
                List<IMongoQuery> queryList = new List<IMongoQuery>();
                foreach (var items in Displaycollection.projects)
                {
                    if (items != ObjectId.Parse(_id))
                    {
                        array.Add(new ObjectId(items.ToString()));
                    }
                }

                var updateCommand = Update.Set("projects", array);
                var updResult = con._db.GetCollection("producers").Update(query, updateCommand);
                IMongoQuery query3 = Query.EQ("_id", ObjectId.Parse(_id));
                Projects Pro = con._db.GetCollection<Projects>("projects").FindOne(query3);
                if (Pro != null)
                {
                    foreach (var items in Pro.uploads)
                    {
                        ids.Add(new ObjectId(items.ToString()));
                    }

                    queryList.Add(Query.In("_id", ids));
                    var finalQuery = Query.And(queryList.ToArray());
                    Uploaditems.Remove(finalQuery);
                }
                Projects.Remove(query3);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog("DeleteProjects" + ":" + ex);
                return View();
            }
        }

        [ValidateLogin]
        [HttpGet]
        public ActionResult ManageContent(string _id)
        {
         
            try
            {
                MongoConnectionBE con = CreateConnection();
                if (!con._db.CollectionExists("contents"))
                {
                    string[] listDefaultPage = ConfigurationManager.AppSettings["listContentPages"].Split(',');
                    if (listDefaultPage.Length > 0)
                    {
                        List<Content> listContentBE = new List<Content>();

                        var documents = new BsonDocument[listDefaultPage.Length];

                        foreach (var collection in listDefaultPage)
                        {
                            Content contentBE = new Content();
                            contentBE._id = new ObjectId(ObjectId.GenerateNewId().ToString());
                            contentBE.pageTitle = collection.Trim();
                            contentBE.pageVanityUrl = collection.Trim().ToLower();
                            contentBE.pageContent = " ";
                            contentBE.created = DateTime.UtcNow;
                            contentBE.modified = DateTime.UtcNow;
                            listContentBE.Add(contentBE);
                        }

                        var contents = con._db.GetCollection<Content>("contents");
                        contents.InsertBatch(listContentBE);
                    }
                }
                if (!String.IsNullOrEmpty(_id))
                {

                    IMongoQuery query = Query.EQ("_id", ObjectId.Parse(_id));
                    Content DisplayContent = con._db.GetCollection<Content>("contents").FindOne(query);
                    return Json(DisplayContent, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    listPages();
                    return View();
                }
            }
            catch (Exception ex)
            {
                Errorlog("ManageContent" + ":" + ex);
                return View();
            }

        }

        protected void listPages()
        {
          
            try
            {
                MongoConnectionBE con = CreateConnection();
                List<ListPagesBE> listPagesBE = con._db.GetCollection<ListPagesBE>("contents").FindAll().SetFields(Fields.Include("_id").Include("pageVanityUrl")).ToList();

                ViewData["listPages"] = listPagesBE.ToJson().ToString();
            }
            catch (Exception ex)
            {
                Errorlog("listPages" + ":" + ex);
            }
        }

        [ValidateLogin]
        [ValidateInput(false)]
        public ActionResult ManageContent(Content model, string selectPage)
        {
            try
            {
                MongoConnectionBE con = CreateConnection();
                IMongoQuery query = Query.EQ("_id", ObjectId.Parse(selectPage));
                Content DisplayContent = con._db.GetCollection<Content>("contents").FindOne(query);
                IMongoUpdate updateQuery = Update.Set("pageTitle",
                model.pageTitle).Set("pageVanityUrl", model.pageVanityUrl).Set("pageContent", model.pageContent).Set("modified", DateTime.UtcNow);
                con._db.GetCollection("contents").Update(query, updateQuery);
                ViewData["displayMessage"] = (JsonConvert.SerializeObject(new ResponseMessageBE { responseCode = 200, message = "Content updated successfully" }));
                return View();
            }
            catch (Exception ex)
            {
                ViewData["displayMessage"] = (JsonConvert.SerializeObject(new ResponseMessageBE { responseCode = 318, message = "Error updating content" }));
                Errorlog("ManageContent" + ":" + ex);
                return View();
            }
            finally
            {
                listPages();
            }
        }

        public MongoConnectionBE CreateConnection()
        {
            MongoConnectionBE newConnection = new MongoConnectionBE();
            try
            {
                
                newConnection._client = new MongoClient(ConfigurationManager.AppSettings["connectionString"]);
                newConnection._server = newConnection._client.GetServer();
                newConnection._db = newConnection._server.GetDatabase(ConfigurationManager.AppSettings["db"]);
                return newConnection;
            }
            catch (Exception ex)
            {
                Errorlog("CreateConnection" + ":" + ex);
                return newConnection;
            }
        }
    }
}
