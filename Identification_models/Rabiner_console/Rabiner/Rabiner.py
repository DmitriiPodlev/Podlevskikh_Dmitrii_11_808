from vector_pi import get_vector_pi
from vectors_a import get_a_vectors
from probability import calculate_p

# главный метод расчета принадлежности последовательности к матрице
print('Введите последовательность:')
inp = input().split()
q = [int(i) for i in inp]

ar_mat = []
ar_name = []
matrix = []
max_p = 0
max_i = ''

# считываем данные из файла
print('\n')
file = open('Matrixs.txt', 'r', encoding='utf-8')
s = file.readlines()
for i in range(len(s)):
    if s[i] != '\n':
        if i % 8 == 0:
            ar_name.append(s[i].split('\n')[0])
        else:
            ar = []
            for el in s[i].split('\n')[0].split(' '):
                ar.append(float(el))
            matrix.append(ar)
            if len(matrix) == 5:
                ar_mat.append(matrix)
                matrix = []

# считаем вероятность принадлежности последовательности к каждому классу
for i in range(11):
    ar_a = []
    pi = get_vector_pi(ar_mat[i], q)
    ar_a.append(pi)
    ar_a = get_a_vectors(ar_mat[i], q, ar_a)
    p = calculate_p(ar_a) 
    if p > max_p:
        max_p = p
        max_i = i
    print('Вероятность принадлежности последовательности к классу {} равна {}'.format(ar_name[i],p))

print('\nС наибольшей вероятностью, равной {}, последовательность {} принадлежит классу {}'.format(\
    max_p, q, ar_name[max_i]))
