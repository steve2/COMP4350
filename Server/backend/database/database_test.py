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
from item import get_items
from item import get_item
from inventory import get_inventory
from equipment import get_equipment

class DatabaseTestCase (unittest.TestCase):
    def setUp (self):
        self.db = db_connect_test()
        self.curs = self.db.cursor()
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
        
    def tearDown (self):
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
        
    def test_get_item (self):
        #Valid Items:
        assert get_item("Axe") != None
        assert len(get_item("Axe")[0]) == 6
        #Invalid Items:
        assert not get_item(None)
        assert not get_item("")
        assert not get_item("Fake Item")
        
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



if __name__ == '__main__':
    unittest.main()
    
        