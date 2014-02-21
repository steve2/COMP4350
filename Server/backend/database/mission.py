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
from database import *


def print_missions():
    db = db_connect()
    c = db.cursor()
    c.execute("SELECT * FROM Mission")
    result = c.fetchone()
    while (result != None):
        print "- ", result, "\n"
        result = c.fetchone()
    print "----\n"
    db.close()

def get_missions():
	db = db_connect()
	c = db.cursor()
	c.execute("SELECT ID, MT.Name As MissionType" +
			"FROM Mission M INNER JOIN Mission_Type MT on M.Mission_Type_ID = MT.ID ")
	result = c.fetchall()
	db.close()
	return result

def reset_tables():
	print "> Reset Mission Completed Table"
	reset_mission_completed()
	print "> Reset Mission Type Table"
	reset_mission_type()
	print "> Reset Mission Table"
	reset_mission()
	
def reset_mission_completed():
    db = db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission_Completed")
    c.execute("CREATE TABLE Mission_Completed (Character_ID INT NOT NULL, Mission_ID INT NOT NULL)")
    db.commit()
    db.close()
	
def reset_mission_type():
    db = db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission_Type")
    c.execute("CREATE TABLE Mission_Type (ID INT NOT NULL PRIMARY KEY, Name CHAR)")
    db.commit()
    db.close()
	
def reset_mission():
    db = db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission")
    c.execute("CREATE TABLE Mission (ID INT PRIMARY KEY NOT NULL, Mission_Type_ID INT NOT NULL)")
    db.commit()
    db.close()
	
if __name__ == '__main__':
    if ("-reset" in sys.argv):
        reset_tables()

