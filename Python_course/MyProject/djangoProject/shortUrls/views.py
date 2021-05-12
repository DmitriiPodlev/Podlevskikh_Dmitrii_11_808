from django.shortcuts import render, get_object_or_404
from django.http import HttpResponseRedirect
from .models import Url
from django.urls import reverse


def index(request):
    url = Url()
    if request.method == 'POST':
        url.original_url = request.POST['urlName']
        url.short_url = "short"
        url.save()
        return HttpResponseRedirect(reverse('shortUrls:index'))
    else:
        return render(request, 'shortUrls/index.html', {'url': url})


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

