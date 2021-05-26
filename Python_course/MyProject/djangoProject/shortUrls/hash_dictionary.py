from hashids import Hashids
import os


class Hash:
    dict = {}

    def hash_url(self, url, id):
        salt = os.urandom(32)
        hashids = Hashids(salt=str(salt), min_length=4)
        key = hashids.encode(id)
        short_url = "https://" + key
        Hash.dict[short_url] = url
        return short_url

    def decode_url(self, short_url):
        url = Hash.dict[short_url]
        return url
