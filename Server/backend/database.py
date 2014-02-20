#===========================================================================
# database.py
# 
# Notes:
#	- Code interacts with MySQL database.
#	- Returns objects in JSON format for sending to client.
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
    print "\nPython-DB Result Object\n============================="
    result = c.fetchone()
    while (result != None):
        print "- ", result, "\n"
        result = c.fetchone()
    print "----\n"
    db.close()

def reset_tables():
    db = db_connect()
    c = db.cursor()
    c.executescript('''DROP TABLE IF EXISTS Player''')
    c.execute('''CREATE TABLE Player (Username text, PasswordHash text)''')
    db.commit()
    db.close()

def create_player(username, password_hash):
    db = db_connect()
    c = db.cursor()
    c.execute("INSERT INTO Player (Username, PasswordHash) values (?, ?)", (username, password_hash))
    db.commit()
    db.close()
    return True

def get_player(username, password_hash):
    db = db_connect()
    c = db.cursor()
    c.execute("SELECT * FROM Player WHERE Username=? AND PasswordHash=?", (username, password_hash))
    result = c.fetchone()
    db.close()
    return result

if __name__ == '__main__':
    reset_tables()
