from flask import render_template

from backend import app

@app.route('/')
def homepage():
    return render_template('index.html')

@app.route('/account', methods=['POST', 'GET'])
def account():
    return render_template('account.html')
