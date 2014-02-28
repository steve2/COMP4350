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

#Returns a list of recipe ID's that can be purchased through gold alone 
#(Not sure what Item_ID Gold would be so at the moment hard coded it as 0)
def get_purchasable_items():
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT in.Quantity, in.Recipe_ID, out.Item_ID From Recipe_In in INNER JOIN Recipe_Out out ON in.Recipe_ID = out.Recipe_ID WHERE Item_ID = 0 GROUP BY Recipe_ID HAVING COUNT(Recipe_ID) = 1"
    
    c.execute(qry)
    result = []
    for row in c:
        result.append(row)
    return result    
   
    
#Given a recipe_id returns a list of the required items and quantity
def get_recipe_in(recipe_id):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Item.name, Quantity FROM Recipe_In JOIN Item ON (recipe.Item_ID = Item.ID) WHERE Recipe_ID="+database.INSERT_SYM
    c.execute(qry, (recipe_id))
    result = []
    for row in c:
        result.append(row)
    return result

#Given a recipe_id returns a list of the resulting items and quantity
def get_recipe_out(recipe_id):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Item.name, Quantity FROM Recipe_Out JOIN Item ON (recipe.Item_ID = Item.ID) WHERE Recipe_ID="+database.INSERT_SYM
    c.execute(qry, (recipe_id))
    result = []
    for row in c:
        result.append(row)
    return result

def create_recipe(recipe_id, recipe_name):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO 'Recipe' (Recipe_ID, Recipe_Name) VALUES ("+database.INSERT_SYM+", "+database.INSERT_SYM+")"
    c.execute(qry, (recipe_id, recipe_name))
    db.commit()
    return True

def create_recipe_in(recipe_id, item_id, quantity):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO 'Recipe_In' (Recipe_ID, Item_ID, Quantity) VALUES ("+database.INSERT_SYM+", "+database.INSERT_SYM+", "+database.INSERT_SYM+")"
    c.execute(qry, (recipe_id, item_id, quantity))
    db.commit()
    return True

def create_recipe_out(recipe_id, item_id, quantity):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO 'Recipe_Out' (Recipe_ID, Item_ID, Quantity) VALUES ("+database.INSERT_SYM+", "+database.INSERT_SYM+", "+database.INSERT_SYM+")"
    c.execute(qry, (recipe_id, item_id, quantity))
    db.commit()
    return True

def reset_recipe_in():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Recipe_In")
    #c.executescript('''DROP TABLE IF EXISTS Recipe_In''')
    #c.execute('''CREATE TABLE Recipe_In (Recipe_ID INT, Item_ID INT, Quantity INT, FOREIGN KEY (Recipe_ID) REFERENCES Recipe (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID)''')
    db.commit()
    db.close()       
    
def reset_recipe_out():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DELETE FROM Recipe_Out")
    #c.executescript('''DROP TABLE IF EXISTS Recipe_Out''')
    #c.execute('''CREATE TABLE Recipe_Out (Recipe_ID INT, Item_ID INT, Quantity INT, FOREIGN KEY (Recipe_ID) REFERENCES Recipe (ID), FOREIGN KEY (Item_ID) REFERENCES Item (ID)''')    
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