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
import database.inventory as inventory
import database.recipe as recipe
import database.achievements as achievements

def hash_password(user, password):
    salt = "3644eec10beb8c22" # super secret, you guys
    return hashlib.sha512(password + salt).hexdigest()
    
@app.route('/isAlive', methods = ['POST', 'GET'])
def handle_is_alive():
    response = {"result" : "1"}
    return jsonify(response)

#===========================================================================================
#
# PLAYER DATA 
#
#===========================================================================================
    
@app.route('/newAccount', methods=['POST', 'GET'])
def handle_new_account():
    data = request.json
    if data == None or 'user' not in data or 'password' not in data:
        result = {'result': False}
    else:
        name = data['user']
        password = data['password']
        password_hash = hash_password(name, password)

        try:
            database.db_connect()
            player.create_player(name, password_hash)
            session['username'] = name
            result = {'result': True}
        except Exception, e:
            result = {'result': False}
        finally:
            database.db_close()
            
    return jsonify(result)

@app.route('/loginRequest', methods = ['POST', 'GET'])
def handle_login_request():
    data = request.json
    
    if data == None or 'user' not in data or 'password' not in data:
        result = {'result': False}
    else:
        name = data['user']
        password = data['password']
        password_hash = hash_password(name, password)
        try:
            database.db_connect()
            loginPlayer = player.get_player(name, password_hash)
        except Exception, e:
            print e
            loginPlayer = None
        finally:
            database.db_close()
            
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

@app.route('/player/addAchievement', methods = ['POST', 'GET'])
def handle_add_player_achievement():
    if 'username' not in session or 'achievement' not in data:
        result = {'result': False, 'BadRequest':True}
    else:
        player = session['username']
        achievement = data['achievement']
        try:
            database.db_connect()
            achievements.add_player_achievement(player, achievement)
            result = {"result": True}
        except Exception, e:
            print e
            result = {"result": False}
        finally:
            database.db_close()
        
    return jsonify(result)

@app.route('/player/getAchievement', methods = ['POST', 'GET'])
def handle_get_all_player_achievements():
    if 'username' not in session:
        result = {'achievements': None, 'BadRequest': True}
    else:
        player = session['username']
        try:
            database.db_connect()
            achieves = achievements.get_player_achievements(player)
            result = {"achievements": achieves}
        except Exception, e:
            print e
            result = {"achievements": None}
        finally:
            database.db_close()
        
    return jsonify(result)
    
#===========================================================================================
#
# RECIPE DATA 
#
#===========================================================================================

@app.route('/getPurchasables', methods = ['POST', 'GET'])
def get_purchasable_item_request():
    data =  request.json
    
    purchasables = recipe.get_purchasable_items();
    result = { 'purchasables' : purchasables }
    return jsonify(result)
    
#Buy/Craft
@app.route('/recipe/use', methods = ['POST', 'GET'])
def handle_use_recipe():
    data = request.json
    recipe = data['recipe']
    charid = data['character']
    success = exec_recipe(recipe, charid, character.SHOP)
    result = { 'result' : success }
    return jsonify(result)

#Sell/Disassemble
@app.route('/recipe/undo', methods = ['POST', 'GET'])
def handle_undo_recipe():
    data = request.json
    recipe = data['recipe']
    charid = data['character']
    success = exec_recipe(recipe, character.SHOP, charid)
    result = { 'result' : success }
    return jsonify(result)


#===========================================================================================
#
# CHARACTER DATA 
#
#===========================================================================================

@app.route('/character/getAll', methods = ['POST', 'GET'])
def handle_get_characters():
    data = request.json
   
    if 'username' not in session:
        result = {'characters':None, 'BadRequest':True}
    else:
        username = session['username']
        try:
            database.db_connect()
            characters = character.get_characters(username)
            result = {'characters': characters}
        except:
            result = {'characters': None}
        finally:
            database.db_close()
            
    return jsonify(result)

@app.route('/character/create', methods = ['POST', 'GET'])
def handle_create_character():
    data = request.json

    if 'username' not in session or 'charname' not in data:
        result = False 
    else:
        username = session['username']
        charname = data['charname']
        try:
            database.db_connect()
            result = character.create_character(username, charname)
        except Exception, e:
            print "Error in /character/create:", e
            result = False
        finally:
            database.db_close()
        
    response = {"result": result}
    return jsonify(response)

@app.route('/character/inventory', methods = ['POST', 'GET'])
def handle_get_character_inventory():
    data = request.json

    if 'charId' not in data:
        result = {"inventory": None, "BadRequest": True }
    else:
        charid = data['charid'] # TODO: Make this character name?
        try:
            database.db_connect()
            inv = inventory.get_inventory(charid)
            result = {"inventory": inv}
        except Exception, e:
            print e
            result = {"inventory": None}
        finally:
            database.db_close()
    
    return jsonify(result)

@app.route('/character/equipped', methods = ['POST', 'GET'])
def handle_get_equipped_character_equipment():
    data = request.json

    if 'charid' not in data:
        result = {"equipment": None, "BadRequest": True}
    else:
        charid = data['charid']
        try:
            database.db_connect()
            eq = equipment.get_equipment(charid)
            result = {"equipment": eq}
        except Exception, e:
            print "Error in /character/equipped:", e
            result = {"equipment": None}
        finally:
            database.db_close()
            
    return jsonify(result)

#===========================================================================================
#
# ITEM DATA 
#
#===========================================================================================
    
@app.route('/item/getAll', methods = ['POST', 'GET'])
def handle_get_all_character_equipment():
    try:
        database.db_connect()
        items = item.get_items()
        result = {"equipment": items}
    except Exception, e:
        print e
        result = {"equipment": None}
    finally:
        database.db_close()
        
    return jsonify(result)

@app.route('/item/get', methods = ['POST', 'GET'])
def handle_get_character_equipment():
    data = request.json

    if 'itemid' not in data:
        result = {"equipment": None, "BadRequest": True}
    else:
        itemid = data['itemid']

        try:
            database.db_connect()
            item = item.get_item(itemId)
            result = {"equipment": item}
        except Exception, e:
            print e
            result = {"equipment": None}
        finally:
            database.db_close()

    return jsonify(result)

#===========================================================================================
#
# Achievement Data
#
#===========================================================================================

@app.route('/achievement/getAll', methods = ['POST', 'GET'])
def handle_get_all_achievements():
    try:
        database.db_connect()
        achieves = achievements.get_all_achievements()
        result = {"achievements": achieves}
    except Exception, e:
        print e
        result = {"achievements": None}
    finally:
        database.db_close()
        
    return jsonify(result)

@app.route('/achievement/description', methods = ['POST', 'GET'])
def handle_get_achievement_description():
    data = request.json

    if 'achievementName' not in data:
        result = {"description": None, "BadRequest": True}
    else:
        achievementName = data['achievementName']

        try:
            database.db_connect()
            description = achievements.get_achievement_description(achievementName)
            result = {"description": description}
        except Exception, e:
            print e
            result = {"description": None}
        finally:
            database.db_close()
        
    return jsonify(result)
    
