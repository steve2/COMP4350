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

INSERT_SYM = '?'
Database = None
    
#***************************************************************************
# Database Functions	
#***************************************************************************
def db_connect():
    global Database
    global INSERT_SYM
    
    if Database == None:
        if "-local" in sys.argv:
            INSERT_SYM = '?'
            Database = sqlite3.connect("local.db")
        else:
            INSERT_SYM = '%s'
            Database = MySQLdb.connect(HOST_NAME, USER_NAME, USER_PASS, TABL_NAME)        
    
    return Database

def db_connect_test():
    global Database
    global INSERT_SYM
    
    INSERT_SYM = '?'
    Database = sqlite3.connect("test.db")
    return Database

	

