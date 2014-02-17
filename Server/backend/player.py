class Player:
    def __init__(self, name, password_hash):
        self.name = name
        self.password_hash = password_hash
        self.statistics = {}

    def __eq__(self, other):
        return self.name == other.name and self.password_hash == other.password_hash

    def __hash__(self):
        return hash(self.name) ^ hash(self.password_hash)

    def add_stat(name, value):
        self.statistics[name] = value

    def get_stat(name):
        return self.statistics[name]
