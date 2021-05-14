import hashlib
import os

"""
hash-code in progress
"""
def hash_url(url):
    salt = os.urandom(32)
    key = hashlib.pbkdf2_hmac('sha256', url.encode('utf-8'), salt, 100000)
