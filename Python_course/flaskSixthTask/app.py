from flask import Flask
from flask import request
from service import ToDoService


app = Flask(__name__)


@app.route('/todo', methods=["POST"])
def create_todo():
    result = ToDoService().create(request.get_json())
    return result.to_json()


@app.route('/mark', methods=["POST"])
def mark_todo():
    result = ToDoService().mark_done(request.get_json())
    return result.to_json()


@app.route('/get', methods=["GET"])
def get_list():
    result = ToDoService().get_all_undones()
    res = [e.to_json() for e in result]
    return res


if __name__ == '__main__':
    app.run()
