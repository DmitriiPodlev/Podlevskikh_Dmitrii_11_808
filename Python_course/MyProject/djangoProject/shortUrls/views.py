from django.shortcuts import render, get_object_or_404
from django.http import HttpResponse, Http404
from .models import Url


def index(request):
    return HttpResponse('Index page!')


def links(request):
    url_list = Url.objects.order_by('-frequency')
    context = {
        'url_list': url_list
    }
    return render(request, 'shortUrls/links.html', context)


def delete(request, url_id):
    url = get_object_or_404(Url, pk=url_id)
    return
