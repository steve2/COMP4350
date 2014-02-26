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
from database import *


#
# get_inventory ()
#   @ charid:   ID of Character to retrieve the inventory for.
#   @ return:   Returns LIST of paired values for item NAME and QUANTITY.
#
# -The result of this function can be used in the "item.py" function
#  to search the details of these items by name.
#
# -We could also just have a function that returns a detailed list of items.
#
def get_inventory(charid):
    db = db_connect()
    c = db.cursor()
    qry = "SELECT Item.Name, Quantity FROM Inventory_Item JOIN Item ON Item_ID=Item.ID WHERE Character_ID=%s"
    c.execute(qry, (charid,)) #result should contain Name of items and their quantity.
    result = []
    for row in c:
        result.append(row)
    db.close()
    return result
    

def reset_inventory():
    db = db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Inventory_Item")
#   c.execute("DROP TABLE IF EXISTS Inventory_Item")
#   c.execute("CREATE TABLE Inventory_Item (Character_ID INT NOT NULL, Item_ID INT NOT NULL, Quantity INT, PRIMARY KEY (Character_ID, Item_ID), FOREIGN KEY (Character_ID) REFERENCES `Character` (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))")
    db.commit()
    db.close()
    
    
def reset_tables():
    print "> Reset Inventory Table"
    reset_inventory()
    
    
if __name__ == '__main__':
    if "-reset" in sys.argv:
        reset_tables()
        
    if "-test" in sys.argv:
        print "Test get_inventory(charid).."
        get_inventory(0)
        print "\t...Success."
        
        print "Testing 'inventory.py' Complete"

