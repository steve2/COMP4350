import hashlib
import json
import uuid

from flask import jsonify, redirect
from flask import request, session

from backend import app

def hash_password(password):
    salt = "3644eec10beb8c22" # super secret, you guys
    return hashlib.sha512(password + salt).hexdigest()

@app.route('/newAccount', methods=['POST', 'GET'])
def handle_new_account():
    data = request.json
    name = data['user']
    password = data['password']
    password_hash = hash_password(password)
	
    database.create_player(name, password_hash)
	
    session['username'] = name
    result = {'result': True}
    return jsonify(result)

@app.route('/loginRequest', methods = ['POST', 'GET'])
def handle_login_request():
	data = request.json
	name = data['user']
	password = data['password']
	password_hash = hash_password(password)
	
	loginPlayer = database.get_player(name, password_hash)
	print (name, " ", password_hash)
	print (loginPlayer)
    
	if (loginPlayer != None):
		session['username'] = name
		result = {'result': True}
	else:
		result = {'result': False}
	return jsonify(result)

SHOP = -1 #Character ID for SHOP
@app.route('/useRecipe', methods = ['POST', 'GET'])
def handle_use_recipe():
    data = request.json
    recipe = ['recipe']
    inChar = ['inChar']
    outChar = ['outChar']
    inItems = recipe.get_recipe_in(recipe)
    outItems = recipe.get_recipe_out(recipe)
    success = false
    #TODO: Verify the recipe is valid (Character has sufficient items in inventory)
    #success = ...
    #if(success):
        #TODO: Remove inItems from inChar (Make a common function)
        #if(inChar != SHOP):
            #loop through all items?
            #inventory.remove(inChar, item)
        #TODO: Remove outItems from outChar
        #if(inChar != SHOP):
            #loop through all items?
            #inventory.remove(outChar, item)
  
    result = { 'result' : success }
    return jsonify(result)

@app.route('/character/getAll', methods = ['POST', 'GET'])
def handle_get_characters():
    data = request.json

    if 'username' not in session:
        return redirect('/login')

    username = session['username']

    result = {'characters': database.get_characters(username)}
    return jsonify(result)

@app.route('/character/create', methods = ['POST', 'GET'])
def handle_create_character():
    data = request.json

    if 'username' not in session:
        return redirect('/login')

    username = session['username']
    charname = data['charname']

    result = database.create_character(username, charname)
    return jsonify(result)

