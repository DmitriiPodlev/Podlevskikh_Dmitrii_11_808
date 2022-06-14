# расчет промежуточных а-векторов
def get_a_vectors(matrix, q, ar):

    res = []
    s = 0

    for i in range(1, len(q)):
        for j in range(0, len(matrix)):
            if q[i] == j:
                for k in range(0, len(ar[0])):
                    s = s + ar[i-1][k] * matrix[k][j]
                res.append(s)
                s = 0
            else:
                res.append(0)
        ar.append(res)
        res = []
    
    return ar


