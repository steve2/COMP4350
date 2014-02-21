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


def print_missions():
    db = database.db_connect()
    c = db.cursor()
    c.execute("SELECT * FROM Mission")
    result = c.fetchone()
    while (result != None):
        print "- ", result, "\n"
        result = c.fetchone()
    print "----\n"
    db.close()

def get_missions():
	db = database.db_connect()
	c = db.cursor()
	c.execute("SELECT ID, MT.Name As MissionType" +
			"FROM Mission M INNER JOIN Mission_Type MT on M.Mission_Type_ID = MT.ID ")
	result = c.fetchall()
	db.close()
	return result

def reset_tables():
	reset_mission_completed()
	reset_mission_type()
	reset_mission()
	
def reset_mission_completed():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission_Completed")
    c.execute("CREATE TABLE Mission_Completed (Character_ID INT NOT NULL, Mission_ID INT NOT NULL)")
    db.commit()
    db.close()
	
def reset_mission_type():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission_Type")
    c.execute("CREATE TABLE Mission_Type (ID INT NOT NULL PRIMARY KEY, Name CHAR)")
    db.commit()
    db.close()
	
def reset_mission():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Mission")
    c.execute("CREATE TABLE Mission (ID INT PRIMARY KEY NOT NULL, Mission_Type_ID INT NOT NULL)")
    db.commit()
    db.close()
	
if __name__ == '__main__':
	reset_tables()