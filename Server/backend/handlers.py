
import json
import database

from flask import jsonify
from flask import request

from backend import app

@app.route('/json', methods=['POST', 'GET'])
def handle_json():
	print "Request:", request, "\n"
	jsonReq = request.get_json()
	print "Get JSON:", jsonReq, "\n"
	print "JSON Elements:", jsonReq['name'], "\n"
	result = { "name" : jsonReq['name'], "password" : "password" }
	print "Result:", result, "\n"
	return jsonify(result)

