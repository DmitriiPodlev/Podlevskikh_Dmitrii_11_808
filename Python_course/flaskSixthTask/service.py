from models import ToDoDB


class ToDoService:
    def __init__(self):
        self.db = ToDoDB()

    def create(self, params):
        return self.db.create(params['title', 'description', 'createdOn', 'dueDate'])

    def mark_done(self, params):
        return self.db.update_is_done(params['title'])

    def get_all_undone(self):
        return self.db.get_undone_todos()
