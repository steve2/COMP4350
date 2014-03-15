#===========================================================================
# recipe.py
#
# Notes:
#      - Code interacts with MySQL database to get recipe information
#
#===========================================================================

# 
# Dependencies
#==================
import database
import sys
import character
import inventory


SHOP = -1 #Character ID for SHOP
#Provides support for use/undo_recipe and trading
def exec_recipe(recipe, inChar, outChar):
    inItems = get_recipe_in(recipe)
    outItems = get_recipe_out(recipe)
    
    #Verify the recipe is valid (Character has sufficient items in inventory)
    #We don't want to remove/add anything until we know that the transaction is valid
    if inChar != character.SHOP and not inventory.contains_items(inChar, inItems):
       return False
    if outChar != character.SHOP and not inventory.contains_items(outChar, outItems):
        return False
        
    #Remove items from inventories
    if(inChar != character.SHOP):
        inventory.remove_items(inChar, inItems)
    if(outChar != character.SHOP):
        inventory.remove_items(outChar, outItems)

    #Add items to inventories
    if(inChar != character.SHOP):
        inventory.add_items(inChar, outItems)
    if(outChar != character.SHOP):
        inventory.add_items(outChar, inItems)
    return True

#Returns a list of recipe ID's with name and quantity that can be purchased through gold alone 
def get_purchasable_items():
    db = database.db_connect()
    c = db.cursor()
    qry = '''SELECT Recipe_In.Quantity, Recipe_In.Recipe_ID, Item_Out.Name FROM 
    (((Recipe_In INNER JOIN Recipe_Out ON Recipe_In.Recipe_ID = Recipe_Out.Recipe_ID) 
    INNER JOIN Item AS Item_Out ON Item_Out.ID = Recipe_Out.Item_ID) 
    INNER JOIN Item AS Item_In On Item_In.ID = Recipe_In.Item_ID)
    WHERE Item_In.Name = "Gold" 
    GROUP BY Recipe_In.Recipe_ID 
    HAVING COUNT(Recipe_In.Recipe_ID) = 1'''
    c.execute(qry)
    result = []
    for row in c:
        result.append(row)
    return result    
   
#Returns a list of recipe ID's that is not purchasable
def get_craftable_items():
    db = database.db_connect()
    c = db.cursor()
    qry = '''Select input.Recipe_ID From ((Recipe_In AS input 
    INNER JOIN Recipe_Out AS output ON input.Recipe_ID = output.Recipe_ID)
    INNER JOIN Item ON Item.ID = output.Item_ID) WHERE input.Recipe_ID 
    NOT IN (SELECT Recipe_In.Recipe_ID FROM (((Recipe_In INNER JOIN Recipe_Out ON Recipe_In.Recipe_ID = Recipe_Out.Recipe_ID)
    INNER JOIN Item AS Item_Out ON Item_Out.ID = Recipe_Out.Item_ID)
    INNER JOIN Item AS Item_In On Item_In.ID = Recipe_In.Item_ID) 
    WHERE Item_In.Name = "Gold" GROUP BY Recipe_In.Recipe_ID 
    HAVING COUNT(Recipe_In.Recipe_ID) = 1)
		GROUP BY input.Recipe_ID'''
    c.execute(qry)
    result = []
    for row in c:
        result.append(row)
    return result 
    
#Given a recipe_id returns a list of the required items and quantity
def get_recipe_in(recipe_id):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Item.ID, Quantity FROM Recipe_In JOIN Item ON (Recipe_In.Item_ID = Item.ID) WHERE Recipe_ID="+database.INSERT_SYM
    c.execute(qry, (recipe_id,))
    result = []
    for row in c:
        result.append(row)
    return result

#Given a recipe_id returns a list of the resulting items and quantity
def get_recipe_out(recipe_id):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Item.ID, Quantity FROM Recipe_Out JOIN Item ON (Recipe_Out.Item_ID = Item.ID) WHERE Recipe_ID="+database.INSERT_SYM

    c.execute(qry, (recipe_id,))
    result = []
    for row in c:
        result.append(row)
    return result

def create_recipe(recipe_id, recipe_name):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO Recipe (ID, Name) VALUES ("+database.INSERT_SYM+", "+database.INSERT_SYM+")"
    c.execute(qry, (recipe_id, recipe_name))
    db.commit()
    return True

def create_recipe_in(recipe_id, item_id, quantity):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO Recipe_In (Recipe_ID, Item_ID, Quantity) VALUES ("+database.INSERT_SYM+", "+database.INSERT_SYM+", "+database.INSERT_SYM+")"
    c.execute(qry, (recipe_id, item_id, quantity))
    db.commit()
    return True

def create_recipe_out(recipe_id, item_id, quantity):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO Recipe_Out (Recipe_ID, Item_ID, Quantity) VALUES ("+database.INSERT_SYM+", "+database.INSERT_SYM+", "+database.INSERT_SYM+")"
    c.execute(qry, (recipe_id, item_id, quantity))
    db.commit()
    return True

def reset_recipe_in():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Recipe_In")
    #c.executescript('''DROP TABLE IF EXISTS Recipe_In''')
    #c.execute('''CREATE TABLE Recipe_In (Recipe_ID INT, Item_ID INT, Quantity INT, FOREIGN KEY (Recipe_ID) REFERENCES Recipe (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))''')
    db.commit()
    db.close()       
    
def reset_recipe_out():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Recipe_Out")
    #c.executescript('''DROP TABLE IF EXISTS Recipe_Out''')
    #c.execute('''CREATE TABLE Recipe_Out (Recipe_ID INT, Item_ID INT, Quantity INT, FOREIGN KEY (Recipe_ID) REFERENCES Recipe (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID))''')    
    db.commit()
    db.close()        
    
def reset_recipe():
    db = database.db_connect()
    c = db.cursor()
    c.execute("Delete From Recipe")
    #c.executescript('''DROP TABLE IF EXISTS Recipe''')
    #c.execute('''CREATE TABLE Recipe (ID INT PRIMARY KEY NOT NULL AUTO_INCREMENT, Name CHAR(255) NOT NULL)''')
    db.commit()
    db.close()
    
def reset_tables():
    print "> Reset Recipe_In Table"
    reset_recipe_in()
    print "> Reset Recipe_Out Table"
    reset_recipe_out()
    print "> Reset Recipe Table"
    reset_recipe()
    
if __name__ == '__main__':
    if ("-reset" in sys.argv):
        reset_tables()    
