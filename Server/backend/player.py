class Player:
    def __init__(self, name, password_hash):
        self.name = name
        self.password_hash = password_hash
        self.statistics = {}

    def add_stat(name, value):
        self.statistics[name] = value

    def get_stat(name):
        return self.statistics[name]
