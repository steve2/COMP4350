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
    qry = '''SELECT Item.Name, Slot.Name, Description, Attribute.Text, Value, Item_Type.Name 
                FROM Item 
                    JOIN Item_Attributes ON (Item.ID=Item_Attributes.Item_ID) 
                    JOIN Item_Type ON (Item.Item_Type_ID=Item_Type.ID) 
                    JOIN Attribute ON (Attribute.ID=Attribute_ID) 
                    JOIN Item_Slot ON (Item_Slot.Item_Type_ID=Item.Item_Type_ID)
                    JOIN Slot ON (Slot.ID=Item_Slot.Slot_ID)''';
    c.execute(qry)
    result = []
    for row in c:
        result.append(row)
    return result
    
def get_item(itemname):
    db = database.db_connect()
    c = db.cursor()
    qry = '''SELECT Item.Name, Slot.Name, Description, Attribute.Text, Value, Item_Type.Name 
                FROM Item 
                    JOIN Item_Attributes ON (Item.ID=Item_Attributes.Item_ID) 
                    JOIN Item_Type ON (Item.Item_Type_ID=Item_Type.ID) 
                    JOIN Attribute ON (Attribute.ID=Attribute_ID) 
                    JOIN Item_Slot ON (Item_Slot.Item_Type_ID=Item.Item_Type_ID)
                    JOIN Slot ON (Slot.ID=Item_Slot.Slot_ID)
                        WHERE Item.Name='''+database.INSERT_SYM;
    c.execute(qry, (itemname,))
    result = []
    for row in c:
        result.append(row)
    print "Got item:", result
    return result

# NB: Only for tests, write a script to put in your own items
def add_item_type(itemname, attribute, value, slotname, description):
    db = database.db_connect()
    c = db.cursor()

    itemId = 1
    itemTypeId = 1
    attribId = 1
    slotId = 1

    qry = "INSERT INTO Item_Type VALUES (" + database.INSERT_SYM + "," + database.INSERT_SYM + ")"
    c.execute(qry, (itemTypeId, itemname))
    itemid = c.lastrowid

    qry = "INSERT INTO Item VALUES ("+ database.INSERT_SYM + "," + database.INSERT_SYM + "," + database.INSERT_SYM +"," + database.INSERT_SYM + ")"
    c.execute(qry, (itemId, itemid, itemname, description))

    qry = "INSERT INTO Attribute VALUES (" + database.INSERT_SYM + "," + database.INSERT_SYM + ")"
    c.execute(qry, (attribId, attribute))
    attrid = c.lastrowid

    qry = "INSERT INTO Item_Attributes VALUES ("+database.INSERT_SYM +","+database.INSERT_SYM+","+database.INSERT_SYM+")"
    c.execute(qry, (itemid, attrid, value))

    qry = "INSERT INTO Slot VALUES ("+database.INSERT_SYM + "," + database.INSERT_SYM+")"
    c.execute(qry, (slotId, slotname))
    slotid = c.lastrowid

    qry = "INSERT INTO Item_Slot VALUES (" +database.INSERT_SYM +","+database.INSERT_SYM + ")"
    c.execute(qry, (itemTypeId, slotId))
    
    db.commit()

    return (itemid, attrid, slotid)
    
def reset_item_types():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Item_Type")
    c.execute("CREATE TABLE IF NOT EXISTS Item_Type (ID INT NOT NULL PRIMARY KEY, Name CHAR(64))")
    c.execute("DELETE FROM Item_Type")
    db.commit()
    
def reset_items():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Item")
    c.execute("CREATE TABLE IF NOT EXISTS Item (ID INT NOT NULL PRIMARY KEY, Item_Type_ID INT NOT NULL, Name CHAR(64), Description CHAR(255), FOREIGN KEY (Item_Type_ID) REFERENCES Item_Type (ID))")
    c.execute("DELETE FROM Item")
    db.commit()
    
def reset_item_attributes():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Item_Attributes")
    c.execute("CREATE TABLE IF NOT EXISTS Item_Attributes ( Item_ID INT NOT NULL, Attribute_ID INT NOT NULL, Value INT, FOREIGN KEY (Item_ID) REFERENCES Item (ID), FOREIGN KEY (Attribute_ID) REFERENCES Attribute (ID), PRIMARY KEY (Item_ID, Attribute_ID) )")
    c.execute("DELETE FROM Item_Attributes")
    db.commit()

def reset_attributes():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Attribute")
    c.execute("CREATE TABLE IF NOT EXISTS Attribute (ID INT NOT NULL PRIMARY KEY, Text CHAR (64))")
    c.execute("DELETE FROM Attribute")
    db.commit()

def reset_item_slots():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE IF EXISTS Item_Slot")
    c.execute("CREATE TABLE IF NOT EXISTS Item_Slot (Item_Type_ID INT NOT NULL, Slot_ID INT NOT NULL, PRIMARY KEY (Item_Type_ID, Slot_ID), FOREIGN KEY (Item_Type_ID) REFERENCES Item_Type (ID), FOREIGN KEY (Slot_ID) REFERENCES Slot (ID))")
    c.execute("DELETE FROM Item_Slot")
    db.commit()
    
def reset_tables():
    print "> Reset Item Type Table"
    reset_item_types()
    print "> Reset Item Attributes Table"
    reset_item_attributes()
    print "> Reset Item Table"
    reset_items()
    print "> Reset Attributes"
    reset_attributes()
    print "> Reset Item Slots"
    reset_item_slots()
	
if __name__ == '__main__':
    if ("-reset" in sys.argv):
        reset_tables()
