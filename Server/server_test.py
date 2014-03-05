import unittest
import json

from backend import app
import backend.database.database as database
import backend.database.player as player
import backend.database.character as character
import backend.database.inventory as inventory
import backend.database.item as item
import backend.database.equipment as equipment
import backend.database.achievements as achievements

# Test that all of our static webpages can be found and are getting loaded
class TemplateTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()

    def tearDown(self):
        pass

    def test_homepage(self):
        rv = self.app.get('/')
        assert rv.data != None

    def test_account(self):
        rv = self.app.get('/account')
        assert rv.data != None

    def test_login(self):
        rv = self.app.get('/login')
        assert rv.data != None

    def test_signup(self):
        rv = self.app.get('/signup')
        assert rv.data != None

    def test_character_create(self):
        rv = self.app.get('/character/new')
        assert rv.data != None

def post(app, url, data, debugPrint = False):
    resp = app.post(url, data=json.dumps(data), content_type='application/json')
    if debugPrint:
        print resp.data
    return json.loads(resp.data)

class HandlerTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()
        database.db_connect_test()

    def tearDown(self):
        pass

    def test_fail_empty_request(self):
        data = post(self.app, '/newAccount', {})
        assert data['result'] == False

    def test_create_new_account(self):
        header = {"user": "ABrandNewUser", "password": "new_guy"}
        data = post(self.app, '/newAccount', header)
        assert data['result'] == True

    def test_fail_bad_login(self):
        header = {"user": "notARealUserForSure", "password": "123456"}
        data = post(self.app, '/loginRequest', header)
        assert data['result'] == False

    def test_current_character_none(self):
        data = post(self.app, '/player/current', {})
        assert data["result"] == None

    def test_current_character_none(self):
        username = 'UserJoe'

        with self.app.session_transaction() as sess:
            sess['username'] = username

        data = post(self.app, '/player/current', {})
        assert data["result"] == username

class CharacterHandlerTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()
        database.db_connect_test()
        player.reset_players()
        character.reset_characters()
        inventory.reset_inventory()
        item.reset_items()
        item.reset_item_types()
        item.reset_item_attributes()
        equipment.reset_equipment()
        equipment.reset_slots()
        player.create_player("UserJoe", "test")

    def tearDown(self):
        pass

    def test_get_characters_empty(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"user": "notARealUserForSure", "password": "123456"}
        data = post(self.app, '/character/getAll', header)
        assert len(data['characters']) == 0

    def test_create_character(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"user": "notARealUserForSure", "password": "123456"}
        data = post(self.app, '/character/create', header)
        assert not data["result"]

    def test_get_created_character(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"charname": "CharacterBob"}
        data = post(self.app, '/character/create', header)
        database.db_connect_test() # reconnect the DB to the mock

        assert data["result"]

        data = post(self.app, '/character/getAll', {})

        characters = data["characters"]

        assert len(characters) == 1
        bob = characters[0]
        assert len(bob) == 5 # make sure schema hasn't changed
        assert bob[2] == "CharacterBob"

    def test_get_empty_inventory(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"charname": "CharacterFred"}
        post(self.app, '/character/create', header)
        database.db_connect_test() # reconnect the DB to the mock

        data = post(self.app, '/character/getAll', {})
        database.db_connect_test() # reconnect the DB to the mock

        assert len(data) == 1 # Make sure we got a character

        header = {"charid": data["characters"][0][0]}
        data = post(self.app, '/character/inventory', header)

        inventory = data["inventory"]
        assert len(inventory) == 0

    def test_get_empty_equipped(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"charname": "CharacterFred"}
        post(self.app, '/character/create', header)
        database.db_connect_test() # reconnect the DB to the mock

        data = post(self.app, '/character/getAll', {})
        database.db_connect_test() # reconnect the DB to the mock

        assert len(data) == 1 # Make sure we got a character

        header = {"charid": data["characters"][0][0]}
        data = post(self.app, '/character/equipped', header)

        inventory = data["equipment"]
        assert len(inventory) == 0

#TODO: Test items when DB tables are completed
class ItemHandlerTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()
        database.db_connect_test()

        player.reset_players()
        character.reset_characters()
        inventory.reset_inventory()
        item.reset_items()
        item.reset_item_types()
        item.reset_item_attributes()

        player.create_player("UserJoe", "test")

    def tearDown(self):
        pass

    def test_get_all_items(self):
        #data = post(self.app, '/item/getAll', header)
        pass

    def test_get_item(self):
        #data = post(self.app, '/item/get', header)
        pass

#TODO: Test recipes once the API has settled
class RecipeHandlerTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()
        database.db_connect_test()

    def tearDown(self):
        pass

class AchievementHandlerTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()
        database.db_connect_test()

        achievements.reset_achievements()
        achievements.reset_achievement_completed()
        player.reset_players()

        self.user = "UserJoe"
        self.name = "Test writing"
        self.descr = "Find ways to entertain yourself while writing tests"
        player.create_player(self.user, "test")

    def tearDown(self):
        pass

    def test_get_empty_achievements(self):
        achievements.reset_achievements()
        achievements.reset_achievement_completed()

        data = post(self.app, '/achievement/getAll', {})
        assert data['achievements'] == []

    def test_get_all_achievements(self):

        achievements.create_achievement(self.name, self.descr)
        data = post(self.app, '/achievement/getAll', {})
        assert data['achievements'] == [self.name]

if __name__ == '__main__':
    app.secret_key = "Testing secret key"
    unittest.main()
