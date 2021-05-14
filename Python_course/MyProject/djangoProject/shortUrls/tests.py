from django.test import TestCase
from .models import Url

# tests in progress
class UrlModelTest(TestCase):

    def test_was_published_with_lowest_frequency(self):
        """
        was_published_with_lowest_frequency() returns False for url whose frequency in lowest
        """
        url = Url(frequency=1)
        # In progress
