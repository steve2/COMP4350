class Model:
    def __init__(self):
        self.players = set()

    def create_player(self, name, password_hash):
        player = Player(name, password_hash)
        self.players.add( player )

    def has_player(self, name, password_hash):
        return Player(name, password_hash) in self.players

    
