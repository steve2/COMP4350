from flask import render_template

from backend import app

@app.route('/')
def homepage():
    return render_template('index.html')

@app.route('/account')
def account():
    return render_template('account.html')

@app.route('/strap')
def strap():
    return render_template('bootstrap.html')
