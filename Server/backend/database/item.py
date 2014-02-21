#===========================================================================
# character.py
# 
# Notes:
#	- Code interacts with MySQL database to get Character information.
#
#===========================================================================

#
# Dependencies
#====================
from database import *
	

    
def reset_item_types():
    db = db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Item_Type")
#   c.execute("DROP TABLE IF EXISTS Item_Type")
#   c.execute("CREATE TABLE Item_Type (ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT, Name CHAR(64))")
    db.commit()
    db.close()
    
def reset_items():
    db = db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Item")
#   c.execute("DROP TABLE IF EXISTS Item")
#   c.execute("CREATE TABLE Item (ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT, Item_Type_ID INT NOT NULL, Name CHAR(64), Description CHAR(255), FORIEGN KEY (Item_Type_ID) REFERENCES Item_Type)")
    db.commit()
    db.close()
    
def reset_item_attributes():
    db = db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Item_Attributes")
#   c.execute("DROP TABLE IF EXISTS Item_Attributes")
#   c.execute("CREATE TABLE Item_Attributes ( Item_ID INT NOT NULL, Attribute_ID INT NOT NULL, Value INT, FOREIGN KEY (Item_ID) REFERENCES Item (ID), FOREIGN KEY (Attribute_ID) REFERENCES Attribute (ID), PRIMARY KEY (Item_ID, Attribute_ID) )")
    db.commit()
    db.close()
    
def reset_tables():
    print "> Reset Item Type Table"
    reset_item_types()
    print "> Reset Item Attributes Table"
    reset_item_attributes()
    print "> Reset Item Table"
    reset_items()
	
if __name__ == '__main__':
	if ("-reset" in sys.argv):
		reset_tables()
