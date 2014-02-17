from player import Player

class Model:
    def __init__(self):
        self.players = set()

    def create_player(self, name, password_hash):
        player = Player(name, password_hash)
        self.players.add( player )

    def has_player(self, name, password_hash):
        player = Player(name, password_hash)
        for p in self.players:
            if p == player:
                return True
        return False

    
