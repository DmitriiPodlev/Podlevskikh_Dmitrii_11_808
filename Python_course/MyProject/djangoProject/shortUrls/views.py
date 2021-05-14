from django.shortcuts import render, get_object_or_404
from django.http import HttpResponseRedirect
from .models import Url
from django.urls import reverse


# create function
def index(request):
    url = Url()
    # if method is post, add data, else open this page
    if request.method == 'POST':
        url.original_url = request.POST['urlName']
        url.short_url = "short"
        url.save()
        return HttpResponseRedirect(reverse('shortUrls:index'))
    else:
        return render(request, 'shortUrls/index.html', {'url': url})


# all links to list function
def links(request):
    # order by frequency
    url_list = Url.objects.order_by('-frequency')
    context = {
        'url_list': url_list
    }
    return render(request, 'shortUrls/links.html', context)


# delete function
def delete(request, id):
    url = get_object_or_404(Url, pk=id)
    url.delete()
    return HttpResponseRedirect(reverse('shortUrls:links'))


# increase frequency of using url function
def increase_by_one(request, id):
    url = get_object_or_404(Url, pk=id)
    url.frequency = url.frequency + 1
    url.save()
    return HttpResponseRedirect(url.original_url)
