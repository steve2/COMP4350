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
    for row in rows:
        #TODO: Search through get_inventory instead?
        if inventory.get_quantity(charid, row['Item.ID']) < row['Quantity']:
            return false
    return true

def get_quantity(charid, itemid):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Quantity FROM Inventory_Item JOIN Item ON Item_ID=Item.ID WHERE Character_ID="+database.INSERT_SYM+" AND Item_ID="+database.INSERT_SYM
    c.execute(qry, (charid,itemid,))
    result = c.fetchone()
    if result != None:
        result = result[0]
    else:
        result = 0
    return result

def remove_items(charid, rows):
    for row in rows:
        remove(charid, row['Item.ID'], row['Quantity'])
       
def remove(charid, itemid, quantity):
    db = database.db_connect()
    c = db.cursor()

    curr_quantity = get_quantity(charid, itemid)
    if curr_quantity > 0 and curr_quantity - quantity > 0:
        quantity = curr_quantity - quantity
        qry = "UPDATE Inventory_Item SET Quantity="+database.INSERT_SYM+" WHERE Item_ID="+database.INSERT_SYM+" AND Character_ID="+database.INSERT_SYM
        c.execute(qry, (quantity, itemid, charid))
    else:
        qry = "DELETE FROM Inventory_Item WHERE Item_ID="+database.INSERT_SYM+" AND Character_ID="+database.INSERT_SYM
        c.execute(qry, (itemid, charid))
        
    db.commit()

def add_items(charid, rows):
    for row in rows:
        add(charid, row['Item.ID'], row['Quantity'])
        
def add(charid, itemid, quantity):
    db = database.db_connect()
    c = db.cursor()
    curr_quantity = get_quantity(charid, itemid)
    if  curr_quantity > 0:
        quantity= curr_quantity + quantity
        qry = "UPDATE Inventory_Item SET Quantity="+database.INSERT_SYM+" WHERE Item_ID="+database.INSERT_SYM+" AND Character_ID="+database.INSERT_SYM
        c.execute(qry, (quantity, itemid, charid))
    else:
        qry = "INSERT INTO Inventory_Item VALUES("+database.INSERT_SYM+","+database.INSERT_SYM+","+database.INSERT_SYM+")"
        c.execute(qry, (charid, itemid, quantity))
    db.commit()

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
