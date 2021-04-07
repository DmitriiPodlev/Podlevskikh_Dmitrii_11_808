from functools import reduce
import pickle

class InvalidTransactionException(Exception):
    pass


class Manager:
    def __init__(self):
        pass

    def transform_to_pickle(self, data):
        with open('metadata.pkl', 'wb') as f:
            pickle.dump(data, f)
        with open('metadata.pkl', 'rb') as f:
            data_new = pickle.load(f)
        print(data_new)

    def print_transactions(self, dict):
        sum = 0
        pickle_data = {}
        for key, value in dict.items():
            trans_sum = reduce(lambda x, y: x + y, value)
            print(f'{key}: {value}, sum is {trans_sum}')
            pickle_data[key] = trans_sum
            sum = sum + trans_sum
        print(f'Total sum is {sum}')
        self.transform_to_pickle(pickle_data)

    def int_input(self, amount_of_money):
        while True:
            try:
                int_amount = int(amount_of_money)
                return int_amount
                break
            except ValueError as e:
                print(f'No valid argument! Error: {e}')
                raise InvalidTransactionException('Failed parse to integer', e)

    def parse_transactions(self):
        dict = {}
        input_data = input("Input transaction:")
        while input_data != "quit":
            data = input_data.split(' ')
            type_of_transaction = data[0]
            for i in range(1, len(data) - 1):
                type_of_transaction = type_of_transaction + " " + data[i]
            amount_of_money = data[len(data) - 1]
            amount_of_money = self.int_input(amount_of_money)
            if type_of_transaction in dict.keys():
                dict[type_of_transaction].append(amount_of_money)
            else:
                dict[type_of_transaction] = [amount_of_money]
            input_data = input()
        self.print_transactions(dict)