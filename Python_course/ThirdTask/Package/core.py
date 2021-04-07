import pickle


def write_to_file():
    count_of_lines = 0
    text = []
    file = input("write filename - ")
    input_text = input()
    while input_text != "quit":
        text.append(input_text)
        input_text = input()
    f = open(file + ".txt", 'w')
    for line in text:
        f.write(line + '\n')
        count_of_lines = count_of_lines + 1
    f.close()

    data = {
        'filename': file + ".txt",
        'count of lines': count_of_lines
    }
    with open('metadata.pkl', 'wb') as f:
        pickle.dump(data, f)
    with open('metadata.pkl', 'rb') as f:
        data_new = pickle.load(f)
    print(data_new)

    file_name = data['filename']
    f = open(file_name, 'r')
    for line in f:
        print(line)
    f.close()
