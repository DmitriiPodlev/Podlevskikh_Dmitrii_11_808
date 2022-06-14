# метод для парсинга степеней при х
def get_pows(s):    
    pows = []
    s = s.split('+')
    for el in s:
        if el == ' 1':
            pows.append(0)
        else:
            pows.append(int(el.split('^')[1]))
    
    return pows


