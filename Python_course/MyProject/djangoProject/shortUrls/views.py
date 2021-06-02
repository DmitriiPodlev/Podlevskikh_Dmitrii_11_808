from django.shortcuts import render, get_object_or_404
from django.http import HttpResponseRedirect
from .models import Url
from django.urls import reverse
from .hash_dictionary import Hash
from django.utils import timezone


# create function
def index(request):
    # if method is post, add data, else open this page
    if request.method == 'POST':
        hash_dict = Hash()
        url = Url()
        url.short_url = hash_dict.hash_url(Url.objects.count() + 1)
        url.original_url = request.POST['urlName']
        url.date = timezone.now()
        url.save()
        return HttpResponseRedirect(reverse('shortUrls:index'))
    else:
        if Url.objects.count() == 0:
            return render(request, 'shortUrls/index.html', {'url': None})
        last_url = Url.objects.latest('id')
        if last_url.was_published_recently():
            return render(request, 'shortUrls/index.html', {'url': last_url})
        else:
            return render(request, 'shortUrls/index.html', {'url': None})


# all links to list function
def links(request):
    # order by frequency
    url_list = Url.objects.order_by('-frequency')
    return render(request, 'shortUrls/links.html', {'url_list': url_list})


# delete function
def delete(request, id):
    url = get_object_or_404(Url, pk=id)
    url.delete()
    return HttpResponseRedirect(reverse('shortUrls:links'))


# increase frequency of using url function
def count(request, id):
    hash_dict = Hash()
    url = get_object_or_404(Url, pk=id)
    url.frequency = url.frequency + 1
    url.save()
    return HttpResponseRedirect(url.original_url)
