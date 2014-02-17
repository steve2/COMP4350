
from flask import Flask
app = Flask(__name__)

#import other files that use 'app' here:
import backend.templates
import backend.handlers
