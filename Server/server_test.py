import unittest

from backend import app

class HandlerTestCase(unittest.TestCase):
    def setUp(self):
        self.app = app.test_client()

    def tearDown(self):
        pass

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

if __name__ == '__main__':
    unittest.main()
