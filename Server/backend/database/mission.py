#===========================================================================
# mission.py
# 
# Notes:
#	- Code interacts with MySQL database to get Mission information.
#
#===========================================================================

#
# Dependencies
#====================
import database
import sys


def print_missions():
    db = database.db_connect()
    c = db.cursor()
    c.execute("SELECT * FROM Mission")
    result = c.fetchone()
    while (result != None):
        print "- ", result, "\n"
        result = c.fetchone()
    print "----\n"

def get_missions():
    db = database.db_connect()
    c = db.cursor()
    c.execute("SELECT M.ID, MT.Name As MissionType, Reward_ID FROM Mission M JOIN Mission_Type MT on M.Mission_Type_ID = MT.ID")
    result = []
    for row in c:
        result.append(row)
    return result

def reset_tables():
	print "> Reset Mission Completed Table"
	reset_mission_completed()
	print "> Reset Mission Type Table"
	reset_mission_type()
	print "> Reset Mission Table"
	reset_mission()
	
def reset_mission_completed():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission_Completed")
    c.execute("CREATE TABLE Mission_Completed (Character_ID INT NOT NULL, Mission_ID INT NOT NULL)")
    db.commit()
	
def reset_mission_type():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission_Type")
    c.execute("CREATE TABLE Mission_Type (ID INT NOT NULL PRIMARY KEY, Name CHAR(255))")
    db.commit()
	
def reset_mission():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission")
    c.execute("CREATE TABLE Mission (ID INT PRIMARY KEY NOT NULL, Mission_Type_ID INT NOT NULL, Reward_ID INT NOT NULL)")
    db.commit()
	
if __name__ == '__main__':
    if ("-reset" in sys.argv):
        reset_tables()

