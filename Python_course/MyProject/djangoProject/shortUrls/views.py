from django.shortcuts import render, get_object_or_404
from django.http import HttpResponseRedirect
from .models import Url
from django.urls import reverse


def index(request):
    if request.method == 'POST':
        url = Url()
        url.original_url = request.POST['urlName']
        url.save()
        return HttpResponseRedirect(reverse('shortUrls:links'))
    else:
        return render(request, 'shortUrls/index.html')


def links(request):
    url_list = Url.objects.order_by('-frequency')
    context = {
        'url_list': url_list
    }
    return render(request, 'shortUrls/links.html', context)


def delete(request, id):
    url = get_object_or_404(Url, pk=id)
    url.delete()
    return HttpResponseRedirect(reverse('shortUrls:links'))

