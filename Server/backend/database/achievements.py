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
        result.append(row)
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

def reset_achievements():
    db = database.db_connect()
    c = db.cursor()
    c.execute("CREATE TABLE IF NOT EXISTS Achievement (ID INT NOT NULL PRIMARY KEY, Name CHAR (64), Descr CHAR (255))")
    c.execute("DELETE FROM Achievement")
    db.commit()

def reset_achievement_completed():
    db = database.db_connect()
    c = db.cursor()
    
    c.execute("CREATE TABLE IF NOT EXISTS Achievement_Completed (FOREIGN KEY (Player_ID) REFERENCES Player (ID), FOREIGN KEY (Achievement_ID) REFERENCES Achievement (ID))")
    c.execute("DELETE FROM Achievement_Completed")
    db.commit()
