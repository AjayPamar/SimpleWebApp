const sqlite3 = require('sqlite3').verbose();

//-------DB query-------
exports.select_all = 'Select * from user';
exports.insert = 'insert into user (Name,Phone,Email,Role,Password) values (?,?,?,?,?)'
exports.select_user = 'select * from user where id=?;'
exports.Update_user = 'update user set Name=? , Phone=? , Email=? where Id=?;'
exports.delete_query = 'delete from user where id = ?';
exports.Check_Mail_Exists = 'select * from user where Email=?'

//Open a DB
exports.OpenDB = function (DBPath) {
	return new sqlite3.Database(DBPath, sqlite3.OPEN_READWRITE, (err) => {
		if (err)
			console.log(err);
		else {
			console.log('Connected to DB');
		}
	})
}

//---Insert Record-----
exports.CreateNewRecord = function (db, query, params) {
	db.run(query, params, function (err) {
		if (err)
			console.log(err.message);
		else
			console.log("New user Added");
	});
}

//---Update Record------
exports.UpdateRecord = function (db, query, params) {
	db.run(query, params, function (err) {
		if (err)
			console.log(err.message);
		else
			console.log("User Updated");
	});
}

//---Delete Record-----
exports.deleteFromDB = function (db, query, params) {
	db.run(query, params, function (err) {
		if (err)
			console.log(err.message);
		else
			console.log("User Deleted");
	});
}

//------Get a single record--------
exports.GetRecord = function (db, query, params) {
	return new Promise(function (resolve, reject) {
		db.all(query, params, function (err, data) {
			if (err)
				reject(new Error("Error"));
			else {
				console.log("User Data is showen");
				resolve(data);
			}
		});
	});
}

//----Get all records from DB-------
exports.GetAllRecord = function (db, query) {
	return new Promise(function (resolve, reject) {
		db.all(query, function (err, data) {
			if (err)
				reject(new Error("Error"));
			else {
				console.log("User Data is showen");
				resolve(data);
			}
		});

	});
}

//----Close DB---
exports.CloseDB = function (db) {
	db.close((err) => {
		if (err)
			return console.log(err);
		else
			console.log('DB closed');
	});
}

//----Check existance of mail---
exports.CheckMailExists = function (db, query, params) {
	return new Promise(function (resolve, reject) {
		db.all(query, params, function (err, data) {
			if (err) {
				reject(new Error("Error"));
			}
			else {
				console.log("Email existance chek");
				console.log(data)
				resolve(data);
			}
		});
	});
}
