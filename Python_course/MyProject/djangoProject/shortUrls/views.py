from django.shortcuts import render, get_object_or_404
from django.http import HttpResponseRedirect
from .models import Url
from django.urls import reverse
from .hash_dictionary import Hash


# create function
def index(request):
    # if method is post, add data, else open this page
    if request.method == 'POST':
        url = Url()
        url.short_url = Hash.hash_url(Hash, request.POST['urlName'], 1)
        url.save()
        return HttpResponseRedirect(reverse('shortUrls:index'))
    else:
        url = Url.objects.latest('id')
        if url.frequency == 0:
            return render(request, 'shortUrls/index.html', {'url': url})
        else:
            return render(request, 'shortUrls/index.html', {'url': None})


# all links to list function
def links(request):
    # order by frequency
    url_list = Url.objects.order_by('-frequency')
    for url in url_list:
        url.original_url = Hash.decode_url(Hash, url.short_url)
        url.save()
    return render(request, 'shortUrls/links.html', {'url_list': url_list})


# delete function
def delete(request, id):
    url = get_object_or_404(Url, pk=id)
    url.delete()
    return HttpResponseRedirect(reverse('shortUrls:links'))


# increase frequency of using url function
def count(request, id):
    url = get_object_or_404(Url, pk=id)
    url.frequency = url.frequency + 1
    url.save()
    original_url = Hash.decode_url(Hash, url.short_url)
    return HttpResponseRedirect(original_url)
