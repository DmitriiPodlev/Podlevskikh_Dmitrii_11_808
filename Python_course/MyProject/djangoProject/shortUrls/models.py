from django.db import models


class Url(models.Model):
    short_url = models.CharField(max_length=10)
    original_url = models.CharField(max_length=500)
    frequency = models.IntegerField(default=0)

    def __str__(self):
        return self.short_url
