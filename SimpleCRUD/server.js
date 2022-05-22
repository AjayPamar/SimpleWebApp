const express = require('express');
const app = express();
const path = require('path');
const router = express.Router();
const fs = require('fs');
const bodyParser= require('body-parser');
const DBOperations = require(path.join(__dirname, 'modules/DB/DBWithQuery'));

//__dirname : It will resolve path to your project folder.
app.set('views', path.join(__dirname, 'View'));
app.set('view engine', 'ejs');

// Make sure you place body-parser before your CRUD handlers!
app.use(bodyParser.urlencoded({ extended: true })); 
app.use(bodyParser.json());

var db = DBOperations.OpenDB(__dirname+'/modules/DB/SampleDB.db');

//---------------------------Normal CRUD------------------------------------
router.get('/', function (req, res) {
	DBOperations.GetAllRecord(db,DBOperations.select_all)
		.then(function(data){
			res.render('index',{title:'all users',records:data});
		})
		.catch(function(err){
			console.log(err);
		});
});

 app.get('/Create',function(req,res){
   res.render('Create');
 });
 
router.get('/Read/:Id',function(req,res){
	DBOperations.GetRecord(db,DBOperations.select_user,[req.params.Id])
		.then(function(data){
			res.render('Edit',{title:'User Data',records:data[0]});
		})
		.catch(function(err){
			console.log(err);
		});
});
 
 app.post('/Create',(req,res)=>{
	DBOperations.CreateNewRecord(db,DBOperations.insert,[req.body.Name,req.body.Phone,req.body.Email]);
	res.redirect('/');
 });
 
app.post('/Edit',function(req,res){
	DBOperations.UpdateRecord(db,DBOperations.Update_user,[req.body.Name,req.body.Phone,req.body.Email,req.body.Id]);
	res.redirect('/');
});

app.post('/Delete',function(req,res){
	DBOperations.deleteFromDB(db,DBOperations.delete_query,[req.body.id]);
	res.redirect('/');
});

//--------------------CRUD API---------------------
 app.get('/api',function(req,res){
   res.render('CrudWithApi');
 });
 
app.get('/api/GetAll', function (req, res) {
	DBOperations.GetAllRecord(db,DBOperations.select_all)
		.then(function(data){
			//res.render('index',{title:'all users',records:data});
			res.json(data);
		})
		.catch(function(err){
			console.log(err);
		});
});

app.get('/api/Read/:Id',function(req,res){
	DBOperations.GetRecord(db,DBOperations.select_user,[req.params.Id])
		.then(function(data){
			res.json(data);
		})
		.catch(function(err){
			console.log(err);
		});
});

app.post('/api/Delete/:Id',function(req,res){
	DBOperations.deleteFromDB(db,DBOperations.delete_query,[req.params.Id]);
	res.json({status:'success'})
	
});

 app.post('/api/Create',(req,res)=>{
	try{
		DBOperations.CreateNewRecord(db,DBOperations.insert,[req.body.Name,req.body.Phone,req.body.Email]);
		res.json({status:'success'})
	}
	catch{
		res.json({status:'failed'})
	}
 });
 
app.post('/api/Edit',function(req,res){
	try{
		DBOperations.UpdateRecord(db,DBOperations.Update_user,[req.body.Name,req.body.Phone,req.body.Email,req.body.Id]);
		res.json({status:'success'})
	}
	catch{
		res.json({status:'failed'})
	}
});

//add the router
app.use('/', router);
app.use("/modules/Scripts", express.static('./modules/Scripts/'));
app.use("/modules/css", express.static('./modules/css/'));
app.listen(process.env.port || 3000);

console.log('Running at Port 3000');