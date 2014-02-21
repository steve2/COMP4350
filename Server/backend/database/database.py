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
# Database Functions	
#***************************************************************************
	
def db_connect():
    if "-local" in sys.argv:
        db = sqlite3.connect("local.db")
    else:
        db = MySQLdb.connect(HOST_NAME, USER_NAME, USER_PASS, TABL_NAME)
    return db


	

