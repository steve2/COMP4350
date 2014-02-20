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
    if "-local" in sys.argv:
        db = sqlite3.connect("local.db")
    else:
        db = MySQLdb.connect(HOST_NAME, USER_NAME, USER_PASS, TABL_NAME)
    return db
	
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
	reset_missionCompleted()
	reset_missionType()
	reset_mission()
	
def reset_missionCompleted():
    db = db_connect()
    c = db.cursor()
    c.executescript('''DROP TABLE IF EXISTS Mission_Completed''')
    c.execute('''CREATE TABLE Mission_Completed (Character_ID INT NOT NULL, Mission_ID INT NOT NULL)''')
    db.commit()
    db.close()
	
def reset_missionType():
    db = db_connect()
    c = db.cursor()
    c.executescript('''DROP TABLE IF EXISTS Mission_Type''')
    c.execute('''CREATE TABLE Mission_Type (ID INT NOT NULL PRIMARY KEY, Name CHAR)''')
    db.commit()
    db.close()
	
def reset_mission():
    db = db_connect()
    c = db.cursor()
    c.executescript('''DROP TABLE IF EXISTS Mission''')
    c.execute('''CREATE TABLE Mission (ID INT PRIMARY KEY NOT NULL, Mission_Type_ID INT NOT NULL)''')
    db.commit()
    db.close()
	
if __name__ == '__main__':
	reset_tables()