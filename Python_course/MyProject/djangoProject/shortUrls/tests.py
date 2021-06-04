from django.test import TestCase, Client
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
        self.assertEquals(response.status_code, 200)
        self.assertTemplateUsed(response, 'shortUrls/links.html')
        self.assertContains(response, "There aren't any urls.")
        self.assertQuerysetEqual(response.context['url_list'], [])

    def test_two_urls(self):
        """
        two urls are displayed on the screen
        """
        create_url("https://ab2", 0, 0)
        create_url("https://k2e", 0, 0)
        response = self.client.get(reverse('shortUrls:links'))
        self.assertEquals(response.status_code, 200)
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
        self.assertEquals(response.status_code, 200)
        self.assertQuerysetEqual(response.context['url_list'],
                                 ['<Url: https://g3q>', '<Url: https://re3>'])


class UrlIndexViewTest(TestCase):

    def test_no_url(self):
        """
        If no url, print only input field in view
        """
        response = self.client.get(reverse('shortUrls:index'))
        self.assertEquals(response.status_code, 200)
        self.assertTemplateUsed(response, 'shortUrls/index.html')
        self.assertIsNone(response.context['url'], None)

    def test_past_url(self):
        """
        If url publish date is very old (more than 3 seconds),
        print only input field in view
        """
        create_url("https://hv4", -20, 0)
        response = self.client.get(reverse('shortUrls:index'))
        self.assertEquals(response.status_code, 200)
        self.assertIsNone(response.context['url'], None)

    def test_future_url(self):
        """
        If url publish date is in the future (more or equal 1 second),
        print only input field in view
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
        self.assertEquals(response.status_code, 200)
        self.assertIsNotNone(response.context['url'])

    def test_post_add_new_url(self):
        """
        add new url to database
        """
        Url.objects.create(short_url="https://jh1", original_url="https://vk.com",
                                  frequency=0, date=timezone.now())
        response = self.client.post(reverse('shortUrls:index'), {
            'urlName': 'https://vk.com'
        })
        link = Url.objects.latest('id')
        self.assertEquals(response.status_code, 302)
        self.assertEquals(link.original_url, "https://vk.com")


class UrlDeleteViewTest(TestCase):

    def test_no_element(self):
        """
        expected error, because there isn't right id
        """
        link = Url.objects.create(short_url="https://jk5", frequency=0, original_url="https://vk.com",
                                  date=timezone.now())
        link_id = link.id + 1
        response = self.client.post(reverse('shortUrls:delete', args=(link_id,)))
        self.assertEqual(response.status_code, 404)

    def test_delete_element(self):
        """
        delete element
        """
        link = Url.objects.create(short_url="https://jk5", frequency=0, original_url="https://vk.com",
                                  date=timezone.now())
        response = self.client.post(reverse('shortUrls:delete', args=(link.id,)))
        self.assertEquals(response.status_code, 302)
        self.assertEquals(Url.objects.count(), 0)


class UrlCountViewTest(TestCase):

    def test_error_id(self):
        """
        expected error, because there isn't right id
        """
        link = Url.objects.create(short_url="https://jk5", frequency=0, original_url="https://vk.com",
                                  date=timezone.now())
        response = self.client.post(reverse('shortUrls:count', args=(10,)))
        self.assertEquals(response.status_code, 404)

    def test_count_element(self):
        """
        check that the frequency has increased by 1
        """
        link = Url.objects.create(short_url="https://jk5", frequency=0, original_url="https://vk.com",
                                  date=timezone.now())
        response = self.client.post(reverse('shortUrls:count', args=(link.id,)))
        self.assertEquals(response.status_code, 302)
        new_url = Url.objects.all().filter(original_url=response.url)
        self.assertEquals(new_url[0].frequency, 1)

    def test_redirect_of_url(self):
        """
        method redirects to original url
        """
        link = Url.objects.create(short_url="https://jk5", frequency=0, original_url="https://vk.com",
                                  date=timezone.now())
        response = self.client.post(reverse('shortUrls:count', args=(link.id,)))
        self.assertEquals(response.status_code, 302)
        self.assertEquals(response.url, "https://vk.com")


class HashTest(TestCase):

    def test_hash(self):
        """
        Hash function is making a short link
        """
        hashset = Hash()
        short_url = hashset.hash_url(1)
        self.assertIsNotNone(short_url)

    def test_length(self):
        """
        Hash's length is 12
        """
        hashset = Hash()
        short_url = hashset.hash_url(1)
        self.assertEquals(len(short_url), 12)

    def test_number(self):
        """
        if number is not int, print short url as empty string
        """
        hashset = Hash()
        short_url = hashset.hash_url("1")
        self.assertEquals(short_url, "")

    def test_number_as_float(self):
        """
        if number is not int, print short url as empty string
        """
        hashset = Hash()
        short_url = hashset.hash_url(1.1)
        self.assertEquals(short_url, "")

    def test_different_hash_for_equal_number(self):
        """
        The same links give different short urls
        """
        hashset = Hash()
        self.assertNotEquals(hashset.hash_url(1), hashset.hash_url(1))
