# расчет начального вектора pi
def get_vector_pi(matrix, q):
    
    pi = []
    l = len(matrix[0])

    for i in range(0, l):
        if i == q[0]:
            pi.append(1)
        else:
            pi.append(0)
    
    return pi


