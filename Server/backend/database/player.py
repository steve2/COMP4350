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
    qry = "INSERT INTO Player (Username, Password) VALUES ("+INSERT_SYM+", "+INSERT_SYM+")"
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
    c.execute("DELETE FROM Player")
#   c.execute("DROP TABLE IF EXISTS Player")
#   c.execute("CREATE TABLE Player (ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT, Username CHAR(255) NOT NULL, Password CHAR(255) NOT NULL)")
    db.commit()
    db.close()
	
if __name__ == '__main__':
    if ("-reset" in sys.argv):
        reset_tables()
        
    if ("-test" in sys.argv):
        print "Test get_player(username, password).."
        get_player("Test\Username", "Test\Password")
        print "\t...Success."
        
        print "Test create_player(username, password).."
        create_player("Test\Username", "Test\Password")
        print "\t...Success."
        
        print "Testing 'player.py' Complete"
        
