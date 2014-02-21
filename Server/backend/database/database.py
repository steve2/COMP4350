#===========================================================================
# database.py
# 
# Notes:
#	- This script has been broken up into more specific scripts
#	- We are no longer using this.
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
# Database Functions	
#***************************************************************************
	
def db_connect():
    if "-local" in sys.argv:
        db = sqlite3.connect("local.db")
    else:
        db = MySQLdb.connect(HOST_NAME, USER_NAME, USER_PASS, TABL_NAME)
    return db



def reset_tables():
	print "Resetting Database Tables.."
	print "> Reset Player Table:"
	reset_players()
	print "> Reset Character Table:"
	reset_characters()
	
def reset_players():
	db = db_connect()
	c = db.cursor()
	c.execute("DELETE FROM Player")
# ! -- DELETE FROM Player will preserve the schema (primary key, auto_increment, etc.)
#	c.execute('''DROP TABLE IF EXISTS Player''')
#	c.execute('''CREATE TABLE Player (Username text, Password text)''')
	db.commit()
	db.close()
	
def reset_characters():
	db = db_connect()
	c = db.cursor()
	c.execute("DELETE FROM `Character`")
	db.commit()
	db.close()
	

#***************************************************************************
# Main Program ("python database.py")
#***************************************************************************
if __name__ == '__main__':
	reset_tables()

	

