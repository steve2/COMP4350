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
import database.mission as mission

def hash_password(user, password):
    salt = "3644eec10beb8c22" # super secret, you guys
    return hashlib.sha512(password + salt).hexdigest()
    
@app.route('/isAlive', methods = ['POST', 'GET'])
def handle_is_alive():
    response = {"result" : "1"}
    return jsonify(response)

@app.route('/addCookie', methods = ['POST', 'GET'])
def handle_add_cookie():
    print "Adding cookie to session:"
    session["cookie"] = "cookie"
    response = {"result" : "1"}
    print "Cookie added"
    return jsonify(response)

@app.route('/hasCookie', methods = ['POST', 'GET'])
def handle_has_cookie():
    if 'cookie' in session:
        cookie = session["cookie"]
        print "Found cookie:", cookie
        present = cookie == "cookie"
        response = {"result" : present}
    else:
        print "Cookie not present"
        response = {"result" : False}
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

_PUR_COST_SPOT = 0
_PUR_ID_SPOT = 1
_PUR_NAME_SPOT = 2

@app.route('/getPurchasables', methods = ['POST', 'GET'])
def get_purchasable_item_request():
    purchasables = recipe.get_purchasable_items();
    
    result = { 'purchasables' : [] }

    for p in purchasables:
        name = p[_PUR_NAME_SPOT];
        cost = p[_PUR_COST_SPOT];
        rid = p[_PUR_ID_SPOT];
        result['purchasables'].append({"name":name, "cost":cost, "recipe":rid})
    return jsonify(result)
    
#Buy/Craft
@app.route('/recipe/use', methods = ['POST', 'GET'])
def handle_use_recipe():
    data = request.json
    if 'username' not in session:
        result = {'recipemade' :None, 'BadRequest':True}
    else:     
        try:
                database.db_connect()
                charid = data['charid']   
                rid = data['recipe']
                success = recipe.exec_recipe(rid, charid, character.SHOP)
                result = { 'result' : success }
        except Exception, e:
                print e
                result = {"result": None}
        finally:
                database.db_close()
                    
    return jsonify(result)

#Sell/Disassemble
@app.route('/recipe/undo', methods = ['POST', 'GET'])
def handle_undo_recipe():
    data = request.json
    rid = data['recipe']
    charid = data['character']
    success = recipe.exec_recipe(rid, character.SHOP, charid)
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

#TODO: Add getQuantity

@app.route('/character/inventory/getgold', methods = ['POST', 'GET'])
def handle_get_character_inventory():
    data = request.json

    if 'charid' not in data:
        result = {"inventory": None, "BadRequest": True }
    else:
        charid = data['charid'] # TODO: Make this character name?
        try:
            database.db_connect()
            inv = inventory.get_inventory(charid)
            result = { "inventory" : [] }
            for entry in inv:
                if entry[0] in ['Gold']
                    result['inventory'].append( {"name":entry[0], "quantity":entry[1]} )
        except Exception, e:
            print e
            result = {"inventory": None}
        finally:
            database.db_close()
     
    return jsonify(result)
    
@app.route('/character/inventory/get', methods = ['POST', 'GET'])
def handle_get_character_inventory():
    data = request.json

    if 'charid' not in data:
        result = {"inventory": None, "BadRequest": True }
    else:
        charid = data['charid'] # TODO: Make this character name?
        try:
            database.db_connect()
            inv = inventory.get_inventory(charid)
            result = { "inventory" : [] }
            for entry in inv:
                result['inventory'].append( {"name":entry[0], "quantity":entry[1]} )
        except Exception, e:
            print e
            result = {"inventory": None}
        finally:
            database.db_close()
     
    return jsonify(result)

@app.route('/character/inventory/add', methods = ['POST', 'GET'])
def handle_add_character_inventory():
    data = request.json

    if 'itemid' not in data or 'quantity' not in data or 'charid' not in data:
        result = {"result": False, "BadRequest": True }
    else:
        charid = data['charid'] # TODO: Make this character name?
        itemid = data['itemid']
        quantity = data['quantity']
        try:
            database.db_connect()
            inv = inventory.add_item(charid, itemid, quantity)
            result = {"result": True}
        except Exception, e:
            print e
            result = {"result": False}
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
    
_ATTR_NAME_SPOT = 0
_ATTR_VALUE_SPOT = 1
    
def getJSONItemAttrs(itemname):
    attributes = item.get_item_attributes(itemname)
    result = []
    for attribute in attributes:
        result.append( {"name":attribute[_ATTR_NAME_SPOT], "value":attribute[_ATTR_VALUE_SPOT]} )
    return result
    
_SLOT_NAME_SPOT = 0
    
def getJSONItemSlots(itemname):
    slots = item.get_item_slots_equippable(itemname)
    result = []
    for slot in slots:
        result.append( {"name":slot[_SLOT_NAME_SPOT]} )
    return result
    
_ITEM_NAME_SPOT = 0
_ITEM_DESC_SPOT = 1
_ITEM_TYPE_SPOT = 2
    
@app.route('/item/getAll', methods = ['POST', 'GET'])
def handle_get_items():
    try:
        database.db_connect()
        items = item.get_items()
        result = { "items" : [] }
        
        for itemInList in items:
            iname = itemInList[_ITEM_NAME_SPOT]
            idesc = itemInList[_ITEM_DESC_SPOT]
            itype = itemInList[_ITEM_TYPE_SPOT]
            attrs = getJSONItemAttrs(iname)
            slots = getJSONItemSlots(iname)
            result['items'].append({"name":iname, "type":itype, "desc":idesc, "attributes":attrs, "slots":slots})
            
    except Exception, e:
        #print "Error in /item/getAll:", e
        result = { "items": None }
    finally:
        database.db_close()
        
    #print "Get All Item Request\nResult: "
    #for itemInList in result['items']:
    #    print itemInList
    #print "\n"
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
    
#===========================================================================================
#
# Mission Data
#
#===========================================================================================

@app.route('/mission/getAll', methods = ['POST', 'GET'])
def handle_get_all_missions():
    try:
        database.db_connect()
        missions = mission.get_missions()
        result = {"missions": missions}
    except Exception, e:
        print e
        result = {"missions": None}
    finally:
        database.db_close()
        
    return jsonify(result)
    
#===========================================================================================
#
# Reward Data
#
#===========================================================================================
@app.route('/reward/get', methods = ['POST', 'GET'])
def handle_get_reward():
    data = request.json
    
    if 'rewardid' not in data:
        result = {"rewardexp": None, "rewarditems": None, "BadRequest": True}
    else:
        rewardid = data['rewardid']
        
        try:
            database.db_connect()
            rewardItems = reward.get_reward_items(rewardid)
            rewardExp = reward.get_reward_exp(rewardid)
            result = {"rewardexp": rewardExp, "rewarditems": rewardItems}
        except Exception, e:
            print e
            result = {"rewards": None}
        finally:
            database.db_close()
        
    return jsonify(result)
