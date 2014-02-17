import hashlib
import json
import uuid

from flask import jsonify, redirect
from flask import request, session

from backend import app
from model import Model

model = Model()

@app.route('/json', methods=['POST', 'GET'])
def handle_json():
    print "Json:", request.json
    print "method:", request.method
    data = request.json
    result = {'result': data['y'] + 3}
    print "Result:", result['result']
    return jsonify(result)

def hash_password(password):
    salt = "3644eec10beb8c22" # super secret, you guys
    return hashlib.sha512(password + salt).hexdigest()

@app.route('/newAccount', methods=['POST', 'GET'])
def handle_new_account():
    data = request.json

    name = data['user']
    password = data['password']
    password_hash = hash_password(password)

    model.create_player(name, password_hash)

    session['username'] = name

    result = {'result': True}
    return jsonify(result)

@app.route('/loginRequest', methods = ['POST', 'GET'])
def handle_login_request():
    data = request.json
    name = data['user']
    password = data['password']
    password_hash = hash_password(password)

    print "Have:", name, password_hash
    print "Players:", model.players
    for player in model.players:
        print "  '" + player.name + ":", player.password_hash

    if model.has_player(name, password_hash):
        session['username'] = name
        result = {'result': True}
    else:
        result = {'result': False}
    return jsonify(result)

@app.route('/accountDetails', methods=['POST', 'GET'])
def account_details():
       pass

