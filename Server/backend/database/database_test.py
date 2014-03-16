import unittest
import sys
import sqlite3

from database import *

from player import get_player
from player import create_player
from character import get_player_id
from character import get_character
from character import get_characters
from character import create_character
from character import SHOP
from item import get_items
#from item import get_item
#TODO: Why are we importing 1 at a time?
from inventory import get_inventory
from inventory import contains_items
from inventory import get_quantity
from inventory import remove_items
from inventory import remove_item
from inventory import add_items
from inventory import add_item
from equipment import get_equipment
from recipe import exec_recipe
from recipe import get_recipe_in
from recipe import get_recipe_out

class DatabaseTestCase (unittest.TestCase):
    def cleanTables(self):
        self.curs.execute("DROP TABLE IF EXISTS Achievement")
        self.curs.execute("DROP TABLE IF EXISTS Attribute")
        self.curs.execute("DROP TABLE IF EXISTS Slot")
        self.curs.execute("DROP TABLE IF EXISTS Player")
        self.curs.execute("DROP TABLE IF EXISTS `Character`")
        self.curs.execute("DROP TABLE IF EXISTS Item_Type")
        self.curs.execute("DROP TABLE IF EXISTS Item")
        self.curs.execute("DROP TABLE IF EXISTS Equipped_Item")
        self.curs.execute("DROP TABLE IF EXISTS Inventory_Item")
        self.curs.execute("DROP TABLE IF EXISTS Item_Attributes")
        self.curs.execute("DROP TABLE IF EXISTS Item_Slot")
        self.curs.execute("DROP TABLE IF EXISTS Recipe")
        self.curs.execute("DROP TABLE IF EXISTS Recipe_In")
        self.curs.execute("DROP TABLE IF EXISTS Recipe_Out")
        
    def setUp (self):
        self.db = db_connect_test()
        self.curs = self.db.cursor()
        #There may be residual tables remaining if our setUp failed
        self.cleanTables() 
        self.curs.execute("CREATE TABLE IF NOT EXISTS Achievement (ID INT NOT NULL PRIMARY KEY, Name CHAR (64), Descr CHAR (255))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Attribute (ID INT NOT NULL PRIMARY KEY, Text CHAR (64))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Slot (ID INT NOT NULL PRIMARY KEY, Name CHAR(64))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Player (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Username CHAR(255) NOT NULL UNIQUE, Password CHAR(255) NOT NULL)")
        self.curs.execute("CREATE TABLE IF NOT EXISTS `Character` (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Player_ID INTEGER NOT NULL, Name CHAR(64), Exp INT DEFAULT 0, Play_Time INT DEFAULT 0)")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Item_Type (ID INT NOT NULL PRIMARY KEY, Name CHAR(64))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Item (ID INT NOT NULL PRIMARY KEY, Item_Type_ID INT NOT NULL, Name CHAR(64), Description CHAR(255), FOREIGN KEY (Item_Type_ID) REFERENCES Item_Type (ID))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Equipped_Item (Character_ID INT NOT NULL, Slot_ID INT NOT NULL, Item_ID INT NOT NULL, PRIMARY KEY (Character_ID, Slot_ID), FOREIGN KEY (Character_ID) REFERENCES `Character` (ID), FOREIGN KEY (Slot_ID) REFERENCES Slot (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Inventory_Item (Character_ID INT NOT NULL, Item_ID INT NOT NULL, Quantity INT, PRIMARY KEY (Character_ID, Item_ID), FOREIGN KEY (Character_ID) REFERENCES `Character` (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Item_Attributes ( Item_ID INT NOT NULL, Attribute_ID INT NOT NULL, Value INT, FOREIGN KEY (Item_ID) REFERENCES Item (ID), FOREIGN KEY (Attribute_ID) REFERENCES Attribute (ID), PRIMARY KEY (Item_ID, Attribute_ID) )")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Item_Slot (Item_Type_ID INT NOT NULL, Slot_ID INT NOT NULL, PRIMARY KEY (Item_Type_ID, Slot_ID), FOREIGN KEY (Item_Type_ID) REFERENCES Item_Type (ID), FOREIGN KEY (Slot_ID) REFERENCES Slot (ID))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Recipe (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name CHAR(255) NOT NULL)")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Recipe_In (Recipe_ID INT, Item_ID INT, Quantity INT, FOREIGN KEY (Recipe_ID) REFERENCES Recipe (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))")
        self.curs.execute("CREATE TABLE IF NOT EXISTS Recipe_Out (Recipe_ID INT, Item_ID INT, Quantity INT, FOREIGN KEY (Recipe_ID) REFERENCES Recipe (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))")
        
        self.curs.execute("INSERT INTO Player (Username, Password) VALUES ('Username', 'Password')")
        self.curs.execute("INSERT INTO Player (Username, Password) VALUES ('Username2', 'Password')")
        self.curs.execute("INSERT INTO `Character` (Player_ID, Name) VALUES (1, 'Character')")
        self.curs.execute("INSERT INTO `Character` (Player_ID, Name) VALUES (1, 'Other Character')")
        
        self.curs.execute("INSERT INTO Attribute VALUES (1, '+Damage')")
        self.curs.execute("INSERT INTO Attribute VALUES (2, '+Health')")
        
        self.curs.execute("INSERT INTO Item_Type VALUES (1, 'Weapon')")
        self.curs.execute("INSERT INTO Item VALUES (1, 1, 'Axe', 'Axe Description')")
        self.curs.execute("INSERT INTO Item_Attributes VALUES (1, 1, 5)")
        self.curs.execute("INSERT INTO Item_Attributes VALUES (1, 2, 5)")
        
        self.curs.execute("INSERT INTO Slot VALUES (1, 'Right Hand')")
        self.curs.execute("INSERT INTO Slot VALUES (2, 'Left Hand')")
        self.curs.execute("INSERT INTO Item_Slot VALUES (1, 1)")
        
        self.curs.execute("INSERT INTO Inventory_Item VALUES (1, 1, 2)")
        self.curs.execute("INSERT INTO Inventory_Item VALUES (2, 1, 2)")
        self.curs.execute("INSERT INTO Equipped_Item VALUES (1, 1, 1)")

        self.curs.execute("INSERT INTO Item_Type VALUES (0, 'Resource')")
        self.curs.execute("INSERT INTO Item VALUES (0, 0, 'Money', 'Used to purchase items')")
        self.curs.execute("INSERT INTO Item VALUES (2, 1, 'Sword', 'Sword Description')")
        self.curs.execute("INSERT INTO Recipe VALUES (1, 'My Recipe')")
        self.curs.execute("INSERT INTO Recipe_In VALUES (1, 0, 50)")
        self.curs.execute("INSERT INTO Recipe_Out VALUES (1, 2, 1)")
        
    def tearDown (self):
        self.cleanTables()
        self.db.close()
        
    def test_create_player (self):
        #Cannot create duplicate Usernames:
        assert not create_player("Username", "Password")
        assert not create_player("Username2", "Password")
        #Invalid input:
        assert not create_player(None, None)
        assert not create_player("TestUsername", None) 
        assert not create_player(None, "TestPassword") 
        #Mixed input:
        assert not create_player(None, "Password")
        #Valid innput:
        assert create_player("TestUsername", "TestPassword")
        
    def test_get_player (self):
        #Valid queries:
        assert get_player("Username2", "Password") != None
        assert get_player("Username", "Password") != None
        #Invalid input:
        assert get_player("TestUsername", "TestPassword") == None
        assert get_player(None, None) == None
        assert get_player(None, "TestPassword") == None
        assert get_player("TestUsername", None) == None
        #Mixed input:
        assert get_player("Username2", "Bad Password") == None
        assert get_player("Username", "Bad Password") == None
        assert get_player("Bad Username", "Password") == None
        assert get_player(None, "Password") == None
        assert get_player("Username2", None) == None
        
    def test_get_player_id (self):
        #Valid Player IDs:
        assert get_player_id("Username") != None
        assert get_player_id("Username2") != None
        #Invalid Player IDs:
        assert get_player_id(None) == None
        assert get_player_id("TestUsername") == None
        
    def test_get_character (self):
        #Valid Character:
        assert get_character(1) != None
        assert get_character(2) != None
        #Invalid Character:
        assert get_character(None) == None
        assert get_character(0) == None
        assert get_character(-1) == None
        
    def test_get_characters (self):
        #Valid characters:
        assert get_characters("Username") != None
        assert get_characters("Username2") != None
        #Correct responses:
        assert len(get_characters("Username")) == 2
        assert len(get_characters("Username2")) == 0
        #Invalid characters:
        assert get_characters("username") == None
        assert get_characters(None) == None
        assert get_characters("") == None
        assert get_characters("TestUsername") == None
        
    def test_create_character  (self):
        #invalid input
        assert not create_character(None, "TestCharName") 
        assert not create_character(None, None) 
        assert not create_character("TestUsername", None)
        #"TestUsername" does not exist, so a character cannot be created for it.
        assert not create_character("TestUsername", "TestCharname")
        #"Username" should work.
        assert create_character("Username", "TestCharName")
        assert create_character("Username2", "TestCharName")
        
    def test_get_items (self):
        #not a whole lot of variance here, just looking for a list and no MySQL errors.
        assert get_items() != None
        assert len(get_items()) >= 0
        
    #def test_get_item (self):
        #Valid Items:
        #assert get_item("Axe") != None
        #assert len(get_item("Axe")[0]) == 6
        #Invalid Items:
        #assert not get_item(None)
        #assert not get_item("")
        #assert not get_item("Fake Item")
        
    def test_get_inventory (self):
        #Valid characters (remember they take an ID to identify):
        assert get_inventory(1)
        assert get_inventory(2)
        assert len(get_inventory(1)) == 1
        assert len(get_inventory(2)) == 1
        #Invalid characters:
        assert not get_inventory("Username")
        assert not get_inventory("Username2")
        assert not get_inventory(None)
        assert not get_inventory(-1)
        assert not get_inventory(0)
        assert not get_inventory("")
        assert not get_inventory("Fake Char")
        
    def test_get_equipment (self):
        #Valid character:
        assert get_equipment(1) != None
        assert get_equipment(2) != None
        assert len(get_equipment(1)) == 1
        assert len(get_equipment(2)) == 0
        #Invalid character:
        assert not get_equipment("Username")
        assert not get_equipment("Username2")
        assert not get_equipment(-1)
        assert not get_equipment(0)
        assert not get_equipment(None)
        assert not get_equipment("")
        assert not get_equipment("Fake Char")


    def test_get_quantity(self):
        #Valid Items
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        #Invalid Character
        assert get_quantity(3000, 1) == 0
        #Invalid Items
        assert get_quantity(1, 3000) == 0
        #Completely Invalid
        assert get_quantity(1234, 1234) == 0
        
###CONTAINS_ITEMS###        
    def test_contains_itemsXpass(self):
        rows = []
        row = []
        rows.append(row)
        row.append(1) #itemid = 1
        row.append(2) #quantity = 2
        assert contains_items(1, rows) == True
        
    def test_contains_itemsXmultipass(self):
        add_item(1, 2, 1) #Add item 2
        rows = []
        row1 = []
        rows.append(row1)
        row1.append(1) #itemid = 1
        row1.append(2) #quantity = 2
        row2 = []
        rows.append(row2)
        row2.append(2) #itemid = 1
        row2.append(1) #quantity = 1
        assert contains_items(1, rows) == True

    def test_contains_itemsXmultifail(self):
        rows = []
        row1 = [] #This row passes
        rows.append(row1)
        row1.append(1) #itemid = 1
        row1.append(2) #quantity = 2
        row2 = [] #This row fails
        rows.append(row2)
        row2.append(2) #itemid = 1
        row2.append(1) #quantity = 1
        assert contains_items(1, rows) == False

    def test_contains_itemsXquantityfail(self):
        rows = []
        row = []
        rows.append(row)
        row.append(1) #itemid = 1
        row.append(3) #quantity = 3
        assert contains_items(1, rows) == False

    def test_contains_itemsXitemidfail(self):
        rows = []
        row = []
        rows.append(row)
        row.append(2) #itemid = 2
        row.append(1) #quantity = 1
        assert contains_items(1, rows) == False

    def test_contains_itemsXinvalidcharid(self):
        rows = []
        row = []
        rows.append(row)
        row.append(1) #itemid = 1
        row.append(1) #quantity = 1
        assert contains_items(5000, rows) == False

    def test_contains_itemsXinvaliditemid(self):
        rows = []
        row = []
        rows.append(row)
        row.append(300) #itemid = 1
        row.append(1) #quantity = 1
        assert contains_items(1, rows) == False
############

###REMOVE###
    def test_removeXincremental(self):
        assert get_quantity(1, 1) == 2
        remove_item(1, 1, 1)
        assert get_quantity(1, 1) == 1
        remove_item(1, 1, 1)
        assert get_quantity(1, 1) == 0

    def test_removeXbatch(self):
        assert get_quantity(1, 1) == 2
        remove_item(1, 1, 2)
        assert get_quantity(1, 1) == 0

    def test_removeXextra(self):
        assert get_quantity(1, 1) == 2
        remove_item(1, 1, 5000)
        assert get_quantity(1, 1) == 0

    def test_removeXnone(self):
        assert get_quantity(1, 1) == 2
        remove_item(1, 1, 0)
        assert get_quantity(1, 1) == 2

    def test_removeXuncessary(self):
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        remove_item(1, 2, 10)
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0

    def test_removeXinvalid(self):
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        remove_item(1, 10, 1)
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        remove_item(2, 1, 1)
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        
    def test_remove_itemsXremovesingle(self):
        assert get_quantity(1, 1) == 2
        rows = []
        row = []
        rows.append(row)
        row.append(1) #itemid = 1
        row.append(1) #quantity = 1
        remove_items(1, rows)
        assert get_quantity(1, 1) == 1

    def test_remove_itemsXremovemulti(self):
        add_item(1, 2, 1) #Add item 2
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 1
        rows = []
        row1 = []
        rows.append(row1)
        row1.append(1) #itemid = 1
        row1.append(1) #quantity = 1
        row2 = []
        rows.append(row2)
        row2.append(2) #itemid = 2
        row2.append(1) #quantity = 1
        remove_items(1, rows)
        assert get_quantity(1, 1) == 1
        assert get_quantity(1, 2) == 0
############
        
####ADD####        
    def test_addXincremental(self):
        assert get_quantity(1, 1) == 2
        add_item(1, 1, 1)
        assert get_quantity(1, 1) == 3
        add_item(1, 1, 1)
        assert get_quantity(1, 1) == 4
        
    def test_addXbatch(self):
        assert get_quantity(1, 1) == 2
        add_item(1, 1, 2)
        assert get_quantity(1, 1) == 4

    def test_addXcreate(self):
        assert get_quantity(1, 2) == 0
        add_item(1, 2, 2)
        assert get_quantity(1, 2) == 2
        add_item(1, 2, 1)
        assert get_quantity(1, 2) == 3

    def test_addXinvalid(self):
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        add_item(1, 10, 5000)
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        add_item(2, 1, 300)
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        
    def test_add_itemsXremovesingle(self):
        assert get_quantity(1, 1) == 2
        rows = []
        row = []
        rows.append(row)
        row.append(1) #itemid = 1
        row.append(1) #quantity = 1
        add_items(1, rows)
        assert get_quantity(1, 1) == 3

    def test_add_itemsXremovemulti(self):
        add_item(1, 2, 1) #Add item 2
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 1
        rows = []
        row1 = []
        rows.append(row1)
        row1.append(1) #itemid = 1
        row1.append(1) #quantity = 1
        row2 = []
        rows.append(row2)
        row2.append(2) #itemid = 2
        row2.append(1) #quantity = 1
        add_items(1, rows)
        assert get_quantity(1, 1) == 3
        assert get_quantity(1, 2) == 2        
############

####RECIPE#### 
    def test_exec_recipeXpass(self):
        add_item(1, 0, 50) #Add 50 Money
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 0) == 50 #Money
        assert get_quantity(1, 2) == 0 #Purchased Item

        assert exec_recipe(1, 1, SHOP) == True

        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 0) == 0 #Money
        assert get_quantity(1, 2) == 1 #Purchased Item

    def test_exec_recipeXclosefail(self):
        add_item(1, 0, 49) #Add 49 Money (Not enough)
        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        assert get_quantity(1, 0) == 49

        assert exec_recipe(1, 1, SHOP) == False

        assert get_quantity(1, 1) == 2
        assert get_quantity(1, 2) == 0
        assert get_quantity(1, 0) == 49

    def test_get_recipe_in(self):
        rows = get_recipe_in(1)
        assert len(rows) == 1
        row = rows[0]
        assert row[0] == 0 #Item id
        assert row[1] == 50 #Quantity

    def test_get_recipe_out(self):
        rows = get_recipe_out(1)
        assert len(rows) == 1
        row = rows[0]
        assert row[0] == 2 #Item id
        assert row[1] == 1 #Quantity
##############
        
        

if __name__ == '__main__':
    unittest.main()
    
        
