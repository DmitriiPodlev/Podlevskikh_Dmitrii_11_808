# расчет частот втречаемости элементов
def calculate_dictionary_of_numbers_in_intervals(count_of_elements, numbers):
    d_of_y = {}
    counter = 0

    for i in range(0, len(count_of_elements)):
        for j in range(0, count_of_elements[i]):
            d_of_y[numbers[counter]] = i
            counter += 1
    return d_of_y


