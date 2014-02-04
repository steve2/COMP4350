from flask import Flask
from flask import jsonify
from flask import request
from flask import render_template

import json

app = Flask(__name__)


@app.route('/')
def hello():
    return render_template('hello.html')

@app.route('/json', methods=['POST', 'GET'])
def handle_json():
    print "Json:", request.json
    print "method:", request.method
    data = request.json
    result = {'result': data['y'] + 2}
    print "Result:", result['result']
    return jsonify(result)

if __name__ == '__main__':

    app.debug = True
    app.run()
