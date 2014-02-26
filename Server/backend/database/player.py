#===========================================================================
# player.py
# 
# Notes:
#	- Code interacts with MySQL database to get Player information.
#
#===========================================================================

#
# Dependencies
#====================
from database import *
from random import random
	
def print_players():
    db = db_connect()
    c = db.cursor()
    c.execute("SELECT * FROM Player")
    result = c.fetchone()
    while (result != None):
        print "- ", result, "\n"
        result = c.fetchone()
    print "----\n"
    db.close()

def get_player(username, password_hash):
	db = db_connect()
	c = db.cursor()
	qry = "SELECT * FROM Player WHERE Username=" + INSERT_SYM + " AND Password=" + INSERT_SYM
	c.execute(qry, (username, password_hash))
	result = c.fetchone()
	db.close()
	return result

def create_player(username, password_hash):
	db = db_connect()
	c = db.cursor()
	qry = "INSERT INTO Player (ID, Username, Password) VALUES (" + INSERT_SYM + ", " + INSERT_SYM + ", " + INSERT_SYM + ")"
	c.execute(qry, (username, password_hash))
	db.commit()
	db.close()
	return True

def reset_tables():
	print "> Reset Player Table"
	reset_players()
	
def reset_players():
    db = db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Player")
    autoincrement = "AUTOINCREMENT" if INSERT_SYM == '?' else "AUTO_INCREMENT"
    c.execute("CREATE TABLE Player (ID INTEGER NOT NULL PRIMARY KEY "+autoincrement+", Username CHAR(255) NOT NULL, Password CHAR(255) NOT NULL)")
    db.commit()
    db.close()
	
if __name__ == '__main__':
	if ("-reset" in sys.argv):
		reset_tables()
