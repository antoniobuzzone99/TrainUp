from flask import request, Flask

from TrainUp_Server.src.Statistiche import cards_most_used, numberExe, averageAge
from home import home_card_displayer, Load_exercise, LoadCardFromDb, add_favorite_card, remove_favorite_card
from NewCard import add_exercise, confirm_creation_card, delete_trainingCard, clear_list_exercise
from models.user import db
from auth import user_login, user_register, user_logout, update_data, update_password

SECRET_KEY = "mysecretkey"

app = Flask(__name__)
app.config['SECRET_KEY'] = SECRET_KEY
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql://root:12345@localhost:3309/TrainUp'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['SESSION_TYPE'] = 'filesystem'
db.init_app(app)

@app.route("/login", methods=['GET', 'POST'])
def login():
    data = request.get_json()
    return user_login(data)

@app.route("/register", methods=['GET', 'POST'])
def register():
    data = request.get_json()
    with app.app_context():
        return user_register(data)

@app.route("/logout", methods=['GET', 'POST'])
def logout():
    data = request.get_json()
    return user_logout(data)

@app.route("/home", methods=['GET', 'POST'])
def home():
    return home_card_displayer()
@app.route("/loadExer", methods=['GET', 'POST'])
def loadExer():
    return Load_exercise()

@app.route("/update_data", methods=['GET', 'POST'])
def update():
    data = request.get_json()
    return update_data(data)

@app.route("/update_password", methods=['GET', 'POST'])
def updatePass():
    data = request.get_json()
    return update_password(data)

@app.route("/add_exe_card", methods=['GET', 'POST'])
def addExe():
    data = request.get_json()
    return add_exercise(data)

@app.route("/confirm_creation_card", methods=['GET', 'POST'])
def confrim():
    data = request.get_json()
    return confirm_creation_card(data)

@app.route("/clear_list", methods=['GET', 'POST'])
def clear():
    return clear_list_exercise()

@app.route("/LoadCardFromDb", methods=['GET', 'POST'])
def LoadCard():
    data = request.get_json()
    return LoadCardFromDb(data)

@app.route("/delete_trainingCard", methods=['GET', 'POST'])
def delete():
    data = request.get_json()
    return delete_trainingCard(data)

@app.route("/add_favorite_card", methods=['GET', 'POST'])
def addFav():
    data = request.get_json()
    return add_favorite_card(data)

@app.route("/remove_favorite_card", methods=['GET', 'POST'])
def removeFav():
    data = request.get_json()
    return remove_favorite_card(data)

@app.route("/cards_most_used", methods=['GET', 'POST'])
def statFav():
    return cards_most_used()

@app.route("/numberExe", methods=['GET', 'POST'])
def num_Exe():
    return numberExe()

@app.route("/ave_age", methods=['GET', 'POST'])
def ave_Age():
    return averageAge()


@app.before_request
def init_db():
    with app.app_context():
        db.create_all()
        db.session.commit()


if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5000)