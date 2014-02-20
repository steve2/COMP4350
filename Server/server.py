import sys

from backend import app

if __name__ == '__main__':
    app.secret_key = "extra secret key"
    app.debug = '-p' not in sys.argv
    app.run(host='0.0.0.0', port=80)
	
