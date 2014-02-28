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
import database
import sys

def get_player(username, password_hash):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT * FROM Player WHERE Username="+database.INSERT_SYM+" AND Password="+database.INSERT_SYM
    c.execute(qry, (username, password_hash))
    result = c.fetchone()
    return result

def create_player(username, password_hash):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO Player (Username, Password) VALUES ("+database.INSERT_SYM+", "+database.INSERT_SYM+")"
    try:
        c.execute(qry, (username, password_hash))
        db.commit()
        result = True
    except:
        result = False
    return result

def reset_tables():
    print "> Reset Player Table"
    reset_players()
	
def reset_players():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS Player (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Username CHAR(255) NOT NULL UNIQUE, Password CHAR(255) NOT NULL)")
    c.execute("DELETE FROM Player")
    #c.execute("DROP TABLE IF EXISTS Player")
    #c.execute("CREATE TABLE Player (ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT, Username CHAR(255) NOT NULL, Password CHAR(255) NOT NULL)")
    
    db.commit()
	
if __name__ == '__main__':
    if ("-reset" in sys.argv):
        reset_tables()
        
