from django.db import models


# url model: short url, original url and frequency of using url
class Url(models.Model):
    short_url = models.CharField(max_length=30)
    original_url = models.CharField(max_length=500)
    frequency = models.IntegerField(default=0)

    def __str__(self):
        return self.short_url
