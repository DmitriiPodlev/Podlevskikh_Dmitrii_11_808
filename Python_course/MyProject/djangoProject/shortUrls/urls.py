from django.urls import path

from . import views


app_name = 'shortUrls'
urlpatterns = [
    #/shortUrls/
    path('', views.index, name='index'),
    #/shortUrls/links/
    path('links/', views.links, name='links'),
    #/shortUrls/id/delete/
    path('<int:id>/delete/', views.delete, name='delete'),
    #/shortUrls/id/count/
    path('<int:id>/count/', views.increase_by_one, name='count')
]
