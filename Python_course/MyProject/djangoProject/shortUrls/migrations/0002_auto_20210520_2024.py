# Generated by Django 2.2.5 on 2021-05-20 17:24

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('shortUrls', '0001_initial'),
    ]

    operations = [
        migrations.AlterField(
            model_name='url',
            name='short_url',
            field=models.CharField(max_length=30),
        ),
    ]