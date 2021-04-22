class ToDoModel:
    def __init__(self, title, description, created_on, due_date):
        self.title = title
        self.description = description
        self.createdOn = created_on
        self.dueDate = due_date
        self._is_done = False

    def to_json(self):
        return {'title': self.title,
                'description': self.description,
                'createdOn': self.createdOn,
                'dueDate': self.dueDate,
                'isDone': self._is_done}


class ToDoDB:
    def __init__(self):
        self.todos = []

    def create(self, title, description, created_on, due_date):
        todo = ToDoModel(title, description, created_on, due_date)
        self.todos.append(todo)
        return todo

    def update_is_done(self, title):
        for e in self.todos:
            if e.title == title:
                e._is_done = True
                todo = e
        return todo

    def get_undone_todos(self):
        res = []
        for e in self.todos:
            if e._is_done == False:
                res.append(e)
        return res

