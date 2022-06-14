# расчет вектора b
def calculate_vector_b(seq_el, d_of_y):
    b = []
    for el in seq_el:
        b.append(d_of_y[el])
    return b


