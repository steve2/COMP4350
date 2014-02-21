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
	
def print_rewards():
    db = db_connect()
    c = db.cursor()
    c.execute("SELECT * FROM Reward")
    result = c.fetchone()
    while (result != None):
        print "- ", result, "\n"
        result = c.fetchone()
    print "----\n"
    db.close()

def reset_tables():
	reset_rewardItem()
	reset_reward()
	
def reset_reward():
    db = db_connect()
    c = db.cursor()
    c.executescript('''DROP TABLE IF EXISTS Reward''')
    c.execute('''CREATE TABLE Reward (ID INT NOT NULL PRIMARY KEY, Exp INT)''')
    db.commit()
    db.close()
	
def reset_reward_item():
    db = db_connect()
    c = db.cursor()
    c.executescript('''DROP TABLE IF EXISTS Reward_Item''')
    c.execute('''CREATE TABLE Reward_Item (Reward_ID INT NOT NULL, Item_ID INT NOT NULL, Quantity INT)''')
    db.commit()
    db.close()
	
if __name__ == '__main__':
	reset_tables()