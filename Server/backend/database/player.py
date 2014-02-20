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
import sys
import MySQLdb
import sqlite3

#
# Constants
#============================
HOST_NAME = "localhost"
USER_NAME = "COMP4350_admin"
USER_PASS = "admin"
TABL_NAME = "COMP4350_GRP5"

#***************************************************************************
#***************************************************************************

def db_connect():
    #TODO: connect just once
    #TODO: -p should imply MySQL, otherwise sqlite for local stuff
    if "-local" in sys.argv:
        db = sqlite3.connect("local.db")
    else:
        db = MySQLdb.connect(HOST_NAME, USER_NAME, USER_PASS, TABL_NAME)
    return db
	
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
	qry = "SELECT * FROM Player WHERE Username=%s AND Password=%s"
	c.execute(qry, (username, password_hash))
	result = c.fetchone()
	db.close()
	return result

def create_player(username, password_hash):
	db = db_connect()
	c = db.cursor()
	qry = "INSERT INTO Player (Username, Password) VALUES (%s, %s)"
	c.execute(qry, (username, password_hash))
	db.commit()
	db.close()
	return True

def reset_tables():
	reset_players()
	
def reset_players():
    db = db_connect()
    c = db.cursor()
    c.executescript('''DROP TABLE IF EXISTS Player''')
    c.execute('''CREATE TABLE Player (Username text, Password text)''')
    db.commit()
    db.close()
	
if __name__ == '__main__':
	reset_tables()
