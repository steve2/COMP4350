#===========================================================================
# item.py
# 
# Notes:
#	- Code interacts with MySQL database to get Item information.
#
#===========================================================================

#
# Dependencies
#====================
import database
import sys

def get_items():
    db = database.db_connect();
    c = db.cursor()
    qry = "SELECT Item.Name, Description, Attribute.Text, Value, Item_Type.Name FROM Item JOIN Item_Attributes ON (Item.ID=Item_Attributes.Item_ID) JOIN Item_Type ON (Item.Item_Type_ID=Item_Type.ID) JOIN Attribute ON (Attribute.ID=Attribute_ID)";
    c.execute(qry)
    result = []
    for row in c:
        result.append(row)
    return result
    
def get_item(itemname):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Item.Name, Description, Attribute.Text, Value, Item_Type.Name FROM Item JOIN Item_Attributes ON (Item.ID=Item_Attributes.Item_ID) JOIN Item_Type ON (Item.Item_Type_ID=Item_Type.ID) JOIN Attribute ON (Attribute.ID=Attribute_ID) WHERE Item.Name="+database.INSERT_SYM;
    c.execute(qry, (itemname,))
    result = []
    for row in c:
        result.append(row)
    return result
    
def reset_item_types():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS Item_Type (ID INT NOT NULL PRIMARY KEY, Name CHAR(64))")
    c.execute("DELETE FROM Item_Type")
#   c.execute("DROP TABLE IF EXISTS Item_Type")
#   c.execute("CREATE TABLE Item_Type (ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT, Name CHAR(64))")
    db.commit()
    
def reset_items():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS Item (ID INT NOT NULL PRIMARY KEY, Item_Type_ID INT NOT NULL, Name CHAR(64), Description CHAR(255), FOREIGN KEY (Item_Type_ID) REFERENCES Item_Type (ID))")
    c.execute("DELETE FROM Item")
#   c.execute("DROP TABLE IF EXISTS Item")
#   c.execute("CREATE TABLE Item (ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT, Item_Type_ID INT NOT NULL, Name CHAR(64), Description CHAR(255), FORIEGN KEY (Item_Type_ID) REFERENCES Item_Type)")
    db.commit()
    
def reset_item_attributes():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS Item_Attributes ( Item_ID INT NOT NULL, Attribute_ID INT NOT NULL, Value INT, FOREIGN KEY (Item_ID) REFERENCES Item (ID), FOREIGN KEY (Attribute_ID) REFERENCES Attribute (ID), PRIMARY KEY (Item_ID, Attribute_ID) )")
    c.execute("DELETE FROM Item_Attributes")
#   c.execute("DROP TABLE IF EXISTS Item_Attributes")
#   c.execute("CREATE TABLE Item_Attributes ( Item_ID INT NOT NULL, Attribute_ID INT NOT NULL, Value INT, FOREIGN KEY (Item_ID) REFERENCES Item (ID), FOREIGN KEY (Attribute_ID) REFERENCES Attribute (ID), PRIMARY KEY (Item_ID, Attribute_ID) )")
    db.commit()
    
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
