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

#
# Get Items (All)
# ----
# Returns List of item tuples. Each tuple contains:
#   (Item Name // Item Description // Item Type Name)
#
def get_items():
    db = database.db_connect()
    c = db.cursor()
    qry = '''SELECT Item.Name, Description, Item_Type.Name 
                FROM Item JOIN Item_Type ON (Item.Item_Type_ID=Item_Type.ID)'''
    c.execute(qry)
    result = []
    for row in c:
        result.append(row)
    return result
 
#
# Get Item's Attributes
# ----
# Returns List of tuples with the attribute name/text and the value of that attribute.
#
def get_item_attributes(itemname):
    db = database.db_connect()
    c = db.cursor()
    qry = '''SELECT Attribute.Text, Item_Attributes.Value 
                FROM Item_Attributes 
                    JOIN Attribute ON Item_Attributes.Attribute_ID=Attribute.ID 
                    JOIN Item ON Item.ID=Item_ID 
                WHERE Item.ID='''+database.INSERT_SYM
    c.execute(qry, (itemname,))
    result = []
    for row in c:
        result.append(row)
    return result
    
#
# Get Item's Equipment Slots
# ----
# Returns List of Slot names that this Item can be equipped to.
#    
def get_item_slots_equippable(itemname):
    db = database.db_connect()
    c = db.cursor()
    qry = '''SELECT Slot.Name 
                FROM Item_Slot 
                    JOIN Slot ON Item_Slot.Slot_ID=Slot.ID 
                    JOIN Item_Type ON Item_Slot.Item_Type_ID=Item_Type.ID 
                    JOIN Item ON Item_Type.ID=Item.Item_Type_ID 
                WHERE Item.Name='''+database.INSERT_SYM
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
    db.commit()
    
def reset_items():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS Item (ID INT NOT NULL PRIMARY KEY, Item_Type_ID INT NOT NULL, Name CHAR(64), Description CHAR(255), FOREIGN KEY (Item_Type_ID) REFERENCES Item_Type (ID))")
    c.execute("DELETE FROM Item")
    db.commit()
    
def reset_item_attributes():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS Item_Attributes ( Item_ID INT NOT NULL, Attribute_ID INT NOT NULL, Value INT, FOREIGN KEY (Item_ID) REFERENCES Item (ID), FOREIGN KEY (Attribute_ID) REFERENCES Attribute (ID), PRIMARY KEY (Item_ID, Attribute_ID) )")
    c.execute("DELETE FROM Item_Attributes")
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
