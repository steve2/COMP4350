from flask	import render_template
from backend import app

@app.route('/')
def homepage():
    return render_template('index.html')

@app.route('/account')
def account():
    return render_template('account.html')
	
@app.route('/login')
def login():
    return render_template('login.html')

@app.route('/signup')
def signup():
    return render_template('signup.html')

@app.route('/character/new')
def character_create():
    return render_template('characterCreate.html')


