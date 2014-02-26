import unittest
import json
#from flask import jsonify

from backend import app

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

class HandlerTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()

    def tearDown(self):
        pass

    def test_fail_empty_request(self):
        resp = self.app.post('/newAccount', data=json.dumps({}), content_type='application/json')
        data = json.loads(resp.data)
        assert data['result'] == False

    def test_create_new_account(self):
        header = {"user": "ABrandNewUser", "password": "new_guy"}
        resp = self.app.post('/newAccount', data=json.dumps(header), content_type='application/json')
        print "Response:", resp
        print "Data:", resp.data
        data = json.loads(resp.data)
        assert data['result'] == True

    def test_fail_bad_login(self):
        header = {"user": "notARealUserForSure", "password": "123456"}
        resp = self.app.post('/loginRequest', data=json.dumps(header), content_type='application/json')
        data = json.loads(resp.data)
        assert data['result'] == False

class CharacterHandlerTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()

    def tearDown(self):
        pass

    def test_get_characters(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"user": "notARealUserForSure", "password": "123456"}
        resp = self.app.post('/character/getAll', data=json.dumps(header), content_type='application/json')

    def test_create_character(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"user": "notARealUserForSure", "password": "123456"}
        resp = self.app.post('/character/create', data=json.dumps(header), content_type='application/json')

    def test_get_created_character(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"charname": "CharacterBob"}
        resp = self.app.post('/character/create', data=json.dumps(header), content_type='application/json')
        resp = self.app.post('/character/getAll', data=json.dumps({}), content_type='application/json')

    def test_get_empty_inventory(self):
        with self.app.session_transaction() as sess:
            sess['username'] = 'UserJoe'

        header = {"charname": "CharacterFred"}
        resp = self.app.post('/character/create', data=json.dumps(header), content_type='application/json')
        resp = self.app.post('/character/getAll', data=json.dumps({}), content_type='application/json')
        data = json.loads(resp.data)

        header = {"charid": data["characters"][0][0]}
        resp = self.app.post('/character/inventory', data=json.dumps(header), content_type='application/json')
        data = json.loads(resp.data)

        inventory = data["inventory"]
        assert len(inventory) == 0

if __name__ == '__main__':
    app.secret_key = "Testing secret key"
    unittest.main()
