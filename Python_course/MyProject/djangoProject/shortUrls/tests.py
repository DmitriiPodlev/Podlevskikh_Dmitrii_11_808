from django.test import TestCase
from .models import Url
import datetime
from django.utils import timezone
from django.urls import reverse
from .hash_dictionary import Hash


class UrlModelTest(TestCase):

    def test_was_published_with_future_time(self):
        """
        was_published_with_future_time() returns False for url whose date is in the future
        time_delta is 3 seconds after creating in the method was_published_recently()
        """
        time = timezone.now() + datetime.timedelta(seconds=1)
        url = Url(date=time)
        self.assertIs(url.was_published_recently(), False)

    def test_was_published_with_past_time(self):
        """
        was_published_with_past_time() returns False for url whose date is in the past
        time_delta is 3 seconds after creating in the method was_published_recently()
        """
        time = timezone.now() - datetime.timedelta(seconds=4)
        url = Url(date=time)
        self.assertIs(url.was_published_recently(), False)

    def test_was_published_with_present_time(self):
        """
        was_published_with_present_time() returns True for url whose date is in the present
        time_delta is 3 seconds after creating in the method was_published_recently()
        """
        time = timezone.now() - datetime.timedelta(seconds=2)
        url = Url(date=time)
        self.assertIs(url.was_published_recently(), True)


def create_url(url, seconds, frequency):
    """
    Create url with params like short_url, frequency of using url and the given number of the seconds
    (negative for urls in the past and positive for the future)
    """
    time = timezone.now() + datetime.timedelta(seconds=seconds)
    Url.objects.create(short_url=url, date=time, frequency=frequency)


class UrlLinksViewTest(TestCase):

    def test_no_urls(self):
        """
        If no urls, print message about it
        """
        response = self.client.get(reverse('shortUrls:links'))
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "There aren't any urls.")
        self.assertQuerysetEqual(response.context['url_list'], [])

    def test_two_urls(self):
        """
        two urls are displayed on the screen
        """
        create_url("https://ab2", 0, 0)
        create_url("https://k2e", 0, 0)
        response = self.client.get(reverse('shortUrls:links'))
        self.assertEqual(response.status_code, 200)
        self.assertQuerysetEqual(response.context['url_list'],
                                 ['<Url: https://ab2>', '<Url: https://k2e>'])

    def test_check_frequency(self):
        """
        two urls are displayed order by frequency
        (with 3, then with 2)
        """
        create_url("https://re3", 0, 2)
        create_url("https://g3q", 0, 3)
        response = self.client.get(reverse('shortUrls:links'))
        self.assertEqual(response.status_code, 200)
        self.assertQuerysetEqual(response.context['url_list'],
                                 ['<Url: https://g3q>', '<Url: https://re3>'])


class UrlIndexViewTest(TestCase):

    def test_no_url(self):
        """
        If no url, print only input field in view
        """
        response = self.client.get(reverse('shortUrls:index'))
        self.assertEqual(response.status_code, 200)
        self.assertIsNone(response.context['url'], None)

    def test_past_url(self):
        """
        If url publish date is very old (more than 3 seconds), print only input field in view
        """
        create_url("https://hv4", -20, 0)
        response = self.client.get(reverse('shortUrls:index'))
        self.assertEqual(response.status_code, 200)
        self.assertIsNone(response.context['url'], None)

    def test_future_url(self):
        """
        If url publish date is in the future (more or equal 1 second), print only input field in view
        """
        create_url("https://hv4", 20, 0)
        response = self.client.get(reverse('shortUrls:index'))
        self.assertEqual(response.status_code, 200)
        self.assertIsNone(response.context['url'], None)

    def test_recent_url(self):
        """
        If url publish date is recent, print it's short_url
        """
        create_url("https://hv4", 0, 0)
        response = self.client.get(reverse('shortUrls:index'))
        self.assertEqual(response.status_code, 200)
        self.assertIsNotNone(response.context['url'])


class HashTest(TestCase):

    def test_hash(self):
        """
        Hash function is making a short link
        """
        hashset = Hash()
        short_url = hashset.hash_url("https://vk.com", 1)
        self.assertIsNotNone(short_url)
