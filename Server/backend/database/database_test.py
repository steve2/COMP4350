import unittest
import sys
import sqlite3

from player import get_player
from player import create_player
from character import get_player_id
from character import get_character
from character import get_characters
from character import create_character

class DatabaseTestCase (unittest.TestCase):
    def setUp (self):
    
        self.db = sqlite3.connect("test.db")
        self.curs = self.db.cursor()
        self.curs.execute("CREATE TABLE Achievement (ID INT NOT NULL PRIMARY KEY, Name CHAR (64), Descr CHAR (255))")
        self.curs.execute("CREATE TABLE Attribute (ID INT NOT NULL PRIMARY KEY, Text CHAR (64))")
        self.curs.execute("CREATE TABLE Slot (ID INT NOT NULL PRIMARY KEY, Name CHAR(64))")
        self.curs.execute("CREATE TABLE Player (ID INT NOT NULL PRIMARY KEY, Username CHAR(255) NOT NULL, Password CHAR(255) NOT NULL)")
        self.curs.execute("CREATE TABLE `Character` (ID INT NOT NULL PRIMARY KEY, Player_ID INT NOT NULL, Name CHAR(64), Exp INT DEFAULT 0, Play_Time INT DEFAULT 0)")
        self.curs.execute("CREATE TABLE Equipped_Item (Character_ID INT NOT NULL, Slot_ID INT NOT NULL, Item_ID INT NOT NULL, PRIMARY KEY (Character_ID, Slot_ID))")
        self.curs.execute("CREATE TABLE Inventory_Item (Character_ID INT NOT NULL, Item_ID INT NOT NULL, Quantity INT, PRIMARY KEY (Character_ID, Item_ID))")
        self.curs.execute("CREATE TABLE Item (ID INT NOT NULL PRIMARY KEY, Item_Type_ID INT NOT NULL, Name CHAR(64), Description CHAR(255))")
        self.curs.execute("CREATE TABLE Item_Attributes ( Item_ID INT NOT NULL, Attribute_ID INT NOT NULL, Value INT, PRIMARY KEY (Item_ID, Attribute_ID) )")
        self.curs.execute("CREATE TABLE Item_Type (ID INT NOT NULL PRIMARY KEY, Name CHAR(64))")
        self.curs.execute("CREATE TABLE Item_Slot (Item_Type_ID INT NOT NULL, Slot_ID INT NOT NULL, PRIMARY KEY (Item_Type_ID, Slot_ID))")
        
    def tearDown (self):
        self.curs.execute("DROP TABLE IF EXISTS Achievement")
        self.curs.execute("DROP TABLE IF EXISTS Attribute")
        self.curs.execute("DROP TABLE IF EXISTS Slot")
        self.curs.execute("DROP TABLE IF EXISTS Player")
        self.curs.execute("DROP TABLE IF EXISTS `Character`")
        self.curs.execute("DROP TABLE IF EXISTS Equipped_Item")
        self.curs.execute("DROP TABLE IF EXISTS Inventory_Item")
        self.curs.execute("DROP TABLE IF EXISTS Item")
        self.curs.execute("DROP TABLE IF EXISTS Item_Attributes")
        self.curs.execute("DROP TABLE IF EXISTS Item_Type")
        self.curs.execute("DROP TABLE IF EXISTS Item_Slot")
        self.db.close()
        
    def test_create_player (self):
        assert create_player(None, None) == False
        assert create_player("Test\Username", None) == False
        assert create_player(None, "Test\Password") == False
        assert create_player("Test\Username", "Test\Password") == True
        
    def test_get_player (self):
        assert get_player("Test\Username", "Test\Password") == None
        assert get_player(None, None) == None
        assert get_player(None, "Test\Password") == None
        assert get_player("Test\Username", None) == None
        
    def test_get_player_id (self):
        assert get_player_id(None) == None
        assert get_player_id("Test\Username") == None
        pass
        
    def test_get_character (self):
        pass
        
    def test_get_characters (self):
        pass
        
    def test_create_character  (self):
        pass
        
    def test_get_items (self):
        pass
        
    def test_get_item (self):
        pass
        
    def test_get_inventory (self):
        pass
        
    def test_get_equipment (self):
        pass



if __name__ == '__main__':
    unittest.main()
    
        