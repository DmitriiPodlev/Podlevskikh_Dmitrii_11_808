# Generated by Django 2.2.5 on 2021-05-26 15:44

import datetime
from django.db import migrations, models
from django.utils.timezone import utc


class Migration(migrations.Migration):

    dependencies = [
        ('shortUrls', '0005_auto_20210526_1743'),
    ]

    operations = [
        migrations.AlterField(
            model_name='url',
            name='date',
            field=models.DateTimeField(default=datetime.datetime(2021, 5, 26, 15, 44, 58, 249939, tzinfo=utc)),
        ),
    ]
