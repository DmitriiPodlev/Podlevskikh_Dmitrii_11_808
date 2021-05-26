from django.db import models
from django.utils import timezone
import datetime


# url model: short url, original url and frequency of using url
class Url(models.Model):
    short_url = models.CharField(max_length=30)
    original_url = models.CharField(max_length=500)
    frequency = models.IntegerField(default=0)
    date = models.DateTimeField('publish date')

    def __str__(self):
        return self.short_url

    def was_published_recently(self):
        now_time = timezone.now()
        return now_time - datetime.timedelta(seconds=3) <= self.date <= now_time
