#===========================================================================
# database.py
# 
# Notes:
#	- This script has been broken up into more specific scripts
#	- We are no longer using this.
#
#===========================================================================

#
# Dependencies
#====================
import sys
import sqlite3
import MySQLdb
#
# Constants
#============================
HOST_NAME = "localhost"
USER_NAME = "COMP4350_admin"
USER_PASS = "admin"
TABL_NAME = "COMP4350_GRP5"

global INSERT_SYM
INSERT_SYM = '?'

global Database
Database = None
    
#***************************************************************************
# Database Functions	
#***************************************************************************
def db_connect():
    global Database
    if Database == None:
        if "-local" in sys.argv:
            symbol = '?'
            Database = sqlite3.connect("local.db")
        else:
            symbol = '%s'
            Database = MySQLdb.connect(HOST_NAME, USER_NAME, USER_PASS, TABL_NAME)        
        global INSERT_SYM
        INSERT_SYM = symbol
    return Database
    
def db_close():
    global Database
    if Database != None:
        Database.close()
        Database = None

def db_connect_test():
    global Database
    global INSERT_SYM
    INSERT_SYM = '?'
    if Database != None:
        Database.close()
    Database = sqlite3.connect("test.db")
    return Database

	

