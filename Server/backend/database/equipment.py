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
# get_equipment ()
#   @charid:    Character to get equipment of.
#
# Returns a paired list of the Item Name that is equipped and the Slot
# Name that it is equipped in. Clients don't really need to know the ID
# of the Slot.
#
def get_equipment(charid):
    db = db_connect()
    c = db.cursor()
    qry = "SELECT Item.Name, Slot.Name FROM Equipped_Item JOIN Slot ON Slot_ID=Slot.ID JOIN Item ON Item_ID=Item.ID WHERE Character_ID=%s"
    c.execute(qry, (charid,)) #result should contain name and slot of items equipped by this character.
    result = []
    for row in c:
        result.append(row)
    db.close()
    return result
    

def reset_equipment():
    db = db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Equipped_Item")
#   c.execute("DROP TABLE IF EXISTS Equipped_Item")
#   c.execute("CREATE TABLE Equipped_Item (Character_ID INT NOT NULL, Slot_ID INT NOT NULL, Item_ID INT NOT NULL, PRIMARY KEY (Character_ID, Slot_ID), FOREIGN KEY (Character_ID) REFERENCES `Character` (ID), FOREIGN KEY (Slot_ID) REFERENCES Slot (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))")
    db.commit()
    db.close()

    
def reset_tables():
    print "> Reset Equipment Table"
    reset_equipment()
    
    
if __name__ == '__main__':
    if "-reset" in sys.argv:
        reset_tables()

