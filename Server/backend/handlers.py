import hashlib
import json
import uuid

from flask import jsonify, redirect
from flask import request, session

from backend import app
import database.character as character
import database.player as player

def hash_password(password):
    salt = "3644eec10beb8c22" # super secret, you guys
    return hashlib.sha512(password + salt).hexdigest()

@app.route('/newAccount', methods=['POST', 'GET'])
def handle_new_account():
    data = request.json
    if data == None or 'user' not in data or 'password' not in data:
        result = {'result': False}
    else:
        name = data['user']
        password = data['password']
        password_hash = hash_password(password)
	
        try:
            player.create_player(name, password_hash)
            session['username'] = name
            result = {'result': True}
        except Exception, e:
            result = {'result': False}

    return jsonify(result)

@app.route('/loginRequest', methods = ['POST', 'GET'])
def handle_login_request():
    data = request.json
    
    if data == None or 'user' not in data or 'password' not in data:
        result = {'result': False}
    else:
        name = data['user']
        password = data['password']
        password_hash = hash_password(password)

        try:
            loginPlayer = player.get_player(name, password_hash)
        except Exception, e:
            loginPlayer = None

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

    result = {'characters': character.get_characters(username)}
    return jsonify(result)

@app.route('/character/create', methods = ['POST', 'GET'])
def handle_create_character():
    data = request.json

    if 'username' not in session:
        result = False 
    else:
        username = session['username']
        charname = data['charname']

        result = character.create_character(username, charname)
    response = {"result": result}
    return jsonify(response)

@app.route('/character/inventory', methods = ['POST', 'GET'])
def handle_get_character_inventory():
    data = request.json

    #if 'username' not in session:
    #    return redirect('/login')

    #username = session['username']
    charid = data['charid'] # TODO: Make this character name?

    result = {"inventory": character.get_inventory(charId)}
    return jsonify(result)

