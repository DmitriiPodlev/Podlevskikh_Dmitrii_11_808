from hashids import Hashids
import os


class Hash:
    # hash function
    def hash_url(self, number):
        # check that number is int
        if type(number) != int:
            return ""
        # create salt to avoid collisions
        salt = os.urandom(32)
        # create hashids
        hashids = Hashids(salt=str(salt), min_length=4)
        # get key of hash
        key = hashids.encode(number)
        short_url = "https://" + key
        return short_url
