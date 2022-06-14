# расчет матрицы по параметрам
def calculate_matrix(m, bigram, count_of_elements):
    matrix = []
    ar = []

    for i in range(0, m):
        for j in range(0, m):
            if f'{i}{j}' in bigram.keys(): 
                ar.append(bigram[f'{i}{j}']/count_of_elements[i])
            else:
                ar.append(0)
        matrix.append(ar)
        ar = []
    return matrix


