# расчет биграм
def get_bigrams(b):
    bigram = {}

    for i in range(0, len(b)):
        if i == len(b) - 1:
            t = 0
        else:
            t = i + 1
        if f'{b[i]}{b[t]}' in bigram.keys():
            bigram[f'{b[i]}{b[t]}'] += 1
        else:
            bigram[f'{b[i]}{b[t]}'] = 1
    return bigram


