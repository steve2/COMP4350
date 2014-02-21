#===========================================================================
# character.py
# 
# Notes:
#	- Code interacts with MySQL database to get Character information.
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

#===================================================================================
# Character Queries
#
# When querying characters all we've got is the User/Player's name that is
# currently logged into the system. Because of this, we need to query the Player's
# ID and then cross-reference that ID with the other tables.
#
#===================================================================================
def get_player_id(username):
	db = db_connect()
	c = db.cursor()
	qry = "SELECT Player.ID FROM Player WHERE Username=%s"
	c.execute(qry, (username,))
	result = c.fetchone()[0]
	db.close()
	return result

	
#
# get_character ()
#	@id:		ID of the character to be retrieved.
#	@return:	Returns an object if the character with that ID exists.
#
# This could be used to check if the Character is in the database when we want
# to delete characters from Player accounts. 
#
def get_character(id):
	db = db_connect()
	c = db.cursor()
	qry = "SELECT * FROM `Character` WHERE ID=%s"
	c.execute(qry, (id,))
	result = c.fetchone()
	db.close()
	return result
	
#
# get_characters ()       
#	@username:	user-name/account-name to retrieve characters for.
# 	@return:	returns list of characters currently owned by specified player.
#
def get_characters(username):
	db = db_connect()
	c = db.cursor()
	id = get_player_id(username)
	qry = "SELECT * FROM `Character` WHERE Player_ID=%s"
	c.execute(qry, (id,))
	result = []
	for row in c:
		result.append(row)
	db.close()
	return result
	
def create_character(username, charname):
	db = db_connect()
	c = db.cursor()
	id = get_player_id(username)
	qry = "INSERT INTO `Character` (Player_ID, Name) VALUES (%s, %s)"
	c.execute(qry, (id, charname))
	db.commit()
	db.close()
	return True

def reset_tables():
	reset_characters()
	
def reset_characters():
	db = db_connect()
	c = db.cursor()
	c.execute("DELETE FROM `Character`")
	db.commit()
	db.close()
	
if __name__ == '__main__':
	reset_tables()