# Generated by Django 2.2.5 on 2021-05-26 16:04

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('shortUrls', '0008_auto_20210526_1902'),
    ]

    operations = [
        migrations.AlterField(
            model_name='url',
            name='date',
            field=models.DateTimeField(verbose_name='publish date'),
        ),
    ]
