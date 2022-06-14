# расчет начального вектора
def create_pi_vector(m, d, num, N):
    pi = []
    count_of_elements = []

    for i in range(0, m-d):
        pi.append(num/N)
        count_of_elements.append(num)
    
    for i in range(0, d):
        pi.append((num+1)/N)
        count_of_elements.append(num+1)        
    return (pi, count_of_elements)


