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
from database import *



#
# Character Queries
#
# When querying characters all we've got is the User/Player's name that is
# currently logged into the system. Because of this, we need to query the Player's
# ID and then cross-reference that ID with the other tables.
#
#

def get_player_id(username):
    db = db_connect()
    c = db.cursor()
    qry = "SELECT Player.ID FROM Player WHERE Username="+INSERT_SYM
    c.execute(qry, (username,))
    result = c.fetchone()
    if (result != None):
        result = result[0]
    else:
        result = None
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
    qry = "SELECT * FROM `Character` WHERE ID=" + INSERT_SYM
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
    if (id != None):
        qry = "SELECT * FROM `Character` WHERE Player_ID=" + INSERT_SYM
        c.execute(qry, (id,))
        result = []
        for row in c:
            result.append(row)
    else:
        result = None
    db.close()
    return result
	

def create_character(username, charname):
    db = db_connect()
    c = db.cursor()
    id = get_player_id(username)
    if (id != None):
        qry = "INSERT INTO `Character` (Player_ID, Name) VALUES ("+INSERT_SYM+", "+INSERT_SYM+")"
        c.execute(qry, (id, charname))
        db.commit()
        success = True
    else:
        success = False
    db.close()
    return success

def reset_tables():
    print "> Reset Character Table"
    reset_characters()
	
def reset_characters():
    db = db_connect()
    c = db.cursor()
    c.execute("DELETE FROM `Character`")
#   c.execute("DROP TABLE IF EXISTS `Character`")
#   c.execute("CREATE TABLE `Character` (ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT, Player_ID INT NOT NULL, Name CHAR(64), Exp INT DEFAULT 0, Play_Time INT DEFAULT 0)")
    db.commit()
    db.close()
	
if __name__ == '__main__':
    if ("-reset" in sys.argv):
        reset_tables()
        
    if ("-test" in sys.argv):
    
        print "Test get_player_id(username).."
        get_player_id("Test\Username")
        print "\t...Success."
        
        print "Test get_player_id(username).."
        get_character(0)
        print "\t...Success."
        
        print "Test get_character(id).."
        get_characters("Test\Username")
        print "\t...Success."
        
        print "Test get_characters(username).."
        create_character("Test\Username", "Test\CharacterName")
        print "\t...Success."
        
        print "Testing 'character.py' Complete."
        
