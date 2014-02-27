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

INSERT_SYM = '%s' if '-local' not in sys.argv else '?'
	
#***************************************************************************
# Database Functions	
#***************************************************************************
def db_connect():
    if "-local" in sys.argv:
        db = sqlite3.connect("local.db")
    else:
        db = MySQLdb.connect(HOST_NAME, USER_NAME, USER_PASS, TABL_NAME)
    return db


	

