#===========================================================================
# achievement.py
# 
# Notes:
#	- Code interacts with database to get Achievement information.
#
#===========================================================================

#
# Dependencies
#====================
import database

def get_all_achievements():
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Achievement.Name FROM Achievement"
    c.execute(qry, ())
    result = []
    for row in c:
        result.append(row[0])
    return result

def get_achievement_description(name):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Achievement.Descr FROM Achievement WHERE Achievement.Name=" + database.INSERT_SYM
    c.execute(qry, (name,))
    result = c.fetchone()
    return result

def add_player_achievement(player, achievement):
    db = database.db_connect()
    c = db.cursor()

    qry = "SELECT Player.ID FROM Player WHERE Username="+database.INSERT_SYM
    c.execute(qry, (player,))
    playerId = c.fetchone()

    qry = "SELECT Achievement.ID FROM Achievement WHERE Achievement.Name="+database.INSERT_SYM
    c.execute(qry, (achievement,))
    achievementId = c.fetchone()

    add_player_achievement_by_id(playerId, achievementId)

def add_player_achievement_by_id(playerId, achievementId):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO Achievement_Completed VALUES (" + database.INSERT_SYM + ", " + INSERT_SYM + ")"
    c.execute(qry, (playerId, achievementId))

def get_player_achievement(player):
    db = database.db_connect()
    c = db.cursor()

    qry = "SELECT Player.ID FROM Player WHERE Username="+database.INSERT_SYM
    c.execute(qry, (player,))
    playerId = c.fetchone()

    ids = get_player_achievement_by_id(playerId)
    
    qry = "SELECT Achievement.Name FROM Achievement WHERE Achievement.ID IN " + database.INSERT_SYM

    c.execute(qry, (ids,)) 

    result = []
    for row in c:
        result.append(row)

    return result

def get_player_achievement_by_id(playerId):
    db = database.db_connect()
    c = db.cursor()
    qry = "SELECT Achievement_Completed.Achievement_ID FROM Achievement_Completed WHERE Achievement_Completed.Player_ID=" + database.INSERT_SYM
    c.execute(qry, (playerId,))

    result = []
    for row in c:
        result.append(row)

    return result

# NB: Don't expose this one to the web
def create_achievement(name, description):
    db = database.db_connect()
    c = db.cursor()
    qry = "INSERT INTO Achievement (Name, Descr) VALUES (" + database.INSERT_SYM + ", " + database.INSERT_SYM + ")"
    c.execute(qry, (name, description))

def reset_achievements():
    db = database.db_connect()
    c = db.cursor()
    c.execute("DROP TABLE Achievement")
    c.execute("CREATE TABLE IF NOT EXISTS Achievement (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name CHAR (64), Descr CHAR (255))")
    c.execute("DELETE FROM Achievement")
    db.commit()

def reset_achievement_completed():
    db = database.db_connect()
    c = db.cursor()
    
    c.execute("CREATE TABLE IF NOT EXISTS Achievement_Completed ( Player_ID INTEGER NOT NULL, Achievement_ID INTEGER NOT NULL, FOREIGN KEY (Player_ID) REFERENCES Player (ID), FOREIGN KEY (Achievement_ID) REFERENCES Achievement (ID) )")
    c.execute("DELETE FROM Achievement_Completed")
    db.commit()
