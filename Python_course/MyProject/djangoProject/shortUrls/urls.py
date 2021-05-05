from django.urls import path

from . import views


app_name = 'shortUrls'
urlpatterns = [
    path('', views.index, name='index'),
    path('links/', views.links, name='links'),
    path('<int:id>/delete/', views.delete, name='delete')
]
