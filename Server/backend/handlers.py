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
    
def use_recipe(recipe, inChar, outChar):
    pass
    #this function didn't compile for me.
    
@app.route('/useRecipe', methods = ['POST', 'GET'])
def handle_use_recipe():
    data = request.json
    recipe = ['recipe']
    inChar = ['inChar']
    outChar = ['outChar']
    success = use_recipe(recipe, inChar, outChar)
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

@app.route('/isAlive', methods = ['POST', 'GET'])
def handle_is_alive():
    #TODO: Should we just accept an empty request, or expect something?
    response = {"result" : "1"}
    return jsonify(response)
    

