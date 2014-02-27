#===========================================================================
# rewards.py
# 
# Notes:
#	- Code interacts with MySQL database.
#
#===========================================================================

#
# Dependencies
#====================
import database
import sys
	
def print_rewards():
    db = database.db_connect()
    c = db.cursor()
    c.execute("SELECT * FROM Reward")
    result = c.fetchone()
    while (result != None):
        print "- ", result, "\n"
        result = c.fetchone()
    print "----\n"
    db.close()

def reset_tables():
	print "> Reset Reward Item Table"
	reset_reward_item()
	print "> Reset Reward Table"
	reset_reward()
	
def reset_reward():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Reward")
    c.execute("CREATE TABLE Reward (ID INT NOT NULL PRIMARY KEY, Exp INT)")
    db.commit()
    db.close()
	
def reset_reward_item():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Reward_Item")
    c.execute("CREATE TABLE Reward_Item (Reward_ID INT NOT NULL, Item_ID INT NOT NULL, Quantity INT)")
    db.commit()
    db.close()
	
if __name__ == '__main__':
	if ("-reset" in sys.argv):
		reset_tables()