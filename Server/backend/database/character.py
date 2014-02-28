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
import database
import sys



#
# Character Queries
#
# When querying characters all we've got is the User/Player's name that is
# currently logged into the system. Because of this, we need to query the Player's
# ID and then cross-reference that ID with the other tables.
#
#

def get_player_id(username):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Player.ID FROM Player WHERE Username="+database.INSERT_SYM
    c.execute(qry, (username,))
    result = c.fetchone()
    if (result != None):
        result = result[0]
    else:
        result = None
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
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT * FROM `Character` WHERE ID=" + database.INSERT_SYM
    c.execute(qry, (id,))
    result = c.fetchone()
    return result
	
#
# get_characters ()       
#	@username:	user-name/account-name to retrieve characters for.
# 	@return:	returns list of characters currently owned by specified player.
#
def get_characters(username):
    db = database.db_connect()
    c = db.cursor()
    id = get_player_id(username)
    if (id != None):
        qry = "SELECT * FROM `Character` WHERE Player_ID=" + database.INSERT_SYM
        c.execute(qry, (id,))
        result = []
        for row in c:
            result.append(row)
    else:
        result = None
    return result
	

def create_character(username, charname):
    db = database.db_connect()
    c = db.cursor()
    id = get_player_id(username)
    if (id != None):
        qry = "INSERT INTO `Character` (Player_ID, Name) VALUES ("+database.INSERT_SYM+", "+database.INSERT_SYM+")"
        c.execute(qry, (id, charname))
        db.commit()
        success = True
    else:
        success = False
    return success

def reset_tables():
    print "> Reset Character Table"
    reset_characters()
	
def reset_characters():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS `Character` (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Player_ID INTEGER NOT NULL, Name CHAR(64), Exp INT DEFAULT 0, Play_Time INT DEFAULT 0)")
    c.execute("DELETE FROM `Character`")
    db.commit()
	
if __name__ == '__main__':
    if ("-reset" in sys.argv):
        reset_tables()
      
