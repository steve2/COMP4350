#===========================================================================
# inventory.py
# 
# Notes:
#	- Code interacts with MySQL database to get Character 
#     inventory information.
#
#===========================================================================

#
# Dependencies
#====================
import database
import sys


#
# get_inventory ()
#   @ charid:   ID of Character to retrieve the inventory for.
#   @ return:   Returns LIST of paired values for item NAME and QUANTITY.
#
# contains_items()
#   @ charid:   ID of Character
#   @ row:      A list of Item.ID and Quantity pairs
#   @ return:   true or false (Whether or not the Inventory contains all of the items)
#
# -The result of this function can be used in the "item.py" function
#  to search the details of these items by name.
#
# -We could also just have a function that returns a detailed list of items.
#
def get_inventory(charid):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Item.Name, Quantity FROM Inventory_Item JOIN Item ON Item_ID=Item.ID WHERE Character_ID="+database.INSERT_SYM
    c.execute(qry, (charid,)) #result should contain Name of items and their quantity.
    result = []
    for row in c:
        result.append(row)
    return result

def contains_items(charid, rows):
    #for row in rows:
        #TODO: Search thrhough get_invenotry?
        #if not inventory.contains(charid, row['Item.ID'], row['Quantity']):
            #return false
    #return true
    return false

def get_item_quantity(charid, itemid):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT 1 Quantity FROM Inventory_Item JOIN Item ON Item_ID=Item.ID WHERE Characer_ID="+database.INSERT_SYM+" AND Item_ID="+database.INSERT_SYM
    c.execute(qry, (charid,itemid,))
    if c.rowcount == 1:
        result = c.fetchone()["Quantity"]
    else:
        result = 0
    return 0

def remove_items(charid, rows):
    for row in rows:
        remove(charid, row['Item.ID'], row['Quantity'])
       
def remove(charid, itemid, quantity):
    db = database.db_connect()
    c = db.cursor()
    #TODO: Find the item,
    #TODO: Decrement quantity
    #TODO: if quantity <= 0. Then remove the row

def add_items(charid, rows):
    for row in rows:
        add(charid, row['Item.ID'], row['Quantity'])
        
def add(charid, itemid, quantity):
    db = database.db_connect()
    c = db.cursor()
    if get_item_quantity(charid, itemid) > 0:
        qry = "UPDATE Inventory_Item SET Quantity="+database.INSERT_SYM+" WHERE Item_ID="+database.INSERT_SYM
        c.execute(qry, (quantity, itemid,))
    else:
        qry = "INSERT INTO Inventory_ITEM VALUES("+database.INSERT_SYM+","+database.INSERT_SYM+","+database.INSERT_SYM+")"
        c.execute(qry, (charid, itemid, quantity))
        
    #TODO: Find the item, insert row if it doesn't exist
    #TODO: Increment quantity

def reset_inventory():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS Inventory_Item (Character_ID INT NOT NULL, Item_ID INT NOT NULL, Quantity INT, PRIMARY KEY (Character_ID, Item_ID), FOREIGN KEY (Character_ID) REFERENCES `Character` (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))")
    c.execute("DELETE FROM Inventory_Item")
#   c.execute("DROP TABLE IF EXISTS Inventory_Item")
#   c.execute("CREATE TABLE Inventory_Item (Character_ID INT NOT NULL, Item_ID INT NOT NULL, Quantity INT, PRIMARY KEY (Character_ID, Item_ID), FOREIGN KEY (Character_ID) REFERENCES `Character` (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))")
    db.commit()
    
    
def reset_tables():
    print "> Reset Inventory Table"
    reset_inventory()
    
    
if __name__ == '__main__':
    if "-reset" in sys.argv:
        reset_tables()
