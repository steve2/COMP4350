import hashlib
import json
import uuid

from flask import jsonify, redirect
from flask import request, session

from backend import app

import database.database as database
import database.character as character
import database.player as player
import database.equipment as equipment
import database.item as item

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
            database.db_connect()
            player.create_player(name, password_hash)
            database.db_close()
            
            session['username'] = name
            result = {'result': True}
        except Exception, e:
            result = {'result': False}

    return jsonify(result)

@app.route('/loginRequest', methods = ['POST', 'GET'])
def handle_login_request():
    print "REQUEST"
    data = request.json
    
    if data == None or 'user' not in data or 'password' not in data:
        print "NOPE:", data
        result = {'result': False}
    else:
        print "Test:", data
        name = data['user']
        password = data['password']
        password_hash = hash_password(password)

        try:
            loginPlayer = player.get_player(name, password_hash)
        except Exception, e:
            print e
            loginPlayer = None

        print "Player:", loginPlayer
        if loginPlayer != None:
            session['username'] = name
            result = {'result': True}
        else:
            result = {'result': False}
    return jsonify(result)

@app.route('/logoutRequest', methods = ['POST', 'GET'])
def handle_logout_request():
    data = request.json
    session.clear()
    result = {'result': True}
    return jsonify(result)

@app.route('/player/current', methods = ['POST', 'GET'])
def handle_current_player():
    result = {}
    if 'username' in session:
        result["result"] = session["username"]
    else:
        result["result"] = None
    return jsonify(result)

######RECIPE######
SHOP = -1 #Character ID for SHOP
#Provides support for use/undo_recipe and trading
def exec_recipe(recipe, inChar, outChar):
    inItems = recipe.get_recipe_in(recipe)
    outItems = recipe.get_recipe_out(recipe)
    
    #Verify the recipe is valid (Character has sufficient items in inventory)
    #We don't want to remove/add anything until we know that the transaction is valid
    if inChar != SHOP and not contains_items(inChar, inItems):
       return false
    if outChar != SHOP and not contains_items(outChar, outItems):
        return false
        
    #Remove items from inventories
    if(inChar != SHOP):
        remove_items(inChar, inItems)
    if(outChar != SHOP):
        remove_items(outChar, outItems)

    #Add items to inventories
    if(inChar != SHOP):
        add_items(outChar, outItems)
    if(outChar != SHOP):
        add_items(inChar, inItems)
    return true

#Buy/Craft
@app.route('/useRecipe', methods = ['POST', 'GET'])
def handle_use_recipe():
    data = request.json
    recipe = data['recipe']
    charid = data['character']
    success = exec_recipe(recipe, charid, SHOP)
    result = { 'result' : success }
    return jsonify(result)

#Sell/Disassemble
@app.route('/undoRecipe', methods = ['POST', 'GET'])
def handle_undo_recipe():
    data = request.json
    recipe = data['recipe']
    charid = data['character']
    success = exec_recipe(recipe, SHOP, charid)
    result = { 'result' : success }
    return jsonify(result)
####################

@app.route('/character/getAll', methods = ['POST', 'GET'])
def handle_get_characters():
    data = request.json
    
    if 'username' not in session:
        return redirect('/login')

    print "Get characters request for ", session['username']
    username = session['username']

    database.db_connect()
    characters = character.get_characters(username)
    database.db_close()
    print "Characters: ", characters
    
    result = {'characters': characters}
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

    charid = data['charid'] # TODO: Make this character name?

    result = {"inventory": character.get_inventory(charId)}
    return jsonify(result)

@app.route('/character/equipped', methods = ['POST', 'GET'])
def handle_get_character_equipment():
    data = request.json

    charid = data['charid']

    result = {"equipment": equipment.get_equipment(charId)}
    return jsonify(result)

@app.route('/item/getAll', methods = ['POST', 'GET'])
def handle_get_character_equipment():
    data = request.json

    result = {"equipment": item.get_items()}
    return jsonify(result)

@app.route('/item/get', methods = ['POST', 'GET'])
def handle_get_character_equipment():
    data = request.json

    itemid = data['itemid']

    result = {"equipment": item.get_item(itemId)}
    return jsonify(result)

@app.route('/isAlive', methods = ['POST', 'GET'])
def handle_is_alive():
    #TODO: Should we just accept an empty request, or expect something?
    response = {"result" : "1"}
    return jsonify(response)
    
