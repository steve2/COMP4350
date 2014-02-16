import json

from flask import jsonify
from flask import request

from backend import app
from store import Store
import player

store = Store()

@app.route('/json', methods=['POST', 'GET'])
def handle_json():
    print "Json:", request.json
    print "method:", request.method
    data = request.json
    result = {'result': data['y'] + 3}
    print "Result:", result['result']
    return jsonify(result)


@app.route('/newAccount', methods=['POST', 'GET'])
def handle_account():
    data = request.json

    name = data['name']
    password_hash = data['password']

    store.create_player(name, password_hash)

    session['username'] = name

    result = {'result': True}
    return jsonify(result)

@app.route('/login', methods = ['POST', 'GET'])
def login():
    data = request.json

    name = data['name']
    password_hash = data['password_hash']

    if store.has_player(name, password_hash):
        session['username'] = name
        return render_template('___')
    else:
        return render_template('login_failed.html')

@app.route('/account_details', methods=['POST', 'GET'])
def account_details():
       pass
