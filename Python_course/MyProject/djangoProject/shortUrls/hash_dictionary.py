from hashids import Hashids
import os


class Hash:

    def hash_url(self, id):
        salt = os.urandom(32)
        hashids = Hashids(salt=str(salt), min_length=4)
        key = hashids.encode(id)
        short_url = "https://" + key
        return short_url
