def decorator(func):
    def wrapper(*args):
        res = func(*args)
        print(f'Most frequency word is {res[0]} with frequency = {res[1]}')
        return res
    return wrapper


@decorator
def get_most_frequency_word(text):
    words = text.split(' ')
    d = dict((i, words.count(i)) for i in set(words))
    maxkey = max(d, key=d.get)
    return [maxkey, d[maxkey]]


song = 'Never gonna give you up Never gonna let you down Never gonna run around Never up'
print(get_most_frequency_word(song))
