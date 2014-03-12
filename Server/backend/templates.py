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

#TODO: Make a marketplace page that contains more than just the buy/sell shop
@app.route('/marketplace')
def marketplace():
    return render_template('shop.html')

@app.route('/leaderboard')
def leaderboard():
    return render_template('leaderboard.html')

@app.route('/items')
def items():
    return render_template('items.html')
	
@app.route('/missions')
def missions():
	return render_template('missions.html')
